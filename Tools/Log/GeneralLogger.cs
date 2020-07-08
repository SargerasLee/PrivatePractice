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
		public abstract void Log(string desc, XmlDocument doc);
	}
}
