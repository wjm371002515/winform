using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS;
using NPOI.SS.UserModel;
using NPOI.Util;

namespace JCodes.Framework.Common.Office
{
    /// <summary>
    /// NPOI操作Excel的工具类(也可以参考NPOIHelper）
    /// </summary>
    public class NPOIUtils
    {
        /// <summary>
        /// 数据表导入到Excel中
        /// </summary>
        /// <param name="SourceTable">数据表</param>
        /// <returns>Excel文件流</returns>
        public static Stream DataTableToExcel(DataTable SourceTable)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet = workbook.CreateSheet();
            IRow headerRow = sheet.CreateRow(0);

            // handling header.
            foreach (DataColumn column in SourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value.
            int rowIndex = 1;

            foreach (DataRow row in SourceTable.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);

                foreach (DataColumn column in SourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }

                rowIndex++;
            }

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        }

        /// <summary>
        /// 数据表导入到Excel中
        /// </summary>
        /// <param name="SourceTable">数据表</param>
        /// <param name="FileName">Excel文件</param>
        public static void DataTableToExcel(DataTable SourceTable, string FileName)
        {
            MemoryStream ms = DataTableToExcel(SourceTable) as MemoryStream;
            FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            byte[] data = ms.ToArray();

            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();

            data = null;
            ms = null;
            fs = null;
        }

        /// <summary>
        /// 将数据导入到Excel中(通过制定格式的模板)
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="excelTemplate"></param>
        /// <param name="fileName"></param>
        public static void DataTableToExcel(DataTable sourceTable, Stream excelTemplate, string fileName, int templateRowIndex = 1)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(excelTemplate);

            ISheet sheet = workbook.GetSheetAt(0);
            IRow templateRow = sheet.GetRow(templateRowIndex);
            List<KeyValuePair<object, bool>> rowTemplate = new List<KeyValuePair<object, bool>>();
            int cellCount = templateRow.LastCellNum;
            for (int i = templateRow.FirstCellNum; i < cellCount; i++)
            {
                ICell cell = templateRow.GetCell(i);
                object cellValue = null;
                if (cell != null)
                {
                    switch (cell.CellType)
                    {

                        case CellType.BOOLEAN:
                            cellValue = cell.BooleanCellValue;
                            break;
                        case CellType.ERROR:
                            cellValue = cell.ErrorCellValue;
                            break;
                        case CellType.FORMULA:
                            cellValue = cell.CellFormula;
                            break;
                        case CellType.NUMERIC:
                            cellValue = cell.NumericCellValue;
                            break;
                        case CellType.BLANK:
                        case CellType.STRING:
                        case CellType.Unknown:
                        default:
                            cellValue = cell.StringCellValue;
                            break;
                    }
                }
                if (cellValue != null && cellValue.ToString().IndexOf("{") > -1 && cellValue.ToString().IndexOf("}") > -1)
                {
                    cellValue = cellValue.ToString().Trim(' ', '\t', '{', '}');
                    rowTemplate.Add(new KeyValuePair<object, bool>(cellValue, true));
                }
                else
                {
                    rowTemplate.Add(new KeyValuePair<object, bool>(cellValue, false));
                }
            }
            int rowIndex = templateRowIndex;
            foreach (DataRow row in sourceTable.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                if (templateRow.RowStyle != null)
                {
                    dataRow.RowStyle = templateRow.RowStyle;
                }
                for (int i = 0; i < rowTemplate.Count; i++)
                {
                    KeyValuePair<object, bool> keyValue = rowTemplate[i];
                    ICell cell = dataRow.CreateCell(i);
                    if (keyValue.Value && sourceTable.Columns.Contains(keyValue.Key.ToString()))//
                    {
                        if (row[keyValue.Key.ToString()] is DateTime)
                        {
                            cell.SetCellValue(((DateTime)row[keyValue.Key.ToString()]).ToString("yyyy/MM/dd"));
                        }
                        else
                        {
                            cell.SetCellValue(row[keyValue.Key.ToString()].ToString());
                        }
                    }
                    else
                    {
                        if (keyValue.Key is bool)
                        {
                            cell.SetCellValue((bool)keyValue.Key);
                        }
                        else if (keyValue.Key is DateTime)
                        {
                            cell.SetCellValue((DateTime)keyValue.Key);
                        }
                        else if (keyValue.Key is string)
                        {
                            cell.SetCellValue((string)keyValue.Key);
                        }
                        else if (keyValue.Key is double)
                        {
                            cell.SetCellValue((double)keyValue.Key);
                        }
                        else if (keyValue.Key is IRichTextString)
                        {
                            cell.SetCellValue((IRichTextString)keyValue.Key);
                        }
                        else
                        {
                            cell.SetCellValue(keyValue.Key == null ? null : keyValue.Key.ToString());
                        }
                    }
                }
                rowIndex++;
            }
            using (FileStream destStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                workbook.Write(destStream);
            }
        }

        /// <summary>
        /// Excel导入数据表
        /// </summary>
        /// <param name="ExcelFileStream">Excel文件流</param>
        /// <param name="SheetName">页签名</param>
        /// <param name="HeaderRowIndex">列头行的行号</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(Stream ExcelFileStream, string SheetName, int HeaderRowIndex = 0)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
            ISheet sheet = workbook.GetSheet(SheetName);
            DataTable table = SheetToDataTable(sheet, HeaderRowIndex);
            return table;
        }

        /// <summary>
        /// Excel导入数据表
        /// </summary>
        /// <param name="ExcelFileStream">Excel文件流</param>
        /// <param name="SheetIndex">页签序号</param>
        /// <param name="HeaderRowIndex">列头行的行号</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(Stream ExcelFileStream, int SheetIndex = 0, int HeaderRowIndex = 0)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
            ISheet sheet = workbook.GetSheetAt(SheetIndex);
            DataTable table = SheetToDataTable(sheet, HeaderRowIndex);
            return table;
        }

        /// <summary>
        /// Excel导入数据表
        /// </summary>
        /// <param name="ExcelFileName">Excel文件名</param>
        /// <param name="SheetName">页签名</param>
        /// <param name="HeaderRowIndex">列头行的行号</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string ExcelFileName, string SheetName, int HeaderRowIndex = 0)
        {
            HSSFWorkbook workbook = null;
            using (FileStream stream = new FileStream(ExcelFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(stream);
            }
            ISheet sheet = workbook.GetSheet(SheetName);
            DataTable table = SheetToDataTable(sheet, HeaderRowIndex);
            return table;
        }

        /// <summary>
        /// Excel导入数据表
        /// </summary>
        /// <param name="ExcelFileName">Excel文件名</param>
        /// <param name="SheetIndex">页签序号</param>
        /// <param name="HeaderRowIndex">列头行的行号</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string ExcelFileName, int SheetIndex = 0, int HeaderRowIndex = 0)
        {
            HSSFWorkbook workbook = null;
            using (FileStream stream = new FileStream(ExcelFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(stream);
            }
            ISheet sheet = workbook.GetSheetAt(SheetIndex);
            DataTable table = SheetToDataTable(sheet, HeaderRowIndex);
            return table;
        }

        /// <summary>
        /// Sheet页导入数据表
        /// </summary>
        /// <param name="sheet">页签对象</param>
        /// <param name="HeaderRowIndex">列头行的序号</param>
        /// <returns></returns>
        private static DataTable SheetToDataTable(ISheet sheet, int HeaderRowIndex)
        {
            DataTable table = new DataTable();

            IRow headerRow = sheet.GetRow(HeaderRowIndex);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                table.Rows.Add(dataRow);
            }
            return table;
        }
    }
}
