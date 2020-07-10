using System;
using System.Collections.Generic;
using Entity;
using Logic.Map;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTest
{
	[TestClass]
	public class TestMapSearch
	{
		[TestMethod]
		public void TestMethod1()
		{
			Friend friend = TestData.MapData();
			string name = MapUtil.BreadthFirstSearch(friend);
			Console.WriteLine(name);
		}
	}
}
