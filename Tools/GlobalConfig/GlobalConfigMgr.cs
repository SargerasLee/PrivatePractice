using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tools.Log;

namespace Tools.GlobalConfig
{
	public sealed class GlobalConfigMgr
	{
		private static Dictionary<string, string> properties = new Dictionary<string, string>();
		private static readonly object obj = new object();
		private static bool flag = false;
		static GlobalConfigMgr()
		{
			new LoggerFactory().GetInstance("test").Log("静态构造函数执行了");
			XmlDocument xd = new XmlDocument();
			xd.Load("GlobalConfig/GlobalConfig.xml");
			FillTable(ref xd);
		}

		private static void FillTable(ref XmlDocument xd)
		{
			if (!flag)
			{
				lock (obj)
				{
					if (!flag)
					{
						XmlNodeList list = xd.SelectNodes("Configuration/properties/property");
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
