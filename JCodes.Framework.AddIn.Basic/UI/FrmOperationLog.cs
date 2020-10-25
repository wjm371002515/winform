using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.CommonControl.Pager.Others;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Common.Winform;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.AddIn.Basic
{
    /// <summary>
    /// 用户关键操作记录
    /// </summary>	
    public partial class FrmOperationLog : BaseDock
    {
        public FrmOperationLog()
        {
            InitializeComponent();

            InitDictItem();

            this.winGridViewPager1.OnPageChanged += new EventHandler(winGridViewPager1_OnPageChanged);
            this.winGridViewPager1.OnStartExport += new EventHandler(winGridViewPager1_OnStartExport);
            this.winGridViewPager1.OnEditSelected += new EventHandler(winGridViewPager1_OnEditSelected);
            this.winGridViewPager1.OnDeleteSelected += new EventHandler(winGridViewPager1_OnDeleteSelected);
            this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);
            this.winGridViewPager1.AppendedMenu = this.contextMenuStrip1;
            this.winGridViewPager1.ShowLineNumber = true;
            this.winGridViewPager1.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置
			this.winGridViewPager1.gridView1.DataSourceChanged +=new EventHandler(gridView1_DataSourceChanged);
            this.winGridViewPager1.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);

            //关联回车键进行查询
            foreach (Control control in this.layoutControl1.Controls)
            {
                control.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
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
                winGridViewPager1.gridView1.SetGridColumWidth("Mac", 150);
                winGridViewPager1.gridView1.SetGridColumWidth("CreatorTime", 150);
                winGridViewPager1.gridView1.SetGridColumWidth("Remark", 200);
            }
        }

        /// <summary>
        /// 编写初始化窗体的实现，可以用于刷新
        /// </summary>
        public override void  FormOnLoad()
        {
            InitTree();
            BindData();
            Init_Function();
        }

        void Init_Function()
        {
            btnSearch.Enabled = HasFunction("OperationLog/search");
            btnExport.Enabled = HasFunction("OperationLog/Export");
            btnSetTableLog.Enabled = HasFunction("OperationLog/OperationLogSet");
        }
        
        /// <summary>
        /// 初始化字典列表内容
        /// </summary>
        private void InitDictItem()
        {
			//初始化代码
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
            if (!HasFunction("OperationLog/del"))
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
                string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "Id");
                BLLFactory<OperationLog>.Instance.DeleteByUser(ID, LoginUserInfo.Id);
            }
             
            BindData();
        }
        
        /// <summary>
        /// 分页控件编辑项操作
        /// </summary>
        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            if (!HasFunction("OperationLog/edit"))
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
                FrmEditOperationLog dlg = new FrmEditOperationLog();
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
        /// 分页控件全部导出操作前的操作
        /// </summary> 
        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            if (!HasFunction("OperationLog/Export"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            string where = GetConditionSql();
            this.winGridViewPager1.AllToExport = BLLFactory<OperationLog>.Instance.FindToDataTable(where);
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
            condition.AddCondition("LoginName", this.txtLoginName.Text.Trim(), SqlOperator.Like);
            condition.AddCondition("TableName", this.txtTableName.Text.Trim(), SqlOperator.Like);
            condition.AddCondition("OperationType", this.txtOperationType.Text.Trim(), SqlOperator.Like);
            condition.AddDateCondition("CreatorTime", this.txtCreateTime1, this.txtCreateTime2); //日期类型

            //如果是公司管理员，增加公司标识
            /*if (Portal.gc.UserInRole(RoleInfo.CompanyAdminName))
            {
                condition.AddCondition("CompanyId", Portal.gc.UserInfo.CompanyId, SqlOperator.Equal);
            }*/

            string where = condition.BuildConditionSql().Replace("Where", "");
            //如果是单击节点得到的条件，则使用树列表的，否则使用查询条件的
            if (!string.IsNullOrEmpty(treeConditionSql))
            {
                where = treeConditionSql;
            }

            // 增加系统可以访问的公司部门的权限
            where += " and (CompanyId " + canOptCompanyId + ")";
            return where;
        }
        
        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindData()
        {
            if (!HasFunction("OperationLog/search"))
            {
                return;
            }

        	//entity
            this.winGridViewPager1.DisplayColumns = "LoginName,FullName,CompanyName,TableName,OperationType,IP,Mac,CreatorTime";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<OperationLog>.Instance.GetColumnNameAlias();//字段列显示名称转义

            string where = GetConditionSql();
	            List<OperationLogInfo> list = BLLFactory<OperationLog>.Instance.FindWithPager(where, this.winGridViewPager1.PagerInfo);
            this.winGridViewPager1.DataSource = new SortableBindingList<OperationLogInfo>(list);
                this.winGridViewPager1.PrintTitle = "用户关键操作记录报表";
         }
        
        /// <summary>
        /// 查询数据操作
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            treeConditionSql = "";
            BindData();
        }
        
        /// <summary>
        /// 提供给控件回车执行查询的操作
        /// </summary>
        private void SearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!HasFunction("OperationLog/search"))
                {
                    MessageDxUtil.ShowError(Const.NoAuthMsg);
                    return;
                }

                btnSearch_Click(null, null);
            }
        }        

	 						 						 						 						 						 						 						 						 						 
        private string moduleName = "用户关键操作记录";

        /// <summary>
        /// 导出Excel的操作
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (!HasFunction("OperationLog/Export"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            string file = FileDialogHelper.SaveExcel(string.Format("{0}.xls", moduleName));
            if (!string.IsNullOrEmpty(file))
            {
                string where = GetConditionSql();
                List<OperationLogInfo> list = BLLFactory<OperationLog>.Instance.Find(where);
                DataTable dtNew = DataTableHelper.CreateTable("序号|int,登录用户ID,登录名,真实名称,所属公司ID,所属公司名称,操作表名称,操作类型,日志描述,IP地址,Mac地址,创建时间");
                DataRow dr;
                int j = 1;
                for (int i = 0; i < list.Count; i++)
                {
                    dr = dtNew.NewRow();
                    dr["序号"] = j++;
                    dr["用户Id"] = list[i].UserId;
                    dr["登录名"] = list[i].Name;
                    dr["真实名称"] = list[i].LoginName;
                    dr["公司Id"] = list[i].CompanyId;
                    dr["公司名字"] = list[i].CompanyName;
                    dr["表名"] = list[i].TableName;
                    dr["操作类型"] = list[i].OperationType;
                    dr["备注"] = list[i].Remark;
                    dr["IP地址"] = list[i].IP;
                    dr["Mac地址"] = list[i].Mac;
                    dr["创建时间"] = list[i].CreatorTime;
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
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmOperationLog));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
         }

        private void btnSetTableLog_Click(object sender, EventArgs e)
        {
            ChildWinManagement.LoadMdiForm(Portal.gc.MainDialog, typeof(FrmOperationLogSetting));
        }

        private void InitTree()
        {
            this.treeView1.BeginUpdate();
            this.treeView1.Nodes.Clear();
           
            TreeNode tableNode = new TreeNode("数据库表", 0, 0);
            this.treeView1.Nodes.Add(tableNode);
            List<string> tableList = BLLFactory<OperationLog>.Instance.GetFieldList("TableName");

            /*bool isCompanyAdmin = Portal.gc.UserInRole(RoleInfo.CompanyAdminName);
            foreach (string tablename in tableList)
            {
                TreeNode subNode = new TreeNode(tablename, 1, 1);
                subNode.Tag = string.Format("TableName='{0}' AND (Company_ID {1}) ", tablename, canOptCompanyId);
                tableNode.Nodes.Add(subNode);

                List<string> operationList = new List<string>() { "增加", "修改", "删除"};
                foreach (string operationType in operationList)
                {
                    TreeNode operationNode = new TreeNode(operationType, 2, 2);
                    operationNode.Tag = string.Format("TableName='{0}'  AND OperationType='{1}' AND (Company_ID {2}) ",
                            tablename, operationType, canOptCompanyId);
                    subNode.Nodes.Add(operationNode);
                }
            }*/

            this.treeView1.ExpandAll();
            this.treeView1.EndUpdate();
        }

        string treeConditionSql = "";
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!HasFunction("OperationLog/search"))
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
