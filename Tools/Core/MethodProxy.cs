using System.Collections.Generic;
using System.Reflection;
using Tools.Attributes;

namespace Tools.Core
{
	internal class MethodProxy
	{
		private readonly object targetObj;
		private readonly MethodInfo methodInfo;
		private readonly Dictionary<int, ParameterInfo> parametersDict = new Dictionary<int, ParameterInfo>();
		private readonly int parametersCount;
		public MethodProxy(object obj, MethodInfo method)
		{
			targetObj = obj;
			methodInfo = method;
			ParameterInfo[] parameters = methodInfo.GetParameters();
			parametersCount = parameters == null || parameters.Length <= 0 ? 0 : parameters.Length;
			foreach (ParameterInfo info in parameters)
			{
				parametersDict[info.Position] = info;
			}
		}

		public object Invoke(Dictionary<string, string> urlParams, Dictionary<string, string> routeParams, params object[] objs)
		{
			
			object[] paramObjects = null;
			RouteContext context = new RouteContext(urlParams, routeParams, objs);
			if (parametersCount > 0)
			{
				paramObjects = new object[parametersCount];
			}
				
			if(urlParams!=null && urlParams.Count>0 || routeParams != null && routeParams.Count > 0)
			{
				for(int i=0;i< parametersCount; i++)
				{
					if(parametersDict[i].ParameterType==typeof(RouteContext))
					{
						paramObjects[i] = context;
						continue;
					}
					UrlParamAttribute urlParam = parametersDict[i].GetCustomAttribute<UrlParamAttribute>(false);
					if(urlParam != null)
					{
						string paramName = urlParam.Name;
						if(string.IsNullOrWhiteSpace(paramName))
						{
							paramObjects[i] = urlParams[paramName];
						}
						else
						{
							paramObjects[i] = urlParams[parametersDict[i].Name];
						}
					}
					RouteVariableAttribute routeParam = parametersDict[i].GetCustomAttribute<RouteVariableAttribute>(false);
					if (routeParam != null)
					{
						string paramName = routeParam.Name;
						if (string.IsNullOrWhiteSpace(paramName))
						{
							paramObjects[i] = urlParams[paramName];
						}
						else
						{
							paramObjects[i] = urlParams[parametersDict[i].Name];
						}
					}
				}
			}
			return methodInfo.Invoke(targetObj, paramObjects);
		}
	}
}
