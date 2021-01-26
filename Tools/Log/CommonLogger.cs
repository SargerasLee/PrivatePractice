using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace Tools.Log
{
	public class CommonLogger : GeneralLogger
	{
		private readonly object lockObj = new object();
		public CommonLogger()
		{
			DatePattern = "yyyy-MM-dd";
			TimePattern = "HH : mm : ss : fff";
		}

		public override void Debug(params string[] text)
		{
			if (Level > LogLevel.DEBUG) return;
			Log(LogLevel.DEBUG, text);
		}

		public override void Error(string text, Exception ex = null)
		{
			if (Level > LogLevel.ERROR) return;
			if (ex!=null)
				Log(LogLevel.ERROR, text, ex.Message, ex.StackTrace);
			else
				Log(LogLevel.ERROR, text);
		}

		public override void Info(params string[] text)
		{
			if (Level > LogLevel.INFO) return;
			Log(LogLevel.INFO, text);
		}

		public override void Warn(params string[] text)
		{
			if (Level > LogLevel.WARN) return;
			Log(LogLevel.WARN, text);
		}


		private void Log(LogLevel level, params string[] text)
		{
			if (Level == LogLevel.OFF) return;
			StringBuilder sb = new StringBuilder(200);
			foreach (var s in text)
			{
				sb.Append(s);
				sb.Append("\r\n");
			}
			StreamWriter writer;
			try
			{
				CreateFileIfNotExists();
				bool token = false;
				Monitor.TryEnter(lockObj, 100, ref token);
				if (token)
				{
					string date = DateTime.Now.ToString(DatePattern);
					string p = FullFilePath + "Log" + date + ".txt";
					using (writer = new StreamWriter(p, true, Encoding.Default))
					{
						string time = DateTime.Now.ToString(TimePattern);
						writer.WriteLine(time + $":【{ levelDict[level]}】");
						writer.WriteLine("【进程】：" + Process.GetCurrentProcess().ProcessName);
						writer.WriteLine("【线程ID】：" + Thread.CurrentThread.ManagedThreadId);
						writer.WriteLine("【信息】：" + sb);
						writer.Flush();
					}
					Monitor.Exit(lockObj);
				}
				else
				{
					throw new TimeoutException("获得锁超时");
				}

			}
			catch (Exception e)
			{
				string log = "日志方法异常" + e.Message;
			}
		}

		public override void Log(params string[] text)
		{
			Log(LogLevel.ALL, text);
		}

		/// <summary>
		///  json数组打印
		/// </summary>
		/// <param name="desc"></param>
		/// <param name="array"></param>
		public override void Log(string desc, JArray array)
		{
			Log(LogLevel.ALL, desc, JsonConvert.SerializeObject(array, Formatting.Indented));
		}

		/// <summary>
		///  json打印
		/// </summary>
		/// <param name="desc"></param>
		/// <param name="obj"></param>
		public override void Log(string desc, JObject obj)
		{
			Log(LogLevel.ALL, desc, JsonConvert.SerializeObject(obj, Formatting.Indented));
		}

		public override void LogJson(string desc, string jsonStr, bool isArray)
		{
			if (isArray)
				Log(desc, JsonConvert.DeserializeObject(jsonStr) as JArray);
			else
				Log(desc, JsonConvert.DeserializeObject(jsonStr) as JObject);
		}

		public override void LogObject(object obj)
		{
			Log(LogLevel.ALL, JsonConvert.SerializeObject(obj));
		}

		/// <summary>
		///  xml打印
		/// </summary>
		/// <param name="desc"></param>
		/// <param name="doc"></param>
		public override void LogXml(string desc, string xmlStr)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xmlStr);
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);
			using (XmlTextWriter writer = new XmlTextWriter(sw))
			{
				writer.Indentation = 4;  // 缩进个数
				writer.IndentChar = ' ';  // 缩进字符
				writer.Formatting = System.Xml.Formatting.Indented;
				doc.WriteTo(writer);
			}
			Log(LogLevel.ALL, desc, sb.ToString());
		}

		private void CreateFileIfNotExists()
		{
			if (!Directory.Exists(FullFilePath))
			{
				Directory.CreateDirectory(FullFilePath);
			}
		}
	}
}
