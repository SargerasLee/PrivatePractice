using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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
		private string timePattern;
		public CommonLogger()
		{
			DatePattern = "yyyy-MM-dd";
			timePattern = "HH : mm : ss : fff";
		}
		public override void Log(params string[] text)
		{
			if (!Open) return;
			StringBuilder sb = new StringBuilder(200);
			foreach (var s in text)
			{
				sb.Append(s);
				sb.Append("\r\n");
			}
			StreamWriter writer = null;
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
						string time = DateTime.Now.ToString(timePattern);
						writer.WriteLine(time + ":    " + sb);
						writer.Flush();
					}
					Monitor.Exit(lockObj);
					//if (writers.ContainsKey(date))
					//{
					//	if (!File.Exists(p))
					//		File.Create(p);
					//	writer = writers[date];
					//}
					//else
					//{					
					//	writer = new StreamWriter(p, true, Encoding.Default);
					//	foreach(var i in writers.Keys)
					//	{
					//		writers[i].Close();
					//	}
					//	writers.Clear();
					//	writers.Add(date, writer);
					//}
				}
				else
				{
					throw new TimeoutException("获得锁超时");
				}

			}
			catch (Exception e)
			{
				string log = "日志方法异常" + e.Message;
				//throw e;
			}
			//finally
			//{
			//		if (writer != null)
			//		{
			//			writer.Close();
			//		}
			//}
		}

		/// <summary>
		///  json数组打印
		/// </summary>
		/// <param name="desc"></param>
		/// <param name="array"></param>
		public override void Log(string desc, JArray array)
		{
			if (!Open) return;
			string jArrayStr = JsonConvert.SerializeObject(array, Formatting.Indented);
			Log(desc, jArrayStr);
		}

		/// <summary>
		///  json打印
		/// </summary>
		/// <param name="desc"></param>
		/// <param name="obj"></param>
		public override void Log(string desc, JObject obj)
		{
			if (!Open) return;
			string jObjStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
			Log(desc, jObjStr);
		}

		public override void LogJson(string desc, string jsonStr, bool isArray)
		{
			if (isArray)
				Log(desc, JsonConvert.DeserializeObject(jsonStr) as JArray);
			else
				Log(desc, JsonConvert.DeserializeObject(jsonStr) as JObject);
		}

		/// <summary>
		///  xml打印
		/// </summary>
		/// <param name="desc"></param>
		/// <param name="doc"></param>
		public override void LogXml(string desc, string xmlStr)
		{
			if (!Open) return;
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
			string xml = sb.ToString();
			Log(desc, xml);
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
