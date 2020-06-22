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
			List<Friend> list1 = new List<Friend>();
			list1.Add(new Friend(){Name = "张三",IsSaleMan = false,Friends = null});
			list1.Add(new Friend(){Name = "李四",IsSaleMan = false,Friends = null});
			list1.Add(new Friend(){Name = "王五",IsSaleMan = false,Friends = null});

			List<Friend> list2 = new List<Friend>();
			list2.Add(new Friend() { Name = "lucy", IsSaleMan = false, Friends = null });
			list2.Add(new Friend() { Name = "tom", IsSaleMan = true, Friends = null });
			list2.Add(new Friend() { Name = "green", IsSaleMan = false, Friends = null });

			List<Friend> list3 = new List<Friend>();
			list3.Add(new Friend() { Name = "ming", IsSaleMan = false, Friends = list1 });
			list3.Add(new Friend() { Name = "ling", IsSaleMan = false, Friends = list2 });

			Friend friend = new Friend() {Name = "me", IsSaleMan = false, Friends = list3};
			string name = MapUtil.BreadthFirstSearch(friend);
			Console.WriteLine(name);
		}
	}
}
