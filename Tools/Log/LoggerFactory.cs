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
		private static readonly object o=new object();
		private static string path;
		private static string className;
		private static string assembly;
		private static bool flag;
		private static XmlNode target;
		private static Dictionary<string, GeneralLogger> logDict = new Dictionary<string, GeneralLogger>();

		/// <summary>
		///  建造日志类
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		private static GeneralLogger Bulid(string code)
		{
			LoadConfig();
			GetProperties(code);
			CreateFileIfNotExists();
			GeneralLogger logger = GetLogger();
			SetProperties(ref logger);
			SetDict(code,ref logger);
			return logDict[code];
		}

		/// <summary>
		///  获取日志实例，先查找字典，有直接返回，否则建造
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public static GeneralLogger GetInstance(string code)
		{
			if(logDict.ContainsKey(code))
			{
				return logDict[code];
			}else
			{
				return Bulid(code);
			}
		}
		private static void LoadConfig()
		{
			document.Load("./Log/GeneralLogConfig.xml");
		}
		private static void GetProperties(string code)
		{
			target = document.SelectSingleNode("//Log[@Code='" + code + "']");
			path = target.Attributes["FullPath"].Value;
			className = target.Attributes["Class"].Value;
			assembly = target.Attributes["Assembly"].Value;
			flag = System.Convert.ToBoolean(target.Attributes["Print"].Value);
		}
		private static void CreateFileIfNotExists()
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}
		private static GeneralLogger GetLogger()
		{
			Type t = Assembly.Load(assembly).GetType(className);
			GeneralLogger logger = Activator.CreateInstance(t) as GeneralLogger;
			return logger;
		}

		private static void SetProperties(ref GeneralLogger logger)
		{
			logger.FullFilePath = path + "Log" + logger.Date + ".txt";
			logger.Open = flag;
		}
		private static void SetDict(string code,ref GeneralLogger logger)
		{
			if (!logDict.ContainsKey(code))
			{
				lock (o)
				{
					if (!logDict.ContainsKey(code))
					{
						logDict.Add(code, logger); 
					}
					else
					{
						int g = GC.GetGeneration(logger);
						GC.Collect(g);
					}
				} 
			}
		}
	}
}
