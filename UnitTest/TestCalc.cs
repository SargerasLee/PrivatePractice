using System;
using System.Collections.Generic;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTest
{
	[TestClass]
	public class TestCalc
	{
		[TestMethod]
		public void TestInfixExpression()
		{
			string s = "4+2*3/(3+4.4)";
			List<string> list = ExpressionUtil.ToInfixExpression(s);
			foreach (var s1 in list)
			{
				Console.WriteLine(s1);
			}
		}

		[TestMethod]
		public void TestSuffixExpression()
		{
			string s = "4+2*3/(3+4.4)";
			List<string> suffixExpression = ExpressionUtil.ToSuffixExpression(ExpressionUtil.ToInfixExpression(s));
			foreach (var str in suffixExpression)
			{
				Console.WriteLine(str);
			}
		}

		[TestMethod]
		public void TestCalcu()
		{
			string s = "4+2*3/(3+4.4)";
			CalculateImpl calculate=new CalculateImpl();
			string calc = calculate.Calc(s);
			Console.WriteLine(calc);
		}
	}
}
