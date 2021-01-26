using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Tools.Log
{
	/// <summary>
	/// 日志类
	/// </summary>
	public abstract class GeneralLogger
	{
		public string DatePattern { get; set; }
		public string TimePattern { get; set; }
		public string FullFilePath { set; get; }
		public LogLevel Level { set; get; }

		protected static readonly Dictionary<LogLevel, string> levelDict = new Dictionary<LogLevel, string>
		{
			{LogLevel.OFF,"OFF" },
			{LogLevel.FATAL,"FATAL" },
			{LogLevel.ERROR,"ERROR" },
			{LogLevel.WARN,"WARN" },
			{LogLevel.INFO,"INFO" },
			{LogLevel.DEBUG,"DEBUG" },
			{LogLevel.TRACE,"TRACE" },
			{LogLevel.ALL,"ALL" }
		};

		public abstract void Log(params string[] text);
		public abstract void Log(string desc, JArray array);
		public abstract void Log(string desc, JObject obj);
		public abstract void LogXml(string desc, string xmlStr);
		public abstract void LogJson(string desc, string jsonStr, bool isArray);
		public abstract void LogObject(object obj);
		public abstract void Debug(params string[] text);
		public abstract void Info(params string[] text);
		public abstract void Error(string text, Exception ex);
		public abstract void Warn(params string[] text);
	}
}
