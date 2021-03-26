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
			if (json == null)
				throw new Exception("GG了");
			Console.WriteLine(json);
			return "我尼玛,GG";
		}
		[RouteMapping("/write/{h1}/{hh1}")]
		public object WriteBack([RouteParam("h1")]string json, [RouteParam("hh1")]string hx, [UrlParam]int hh)
		{
			Console.WriteLine(json);
			Console.WriteLine(hx);
			Console.WriteLine(hh);
			return "s";
		}

		[RouteMapping("/write/hello/app")]
		[Json]
		public Dictionary<string,string> Test2(RouteContext context)
		{
			return context.UrlParams;
		}

		[RouteMapping("/write/bbq")]
		public void Test3()
		{
			Console.WriteLine("没参数，不返回");
		}
	}
}
