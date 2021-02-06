using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.GlobalConfig;
namespace UnitTest.program
{
	[TestClass]
	public class TestConfig
	{
		[TestMethod]
		public void TestMethod1()
		{
			string zj = ProjectConfigContainer.GetProperty("zj");
			string fy = ProjectConfigContainer.GetProperty("fy");
			Console.WriteLine(zj);
			Console.WriteLine(fy);
		}
	}
}
