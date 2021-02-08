using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest.Practise
{
	[TestClass]
	public class ArrayTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			//int a = MajorityElement(new int[] { 3, 2, 3 });
			int[] a = { 1, 2, 4, 5, 7 };
			//SumOddLengthSubarrays(a);
			//Reverse(1534236469);
			IList<string> list = SummaryRanges(a);
			foreach(var item in list)
				Console.WriteLine(item);
		}

		/// <summary>
		/// 矩阵转置
		/// </summary>
		/// <param name="A"></param>
		/// <returns></returns>
		public int[][] Transpose(int[][] A)
		{
			int[][] B = new int[A[0].Length][];
			for(int n =0;n<B.Length;n++)
			{
				B[n] = new int[A.Length];
			}
			for (int i = 0; i < A.Length; i++)
			{
				for (int j = 0; j < A[0].Length; j++)
				{
					B[j][i] = A[i][j];
				}
			}
			return B;
		}

		/// <summary>
		/// 数组中占比超过一半的元素称之为主要元素。
		/// 给定一个整数数组，找到它的主要元素。若没有，返回-1。
		/// </summary>
		/// <param name="nums"></param>
		/// <returns></returns>
		public int MajorityElement(int[] nums)
		{
			int count = 0;
			int rkey = -1;
			int half = nums.Length / 2 + 1;
			Dictionary<int, int> dict = new Dictionary<int, int>();
			for(int i=0;i<nums.Length;i++)
			{
				if(dict.ContainsKey(nums[i]))
				{
					dict[nums[i]] += 1;
				}
				else
				{
					dict[nums[i]] = 1;
				}
			}
			foreach(var key in dict.Keys)
			{
				if (dict[key] > count)
				{
					count = dict[key];
					rkey = key;
				}
			}
			return count >= half ? rkey : -1;
		}

		/// <summary>
		/// 给定一个按非递减顺序排序的整数数组 A，
		/// 返回每个数字的平方组成的新数组，要求也按非递减顺序排序。
		/// </summary>
		/// <param name="nums"></param>
		/// <returns></returns>
		public int[] SortedSquares(int[] nums)
		{
			int[] newNums = new int[nums.Length];
			for(int i=0;i<nums.Length;i++)
			{
				newNums[i] = nums[i] * nums[i];
			}
			Array.Sort(newNums);
			return newNums;
		}

		/// <summary> 
		/// 48、旋转图像
		/// 给定一个 n × n 的二维矩阵表示一个图像。
		///将图像顺时针旋转 90 度。
		///你必须在原地旋转图像，这意味着你需要直接修改输入的二维矩阵。
		///请不要使用另一个矩阵来旋转图像。
		/// </summary>
		/// <param name="matrix"></param>
		public void Rotate(int[][] matrix)
		{
			/*
			 简单思路：先水平翻转 在转置 ，我自己没想到
			 */
			/* n*n
			  [1,   2,   3,  4]
			  [5,   6,   7,  8]
			  [9,  10,11,12]
			  [13,14,15,16]
			  a[row][col]->a[col][n-1-row]
			  a[col][n-1-row]->a[n-1-row][n-1-col]
			  a[n-1-row][n-1-col]->a[n-1-col][row]
			  a[n-1-col][row]->a[row][col]
			  分为四步。上述的逆序即为赋值的顺序
			  len/2 (len+1)/2 是小技巧
			  偶数时需要枚举 n/2 * n/2个
			  奇数时需要枚举 (n-1)/2 * (n+1/)2个
			  折中 ，奇数偶数 行都为 n/2==n-1/2 整型
			*/
			int temp;
			int len = matrix.Length;
			for(int i=0;i< len/2; i++)
			{
				for(int j=0;j< (len+1)/2; j++)
				{
					temp = matrix[i][j];
					matrix[i][j] = matrix[len - 1 - j][i];
					matrix[len - 1 - j][i] = matrix[len - 1 - i][len - 1 - j];
					matrix[len - 1 - i][len - 1 - j] = matrix[j][len - 1 - i];
					matrix[j][len - 1 - i] = temp;
				}
			}
		}

		/// <summary>
		/// 1588. 所有奇数长度子数组的和
		/// </summary>
		/// <param name="arr"></param>
		/// <returns></returns>
		public int SumOddLengthSubarrays(int[] arr)
		{
			int odd = arr.Length % 2 == 0 ? arr.Length - 1 : arr.Length;
			int s = 0;
			for (int i = 1; i <=arr.Length; i += 2)//1,3,5,7.。。odd 元素个数
			{	
				int sum = 0;
				for(int j=0;j<arr.Length-i+1;j++)// 同元素个数 子数组有多少个
				{
					int ss = 0;
					for(int k =j;k<j+i;k++)//同元素个数 每个子数组求和
					{
						ss += arr[k];
					}
					sum += ss;
				}
				s += sum;
			}
			return s;
		}

		/// <summary>
		/// 628. 三个数的最大乘积
		/// </summary>
		/// <param name="nums"></param>
		/// <returns></returns>
		public int MaximumProduct(int[] nums)
		{
			//2个负数 1个整数，或 3个正数
			Array.Sort(nums);
			int a = nums[0] * nums[1] * nums[nums.Length - 1];
			int b = nums[nums.Length - 3] * nums[nums.Length - 2] * nums[nums.Length - 1];
			return a > b ? a : b;
		}

		/// <summary>
		/// 7. 整数反转
		/// 给出一个 32 位的有符号整数，你需要将这个整数中每位上的数字进行反转。
		/// 输入: 120   输出: 21    -123   -321
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public int Reverse(int x)
		{
			//吧每一位放到list 里，在乘10累加
			try
			{
				if (x == 0)
					return x;
				else
				{
					List<int> list = new List<int>();
					while (x / 10 != 0)
					{
						list.Add(x % 10);
						x /= 10;
					}
					list.Add(x % 10);
					int sum = 0;
					foreach(int i in list)
					{
						checked
						{
							sum = sum * 10 + i; 
						}
					}
					return sum;
				}
			}
			catch (OverflowException)
			{
				return 0;
			}
		}


		/// <summary>
		/// 228. 汇总区间
		/// 给定一个  无重复元素  的  有序整数数组  nums 。
		/// 输入：nums = [0,2,3,4,6,8,9] 输出：["0","2->4","6","8->9"]
		/// 解释：区间范围是：
		/// [0,0] --> "0"
		/// [2,4] --> "2->4"
		/// [6,6] --> "6"
		/// [8,9] --> "8->9"
		/// </summary>
		/// <param name="nums"></param>
		/// <returns></returns>
		public IList<string> SummaryRanges(int[] nums)
		{
			// 1 2  4 5  7
			//用start 和end 记录一组中 的开始位置和结束位置
			List<string> list = new List<string>();
			if (nums.Length == 0)
				return list;
			else if (nums.Length == 1)
			{
				list.Add(nums[0].ToString());
				return list;
			}
			else
			{
				int start = 0;
				int end = 0;

				for (int i = start; i < nums.Length - 1; i++)
				{
					if (nums[i] == nums[i + 1] - 1)
					{
						end = i + 1;
						continue;
					}
					else
					{
						end = i;
						if (start == end)
						{
							list.Add($"{nums[i]}");
						}
						else
						{
							list.Add($"{nums[start]}->{nums[end]}");
						}
						//if (i == nums.Length - 2)
						//{
						//	list.Add($"\"{nums[i + 1]}\"");
						//	start = i + 1;
						//}	
						//else
						start = i + 1;
					}
				}
				if (start == end+1)
					list.Add($"{nums[start]}");
				else
					list.Add($"{nums[start]}->{nums[end]}");
			}
			return list;
		}
	}
}
