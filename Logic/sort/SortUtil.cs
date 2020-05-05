using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Sort
{
	public static class SortUtil
	{
		/// <summary>
		/// 冒泡排序
		/// </summary>
		/// <param name="array"></param>
		/// <param name="compareFunc"></param>
		public static void BubbleSort<T>(T[] array,Func<T,T,bool> compareFunc)
		{
			//每经过一趟排序，把本趟最大的数放在本趟最后一个位置
			int len = array.Length;
			//设置标志位，如果在一趟排序中未发生交换，那么说明数组已有序，可以终止循环
			bool flag = false;
			//进行len-1趟排序
			for (int i = 0; i < len-1; i++)
			{
				//每进行一趟，前面数比后面大，进行交换
				for (int j = 0; j < len - 1 - i; j++)
				{
					if (compareFunc(array[j], array[j + 1]))
					{
						flag = true;
						var temp = array[j];
						array[j] = array[j + 1];
						array[j + 1] = temp;
					}
				}
				if (!flag)
					break;
				else
					flag = false;
				array.PrintArray();
			}
		}

		/// <summary>
		/// 选择排序
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		public static void SelectSort<T>(T[] array,Func<T,T,bool> compareFunc)
		{

		}



		/// <summary>
		/// 泛型数组扩展方法，测试输出用
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		public static void PrintArray<T>(this T[] array)
		{
			foreach (var t in array)
			{
				Console.Write(t+" ");
			}
			Console.WriteLine();
		}
	}
}
