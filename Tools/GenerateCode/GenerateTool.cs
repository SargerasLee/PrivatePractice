using System.Data;
using System.Text;

namespace Tools.GenerateCode
{
	public class GenerateTool
	{
		public string GenerateEntityCode(string tableName,string className)
		{
			StringBuilder sb = new StringBuilder(1000);
			sb.Append($"public class {className}");
			sb.Append("{");
			string colInfo = $"select COLUMN_NAME,DATA_TYPE,DATA_LENGTH,DATA_PRECISION,DATA_SCALE,NULLABLE from user_tab_columns where table_name='{tableName.ToUpper()}'";
			DataSet ds = new DataSet();
			foreach(DataRow row in ds.Tables[0].Rows)
			{
				
			}
			sb.Append("}");
			return sb.ToString();
		}
	}
}
