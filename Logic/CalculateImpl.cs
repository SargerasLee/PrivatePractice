/************************************************************************
*Copyright  (c)   2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*命名空间    ：Logic
*文件名称    ：CalculateImpl.cs
*版本号        :   2020|V1.0.0.0 
*=================================
*创 建 者      ：@ lichanghao01
*创建日期    ：2020/5/3 17:05:41 
*功能描述    ：
*使用说明    ：
*=================================
*修改日期    ：2020/5/3 17:05:41 
*修改者        ：lichanghao01
*修改描述    ：
*版本号        :   2020|V1.0.0.0 
***********************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic
{
	public class CalculateImpl:ICalculate
	{
		/// <summary>
		/// 计算方法
		/// </summary>
		/// <param name="expression">计算表达式</param>
		/// <returns></returns>
		public string Calc(string expression)
		{
			if(string.IsNullOrWhiteSpace(expression))
				throw new Exception("表达式为空");
			string s = OperationUtil.ReplaceOperatorSymbol(expression);
			s = OperationUtil.RemoveWhiteSpace(s);
			//转为中缀表达式list
			List<string> infixExpression = ExpressionUtil.ToInfixExpression(s);
			//转为后缀表达式list
			List<string> suffixExpression = ExpressionUtil.ToSuffixExpression(infixExpression);
			Stack<string> stack = new Stack<string>();
			foreach (var item in suffixExpression)
			{
				//如果是数，则压入栈中
				if (Regex.IsMatch(item, "\\d+[.]?\\d*"))//匹配小数
				{
					stack.Push(item);
				}
				//若是运算符，依次弹出栈顶的两个数进行运算，再压入栈中
				else
				{
					string s1 = stack.Pop();
					string s2 = stack.Pop();
					string result = Compute(s2, s1, item);
					stack.Push(result);
				}
			}
			
			var reStr = stack.Pop();
			var round = Math.Round(decimal.Parse(reStr), 2);
			return round.ToString();
		}

		/// <summary>
		/// 两个操作数运算
		/// </summary>
		/// <param name="n1">数 1</param>
		/// <param name="n2">数 2</param>
		/// <param name="op">运算符</param>
		/// <returns></returns>
		private string Compute(string n1,string n2,string op)
		{
			decimal d1 = decimal.Parse(n1);
			decimal d2 = decimal.Parse(n2);
			decimal result = 0;
			switch (op)
			{
				case "+": result = d1 + d2; return result.ToString();
				case "-": result = d1 - d2; return result.ToString();
				case "*": result = d1 * d2; return result.ToString();
				case "/": result = d1 / d2; return result.ToString();
				default:throw new Exception("运算符"+op+"无效");
			}
		}
	}
}
