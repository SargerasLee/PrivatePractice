﻿/************************************************************************
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
using System.IO;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace Tools.Log
{
	/// <summary>
	/// 日志类
	/// </summary>
	public abstract class GeneralLogger
	{
		public string DatePattern { get; set; }
		public bool Open{ set; get; }
		public string FullFilePath{ set; get; }

		public string Date{ set; get; }
		public string LogCode { set; get; }

		public GeneralLogger()
		{
		}

		public void CreateFileIfNotExists()
		{
			if (!Directory.Exists(FullFilePath))
			{
				Directory.CreateDirectory(FullFilePath);
			}
		}

		public void Log(params string[] text)
		{
			DateTime nowTime = DateTime.Now;
			string time = nowTime.ToShortTimeString();
			string date = nowTime.ToString("yyyy-MM-dd");
			string filePath = "{0}:\\{1}\\{2}\\";
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
		/// json数组打印
		/// </summary>
		/// <param name="array">json数组</param>
		public void Log(JArray array)
		{
			string s = JsonConvert.SerializeObject(array, Formatting.Indented);
			Log(s);
		}

		/// <summary>
		/// json打印
		/// </summary>
		/// <param name="obj">单个json对象</param>
		public void Log(JObject obj)
		{
			string s = JsonConvert.SerializeObject(obj, Formatting.Indented);
			Log(s);
		}

		/// <summary>
		/// xml打印
		/// </summary>
		/// <param name="document">xml文档</param>
		public void Log(XmlDocument document)
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
