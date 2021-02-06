using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Struct.Facade
{
	public class Facade
	{
		private SubSystem1 s1;
		private SubSystem2 s2;
		private SubSystem3 s3;
		private SubSystem4 s4;
		private SubSystem5 s5;
		public Facade()
		{
			s1 = new SubSystem1();
			s2 = new SubSystem2();
			s3 = new SubSystem3();
			s4 = new SubSystem4();
			s5 = new SubSystem5();
		}

		public void GroupFuncA()
		{
			s1.Method1();
			s2.Method2();
			s3.Method3();
		}
		public void GroupFuncB()
		{
			s4.Method4();
			s5.Method5();
		}
		public void GroupFuncC()
		{
			s1.Method1();
			s2.Method2();
			s5.Method5();
		}
	}


	public class SubSystem1
	{
		public void Method1()
		{
			Console.WriteLine("系统1方法1");
		}
	}
	public class SubSystem2
	{
		public void Method2()
		{
			Console.WriteLine("系统2方法2");
		}
	}
	public class SubSystem3
	{
		public void Method3()
		{
			Console.WriteLine("系统3方法3");
		}
	}
	public class SubSystem4
	{
		public void Method4()
		{
			Console.WriteLine("系统4方法4");
		}
	}
	public class SubSystem5
	{
		public void Method5()
		{
			Console.WriteLine("系统5方法5");
		}
	}
}
