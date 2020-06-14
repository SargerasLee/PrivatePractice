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
			Console.WriteLine(DateTime.Now);
			SortUtil.BubbleSort(arr,Compare);
			Console.WriteLine(DateTime.Now);
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
			Console.WriteLine(DateTime.Now);
			SortUtil.SelectSort(arr, Compare);
			Console.WriteLine(DateTime.Now);
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
			Console.WriteLine(DateTime.Now);
			SortUtil.InsertSort(arr, Compare);
			Console.WriteLine(DateTime.Now);
		}

		[TestMethod]
		public void TestShellSort()
		{
			int[] arr = new int[10];
			Random random = new Random();
			for (int i = 0; i < arr.Length; i++)
			{
				arr[i] = (int)(random.NextDouble() * 1000);
			}
			Console.WriteLine(DateTime.Now);
			SortUtil.ShellSort(arr, Compare,"insert");
			Console.WriteLine(DateTime.Now);
		}

		[TestMethod]
		public void TestQuickSort()
		{
			int[] arr = new int[10];
			Random random = new Random();
			for (int i = 0; i < arr.Length; i++)
			{
				arr[i] = (int)(random.NextDouble() * 1000);
			}
			Console.WriteLine(DateTime.Now);
			SortUtil.QuickSort(arr, Compare,0,arr.Length-1);
			Console.WriteLine(DateTime.Now);
			
		}
		private static bool Compare(int n1, int n2) => n1 - n2 > 0;
	}
}
