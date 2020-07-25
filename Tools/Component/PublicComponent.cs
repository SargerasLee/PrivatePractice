using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tools.Component
{
	public class PublicComponent
	{
		public Dictionary<string,object> Invoke(string classname,string methodName,List<object> objs)
		{
			Dictionary<string, object> dict = new Dictionary<string, object>
			{
				{ "Status", null },
				{ "Data", null },
				{ "ErrorMsg", null }
			};
			Type t = Type.GetType(classname);
			object proxy = Activator.CreateInstance(t);
			Type[] types = new Type[objs.Count];

			for (int i = 0; i < objs.Count; i++)
			{
				types[i] = objs[i].GetType();
			}
			MethodInfo info = t.GetMethod(methodName, types);
			ParameterInfo returnVal = info.ReturnParameter;
			object[] param = objs.ToArray();
			object resObj = info.Invoke(proxy, param);
			string s = JsonConvert.SerializeObject(resObj);
			dict["Status"] = "S";
			dict["Data"] = s;
			return dict;
		}
	}
}
