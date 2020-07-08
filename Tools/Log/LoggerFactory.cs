using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tools.Log
{
	public class LoggerFactory
	{
		private static readonly XmlDocument document = new XmlDocument();
		private static string code;
		private static string path;
		private static string date;
		private static string className;
		private static string assembly;
		private static bool flag;
		private static XmlNode target;

		public static GeneralLogger Bulid(string code)
		{
			LoadConfig();
			GetProperties(code);
			CreateFileIfNotExists();
			GeneralLogger logger = GetInstance();
			SetProperties(ref logger);
			return logger;
		}
		private static void LoadConfig()
		{
			document.Load("./GeneralLogConfig.xml");
		}
		private static void GetProperties(string code)
		{
			target = document.SelectSingleNode("//Log[@Code='" + code + "']");
			path = target.Attributes["FullPath"].ToString();
			className = target.Attributes["Class"].ToString();
			assembly = target.Attributes["Assembly"].ToString();
			flag = System.Convert.ToBoolean(target.Attributes["Print"]);
		}
		private static void CreateFileIfNotExists()
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}
		private static GeneralLogger GetInstance()
		{
			Type t = Assembly.Load(assembly).GetType(className);
			GeneralLogger logger = Activator.CreateInstance(t) as GeneralLogger;
			return logger;
		}

		private static void SetProperties(ref GeneralLogger logger)
		{
			logger.FullFilePath = path + "Log" + logger.Date + ".txt";
		}
	}
}
