using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tools.Component
{
	public class PublicComponent
	{
		public Dictionary<string,object> Invoke(string classname,string methodName,object[] objs)
		{
			Dictionary<string, object> dict = new Dictionary<string, object>();
			dict.Add("Status", null);
			dict.Add("Data", null);
			dict.Add("ErrorMsg", null);
			Type t = Type.GetType(classname);
			Activator.CreateInstance(t);
			Type[] types = new Type[objs.Length];
			MethodInfo info = t.GetMethod(methodName, types);
			info.Invoke();
			return dict;
		}
	}
}
