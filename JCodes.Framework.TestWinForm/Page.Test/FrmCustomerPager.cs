using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using JCodes.Framework.Entity;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.CommonControl;
using System.Diagnostics;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace WHC.OrderWater.UI
{
    public partial class FrmCustomerPager : DevExpress.XtraEditors.XtraForm
    {
        public FrmCustomerPager()
        {
            InitializeComponent();
        }

        private void FrmCustomerPager_Load(object sender, EventArgs e)
        {
            this.pager1.PageChanged += new JCodes.Framework.CommonControl.PageChangedEventHandler(pager1_PageChanged);
            this.pager1.ExportCurrent += new JCodes.Framework.CommonControl.ExportCurrentEventHandler(pager1_ExportCurrent);
            this.pager1.ExportAll += new JCodes.Framework.CommonControl.ExportAllEventHandler(pager1_ExportAll);

            BindData();
        }

        void pager1_ExportAll(object sender, EventArgs e)
        {
            MessageUtil.ShowTips("导出所有");

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Excel (*.xls)|*.xls";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (!dlg.FileName.Equals(String.Empty))
                {
                    BackgroundWorker bg = new BackgroundWorker();
                    bg.DoWork += new DoWorkEventHandler(All_DoWork);
                    bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(All_WorkerCompleted);
                    bg.RunWorkerAsync(dlg.FileName);
                }
            }
        }
        
        /// <summary>
        /// 使用背景线程导出Excel文档
        /// </summary>
        private void All_DoWork(object sender, DoWorkEventArgs e)
        {
            string where = "1=1";
            PagerInfo info = new PagerInfo();
            info.PageSize = int.MaxValue;
            info.CurrenetPageIndex = 1;
            DataTable table = FindToDataTable(where, info);

            string outError = "";
            string filePath = (String)e.Argument;
            AsposeExcelTools.DataTableToExcel2(table, filePath, out outError);
            e.Result = filePath;
        }
        private void All_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (MessageBox.Show("导出操作完成, 您想打开该Excel文件么?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Process.Start(e.Result.ToString());
            }
        }

        void pager1_ExportCurrent(object sender, EventArgs e)
        {
            MessageUtil.ShowTips("导出当前页");
        }

        void pager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        #region 查询辅助函数
        /// <summary>    
        /// 执行SQL查询语句，返回查询结果的所有记录的第一个字段,用逗号分隔。    
        /// </summary>    
        /// <param name="sql">SQL语句</param>    
        /// <returns>    
        /// 返回查询结果的所有记录的第一个字段,用逗号分隔。    
        /// </returns>    
        public string SqlValueList(string sql)
        {
            StringBuilder result = new StringBuilder();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);

            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    result.AppendFormat("{0},", dr[0].ToString());
                }
            }
            string strResult = result.ToString().Trim(',');
            return strResult;
        }

        /// <summary>    
        /// 执行SQL查询语句，返回所有记录的DataTable集合。    
        /// </summary>    
        /// <param name="sql">SQL查询语句</param>    
        /// <returns></returns>    
        public DataTable SqlTable(string sql)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);
            return db.ExecuteDataSet(command).Tables[0];
        }

        /// <summary>
        /// 标准的记录查询函数
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pagerInfo"></param>
        /// <returns></returns>
        private DataTable FindToDataTable(string where, PagerInfo pagerInfo)
        {
            JCodes.Framework.CommonControl.PagerHelper helper = new JCodes.Framework.CommonControl.PagerHelper("All_Customer", "*", "LastUpdated", pagerInfo.PageSize, pagerInfo.CurrenetPageIndex, true, where);
            string countSql = helper.GetPagingSql(DatabaseType.Access, true);
            string dataSql = helper.GetPagingSql(DatabaseType.Access, false);

            string value = SqlValueList(countSql);
            pagerInfo.RecordCount = Convert.ToInt32(value);//为了显示具体的信息，需要设置总记录数

            DataTable dt = SqlTable(dataSql);
            return dt;
        }

        /// <summary>
        /// 根据查询条件构造查询语句
        /// </summary>
        /// <returns></returns>
        private string GetSearchSql()
        {
            SearchCondition condition = new SearchCondition();
            condition.AddCondition("Name", this.txtName.Text, SqlOperator.Like)
                .AddCondition("Type", this.cmbType.Text, SqlOperator.Like)
                .AddCondition("Area", this.cmbArea.Text, SqlOperator.Like);
            string where = condition.BuildConditionSql().Replace("Where", "");
            return where;
        }
        #endregion

        private void BindData()
        {
            string where = GetSearchSql();
            this.pager1.PageSize = 30;
            DataTable dt = FindToDataTable(where, this.pager1.PagerInfo);            
            this.gridControl1.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
    }
}