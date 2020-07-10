using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Tools.Convert;

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

		public static void XmlQuery<T>(List<T> t)
		{
			string xmlStr = ConvertTools.EntityToXmlStr<T>(t, string.Empty, string.Empty, null);
			StringReader sr = new StringReader(xmlStr);
			XmlReader xmlReader = XmlReader.Create(sr);
			Console.WriteLine(xmlStr);
			var contacts = XElement.Load(xmlReader);
			xmlReader.Close();
			sr.Close();
		}
	}
}
