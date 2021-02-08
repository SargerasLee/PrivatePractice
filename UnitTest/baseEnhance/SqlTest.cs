using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.EnterpriseServices;

namespace UnitTest.baseEnhance
{
	[TestClass]
	public class SqlTest
	{
		[TestMethod]
		public async Task TestMethod1Async()
		{
			MyTransaction  my= new MyTransaction();
			await my.TestSql();
		}
		[TestMethod]
		public async Task TestTransactionc()
		{
			TransactionInformation info = Transaction.Current.TransactionInformation;
		}

		[TestMethod]
		public void TestScopeTran()
		{
			using(var scope=new TransactionScope())
			{
				Transaction.Current.TransactionCompleted += CurCompleted;
				scope.Complete();
				
			}
		}

		private void CurCompleted(object sender, TransactionEventArgs e)
		{
			Console.WriteLine(e.Transaction.TransactionInformation.LocalIdentifier);
		}
	}

	[Transaction(TransactionOption.Required)]
	class MyTransaction:ServicedComponent
	{
		[AutoComplete]
		public async Task TestSql()
		{
			SqlTransaction transaction = null;
			try
			{
				SqlConnection connection = new SqlConnection("");
				await connection.OpenAsync();
				transaction = connection.BeginTransaction();
				SqlCommand sql = connection.CreateCommand();
				sql.Transaction = transaction;
				sql.ExecuteNonQuery();
				transaction.Commit();
			}
			catch (Exception ex)
			{
				transaction.Rollback();
			}
		}
	}

	class MyResourceManager : IEnlistmentNotification
	{
		public void Commit(Enlistment enlistment)
		{
			throw new NotImplementedException();
		}

		public void InDoubt(Enlistment enlistment)
		{
			throw new NotImplementedException();
		}

		public void Prepare(PreparingEnlistment preparingEnlistment)
		{
			throw new NotImplementedException();
		}

		public void Rollback(Enlistment enlistment)
		{
			throw new NotImplementedException();
		}
	}
}
