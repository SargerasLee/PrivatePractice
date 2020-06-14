using System;
using Logic.Sort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class TestSort
	{
		[TestMethod]
		public void TestBubbleSort()
		{
			int[] arr = new int[100];
			Random random=new Random();
			for (int i = 0; i < arr.Length; i++)
			{
				arr[i] = (int)(random.NextDouble() * 1000);
			}
			SortUtil.BubbleSort(arr,Compare);
		}


		[TestMethod]
		public void TestSelectSort()
		{
			int[] arr = new int[10];
			Random random = new Random();
			for (int i = 0; i < arr.Length; i++)
			{
				arr[i] = (int)(random.NextDouble() * 1000);
			}
			SortUtil.SelectSort(arr, Compare);
		}

		[TestMethod]
		public void TestInsertSort()
		{
			int[] arr = new int[10];
			Random random = new Random();
			for (int i = 0; i < arr.Length; i++)
			{
				arr[i] = (int)(random.NextDouble() * 1000);
			}
			SortUtil.InsertSort(arr, Compare);
		}
		private static bool Compare(int n1, int n2) => n1 - n2 > 0;
	}
}
