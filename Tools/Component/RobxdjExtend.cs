using System;
using Tools.Attributes;
using System.Collections.Generic;
using Tools.Core;
using System.Web.ModelBinding;

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
		public object WriteBack([RouteParam("0")]string json, [RouteParam("1")]string hx, [UrlParam]string hh)
		{
			return "s";
		}

		[RouteMapping("/write/hello/ppp")]
		[Json]
		public Dictionary<string,string> Test2(RouteContext context)
		{
			string s = context.UrlParams["s"];
			return null;
		}
	}
}
