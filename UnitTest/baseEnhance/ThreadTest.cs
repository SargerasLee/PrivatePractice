using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Timers;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class ThreadTest
	{
		public int State{ get; set; }
		private static readonly object obj = new object();
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

		[TestMethod]
		public void TestSync()
		{
			int i = 0;
			var tasks = new Task[20];
			for(int n=0;n<20;n++)
			{
				tasks[i] = Task.Run(DoLoop);
			}
			for (int n = 0; n < 20; n++)
			{
				tasks[i].Wait();
			}
			Console.WriteLine(State);
		}

		[TestMethod]
		public void TestMutiThread()
		{
			Mutex sps = new Mutex(false,"shit",out bool New);
			Task[] tasks = new Task[6];
			for(int i=0;i<6;i++)
			{
				tasks[i] = Task.Run(() => TaskDo(sps));
			}
			Task.WaitAll(tasks);
			Console.WriteLine("All tasks are finished");
		}

		[TestMethod]
		public void TestBarrier()
		{
			Barrier b = new Barrier(3);
			int[] x = new int[3] { 3, 4, 6 };
			int[] y = new int[4] { 6, 34, 33,77 };
			int[] z = x.Zip(y, (c1, c2) => c1 * c2).ToArray();
			foreach(int i in z)
			{
				Console.WriteLine(i);
			}
		}

		[TestMethod]
		public void TestReadWriteLock()
		{
			ReaderWriterLockSlim rwls = new ReaderWriterLockSlim();
			SemaphoreSlim ss = new SemaphoreSlim(2);
			Semaphore sp = new Semaphore(0, 5, "测试的");
			
		}

		[TestMethod]
		public void TestTimer()
		{
			System.Threading.Timer timer = new System.Threading.Timer(ConsoleSomthing, "你好啊", 1000, 5000);
			System.Timers.Timer t1 = new System.Timers.Timer(3000);
			t1.AutoReset = true;
			t1.Elapsed += TimerTask;
		}

		private void TimerTask(object sender, ElapsedEventArgs e)
		{

		}
		private void ConsoleSomthing(object obj)
		{
			Console.WriteLine(obj + ":" + DateTime.Now.ToString());
		}

		private void TaskDo(Mutex sps)
		{
			bool isComplete = false;
			while(!isComplete)
			{
				if(sps.WaitOne(600))
				{
					try
					{
						Console.WriteLine("lock thread {0}", Thread.CurrentThread.ManagedThreadId);
						Thread.Sleep(2000);
					}
					finally
					{
						sps.ReleaseMutex();
						Console.WriteLine("release thread {0}", Thread.CurrentThread.ManagedThreadId);
						isComplete = true;
					}
				}
				else
				{
					Console.WriteLine("timeout thread {0}", Thread.CurrentThread.ManagedThreadId);
				}
			}
		}

		private void DoLoop()
		{
			for (int i = 0; i < 500000; i++)
			{
				lock(obj)
				{
					State += 1; 
				}	
			}
			//Thread.Sleep(1000);
		}
	}

	public class Demo
	{
		private class DemoSync:Demo
		{
			private static object obj = new object();
			private Demo d;

			public DemoSync(Demo d)
			{
				this.d = d;
			}

			public override bool IsSynchronized{ get{ return true; } }
			public override void DoThis()
			{
				lock(obj)
				{
					d.DoThis();
				}
			}
			public override void DoThat()
			{
				lock (obj)
				{
					d.DoThat();
				}
			}
		}

		public virtual bool IsSynchronized{ get{ return false; } }

		public static Demo Synchronized(Demo d)
		{
			if (!d.IsSynchronized)
				return new DemoSync(d);
			return d;
		}

		public virtual void DoThis(){ }
		public virtual  void DoThat(){ }
	}
}
