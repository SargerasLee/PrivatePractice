using System;
using System.Collections.Generic;
using System.Data;
using Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.Convert;

namespace UnitTest
{
	[TestClass]
	public class TestConvertTool
	{
		[TestMethod]
		public void TestXmlToEntityToXml()
		{
			List<Book> list = TestData.ListData();
			BookRoot books = new BookRoot { Books = list };
			string xml = ConvertTools.EntityToXmlStr(books, string.Empty, string.Empty, null);
			Console.WriteLine(xml);
			BookRoot l = ConvertTools.XmlStrToEntity<BookRoot>(xml);
			Console.WriteLine(l.Books[0].ToString());
		}

		[TestMethod]
		public void TestListToDataTable()
		{
			List<Book> books = TestData.ListData();
			DataTable data = ConvertTools.ListToDataTable(books);
			foreach (DataRow row in data.Rows)
			{
				foreach (var obj in row.ItemArray)
				{
					Console.WriteLine(obj + "	");
				}
				Console.WriteLine();
			}
		}
	}
}
