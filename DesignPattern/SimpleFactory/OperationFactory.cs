using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.SimpleFactory
{
	public class OperationFactory
	{
		public static Operation CreateInstance(string operate)
		{
			//Operation op = null;
			switch(operate)
			{
				case "+": return new OpAdd();
				case "-": return new OpSub();
				case "*": return new OpMul();
				case "/": return new OpDiv();
				default:throw new Exception("类型不存在");
			}
		}
	}
}
