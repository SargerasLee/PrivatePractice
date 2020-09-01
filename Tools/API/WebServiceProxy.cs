using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Services.Description;
using System.Xml.Serialization;

namespace Tools.API
{
	public class WebServiceProxy
	{
		/// <summary>
		///  生成dll保存到本地
		/// </summary>
		/// <param name="url">service地址</param>
		/// <param name="className">类全限定名</param>
		/// <param name="methodName">方法名</param>
		/// <param name="filePath">本地dll保存路径</param>
		public void GenerateLocalDll(string url,string className,string methodName,string filePath)
		{
			WebClient webClient = new WebClient();
			if(!url.Contains("?WSDL"))
			{
				url += "?WSDL";
			}
			//从输入的url页面处打开流
			Stream stream = webClient.OpenRead(url);
			//创建wsdl描述类
			ServiceDescription description = ServiceDescription.Read(stream);
			if(!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}
			//判断缓存是否过期,过期删掉，否则不处理
			DeleteDllIfCaheExpired(filePath,className,methodName);
			GenerateLocalDll(url, className, methodName, filePath);
		}

		/// <summary>
		///  判断缓存是否过期,过期删掉，否则不处理
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="className"></param>
		/// <param name="methodName"></param>
		private void DeleteDllIfCaheExpired(string filePath, string className, string methodName)
		{
			string name = filePath + className + "_" + methodName;
			if(File.Exists(name+".dll"))
			{
				var cache = HttpRuntime.Cache.Get(className + "_" + methodName);
				if(cache==null)
				{
					File.Delete(name + ".dll");
				}
			}
		}

		/// <summary>
		/// 创建本地代理类
		/// </summary>
		public void CreateLocalProxy(ServiceDescription description, string dllName)
		{
			ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
			importer.ProtocolName = "Soap";
			importer.Style = ServiceDescriptionImportStyle.Client;
			importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;
			importer.AddServiceDescription(description, null, null);

			CodeNamespace codeNamespace = new CodeNamespace();
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			codeCompileUnit.Namespaces.Add(codeNamespace);

			ServiceDescriptionImportWarnings warnings = importer.Import(codeNamespace, codeCompileUnit);
			CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
			CompilerParameters parameters = new CompilerParameters();
			
			parameters.GenerateExecutable = false;
			parameters.OutputAssembly = dllName + ".dll";
			parameters.ReferencedAssemblies.Add("System.dll");
			parameters.ReferencedAssemblies.Add("System.Xml.dll");
			parameters.ReferencedAssemblies.Add("System.Web.Services.dll");
			parameters.ReferencedAssemblies.Add("System.Data.dll");

			CompilerResults results = provider.CompileAssemblyFromDom(parameters, codeCompileUnit);
			if(results.Errors.HasErrors)
			{
				StringBuilder sb = new StringBuilder();
				foreach(CompilerError error in results.Errors)
				{
					sb.Append(error.ToString());
					sb.Append(Environment.NewLine);
				}
				throw new Exception(sb.ToString());
			}
			var objCache = HttpRuntime.Cache;
			objCache.Insert("", "1", null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, CacheItemPriority.High, null);
		}
	}
}
