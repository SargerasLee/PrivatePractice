using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Tools.Convert
{
	public class ConvertTools
	{
		/// <summary>
		/// 列表转datatable，泛型T为实体类，只写公共属性，别写字段 例如public string Name{set;get;}
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static DataTable ListToDataTable<T>(List<T> list)
		{
			Type t = typeof(T);
			Console.WriteLine(t.Name);
			DataTable table = new DataTable(t.Name);
			DataRowCollection collection = table.Rows;
			PropertyInfo[] infos = t.GetProperties();
			Console.WriteLine(infos.Length);
			foreach (var info in infos)
			{
				table.Columns.Add(info.Name, info.PropertyType);
				Console.Write(info.Name + "	");
			}
			Console.WriteLine();
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
		/// <typeparam name="List<T>">实体类列表</typeparam>
		/// <param name="t"></param>
		/// <returns></returns>
		public static string EntityToXmlStr<T>(List<T> t)
		{
			XmlSerializer xs = new XmlSerializer(t.GetType());

			StringBuilder sb = new StringBuilder();
			XmlWriter xw = XmlWriter.Create(sb, new XmlWriterSettings
			{ Encoding = Encoding.UTF8, Indent = true, IndentChars = "	", NewLineChars = "\r\n" });

			XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
			xsn.Add(string.Empty, string.Empty);//命名空间置空
			xs.Serialize(xw, t, xsn);

			string xmlStr = sb.ToString();
			xw.Close();
			return xmlStr;
		}

		public static List<T> JsonArrayStrToEntity<T>(string json)
		{
			List<T> list = JsonConvert.DeserializeObject<List<T>>(json);
			return list;
		}

		public static string JsonArrayStrToXmlStr<T>(string json)
		{
			List<T> list = JsonArrayStrToEntity<T>(json);
			return EntityToXmlStr<T>(list);
		}

		public static List<T> XmlStrToEntity<T>(string xmlStr)
		{
			return null;
		}

		public static string DataTableToJsonArrayStr<T>(DataTable table)
		{
			return null;
		}
	}
}
