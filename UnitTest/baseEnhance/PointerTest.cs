using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class PointerTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			DoSome();
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
