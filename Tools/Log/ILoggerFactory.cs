using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tools.Log
{
	public class ILoggerFactory
	{
		private static readonly XmlDocument document = new XmlDocument();
		private static string path;
		private static string datePattern;
		private static string className;
		private static string assembly;
		private static bool flag;
		private static XmlNode target;

		public static ILogger Bulid(string code)
		{
			LoadConfig();
			SetProperties(code);
			return GetInstance();
		}
		private static void LoadConfig()
		{
			document.Load("./GeneralLogConfig.xml");
		}
		private static void SetProperties(string code)
		{
			target = document.SelectSingleNode("//Log[@Code='" + code + "']");
			path = target.Attributes["FullPath"].ToString();
			datePattern = target.Attributes["DatePattern"].ToString();
			className = target.Attributes["Class"].ToString();
			assembly = target.Attributes["Assembly"].ToString();
			flag = System.Convert.ToBoolean(target.Attributes["Print"]);
		}
		private static ILogger GetInstance()
		{
			Type t = Assembly.Load(assembly).GetType(className);
			GeneralLogger logger = Activator.CreateInstance(t) as GeneralLogger;
			return logger;
		}
	}
}
