using System.Collections.Generic;
using Tools.Core;

namespace Tools.Component
{
	public class PublicComponent
	{
		public Dictionary<string,object> MethodMapping(string route, string json)
		{
			object o = null;
			Dictionary<string, CustomComponentInfo> dict = CustomComponentContainer.GetContainer().ClassMapping;
			foreach (string key in dict.Keys)
			{
				if (route.StartsWith(key))
				{
					o = dict[key].Invoke(route.Substring(key.Length), json);
				}
			}
			return new Dictionary<string, object> { { "1", o } };
		}
	}
}
