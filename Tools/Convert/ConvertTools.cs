using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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
		/// dataset转列表
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<T> DataTableToList<T>(DataTable table)
		{
			List<T> list = new List<T>();
			Type type = typeof(T);
			PropertyInfo[] pInfo = type.GetProperties();
			List<PropertyInfo> info = pInfo.ToList();
			foreach (DataRow row in table.Rows)
			{
				T t = Activator.CreateInstance<T>();
				for (int i = 0; i < table.Columns.Count; i++)
				{
					PropertyInfo pi = info.Find(p => p.Name == table.Columns[i].ColumnName);
					if (pi.PropertyType == typeof(string))
						pi.SetValue(t, row[i] is DBNull | row[i] is null ? "" : System.Convert.ToString(row[i]));
					else if (pi.PropertyType == typeof(int))
						pi.SetValue(t, row[i] is DBNull | row[i] is null ? 0 : System.Convert.ToInt32(row[i]));
					else if (pi.PropertyType == typeof(decimal))
						pi.SetValue(t, row[i] is DBNull | row[i] is null ? 0m : System.Convert.ToDecimal(row[i]));
					else if (pi.PropertyType == typeof(bool))
						pi.SetValue(t, row[i] is DBNull | row[i] is null ? false : System.Convert.ToBoolean(row[i]));
					else if (pi.PropertyType == typeof(double))
						pi.SetValue(t, row[i] is DBNull | row[i] is null ? 0d : System.Convert.ToDouble(row[i]));
					else if (pi.PropertyType == typeof(byte))
						pi.SetValue(t, row[i] is DBNull | row[i] is null ? 0 : System.Convert.ToByte(row[i]));
					else if (pi.PropertyType == typeof(char))
						pi.SetValue(t, row[i] is DBNull | row[i] is null ? '\0' : System.Convert.ToChar(row[i]));
					else
						pi.SetValue(t, row[i] is DBNull ? null : row[i]);
				}
				list.Add(t);
			}
			return list;
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
