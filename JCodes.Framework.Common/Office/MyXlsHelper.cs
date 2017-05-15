using System;
using System.Linq;
using System.Data;
using System.Text;
using System.Collections.Generic;

using org.in2bits.MyXls;
using org.in2bits.MyXls.ByteUtil;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 使用MyXls操作Excel的辅助类
    /// </summary>
    public class MyXlsHelper
    {
        /// <summary>
        /// 把相关数据导出到Excel文件中
        /// </summary>
        /// <param name="dtSource">数据源内容</param>
        /// <param name="strFileName">导出的Excel文件名</param>
        public static void Export(DataTable dtSource, string strFileName)
        {
            XlsDocument xls = new XlsDocument();
            xls.FileName = DateTime.Now.ToString("yyyyMMddHHmmssffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            //xls.SummaryInformation.Author = ""; //填加xls文件作者信息
            //xls.SummaryInformation.NameOfCreatingApplication = ""; //填加xls文件创建程序信息
            //xls.SummaryInformation.LastSavedBy = ""; //填加xls文件最后保存者信息
            //xls.SummaryInformation.Comments = ""; //填加xls文件作者信息
            //xls.SummaryInformation.Title = ""; //填加xls文件标题信息
            //xls.SummaryInformation.Subject = "";//填加文件主题信息
            //xls.DocumentSummaryInformation.Company = "";//填加文件公司信息

            Worksheet sheet = xls.Workbook.Worksheets.Add("Sheet1");//状态栏标题名称
            Cells cells = sheet.Cells;

            foreach (DataColumn col in dtSource.Columns)
            {
                Cell cell = cells.Add(1, col.Ordinal + 1, col.ColumnName);
                cell.Font.FontFamily = FontFamilies.Roman; //字体
                cell.Font.Bold = true;  //字体为粗体                  
            }

            #region 填充内容

            XF dateStyle = xls.NewXF();
            dateStyle.Format = "yyyy-mm-dd";

            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int rowIndex = i + 2;
                    int colIndex = j + 1;
                    string drValue = dtSource.Rows[i][j].ToString();

                    switch (dtSource.Rows[i][j].GetType().ToString())
                    {
                        case "System.String"://字符串类型
                            cells.Add(rowIndex, colIndex, drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            cells.Add(rowIndex, colIndex, dateV, dateStyle);
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            cells.Add(rowIndex, colIndex, boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            cells.Add(rowIndex, colIndex, intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            cells.Add(rowIndex, colIndex, doubV);
                            break;
                        case "System.DBNull"://空值处理
                            cells.Add(rowIndex, colIndex, null);
                            break;
                        default:
                            cells.Add(rowIndex, colIndex, null);
                            break;
                    }
                }
            }

            #endregion

            xls.FileName = strFileName;
            xls.Save(true);
        }

        /// <summary>
        /// 把相关数据导出到Excel文件中
        /// </summary>
        /// <param name="dtSource">数据源内容</param>
        /// <param name="strFileName">导出的Excel文件名</param>
        public static void ExportEasy(DataTable dtSource, string strFileName)
        {
            XlsDocument xls = new XlsDocument();
            Worksheet sheet = xls.Workbook.Worksheets.Add("Sheet1");

            //填充表头
            foreach (DataColumn col in dtSource.Columns)
            {
                sheet.Cells.Add(1, col.Ordinal + 1, col.ColumnName);
            }

            //填充内容
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    sheet.Cells.Add(i + 2, j + 1, dtSource.Rows[i][j].ToString());
                }
            }

            //保存
            xls.FileName = strFileName;
            xls.Save(true);
        }
    }
 }