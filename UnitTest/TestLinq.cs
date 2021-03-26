using System;
using System.Collections.Generic;
using System.Data;
using Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.Convert;
using Tools.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UnitTest
{
	[TestClass]
	public class TestLinq
	{
		[TestMethod]
		public void TestSimpleQuery()
		{
			LinqTools.SimpleQuery();
		}

		[TestMethod]
		public void TestXmlQuery()
		{
			JObject obj = new JObject();
			obj.Add(new JProperty("hhh", new List<int> { 0, 1, 2 }));
			Console.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));
		}
	}
}
