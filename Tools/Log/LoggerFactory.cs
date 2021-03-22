using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Tools.Log
{
	public class LoggerFactory
	{
		private readonly XmlDocument document = new XmlDocument();
		private static readonly object o=new object();//公共对象锁
		private readonly string configPath = string.Empty;//配置文件；路径

		private static readonly Dictionary<string, GeneralLogger> logDict = new Dictionary<string, GeneralLogger>();
		private static readonly Dictionary<string, LogLevel> levelDict = new Dictionary<string, LogLevel>
		{
			{"OFF",LogLevel.OFF },
			{"FATAL",LogLevel.FATAL },
			{"ERROR",LogLevel.ERROR },
			{"WARN",LogLevel.WARN },
			{"INFO",LogLevel.INFO },
			{"DEBUG",LogLevel.DEBUG },
			{"TRACE",LogLevel.TRACE },
			{"ALL",LogLevel.ALL }
		};
		private static readonly string DefaultClass = typeof(CommonLogger).FullName;
		private static readonly string DefaultAssembly = typeof(CommonLogger).GetType().Assembly.FullName;

		private static LoggerFactory factory = null;

		private LoggerFactory()
		{
			configPath = "Log/GeneralLogConfig.xml";
			LoadConfig();
		}

		public static LoggerFactory SingleInstance()
		{
			if (factory == null)
			{
				lock (o)
				{
					if (factory == null)
					{
						factory = new LoggerFactory();
					}					
				}
			}
			return factory;
		}
		/// <summary>
		///  建造日志类
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		private GeneralLogger Bulid(string code, LogLevel level)
		{
			Dictionary<string, object> module = GetModuleConfig(code, level);
			GeneralLogger logger = CreateLogger(module);
			PutCache(code, logger);
			return logDict[code];
		}

		public GeneralLogger GetLogger(string moduleCode)
		{
			return GetLogger(moduleCode, LogLevel.ALL);
		}

		public GeneralLogger GetLogger(string moduleCode, LogLevel level)
		{
			if (logDict.ContainsKey(moduleCode))
			{
				return logDict[moduleCode];
			}
			else
			{
				return Bulid(moduleCode, level);
			}
		}
		private void LoadConfig()
		{
			if(File.Exists(configPath))
			{
				document.Load(configPath);
			}
		}
		private Dictionary<string, object> GetModuleConfig(string code, LogLevel level)
		{
			Dictionary<string, object> module = new Dictionary<string, object>(5);
			if (File.Exists(configPath))
			{
				XmlNode target = document.SelectSingleNode("//Log[@Code='" + code + "']");
				module["path"] = target.Attributes["FullPath"].Value;
				module["className"] = string.IsNullOrWhiteSpace(target.Attributes["Class"].Value) ? DefaultClass : target.Attributes["Class"].Value;
				module["assembly"] = string.IsNullOrWhiteSpace(target.Attributes["Assembly"].Value) ? DefaultAssembly : target.Attributes["Assembly"].Value;
				module["logLevel"] = levelDict[target.Attributes["Level"].Value];
			}
			else
			{
				module["path"] = "d:\\Log\\"+code+"\\";
				module["className"] = DefaultClass;
				module["assembly"] = DefaultAssembly;
				module["logLevel"] = level;
			}
			return module;
		}
		
		private GeneralLogger CreateLogger(Dictionary<string, object> module)
		{
			Type t = Assembly.Load(module["assembly"].ToString()).GetType(module["className"].ToString());
			//GeneralLogger logger = Assembly.Load(assembly).CreateInstance(className) as GeneralLogger;
			GeneralLogger logger = Activator.CreateInstance(t) as GeneralLogger;
			logger.Directory = module["path"].ToString();
			logger.Level = (LogLevel)module["logLevel"];
			return logger;
		}


		private void PutCache(string code,GeneralLogger logger)
		{
			if (!logDict.ContainsKey(code))
			{
				lock (o)
				{
					if (!logDict.ContainsKey(code))
					{
						logDict.Add(code, logger);
					}
				} 
			}
		}
	}
}
