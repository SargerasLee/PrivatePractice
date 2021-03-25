using System.Collections.Generic;
using System.Linq;
using Tools.Core;
using Tools.Exceptions;

namespace Tools.Component
{
	public class ComponentDispatcher
	{
		private readonly CustomComponentContainer container = CustomComponentContainer.GetContainer();
		private static readonly ComponentDispatcher dispatcher = new ComponentDispatcher();
		public object Dispatch(string route,params object[] objs)
		{
			route = route.Trim();
			Dictionary<string, CustomComponentInfo> dict = container.ClassMapping;
			string targetKey = dict.Keys.Where(key => route.StartsWith(key)).FirstOrDefault();
			if (string.IsNullOrWhiteSpace(targetKey))
				throw new RouteNotMatchException("未匹配对应的类");
			object obj = dict[targetKey].Invoke(route.Substring(targetKey.Length), objs);
			return obj;
		}

		public static ComponentDispatcher GetInstance()
		{
			return dispatcher;
		}
	}
}
