namespace Startup
{
	partial class Form1
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.Input = new System.Windows.Forms.TextBox();
			this.Output = new System.Windows.Forms.TextBox();
			this.CalcBtn = new System.Windows.Forms.Button();
			this.ClearBtn = new System.Windows.Forms.Button();
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// Input
			// 
			this.Input.Location = new System.Drawing.Point(557, 95);
			this.Input.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Input.Multiline = true;
			this.Input.Name = "Input";
			this.Input.Size = new System.Drawing.Size(198, 24);
			this.Input.TabIndex = 0;
			// 
			// Output
			// 
			this.Output.Location = new System.Drawing.Point(557, 154);
			this.Output.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Output.Name = "Output";
			this.Output.ReadOnly = true;
			this.Output.Size = new System.Drawing.Size(198, 25);
			this.Output.TabIndex = 1;
			// 
			// CalcBtn
			// 
			this.CalcBtn.Location = new System.Drawing.Point(772, 89);
			this.CalcBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.CalcBtn.Name = "CalcBtn";
			this.CalcBtn.Size = new System.Drawing.Size(85, 32);
			this.CalcBtn.TabIndex = 2;
			this.CalcBtn.Text = "计算\r\n";
			this.CalcBtn.UseVisualStyleBackColor = true;
			this.CalcBtn.Click += new System.EventHandler(this.button1_Click);
			// 
			// ClearBtn
			// 
			this.ClearBtn.Location = new System.Drawing.Point(772, 154);
			this.ClearBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.ClearBtn.Name = "ClearBtn";
			this.ClearBtn.Size = new System.Drawing.Size(85, 27);
			this.ClearBtn.TabIndex = 3;
			this.ClearBtn.Text = "清空";
			this.ClearBtn.UseVisualStyleBackColor = true;
			this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
			// 
			// webBrowser1
			// 
			this.webBrowser1.Location = new System.Drawing.Point(12, 65);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new System.Drawing.Size(526, 480);
			this.webBrowser1.TabIndex = 4;
			this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(12, 28);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(526, 25);
			this.textBox1.TabIndex = 5;
			this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(730, 38);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(67, 15);
			this.linkLabel1.TabIndex = 6;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "加入我们";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(893, 557);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.webBrowser1);
			this.Controls.Add(this.ClearBtn);
			this.Controls.Add(this.CalcBtn);
			this.Controls.Add(this.Output);
			this.Controls.Add(this.Input);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "Form1";
			this.Text = "计算器";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox Input;
		private System.Windows.Forms.TextBox Output;
		private System.Windows.Forms.Button CalcBtn;
		private System.Windows.Forms.Button ClearBtn;
		private System.Windows.Forms.WebBrowser webBrowser1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.LinkLabel linkLabel1;
	}
}

