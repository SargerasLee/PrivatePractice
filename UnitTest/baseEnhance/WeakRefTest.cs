using System;
using System.Threading;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class WeakRefTest
	{
		[TestMethod]
		public void TestWeakRef()
		{
			WeakReference weakReference = new WeakReference(new MyTest());
			MyTest my;
			if(weakReference.IsAlive)
			{
				my = weakReference.Target as MyTest;
				my.Value = "50";
				Console.WriteLine(my.Value);
			}
			else
			{
				Console.WriteLine("对象已被回收");
			}
			GC.Collect();
			for (int i = 0; i < 100000; i++) { }
			Thread.Sleep(5000);
			if(!weakReference.IsAlive)
			{
				Console.WriteLine("对象已被回收");
			}
			else
			{
				Console.WriteLine("我还活着");
			}
		}

		[TestMethod]
		public void Test2()
		{
			Array arr = Array.CreateInstance(typeof(int), 5);
			arr.SetValue(5, 0);
			arr.Clone();
			ArraySegment<int> ass= new ArraySegment<int>((int[])arr,0,3);
		}
	}
	struct Ha
	{
		int a;
	}
	class MyTest
	{
		public string Value{ get; set; }
	}
}
