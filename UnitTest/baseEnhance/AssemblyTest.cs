using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;
using Tools.Component;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class AssemblyTest
	{
		[TestMethod]
		public void TestDyAss()
		{
			string custom = "Console.WriteLine(\"hahahaaha\")";
			string rounding = @"using System;
										public class MyDyClass{
											public void MyMethod()
											{
												{0}
											}
										}";
			CSharpCodeProvider provider = new CSharpCodeProvider();//编译器
			
			CompilerParameters parameters = new CompilerParameters();//编译时需要的参数
			parameters.GenerateInMemory = true;//仅在内存中使用
			//parameters.OutputAssembly = "MyTestAssembly";
			string target = string.Format(rounding, custom);
			CompilerResults results = provider.CompileAssemblyFromSource(parameters, target);//获得编译结果
			if(results.Errors.HasErrors)
			{
				foreach(CompilerError error in results.Errors)
				{
					Console.WriteLine(error.Line+":"+error.ErrorText);
				}
			}
			else
			{
				var tempOut = Console.Out;//先保存原先的标准输出流
				StringWriter sw = new StringWriter();
				Console.SetOut(sw);//用字符流 替换掉原来的流，使的编译的 代码中的console输出到stringwriter里
				Type t = results.CompiledAssembly.GetType("MyDyClass");
				t.InvokeMember("MyMethod", System.Reflection.BindingFlags.Public, null, null, null);
				Console.SetOut(tempOut);
				string text = sw.ToString();//获取被调用的编译代码的函数的输出
			}
		}

		[TestMethod]
		public void TestDomain()
		{
			string name = AppDomain.CurrentDomain.FriendlyName;
			AppDomain.CurrentDomain.ExecuteAssembly("");
			AppDomain second = AppDomain.CreateDomain("newAppDomain");
			second.CreateInstance("xxx", "xxx.Class", true, System.Reflection.BindingFlags.CreateInstance, null, new object[] { 1, 2 }, null, null);
			//second.CreateInstanceAndUnwrap()
		}

		[TestMethod]
		public void TestMvc()
		{
			PublicComponent component = new PublicComponent();
			Console.WriteLine(component.MethodMapping("/robxdj/write/abcde/fghij?hh=1", "我你哥"));
			Console.WriteLine(component.MethodMapping("/robxdj/write/hello/app?id=1&name=海明威&lesson=c#", "我你哥"));
			Console.WriteLine(component.MethodMapping("/robxdj/write/bbq", "我你哥"));
			//Console.WriteLine(component.MethodMapping("/robxdj/write/aaraadtyvdga/2?hh=1", "我你哥"));
			
		}
	}

	public class MyDriver : MarshalByRefObject
	{
		
	}
}
