using JCodes.Framework.AddIn.Other;
using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.UI.Basic
{
    public partial class FrmSystemType : BaseForm
    {
        public FrmSystemType()
        {
            InitializeComponent();

            InitDictItem();

            this.winGridView1.OnEditSelected += new EventHandler(winGridView1_OnEditSelected);
            this.winGridView1.OnDeleteSelected += new EventHandler(winGridView1_OnDeleteSelected);
            this.winGridView1.OnRefresh += new EventHandler(winGridView1_OnRefresh);
            this.winGridView1.OnAddNew += new EventHandler(winGridView1_OnAddNew);
            this.winGridView1.AppendedMenu = this.contextMenuStrip1;
            this.winGridView1.BestFitColumnWith = false;
            this.winGridView1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
        }

        /// <summary>
        /// 编写初始化窗体的实现，可以用于刷新
        /// </summary>
        public override void FormOnLoad()
        {
            BindData();
        }

        /// <summary>
        /// 初始化字典列表内容
        /// </summary>
        private void InitDictItem()
        {
            //初始化代码
        }

        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.winGridView1.gridView1.Columns.Count > 0 && this.winGridView1.gridView1.RowCount > 0)
            {
                this.winGridView1.gridView1.Columns["OID"].Width = 100;
                this.winGridView1.gridView1.Columns["Name"].Width = 100;
                this.winGridView1.gridView1.Columns["CustomID"].Width = 100;
                this.winGridView1.gridView1.Columns["Authorize"].Width = 100;
                this.winGridView1.gridView1.Columns["Note"].Width = 200;
            }
        }

        private void winGridView1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
        }

        private void winGridView1_OnDeleteSelected(object sender, EventArgs e)
        {
            if (MessageDxUtil.ShowYesNoAndTips("您确定删除选定的记录么？") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridView1.GridView1.GetSelectedRows();
            if (rowSelected != null)
            {
                foreach (int iRow in rowSelected)
                {
                    string ID = this.winGridView1.GridView1.GetRowCellDisplayText(iRow, "OID");
                    BLLFactory<SystemType>.Instance.Delete(ID);
                }
                BindData();
            }
        }

        private void winGridView1_OnEditSelected(object sender, EventArgs e)
        {
            int[] rowSelected = this.winGridView1.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                FrmEditSystemType dlg = new FrmEditSystemType();
                dlg.ID = this.winGridView1.GridView1.GetRowCellDisplayText(iRow, "OID");
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    BindData();
                }

                break;
            }
        }

        private void winGridView1_OnAddNew(object sender, EventArgs e)
        {
            FrmEditSystemType dlg = new FrmEditSystemType();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindData();
            }
        }

        private void BindData()
        {
            this.winGridView1.DisplayColumns = "OID,Name,CustomID,Authorize,Note";
            #region 添加别名解析
            this.winGridView1.AddColumnAlias("OID", "系统标识");
            this.winGridView1.AddColumnAlias("Name", "系统名称");
            this.winGridView1.AddColumnAlias("CustomID", "客户编码");
            this.winGridView1.AddColumnAlias("Authorize", "授权编码");
            this.winGridView1.AddColumnAlias("Note", "备注");
            #endregion

            this.winGridView1.DataSource = BLLFactory<SystemType>.Instance.GetAllToDataTable();
            this.winGridView1.PrintTitle = Portal.gc.gAppUnit + " -- " + "系统类型信息报表";
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            winGridView1_OnAddNew(null, null);
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            winGridView1_OnEditSelected(null, null);
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            winGridView1_OnDeleteSelected(null, null);
        }
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
