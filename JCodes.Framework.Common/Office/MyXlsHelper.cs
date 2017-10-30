using System;
using System.Data;

using org.in2bits.MyXls;
using JCodes.Framework.Common.Format;

namespace JCodes.Framework.Common.Office
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
        public static void Export(DataTable dtSource, string strFileName, string SheetName = "Sheet1", Int32 startRow = 0, Int32 startColumn = 0)
        {
            XlsDocument xls = new XlsDocument();
            xls.FileName = DateTimeHelper.GetServerDateTime2().ToString("yyyyMMddHHmmssffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            xls.SummaryInformation.Author = "吴建明"; //填加xls文件作者信息
            xls.SummaryInformation.NameOfCreatingApplication = "jCodes 项目管理"; //填加xls文件创建程序信息
            xls.SummaryInformation.LastSavedBy = "吴建明"; //填加xls文件最后保存者信息
            xls.SummaryInformation.Comments = "吴建明添加的项目信息"; //填加xls文件作者信息
            xls.SummaryInformation.Title = "项目管理信息"; //填加xls文件标题信息
            xls.SummaryInformation.Subject = "jCodes项目管理信息";//填加文件主题信息
            xls.DocumentSummaryInformation.Company = "jCodes.cn";//填加文件公司信息

            Worksheet sheet = xls.Workbook.Worksheets.Add(SheetName);//状态栏标题名称
            Cells cells = sheet.Cells;

            foreach (DataColumn col in dtSource.Columns)
            {
                Cell cell = cells.Add(1 + startRow, col.Ordinal + 1 + startColumn, col.ColumnName);
                cell.Font.FontName = "宋体"; // 字体  
                cell.HorizontalAlignment = HorizontalAlignments.Centered;
                cell.VerticalAlignment = VerticalAlignments.Centered;
                cell.UseBorder = true; // 使用边框   
                cell.TopLineStyle = 2; // 上边框样式  
                cell.TopLineColor = Colors.Black; // 上边框颜色  
                cell.LeftLineStyle = 2; // 左边框样式  
                cell.LeftLineColor = Colors.Black; // 左边框颜色  
                cell.RightLineStyle = 2; // 右边框样式  
                cell.RightLineColor = Colors.Black; // 右边框颜色 
                cell.BottomLineStyle = 2;
                cell.BottomLineColor = Colors.Black;
                cell.Pattern = 1; // 单元格填充风格。如果设定为0，则是纯色填充(无色)，1代表没有间隙的实色  
                cell.PatternColor = Colors.Default2B;// ffff99
                cell.PatternBackgroundColor = Colors.Default2B; // 填充的底色   
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
                    Cell cell = null;
                    switch (dtSource.Rows[i][j].GetType().ToString())
                    {
                        case "System.String"://字符串类型
                            cell = cells.Add(rowIndex + startColumn, colIndex + startColumn, drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            cell = cells.Add(rowIndex + startColumn, colIndex + startColumn, dateV, dateStyle);
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            cell = cells.Add(rowIndex + startColumn, colIndex + startColumn, boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            cell = cells.Add(rowIndex + startColumn, colIndex + startColumn, intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                           cell = cells.Add(rowIndex + startColumn, colIndex + startColumn, doubV);
                            break;
                        case "System.DBNull"://空值处理
                            cell = cells.Add(rowIndex + startColumn, colIndex + startColumn, null);
                            break;
                        default:
                            cell = cells.Add(rowIndex + startColumn, colIndex + startColumn, null);
                            break;
                    }

                    if (null != cell)
                    {
                        cell.UseBorder = true; // 使用边框   
                        cell.TopLineStyle = 1; // 上边框样式  
                        cell.TopLineColor = Colors.Black; // 上边框颜色  
                        cell.LeftLineStyle = 1; // 左边框样式  
                        cell.LeftLineColor = Colors.Black; // 左边框颜色  
                        cell.RightLineStyle = 1; // 右边框样式  
                        cell.RightLineColor = Colors.Black; // 右边框颜色 
                        cell.BottomLineStyle = 1;
                        cell.BottomLineColor = Colors.Black;
                        cell.Pattern = 1; // 单元格填充风格。如果设定为0，则是纯色填充(无色)，1代表没有间隙的实色  
                        cell.PatternColor = Colors.White;// 白色
                        cell.PatternBackgroundColor = Colors.White; // 填充的底色  
                    }
                }
            }

            #endregion

            xls.FileName = strFileName;
            xls.Save(true);
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="SheetName"></param>
        /// <param name="startRow"></param>
        /// <param name="startColumn"></param>
        /// <returns></returns>
        public static DataTable Import(string strFileName, string SheetName = "Sheet1", Int32 startRow = 0, Int32 startColumn = 0)
        {
            DataTable dt = new DataTable();
            XlsDocument xls = new XlsDocument(strFileName);

            Worksheet sheet = xls.Workbook.Worksheets[SheetName];
            //Cells cells = sheet.Rows;
            // sheet.Rows[3].GetCell(ushort.Parse("2")).Value
            // sheet.Rows[3].GetCell(ushort.Parse("2")).PatternBackgroundColor
            // sheet.Rows[3].GetCell(ushort.Parse("2")).PatternColor

            // 第一行是表头
            var cellrowheader = sheet.Rows[ushort.Parse(startRow.ToString())];
            if (cellrowheader.CellCount == 0)
                return null;

            for (Int32 j = startColumn; j <= cellrowheader.CellCount; j++)
            {
                dt.Columns.Add(cellrowheader.GetCell(ushort.Parse(j.ToString())).Value.ToString());
            }

            for (Int32 i = startRow + 1; i < sheet.Rows.Count; i ++)
            {
                Int32 idx = 0;
                DataRow dr = dt.NewRow();

                var cellrow = sheet.Rows[ushort.Parse(i.ToString())];
                if (cellrow.CellCount == 0)
                    continue;

                bool isAdd = false;

                for (Int32 j = startColumn; j <= cellrow.CellCount; j++)
                {
                    object cellvalue = cellrow.GetCell(ushort.Parse(j.ToString())).Value;
                    if (cellvalue != null && !string.IsNullOrEmpty(cellvalue.ToString()))
                    {
                        dr[idx] = cellvalue;
                        idx++;
                        isAdd = true;
                    }
                    
                }
                if (isAdd)
                    dt.Rows.Add(dr);
            }
            return dt;

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