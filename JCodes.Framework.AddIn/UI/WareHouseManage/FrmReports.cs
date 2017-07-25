using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using Aspose.Cells;
using System.IO;
using System.Diagnostics;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Entity;
using JCodes.Framework.AddIn.UI.BizControl;
using JCodes.Framework.CommonControl.BaseUI;

namespace JCodes.Framework.AddIn.UI.WareHouseManage
{
    public partial class FrmReports : BaseDock
    {
        public FrmReports()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取字母，从1开始计数
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetChar(int index)
        {
            string charString = "ABCDEFGHIGKLMNOPQRSTUVWXYZ";
            return charString[index-1].ToString();
        }

        private void btnDeptMonthlyReport_Click(object sender, EventArgs e)
        {
            FrmMonthReportSearch dlg = new FrmMonthReportSearch();
            dlg.ReprotTypeName = ((VistaButton)sender).ButtonText;
            dlg.ReportType = MonthlyReportType.库房部门结存;
            dlg.ShowDialog();

            #region MyRegion
            // TODO
//            string sql = @"Select [LastCount] as LC, [LastMoney] as LM, [CurrentInCount] as CIC, [CurrentInMoney] as CIM, 
//                           [CurrentOutCount] as COC, [CurrentOutMoney] as COM, [CurrentCount] as CC, [CurrentMoney] as CM, 
//                           YearMonth,ItemName 
//                           from WM_ReportMonthCheckOut where ReportType =1";
//            DataTable dt = GetTable(sql);
//            dt.TableName = "BigType";
//            if (dt.Rows.Count == 0)
//                return;

//            WorkbookDesigner designer = new WorkbookDesigner();
//            string path = System.IO.Path.Combine(Application.StartupPath, "Report2-2.xls");
//            designer.Open(path);
//            designer.SetDataSource(dt);
//            designer.SetDataSource("YearMonth", dt.Rows[0]["YearMonth"].ToString());
//            designer.Process();

//            //Save the excel file
//            string fileToSave = FileDialogHelper.SaveExcel();
//            if (string.IsNullOrEmpty(fileToSave))
//            {
//                return;
//            }
//            if (File.Exists(fileToSave))
//            {
//                File.Delete(fileToSave);
//            }
//            designer.Save(fileToSave, FileFormatType.Excel2003);
//            Process.Start(fileToSave); 
            #endregion
        }

        private void btnAllMonthReport_Click(object sender, EventArgs e)
        {
            FrmMonthReportSearch dlg = new FrmMonthReportSearch();
            dlg.ReprotTypeName = ((VistaButton)sender).ButtonText;
            dlg.ReportType = MonthlyReportType.所有库房结存;
            dlg.ShowDialog();

            #region MyRegion
            // TODO
//            string sql = @"Select [LastCount] as LC, [LastMoney] as LM, [CurrentInCount] as CIC, [CurrentInMoney] as CIM, 
//                           [CurrentOutCount] as COC, [CurrentOutMoney] as COM, [CurrentCount] as CC, [CurrentMoney] as CM, 
//                           YearMonth,ItemName 
//                           from WM_ReportMonthCheckOut ";
//            DataTable dtBigType = GetTable(sql + " where ReportType =3");
//            dtBigType.TableName = "BigType";
//            if (dtBigType.Rows.Count == 0)
//                return;

//            DataTable dtItemType = GetTable(sql + " where ReportType =4");
//            dtItemType.TableName = "ItemType";

//            WorkbookDesigner designer = new WorkbookDesigner();
//            string path = System.IO.Path.Combine(Application.StartupPath, "Report2-1.xls");
//            designer.Open(path);
//            designer.SetDataSource(dtBigType);
//            designer.SetDataSource(dtItemType);
//            designer.SetDataSource("YearMonth", dtBigType.Rows[0]["YearMonth"].ToString());
//            designer.Process();

//            //Save the excel file
//            string fileToSave = FileDialogHelper.SaveExcel();
//            if (File.Exists(fileToSave))
//            {
//                File.Delete(fileToSave);
//            }
//            designer.Save(fileToSave, FileFormatType.Excel2003);
//            Process.Start(fileToSave); 
            #endregion
        }

        private void btnWareMonthlyReport_Click(object sender, EventArgs e)
        {
            FrmMonthReportSearch dlg = new FrmMonthReportSearch();
            dlg.ReprotTypeName = ((VistaButton)sender).ButtonText;
            dlg.ReportType = MonthlyReportType.库房结存;
            dlg.ShowDialog();

            #region MyRegion
            // TODO
//            string sql = @"Select [LastCount] as LC, [LastMoney] as LM, [CurrentInCount] as CIC, [CurrentInMoney] as CIM, 
//                           [CurrentOutCount] as COC, [CurrentOutMoney] as COM, [CurrentCount] as CC, [CurrentMoney] as CM, 
//                           YearMonth,ItemName 
//                           from WM_ReportMonthCheckOut where ReportType =2";
//            DataTable dt = GetTable(sql);
//            dt.TableName = "BigType";
//            if (dt.Rows.Count == 0)
//                return;

//            WorkbookDesigner designer = new WorkbookDesigner();
//            string path = System.IO.Path.Combine(Application.StartupPath, "Report2-3.xls");
//            designer.Open(path);
//            designer.SetDataSource(dt);
//            designer.SetDataSource("YearMonth", dt.Rows[0]["YearMonth"].ToString());
//            designer.Process();

//            //Save the excel file
//            string fileToSave = FileDialogHelper.SaveExcel();
//            if (File.Exists(fileToSave))
//            {
//                File.Delete(fileToSave);
//            }
//            designer.Save(fileToSave, FileFormatType.Excel2003);
//            Process.Start(fileToSave); 
            #endregion
        }

        private void btnPartMonthlyReport_Click(object sender, EventArgs e)
        {
            FrmMonthReportSearch dlg = new FrmMonthReportSearch();
            dlg.ReprotTypeName = ((VistaButton)sender).ButtonText;
            dlg.ReportType = MonthlyReportType.车间成本月报表;
            dlg.ShowDialog();

            #region MyRegion
            // TODO
            //            string sql = @"Select [YearMonth], [DeptName], [ItemType], [TotalMoney]
            //                           from WM_ReportDeptCost ";
            //            DataTable dt = GetTable(sql);
            //            if (dt.Rows.Count == 0)
            //                return;

            //            List<string> itemTypeList = new List<string>();
            //            List<string> partList = new List<string>();
            //            foreach (DataRow row in dt.Rows)
            //            {
            //                string itemType = row["ItemType"].ToString();
            //                if (!itemTypeList.Contains(itemType))
            //                {
            //                    itemTypeList.Add(itemType);
            //                }

            //                string part = row["DeptName"].ToString();
            //                if (!partList.Contains(part))
            //                {
            //                    partList.Add(part);
            //                }
            //            }

            //            string columnString = "DeptName";
            //            for (int i = 0; i < itemTypeList.Count; i++)
            //            {
            //                columnString += string.Format(",TotalMoney{0}|decimal", i);
            //            }
            //            DataTable dtBigType = DataTableHelper.CreateTable(columnString);
            //            dtBigType.TableName = "BigType";
            //            foreach (string part in partList)
            //            {
            //                DataRow row = dtBigType.NewRow();
            //                row["DeptName"] = part;
            //                for (int i = 0; i < itemTypeList.Count; i++)
            //                {
            //                    string itemType = itemTypeList[i];
            //                    DataRow[] rowSelect = dt.Select(string.Format("DeptName='{0}' AND ItemType='{1}'", part, itemType));
            //                    if (rowSelect.Length > 0)
            //                    {
            //                        row["TotalMoney" + i.ToString()] = rowSelect[0]["TotalMoney"];
            //                    }
            //                }
            //                dtBigType.Rows.Add(row);
            //            }

            //            WorkbookDesigner designer = new WorkbookDesigner();
            //            string path = System.IO.Path.Combine(Application.StartupPath, "Report1.xls");
            //            designer.Open(path);

            //            Aspose.Cells.Worksheet w = designer.Workbook.Worksheets[0];
            //            //先设置标题项目：如大修件，日常备件等
            //            int rowIndex = 2;//第三行为标题
            //            Aspose.Cells.Style style = w.Cells[rowIndex + 1, 1].Style;//继承数字栏目的样式
            //            style.Number = 4;//对应格式是#,##0.00
            //            Aspose.Cells.Style boldStyle = w.Cells[rowIndex, 0].Style;//继承开始栏目的样式
            //            for (int i = 0; i < itemTypeList.Count; i++)
            //            {
            //                w.Cells[rowIndex, i + 1].PutValue(itemTypeList[i]);
            //                w.Cells[rowIndex, i + 1].SetStyle(boldStyle);
            //                w.Cells[rowIndex + 1, i + 1].PutValue("&=BigType.TotalMoney" + i.ToString());
            //                w.Cells[rowIndex + 1, i + 1].SetStyle(style);
            //            }

            //            //添加合计行
            //            w.Cells[rowIndex, itemTypeList.Count + 1].PutValue("合计");
            //            w.Cells[rowIndex, itemTypeList.Count + 1].SetStyle(boldStyle);
            //            w.Cells[rowIndex + 1, itemTypeList.Count + 1].PutValue(string.Format("&=&=SUM(B{{r}}:{0}{{r}})", GetChar(itemTypeList.Count + 1)));
            //            w.Cells[rowIndex + 1, itemTypeList.Count + 1].SetStyle(style);

            //            designer.SetDataSource(dtBigType);
            //            designer.SetDataSource("YearMonth", dt.Rows[0]["YearMonth"].ToString());
            //            designer.Process();

            //            //Save the excel file
            //            string fileToSave = FileDialogHelper.SaveExcel();
            //            if (File.Exists(fileToSave))
            //            {
            //                File.Delete(fileToSave);
            //            }
            //            designer.Save(fileToSave, FileFormatType.Excel2003);
            //            Process.Start(fileToSave); 
            #endregion
        }

        private void btnAnnualReport_Click(object sender, EventArgs e)
        {
            FrmYearReportSearch dlg = new FrmYearReportSearch();
            dlg.ReprotTypeName = ((VistaButton)sender).ButtonText;
            dlg.ReportType = MonthlyReportType.全年费用汇总表;
            dlg.ShowDialog();
        }

    }
}
