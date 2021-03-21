using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tools.Log;

namespace Tools.GlobalConfig
{
	public static class ProjectConfigContainer
	{
		private static readonly object obj = new object();
		private const string PATH = "GlobalConfig/ProjectGlobalConfig.xml";
		private static bool flag = false;
		private static DateTime lastModifyTime;

		public static Dictionary<string, string> Properties { get; } = new Dictionary<string, string>();

		public static List<string> Assemblies { get; } = new List<string>();

		static ProjectConfigContainer()
		{
			Load();
		}

		private static void Load()
		{
			if (!flag)
			{
				lock (obj)
				{
					if (!flag)
					{
						XmlDocument xd = new XmlDocument();
						lastModifyTime = File.GetLastWriteTime(PATH);
						//GSPContext.Current.ServerInstallPath
						xd.Load(PATH);
						PutProperties(xd);
						PutAssemblies(xd);
						flag = true;
					}
				}
			}
		}

		private static void PutAssemblies(XmlDocument xd)
		{
			XmlNodeList assembliesList = xd.SelectNodes("/Configuration/Modules/Componment-Scan/Assembly");
			Assemblies.Clear();
			foreach (XmlNode item in assembliesList)
			{
				Assemblies.Add(item.Attributes["name"].Value);
			}
		}

		private static void PutProperties(XmlDocument xd)
		{
			XmlNodeList list = xd.SelectNodes("/Configuration/Modules/Properties/Property");
			Properties.Clear();
			foreach (XmlNode item in list)
			{
				Properties.Add(item.Attributes["code"].Value, item.Attributes["value"].Value);
			}
		}

		public static string GetProperty(string propertyCode)
		{
			if (File.GetLastWriteTime(PATH) > lastModifyTime)
				Load();
			return Properties[propertyCode];
		}
	}
}
