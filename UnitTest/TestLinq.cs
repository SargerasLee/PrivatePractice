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
			//List<Book> books = TestData.ListData();
			//BookRoot bookRoot = new BookRoot { Books = books };
			//LinqTools.XmlQuery(bookRoot);
			string xx=@"{""name"":""zhangsan"",""hobby"":{""lala"":""哈哈哈""}}";
			JObject obj = JObject.Parse(xx);
			Console.WriteLine(obj["hobby"]);
			//string str = "";
			//JObject all = JObject.Parse(str);
			//JObject tables = JObject.Parse(all["Tables"].ToString());
			//JArray header = JArray.Parse(tables["Header"].ToString());
			//JArray rows = JArray.Parse(header[0]["Rows"].ToString());

		}
	}
}
