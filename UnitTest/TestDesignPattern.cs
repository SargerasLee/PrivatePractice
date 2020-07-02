using System;
using DesignPattern.AbstractFactory;
using DesignPattern.Facade;
using DesignPattern.FactoryMethod;
using DesignPattern.Singleton;
using DesignPattern.TemplateMethod;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperationFM = DesignPattern.FactoryMethod.Operation;
using OperationSF = DesignPattern.SimpleFactory.Operation;

namespace UnitTest
{
	[TestClass]
	public class TestDesignPattern
	{
		[TestMethod]
		public void TestSimpleFactory()
		{
			OperationSF op = DesignPattern.SimpleFactory.OperationFactory.CreateInstance("+");
			op.NumA = 2.2;
			op.NumB = 3.3;
			double res = op.GetResult();
			Console.WriteLine(res);
		}

		[TestMethod]
		public void TestFactoryMethod()
		{
			OperationFactory operationFactory = new OperationFactory();
			OperationFM operation = operationFactory.CreateInstance();
			operation.NumA = 5.5;
			operation.NumA = 3.2;
			Console.WriteLine(operation.GetResult());
			OpAddFactory opAddFactory = new OpAddFactory();
			OperationFM opAdd = opAddFactory.CreateInstance();
			opAdd.NumA = 5.5;
			opAdd.NumB = 3.2;
			Console.WriteLine(opAdd.GetResult());
		}

		[TestMethod]
		public void TestSingleton()
		{
			Database db1 = Database.GetInstance();
			db1.Update();
			Console.WriteLine(MemoryUtil.GetMemory(Database.GetInstance()));
			Console.WriteLine(MemoryUtil.GetMemory(Database.GetInstance()));
			Console.WriteLine(MemoryUtil.GetMemory(Database.GetInstance()));
		}

		[TestMethod]
		public void TestFacade()
		{
			Facade face = new Facade();
			face.GroupFuncA();
			face.GroupFuncB();
			face.GroupFuncC();
		}

		[TestMethod]
		public void TestTemplateMethod()
		{
			GameAI zerg = new ZergAI();
			GameAI tyron = new TyronAI();
			zerg.TakeTurn();
			tyron.TakeTurn();
		}

		[TestMethod]
		public void TestAbstractFactory()
		{
			IDatabaseFactory factory = new OracleFactory();
			IUserDB userdao = factory.CreateUserDatabase();
			Console.WriteLine(userdao);
		}
	}
}
