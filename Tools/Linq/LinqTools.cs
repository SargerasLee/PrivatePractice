using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Tools.Linq
{
	public class LinqTools
	{
		public static void SimpleQuery()
		{
			var nums = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
			//2.创建查询  
			var numQuery =
				from num in nums
				where(num % 2) == 0
				orderby num descending
				select num;

			var numList =
			(from num in nums
			 where (num % 2) == 0
			 select num).ToList();

			//3.执行查询  
			foreach (var num in numQuery)
            {
				Console.WriteLine("{0}", num);
			 }
			 //4.聚合查询
			Console.WriteLine(numQuery.Count());
			Console.WriteLine(numQuery.Max());
			Console.WriteLine(numQuery.Average());
			Console.WriteLine(numQuery.First());
			Console.WriteLine(numQuery.Last());
			Console.WriteLine(numQuery.Min());
		}

		public static void XmlQuery<T>(T t)
		{
			
			XmlSerializer xs = new XmlSerializer(t.GetType());

			StringBuilder sb = new StringBuilder();
			XmlWriter xw =  XmlWriter.Create(sb,new XmlWriterSettings 
			{ Encoding=Encoding.UTF8,Indent=true,IndentChars="	",NewLineChars="\n"});

			XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
			xsn.Add(string.Empty, string.Empty);//命名空间置空
			xs.Serialize(xw, t,xsn);

			string xmlStr = sb.ToString();
			
			StringReader sr = new StringReader(xmlStr);
			XmlReader xmlReader = XmlReader.Create(sr);
			Console.WriteLine(xmlStr);
			
			var contacts = XElement.Load(xmlReader);
			xw.Close();
			xmlReader.Close();
			sr.Close();
		}

		/// <summary>
		/// 列表转datatable，T为实体类，只写公共属性，别写字段 例如public string Name{set;get;}
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
			foreach(var info in infos)
			{
				table.Columns.Add(info.Name, info.PropertyType);
				Console.Write(info.Name + "	");
			}
			Console.WriteLine();
			foreach(var item in list)
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
	}
}
