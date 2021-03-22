﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Tools.Exceptions;
using Tools.Log;

namespace Tools.GlobalConfig
{
	public delegate void ConfigFileChangedEventHandler(object sender, FileChangedEvent changedEvent);
	public static class ProjectConfigContainer
	{
		public static event ConfigFileChangedEventHandler ConfigFileChanged;
		private static readonly object obj = new object();
		private const string PATH = "GlobalConfig/ProjectGlobalConfig.xml";
		private static bool flag = false;
		private static DateTime lastModifyTime = File.GetLastWriteTime(PATH).AddDays(-1.0);

		private static readonly Dictionary<string, string> Properties = new Dictionary<string, string>();
		private static readonly List<string> Assemblies = new List<string>();
		private static readonly Dictionary<string, LogConfig> LoggerConfig = new Dictionary<string, LogConfig>();

		//static ProjectConfigContainer()
		//{
		//	lock (obj)
		//	{
		//		Load(); 
		//	}
		//}

		private static void Load()
		{
			if (!flag)
			{
				XmlDocument xd = new XmlDocument();
				lastModifyTime = File.GetLastWriteTime(PATH);//GSPContext.Current.ServerInstallPath
				xd.Load(PATH);
				PutProperties(xd);
				PutAssemblies(xd);
				PutLogConfig(xd);
				//List <Task> tasks = new List<Task>
				//{
				//	Task.Run(() => PutProperties(xd)),
				//	Task.Run(() => PutAssemblies(xd)),
				//	Task.Run(() => PutLogConfig(xd))
				//};
				//Task.WaitAll(tasks.ToArray());
				flag = true;
			}
		}

		private static void PutLogConfig(XmlDocument xd)
		{
			XmlNodeList list = xd.SelectNodes("/Configuration/Modules/Log/Type");
			LoggerConfig.Clear();
			foreach (XmlNode item in list)
			{
				LoggerConfig.Add(item.Attributes["Code"].Value, new LogConfig 
				{ 
					Code= item.Attributes["Code"].Value,
					Assembly= item.Attributes["Assembly"].Value,
					Class= item.Attributes["Class"].Value,
					FullPath= item.Attributes["FullPath"].Value,
					Level= item.Attributes["Level"].Value,
					Name= item.Attributes["Name"].Value
				}) ;
			}
		}

		private static void PutAssemblies(XmlDocument xd)
		{
			XmlNodeList assembliesList = xd.SelectNodes("/Configuration/Modules/Component-Scan/Assembly");
			Assemblies.Clear();
			foreach (XmlNode item in assembliesList)
			{
				Assemblies.Add(item.Attributes["Name"].Value);
			}
		}

		private static void PutProperties(XmlDocument xd)
		{
			XmlNodeList list = xd.SelectNodes("/Configuration/Modules/Properties/Property");
			Properties.Clear();
			foreach (XmlNode item in list)
			{
				Properties.Add(item.Attributes["Code"].Value, item.Attributes["Value"].Value);
			}
		}

		public static string GetProperty(string code)
		{
			ReloadIfFileChanged();
			if (Properties.ContainsKey(code))
				return Properties[code];
			else
				throw new ConfigNotFoundException("未找到相应的属性配置");
		}

		public static List<string> GetAutoScanAssemblies()
		{
			ReloadIfFileChanged();
			return Assemblies;
		}

		public static LogConfig GetLogConfig(string code)
		{
			ReloadIfFileChanged();
			if (LoggerConfig.ContainsKey(code))
				return LoggerConfig[code];
			else
				throw new ConfigNotFoundException("未找到相应的日志配置");
		}

		private static void ReloadIfFileChanged()
		{
			if (File.GetLastWriteTime(PATH) > lastModifyTime)
			{
				lock (obj)
				{
					if (File.GetLastWriteTime(PATH) > lastModifyTime)
					{
						DateTime before = lastModifyTime;
						flag = false;
						Load();
						ConfigFileChanged?.Invoke(typeof(ProjectConfigContainer), new FileChangedEvent
						{
							ConfigFileName = "ProjectGlobalConfig.xml",
							ConfigFilePath = PATH,
							BeforeTime = before,
							UpdateTime = lastModifyTime
						});
					}	
				}	
			}
		}
	}

	public class FileChangedEvent : EventArgs
	{
		public string ConfigFilePath { get; set; }
		public string ConfigFileName{ get; set; }
		public DateTime BeforeTime { get; set; }
		public DateTime UpdateTime{ get; set; }
	}
}
