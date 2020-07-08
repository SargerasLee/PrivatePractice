﻿using System;
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
		public void TestMethod1()
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
			GeneralLogger logger = LoggerFactory.GetInstance("ZJ");
			logger.Log("xml", document);
		}

		[TestMethod]
		public void TestJson()
		{
			string json = "[{name:\"lisi\",age:\"30\"},{name:\"xxx\",age:\"66\"}";
			JArray jObject = JsonConvert.DeserializeObject(json) as JArray;
			GeneralLogger logger1 = LoggerFactory.GetInstance("ZJ");
			GeneralLogger logger2 = LoggerFactory.GetInstance("YS");
			GeneralLogger logger3 = LoggerFactory.GetInstance("FY");
			logger1.Log("json", jObject);
			logger2.Log("json", jObject);
			logger3.Log("json", jObject);
		}
	}
}
