using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Logic.Calc
{
    public static class ExpressionUtil
    {
		/// <summary>
		/// 字符串转中缀表达式
		/// </summary>
		/// <param name="expression">字符串表达式</param>
		/// <returns></returns>
	    public static List<string> ToInfixExpression(string expression)
	    {
			var list=new List<string>(50);
			var concatStr = string.Empty;
			//char c;
			int count = 0;
			while (count < expression.Length)
			{
				if ((expression[count] < 48 || expression[count] > 57) && expression[count]!=46)
				{
					list.Add(expression[count].ToString());
					count++;
				}
				else
				{
					concatStr = string.Empty;
					while (count<expression.Length && (expression[count]>=48 && expression[count]<=57 || expression[count]==46))
					{
						concatStr += expression[count];
						count++;
					}
					list.Add(concatStr);
				}
			}
			return list;
	    }

		/// <summary>
		/// 中缀表达式转后缀表达式
		/// </summary>
		/// <param name="infix">中缀表达式列表</param>
		/// <returns></returns>
		public static List<string> ToSuffixExpression(List<string> infix)
		{
			//1定义两个栈，运算符栈s1和 存储中间结果的栈s2
			Stack<string> s1=new Stack<string>();
			Stack<string> s2=new Stack<string>();
			//2从左到右扫描中缀表达式
			//重复2-5，直到结束
			foreach (string item in infix)
			{
				//3遇到操作数，将其压入s2
				if (Regex.IsMatch(item, "\\d+[.]?\\d*"))//小数，整数等匹配
				{
					s2.Push(item);	
				}
				//5遇到括号
				else if (item == "(" || item == ")")
				{
					//如果是(，直接压入s1
					if (item == "(")
					{
						s1.Push(item);
					}
					//如果是 )，则依次弹出s1栈顶，压入s2，直到遇到 (，此时丢弃这对括号
					else if (item == ")")
					{
						while (s1.Peek() != "(")
						{
							s2.Push(s1.Pop());
						}
						s1.Pop();
					}
				}
				//4遇到运算符，比较与s1栈顶运算符优先级
				else
				{
					//4.1如果s1为空，或者其栈顶元素为 (，则直接压入s1，若比s1栈顶优先级高，也压入s1
					if (s1.Count == 0 || s1.Peek() == "(" || OperationUtil.GetPriority(item) > OperationUtil.GetPriority(s1.Peek()))
					{
						s1.Push(item);
						continue;
					}
					//4.1若s1不为空，且s比s1栈顶优先级低，将s1栈顶弹出压入s2，再与s1栈顶比较
					while (s1.Count != 0 && OperationUtil.GetPriority(item) <= OperationUtil.GetPriority(s1.Peek()))
					{
						s2.Push(s1.Pop());
					}
					s1.Push(item);
				}
			}
			//7将s1剩余运算符依次弹出并压入s2
			while (s1.Count != 0)
			{
				s2.Push(s1.Pop());
			}
			//依次弹出s2，结果的逆序即为    后缀表达式
			List<string> suffix = s2.ToList();
			suffix.Reverse();
			return suffix;
		}
    }
}
