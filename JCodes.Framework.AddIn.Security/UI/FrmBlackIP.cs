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
using JCodes.Framework.AddIn.Basic;

namespace JCodes.Framework.AddIn.Security
{
    /// <summary>
    /// 登陆系统的黑白名单列表
    /// </summary>	
    public partial class FrmBlackIP : BaseDock
    {
        public FrmBlackIP()
        {
            InitializeComponent();
            InitDictItem();
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

        void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //AuthorizeType,Forbid
            if (e.Column.FieldName == "AuthorizeType")
            {
                Color color = Color.White;
                if (e.CellValue.ToString().ToInt16() == (short)AuthorizeType.黑名单)
                {
                    e.Appearance.BackColor = Color.Black;
                    e.Appearance.BackColor2 = Color.LightCyan;
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                }
            }
            if (e.Column.FieldName == "IsForbid")
            {
                Color color = Color.White;
                if (e.CellValue.ToString().ToInt16() == (short)IsForbid.是)
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.BackColor2 = Color.LightCyan;
                }
            }
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
            else if (e.Column.FieldName == "AuthorizeType")
            {
                if (e.Value != null)
                {
                    e.DisplayText = EnumHelper.GetMemberName<AuthorizeType>(e.Value);
                }
            }
            else if (e.Column.FieldName == "IsForbid")
            {
                if (e.Value != null)
                {
                    e.DisplayText = EnumHelper.GetMemberName<IsForbid>(e.Value);
                }
            }
            else if (string.Equals(e.Column.FieldName, "EditorId", StringComparison.CurrentCultureIgnoreCase))
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()) && Portal.gc.AllUserInfo.ContainsKey(e.Value.ToString().ToInt32()))
                {
                    e.DisplayText = Portal.gc.AllUserInfo[e.Value.ToString().ToInt32()];
                }
            }
            else if (string.Equals(e.Column.FieldName, "CreatorId", StringComparison.CurrentCultureIgnoreCase))
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()) && Portal.gc.AllUserInfo.ContainsKey(e.Value.ToString().ToInt32()))
                {
                    e.DisplayText = Portal.gc.AllUserInfo[e.Value.ToString().ToInt32()];
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
                winGridViewPager1.gridView1.SetGridColumWidth("Remark", 300);
                winGridViewPager1.gridView1.SetGridColumWidth("AuthorizeType", 70);
                winGridViewPager1.gridView1.SetGridColumWidth("IsForbid", 70);
                winGridViewPager1.gridView1.SetGridColumWidth("CreatorId", 70); // 创建日期
                winGridViewPager1.gridView1.SetGridColumWidth("CreatorTime", 130); // 创建日期
                winGridViewPager1.gridView1.SetGridColumWidth("EditorId", 70);
                winGridViewPager1.gridView1.SetGridColumWidth("LastUpdateTime", 130); // 最后更新日期
                winGridViewPager1.gridView1.SetGridColumWidth("Seq", 130); // 最后修改密码时间
                winGridViewPager1.gridView1.SetGridColumWidth("StartTime", 130); // 创建日期
                winGridViewPager1.gridView1.SetGridColumWidth("EndTime", 130); // 创建日期
            }
        }

        /// <summary>
        /// 编写初始化窗体的实现，可以用于刷新
        /// </summary>
        public override void  FormOnLoad()
        {   
            BindData();

            Init_Function();
        }

        void Init_Function()
        {
            btnSearch.Enabled = HasFunction("BlackIP/search");
            btnAddNew.Enabled = HasFunction("BlackIP/add");
        }

        /// <summary>
        /// 初始化字典列表内容
        /// </summary>
        private void InitDictItem()
        {
            //初始化分类
            Dictionary<string, object> dictEnum = EnumHelper.GetMemberKeyValue<AuthorizeType>();
            this.txtAuthorizeType.Properties.Items.Clear();
            foreach (string item in dictEnum.Keys)
            {
                // 20170901 wjm 调整key 和value的顺序
                this.txtAuthorizeType.Properties.Items.Add(new CListItem(dictEnum[item].ToString(), item));
            }
        }
        
        /// <summary>
        /// 分页控件刷新操作
        /// </summary>
        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
        }
        
        /// <summary>
        /// 分页控件删除操作
        /// </summary>
        private void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
        {
            if (!HasFunction("Basic/BlackIP/BlackIPDel"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            if (MessageDxUtil.ShowYesNoAndTips("您确定删除选定的记录么？") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                Int32 Id = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "Id").ToInt32();
                BLLFactory<BlackIP>.Instance.DeleteByUser(Id, LoginUserInfo.Id);
                BLLFactory<BlackIP>.Instance.RemoveUserByBlackId(Id);
            }
             
            BindData();
        }
        
        /// <summary>
        /// 分页控件编辑项操作
        /// </summary>
        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            if (!HasFunction("Basic/BlackIP/BlackIPEdit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            Int32 Id = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("Id").ToInt32();
            List<Int32> IdList = new List<Int32>();
            for (int i = 0; i < this.winGridViewPager1.gridView1.RowCount; i++)
            {
                Int32 intTemp = this.winGridViewPager1.GridView1.GetRowCellDisplayText(i, "Id").ToInt32();
                IdList.Add(intTemp);
            }

            if (Id > 0)
            {
                FrmEditBlackIP dlg = new FrmEditBlackIP();
                dlg.Id = Id;
                dlg.IdList = IdList;
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
            if (!HasFunction("Basic/BlackIP/BlackIPAdd"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            btnAddNew_Click(null, null);
        }
        
        /// <summary>
        /// 分页控件全部导出操作前的操作
        /// </summary> 
        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            string where = GetConditionSql();
            this.winGridViewPager1.AllToExport = BLLFactory<BlackIP>.Instance.FindToDataTable(where);
         }

        /// <summary>
        /// 分页控件翻页的操作
        /// </summary> 
        private void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
        {
            BindData();
        }
 
        
        /// <summary>
        /// 根据查询条件构造查询语句
        /// </summary> 
        private string GetConditionSql()
        {
            //如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
            SearchCondition condition = new SearchCondition();
            condition.AddCondition("Name", this.txtName.Text.Trim(), SqlOperator.Like);

            if (this.txtAuthorizeType.Text.Length > 0)
            {
                condition.AddCondition("AuthorizeType", this.txtAuthorizeType.GetComboBoxStrValue().ToInt32(), SqlOperator.Equal); //数值类型
            }
            if (this.txtForbid.Checked)
            {
                condition.AddCondition("IsForbid", (short)IsForbid.是, SqlOperator.Equal);//数值类型
            }

            string where = condition.BuildConditionSql().Replace("Where", "");
            return where;
        }
        
        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindData()
        {
            var columnNameAlias = BLLFactory<BlackIP>.Instance.GetColumnNameAlias();//字段列显示名称转义
            this.winGridViewPager1.DisplayColumns = columnNameAlias.ToDiplayKeyString();
            this.winGridViewPager1.ColumnNameAlias = columnNameAlias;//字段列显示名称转义
            string where = GetConditionSql();
            List<BlackIPInfo> list = BLLFactory<BlackIP>.Instance.Find(where);
            this.winGridViewPager1.DataSource = new SortableBindingList<BlackIPInfo>(list);
            this.winGridViewPager1.PrintTitle = "登陆系统的黑白名单列表报表";
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
            FrmEditBlackIP dlg = new FrmEditBlackIP();
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

        private void txtForbid_CheckedChanged(object sender, EventArgs e)
        {
            BindData();
        }        					 						 						 						 						 						 						 						 						 						 						 	 						 	 
    }
}
