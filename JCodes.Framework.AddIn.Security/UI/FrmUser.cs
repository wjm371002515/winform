using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.CommonControl.Pager.Others;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Network;
using JCodes.Framework.Common.Device;
using JCodes.Framework.AddIn.Basic;
using JCodes.Framework.Common.Format;

namespace JCodes.Framework.AddIn.Security
{
    /// <summary>
    /// 系统用户信息
    /// </summary>	
    public partial class FrmUser : BaseDock
    {
        public FrmUser()
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
            this.winGridViewPager1.PagerInfo.PageSize = 20;//指定20个一页

            //关联回车键进行查询
            foreach (Control control in this.layoutControl1.Controls)
            {
                control.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            }
        }

        void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "AuditStatus")
            {
                string status = this.winGridViewPager1.gridView1.GetRowCellValue(e.RowHandle, "AuditStatus").ToString();
                Color color = Color.White;
                if (status == "未审核")
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.BackColor2 = Color.LightCyan;
                }
            }
            else if (e.Column.FieldName == "IsExpire")
            {
                string status = this.winGridViewPager1.gridView1.GetRowCellValue(e.RowHandle, "IsExpire").ToString();
                Color color = Color.White;
                if (status.ToBoolean())
                {
                    e.Appearance.BackColor = Color.Yellow;
                    e.Appearance.BackColor2 = Color.LightCyan;
                }
            }
            else if (e.Column.FieldName == "Deleted")
            {
                string status = this.winGridViewPager1.gridView1.GetRowCellValue(e.RowHandle, "Deleted").ToString();
                Color color = Color.White;
                if (status.ToBoolean())
                {
                    e.Appearance.BackColor = Color.Yellow;
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
                winGridViewPager1.gridView1.SetGridColumWidth("Gender", 50);
                winGridViewPager1.gridView1.SetGridColumWidth("Email", 150);
                winGridViewPager1.gridView1.SetGridColumWidth("Note", 200);
            }
        }

        /// <summary>
        /// 编写初始化窗体的实现，可以用于刷新
        /// </summary>
        public override void FormOnLoad()
        {
            InitDeptTreeview();
            InitRoleTree();
            BindData();
            Init_Function();
        }

        void Init_Function()
        {
            btnSearch.Enabled = HasFunction("User/search");
            btnAddNew.Enabled = HasFunction("User/add");
            btnImport.Enabled = HasFunction("User/import");
            btnExport.Enabled = HasFunction("User/export");
        }

        #region 初始化组织结构树方法

        /// <summary>
        /// 初始化组织机构列表
        /// </summary>
        private void InitDeptTreeview()
        {
            this.treeDept.BeginUpdate();
            this.treeDept.Nodes.Clear();

            List<OUInfo>  list = Portal.gc.GetMyTopGroup();
            foreach (OUInfo groupInfo in list)
            {
                if (groupInfo != null && !groupInfo.Deleted)
                {
                    TreeNode topnode = new TreeNode();
                    topnode.Text = groupInfo.Name;
                    topnode.Name = groupInfo.ID.ToString();
                    topnode.ImageIndex = Portal.gc.GetImageIndex(groupInfo.Category);
                    topnode.SelectedImageIndex = Portal.gc.GetImageIndex(groupInfo.Category);
                    topnode.Tag = string.Format("Company_ID='{0}' ", groupInfo.ID);
                    this.treeDept.Nodes.Add(topnode);

                    List<OUNodeInfo> sublist = BLLFactory<OU>.Instance.GetTreeByID(groupInfo.ID);
                    AddOUNode(sublist, topnode);
                }
            }

            this.treeDept.ExpandAll();
            this.treeDept.EndUpdate();
        }

        private void AddOUNode(List<OUNodeInfo> list, TreeNode parentNode)
        {
            foreach (OUNodeInfo ouInfo in list)
            {
                TreeNode ouNode = new TreeNode();
                ouNode.Text = ouInfo.Name;
                ouNode.Name = ouInfo.ID.ToString();
                if (ouInfo.Deleted)
                {
                    ouNode.ForeColor = Color.Red;
                    continue;//跳过不显示
                }
                ouNode.ImageIndex = Portal.gc.GetImageIndex(ouInfo.Category);
                ouNode.SelectedImageIndex = Portal.gc.GetImageIndex(ouInfo.Category);
                if (ouNode.ImageIndex <= 1)//0,1为集团、公司
                {
                    ouNode.Tag = string.Format("Company_ID='{0}'", ouInfo.ID);
                }
                else
                {
                    ouNode.Tag = string.Format("Dept_ID={0}", ouInfo.ID);
                }
                parentNode.Nodes.Add(ouNode);

                AddOUNode(ouInfo.Children, ouNode);
            }            
        }

        #endregion

        #region 初始化角色树方法

        /// <summary>
        /// 初始化角色列表
        /// </summary>
        private void InitRoleTree()
        {
            this.treeRole.BeginUpdate();
            this.treeRole.Nodes.Clear();

            List<OUInfo> list = Portal.gc.GetMyTopGroup();
            foreach (OUInfo groupInfo in list)
            {
                if (groupInfo != null && !groupInfo.Deleted)
                {
                    TreeNode topnode = AddOUNode(groupInfo);
                    AddRole(groupInfo, topnode);

                    if (groupInfo.Category == "集团")
                    {
                        List<OUInfo> sublist = BLLFactory<OU>.Instance.GetAllCompany(groupInfo.ID);
                        foreach (OUInfo info in sublist)
                        {
                            TreeNode ouNode = AddOUNode(info, topnode);
                            AddRole(info, ouNode);
                        }
                    }
                    this.treeRole.Nodes.Add(topnode);
                }
            }

            this.treeRole.ExpandAll();
            this.treeRole.EndUpdate();
        }

        private TreeNode AddOUNode(OUInfo ouInfo, TreeNode parentNode = null)
        {
            TreeNode ouNode = new TreeNode();
            ouNode.Text = ouInfo.Name;
            ouNode.Tag = string.Format("Company_ID='{0}' ", ouInfo.ID);
            ouNode.ImageIndex = Portal.gc.GetImageIndex(ouInfo.Category);
            ouNode.SelectedImageIndex = Portal.gc.GetImageIndex(ouInfo.Category);

            if (parentNode != null)
            {
                parentNode.Nodes.Add(ouNode);
            }

            return ouNode;
        }

        private void AddRole(OUInfo ouInfo, TreeNode treeNode)
        {
            List<RoleInfo> roleList = BLLFactory<Role>.Instance.GetRolesByCompany(ouInfo.ID.ToString());
            foreach (RoleInfo roleInfo in roleList)
            {
                TreeNode roleNode = new TreeNode();
                roleNode.Text = roleInfo.Name;
                roleNode.Tag = roleInfo.ID;
                roleNode.ImageIndex = 5;
                roleNode.SelectedImageIndex = 5;

                treeNode.Nodes.Add(roleNode);
            }
        }

        #endregion

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
            if (!HasFunction("User/del"))
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
                BLLFactory<User>.Instance.SetDeletedFlag(ID);
            }

            BindData();
        }

        /// <summary>
        /// 分页控件编辑项操作
        /// </summary>
        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            if (!HasFunction("User/edit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            string ID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID");
            List<string> IDList = new List<string>();
            for (int i = 0; i < this.winGridViewPager1.gridView1.RowCount; i++)
            {
                string strTemp = this.winGridViewPager1.GridView1.GetRowCellDisplayText(i, "ID");
                IDList.Add(strTemp);
            }

            if (!string.IsNullOrEmpty(ID))
            {
                FrmEditUser dlg = new FrmEditUser();
                dlg.ID = ID;
                dlg.IDList = IDList;
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
            if (!HasFunction("User/add"))
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
            this.winGridViewPager1.AllToExport = BLLFactory<User>.Instance.FindToDataTable(where);
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
        private string treeConditionSql = "";
        bool isUseRoleSearch = false;//是否使用角色查询

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
                condition.AddCondition("HandNo", this.txtHandNo.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("Name", this.txtName.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("FullName", this.txtFullName.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("Nickname", this.txtNickname.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("MobilePhone", this.txtMobilePhone.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("Email", this.txtEmail.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("Gender", this.txtGender.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("QQ", this.txtQq.Text.Trim(), SqlOperator.Like);
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

            //如非选定，只显示正常用户
            if (!this.chkIncludeDelete.Checked)
            {
                where += string.Format(" AND Deleted=0");
            }
            return where;
        }

        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindData()
        {
            //entity
            this.winGridViewPager1.DisplayColumns = "HandNo,Name,FullName,Title,MobilePhone,OfficePhone,Email,Gender,QQ,AuditStatus,IsExpire,Deleted,Note";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<User>.Instance.GetColumnNameAlias();//字段列显示名称转义
            string where = GetConditionSql();
            List<UserInfo> list = BLLFactory<User>.Instance.FindWithPager(where, this.winGridViewPager1.PagerInfo);
            this.winGridViewPager1.DataSource = new SortableBindingList<UserInfo>(list);
            this.winGridViewPager1.PrintTitle = "系统用户信息报表";
        }

        /// <summary>
        /// 绑定列表数据（根据角色查询）
        /// </summary>
        private void BindDataUseRole(int roleId)
        {
            //entity
            this.winGridViewPager1.DisplayColumns = "HandNo,Name,FullName,Title,MobilePhone,OfficePhone,Email,Gender,QQ,AuditStatus,IsExpire,Deleted,Note";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<User>.Instance.GetColumnNameAlias();//字段列显示名称转义

            List<UserInfo> list = BLLFactory<User>.Instance.GetUsersByRole(roleId);
            this.winGridViewPager1.DataSource = new SortableBindingList<UserInfo>(list);
            this.winGridViewPager1.PrintTitle = "系统用户信息报表";
        }

        /// <summary>
        /// 查询数据操作
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!HasFunction("User/search"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            treeConditionSql = "";
            advanceCondition = null;//必须重置查询条件，否则可能会使用高级查询条件了
            isUseRoleSearch = false;

            BindData();
        }

        /// <summary>
        /// 新增数据操作
        /// </summary>
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            //默认部门
            string deptId = "";
            TreeNode node = treeDept.SelectedNode;
            if (node != null)
            {
                deptId = node.Name;
            }

            FrmEditUser dlg = new FrmEditUser();
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

        string SelectedDeptId = "";
        private string moduleName = "系统用户信息";
        /// <summary>
        /// 导入Excel的操作
        /// </summary>          
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (!HasFunction("User/import"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            //如果导入的Excel不指定部门，则默认使用选定的部门作为记录的部门
            TreeNode deptNode = this.treeDept.SelectedNode;
            if (deptNode != null)
            {
                SelectedDeptId = deptNode.Name;
            }
            else
            {
                MessageDxUtil.ShowTips("请选择组织机构节点，然后在进行导入，默认导入用户属于该部门节点");
                return;
            }

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
            string companyName = dr["所属公司名称"].ToString();
            OUInfo companyInfo = BLLFactory<OU>.Instance.FindByName(companyName);
            if (companyInfo == null)
            {
                //公司名称不存在，提示错误并记录日志
                throw new ArgumentException(string.Format("公司名称【{0}】不存在，记录已跳过", companyName));
            }

            string name = dr["用户名/登录名"].ToString();
            if (string.IsNullOrEmpty(name))
            {
                return false;//用户名为空，则跳过
            }
            else
            {
                //判断是否登录名重复，如果重复，则提示错误，并记录了日志
                bool isExist = BLLFactory<User>.Instance.IsExistKey("Name", name);
                if (isExist)
                {
                    throw new ArgumentException(string.Format("用户名/登录名【{0}】已存在，记录已跳过", name));
                }
            }

            string deptName = dr["默认部门名称"].ToString();
            OUInfo deptInfo = null;
            //if (!string.IsNullOrEmpty(deptName))
            //{
            //    deptInfo = BLLFactory<OU>.Instance.FindByName(deptName);
            //}

            //默认使用选定的部门作为记录的部门                
            if (!string.IsNullOrEmpty(SelectedDeptId))
            {
                deptInfo = BLLFactory<OU>.Instance.FindByID(SelectedDeptId.ToInt32());
            }

            bool success = false;
            bool converted = false;
            DateTime dtDefault = Convert.ToDateTime("1900-01-01");
            DateTime dt;
            UserInfo info = new UserInfo();
            info.HandNo = dr["用户编码"].ToString();
            info.Name = name;
            info.FullName = dr["用户全名"].ToString();
            info.Nickname = dr["用户呢称"].ToString();
            info.Gender = dr["性别"].ToString();
            info.MobilePhone = dr["移动电话"].ToString();
            info.Email = dr["邮件地址"].ToString();
            info.CurrentLoginUserId = Portal.gc.UserInfo.ID.ToString();
            #region 可选字段

            if (dr.Table.Columns.Contains("是否过期"))
            {
                info.IsExpire = dr["是否过期"].ToString().ToInt32() > 0;
            }
            if (dr.Table.Columns.Contains("职务头衔"))
            {
                info.Title = dr["职务头衔"].ToString();
            }
            if (dr.Table.Columns.Contains("身份证号码"))
            {
                info.IdentityCard = dr["身份证号码"].ToString();
            }
            if (dr.Table.Columns.Contains("办公电话"))
            {
                info.OfficePhone = dr["办公电话"].ToString();
            }
            if (dr.Table.Columns.Contains("家庭电话"))
            {
                info.HomePhone = dr["家庭电话"].ToString();
            }
            if (dr.Table.Columns.Contains("住址"))
            {
                info.Address = dr["住址"].ToString();
            }
            if (dr.Table.Columns.Contains("办公地址"))
            {
                info.WorkAddr = dr["办公地址"].ToString();
            }
            if (dr.Table.Columns.Contains("出生日期"))
            {
                converted = DateTime.TryParse(dr["出生日期"].ToString(), out dt);
                if (converted && dt > dtDefault)
                {
                    info.Birthday = dt;
                }
            }
            if (dr.Table.Columns.Contains("QQ号码"))
            {
                info.QQ = dr["QQ号码"].ToString();
            }
            if (dr.Table.Columns.Contains("个性签名"))
            {
                info.Signature = dr["个性签名"].ToString();
            }
            if (dr.Table.Columns.Contains("审核状态"))
            {
                info.AuditStatus = dr["审核状态"].ToString();
            }
            if (dr.Table.Columns.Contains("备注"))
            {
                info.Note = dr["备注"].ToString();
            }
            if (dr.Table.Columns.Contains("自定义字段"))
            {
                info.CustomField = dr["自定义字段"].ToString();
            }
            if (dr.Table.Columns.Contains("排序码"))
            {
                info.SortCode = dr["排序码"].ToString();
            } 
            #endregion

            #region 自动字段

            //默认部门，可以为空
            info.DeptName = deptName;
            if (deptInfo != null)
            {
                info.Dept_ID = deptInfo.ID.ToString();
            }

            //公司名称，不能为空
            info.CompanyName = companyName;
            if (companyInfo != null)
            {
                info.Company_ID = companyInfo.ID.ToString();
            }

            info.Creator = Portal.gc.UserInfo.FullName;
            info.Creator_ID = Portal.gc.UserInfo.ID.ToString();
            info.CreateTime = DateTimeHelper.GetServerDateTime2();
            info.Editor = Portal.gc.UserInfo.FullName;
            info.Editor_ID = Portal.gc.UserInfo.ID.ToString(); 
            #endregion

            success = BLLFactory<User>.Instance.Insert(info);
            return success;
        }

        /// <summary>
        /// 导出Excel的操作
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (!HasFunction("User/export"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            string file = FileDialogHelper.SaveExcel(string.Format("{0}.xls", moduleName));
            if (!string.IsNullOrEmpty(file))
            {
                List<UserInfo> list = new List<UserInfo>();

                TreeNode selectedNode = this.treeRole.SelectedNode;
                if (isUseRoleSearch && selectedNode != null && selectedNode.Tag != null)
                {
                    string roleId = selectedNode.Tag.ToString();
                    if (!string.IsNullOrEmpty(roleId))
                    {
                        list = BLLFactory<User>.Instance.GetUsersByRole(roleId.ToInt32());
                    }
                }
                else
                {
                    string where = GetConditionSql();
                    list = BLLFactory<User>.Instance.Find(where);
                }

                DataTable dtNew = DataTableHelper.CreateTable("序号|int,用户编码,用户名/登录名,用户全名,用户呢称,是否过期,职务头衔,身份证号码,移动电话,办公电话,家庭电话,邮件地址,住址,办公地址,性别,出生日期,QQ号码,个性签名,审核状态,备注,自定义字段,默认部门名称,所属公司名称,排序码");
                DataRow dr;
                int j = 1;
                DateTime dtDefault = Convert.ToDateTime("1900-01-01");
                for (int i = 0; i < list.Count; i++)
                {
                    dr = dtNew.NewRow();
                    dr["序号"] = j++;
                    dr["用户编码"] = list[i].HandNo;
                    dr["用户名/登录名"] = list[i].Name;
                    dr["用户全名"] = list[i].FullName;
                    dr["用户呢称"] = list[i].Nickname;
                    dr["是否过期"] = list[i].IsExpire ? "1" : "0";
                    dr["职务头衔"] = list[i].Title;
                    dr["身份证号码"] = list[i].IdentityCard;
                    dr["移动电话"] = list[i].MobilePhone;
                    dr["办公电话"] = list[i].OfficePhone;
                    dr["家庭电话"] = list[i].HomePhone;
                    dr["邮件地址"] = list[i].Email;
                    dr["住址"] = list[i].Address;
                    dr["办公地址"] = list[i].WorkAddr;
                    dr["性别"] = list[i].Gender;
                    if (list[i].Birthday > dtDefault)
                    {
                        dr["出生日期"] = list[i].Birthday;
                    }
                    dr["QQ号码"] = list[i].QQ;
                    dr["个性签名"] = list[i].Signature;
                    dr["审核状态"] = list[i].AuditStatus;
                    dr["备注"] = list[i].Note;
                    dr["自定义字段"] = list[i].CustomField;
                    dr["默认部门名称"] = list[i].DeptName;
                    dr["所属公司名称"] = list[i].CompanyName;
                    dr["排序码"] = list[i].SortCode;
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
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmUser));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }

        private void treeDept_AfterSelect(object sender, TreeViewEventArgs e)
        {
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

        private void treeRole_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                if (e.Node.Tag.ToString().StartsWith("Dept_ID", StringComparison.OrdinalIgnoreCase) ||
                    e.Node.Tag.ToString().StartsWith("Company_ID", StringComparison.OrdinalIgnoreCase))
                {
                    treeConditionSql = e.Node.Tag.ToString();
                    BindData();
                }
                else
                {
                    isUseRoleSearch = true;
                    BindDataUseRole(e.Node.Tag.ToString().ToInt32());
                }
            }
            else
            {
                treeConditionSql = "";
                BindData();
            }
        }

        private void menuDept_AddNew_Click(object sender, EventArgs e)
        {
            if (!HasFunction("User/add"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            btnAddNew_Click(null, null);
        }

        private void menuDept_ExpandAll_Click(object sender, EventArgs e)
        {            
            this.treeDept.ExpandAll();
        }

        private void menuDept_Collapse_Click(object sender, EventArgs e)
        {
            this.treeDept.CollapseAll();
        }

        private void menuDept_Refresh_Click(object sender, EventArgs e)
        {
            InitDeptTreeview();
        }

        private void menuRole_ExpandAll_Click(object sender, EventArgs e)
        {            
            this.treeRole.ExpandAll();
        }

        private void menuRole_Collapse_Click(object sender, EventArgs e)
        {
            this.treeRole.CollapseAll();
        }

        private void menuRole_Refresh_Click(object sender, EventArgs e)
        {
            InitRoleTree();
        }

        private void menu_InitPassword_Click(object sender, EventArgs e)
        {
            if (!HasFunction("User/initPwd"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            if (MessageDxUtil.ShowYesNoAndTips("您确定重置选定记录的用户密码么？ \r\n重置后密码将设置为【12345678】") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                string ip = NetworkUtil.GetLocalIP();
                string macAddr = HardwareInfoHelper.GetMacAddress();
                string changeUserId = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");

                bool success = BLLFactory<User>.Instance.ResetPassword(Portal.gc.UserInfo.ID, changeUserId.ToInt32(),Portal.gc.SystemType, ip, macAddr);
                MessageDxUtil.ShowTips(success ? "重置密码操作成功" : "操作失败");
            }
        }

        private void chkIncludeDelete_CheckedChanged(object sender, EventArgs e)
        {
            BindData();
        }

    }
}
