using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
	}
}
