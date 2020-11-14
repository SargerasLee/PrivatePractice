using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class TurpleTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			int a = 7;
			string b = "3";
			Tuple<int, string> t = Tuple.Create(a, b);

		}

		[TestMethod]
		public void TestChecked()
		{
			byte b = 255;
			checked
			{
				b--;
			}
			Console.WriteLine(b.ToString());
		}
	}
}
