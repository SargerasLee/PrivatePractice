using Logic.Calc;
using System;
using System.Windows.Forms;

namespace Startup
{
	public partial class Form1 : Form
	{
		private ICalculate calculator=new CalculateImpl();
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var inputText = Input.Text;
			string outValue = string.Empty;
			try
			{
				outValue = calculator.Calc(inputText);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,"警告",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			Output.Text = outValue;
		}

		private void ClearBtn_Click(object sender, EventArgs e)
		{
			Input.Text = string.Empty;
			Output.Text = string.Empty;
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar==(char)13)
			{
				webBrowser1.Navigate(textBox1.Text);
			}
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			WebBrowser browser = new WebBrowser();
			browser.Navigate("https://www.taobao.com", true);
		}

		private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
		{
			this.Text = webBrowser1.DocumentTitle;
			textBox1.Text = webBrowser1.Url.ToString();
		}
	}
}
