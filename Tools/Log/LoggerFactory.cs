using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using Tools.GlobalConfig;

namespace Tools.Log
{
	public class LoggerFactory
	{
		private static readonly object o=new object();//公共对象锁
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
		private static readonly string DefaultAssembly = typeof(CommonLogger).Assembly.FullName;

		private static readonly LoggerFactory factory = new LoggerFactory();

		private LoggerFactory(){ }

		public static LoggerFactory SingleInstance()
		{
			return factory;
		}
		/// <summary>
		///  建造日志类
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		private GeneralLogger Bulid(string code)
		{
			Dictionary<string, object> module = GetModuleConfig(code);
			GeneralLogger logger = CreateLogger(module);
			PutCache(code, logger);
			return logDict[code];
		}

		public GeneralLogger GetLogger(string moduleCode)
		{
			if (logDict.ContainsKey(moduleCode))
			{
				return logDict[moduleCode];
			}
			else
			{
				return Bulid(moduleCode);
			}
		}
		private Dictionary<string, object> GetModuleConfig(string code)
		{
			Dictionary<string, object> module = new Dictionary<string, object>(4);
			LogConfig config = ProjectConfigContainer.GetLogConfig(code);
			module["path"] = config.FullPath;
			module["className"] = string.IsNullOrWhiteSpace(config.Class) ? DefaultClass : config.Class;
			module["assembly"] = string.IsNullOrWhiteSpace(config.Assembly) ? DefaultAssembly : config.Assembly;
			module["logLevel"] = levelDict[config.Level];
			
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
