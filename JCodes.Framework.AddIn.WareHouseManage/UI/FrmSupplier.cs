using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JCodes.Framework.BLL;

namespace JCodes.Framework.AddIn.WareHouseManage
{
    public partial class FrmSupplier : BaseDock
    {
        public FrmSupplier()
        {
            InitializeComponent();
        }

        private void FrmSupplier_Load(object sender, EventArgs e)
        {
            this.winGridViewPager1.OnPageChanged += new EventHandler(winGridViewPager1_OnPageChanged);
            this.winGridViewPager1.OnStartExport += new EventHandler(winGridViewPager1_OnStartExport);
            this.winGridViewPager1.OnEditSelected += new EventHandler(winGridViewPager1_OnEditSelected);
            this.winGridViewPager1.OnDeleteSelected += new EventHandler(winGridViewPager1_OnDeleteSelected);
            this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);
            this.winGridViewPager1.OnAddNew += new EventHandler(winGridViewPager1_OnAddNew);
            this.winGridViewPager1.AppendedMenu = this.contextMenuStrip1;
            this.winGridViewPager1.BestFitColumnWith = false;//使用固定列宽的做法，True为自动适应宽度
            this.winGridViewPager1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
            this.winGridViewPager1.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);
            BindData();
            Init_Function();
        }

        void Init_Function()
        {
            tsbNew.Enabled = HasFunction("Supplier/add");
            tsbEdit.Enabled = HasFunction("Supplier/Edit");
            tsbDelete.Enabled = HasFunction("Supplier/Del");
        }

        /// <summary>
        /// 对显示的字段内容进行转义
        /// </summary>
        void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            string columnName = e.Column.FieldName;
            if (e.Column.ColumnType == typeof(DateTime))
            {
                if (e.Value != null)
                {
                    if (e.Value == DBNull.Value || Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
                    {
                        e.DisplayText = "";
                    }
                    else
                    {
                        e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm");//yyyy-MM-dd
                    }
                }
            }
        }

        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.winGridViewPager1.gridView1.Columns.Count > 0 && this.winGridViewPager1.gridView1.RowCount > 0)
            {
                //统一设置100宽度
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in this.winGridViewPager1.gridView1.Columns)
                {
                    column.Width = 100;
                }

                //可特殊设置特别的宽度
                winGridViewPager1.gridView1.SetGridColumWidth("Name", 200);
                winGridViewPager1.gridView1.SetGridColumWidth("Address", 200);
                winGridViewPager1.gridView1.SetGridColumWidth("Note", 200);
            }
        }

        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
        }

        private void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
        {
            if (!HasFunction("Supplier/Del"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            if (MessageDxUtil.ShowYesNoAndTips("您确定删除选定的记录么？") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            if (rowSelected != null)
            {
                foreach (int iRow in rowSelected)
                {
                    string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");
                    BLLFactory<Supplier>.Instance.DeleteByUser(ID, LoginUserInfo.ID);
                }
                BindData();
            }
        }

        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            if (!HasFunction("Supplier/Edit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                FrmEditSupplier dlg = new FrmEditSupplier();
                dlg.ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");
                dlg.InitFunction(this.LoginUserInfo, this.FunctionDict);//初始化权限控制信息
                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);

                if (DialogResult.OK == dlg.ShowDialog())
                {
                    BindData();
                }

                break;
            }
        }

        private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
        {
            if (!HasFunction("Supplier/add"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            FrmEditSupplier dlg = new FrmEditSupplier();

            dlg.InitFunction(this.LoginUserInfo, this.FunctionDict);//初始化权限控制信息
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindData();
            }
        }

        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            BindData();
        }

        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = 1;
            pagerInfo.PageSize = int.MaxValue;
            this.winGridViewPager1.AllToExport = BLLFactory<Supplier>.Instance.GetAllToDataTable(pagerInfo);
        }

        private void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
        {
            BindData();
        }


        private void BindData()
        {
            this.winGridViewPager1.DisplayColumns = "Code,Name,Phone,Address,Note";
            #region 添加别名解析
            this.winGridViewPager1.AddColumnAlias("ID", "编号");
            this.winGridViewPager1.AddColumnAlias("Code", "供应商编码");
            this.winGridViewPager1.AddColumnAlias("Name", "供应商名称");
            this.winGridViewPager1.AddColumnAlias("Phone", "供应商电话");
            this.winGridViewPager1.AddColumnAlias("Address", "供应商地址");
            this.winGridViewPager1.AddColumnAlias("Note", "备注");
            #endregion

            this.winGridViewPager1.DataSource = BLLFactory<Supplier>.Instance.GetAllToDataTable(this.winGridViewPager1.PagerInfo);
            this.winGridViewPager1.PrintTitle = this.AppInfo.AppUnit + " -- " + "供应商信息报表";
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnAddNew(null, null);
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnEditSelected(null, null);
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnDeleteSelected(null, null);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
