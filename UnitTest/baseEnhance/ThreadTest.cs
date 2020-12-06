using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class ThreadTest
	{
		[TestMethod]
		public void TestParal()
		{
			Parallel.For(10, 20, async (int i, ParallelLoopState pls) => 
			{
				Console.WriteLine(i + " task " + Task.CurrentId + " thread " + Thread.CurrentThread.ManagedThreadId);
				await Task.Delay(100);
				Console.WriteLine(i + " task " + Task.CurrentId + " thread " + Thread.CurrentThread.ManagedThreadId);
			});
			//ParallelOptions po = new ParallelOptions();
			Thread.Sleep(2000);
		}
	}
}
