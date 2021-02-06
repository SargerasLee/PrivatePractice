using System;
using System.Collections.Generic;
using DesignPattern.Behavior.TemplateMethod;
using DesignPattern.Create.AbstractFactory;
using DesignPattern.Create.FactoryMethod;
using DesignPattern.Create.Singleton;
using DesignPattern.Struct.Composite;
using DesignPattern.Struct.Decorator;
using DesignPattern.Struct.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperationFM = DesignPattern.Create.FactoryMethod.Operation;
using OperationSF = DesignPattern.Create.SimpleFactory.Operation;

namespace UnitTest
{
	[TestClass]
	public class TestDesignPattern
	{
		[TestMethod]
		public void TestSimpleFactory()
		{
			OperationSF op = DesignPattern.Create.SimpleFactory.OperationFactory.CreateInstance("+");
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
			Database db2 = Database.GetInstance();
			Database db3 = Database.GetInstance();
			db1.Update();
			Console.WriteLine(ReferenceEquals(db1, db2));
			Console.WriteLine(ReferenceEquals(db2, db3));
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

		[TestMethod]
		public void TestComposite()
		{
			ImageEditor editor = new ImageEditor();
			editor.Load();
			List<IGraphic> list = new List<IGraphic>();
			list.Add(new CompoundGraphic());
			list.Add(new Dot(1.0,2.0));
			list.Add(new Circle(5.0, 5.0, 5.0));
			editor.GroupSelected(list);
		}

		[TestMethod]
		public void TestWrapper()
		{
			CompressionWrapper wrapper = new CompressionWrapper(new FileDataSource());
			wrapper.ReadData();
			wrapper.WriteData(new byte[1024]);
		}
	}
}
