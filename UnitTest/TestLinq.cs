using System;
using System.Collections.Generic;
using System.Data;
using Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.Convert;
using Tools.Linq;

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
			List<Book> books = TestData.ListData();
			BookRoot bookRoot = new BookRoot { Books = books };
			LinqTools.XmlQuery(bookRoot);
		}
	}
}
