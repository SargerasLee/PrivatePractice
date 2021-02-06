using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Web;
using System.Net.Mail;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class WebTest
	{
		[TestMethod]
		public void TestHttpClient()
		{
			//GetData();
			//Thread.Sleep(3000);
			TestExpl();
		}

		[TestMethod]
		public void TestSmtp()
		{
			try
			{
				//ygzcjelbmgwjgaig
				SmtpClient client = new SmtpClient("smtp.qq.com");
				client.UseDefaultCredentials = false;
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.EnableSsl = true;
				client.Credentials = new NetworkCredential("1328008364@qq.com", "ygzcjelbmgwjgaig");
				client.Send("1328008364@qq.com", "2682856778@qq.com", "主体1", "自动任务，勿回");

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
			}
		}

		[TestMethod]
		public void TestSocket()
		{

		}

		private async void GetData()
		{
			HttpClient client = new HttpClient();
			HttpResponseMessage msg =await client.GetAsync("https://www.baidu.com");
			if(msg.IsSuccessStatusCode)
			{
				Console.WriteLine(msg.StatusCode);
				Console.WriteLine(msg.Content.ReadAsStringAsync().Result);
			}
			
		}

		private void TestExpl()
		{
			Process proc = new Process();
			proc.StartInfo.FileName = "chrome.exe";
			proc.StartInfo.Arguments = "https://www.baidu.com";
			proc.Start();
		}

		private void WebReq()
		{
			WebRequest.Create("");
			Uri u = new Uri("https://www.baidu.com");
		}
	}
}
