using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Util;
using System.Threading;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace UnitTest.excel
{
	[TestClass]
	public class ExcelTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			HSSFWorkbook workbook = new HSSFWorkbook();
			
			DocumentSummaryInformation documentSummaryInformation = PropertySetFactory.CreateDocumentSummaryInformation();
			documentSummaryInformation.Company = "lch";
			SummaryInformation summaryInformation = PropertySetFactory.CreateSummaryInformation();
			summaryInformation.Title = "excel";
			summaryInformation.Author = "lch";
			summaryInformation.Comments = "export";
			summaryInformation.Subject = "lll";
			workbook.DocumentSummaryInformation = documentSummaryInformation;
			workbook.SummaryInformation = summaryInformation;
			HSSFSheet sheet = workbook.CreateSheet("sheet1") as HSSFSheet;
			HSSFRow row = sheet.CreateRow(0) as HSSFRow;
			HSSFCell cell0 = row.CreateCell(0) as HSSFCell;
			HSSFCell cell1 = row.CreateCell(1) as HSSFCell;
			HSSFCell cell2 = row.CreateCell(2) as HSSFCell;
			HSSFCell cell3 = row.CreateCell(3) as HSSFCell;
			HSSFCell cell4 = row.CreateCell(4) as HSSFCell;
			HSSFCell cell5 = row.CreateCell(5) as HSSFCell;
			foreach(string format in HSSFDataFormat.GetBuiltinFormats())
			{
				Console.WriteLine(format);
			}
			cell0.SetCellValue(new DateTime(2021, 3, 9));
			HSSFCellStyle cellStyle0 = workbook.CreateCellStyle() as HSSFCellStyle;
			HSSFDataFormat format0 = workbook.CreateDataFormat() as HSSFDataFormat;
			cellStyle0.DataFormat = format0.GetFormat("yyyy年m月d日");
			cell0.CellStyle = cellStyle0;

			cell1.SetCellValue(12.0);
			HSSFCellStyle cellStyle1 = workbook.CreateCellStyle() as HSSFCellStyle;
			//HSSFDataFormat format1 = workbook.CreateDataFormat() as HSSFDataFormat;
			cellStyle1.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
			cell1.CellStyle = cellStyle1;

			cell2.SetCellValue(3000.3);
			HSSFCellStyle cellStyle2 = workbook.CreateCellStyle() as HSSFCellStyle;
			HSSFDataFormat format2 = workbook.CreateDataFormat() as HSSFDataFormat;
			cellStyle2.DataFormat = format2.GetFormat("￥#,##0");
			cell2.CellStyle = cellStyle2;

			cell3.SetCellValue(45);
			HSSFCellStyle cellStyle3 = workbook.CreateCellStyle() as HSSFCellStyle;
			//HSSFDataFormat format3 = workbook.CreateDataFormat() as HSSFDataFormat;
			cellStyle3.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");
			cell3.CellStyle = cellStyle3;

			cell4.SetCellValue(12345.67);
			HSSFCellStyle cellStyle4 = workbook.CreateCellStyle() as HSSFCellStyle;
			HSSFDataFormat format4 = workbook.CreateDataFormat() as HSSFDataFormat;
			cellStyle4.DataFormat = format4.GetFormat("[DbNum2][$-804]0");
			cell4.CellStyle = cellStyle4;

			cell5.SetCellValue(123456789);
			HSSFCellStyle cellStyle5 = workbook.CreateCellStyle() as HSSFCellStyle;
			//HSSFDataFormat format5 = workbook.CreateDataFormat() as HSSFDataFormat;
			cellStyle5.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00E+00");
			cell5.CellStyle = cellStyle5;



			FileStream fileStream = new FileStream(@"C:\Users\lichanghao\Desktop\xxx.xls", FileMode.Create);
			workbook.Write(fileStream);
			fileStream.Close();
		}

		[TestMethod]
		public void TestMergeCell()
		{
			HSSFWorkbook workbook = new HSSFWorkbook();
			HSSFSheet sheet = workbook.CreateSheet("sheet1") as HSSFSheet;
			HSSFRow row = sheet.CreateRow(0) as HSSFRow;
			HSSFCell cell = row.CreateCell(0) as HSSFCell;
			HSSFCell cell1 = row.CreateCell(5) as HSSFCell;
			HSSFCellStyle style = workbook.CreateCellStyle() as HSSFCellStyle;
			style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
			style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
			HSSFFont font = workbook.CreateFont() as HSSFFont;
			font.FontHeightInPoints = 18;
			font.FontName = "宋体";
			font.Color = HSSFColor.Red.Index; 
			style.SetFont(font);
			style.FillBackgroundColor = HSSFColor.Blue.Index;
			style.FillForegroundColor = HSSFColor.White.Index;
			style.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
			//style.IsLocked = true;
			cell.CellStyle = style;
			cell.SetCellValue("我尼玛？");
			cell1.SetCellValue(Convert.ToDouble(4.3m));
			//Region region = new Region(0, 0, 0, 4);
			CellRangeAddress range = new CellRangeAddress(0, 0, 0, 4);
			sheet.AddMergedRegion(range);
			//高度 height 1/20个点 *20，宽度 *256
			sheet.SetColumnWidth(0, cell.StringCellValue.Length*256);
			//sheet.ProtectSheet("aaaaaa");
			CellRangeAddressList regions = new CellRangeAddressList(2, 65535, 0, 0);
			DVConstraint constraint = DVConstraint.CreateExplicitListConstraint(new string[] { "itemA", "itemB", "itemC" });
			HSSFDataValidation dataValidate = new HSSFDataValidation(regions, constraint);
			sheet.AddValidationData(dataValidate);
			//sheet1.ForceFormulaRecalculation=true;
			FileStream fileStream = new FileStream(@"C:\Users\lichanghao\Desktop\yyy.xls", FileMode.Create);
			workbook.Write(fileStream);
			fileStream.Close();
			ThreadPool.QueueUserWorkItem((state) => { });
		}


		[TestMethod]
		public void TestXSSF()
		{
			XSSFWorkbook workbook = new XSSFWorkbook();
			ISheet sheet = workbook.CreateSheet("dd");
		}
	}
}
