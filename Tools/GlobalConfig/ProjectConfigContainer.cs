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
		private static Dictionary<string, string> properties = new Dictionary<string, string>();
		private static readonly object obj = new object();
		private static string path = "../zzy/cnhtc/ConfigFile/ProjectGlobalConfig.xml";
		private static bool flag = false;
		private static DateTime lastModifyTime;
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
						lastModifyTime = File.GetLastWriteTime(path);
						//GSPContext.Current.ServerInstallPath
						xd.Load(path);
						XmlNodeList list = xd.SelectNodes("/Configuration/Modules/Properties/Property");
						properties.Clear();
						foreach (XmlNode item in list)
						{
							properties.Add(item.Attributes["code"].Value, item.Attributes["value"].Value);
						}
						flag = true;
					}
				}
			}
		}

		public static string GetProperty(string propertyCode)
		{
			if (File.GetLastWriteTime(path) > lastModifyTime)
				Load();
			return properties[propertyCode];
		}
	}
}
