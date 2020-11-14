using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class IEnumerateTest
	{
		[TestMethod]
		public void testEnumable()
		{
			MyIEnumable m = new MyIEnumable();
			foreach (string i in m)
			{
				Console.WriteLine(i);
			}
		}
	}
	class MyIEnumable
	{
		private string[] vs={"aa","ss","bb"};

		public IEnumerator<string> GetEnumerator()
		{
			for(int i=0;i<vs.Length;i++)
			{
				yield return vs[i];
			}
		}
	}
}
