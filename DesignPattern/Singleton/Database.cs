

using System;
using System.Runtime.InteropServices;

namespace DesignPattern.Singleton
{
	//[StructLayout(LayoutKind.Sequential)]
	public class Database
	{
		private static Database database = null;
		private static readonly object obj = new object();//私有的静态对象当锁
		private Database(){ }

		public static Database GetInstance()
		{
			if(database==null)
			{
				lock (obj)//多线程模式下获取实例
				{
					if(database==null)
					{
						database = new Database();
					}
				}
				return database;
			}
			return database;
		}

		public void Update()
		{
			Console.WriteLine("update");
		}
		
	}

	public class MemoryUtil
	{
		/// <summary>
		/// 获取引用类型的内存地址方法 
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static string GetMemory(object o)
		{
			GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);
			IntPtr addr = GCHandle.ToIntPtr(h);
			return "0x" + addr.ToString("X");
		}
	}
}
