using System;
using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class DymaticTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			var staticPerson = new Person();
			dynamic dynamicPerson = new Person();
			//dynamicPerson.GetFullName();
			//dynamic eo = new ExpandoObject();
			//eo.R = "s";
		}
	}

	public class Person
	{
		public string Name{ get; set; }
		public string Sex { get; set; }
		public int Age { get; set; }

		public Person()
		{
			
		}

		public string GetFullName()
		{
			return this.Name;
		}
	}
}
