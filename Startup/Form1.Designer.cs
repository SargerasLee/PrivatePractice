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
			this.SuspendLayout();
			// 
			// Input
			// 
			this.Input.Location = new System.Drawing.Point(195, 114);
			this.Input.Multiline = true;
			this.Input.Name = "Input";
			this.Input.Size = new System.Drawing.Size(222, 28);
			this.Input.TabIndex = 0;
			// 
			// Output
			// 
			this.Output.Location = new System.Drawing.Point(195, 189);
			this.Output.Name = "Output";
			this.Output.ReadOnly = true;
			this.Output.Size = new System.Drawing.Size(222, 28);
			this.Output.TabIndex = 1;
			// 
			// CalcBtn
			// 
			this.CalcBtn.Location = new System.Drawing.Point(467, 114);
			this.CalcBtn.Name = "CalcBtn";
			this.CalcBtn.Size = new System.Drawing.Size(96, 39);
			this.CalcBtn.TabIndex = 2;
			this.CalcBtn.Text = "计算\r\n";
			this.CalcBtn.UseVisualStyleBackColor = true;
			this.CalcBtn.Click += new System.EventHandler(this.button1_Click);
			// 
			// ClearBtn
			// 
			this.ClearBtn.Location = new System.Drawing.Point(467, 189);
			this.ClearBtn.Name = "ClearBtn";
			this.ClearBtn.Size = new System.Drawing.Size(96, 33);
			this.ClearBtn.TabIndex = 3;
			this.ClearBtn.Text = "清空";
			this.ClearBtn.UseVisualStyleBackColor = true;
			this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.ClearBtn);
			this.Controls.Add(this.CalcBtn);
			this.Controls.Add(this.Output);
			this.Controls.Add(this.Input);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
	}
}

