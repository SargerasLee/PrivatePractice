using System;
using System.Collections.Generic;
using System.Data;
using Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.Linq;

namespace UnitTest
{
	[TestClass]
	public class TestLinq
	{
		[TestMethod]
		public void TestXmlQuery()
		{
			List<Book> books = ReturnTestData();
			BookRoot bookRoot = new BookRoot { Books = books };
			LinqTools.XmlQuery(bookRoot);
		}

		[TestMethod]
		public void TestDataTableQuery()
		{
			List<Book> books = ReturnTestData();
			DataTable data = LinqTools.ListToDataTable(books);
			foreach(DataRow row in data.Rows)
			{
				foreach(var obj in row.ItemArray)
				{
					Console.WriteLine(obj+"	");
				}
				Console.WriteLine();
			}
		}







		private List<Book> ReturnTestData()
		{
			List<Book> books = new List<Book>{
				new Book{BookCode=1,Author="zhangsan",BookName="Oracle Programing",IsSuit=false,Price=19.9m,PublishDate=DateTime.Now.AddDays(-12) },
				new Book{BookCode=2,Author="lisi",BookName="Thinking in Java",IsSuit=false,Price=79.9m,PublishDate=DateTime.Now.AddDays(-1) },
				new Book{BookCode=3,Author="wangwu",BookName="Thinking in C#",IsSuit=true,Price=55.2m,PublishDate=DateTime.Now.AddDays(-20) },
				new Book{BookCode=4,Author="maliu",BookName="Javascript Programing",IsSuit=false,Price=39.9m,PublishDate=DateTime.Now.AddDays(-100) },
			};
			return books;
		}
	}
}
