using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebDataExchange.Import
{
	/// <summary>
	/// ExcelExportHandler 的摘要说明
	/// </summary>
	public class ExcelExportHandler : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			context.Response.Write("Hello World");
			IWorkbook workbook = CreateWorkbook("xls");
			
			FillWorkbook(workbook, null,null);
			workbook.Write(context.Response.OutputStream);
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public IWorkbook CreateWorkbook(string type)
		{
			switch(type)
			{
				case "xls":return new HSSFWorkbook();
				case "xlsx":return new XSSFWorkbook();
				default:throw new ArgumentException("传入的工作表类型不正确");
			}
		}

		public void FillWorkbook(IWorkbook workbook,DataSet dataSet,DataSet configSet)
		{
			if(dataSet==null || dataSet.Tables.Count<=0)
			{
				workbook.CreateSheet("sheet1");
				return;
			}
			int sheetCount = dataSet.Tables.Count;
			string[] sheetNames = configSet.Tables[0].Rows[0]["SheetNames"].ToString().Split(';');
			string[] querySqls= configSet.Tables[0].Rows[0]["QuerySqls"].ToString().Split(';');
			for (int i=0;i<sheetCount;i++)
			{
				ISheet sheet = workbook.CreateSheet(sheetNames[i]);
				SetSheetHeader(sheet, configSet.Tables[1]);
				FillSheet(sheet, dataSet.Tables[i]);
			}
			
		}

		private void FillSheet(ISheet sheet, DataTable dataTable)
		{
			
		}

		public void SetSheetHeader(ISheet sheet,DataTable table)
		{
			IRow header = sheet.CreateRow(0);
			for(int i=0;i<table.Rows.Count;i++)
			{
				header.CreateCell(i);
			}
		}
	}
}