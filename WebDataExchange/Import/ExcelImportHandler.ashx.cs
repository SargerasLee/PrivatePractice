using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Tools.Log;

namespace Genersoft.WEICHAI.FSSC.WebData.Import
{
    /// <summary>
    /// ExcelImportHandler 的摘要说明
    /// </summary>
    public class ExcelImportHandler : IHttpHandler
    {
        private GeneralLogger logger;
        public ExcelImportHandler() : base()
        {
            logger = new LoggerFactory().GetInstance("importExcel");
        }
        public void ProcessRequest(HttpContext context)
        {
            HttpPostedFile file = context.Request.Files["excel"];
            string ifSave = context.Request.Form["ifSave"];//是否存数据库
            string compID = context.Request.Form["compID"];//扩展构件id
            string tableName = context.Request.Form["tableName"];//表名
            string beginRow = context.Request.Form["beginRow"];//起始行
            string endRow = context.Request.Form["endRow"];//结束行
            string exName = GetExtendName(file.FileName);//扩展名
            int length = file.ContentLength;//文件字节数
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            string obj = string.Empty;
            try
            {
                string path = SaveFile(file);
                DataSet ds = ResloveExcelToDataTable(path, tableName, beginRow, endRow);

                string dire = path.Substring(0, path.LastIndexOf("\\") + 1);
                Directory.Delete(dire, true);

                MergeIntoTale(ifSave, tableName);
                obj = JsonConvert.SerializeObject(new { status = "success", name = file.FileName, data = ds.Tables[0] });
                context.Response.Write(obj);
            }
            catch (Exception e)
            {
                obj = JsonConvert.SerializeObject(new { status = "fail", name = file.FileName, data = e.Message + e.StackTrace });
                context.Response.Write(obj);
            }
        }

        private void MergeIntoTale(string ifSave, string tableName)
        {
            if (ifSave == "0") return;
        }

        private static string GetExtendName(string fileName)
        {
            string[] strs = fileName.Split('.');
            string exName = strs[strs.Length - 1];//文件扩展名
            return exName;
        }

        private static string SaveFile(HttpPostedFile file)
        {
            string guid = Guid.NewGuid().ToString();
            string path = $"c:\\tempfile\\{guid}";//文件保存路径
            //string user = "Administrators";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + file.FileName;
            file.SaveAs(path);
            return path;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        #region 校验
 
        private string GetUrlParam(HttpContext context, string paraName)
        {
            if (context.Request[paraName] == null)
            {
                return "";
            }
            return context.Request[paraName].ToString();
        }

        #endregion


        private DataSet ResloveExcelToDataTable(string path, string tableName, string begin, string end)
        {
            IWorkbook wb;
            IFormulaEvaluator eva;
            FileStream stream = File.OpenRead(path);
            if (path.ToLower().IndexOf("xlsx") > 0)
            {
                wb = new XSSFWorkbook(stream);
                eva = new XSSFFormulaEvaluator(wb);
            }
            else if (path.ToLower().IndexOf("xls") > 0)
            {
                wb = new HSSFWorkbook(stream);
                eva = new HSSFFormulaEvaluator(wb);
            }
            else
            {
                throw new ArgumentException("文件格式错误");
            }

            ISheet sheet = tableName == null || string.IsNullOrWhiteSpace(tableName) ? wb.GetSheetAt(0) : wb.GetSheet(tableName);
            DataTable dt = new DataTable();
            int beginRowNum = begin == null || string.IsNullOrWhiteSpace(begin) ? sheet.FirstRowNum + 1 : Convert.ToInt32(begin) - 1; //开始行
            int endRowNum = end == null || string.IsNullOrWhiteSpace(end) ? sheet.LastRowNum : Convert.ToInt32(end) - 1;  //结束行
            logger.Log(beginRowNum + "~~~~~" + endRowNum);
            IRow row0 = sheet.GetRow(0);
            //logger.Log(sheet.SheetName);
            int beginColNum = row0.FirstCellNum;//开始列
            int endColNum = row0.LastCellNum;//结束列
            for (int i = beginColNum; i < endColNum + 1; i++)//第0行作为datatable列名
            {
                ICell c = row0.GetCell(i);
                if (c != null)
                {
                    string name = c.StringCellValue;
                    dt.Columns.Add(name);
                }
            }
            DataRow dr;
            IRow row;
            ICell cell;
            for (int j = beginRowNum; j < endRowNum + 1; j++)
            {
                row = sheet.GetRow(j);
                if (row == null || row.FirstCellNum < 0) continue;
                dr = dt.NewRow();
                for (int k = beginColNum; k < row.Cells.Count; k++)
                {
                    cell = row.GetCell(k);
                    if (cell != null)
                    {
                        //特殊类型 日期和数字都是numeric
                        if (cell.CellType == CellType.Numeric)
                        {
                            if (DateUtil.IsCellDateFormatted(cell))
                            {
                                dr[k] = cell.DateCellValue;
                                logger.Log(j + "&&" + k + dr[k]);
                            }
                            else
                            {
                                dr[k] = Convert.ToDecimal(cell.NumericCellValue);
                                logger.Log(j + "--" + k + dr[k]);
                            }
                        }
                        else if (cell.CellType == CellType.Blank)//空数据类型
                        {
                            dr[k] = "";
                            logger.Log(j + "++" + k);
                        }
                        else if (cell.CellType == CellType.Formula)//公式类型
                        {
                            dr[k] = eva.Evaluate(cell).StringValue;
                            logger.Log(j + "##" + k + dr[k]);
                        }
                        else if (cell.CellType == CellType.Error)//错误类型
                        {
                            dr[k] = cell.ErrorCellValue;
                            logger.Log(j + "%%" + k + dr[k]);
                        }
                        else //其他类型都按字符串类型来处理
                        {
                            dr[k] = cell.StringCellValue;
                            logger.Log(j + "**" + k + dr[k]);
                        }
                    }
                    else
                    {
                        dr[k] = "";
                    }
                }
                dt.Rows.Add(dr);
            }
            DataSet set = new DataSet();
            set.Tables.Add(dt);
            return set;
        }

        /// <summary>
        /// 去除空行
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DataTable RemoveEmpty(DataTable data)
        {
            try
            {
                int cutline = 0;
                List<DataRow> removelist = new List<DataRow>();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    bool IsNull = true;
                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(data.Rows[i][j].ToString().Trim()))
                        {
                            IsNull = false;
                        }
                    }
                    if (IsNull)
                    {
                        cutline = i;
                        break;
                        //removelist.Add(data.Rows[i]);
                    }
                }
                DataTable cutDt = data.Clone();
                if (cutline > 0)
                {
                    var query = data.AsEnumerable().Skip(0).Take(cutline);
                    foreach (DataRow item in query)
                    {
                        cutDt.Rows.Add(item.ItemArray);
                    }
                    return cutDt;
                }
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}