using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.CommonControl.Pager.Others;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.TestWinForm
{
    /// <summary>
    /// 登陆系统的黑白名单列表
    /// </summary>	
    public partial class FrmConsignment : BaseDock
    {
        public FrmConsignment()
        {
            InitializeComponent();
            this.winGridViewPager1.OnStartExport += new EventHandler(winGridViewPager1_OnStartExport);
            this.winGridViewPager1.OnEditSelected += new EventHandler(winGridViewPager1_OnEditSelected);
            this.winGridViewPager1.OnAddNew += new EventHandler(winGridViewPager1_OnAddNew);
            this.winGridViewPager1.OnDeleteSelected += new EventHandler(winGridViewPager1_OnDeleteSelected);
            this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);
            this.winGridViewPager1.AppendedMenu = this.contextMenuStrip1;
            this.winGridViewPager1.ShowLineNumber = true;
            this.winGridViewPager1.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置
			this.winGridViewPager1.gridView1.DataSourceChanged +=new EventHandler(gridView1_DataSourceChanged);
            this.winGridViewPager1.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);
            this.winGridViewPager1.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView1_RowCellStyle);
            //关联回车键进行查询
            foreach (Control control in this.layoutControl1.Controls)
            {
                control.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            }
        }

        /// <summary>
        /// 编辑行颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //AuthorizeType,Forbid
            if (e.Column.FieldName == "EnableStatus")
            {
                Color color = Color.White;
                if (e.CellValue.ToString().ToInt16() == (short)EnableStatus.启用)
                {
                    e.Appearance.BackColor = Color.Green;
                    e.Appearance.BackColor2 = Color.LightCyan;
                }
                else
                {
                    e.Appearance.BackColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// 内容展示修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            else if (e.Column.FieldName == "EnableStatus")
            {
                if (e.Value != null)
                {
                    e.DisplayText = EnumHelper.GetMemberName<EnableStatus>(e.Value);
                }
            }
        }
        
        /// <summary>
        /// 绑定数据后，分配各列的宽度
        /// </summary>
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
                //winGridViewPager1.gridView1.SetGridColumWidth("Name", 200);
            }
        }

        /// <summary>
        /// 编写初始化窗体的实现，可以用于刷新
        /// </summary>
        public override void  FormOnLoad()
        {   
            BindData();
        }
        
        /// <summary>
        /// 分页控件刷新操作
        /// </summary>
        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
        }
        
        /// <summary>
        /// 删除操作
        /// </summary>
        private void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
        {
            if (MessageDxUtil.ShowYesNoAndTips("您确定删除选定的记录么？") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();

            // 不支持多行删除
            if (rowSelected.Length > 2)
            {
                MessageDxUtil.ShowWarning("不支持多行删除");
                return;
            }

            foreach (int iRow in rowSelected)
            {
                Int32 Id = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "Id").ToInt32();
                BLLFactory<Consignment>.Instance.DeleteConsignmentById(Id);
            }
             
            BindData();
        }
        
        /// <summary>
        /// 编辑项操作
        /// </summary>
        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            Int32 Id = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("Id").ToInt32();
           
            if (Id > 0)
            {
                FrmEditConsignment dlg = new FrmEditConsignment();
                dlg.Id = Id;
                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
                
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    BindData();
                }
            }
        }        
        
        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            BindData();
        }
        
        /// <summary>
        /// 分页控件新增操作
        /// </summary>        
        private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
        {
            btnAddNew_Click(null, null);
        }
        
        /// <summary>
        /// 分页控件全部导出操作前的操作
        /// </summary> 
        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            string where = GetConditionSql();
            DataTable dt = BLLFactory<Consignment>.Instance.FindToDataTable(where);
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName == "EnableStatus")
                {
                    //修改列类型
                    col.DataType = typeof(String);
                }
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][3] = EnumHelper.GetMemberName<EnableStatus>(dt.Rows[i][3].ToString().ToInt16());
            }

            this.winGridViewPager1.AllToExport = dt;
        }

        /// <summary>
        /// 根据查询条件构造查询语句
        /// </summary> 
        private string GetConditionSql()
        {
            //如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
            SearchCondition condition = new SearchCondition();
            condition.AddCondition("StrValue", txtSearch.Text.Trim(), SqlOperator.Like, true, "SearchData");
            condition.AddCondition("Name", txtSearch.Text.Trim(), SqlOperator.Like, true, "SearchData");
            condition.AddCondition("SysValue", txtSearch.Text.Trim(), SqlOperator.Like, true, "SearchData");
            return condition.BuildConditionSql().Replace("Where", "");
        }
        
        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindData()
        {
            //entity
            this.winGridViewPager1.DisplayColumns = "Id,StrValue,Name,EnableStatus,SysValue";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<Consignment>.Instance.GetColumnNameAlias();//字段列显示名称转义
            this.winGridViewPager1.AddColumnAlias("StrValue", "小账号");
            this.winGridViewPager1.AddColumnAlias("Name", "代销名字");
            this.winGridViewPager1.AddColumnAlias("SysValue", "代销商号");
            string where = GetConditionSql();
            List<ConsignmentInfo> list = BLLFactory<Consignment>.Instance.GetAllConsignmentInfo(where);
            this.winGridViewPager1.DataSource = new SortableBindingList<ConsignmentInfo>(list);
            this.winGridViewPager1.PrintTitle = "代销数据报表";
        }
        
        /// <summary>
        /// 查询数据操作
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        
        /// <summary>
        /// 新增数据操作
        /// </summary>
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            FrmEditConsignment dlg = new FrmEditConsignment();
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
            
            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindData();
            }
        }
        
        /// <summary>
        /// 提供给控件回车执行查询的操作
        /// </summary>
        private void SearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }			 						 	 						 	 
    }
}
