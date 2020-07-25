using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tools.Log;

namespace UnitTest
{
	[TestClass]
	public class TestLog
	{
		[TestMethod]
		public void TestLogWithConfig()
		{
			XmlDocument document=new XmlDocument();
			XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "UTF-8", string.Empty);
			document.AppendChild(declaration);
			XmlElement root = document.CreateElement("Bill");
			XmlElement name = document.CreateElement("name");
			name.InnerText = "zhangsan";
			XmlElement age = document.CreateElement("age");
			age.InnerText = "18";
			root.AppendChild(name);
			root.AppendChild(age);
			document.AppendChild(root);
			LoggerFactory factory = new LoggerFactory();
			GeneralLogger logger1 = factory.GetInstance("ZJ", "./Log/GeneralLogConfig.xml");
			GeneralLogger logger4 = factory.GetInstance("ZJ", "./Log/GeneralLogConfig.xml");
			Console.WriteLine(ReferenceEquals(logger1, logger4));
			GeneralLogger logger2 = factory.GetInstance("YS", "./Log/GeneralLogConfig.xml");
			GeneralLogger logger3 = factory.GetInstance("FY", "./Log/GeneralLogConfig.xml");
			//logger1.LogXml("xml", document);
			//logger2.LogXml("xml", document);
			//logger3.LogXml("xml", document);
		}

		[TestMethod]
		public void TestJson()
		{
			string json = TestData.JsonArrayData();
			JArray jObject = JsonConvert.DeserializeObject(json) as JArray;
			LoggerFactory factory = new LoggerFactory();
			GeneralLogger logger1 = factory.GetInstance("ZJ");
			GeneralLogger logger2 = factory.GetInstance("YS");
			GeneralLogger logger3 = factory.GetInstance("FY");
			logger1.Log("json", jObject);
			logger2.Log("json", jObject);
			logger3.Log("json", jObject);
		}

		[TestMethod]
		public void TestConvert()
		{
			Console.WriteLine(Convert.ToString(null));
		}
	}
}
