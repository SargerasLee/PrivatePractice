using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class FormatTest
	{
		[TestMethod]
		public void TestMethod1()
		{
		}
	}


	struct Vector : IFormattable
	{
		private double x,y,z;

		public Vector(double x,double y,double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (format == null) return this.ToString();
			string f = format.ToUpper();
			switch(f)
			{
				case "A":return "";
				default:return this.ToString();
			}
		}
	}
}
