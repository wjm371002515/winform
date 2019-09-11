using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.Pager.Others;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Common;
using JCodes.Framework.CommonControl.PlugInInterface;
using JCodes.Framework.CommonControl.AdvanceSearch;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.BLL;
using JCodes.Framework.Common.Format;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.AddIn.Contact
{
    /// <summary>
    /// 通讯录联系人
    /// </summary>	
    public partial class FrmAddressCompany : BaseDock
    {
        public FrmAddressCompany()
        {
            InitializeComponent();

            InitDictItem();

            this.winGridViewPager1.OnPageChanged += new EventHandler(winGridViewPager1_OnPageChanged);
            this.winGridViewPager1.OnStartExport += new EventHandler(winGridViewPager1_OnStartExport);
            this.winGridViewPager1.OnEditSelected += new EventHandler(winGridViewPager1_OnEditSelected);
            this.winGridViewPager1.OnAddNew += new EventHandler(winGridViewPager1_OnAddNew);
            this.winGridViewPager1.OnDeleteSelected += new EventHandler(winGridViewPager1_OnDeleteSelected);
            this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);
            this.winGridViewPager1.AppendedMenu = this.contextMenuStrip1;
            this.winGridViewPager1.ShowLineNumber = true;
            this.winGridViewPager1.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置
            this.winGridViewPager1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
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
                winGridViewPager1.gridView1.SetGridColumWidth("Email", 200);
                winGridViewPager1.gridView1.SetGridColumWidth("HomeAddress", 200);
                winGridViewPager1.gridView1.SetGridColumWidth("OfficeAddress", 200);
                winGridViewPager1.gridView1.SetGridColumWidth("Note", 200);
                winGridViewPager1.gridView1.SetGridColumWidth("Name", 80);
                winGridViewPager1.gridView1.SetGridColumWidth("Sex", 60);
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

        void Init_Function()
        {
            btnSearch.Enabled = HasFunction("CommonAddress/ContactSearch");
            btnAddNew.Enabled = HasFunction("CommonAddress/ContactAdd");
            btnBatchAdd.Enabled = HasFunction("CommonAddress/ContactBatchAdd");
            btnImport.Enabled = HasFunction("CommonAddress/ContactExport");
            btnExport.Enabled = HasFunction("CommonAddress/ContactImport");
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
            if (!HasFunction("CommonAddress/ContactDel"))
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
                BLLFactory<Address>.Instance.DeleteByUser(ID, LoginUserInfo.Id);
            }

            BindData();
        }

        /// <summary>
        /// 分页控件编辑项操作
        /// </summary>
        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            if (!HasFunction("CommonAddress/ContactEdit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            Int32 Id = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID").ToInt32();
            List<Int32> IdList = new List<Int32>();
            for (int i = 0; i < this.winGridViewPager1.gridView1.RowCount; i++)
            {
                Int32 strTemp = this.winGridViewPager1.GridView1.GetRowCellDisplayText(i, "ID").ToInt32();
                IdList.Add(strTemp);
            }

            if (Id > 0)
            {
                FrmEditAddress dlg = new FrmEditAddress();
                dlg.Id = Id;
                dlg.AddressType = AddressType.公共;
                dlg.IdList = IdList;
                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
                dlg.InitFunction(LoginUserInfo, FunctionDict);//给子窗体赋值用户权限信息

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
            if (!HasFunction("CommonAddress/ContactAdd"))
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
            if (!HasFunction("CommonAddress/ContactExport"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            string where = GetConditionSql();
            this.winGridViewPager1.AllToExport = BLLFactory<Address>.Instance.FindToDataTable(where);
        }

        /// <summary>
        /// 分页控件翻页的操作
        /// </summary> 
        private void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 高级查询条件语句对象
        /// </summary>
        private SearchCondition advanceCondition;

        /// <summary>
        /// 根据查询条件构造查询语句
        /// </summary> 
        private string GetConditionSql()
        {
            //如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
            SearchCondition condition = advanceCondition;
            if (condition == null)
            {
                condition = new SearchCondition();
                condition.AddCondition("Name", this.txtName.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("Mobile", this.txtMobile.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("Email", this.txtEmail.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("HomeAddress", this.txtHomeAddress.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("OfficeAddress", this.txtOfficeAddress.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("Company", this.txtCompany.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("Note", this.txtNote.Text.Trim(), SqlOperator.Like);
            }
            condition.AddCondition("AddressType", AddressType.公共.ToString(), SqlOperator.Equal);

            string where = condition.BuildConditionSql().Replace("Where", "");
            //如果是单击节点得到的条件，则使用树列表的，否则使用查询条件的
            if (!string.IsNullOrEmpty(treeConditionSql))
            {
                where = treeConditionSql;
            }

            return where;
        }

        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindData()
        {
            if (!HasFunction("CommonAddress/ContactSearch"))
            {
                return;
            }

            //entity
            this.winGridViewPager1.DisplayColumns = "Name,Sex,Dept,Position,Birthdate,Mobile,OfficeTelephone,Email,QQ,HomeTelephone,HomeAddress,OfficeAddress,Fax,Company,Other,Note,Creator,CreateTime";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<Address>.Instance.GetColumnNameAlias();//字段列显示名称转义

            #region 添加别名解析

            //this.winGridViewPager1.AddColumnAlias("Name", "姓名");
            //this.winGridViewPager1.AddColumnAlias("Sex", "性别");
            //this.winGridViewPager1.AddColumnAlias("Birthdate", "出生日期");
            //this.winGridViewPager1.AddColumnAlias("Mobile", "手机");
            //this.winGridViewPager1.AddColumnAlias("Email", "电子邮箱");
            //this.winGridViewPager1.AddColumnAlias("Qq", "QQ");
            //this.winGridViewPager1.AddColumnAlias("HomeTelephone", "家庭电话");
            //this.winGridViewPager1.AddColumnAlias("OfficeTelephone", "办公电话");
            //this.winGridViewPager1.AddColumnAlias("HomeAddress", "家庭住址");
            //this.winGridViewPager1.AddColumnAlias("OfficeAddress", "办公地址");
            //this.winGridViewPager1.AddColumnAlias("Fax", "传真号码");
            //this.winGridViewPager1.AddColumnAlias("Company", "公司单位");
            //this.winGridViewPager1.AddColumnAlias("Dept", "部门");
            //this.winGridViewPager1.AddColumnAlias("Other", "其他");
            //this.winGridViewPager1.AddColumnAlias("Note", "备注");
            //this.winGridViewPager1.AddColumnAlias("Creator", "创建人");
            //this.winGridViewPager1.AddColumnAlias("CreateTime", "创建时间");

            #endregion

            string where = GetConditionSql();
            List<AddressInfo> list = BLLFactory<Address>.Instance.FindWithPager(where, this.winGridViewPager1.PagerInfo);
            this.winGridViewPager1.DataSource = new SortableBindingList<AddressInfo>(list);
            this.winGridViewPager1.PrintTitle = "通讯录联系人报表";
        }

        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindDataWithGroup(string groupName)
        {
            //entity
            this.winGridViewPager1.DisplayColumns = "Name,Sex,Dept,Position,Birthdate,Mobile,OfficeTelephone,Email,QQ,HomeTelephone,HomeAddress,OfficeAddress,Fax,Company,Other,Note,Creator,CreateTime";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<Address>.Instance.GetColumnNameAlias();//字段列显示名称转义

            #region 添加别名解析

            //this.winGridViewPager1.AddColumnAlias("Name", "姓名");
            //this.winGridViewPager1.AddColumnAlias("Sex", "性别");
            //this.winGridViewPager1.AddColumnAlias("Birthdate", "出生日期");
            //this.winGridViewPager1.AddColumnAlias("Mobile", "手机");
            //this.winGridViewPager1.AddColumnAlias("Email", "电子邮箱");
            //this.winGridViewPager1.AddColumnAlias("Qq", "QQ");
            //this.winGridViewPager1.AddColumnAlias("HomeTelephone", "家庭电话");
            //this.winGridViewPager1.AddColumnAlias("OfficeTelephone", "办公电话");
            //this.winGridViewPager1.AddColumnAlias("HomeAddress", "家庭住址");
            //this.winGridViewPager1.AddColumnAlias("OfficeAddress", "办公地址");
            //this.winGridViewPager1.AddColumnAlias("Fax", "传真号码");
            //this.winGridViewPager1.AddColumnAlias("Company", "公司单位");
            //this.winGridViewPager1.AddColumnAlias("Dept", "部门");
            //this.winGridViewPager1.AddColumnAlias("Other", "其他");
            //this.winGridViewPager1.AddColumnAlias("Note", "备注");
            //this.winGridViewPager1.AddColumnAlias("Creator", "创建人");
            //this.winGridViewPager1.AddColumnAlias("CreateTime", "创建时间");

            #endregion

            string where = GetConditionSql();
            List<AddressInfo> list = BLLFactory<Address>.Instance.FindByGroupNamePublic(groupName, this.winGridViewPager1.PagerInfo);
            this.winGridViewPager1.DataSource = new SortableBindingList<AddressInfo>(list);
            this.winGridViewPager1.PrintTitle = "通讯录联系人报表";
        }
        /// <summary>
        /// 查询数据操作
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            treeConditionSql = null;
            advanceCondition = null;//必须重置查询条件，否则可能会使用高级查询条件了
            BindData();
        }

        /// <summary>
        /// 新增数据操作
        /// </summary>
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            FrmEditAddress dlg = new FrmEditAddress();
            dlg.AddressType = AddressType.公共;
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
            dlg.InitFunction(LoginUserInfo, FunctionDict);//给子窗体赋值用户权限信息

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


        private string moduleName = "通讯录联系人";
        /// <summary>
        /// 导入Excel的操作
        /// </summary>          
        private void btnImport_Click(object sender, EventArgs e)
        {
            string templateFile = string.Format("{0}-模板.xls", moduleName);
            FrmImportExcelData dlg = new FrmImportExcelData();
            dlg.SetTemplate(templateFile, System.IO.Path.Combine(Application.StartupPath, templateFile));
            dlg.OnDataSave += new FrmImportExcelData.SaveDataHandler(ExcelData_OnDataSave);
            dlg.OnRefreshData += new EventHandler(ExcelData_OnRefreshData);
            dlg.ShowDialog();
        }

        void ExcelData_OnRefreshData(object sender, EventArgs e)
        {
            BindData();
        }

        bool ExcelData_OnDataSave(DataRow dr)
        {
            bool success = false;
            bool converted = false;
            DateTime dtDefault = Convert.ToDateTime("1900-01-01");
            DateTime dt;
            AddressInfo info = new AddressInfo();
            info.AddressType = AddressType.公共;
            info.Name = dr["姓名"].ToString();
            info.Sex = Convert.ToInt32(dr["性别"]);
            converted = DateTime.TryParse(dr["出生日期"].ToString(), out dt);
            if (converted && dt > dtDefault)
            {
                info.Birthday = dt;
            }
            info.MobilePhone = dr["手机"].ToString();
            info.Email = dr["电子邮箱"].ToString();
            info.QQ = Convert.ToInt32( dr["QQ"]);
            info.HomePhone = dr["家庭电话"].ToString();
            info.OfficePhone = dr["办公电话"].ToString();
            info.HomeAddress = dr["家庭住址"].ToString();
            info.OfficeAddress = dr["办公地址"].ToString();
            info.Fax = dr["传真号码"].ToString();
            info.CompanyName = dr["公司单位"].ToString();
            info.DeptName = dr["部门"].ToString();
            info.Position = dr["职位"].ToString();
            info.Other = dr["其他"].ToString();
            info.Remark = dr["备注"].ToString();

            info.CreatorId = LoginUserInfo.Id;
            info.CreatorTime = DateTimeHelper.GetServerDateTime2();
            info.EditorId = LoginUserInfo.Id;
            info.LastUpdateTime = DateTimeHelper.GetServerDateTime2();
            info.DeptId = LoginUserInfo.DeptId;
            info.CompanyId = LoginUserInfo.CompanyId;

            success = BLLFactory<Address>.Instance.Insert(info);
            return success;
        }

        /// <summary>
        /// 导出Excel的操作
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string file = FileDialogHelper.SaveExcel(string.Format("{0}.xls", moduleName));
            if (!string.IsNullOrEmpty(file))
            {
                string where = GetConditionSql();
                List<AddressInfo> list = new List<AddressInfo>();

                TreeNode selectedNode = this.treeView1.SelectedNode;
                if (isUserGroupName && selectedNode != null)
                {
                    //只有是数据库的分组才使用条件
                    if (selectedNode.Text != "所有联系人" && selectedNode.Text != "未分组联系人")
                    {
                        string groupName = this.treeView1.SelectedNode.Text;
                        list = BLLFactory<Address>.Instance.FindByGroupName(LoginUserInfo.Id, groupName);
                    }
                }
                else
                {
                    list = BLLFactory<Address>.Instance.Find(where);
                }

                DataTable dtNew = DataTableHelper.CreateTable("序号|int,姓名,性别,出生日期,手机,电子邮箱,QQ,家庭电话,办公电话,家庭住址,办公地址,传真号码,公司单位,部门,职位,其他,备注,创建人,创建时间");
                DataRow dr;
                int j = 1;
                for (int i = 0; i < list.Count; i++)
                {
                    dr = dtNew.NewRow();
                    dr["序号"] = j++;
                    dr["姓名"] = list[i].Name;
                    dr["性别"] = list[i].Sex;
                    dr["出生日期"] = list[i].Birthday;
                    dr["手机"] = list[i].MobilePhone;
                    dr["电子邮箱"] = list[i].Email;
                    dr["QQ"] = list[i].QQ;
                    dr["家庭电话"] = list[i].HomePhone;
                    dr["办公电话"] = list[i].OfficePhone;
                    dr["家庭住址"] = list[i].HomeAddress;
                    dr["办公地址"] = list[i].OfficeAddress;
                    dr["传真号码"] = list[i].Fax;
                    dr["公司单位"] = list[i].CompanyName;
                    dr["部门"] = list[i].DeptName;
                    dr["职位"] = list[i].Position;
                    dr["其他"] = list[i].Other;
                    dr["备注"] = list[i].Remark;
                    dr["创建人"] = list[i].CreatorId;
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
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmAddressCompany));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }

        private void InitTree()
        {
            base.LoginUserInfo = Cache.Instance["LoginUserInfo"] as LoginUserInfo;

            this.treeView1.BeginUpdate();
            this.treeView1.Nodes.Clear();
            //添加一个未分类和全部客户的组别
            TreeNode topNode = new TreeNode("所有联系人", 0, 0);
            this.treeView1.Nodes.Add(topNode);
            this.treeView1.Nodes.Add(new TreeNode("未分组联系人", 2, 2));

            List<AddressGroupNodeInfo> groupList = BLLFactory<AddressGroup>.Instance.GetTree(AddressType.公共.ToString());
            AddContactGroupTree(groupList, topNode, 1);

            this.treeView1.ExpandAll();
            this.treeView1.EndUpdate();
        }

        /// <summary>
        /// 获取客户分组并绑定
        /// </summary>
        private void AddContactGroupTree(List<AddressGroupNodeInfo> nodeList, TreeNode treeNode, int i)
        {
            foreach (AddressGroupNodeInfo nodeInfo in nodeList)
            {
                TreeNode subNode = new TreeNode(nodeInfo.Name, i, i);
                treeNode.Nodes.Add(subNode);

                AddContactGroupTree(nodeInfo.Children, subNode, i);
            }
        }

        bool isUserGroupName = false;
        string treeConditionSql = "";
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                if (e.Node.Text == "所有联系人")
                {
                    treeConditionSql = "";
                    BindData();
                }
                else if (e.Node.Text == "未分组联系人")
                {
                    isUserGroupName = true;
                    BindDataWithGroup(null);
                }
                else
                {
                    isUserGroupName = true;
                    BindDataWithGroup(e.Node.Text);
                }
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

        private void menuTree_AddNew_Click(object sender, EventArgs e)
        {
            btnAddNew_Click(null, null);
        }

        private void menu_GroupManage_Click(object sender, EventArgs e)
        {
            FrmAddressGroup dlg = new FrmAddressGroup();
            dlg.AddressType = AddressType.公共;
            dlg.InitFunction(LoginUserInfo, FunctionDict);
            dlg.OnDataSaved += new EventHandler(AddressGroup_OnDataSaved);
            dlg.ShowDialog();
        }

        void AddressGroup_OnDataSaved(object sender, EventArgs e)
        {
            InitTree();
            BindData();
        }

        private void btnBatchAdd_Click(object sender, EventArgs e)
        {
            FrmBatchAddAddress dlg = new FrmBatchAddAddress();
            dlg.AddressType = AddressType.公共;
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
            dlg.ShowDialog();
        }
    }
}
