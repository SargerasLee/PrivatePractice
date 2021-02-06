using System;
using System.Collections.Generic;
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
		private static bool flag = false;
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
						xd.Load("GlobalConfig/GlobalConfig.xml");
						XmlNodeList list = xd.SelectNodes("/Configuration/Modules/Properties/Property");
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
			return properties[propertyCode];
		}
	}
}
