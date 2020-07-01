using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.AbstractFactory
{
	/// <summary>
	/// 抽象工厂
	/// </summary>
	public interface IDatabaseFactory
	{
		IUser CreateUserDatabase();
	}

	public class OracleFactory : IDatabaseFactory
	{
		public IUser CreateUserDatabase()
		{
			return new OracleUser();
		}
	}

	public class MysqlFactory : IDatabaseFactory
	{
		public IUser CreateUserDatabase()
		{
			return new MySqlUser();
		}
	}




	/// <summary>
	/// 抽象产品
	/// </summary>
	public interface IUser
	{
		void Insert(User user);
		User FindUser(int id);
	}

	public class OracleUser : IUser
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

	public class MySqlUser : IUser
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
