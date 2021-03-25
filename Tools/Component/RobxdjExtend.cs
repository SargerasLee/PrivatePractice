using System;
using Tools.Attributes;
using System.Collections.Generic;
using Tools.Core;

namespace Tools.Component
{
	[CustomComponent]
	[RouteMapping("/robxdj")]
	public class RobxdjExtend
	{
		[RouteMapping("/checkbefore")]	
		public string CheckBeforeSave(string json)
		{
			Console.WriteLine(json);
			return "我尼玛,GG";
		}
		[RouteMapping("/write/{0}/{1}")]
		public object WriteBack([RouteVariable("0")]string json, [RouteVariable("1")]string hx, [UrlParam]string hh)
		{
			return "s";
		}

		[RouteMapping("/test2")]
		public object Test2(RouteContext context)
		{
			string s = context.UrlParams["s"];
			return null;
		}
	}
}
