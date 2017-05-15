using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Aspose.Cells;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.BLL;

namespace JCodes.Framework.AddIn.UI.WareHouse
{
    public partial class FrmMonthReportSearch : BaseForm
    {
        public string ReprotTypeName = "";
        public MonthlyReportType ReportType;

        public FrmMonthReportSearch()
        {
            InitializeComponent();
        }

        private void FrmMonthReportSearch_Load(object sender, EventArgs e)
        {
            this.winGridView1.AppendedMenu = this.contextMenuStrip1;
            this.winGridView1.gridView1.DoubleClick +=new EventHandler(gridView1_DoubleClick);
            this.winGridView1.gridControl1.DataSourceChanged += new EventHandler(gridControl1_DataSourceChanged);

            InitItem();
            BindMonthlyData();
        }

        void gridView1_DoubleClick(object sender, EventArgs e)
        {
            menu_Excel_Click(null, null);
        }

        void gridControl1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.winGridView1.gridView1.Columns.Count > 0 &&
                this.winGridView1.gridView1.RowCount > 0)
            {
                this.winGridView1.gridView1.Columns["ReportTitle"].Width = 200;
            }
        }

        /// <summary>
        /// 获取字母，从1开始计数
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetChar(int index)
        {
            string charString = "ABCDEFGHIGKLMNOPQRSTUVWXYZ";
            return charString[index - 1].ToString();
        }

        private void ExportExelReport(string headerId, string yearhMonth)
        {
            string sql = string.Format(@"Select [LastCount] as LC, [LastMoney] as LM, [CurrentInCount] as CIC, [CurrentInMoney] as CIM, 
                           [CurrentOutCount] as COC, [CurrentOutMoney] as COM, [CurrentCount] as CC, [CurrentMoney] as CM, 
                           ItemName 
                           from WM_ReportMonthlyDetail WHERE Header_ID={0} ", headerId);
            WorkbookDesigner designer = new WorkbookDesigner();
            if (ReportType == MonthlyReportType.库房部门结存)
            {
                #region MyRegion
                DataTable dt = BLLFactory<BLL.WareHouse>.Instance.SqlTable(sql);
                dt.TableName = "BigType";
                if (dt.Rows.Count == 0)
                    return;

                string path = System.IO.Path.Combine(Application.StartupPath, "Report2-2.xls");
                designer.Workbook = new Workbook(path);
                designer.SetDataSource(dt);
                designer.SetDataSource("YearMonth", yearhMonth);
                designer.Process(); 
                #endregion
            }
            else if (ReportType == MonthlyReportType.库房结存)
            {
                #region MyRegion
                DataTable dt = BLLFactory<BLL.WareHouse>.Instance.SqlTable(sql);
                dt.TableName = "BigType";
                if (dt.Rows.Count == 0)
                    return;

                string path = System.IO.Path.Combine(Application.StartupPath, "Report2-3.xls");
                designer.Workbook = new Workbook(path);
                designer.SetDataSource(dt);
                designer.SetDataSource("YearMonth", yearhMonth);
                designer.Process(); 
                #endregion
            }
            else if (ReportType == MonthlyReportType.所有库房结存)
            {
                #region MyRegion
                DataTable dtBigType = BLLFactory<BLL.WareHouse>.Instance.SqlTable(sql + " AND ReportCode ='ItemBigType'");
                dtBigType.TableName = "BigType";
                if (dtBigType.Rows.Count == 0)
                    return;

                DataTable dtItemType = BLLFactory<BLL.WareHouse>.Instance.SqlTable(sql + " AND ReportCode ='ItemType'");
                dtItemType.TableName = "ItemType";

                string path = System.IO.Path.Combine(Application.StartupPath, "Report2-1.xls");
                designer.Workbook = new Workbook(path);
                designer.SetDataSource(dtBigType);
                designer.SetDataSource(dtItemType);
                designer.SetDataSource("YearMonth", yearhMonth);
                designer.Process(); 
                #endregion
            }
            else if (ReportType == MonthlyReportType.车间成本月报表)
            {
                sql = string.Format(@"Select [DeptName], [ItemType], [TotalMoney] from WM_ReportMonthlyCostDetail WHERE Header_ID={0} ", headerId);
                DataTable dt = BLLFactory<BLL.WareHouse>.Instance.SqlTable(sql);
                if (dt.Rows.Count == 0)
                    return;

                #region 获取备件类型和部门列表（不重复记录）
                List<string> itemTypeList = new List<string>();
                List<string> partList = new List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    string itemType = row["ItemType"].ToString();
                    if (!itemTypeList.Contains(itemType))
                    {
                        itemTypeList.Add(itemType);
                    }

                    string part = row["DeptName"].ToString();
                    if (!partList.Contains(part))
                    {
                        partList.Add(part);
                    }
                } 
                #endregion

                #region 构造部门及备件类型的二维表格数据
                string columnString = "DeptName";
                for (int i = 0; i < itemTypeList.Count; i++)
                {
                    columnString += string.Format(",TotalMoney{0}|decimal", i);
                }
                DataTable dtBigType = DataTableHelper.CreateTable(columnString);
                dtBigType.TableName = "BigType";
                foreach (string part in partList)
                {
                    DataRow row = dtBigType.NewRow();
                    row["DeptName"] = part;
                    for (int i = 0; i < itemTypeList.Count; i++)
                    {
                        string itemType = itemTypeList[i];
                        DataRow[] rowSelect = dt.Select(string.Format("DeptName='{0}' AND ItemType='{1}'", part, itemType));
                        if (rowSelect.Length > 0)
                        {
                            row["TotalMoney" + i.ToString()] = rowSelect[0]["TotalMoney"];
                        }
                    }
                    dtBigType.Rows.Add(row);
                } 
                #endregion

                #region 动态构造栏目
                string path = System.IO.Path.Combine(Application.StartupPath, "Report1.xls");
                designer.Workbook = new Workbook(path);

                Aspose.Cells.Worksheet w = designer.Workbook.Worksheets[0];
                //先设置标题项目：如大修件，日常备件等
                int rowIndex = 2;//第三行为标题
                Aspose.Cells.Style boldStyle = w.Cells[rowIndex, 0].GetStyle();//继承开始栏目的样式
                Aspose.Cells.Style style = w.Cells[rowIndex + 1, 1].GetStyle();//继承数字栏目的样式
                style.Number = 4;//金额数据对应格式是#,##0.00

                for (int i = 0; i < itemTypeList.Count; i++)
                {
                    //动态添加备件类型栏目
                    w.Cells[rowIndex, i + 1].PutValue(itemTypeList[i]);
                    w.Cells[rowIndex, i + 1].SetStyle(boldStyle);

                    //动态添加下面的对应的金额数据
                    w.Cells[rowIndex + 1, i + 1].PutValue("&=BigType.TotalMoney" + i.ToString());
                    w.Cells[rowIndex + 1, i + 1].SetStyle(style);
                }

                //添加合计行
                w.Cells[rowIndex, itemTypeList.Count + 1].PutValue("合计");
                w.Cells[rowIndex, itemTypeList.Count + 1].SetStyle(boldStyle);
                w.Cells[rowIndex + 1, itemTypeList.Count + 1].PutValue(string.Format("&=&=SUM(B{{r}}:{0}{{r}})", GetChar(itemTypeList.Count + 1)));
                w.Cells[rowIndex + 1, itemTypeList.Count + 1].SetStyle(style); 
                #endregion

                designer.SetDataSource(dtBigType);
                designer.SetDataSource("YearMonth", yearhMonth);
                designer.Process();
            }

            #region 保存Excel文件
            SpecialDirectories sp = new SpecialDirectories();
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel(*.xls)|*.xls|All File(*.*)|*.*";
            dialog.Title = "保存Excel";
            dialog.InitialDirectory = sp.Desktop;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileToSave = dialog.FileName;
                if (string.IsNullOrEmpty(fileToSave))
                {
                    return;
                }
                if (File.Exists(fileToSave))
                {
                    File.Delete(fileToSave);
                }
                designer.Workbook.Save(fileToSave, SaveFormat.Excel97To2003);
                Process.Start(fileToSave);
            }
            #endregion
        }

        private void InitItem()
        {
            //显示报表类型标题，提示是那类型的月报表
            if (!string.IsNullOrEmpty(ReprotTypeName))
            {
                this.Text = ReprotTypeName;
            }

            for (int year = 2010; year <= 2020; year++)
            {
                this.txtMonthlyYear.Properties.Items.Add(string.Format("{0}", year));
            }
            this.txtMonthlyYear.Text = string.Format("{0}", DateTime.Now.Year);
        }

        private void btnMonthlySearch_Click(object sender, EventArgs e)
        {
            BindMonthlyData();
        }

        private void BindMonthlyData()
        {
            this.winGridView1.DisplayColumns = "ID,ReportTitle,ReportYear,ReportMonth,YearMonth,CreateDate,Creator";

            #region 添加别名解析
            this.winGridView1.AddColumnAlias("ID", "编号");
            this.winGridView1.AddColumnAlias("ReportTitle", "报表名称");
            this.winGridView1.AddColumnAlias("ReportYear", "报表年份");
            this.winGridView1.AddColumnAlias("ReportMonth", "报表月份");
            this.winGridView1.AddColumnAlias("YearMonth", "报表年月");
            this.winGridView1.AddColumnAlias("CreateDate", "创建日期");
            this.winGridView1.AddColumnAlias("Creator", "操作员");
            this.winGridView1.AddColumnAlias("Note", "备注信息");
            #endregion

            SearchCondition condition = new SearchCondition();
            condition.AddCondition("ReportYear", Convert.ToInt32(this.txtMonthlyYear.Text), SqlOperator.Equal)
                .AddCondition("ReportType", (int)ReportType, SqlOperator.Equal);
            string filter = condition.BuildConditionSql();

            DataTable dt = BLLFactory<ReportMonthlyHeader>.Instance.GetMonthlyReport(filter);
            this.winGridView1.DataSource = dt.DefaultView;
            this.winGridView1.PrintTitle = this.AppInfo.AppUnit + " -- " + ReprotTypeName;
            if (dt.Rows.Count == 0)
            {
                MessageDxUtil.ShowTips("数据库中没有报表记录");
            }
        }

        private void txtMonthlyYear_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindMonthlyData();
            }
        }

        private void menu_InRefresh_Click(object sender, EventArgs e)
        {
            BindMonthlyData();
        }

        private void menu_Excel_Click(object sender, EventArgs e)
        {
            if (this.winGridView1.gridView1.FocusedRowHandle >= 0)
            {
                string headerID = this.winGridView1.gridView1.GetFocusedRowCellDisplayText("ID");
                string year = this.winGridView1.gridView1.GetFocusedRowCellDisplayText("ReportYear");
                string month = this.winGridView1.gridView1.GetFocusedRowCellDisplayText("ReportMonth");
                string yearMonth = this.winGridView1.gridView1.GetFocusedRowCellDisplayText("YearMonth");

                ExportExelReport(headerID, yearMonth);
            }
        }
    }
}
