using System.Collections.Generic;
using System.Reflection;
using Tools.Attributes;

namespace Tools.Core
{
	public class CustomComponentInfo
	{
		public string ClassFullName { get; set; }

		private readonly object realCustomComponent;
		public Dictionary<string, MethodInfo> MethodDict { get; set; }
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
			object o = null;
			foreach (string key in MethodDict.Keys)
			{
				if (route.StartsWith(key))
				{
					o = MethodDict[key].Invoke(realCustomComponent, objs);
				}
			}
			return o;
		}
	}
}
