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
	public class WorkbookExportHandler : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "application/octet-stream";
			context.Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpUtility.UrlEncode(name));
			context.Response.Write("Hello World");
			WorkbookExporter exporter = new WorkbookExporter();
			exporter.Export(context.Response.OutputStream);
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

	}
}