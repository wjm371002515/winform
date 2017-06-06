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
using JCodes.Framework.AddIn.Other;

namespace JCodes.Framework.AddIn.UI.Security
{
    /// <summary>
    /// 登陆系统的黑白名单列表
    /// </summary>	
    public partial class FrmBlackIP : BaseDock
    {
        public FrmBlackIP()
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试2 start" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
            InitializeComponent();
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试2-1 start" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
            InitDictItem();
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试2-2 start" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
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
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试2-3 start" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
            //关联回车键进行查询
            foreach (Control control in this.layoutControl1.Controls)
            {
                control.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            }

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试2" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
        }

        void Init_Function()
        {
            if (!Portal.gc.HasFunction("BlackIP/search"))
            {
                btnSearch.Visible = false;
                btnSearch.Enabled = false;
            }

            if (!Portal.gc.HasFunction("BlackIP/add"))
            {
                btnAddNew.Visible = false;
                btnAddNew.Enabled = false;
            }

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试3" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
        }

        void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //AuthorizeType,Forbid
            if (e.Column.FieldName == "AuthorizeType")
            {
                Color color = Color.White;
                if (e.CellValue.ToString() == "0")
                {
                    e.Appearance.BackColor = Color.Black;
                    e.Appearance.BackColor2 = Color.LightCyan;
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                }
            }
            if (e.Column.FieldName == "Forbid")
            {
                Color color = Color.White;
                if (e.CellValue.ToString().ToBoolean())
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.BackColor2 = Color.LightCyan;
                }
            }
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试4" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
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
                    e.DisplayText = ((AuthrizeType)e.Value).ToString();
                }
            }
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试5" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
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
                SetGridColumWidth("Name", 200);
                SetGridColumWidth("Note", 200);
            }
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试6" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
        }

        private void SetGridColumWidth(string columnName, int width)
        {
            DevExpress.XtraGrid.Columns.GridColumn column = this.winGridViewPager1.gridView1.Columns.ColumnByFieldName(columnName);
            if (column != null)
            {
                column.Width = width;
            }
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试7" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
        }

        /// <summary>
        /// 编写初始化窗体的实现，可以用于刷新
        /// </summary>
        public override void  FormOnLoad()
        {   
            BindData();

            Init_Function();

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试8" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
        }
        
        /// <summary>
        /// 初始化字典列表内容
        /// </summary>
        private void InitDictItem()
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试9 start" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
            //初始化分类
            Dictionary<string, object> dictEnum = EnumHelper.GetMemberKeyValue<AuthrizeType>();
            this.txtAuthorizeType.Properties.Items.Clear();
            foreach (string item in dictEnum.Keys)
            {
                this.txtAuthorizeType.Properties.Items.Add(new CListItem(item, dictEnum[item].ToString()));
            }
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试9" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
        }
        
        /// <summary>
        /// 分页控件刷新操作
        /// </summary>
        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试10" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
        }
        
        /// <summary>
        /// 分页控件删除操作
        /// </summary>
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
                BLLFactory<BlackIP>.Instance.Delete(ID);
            }
             
            BindData();
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试11" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
        }
        
        /// <summary>
        /// 分页控件编辑项操作
        /// </summary>
        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            string ID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID");
            List<string> IDList = new List<string>();
            for (int i = 0; i < this.winGridViewPager1.gridView1.RowCount; i++)
            {
                string strTemp = this.winGridViewPager1.GridView1.GetRowCellDisplayText(i, "ID");
                IDList.Add(strTemp);
            }

            if (!string.IsNullOrEmpty(ID))
            {
                FrmEditBlackIP dlg = new FrmEditBlackIP();
                dlg.ID = ID;
                dlg.IDList = IDList;
                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
                
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    BindData();
                }
            }
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试12" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
        }        
        
        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            BindData();

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试13" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
        }
        
        /// <summary>
        /// 分页控件新增操作
        /// </summary>        
        private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
        {
            btnAddNew_Click(null, null);
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试14" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
        }
        
        /// <summary>
        /// 分页控件全部导出操作前的操作
        /// </summary> 
        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            string where = GetConditionSql();
            this.winGridViewPager1.AllToExport = BLLFactory<BlackIP>.Instance.FindToDataTable(where);

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试15" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
         }

        /// <summary>
        /// 分页控件翻页的操作
        /// </summary> 
        private void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
        {
            BindData();

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试16" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
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
                condition.AddCondition("AuthorizeType", this.txtAuthorizeType.GetComboBoxValue().ToInt32(), SqlOperator.Equal); //数值类型
            }
            if (this.txtForbid.Checked)
            {
                condition.AddCondition("Forbid", 1, SqlOperator.Equal);//数值类型
            }

            string where = condition.BuildConditionSql().Replace("Where", "");
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试17" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
            return where;
        }
        
        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindData()
        {
            //entity
            this.winGridViewPager1.DisplayColumns = "Name,AuthorizeType,Forbid,IPStart,IPEnd,Note,Creator,CreateTime";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<BlackIP>.Instance.GetColumnNameAlias();//字段列显示名称转义

            #region 添加别名解析

            //this.winGridViewPager1.AddColumnAlias("Name", "显示名称");
            //this.winGridViewPager1.AddColumnAlias("AuthorizeType", "授权类型");
            //this.winGridViewPager1.AddColumnAlias("Forbid", "是否禁用");
            //this.winGridViewPager1.AddColumnAlias("IPStart", "IP起始地址");
            //this.winGridViewPager1.AddColumnAlias("IPEnd", "IP结束地址");
            //this.winGridViewPager1.AddColumnAlias("Note", "备注");
            //this.winGridViewPager1.AddColumnAlias("Creator", "创建人");
            //this.winGridViewPager1.AddColumnAlias("CreateTime", "创建时间");

            #endregion

            string where = GetConditionSql();
            List<BlackIPInfo> list = BLLFactory<BlackIP>.Instance.Find(where);
            this.winGridViewPager1.DataSource = new SortableBindingList<BlackIPInfo>(list);
            this.winGridViewPager1.PrintTitle = "登陆系统的黑白名单列表报表";
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试18" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
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

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试19" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
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
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试20" + DateTime.Now.ToShortTimeString(), typeof(FrmBlackIP));
        }

        private void txtForbid_CheckedChanged(object sender, EventArgs e)
        {
            //BindData();
        }        					 						 						 						 						 						 						 						 						 						 						 	 						 	 
    }
}
