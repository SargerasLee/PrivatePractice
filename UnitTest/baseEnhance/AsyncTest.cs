using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class AsyncTest
	{
		[TestMethod]
		public void TestAsync()
		{
			CallerWithAsync();
		}

		private static string GetName(string name)
		{
			Thread.Sleep(3000);
			return string.Format("hello,{0}", name);
		}

		private static Task<string> Greeting(string name)
		{
			return Task.Run(() =>
			{
				return GetName(name);
			});
		}
		private static void A(string name)
		{
			Task<string> t1 = Task.Run(() => GetName(name));
			t1.ContinueWith(t => t.Result);

		}

		private async static void CallerWithAsync()
		{
			string h = await Greeting("zhangsan");
			string a = await Task.Run(() => GetName("zhangsss"));
			//Task.WhenAll()
			Console.WriteLine(h);
			Console.ReadLine();
		}

		private async static void WaitFull()
		{
			Task t1 = Greeting("zhangsan");
			Task t2 = Task.Run(() => GetName("sdsd"));
			await Task.WhenAll(t1, t2);
		}

		private void B()
		{
			//Task.Factory.FromAsync()
		}
	}
}
