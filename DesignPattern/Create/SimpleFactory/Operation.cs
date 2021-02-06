using System;

namespace DesignPattern.Create.SimpleFactory
{
	public class Operation
	{
		public double NumA { set; get; }
		public double NumB { set; get; }

		protected double Result;
		public Operation()
		{

		}
		public Operation(double num1,double num2)
		{
			this.NumA = num1;
			this.NumB = num2;
		}
		public virtual double GetResult()
		{
			Result = 0;
			return Result;
		}
	}

	class OpAdd:Operation
	{
		public override double GetResult()
		{
			Result = NumA + NumB;
			return Result;
		}
	}
	class OpSub : Operation
	{
		public override double GetResult()
		{
			Result = NumA - NumB;
			return Result;
		}
	}
	class OpMul : Operation
	{
		public override double GetResult()
		{
			Result = NumA * NumB;
			return Result;
		}
	}
	class OpDiv : Operation
	{
		public override double GetResult()
		{
			if (NumB==0)
			{
				throw new ApplicationException("除数不能为0");
			}
			Result = NumA / NumB;
			return Result;
		}
	}
}
