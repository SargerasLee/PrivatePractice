using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPattern.AbstractFactory
{
	/// <summary>
	/// 抽象数据库工厂
	/// </summary>
	public interface IDatabaseFactory
	{
		IUserDB CreateUserDatabase();
	}

	public class OracleFactory : IDatabaseFactory
	{
		public IUserDB CreateUserDatabase()
		{
			string s = "DesignPattern.AbstractFactory.OracleUser";
			Type t = Type.GetType(s);
			return (IUserDB)Activator.CreateInstance(t);
			//return new OracleUser();
			
		}
	}

	public class MysqlFactory : IDatabaseFactory
	{
		public IUserDB CreateUserDatabase()
		{
			string s = "DesignPattern.AbstractFactory.MySqlUser";
			Type t = Type.GetType(s);
			return (IUserDB)Activator.CreateInstance(t);
		}
	}




	/// <summary>
	/// 抽象数据库产品
	/// </summary>
	public interface IUserDB
	{
		void Insert(User user);
		User FindUser(int id);
	}

	public class OracleUserDB : IUserDB
	{
		public User FindUser(int id)
		{
			Console.WriteLine("oracle找到ID为{0}的用户", id);
			return null;
		}

		public void Insert(User user)
		{
			Console.WriteLine("oracle插入一条记录");
		}
	}

	public class MySqlUserDB : IUserDB
	{
		public User FindUser(int id)
		{
			Console.WriteLine("mysql找到ID为{0}的用户", id);
			return null;
		}

		public void Insert(User user)
		{
			Console.WriteLine("mysql插入一条记录");
		}
	}
}
