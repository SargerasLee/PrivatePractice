using System;
using Tools.Attributes;

namespace Tools.Component
{
	[CustomComponent]
	[RouteMapping(Value ="/robxdj")]
	public class RobxdjExtend
	{
		[RouteMapping(Value ="/checkbefore")]	
		public string CheckBeforeSave(string json)
		{
			Console.WriteLine(json);
			return "我尼玛,GG";
		}
		[RouteMapping(Value ="/write/{json}")]
		public object WriteBack([RouteVariable]string json, [UrlParam]string hh)
		{
			return "s";
		}
	}
}
