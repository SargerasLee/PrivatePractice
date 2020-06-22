using System;

namespace DesignPattern.FactoryMethod
{
	public class OperationFactory
	{
		public virtual Operation CreateInstance()
		{
			return new Operation();
		}
	}

	public class OpAddFactory:OperationFactory
	{
		public override Operation CreateInstance()
		{
			return new OpAdd();
		}
	}

	public class OpSubFactory : OperationFactory
	{
		public override Operation CreateInstance()
		{
			return new OpSub();
		}
	}

	public class OpMulFactory : OperationFactory
	{
		public override Operation CreateInstance()
		{
			return new OpMul();
		}
	}

	public class OpDivFactory : OperationFactory
	{
		public override Operation CreateInstance()
		{
			return new OpDiv();
		}
	}
}
