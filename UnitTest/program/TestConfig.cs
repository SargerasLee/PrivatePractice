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
			string zj = GlobalConfigMgr.GetProperty("zj");
			string fy = GlobalConfigMgr.GetProperty("fy");
			Console.WriteLine(zj);
			Console.WriteLine(fy);
		}
	}
}
