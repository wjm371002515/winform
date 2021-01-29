using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OleDb;

namespace JCodes.Framework.Common.Files
{
    /// <summary>
    /// CSV文件和DataTable对象转换辅助类
    /// </summary>
    public class CSVHelper
    {
        /// <summary>
        /// CSV转换成DataTable（OleDb数据库访问方式）
        /// </summary>
        /// <param name="csvPath">csv文件路径</param>
        /// <returns></returns>
        public static DataTable CSVToDataTableByOledb(string csvPath)
        {
            DataTable csvdt = new DataTable("csv");
            if (!File.Exists(csvPath))
            {
                throw new FileNotFoundException("csv文件路径不存在!");
            }

            FileInfo fileInfo = new FileInfo(csvPath);
            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileInfo.DirectoryName + ";Extended Properties='Text;'"))
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [" + fileInfo.Name + "]", conn);
                adapter.Fill(csvdt);
            }
            return csvdt;
        }

        /// <summary>
        /// CSV转换成DataTable（文件流方式）
        /// </summary>
        /// <param name="csvPath">csv文件路径</param>
        /// <returns></returns>
        public static DataTable CSVToDataTableByStreamReader(string csvPath)
        {
            DataTable csvdt = new DataTable("csv");

            int intColCount = 0;
            bool blnFlag = true;
            DataColumn column;
            DataRow row;
            string strline = null;
            string[] aryline;

            using (StreamReader reader = new StreamReader(csvPath, FileUtil.GetEncoding(csvPath)))
            {
                while (!string.IsNullOrEmpty((strline = reader.ReadLine())))
                {
                    aryline = strline.Split(new char[] { ',' });

                    if (blnFlag)
                    {
                        blnFlag = false;
                        intColCount = aryline.Length;
                        for (int i = 0; i < aryline.Length; i++)
                        {
                            column = new DataColumn(aryline[i]);
                            csvdt.Columns.Add(column);
                        }
                        continue;
                    }

                    row = csvdt.NewRow();
                    for (int i = 0; i < intColCount; i++)
                    {
                        row[i] = aryline[i];
                    }
                    csvdt.Rows.Add(row);
                }
            }

            return csvdt;
        }

        /// <summary>
        /// DataTable 生成 CSV
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="csvPath">csv文件路径</param>
        public static void DataTableToCSV(DataTable dt, string csvPath)
        {
            if (null == dt)
                return;

            StringBuilder csvText = new StringBuilder();
            StringBuilder csvrowText = new StringBuilder();
            foreach (DataColumn dc in dt.Columns)
            {
                csvrowText.Append(",");
                csvrowText.Append(dc.ColumnName);
            }
            csvText.AppendLine(csvrowText.ToString().Substring(1));

            foreach (DataRow dr in dt.Rows)
            {
                csvrowText = new StringBuilder();
                foreach (DataColumn dc in dt.Columns)
                {
                    csvrowText.Append(",");
                    // 20170427 wujm 支持换行
                    if (dr[dc.ColumnName].ToString().Contains(Const.WrapText) || dr[dc.ColumnName].ToString().Contains(Const.DoubleQuote))
                    {
                        csvrowText.Append(Const.DoubleQuote + dr[dc.ColumnName].ToString().Replace(',', ' ').Replace(Const.DoubleQuote, Const.DoubleQuote+Const.DoubleQuote) + Const.DoubleQuote);
                    }
                    else
                    {
                        csvrowText.Append(dr[dc.ColumnName].ToString().Replace(',', ' '));
                    }
                    
                }
                csvText.AppendLine(csvrowText.ToString().Substring(1));
            }

            File.WriteAllText(csvPath, csvText.ToString(), Encoding.Default);
        }
    }
}
