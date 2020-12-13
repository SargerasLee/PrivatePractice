using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
namespace UnitTest.baseEnhance
{
	[TestClass]
	public class IOTest
	{
		[TestMethod]
		public void TestRegedit()
		{
			RegistryKey rk = Registry.LocalMachine;
			RegistryKey sub1 = rk.OpenSubKey("software");
			RegistryKey sub2 = sub1.OpenSubKey("microsoft");
		}
	}
}
