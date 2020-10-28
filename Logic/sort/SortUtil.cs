using System;

namespace Logic.Sort
{
	public static class SortUtil
	{
		#region 冒泡排序
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
				//array.PrintArray();
			}
		}
		#endregion

		#region 选择排序
		/// <summary>
		/// 选择排序
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		public static void SelectSort<T>(T[] array,Func<T,T,bool> compareFunc)
		{
			for(int i=0;i<array.Length-1;i++)//共进行n-1趟选择
			{
				for(int j=i+1;j<array.Length;j++)//从第i+1个位置到底n-1个位置和第i个位置比较 ，并交换小的数
				{
					if(compareFunc(array[i],array[j]))
					{
						var temp = array[i];
						Console.Write(temp+"   ");
						array[i] = array[j];
						Console.Write(array[i]+"   ");
						array[j] = temp;//对象交换可能有问题
						Console.Write(array[j]+"   ");
						Console.WriteLine();
					}
				}
				array.PrintArray();
			}
		}
		#endregion

		#region 插入排序
		/// <summary>
		/// 插入排序
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		public static void InsertSort<T>(T[] array, Func<T, T, bool> compareFunc)
		{
			for(int i=1;i<array.Length;i++)//进行n-1趟
			{
				var temp = array[i];//先保留当前要插入有序表的数
				int t = i - 1;//取有序表最后一个数下标
				while(t>=0 && compareFunc(array[t],temp))//下标没越界 或 与有序表当前数数一次比较，若小，进入循环
				{
					array[t+1] = array[t];//则当前数向后移动一位
					t--;//有序表下标向前移动一位
				}
				array[t+1] = temp;//循环结束，说明到了最前边或者不再小于有序表当前数，有序表当前位置+1即为插入位置
				array.PrintArray();
			}
		}
		#endregion

		#region 希尔排序
		/// <summary>
		/// 希尔排序
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		public static void ShellSort<T>(T[] array, Func<T, T, bool> compareFunc,string mode)
		{
			switch(mode)
			{
				case "insert":ShellInsertSort(array, compareFunc); break;
				case "exchange":ShellExchangeSort(array, compareFunc); break;
			}
		}
		private static void ShellInsertSort<T>(T[] array, Func<T, T, bool> compareFunc)
		{
			T temp;
			for (int step = array.Length / 2; step > 0; step /= 2)
			{
				for (int i = step; i < array.Length; i++)
				{
					temp = array[i];//先保留当前要插入有序表的数
					int t = i - step;//取有序表最后一个数下标
					while (t >= 0 && compareFunc(array[t], temp))
					{
						array[t + step] = array[t];
						t-=step;
					}
					array[t + step] = temp;//循环结束，说明到了最前边或者不再小于有序表当前数，有序表当前位置+1即为插入位置
				}
				array.PrintArray();
			}
		}
		private static void ShellExchangeSort<T>(T[] array, Func<T, T, bool> compareFunc)
		{
			T temp;
			for (int step = array.Length / 2; step > 0; step /= 2)
			{
				for (int i = step; i < array.Length; i++)
				{
					for (int j = i - step; j >= 0; j -= step)
					{
						if (compareFunc(array[j], array[j + step]))
						{
							temp = array[j];
							array[j] = array[j + step];
							array[j + step] = temp;
						}
					}
				}
				array.PrintArray();
			}
		}

		#endregion

		#region 快速排序
		/// <summary>
		/// 快速排序
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		public static void QuickSort<T>(T[] array, Func<T, T, bool> compareFunc, int left,int right)
		{
			if (left<right)
			{
				T temp = array[left];
				int i = left;
				int j = right;
				while (i < j)
				{
					while (i < j && compareFunc(array[j], temp))
						j--;
					if (i < j)
					{
						array[i] = array[j];
						i++;
					}
					while (i < j && compareFunc(temp, array[i]))
						i++;
					if (i < j)
					{
						array[j] = array[i];
						j--;
					}
				}
				array[i] = temp;
				QuickSort(array, compareFunc, left, i - 1);
				QuickSort(array, compareFunc, i + 1, right); 
			}
			array.PrintArray();
		}
		#endregion

		#region 输出数组
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
		#endregion
	}
}
