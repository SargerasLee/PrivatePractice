using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Tools.Convert
{
	public class ConvertTools
	{
		/// <summary>
		/// 列表转datatable，泛型T为实体类，只写公共属性，例如   public string Name{set;get;}
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static DataTable ListToDataTable<T>(List<T> list)
		{
			Type t = typeof(T);
			DataTable table = new DataTable(t.Name);
			DataRowCollection collection = table.Rows;
			PropertyInfo[] infos = t.GetProperties();
			foreach (var info in infos)
			{
				table.Columns.Add(info.Name, info.PropertyType);
			}
			foreach (var item in list)
			{
				DataRow row = table.NewRow();
				foreach (var info in infos)
				{
					row[info.Name] = info.GetValue(item);
				}
				collection.Add(row);
			}
			return table;
		}

		/// <summary>
		///  实体类转xml字符串
		///  根节点默认ArrayOf+（Entity类名）
		/// </summary>
		/// <param name="t">实体类列表</param>
		/// <param name="encoding">序列化时的编码</param>
		/// <param name="nameSpace">名称空间</param>
		/// <param name="prefix">前缀</param>
		/// <param name="settings">xmlsetting</param>
		/// <returns></returns>
		public static string EntityToXmlStr<T>(T t,string prefix,string nameSpace,XmlWriterSettings settings)
		{
			XmlSerializer xs = new XmlSerializer(t.GetType());

			XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
			xsn.Add(prefix, nameSpace);//命名空间置空

			if (settings == null)
				settings = new XmlWriterSettings { Encoding = Encoding.UTF8, Indent = true, IndentChars = "	", NewLineChars = "\r\n" };
			
			StringBuilder sb = new StringBuilder();
			
			using (XmlWriter xw = XmlWriter.Create(sb, settings))
			{
				xs.Serialize(xw, t, xsn);
			}
			return sb.ToString();
		}

		public static T JsonArrayStrToEntity<T>(string json)
		{
			T list = JsonConvert.DeserializeObject<T>(json);
			return list;
		}

		public static string JsonArrayStrToXmlStr<T>(string json)
		{
			T list = JsonArrayStrToEntity<T>(json);
			return EntityToXmlStr(list, string.Empty, string.Empty, null);
		}

		public static T XmlStrToEntity<T>(string xmlStr)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xmlStr);
			StringReader sr = new StringReader(xmlStr);
			XmlReader xr = XmlReader.Create(sr);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			T t;
			if (xmlSerializer.CanDeserialize(xr))
			{
				t = (T)xmlSerializer.Deserialize(xr);
			}
			else
				throw new XmlException("该xml字符串不能被序列化");
			return t;
		}

		public static string EntityToJsonArrayStr<T>(T list) 
		{
			return JsonConvert.SerializeObject(list);
		}
	}
}
