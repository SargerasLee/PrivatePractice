using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class DelegateTest
	{
		public delegate void Haha(int a, string b);
		[TestMethod]
		public void TestMethod1()
		{
			Haha ha = new Haha(Nima);
			ha.Invoke(3, "5");
		}

		private void Nima(int a,string b)
		{
			Console.WriteLine(a + b);
		}

		[TestMethod]
		public void Add()
		{
			M a = new M { Age = 1 };
			M b = new M { Age = 4 };
			M c = a + b;
			Console.WriteLine(c.Age);
		}

		[TestMethod]
		public void UnSafe()
		{
			var values = new List<int>() { 5, 6, 7 };
			var funcs = new List<Func<int>>();
			foreach(var i in values)
			{
				funcs.Add(() => i);
			}
			foreach(var n in funcs)
			{
				Console.WriteLine(n());
			}
		}
	}
	class M
	{
		public int Age{ get; set; }

		public static M operator +(M a,M b)
		{
			return new M { Age = a.Age + b.Age };
		}
	}

}
