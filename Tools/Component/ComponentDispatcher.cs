using System.Collections.Generic;
using Tools.Core;

namespace Tools.Component
{
	public class ComponentDispatcher
	{
		private readonly CustomComponentContainer container = CustomComponentContainer.GetContainer();
		private static readonly ComponentDispatcher dispatcher = new ComponentDispatcher();
		public object Dispatch(string route,params object[] objs)
		{
			object obj = null;
			Dictionary<string, CustomComponentInfo> dict = container.ClassMapping;
			foreach (string key in dict.Keys)
			{
				if (route.StartsWith(key))
				{
					obj = dict[key].Invoke(route.Substring(key.Length), objs);
				}
			}
			return obj;
		}

		public static ComponentDispatcher GetInstance()
		{
			return dispatcher;
		}
	}
}
