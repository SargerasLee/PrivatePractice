using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class ReflectorTest
	{
		[TestMethod]
		public void TestAttribute()
		{
			try
			{
				try
				{
				}
				catch(IOException e){ }
				int a = 7;
			}
			catch(Exception e){ }
		}

		[TestMethod]
		public void ShowLog()
		{
			Eat();
		}
		

		[MyAttibute("lol",Comment ="wewe")]
		private void Eat()
		{
			Type t = typeof(GR);
			string type = "UnitTest.baseEnhance.GR";
			GR gr = Activator.CreateInstance(Type.GetType(type)) as GR;
			gr.Log();
			MethodInfo[] mi = t.GetMethods(BindingFlags.Public);
			//ParameterInfo[] pi = mi[0].GetParameters();
			//Assembly.Load("");
			//Assembly.LoadFrom("");
			//Assembly.LoadFile("");
			//mi[0].GetCustomAttributes(typeof(GR),false);
			//Activator
		}
	}

	public class GR
	{
		public void Log([CallerLineNumber] int line = -1, [CallerFilePath] string path = null, [CallerMemberName] string name = null)
		{
			Console.WriteLine(line);
			Console.WriteLine(path);
			Console.WriteLine(name);
		}
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple =false,Inherited =false)]
	public class MyAttibute : Attribute
	{
		private string name;

		public string Comment{ get; set; }
		public MyAttibute(string name)
		{
			this.name = name;
		}
	}
}
