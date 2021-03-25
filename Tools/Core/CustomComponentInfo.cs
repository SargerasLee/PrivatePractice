using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;
using Tools.Attributes;
using Tools.Exceptions;

namespace Tools.Core
{
	internal class CustomComponentInfo
	{
		public string ClassFullName { get; private set; }

		private readonly object realCustomComponent;
		private readonly Dictionary<string, MethodInfo> MethodDict;
		public CustomComponentInfo(object comp)
		{
			realCustomComponent = comp;
			ClassFullName = realCustomComponent.GetType().FullName;
			MethodDict = new Dictionary<string, MethodInfo>();
			MethodInfo[] methodInfos = realCustomComponent.GetType().GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
			foreach (MethodInfo info in methodInfos)
			{
				RouteMappingAttribute routeMapping = info.GetCustomAttribute<RouteMappingAttribute>(false);
				if (routeMapping != null)
					MethodDict.Add(routeMapping.Value, info);
			}
		}

		public object Match(string route, params object[] objs)
		{
			route = route.Trim();
			string[] sp = route.Split('?');
			string[] keyValues;
			if (sp.Length > 1)
			{
				keyValues = sp[1].Split('&');
				Dictionary<string, string> urlParams = new Dictionary<string, string>(8);
				foreach (string kv in keyValues)
				{
					string[] s = kv.Split('=');
					urlParams.Add(s[0], s[1]);
				}
			}
			Regex.IsMatch(route, "{[a-zA-Z0-9_]+}");
			string targetKey = MethodDict.Keys.Where(key => key == route).FirstOrDefault();
			if (string.IsNullOrWhiteSpace(targetKey))
				throw new RouteNotMatchException("未匹配方法");
			ParameterInfo[] parameters = MethodDict[targetKey].GetParameters();
			foreach(ParameterInfo info in parameters)
			{
				UrlParamAttribute param = info.GetCustomAttribute<UrlParamAttribute>(false);
				RouteVariableAttribute variable = info.GetCustomAttribute<RouteVariableAttribute>(false);
			}
			object o = MethodDict[targetKey].Invoke(realCustomComponent, objs);
			return o;
		}
	}
}
