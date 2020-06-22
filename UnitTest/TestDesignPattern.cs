using System;
using DesignPattern.SimpleFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class TestDesignPattern
	{
		[TestMethod]
		public void TestSimpleFactory()
		{
			Operation op = OperationFactory.CreateInstance("+");
			op.NumA = 2.2;
			op.NumB = 3.3;
			double res = op.GetResult();
			Console.WriteLine(res);
		}
	}
}
