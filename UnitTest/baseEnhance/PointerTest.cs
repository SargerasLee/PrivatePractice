using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class PointerTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			//DoSome();
			string xx = "adad";
			Console.WriteLine(xx.Split('?').Length);
			bool ss = Regex.IsMatch("/userid/{0", "{[a-zA-Z0-9]+}");
			Console.WriteLine(ss);
		}
		private unsafe void DoSome()
		{
			int a = 10;
			int* pWidth = &a;
			Console.WriteLine("{0:X}", (ulong)pWidth);
			int* bb = stackalloc int[10];
		}
	}
}
