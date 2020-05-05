/************************************************************************
*Copyright  (c)   2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*命名空间    ：Logic
*文件名称    ：OperationUtil.cs
*版本号        :   2020|V1.0.0.0 
*=================================
*创 建 者      ：@ lichanghao01
*创建日期    ：2020/5/3 18:51:30 
*功能描述    ：
*使用说明    ：
*=================================
*修改日期    ：2020/5/3 18:51:30 
*修改者        ：lichanghao01
*修改描述    ：
*版本号        :   2020|V1.0.0.0 
***********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Calc
{
	public static class OperationUtil
	{
		private const int Add = 1;
		private const int Sub = 1;
		private const int Mul = 2;
		private const int Div = 2;
		private const int Sqrt = 3;

		public static int GetPriority(string op)
		{
			switch (op)
			{
				case "+": return Add;
				case "-": return Sub;
				case "/": return Div;
				case "*": return Mul;
				default: throw new Exception("运算符"+op+"不存在");
			}
		}

		/// <summary>
		/// 将表达式中×÷替换为*/
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static string ReplaceOperatorSymbol(string expression)
		{
			if (!(expression.Contains("×") || expression.Contains("÷")))
			{
				return expression;
			}

			var s = expression.Replace("×", "*").Replace("÷", "/");
			return s;
		}

		/// <summary>
		/// 去除空格
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static string RemoveWhiteSpace(string expression)
		{
			if (!expression.Contains(" "))
			{
				return expression;
			}

			var s = expression.Replace(" ", "");
			return s;
		}
	}
}
