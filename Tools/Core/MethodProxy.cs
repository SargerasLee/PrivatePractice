using Newtonsoft.Json;
using System;
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
			Dictionary<Type, Dictionary<string, string>> paramDict = new Dictionary<Type, Dictionary<string, string>>();
			paramDict.Add(typeof(UrlParamAttribute), urlParams);
			paramDict.Add(typeof(RouteParamAttribute), routeParams);
			if(urlParams!=null && urlParams.Count>0 || routeParams != null && routeParams.Count > 0)
			{
				for(int i=0;i< parametersCount; i++)
				{
					if (parametersDict[i].ParameterType == typeof(RouteContext))
					{
						paramObjects[i] = context;
						continue;
					}

					AssembleParam<UrlParamAttribute>(paramDict, paramObjects, i);
					AssembleParam<RouteParamAttribute>(paramDict, paramObjects, i);
				}
			}
			JsonAttribute jsonAttribute = methodInfo.GetCustomAttribute<JsonAttribute>(false);
			object obj = methodInfo.Invoke(targetObj, paramObjects);
			if (jsonAttribute != null)
				obj = JsonConvert.SerializeObject(obj);
			return obj;
		}

		private void AssembleParam<T>(Dictionary<Type, Dictionary<string, string>> paramDict, object[] paramObjects, int i) where T : ParameterAttribute
		{
			ParameterAttribute paramAttr = parametersDict[i].GetCustomAttribute<T>(false);
			if (paramAttr != null)
			{
				string paramName = paramAttr.Name;
				if (!string.IsNullOrWhiteSpace(paramName))
				{
					paramObjects[i] = ConvertParamToBasicType(parametersDict[i].ParameterType, paramDict[paramAttr.GetType()][paramName]);
				}
				else
				{
					paramObjects[i] = ConvertParamToBasicType(parametersDict[i].ParameterType, paramDict[paramAttr.GetType()][parametersDict[i].Name]);
				}
			}
		}

		private object ConvertParamToBasicType(Type t, string value)
		{

			if (t == typeof(int)) return System.Convert.ToInt32(value);
			else if (t == typeof(long)) return System.Convert.ToInt64(value);
			else if (t == typeof(short)) return System.Convert.ToInt16(value);
			else if (t == typeof(decimal)) return System.Convert.ToDecimal(value);
			else if (t == typeof(bool)) return System.Convert.ToBoolean(value);
			else if (t == typeof(DateTime)) return System.Convert.ToDateTime(value);
			else if (t == typeof(char)) return System.Convert.ToChar(value);
			else if (t == typeof(double)) return System.Convert.ToDouble(value);
			else if (t == typeof(float)) return System.Convert.ToSingle(value);
			else if (t == typeof(byte)) return System.Convert.ToByte(value);
			else if (t == typeof(sbyte)) return System.Convert.ToSByte(value);
			else return value;
		}
	}
}
