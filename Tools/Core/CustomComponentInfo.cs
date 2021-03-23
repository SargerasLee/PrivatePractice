using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tools.Attributes;
using Tools.Exceptions;

namespace Tools.Core
{
	public class CustomComponentInfo
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

		public object Invoke(string route, params object[] objs)
		{
			string targetKey = MethodDict.Keys.Where(key => key == route).FirstOrDefault();
			if (string.IsNullOrWhiteSpace(targetKey))
				throw new RouteNotMatchException("未匹配方法");
			object o = MethodDict[targetKey].Invoke(realCustomComponent, objs);
			return o;
		}
	}
}
