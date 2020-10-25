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
using JCodes.Framework.jCodesenum;
using DevExpress.XtraPrinting;

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
            this.winGridViewPager1.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(gridView1_RowStyle);
            this.winGridViewPager1.AppendedMenu = this.contextMenuStrip1;
            this.winGridViewPager1.ShowLineNumber = true;
            this.winGridViewPager1.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置
            this.winGridViewPager1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
            this.winGridViewPager1.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);
            this.winGridViewPager1.PagerInfo.PageSize = 20;//指定20个一页

            //关联回车键进行查询
            foreach (Control control in this.layoutControl1.Controls)
            {
                control.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            }
        }

        void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if ((sender as DevExpress.XtraGrid.Views.Grid.GridView).GetRowCellValue(e.RowHandle, "IsExpire").ToString().ToInt32() == (short)IsDelete.是)
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }

                    if ((sender as DevExpress.XtraGrid.Views.Grid.GridView).GetRowCellValue(e.RowHandle, "IsDelete").ToString().ToInt32() == (short)IsDelete.是)
                    {
                        e.Appearance.ForeColor = Color.Red;
                        Font font = new System.Drawing.Font("", e.Appearance.Font.Size, FontStyle.Strikeout);
                        e.Appearance.Font = font;
                    }    
                }
            }
            catch (Exception)
            {

            }
        }


        void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (string.Equals(e.Column.FieldName, "Birthday", StringComparison.CurrentCultureIgnoreCase))
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd");//yyyy-MM-dd
                }
            }
            else if (e.Column.ColumnType == typeof(DateTime))
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
                        e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm:ss");//yyyy-MM-dd
                    }
                }
            }
            else if (string.Equals(e.Column.FieldName, "IsExpire", StringComparison.CurrentCultureIgnoreCase))
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.DisplayText = EnumHelper.GetMemberName<IsExpire>(e.Value);
                }
            }
            else if (string.Equals(e.Column.FieldName, "Password", StringComparison.CurrentCultureIgnoreCase))
            {
                e.DisplayText = "******";
            }
            else if (string.Equals(e.Column.FieldName, "Gender", StringComparison.CurrentCultureIgnoreCase))
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.DisplayText = EnumHelper.GetMemberName<Gender>(e.Value);
                }
            }
            else if (string.Equals(e.Column.FieldName, "AuditStatus", StringComparison.CurrentCultureIgnoreCase))
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.DisplayText = EnumHelper.GetMemberName<AuditStatus>(e.Value);
                }
            }
            else if (string.Equals(e.Column.FieldName, "CreatorId", StringComparison.CurrentCultureIgnoreCase))
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()) && Portal.gc.AllUserInfo.ContainsKey(e.Value.ToString().ToInt32()))
                {
                    e.DisplayText = Portal.gc.AllUserInfo[e.Value.ToString().ToInt32()];
                }
            }
            else if (string.Equals(e.Column.FieldName, "EditorId", StringComparison.CurrentCultureIgnoreCase))
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()) && Portal.gc.AllUserInfo.ContainsKey(e.Value.ToString().ToInt32()))
                {
                    e.DisplayText = Portal.gc.AllUserInfo[e.Value.ToString().ToInt32()];
                }
            }
            else if (string.Equals(e.Column.FieldName, "IsDelete", StringComparison.CurrentCultureIgnoreCase))
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.DisplayText = EnumHelper.GetMemberName<IsDelete>(e.Value);
                }
            }
            else if (string.Equals(e.Column.FieldName, "DeptId", StringComparison.CurrentCultureIgnoreCase))
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()) && Portal.gc.AllOuInfo.ContainsKey(e.Value.ToString().ToInt32()))
                {
                    e.DisplayText = Portal.gc.AllOuInfo[e.Value.ToString().ToInt32()];
                }
            }
            else if (string.Equals(e.Column.FieldName, "CompanyId", StringComparison.CurrentCultureIgnoreCase))
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()) && Portal.gc.AllOuInfo.ContainsKey(e.Value.ToString().ToInt32()))
                {
                    e.DisplayText = Portal.gc.AllOuInfo[e.Value.ToString().ToInt32()];
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
                winGridViewPager1.gridView1.SetGridColumWidth("Remark", 300);
                winGridViewPager1.gridView1.SetGridColumWidth("IsExpire", 70);
                winGridViewPager1.gridView1.SetGridColumWidth("IdCard", 140); // 身份证
                winGridViewPager1.gridView1.SetGridColumWidth("Address", 300); // 地址
                winGridViewPager1.gridView1.SetGridColumWidth("WorkAddress", 300); // 工作地点
                winGridViewPager1.gridView1.SetGridColumWidth("Birthday", 80); // 生日
                winGridViewPager1.gridView1.SetGridColumWidth("QQ", 80); // QQ
                winGridViewPager1.gridView1.SetGridColumWidth("Signature", 300); // 个性签名
                winGridViewPager1.gridView1.SetGridColumWidth("AuditStatus", 70); // 审核状态
                winGridViewPager1.gridView1.SetGridColumWidth("DeptId", 70); // 部门Id
                winGridViewPager1.gridView1.SetGridColumWidth("CompanyId", 70); // 公司Id
                winGridViewPager1.gridView1.SetGridColumWidth("CreatorId", 70); // 创建日期
                winGridViewPager1.gridView1.SetGridColumWidth("CreatorTime", 130); // 创建日期
                winGridViewPager1.gridView1.SetGridColumWidth("EditorId", 70);
                winGridViewPager1.gridView1.SetGridColumWidth("IsDelete", 70);
                winGridViewPager1.gridView1.SetGridColumWidth("LastUpdateTime", 130); // 最后更新日期
                winGridViewPager1.gridView1.SetGridColumWidth("LastLoginIp", 110); // 登陆IP
                winGridViewPager1.gridView1.SetGridColumWidth("LastLoginTime", 130); // 最后登陆日期
                winGridViewPager1.gridView1.SetGridColumWidth("LastLoginMac", 120); // 最后登陆mac	
                winGridViewPager1.gridView1.SetGridColumWidth("CurLoginIp", 110); // 当前登陆IP
                winGridViewPager1.gridView1.SetGridColumWidth("CurLoginTime", 130); // 当前登录日期
                winGridViewPager1.gridView1.SetGridColumWidth("CurLoginMac", 120); // 当前登录mac
                winGridViewPager1.gridView1.SetGridColumWidth("LastChangePwdTime", 130); // 最后修改密码时间
                winGridViewPager1.gridView1.SetGridColumWidth("Seq", 130); // 最后修改密码时间
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
            btnAddNew.Enabled = HasFunction("User/Set/UserAdd");
            btnImport.Enabled = HasFunction("User/Set/UserImport");
            btnExport.Enabled = HasFunction("User/Set/UserExport");
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
                if (groupInfo != null && groupInfo.IsDelete == (short)IsDelete.否 )
                {
                    TreeNode topnode = new TreeNode();
                    topnode.Text = groupInfo.Name;
                    topnode.Name = groupInfo.Id.ToString();
                    topnode.ImageIndex = groupInfo.OuType;//Portal.gc.GetImageIndex(groupInfo.OuType);
                    topnode.SelectedImageIndex = groupInfo.OuType; //Portal.gc.GetImageIndex(groupInfo.Category);
                    topnode.Tag = string.Format("CompanyId='{0}' ", groupInfo.Id);

                    this.treeDept.Nodes.Add(topnode);

                    List<OUNodeInfo> sublist = BLLFactory<OU>.Instance.GetTreeById(groupInfo.Id);
                  
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
                ouNode.Name = ouInfo.Id.ToString();
                if (ouInfo.IsDelete == (short)IsDelete.是)
                {
                    ouNode.ForeColor = Color.Red;
                    continue;//跳过不显示
                }
                ouNode.ImageIndex = ouInfo.OuType;// Portal.gc.GetImageIndex(ouInfo.OuType);
                ouNode.SelectedImageIndex = ouInfo.OuType; //Portal.gc.GetImageIndex(ouInfo.Category);
                // 公司或者集团
                if (ouInfo.OuType == (short)OuType.集团 || ouInfo.OuType == (short)OuType.公司)
                {
                    ouNode.Tag = string.Format("CompanyId='{0}'", ouInfo.Id);
                }
                else
                {
                    ouNode.Tag = string.Format("DeptId={0}", ouInfo.Id);
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
                if (groupInfo != null && groupInfo.IsDelete == (short)IsDelete.否)
                {
                    TreeNode topnode = AddOUNode(groupInfo);
                    AddRole(groupInfo, topnode);

                    if (groupInfo.OuType == 1)
                    {
                        List<OUInfo> sublist = BLLFactory<OU>.Instance.GetAllCompany(groupInfo.Id);
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
            ouNode.Tag = string.Format("CompanyId='{0}' ", ouInfo.Id);
            ouNode.ImageIndex = ouInfo.OuType; 
            ouNode.SelectedImageIndex = ouInfo.OuType; 

            if (parentNode != null)
            {
                parentNode.Nodes.Add(ouNode);
            }

            return ouNode;
        }

        private void AddRole(OUInfo ouInfo, TreeNode treeNode)
        {
            List<RoleInfo> roleList = BLLFactory<Role>.Instance.GetRolesByCompanyId(ouInfo.Id);
            foreach (RoleInfo roleInfo in roleList)
            {
                TreeNode roleNode = new TreeNode();
                roleNode.Text = roleInfo.Name;
                roleNode.Tag = roleInfo.Id;
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
            if (!HasFunction("User/Set/UserDel"))
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
                BLLFactory<User>.Instance.SetDeletedFlag(Id);
            }

            BindData();
        }

        /// <summary>
        /// 分页控件编辑项操作
        /// </summary>
        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            if (!HasFunction("User/Set/UserEdit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            Int32 Id = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("Id").ToInt32();

            if (Id > 0)
            {
                FrmEditUser dlg = new FrmEditUser();
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
            if (!HasFunction("User/Set/UserAdd"))
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
                condition.AddCondition("UserCode", this.txtHandNo.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("Name", this.txtName.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("LoginName", this.txtLoginName.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("IdCard", this.txtIdCard.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("MobilePhone", this.txtMobilePhone.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("Email", this.txtEmail.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("Gender", this.txtGender.EditValue, SqlOperator.Like);
                condition.AddCondition("QQ", this.txtQq.Text.Trim(), SqlOperator.Like);
            }

            //如果是公司管理员，增加公司标识
            if (!Portal.gc.IsSuperAdmin)
            {
                condition.AddCondition("CompanyId", Portal.gc.UserInfo.CompanyId, SqlOperator.Equal);
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
                where += string.Format(" AND IsDelete={0}", (short)IsDelete.否);
            }

            return where;
        }

        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindData()
        {
            var columnNameAlias = BLLFactory<User>.Instance.GetColumnNameAlias();//字段列显示名称转义
            columnNameAlias.Remove("Password");
            //entity
            this.winGridViewPager1.DisplayColumns = columnNameAlias.ToDiplayKeyString();
            this.winGridViewPager1.ColumnNameAlias = columnNameAlias;
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
            var columnNameAlias = BLLFactory<User>.Instance.GetColumnNameAlias();//字段列显示名称转义
            columnNameAlias.Remove("Password");
            //entity
            this.winGridViewPager1.DisplayColumns = columnNameAlias.ToDiplayKeyString();
            this.winGridViewPager1.ColumnNameAlias = columnNameAlias;

            List<UserInfo> list = BLLFactory<User>.Instance.GetUsersByRoleId(roleId);
            this.winGridViewPager1.DataSource = new SortableBindingList<UserInfo>(list);
            this.winGridViewPager1.PrintTitle = "系统用户信息报表";
        }

        /// <summary>
        /// 查询数据操作
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
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

        private string moduleName = "系统用户信息";
        /// <summary>
        /// 导入Excel的操作
        /// </summary>          
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (!HasFunction("User/Set/UserImport"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
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
            bool success = false;

            UserInfo info = new UserInfo();
            // ID序号	用户编码	名称	登录名	是否过期	
            // 身份证	手机	办公电话	家庭电话	Email邮箱	
            // 地址	工作地址	性别	生日	QQ号	
            // 个性签名	审核状态	个人图片	备注	部门Id	
            // 公司Id	创建人编号	创建时间	编辑人编号	最后更新时间	
            // 是否删除	问题1	问题2	问题3	回答1	
            // 回答2	回答3	最后登录IP	最后登录日期	最后登录Mac	
            // 当前登录IP	当前登录日期	当前登录Mac	最后修改密码时间	
            // 排序
            UserInfo tmpInfo = BLLFactory<User>.Instance.FindById(dr["ID序号"].ToString().ToInt32());
            if (null == tmpInfo)
                info.Id = BLLFactory<User>.Instance.GetMaxId() + 1;
            else
                info.Id = tmpInfo.Id;
            info.UserCode = dr["用户编码"].ToString();

            tmpInfo = BLLFactory<User>.Instance.GetUserByName(dr["名称"].ToString());
            if (tmpInfo != null)
            {
                MessageDxUtil.ShowError("存在相同的操作员名字，该条数据将不会被导入！");
                return false;
            }

            info.Name = dr["名称"].ToString();
            info.LoginName = dr["登录名"].ToString();
            info.IsExpire = (short)EnumHelper.GetMemberValue<IsExpire>(dr["是否过期"].ToString()).ToString().ToInt32();
            info.IdCard = dr["身份证"].ToString();
            info.MobilePhone = dr["手机"].ToString();
            info.OfficePhone = dr["办公电话"].ToString();
            info.HomePhone = dr["家庭电话"].ToString();
            info.Email = dr["Email邮箱"].ToString();
            info.Address = dr["地址"].ToString();
            info.WorkAddress = dr["工作地址"].ToString();
            info.Gender = (short)EnumHelper.GetMemberValue<Gender>(dr["性别"].ToString()).ToString().ToInt32();
            info.Birthday = ConvertHelper.ToDateTime( dr["生日"], DateTime.MinValue);
            info.QQ = ConvertHelper.ToInt32(dr["QQ号"], 0);
            info.Signature = dr["个性签名"].ToString();
            info.AuditStatus = (short)EnumHelper.GetMemberValue<AuditStatus>(dr["审核状态"].ToString()).ToString().ToInt32();
            info.Portrait = dr["个人图片"].ToString();
            info.Remark = dr["备注"].ToString();
            OUInfo deptInfo = BLLFactory<OU>.Instance.FindByName( dr["部门Id"].ToString());
            if (deptInfo != null && deptInfo.OuType == (short)OuType.部门)
            {
                info.DeptId =  deptInfo.Id;
            }
            OUInfo companyInfo = BLLFactory<OU>.Instance.FindByName( dr["公司Id"].ToString());
            if (companyInfo != null && companyInfo.OuType == (short)OuType.公司)
            {
                info.CompanyId =  companyInfo.Id;
            }
            info.CreatorId = Portal.gc.UserInfo.Id;
            info.CreatorTime = DateTimeHelper.GetServerDateTime2();
            info.EditorId = Portal.gc.UserInfo.Id;
            info.LastUpdateTime = DateTimeHelper.GetServerDateTime2();
            info.IsDelete = (short)EnumHelper.GetMemberValue<IsDelete>(dr["是否删除"].ToString()).ToString().ToInt32();
            info.Question1 = dr["问题1"].ToString();
            info.Question2 = dr["问题2"].ToString();
            info.Question3 = dr["问题3"].ToString();
            info.Answer1 = dr["回答1"].ToString();
            info.Answer2 = dr["回答2"].ToString();
            info.Answer3 = dr["回答3"].ToString();
            info.LastLoginIp = NetworkUtil.GetLocalIP();
            info.LastLoginTime = DateTimeHelper.GetServerDateTime2();
            info.LastLoginMac = NetworkUtil.GetMacAddress();
            info.CurLoginIp = NetworkUtil.GetLocalIP();
            info.CurLoginTime = DateTimeHelper.GetServerDateTime2();
            info.CurLoginMac = NetworkUtil.GetMacAddress();
            info.Seq = dr["排序"].ToString();

            success = BLLFactory<User>.Instance.Insert(info);
            return success;
        }

        /// <summary>
        /// 导出Excel的操作
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (!HasFunction("User/Set/UserExport"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            string file = FileDialogHelper.SaveExcel(string.Format("{0}.xls", moduleName));
            if (!string.IsNullOrEmpty(file))
            {
                try
                {
                    DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions(DevExpress.XtraPrinting.TextExportMode.Value);
                    options.TextExportMode = TextExportMode.Text;//设置导出模式为文本
                    winGridViewPager1.gridView1.OptionsPrint.AutoWidth = false;
                    winGridViewPager1.gridView1.ExportToXls(file, options);

                    if (MessageDxUtil.ShowYesNoAndTips("导出成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(file);
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
                if (e.Node.Tag.ToString().StartsWith("DeptId", StringComparison.OrdinalIgnoreCase) ||
                    e.Node.Tag.ToString().StartsWith("CompanyId", StringComparison.OrdinalIgnoreCase))
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
            if (!HasFunction("User/Set/UserAdd"))
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
            if (!HasFunction("User/Set/UserInitPwd"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            if (MessageDxUtil.ShowYesNoAndTips(string.Format("您确定重置选定记录的用户密码么？ \r\n重置后密码将设置为【{0}】", Const.defaultPwd)) == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                string ip = NetworkUtil.GetLocalIP();
                string macAddr = HardwareInfoHelper.GetMacAddress();
                Int32 changeUserId = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "Id").ToInt32();

                bool success = BLLFactory<User>.Instance.ResetPassword(Portal.gc.UserInfo.Id, changeUserId,Portal.gc.SYSTEMTYPEID, ip, macAddr);
                MessageDxUtil.ShowTips(success ? "重置密码操作成功" : "操作失败");
            }
        }

        private void chkIncludeDelete_CheckedChanged(object sender, EventArgs e)
        {
            BindData();
        }

    }
}
