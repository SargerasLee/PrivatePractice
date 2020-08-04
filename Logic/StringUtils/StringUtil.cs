using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.StringUtils
{
	public class StringUtil
	{
		/// <summary>
		/// 有序发票字符串处理  10000001......10000010  -> 10000001-10000010
		/// </summary>
		/// <param name="invoice"></param>
		/// <returns></returns>
		public static string DealInvoice(string invoice)
		{
			/**
			 * 思路：
			 * 1.先根据逗号拆分发票成数组
			 * 2.根据前5位吧数组进行分组 DoGroup
			 * 3.数组中1,2,3,4,6,7,8,9,11,24,25,26 选出不连续的，拆成段1-4,6-9,24-26 DoFragment
			 * 4.拼接
			 */
			Console.WriteLine(DateTime.Now.ToString());
			string[] invoices = invoice.Split(',');
			List<List<string>> list = new List<List<string>>();
			DoGroup(0, invoices, ref list);
			string res = DoFragment(list);
			Console.WriteLine(DateTime.Now.ToString());
			return res;
		}

		private static void DoGroup(int start, string[] invoices, ref List<List<string>> datas)
		{
			/*
			 * 递归分组，每一次递归形成一个新组，invoices已经排好序
			 */
			bool flag = false;//标志位
			string temp = invoices[start].Substring(0, 5);//当前以第一个数的前5位做标准
			List<string> list = new List<string>();//存放一组的数据
			for (int i = start; i < invoices.Length; i++)
			{
				if (invoices[i].Substring(0, 5) == temp)//用前5位判断分组依据
				{
					list.Add(invoices[i]);
				}
				else
				{
					flag = true;
					datas.Add(list);
					DoGroup(i, invoices, ref datas);//递归进行下一组 分组
					break;
				}
			}
			if (!flag)//用标志位区分走没走else分支， 没走在这里吧list添加进集合中
			{
				datas.Add(list);
			}
		}

		private static string DoFragment(List<List<string>> list)
		{
			/*
			 * 每一组中进行分段
			 */
			StringBuilder sb = new StringBuilder(500);
			foreach (List<string> item in list)
			{
				int start = 0;//存放下一段开始的索引
				for (int i = 0; i < item.Count - 1; i++)
				{
					if (StringSubmit(item[i + 1], item[i]) - 1 != 0)
					{
						sb.Append(item[start]);

						if (i != start)//如果遍历的符合条件的当前位置  不是当前段开始，才加入-
						{
							sb.Append("-");
							sb.Append(item[i].Substring(5));
						}
						sb.Append(",");
						start = i + 1;
					}
				}
				//添加最后一段
				sb.Append(item[start]);
				if (start != item.Count - 1)
				{
					sb.Append("-");
					sb.Append(item[item.Count - 1].Substring(5));
				}
				sb.Append(",");
			}
			sb.Remove(sb.Length - 1, 1);//去掉最后一个逗号
			return sb.ToString();
		}

		private static int StringSubmit(string s1, string s2)
		{
			return Convert.ToInt32(s1) - Convert.ToInt32(s2);
		}
	}
}
