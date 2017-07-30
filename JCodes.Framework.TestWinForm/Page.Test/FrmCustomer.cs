using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JCodes.Framework.Entity;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DevExpress.XtraGrid.Views.Grid;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.CommonControl.Pager;
using JCodes.Framework.CommonControl.Controls;

namespace WHC.OrderWater.UI
{
    public partial class FrmCustomer : DevExpress.XtraEditors.XtraForm
    {
         public FrmCustomer()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                this.winGridViewPager1.OnPageChanged += new EventHandler(winGridViewPager1_OnPageChanged);
                this.winGridViewPager1.OnStartExport += new EventHandler(winGridViewPager1_OnStartExport);
                this.winGridViewPager1.OnEditSelected += new EventHandler(winGridViewPager1_OnEditSelected);
                this.winGridViewPager1.OnDeleteSelected += new EventHandler(winGridViewPager1_OnDeleteSelected);
                this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);
                this.winGridViewPager1.OnAddNew += new EventHandler(winGridViewPager1_OnAddNew);
                //this.winGridViewPager1.OnGridViewMouseClick += new EventHandler(winGridViewPager1_OnGridViewMouseClick);
                this.winGridViewPager1.AppendedMenu = this.contextMenuStrip1;
                this.winGridViewPager1.ShowLineNumber = true;//显示行号
                this.winGridViewPager1.PagerInfo.PageSize = 30;//页面大小                
                //this.winGridViewPager1.EventRowBackColor = Color.LightCyan;//间隔颜色
                this.winGridViewPager1.ShowCheckBox = true;
                this.winGridViewPager1.OnCheckBoxSelectionChanged += new SelectionChangedEventHandler(winGridViewPager1_OnCheckBoxSelectionChanged);
                this.winGridViewPager1.BestFitColumnWith = false;
                
                this.winGridViewPager1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
                this.winGridViewPager1.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);
                this.winGridViewPager1.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView1_RowCellStyle);
            }
        }
         void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
         {
             //if (e.Column.FieldName == "OrderStatus")
             //{
             //    string status = this.winGridViewPager1.gridView1.GetRowCellValue(e.RowHandle, "OrderStatus").ToString();
             //    Color color = Color.White;
             //    if (status == "已审核")
             //    {
             //        e.Appearance.BackColor = Color.Red;
             //        e.Appearance.BackColor2 = Color.LightCyan;
             //    }
             //}
         }
         void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
         {
             if (e.Column.ColumnType == typeof(DateTime))
             {
                 string columnName = e.Column.FieldName;
                 if (e.Value != null)
                 {
                     if (Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
                     {
                         e.DisplayText = "";
                     }
                     else
                     {
                         e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm");//yyyy-MM-dd
                     }
                 }
             }
             else if (e.Column.FieldName == "Creator")
             {
                 if (e.Value != null)
                 {
                     //e.DisplayText = BLLFactory<User>.Instance.GetFullNameByID(e.Value.ToString().ToInt32());
                 }
             }
         }

         void gridView1_DataSourceChanged(object sender, EventArgs e)
         {
             this.winGridViewPager1.FixedColumns = "Name";

             if (this.winGridViewPager1.gridView1.Columns.Count > 0 && this.winGridViewPager1.gridView1.RowCount > 0)
             {
                 //统一设置100宽度
                 foreach (DevExpress.XtraGrid.Columns.GridColumn column in this.winGridViewPager1.gridView1.Columns)
                 {
                     column.Width = 100;
                 }

                 //可特殊设置特别的宽度
                 //CreateDate,Note,LastUpdated
                 winGridViewPager1.gridView1.SetGridColumWidth("CreateDate", 120);
                 winGridViewPager1.gridView1.SetGridColumWidth("LastUpdated", 120);
                 winGridViewPager1.gridView1.SetGridColumWidth("Address", 200);
                 winGridViewPager1.gridView1.SetGridColumWidth("Note", 200);
             }
         }

         void winGridViewPager1_OnCheckBoxSelectionChanged(object sender, EventArgs e)
         {
             List<int> list = this.winGridViewPager1.GetCheckedRows();
             StringBuilder sb = new StringBuilder();
             int i = 1;
             foreach (int rowindex in list)
             {
                 sb.Append(rowindex.ToString() + ",");
                 if (i++ % 10 == 0)
                 {
                     sb.Append("\r\n");
                 }
             }
             lblCheckboxSelection.Text = sb.ToString().Trim(',');
         }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            EnableDate(false);
            InitArea();
            InitType();
            

            BindData();
        }

        private void winGridViewPager1_OnGridViewMouseClick(object sender, EventArgs e)
        {
            GridView grid = this.winGridViewPager1.gridView1;
            if (grid != null && grid.SelectedRowsCount > 0)
            {
                 int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
                 if (rowSelected != null && rowSelected.Length > 0)
                 {
                     string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(rowSelected[0], "ID");
                     this.Text = ID;
                 }
            }
        }

        private void InitArea()
        {
            this.cmbArea.Items.Clear();
            //List<CustomerAreaInfo> areaList = BLLFactory<CustomerArea>.Instance.GetAll();
            //foreach (CustomerAreaInfo info in areaList)
            //{
            //    this.cmbArea.Items.Add(info.Area);
            //}
        }

        private void InitType()
        {
            this.cmbType.Items.Clear();
            //List<CustomerTypeInfo> typeList = BLLFactory<CustomerType>.Instance.GetAll();
            //foreach (CustomerTypeInfo info in typeList)
            //{
            //    this.cmbType.Items.Add(info.CustomerType);
            //}
        }

        private void EnableDate(bool enable)
        {
            this.dateTimePicker1.Enabled = enable;
            this.dateTimePicker2.Enabled = enable;
        }

        private void chkUseDate_CheckedChanged(object sender, EventArgs e)
        {
            EnableDate(this.chkUseDate.Checked);
        }

        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
        }

        private void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
        {
            if (MessageDxUtil.ShowYesNoAndTips("您确定删除选定的记录么？") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");
                //BLLFactory<Customer>.Instance.Delete(ID);
            }
            BindData();
        }

        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            string ID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID");
            if (!string.IsNullOrEmpty(ID))
            {
                FrmEditCustomer dlg = new FrmEditCustomer();
                dlg.ID = ID;
                //dlg.IDList = IDList;
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    BindData();
                }
            }
        }

        private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
        {
            btnAddNew_Click(null, null);
        }

        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            string where = GetSearchSql();
            this.winGridViewPager1.AllToExport = FindToDataTable(where, this.winGridViewPager1.PagerInfo);
        }

        private void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
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
            PagerHelper helper = new PagerHelper("All_Customer", "*", "LastUpdated", pagerInfo.PageSize, pagerInfo.CurrenetPageIndex, true, where);
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
            condition.AddCondition("Number", this.txtNumber.Text, SqlOperator.Like)
                .AddCondition("Name", this.txtName.Text, SqlOperator.Like)
                .AddCondition("Type", this.cmbType.Text, SqlOperator.Like)
                .AddCondition("Area", this.cmbArea.Text, SqlOperator.Like)
                .AddCondition("Address", this.txtAddress.Text, SqlOperator.Like)
                .AddCondition("Company", this.txtCompany.Text, SqlOperator.Like)
                .AddCondition("Note", this.txtNote.Text, SqlOperator.Like)
                .AddCondition("Telephone1", this.txtTelephone.Text, SqlOperator.Like, true, "Telephone")
                .AddCondition("Telephone2", this.txtTelephone.Text, SqlOperator.Like, true, "Telephone")
                .AddCondition("Telephone3", this.txtTelephone.Text, SqlOperator.Like, true, "Telephone")
                .AddCondition("Telephone4", this.txtTelephone.Text, SqlOperator.Like, true, "Telephone")
                .AddCondition("Telephone5", this.txtTelephone.Text, SqlOperator.Like, true, "Telephone");

            if (chkUseDate.Checked)
            {
                condition.AddCondition("CreateDate", dateTimePicker1.Value.ToString("yyyy-MM-dd"), SqlOperator.MoreThanOrEqual, true)
                    .AddCondition("CreateDate", dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd"), SqlOperator.LessThanOrEqual, true);

            }
            string where = condition.BuildConditionSql().Replace("Where", "");
            return where;
        } 
        #endregion

        private void BindData()
        {
            #region 添加别名解析
            //DisplayColumns与显示的字段名或者实体属性一致，大小写不敏感，顺序代表显示顺序，用逗号或者|分开
            this.winGridViewPager1.DisplayColumns = "Number,NAME,type,Area,Company,Address,Telephone1,Telephone2,Telephone3,Telephone4,Telephone5,CreateDate,Note,LastUpdated";
            this.winGridViewPager1.AddColumnAlias("ID", "编号");
            this.winGridViewPager1.AddColumnAlias("Number", "客户编号");
            this.winGridViewPager1.AddColumnAlias("Name", "客户名称");
            this.winGridViewPager1.AddColumnAlias("Type", "客户类型");
            this.winGridViewPager1.AddColumnAlias("Area", "客户地区");
            this.winGridViewPager1.AddColumnAlias("Company", "客户单位");
            this.winGridViewPager1.AddColumnAlias("Address", "客户地址");
            this.winGridViewPager1.AddColumnAlias("Telephone1", "电话1");
            this.winGridViewPager1.AddColumnAlias("Telephone2", "电话2");
            this.winGridViewPager1.AddColumnAlias("Telephone3", "电话3");
            this.winGridViewPager1.AddColumnAlias("Telephone4", "电话4");
            this.winGridViewPager1.AddColumnAlias("Telephone5", "电话5");
            this.winGridViewPager1.AddColumnAlias("CreateDate", "开户日期");
            this.winGridViewPager1.AddColumnAlias("Shop_ID", "分店ID");
            this.winGridViewPager1.AddColumnAlias("Note", "备注");
            this.winGridViewPager1.AddColumnAlias("LastUpdated", "更新日期");

            #endregion

            string where = GetSearchSql();
            DataTable dt = FindToDataTable(where, this.winGridViewPager1.PagerInfo);
            this.winGridViewPager1.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void SearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }

        #region 其他菜单操作
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            FrmEditCustomer dlg = new FrmEditCustomer();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindData();
            }
        }

        private void menu_New_Click(object sender, EventArgs e)
        {
            btnAddNew_Click(null, null);
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }

        private void btnNoOrderCustomer_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void menu_NoOrderCustomer_Click(object sender, EventArgs e)
        {
            btnNoOrderCustomer_Click(null, null);
        }

        private void menu_NewOrder_Click(object sender, EventArgs e)
        {

        }

        private void menu_BuyTicket_Click(object sender, EventArgs e)
        {

        } 
        #endregion

        private void btnGetCheckedRows_Click(object sender, EventArgs e)
        {
            List<int> list = this.winGridViewPager1.GetCheckedRows();
            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (int rowindex in list)
            {
                sb.Append(rowindex.ToString() + ",");
                if (i++ % 10 == 0)
                {
                    sb.Append("\r\n");
                }
            }
            MessageDxUtil.ShowTips(sb.ToString().Trim(','));
        }

        private void btnSecondPager_Click(object sender, EventArgs e)
        {
            FrmCustomerPager dlg = new FrmCustomerPager();
            dlg.Show();
        }
    }
}
