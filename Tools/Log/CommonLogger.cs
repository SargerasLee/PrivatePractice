using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Runtime.InteropServices;
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
			Date = DateTime.Now.ToString(DatePattern);
		}
		public override void Log(params string[] text)
		{
			if (!Open) return;
			DateTime nowTime = DateTime.Now;
			string time = nowTime.ToShortTimeString();
			StreamWriter writer = null;
			StringBuilder sb = new StringBuilder(200);
			foreach (var s in text)
			{
				sb.Append(s);
				sb.Append("\r\n");
			}
			try
			{
				writer = new StreamWriter(FullFilePath, true, Encoding.Default);
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
			string s = JsonConvert.SerializeObject(array, Formatting.Indented);
			Log(desc, s);
		}

		/// <summary>
		///  json打印
		/// </summary>
		/// <param name="desc"></param>
		/// <param name="obj"></param>
		public override void Log(string desc, JObject obj)
		{
			if (!Open) return;
			string jStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
			Log(desc, jStr);
		}

		/// <summary>
		///  xml打印
		/// </summary>
		/// <param name="desc"></param>
		/// <param name="doc"></param>
		public override void Log(string desc, XmlDocument doc)
		{
			if (!Open) return;
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
