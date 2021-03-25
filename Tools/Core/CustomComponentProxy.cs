using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Tools.Attributes;

namespace Tools.Core
{
	internal class CustomComponentProxy
	{
		public string Id { get; private set; }

		private readonly object realCustomComponent;
		private readonly Dictionary<string, MethodProxy> MethodDict;
		public CustomComponentProxy(object comp, string id)
		{
			realCustomComponent = comp;
			Id = id;
			MethodDict = new Dictionary<string, MethodProxy>();
			MethodInfo[] methodInfos = realCustomComponent.GetType().GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
			RouteMappingAttribute routeMapping;
			foreach (MethodInfo info in methodInfos)
			{
				routeMapping = info.GetCustomAttribute<RouteMappingAttribute>(false);
				if (routeMapping != null)
					MethodDict.Add(routeMapping.Value, new MethodProxy(realCustomComponent,info));
			}
		}

		public object Match(string route, params object[] objs)
		{
			route = route.Trim();
			string url = route, kv = string.Empty;
			SplitUrl(route, ref url, ref kv);
			
			Dictionary<string, string> urlParams = ExtractUrlParams(kv);
			Dictionary<string, string> routeParams = ExtractRouteParams(url);
			
			object o = MethodDict[""].Invoke(urlParams, routeParams, objs);
			return o;
		}

		private Dictionary<string, string> ExtractRouteParams(string url)
		{
			MatchCollection mc = Regex.Matches(url, "{[a-zA-Z0-9_]+}");
			string targetKey = MethodDict.Keys.Where(key =>
			{
				return key == url;
			}
			).FirstOrDefault();
			return null;
		}

		private static void SplitUrl(string route, ref string url, ref string kv)
		{
			int mark = route.IndexOf('?');
			if (mark > 0)
			{
				url = route.Substring(0, mark);
				if (mark < route.Length - 1)
					kv = route.Substring(mark + 1);
			}
		}

		private static Dictionary<string, string> ExtractUrlParams(string kvStr)
		{
			kvStr = kvStr.Trim();
			if (string.IsNullOrWhiteSpace(kvStr)) 
				return null;
			string[] keyValues = kvStr.Split('&');
			Dictionary<string, string> urlParams = new Dictionary<string, string>();
			string[] s;
			foreach (string kv in keyValues)
			{
				if (string.IsNullOrWhiteSpace(kv.Trim()))
					continue;
				s = kv.Split('=');
				if (string.IsNullOrWhiteSpace(s[0].Trim()))
					continue;
				urlParams.Add(s[0], s[1]);
			}
			if (urlParams.Count <= 0)
				return null;
			return urlParams;
		}
	}
}
