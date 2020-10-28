using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace Tools.Log
{
	public class CommonLogger : GeneralLogger
	{
		public CommonLogger()
		{
			DatePattern = "yyyy-MM-dd";	
		}

		/// <summary>
		/// 通用
		/// </summary>
		/// <param name="text"></param>
		public override void Log(params string[] text)
		{
			if (!Open) return;
			StringBuilder sb = new StringBuilder(2000);
			foreach (var s in text)
			{
				sb.Append(s);
				sb.Append("\r\n");
			}
			StreamWriter writer = null;

			DateTime nowTime = DateTime.Now;
			string date = nowTime.ToString(DatePattern);
			string time = nowTime.ToShortTimeString();
			try
			{
				CreateDictIfNotExists();
				string path = FullFilePath + "Log" + date + ".txt";
				writer = new StreamWriter(path, true, Encoding.Default);
				writer.WriteLine(time + ":    " + sb);
			}
			catch (Exception e)
			{
				string log = "日志方法异常" + e.Message;
				throw new Exception(log);
			}
			finally
			{
				if (writer != null)
				{
					writer.Close();
					writer.Dispose();
				}
			}
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
	}
}
