using JCodes.Framework.AddIn.Other;
using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.CommonControl.Pager.Others;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.UI.Basic
{ 
    /// <summary>
    /// 用户登录日志信息
    /// </summary>	
    public partial class FrmLoginLog : BaseDock
    {
        public FrmLoginLog()
        {
            InitializeComponent();

            this.winGridViewPager1.OnPageChanged += new EventHandler(winGridViewPager1_OnPageChanged);
            this.winGridViewPager1.OnStartExport += new EventHandler(winGridViewPager1_OnStartExport);
            this.winGridViewPager1.OnDeleteSelected += new EventHandler(winGridViewPager1_OnDeleteSelected);
            this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);
            this.winGridViewPager1.ShowLineNumber = true;
            this.winGridViewPager1.AppendedMenu = this.contextMenuStrip1;
            this.winGridViewPager1.BestFitColumnWith = false;
            this.winGridViewPager1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
            this.winGridViewPager1.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView1_RowCellStyle);
            this.winGridViewPager1.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);

            InitDictItem();
            //关联回车键进行查询
            foreach (Control control in this.layoutControl1.Controls)
            {
                control.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            }
        }

        private void InitDictItem()
        {
            List<SystemTypeInfo> systemList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo info in systemList)
            {
                this.txtSystemType.Properties.Items.Add(new CListItem(info.Name, info.OID));
            }
            this.txtSystemType.Properties.Items.Add(new CListItem("所有", ""));
        }

        void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "LastUpdated")
            {
                e.Column.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            }
        }

        /// <summary>
        /// 对显示的字段内容进行转义
        /// </summary>
        void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.ColumnType == typeof(DateTime))
            {
                string columnName = e.Column.FieldName;                
                if (e.Value != null && Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
                {
                    e.DisplayText = "";
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
                winGridViewPager1.gridView1.SetGridColumWidth("ID", 60);
                winGridViewPager1.gridView1.SetGridColumWidth("User_ID", 80);
                winGridViewPager1.gridView1.SetGridColumWidth("LoginName", 60);
                winGridViewPager1.gridView1.SetGridColumWidth("Company_ID", 80);
                winGridViewPager1.gridView1.SetGridColumWidth("Note", 200);
                winGridViewPager1.gridView1.SetGridColumWidth("LastUpdated", 150);
            }
        }

        /// <summary>
        /// 编写初始化窗体的实现，可以用于刷新
        /// </summary>
        public override void FormOnLoad()
        {
            InitTree();

            Init_Function();
        }

        private void Init_Function()
        {
            btnSearch.Enabled = HasFunction("LoginLog/search");
            btnDeleteMonthLog.Enabled  = HasFunction("LoginLog/del30");
            btnExport.Enabled = HasFunction("LoginLog/Export");
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
            if (!HasFunction("LoginLog/del"))
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
                string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");
                BLLFactory<LoginLog>.Instance.DeleteByUser(ID, LoginUserInfo.ID.ToString());
            }
            BindData();
        }

        
        /// <summary>
        /// 分页控件全部导出操作前的操作
        /// </summary> 
        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            if (!HasFunction("LoginLog/Export"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = 1;
            pagerInfo.PageSize = int.MaxValue;
            this.winGridViewPager1.AllToExport = BLLFactory<LoginLog>.Instance.GetAllToDataTable(pagerInfo);
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
            SearchCondition condition = new SearchCondition();
            condition.AddCondition("LoginName", this.txtLoginName.Text, SqlOperator.Like);
            condition.AddCondition("FullName", this.txtRealName.Text, SqlOperator.Like);
            condition.AddCondition("Note", this.txtNote.Text, SqlOperator.Like);
            condition.AddCondition("IPAddress", this.txtIPAddress.Text, SqlOperator.Like);
            condition.AddCondition("MacAddress", this.txtMacAddress.Text, SqlOperator.Like);

            if (dateTimePicker1.Text.Length > 0)
            {
                condition.AddCondition("LastUpdated", Convert.ToDateTime(dateTimePicker1.DateTime.ToString("yyyy-MM-dd")), SqlOperator.MoreThanOrEqual);
            }
            if (dateTimePicker2.Text.Length > 0)
            {
                condition.AddCondition("LastUpdated", Convert.ToDateTime(dateTimePicker2.DateTime.AddDays(1).ToString("yyyy-MM-dd")), SqlOperator.LessThanOrEqual);
            }

            string systemType = this.txtSystemType.GetComboBoxStrValue();
            if (!string.IsNullOrEmpty(systemType))
            {
                condition.AddCondition("SystemType_ID", systemType, SqlOperator.Equal);
            }

            //如果是公司管理员，增加公司标识
            if (Portal.gc.UserInRole(RoleInfo.CompanyAdminName))
            {
                condition.AddCondition("Company_ID", Portal.gc.UserInfo.Company_ID, SqlOperator.Equal);
            }

            string where = condition.BuildConditionSql().Replace("Where", "");
            //如果是单击节点得到的条件，则使用树列表的，否则使用查询条件的
            if (!string.IsNullOrEmpty(treeConditionSql))
            {
                where = treeConditionSql;
            }
            // 增加系统可以访问的公司部门的权限
            where += " and (Company_ID " + canOptCompanyID + ")";

            return where;
        }
        
        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindData()
        {
            #region 添加别名解析
            this.winGridViewPager1.DisplayColumns = "ID,User_ID,LoginName,FullName,Company_ID,CompanyName,Note,IPAddress,MacAddress,SystemType_ID,LastUpdated";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<LoginLog>.Instance.GetColumnNameAlias();//字段列显示名称转义
            //this.winGridViewPager1.AddColumnAlias("ID", "编号");
            //this.winGridViewPager1.AddColumnAlias("User_ID", "登录用户ID");
            //this.winGridViewPager1.AddColumnAlias("LoginName", "登录名");
            //this.winGridViewPager1.AddColumnAlias("FullName", "真实名称");
            //this.winGridViewPager1.AddColumnAlias("Note", "日志描述");
            //this.winGridViewPager1.AddColumnAlias("IPAddress", "IP地址");
            //this.winGridViewPager1.AddColumnAlias("MacAddress", "Mac地址");
            //this.winGridViewPager1.AddColumnAlias("LastUpdated", "记录日期");
            //this.winGridViewPager1.AddColumnAlias("SystemType_ID", "系统类型");

            #endregion

            string where = GetConditionSql();
            List<LoginLogInfo> list = BLLFactory<LoginLog>.Instance.FindWithPager(where, this.winGridViewPager1.PagerInfo);
            this.winGridViewPager1.DataSource = new SortableBindingList<LoginLogInfo>(list);
            this.winGridViewPager1.PrintTitle = "用户登录日志信息报表";
        }
        
        /// <summary>
        /// 查询数据操作
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.treeConditionSql = null;
            BindData();
        }

        /// <summary>
        /// 提供给控件回车执行查询的操作
        /// </summary>
        private void SearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!HasFunction("LoginLog/search"))
                {
                    MessageDxUtil.ShowError(Const.NoAuthMsg);
                    return;
                }

                btnSearch_Click(null, null);
            }
        }

        private void btnDeleteMonthLog_Click(object sender, EventArgs e)
        {
            if (MessageDxUtil.ShowYesNoAndTips("您确定删除一个月前的日志记录么？") == DialogResult.No)
            {
                return;
            }

            try
            {
                BLLFactory<LoginLog>.Instance.DeleteMonthLog();
                BindData();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmLoginLog));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        private string moduleName = "用户登录日志信息";
        /// <summary>
        /// 导出Excel的操作
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string file = FileDialogHelper.SaveExcel(string.Format("{0}.xls", moduleName));
            if (!string.IsNullOrEmpty(file))
            {
                string where = GetConditionSql();
                List<LoginLogInfo> list = BLLFactory<LoginLog>.Instance.Find(where);
                DataTable dtNew = DataTableHelper.CreateTable("序号|int,登录用户ID,登录名,真实名称,所属公司ID,所属公司名称,日志描述,IP地址,Mac地址,更新时间,系统编号");
                DataRow dr;
                int j = 1;
                for (int i = 0; i < list.Count; i++)
                {
                    dr = dtNew.NewRow();
                    dr["序号"] = j++;
                    dr["登录用户ID"] = list[i].User_ID;
                    dr["登录名"] = list[i].LoginName;
                    dr["真实名称"] = list[i].FullName;
                    dr["所属公司ID"] = list[i].Company_ID;
                    dr["所属公司名称"] = list[i].CompanyName;
                    dr["日志描述"] = list[i].Note;
                    dr["IP地址"] = list[i].IPAddress;
                    dr["Mac地址"] = list[i].MacAddress;
                    dr["更新时间"] = list[i].LastUpdated;
                    dr["系统编号"] = list[i].SystemType_ID;
                    dtNew.Rows.Add(dr);
                }

                try
                {
                    string error = "";
                    AsposeExcelTools.DataTableToExcel2(dtNew, file, out error);
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageDxUtil.ShowError(string.Format("导出Excel出现错误：{0}", error));
                    }
                    else
                    {
                        if (MessageDxUtil.ShowYesNoAndTips("导出成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(file);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmLoginLog));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }

        private void InitTree()
        {
            treeView1.Nodes.Clear();
            treeView1.BeginUpdate();
            Cursor.Current = Cursors.WaitCursor;

            List<OUInfo> list = Portal.gc.GetMyTopGroup();
            foreach (OUInfo groupInfo in list)
            {
                //不显示删除的机构
                if (groupInfo != null && !groupInfo.Deleted)
                {
                    TreeNode topnode = new TreeNode();
                    topnode.Text = groupInfo.Name;
                    topnode.Name = groupInfo.ID.ToString();
                    topnode.Tag = string.Format("Company_ID = {0} and SystemType_ID = '{1}'", groupInfo.ID, Portal.gc.SystemType);
                    topnode.ImageIndex = Portal.gc.GetImageIndex(groupInfo.Category);
                    topnode.SelectedImageIndex = Portal.gc.GetImageIndex(groupInfo.Category);
                    this.treeView1.Nodes.Add(topnode);

                    List<OUNodeInfo> sublist = BLLFactory<OU>.Instance.GetTreeByID(groupInfo.ID);
                    AddOUNode(sublist, topnode);
                }
            }

            Cursor.Current = Cursors.Default;
            treeView1.EndUpdate();
            this.treeView1.ExpandAll();
        }

        private void AddOUNode(List<OUNodeInfo> list, TreeNode parentNode)
        {
            foreach (OUNodeInfo ouInfo in list)
            {
                TreeNode ouNode = new TreeNode();
                ouNode.Text = ouInfo.Name;
                ouNode.Name = ouInfo.ID.ToString();
                ouNode.Tag = string.Format("Company_ID = {0} and SystemType_ID = '{1}'", ouInfo.ID, Portal.gc.SystemType);
                if (ouInfo.Deleted)
                {
                    ouNode.ForeColor = Color.Red;
                    continue;//跳过不显示
                }
                ouNode.ImageIndex = Portal.gc.GetImageIndex(ouInfo.Category);
                ouNode.SelectedImageIndex = Portal.gc.GetImageIndex(ouInfo.Category);
                parentNode.Nodes.Add(ouNode);
            }
        }

        string treeConditionSql = "";
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!HasFunction("LoginLog/search"))
            {
                return;
            }

            if (e.Node != null && e.Node.Tag != null)
            {
                treeConditionSql = e.Node.Tag.ToString();
                BindData();
            }
            else
            {
                treeConditionSql = "";
                BindData();
            }
        }

        private void menuTree_ExpandAll_Click(object sender, EventArgs e)
        {
            this.treeView1.ExpandAll();
        }

        private void menuTree_Clapase_Click(object sender, EventArgs e)
        {
            this.treeView1.CollapseAll();
        }

        private void menuTree_Refresh_Click(object sender, EventArgs e)
        {
            InitTree();
        }
    }
}
