/************************************************************************
*Copyright  (c)   2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*命名空间    ：Genersoft.WEICHAI.FSSC.AR.tools
*文件名称    ：ARLogger.cs
*版本号        :   2020|V1.0.0.0 
*=================================
*创 建 者      ：@ lichanghao01
*创建日期    ：2020/4/30 13:33:06 
*功能描述    ：调试用 日志
*使用说明    ：
*=================================
*修改日期    ：2020/4/30 13:33:06 
*修改者        ：lichanghao01
*修改描述    ：
*版本号        :   2020|V1.0.0.0 
***********************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace Tools.Log
{
	/// <summary>
	/// 日志类
	/// </summary>
	public class ArLogger
	{
		public static string FormatPattern { get; set; }
		public static bool Open = true;


		/// <summary>
		/// 通用日志
		/// </summary>
		/// <param name="disk">盘符,根据实际情况</param>
		/// <param name="basePath">盘符下第一级目录</param>
		/// <param name="path">第二级目录</param>
		/// <param name="text">打印文本数组</param>
		public static void Log(string disk, string basePath, string path, params string[] text)
		{
			if (!Open) return;
			DateTime nowTime = DateTime.Now;
			string time = nowTime.ToShortTimeString();
			string date = nowTime.ToString("yyyy-MM-dd");
			string filePath = "{0}:\\{1}\\{2}\\";
			filePath = string.Format(filePath, disk, basePath, path);
			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}

			filePath += $"Debug_{date}.txt";
			StreamWriter writer = null;
			StringBuilder sb = new StringBuilder(50);
			foreach (var s in text)
			{
				sb.Append(s);
				sb.Append("\r\n");
			}
			try
			{
				writer = new StreamWriter(filePath, true, Encoding.Default);

				writer.WriteLine(time + ":    " + sb);
			}
			catch (Exception e)
			{
				// ignored
				throw new Exception(e.Message);
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
		/// 应收日志  路径采用默认
		/// </summary>
		/// <param name="text">打印文本数组</param>
		public static void Log(params string[] text)
		{
			string disk = "C";
			string basePath = "Log";
			string path = "ARLog";
			Log(disk, basePath, path, text);
		}

		/// <summary>
		/// json数组打印
		/// </summary>
		/// <param name="array">json数组</param>
		public static void Log(JArray array)
		{
			string s = JsonConvert.SerializeObject(array, Formatting.Indented);
			Log(s);
		}

		/// <summary>
		/// json打印
		/// </summary>
		/// <param name="obj">单个json对象</param>
		public static void Log(JObject obj)
		{
			string s = JsonConvert.SerializeObject(obj, Formatting.Indented);
			Log(s);
		}

		/// <summary>
		/// xml打印
		/// </summary>
		/// <param name="document">xml文档</param>
		public static void Log(XmlDocument document)
		{
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);
			using (XmlTextWriter writer = new XmlTextWriter(sw))
			{
				writer.Indentation = 4;  // the Indentation
				writer.IndentChar = ' ';
				writer.Formatting = System.Xml.Formatting.Indented;
				document.WriteTo(writer);
				writer.Close();
			}

			string xml = sb.ToString();
			Log(xml);
		}
	}
}
