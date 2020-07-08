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

		public abstract void Log(params string[] text);
		public abstract void Log(string desc, JArray array);
		public abstract void Log(string desc, JObject obj);
		public abstract void Log(string desc, XmlDocument doc);
	}
}
