using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
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
			GeneralLogger logger1 = factory.GetInstance("ZJ",LogLevel.ALL);
			GeneralLogger logger2 = factory.GetInstance("YS",LogLevel.DEBUG);
			GeneralLogger logger3 = factory.GetInstance("FY",LogLevel.INFO);
			logger1.Log("json", jObject);
			logger1.Error("我是error1", new Exception("自定义error"));
			logger1.Info("我是info1");
			logger1.Debug("我是debug1");
			logger2.Debug("我是debug2");
			logger2.Info("我是info2");
			logger3.Info("我是info3");
			logger3.Debug("我是debug3");
		}

		[TestMethod]
		public void TestConvert()
		{
			Console.WriteLine(Convert.ToString(null));
		}
	}
}
