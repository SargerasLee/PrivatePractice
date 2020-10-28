using System.IO;
using System.Xml;
using Newtonsoft.Json.Linq;

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

		public abstract void Log(params string[] text);
		public abstract void Log(string desc, JArray array);
		public abstract void Log(string desc, JObject obj);
		public abstract void LogXml(string desc, string xmlStr);

		public abstract void LogJson(string desc, string jsonStr, bool isArray);
		protected void CreateDictIfNotExists()
		{
			if (!Directory.Exists(FullFilePath))
			{
				Directory.CreateDirectory(FullFilePath);
			}
		}
	}
}
