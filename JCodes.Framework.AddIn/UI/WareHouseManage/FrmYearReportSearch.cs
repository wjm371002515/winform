using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using Aspose.Cells;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Files;

namespace JCodes.Framework.AddIn.UI.WareHouseManage
{
    public partial class FrmYearReportSearch : BaseDock
    {
        public string ReprotTypeName = "";
        public MonthlyReportType ReportType;

        public FrmYearReportSearch()
        {
            InitializeComponent();
        }

        private void FrmYearReportSearch_Load(object sender, EventArgs e)
        {
            this.winGridView1.AppendedMenu = this.contextMenuStrip1;
            this.winGridView1.gridView1.DoubleClick +=new EventHandler(gridView1_DoubleClick);
            //this.winGridView1.dataGridView1.RowEnter += new DataGridViewCellEventHandler(dataGridView1_RowEnter);
            this.winGridView1.gridControl1.DataSourceChanged += new EventHandler(gridControl1_DataSourceChanged);
            this.winGridView1.ShowLineNumber = true;            

            InitItem();
            BindAnnualData();
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

        private void ExportExelReport(string headerId, string year)
        {
            // TODO
            string sql = string.Format(@"Select [ItemType],[CostCenterOrDept],
                           [One],[Two],[Three],[Four],[Five],[Six],
                           [Seven],[Eight],[Nine],[Ten],[Eleven],[Twelve],[Total] 
                           from WM_ReportAnnualCostDetail WHERE Header_ID={0} ", headerId);
            WorkbookDesigner designer = new WorkbookDesigner();
            if (ReportType == MonthlyReportType.全年费用汇总表)
            {
                #region MyRegion

                DataTable dtCostCenter = BLLFactory<WareHouse>.Instance.SqlTable(sql + string.Format(" AND ReportCode='001' AND Total <> 0 "));
                dtCostCenter.TableName = "CostCenter";

                DataTable dtNormalDept = BLLFactory<WareHouse>.Instance.SqlTable(sql + string.Format(" AND ReportCode='002' AND Total <> 0 "));
                dtNormalDept.TableName = "NormalDept";

                DataTable dtSpecialDept = BLLFactory<WareHouse>.Instance.SqlTable(sql + string.Format(" AND ReportCode='003' AND Total <> 0 "));
                dtSpecialDept.TableName = "SpecialDept";

                string path = System.IO.Path.Combine(Application.StartupPath, "Report3.xls");
                designer.Workbook = new Workbook(path);
                designer.SetDataSource("Year", year);
                designer.SetDataSource(dtCostCenter);
                designer.SetDataSource(dtSpecialDept);
                designer.SetDataSource(dtNormalDept);
                designer.Process(); 
                #endregion
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
            //显示报表类型标题，提示是那类型的报表
            if (!string.IsNullOrEmpty(ReprotTypeName))
            {
                this.Text = ReprotTypeName;
            }
        }

        private void BindAnnualData()
        {
            this.winGridView1.DisplayColumns = "ID,ReportTitle,ReportYear,CreateDate,Creator";

            #region 添加别名解析
            this.winGridView1.AddColumnAlias("ID", "编号");
            this.winGridView1.AddColumnAlias("ReportTitle", "报表名称");
            this.winGridView1.AddColumnAlias("ReportYear", "报表年份");
            this.winGridView1.AddColumnAlias("CreateDate", "创建日期");
            this.winGridView1.AddColumnAlias("Creator", "操作员");
            this.winGridView1.AddColumnAlias("Note", "备注信息");
            #endregion

            SearchCondition condition = new SearchCondition();
            condition.AddCondition("ReportType", (int)ReportType, SqlOperator.Equal);
            string filter = condition.BuildConditionSql();

            DataTable dt = BLLFactory<ReportAnnualCostHeader>.Instance.GetAnnualReport(filter);
            this.winGridView1.DataSource = dt.DefaultView;
            this.winGridView1.PrintTitle = this.AppInfo.AppUnit + " -- " + ReprotTypeName;
            if (dt.Rows.Count == 0)
            {
                MessageDxUtil.ShowTips("数据库中没有报表记录");
            }
        }

        private void menu_InRefresh_Click(object sender, EventArgs e)
        {
            BindAnnualData();
        }

        private void menu_Excel_Click(object sender, EventArgs e)
        {
            if (this.winGridView1.gridView1.FocusedRowHandle >= 0)
            {
                string headerID = this.winGridView1.gridView1.GetFocusedRowCellDisplayText("ID");
                string year = this.winGridView1.gridView1.GetFocusedRowCellDisplayText("ReportYear");

                ExportExelReport(headerID, year);
            }
        }
    }
}
