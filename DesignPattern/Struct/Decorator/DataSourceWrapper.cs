using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Struct.Decorator
{
	public interface IDataSource
	{
		void WriteData(byte[] data);
		byte[] ReadData();
	}

	public class FileDataSource : IDataSource
	{
		public byte[] ReadData()
		{
			byte[] buf = new byte[1024];
			Console.WriteLine("读出数据");
			return buf;
		}

		public void WriteData(byte[] data)
		{
			Console.WriteLine("写入数据");
		}
	}

	public class DataSourceWrapper : IDataSource
	{
		private IDataSource dataSource;

		public DataSourceWrapper(IDataSource dataSource)
		{
			this.dataSource = dataSource;
		}

		public virtual byte[] ReadData()
		{
			return dataSource.ReadData();
		}

		public virtual void WriteData(byte[] data)
		{
			dataSource.WriteData(data);
		}
	}

	public class CompressionWrapper : DataSourceWrapper
	{
		public CompressionWrapper(IDataSource dataSource) : base(dataSource){ }

		public override byte[] ReadData()
		{
			Console.WriteLine("读取前解压缩");
			return base.ReadData();
		}
		public override void WriteData(byte[] data)
		{
			Console.WriteLine("写入前压缩");
			base.WriteData(data);
		}
	}
}
