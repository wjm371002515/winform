using JCodes.Framework.BLL;
using JCodes.Framework.BLL._50Go;
using JCodes.Framework.BLL.Security;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.CommonControl.Pager.Others;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn._50Go
{ 
    /// <summary>
    /// 用户登录日志信息
    /// </summary>	
    public partial class FrmCouponCategory : BaseDock
    {
        public FrmCouponCategory()
        {
            InitializeComponent();

            this.winGridViewPager1.OnPageChanged += new EventHandler(winGridViewPager1_OnPageChanged);
            this.winGridViewPager1.OnStartExport += new EventHandler(winGridViewPager1_OnStartExport);
            this.winGridViewPager1.OnDeleteSelected += new EventHandler(winGridViewPager1_OnDeleteSelected);
            this.winGridViewPager1.OnEditSelected += new EventHandler(winGridViewPager1_OnEditSelected);
            this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);
            this.winGridViewPager1.ShowLineNumber = true;
            this.winGridViewPager1.AppendedMenu = this.contextMenuStrip1;
            this.winGridViewPager1.BestFitColumnWith = false;
            this.winGridViewPager1.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);

            //关联回车键进行查询
            foreach (Control control in this.layoutControl1.Controls)
            {
                control.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            }
        }

        /// <summary>
        /// 对显示的字段内容进行转义
        /// </summary>
        void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "DELETED")
            {
                e.DisplayText = Convert.ToInt32(e.Value) == 0 ? "否" : "是";
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
            if (!HasFunction("Generatecoupons/del"))
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
                BLLFactory<Coupon>.Instance.DeleteByUser(ID, LoginUserInfo.ID.ToString());
            }
            BindData();
        }

        /// <summary>
        /// 分页控件删除操作
        /// </summary>
        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            if (!HasFunction("Generatecoupons/edit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            if (rowSelected.Length > 1 || rowSelected.Length == 0)
                return;
            string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(rowSelected[0], "ID");

            FrmGenerateCoupon dlg = new FrmGenerateCoupon();
            dlg.Text = Const.Edit + dlg.Text;
            dlg.ID = ID;
            dlg.OnDataSaved += new EventHandler(winGridViewPager1_OnRefresh);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindData();
            }
        }

        
        /// <summary>
        /// 分页控件全部导出操作前的操作
        /// </summary> 
         private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
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
        /// 提供给控件回车执行查询的操作
        /// </summary>
        private void SearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }

        /// <summary>
        /// 导出Excel的操作
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string file = FileDialogHelper.SaveExcel(string.Format("{0}.xls", "优惠券信息数据" + DateTime.Now.ToString("yyyyMMddHHmmss")));
            if (!string.IsNullOrEmpty(file))
            {
                string where = GetConditionSql();
                List<CouponInfo> list = BLLFactory<Coupon>.Instance.Find(where);
                DataTable dtNew = DataTableHelper.CreateTable("序号|int,优惠券序列号,分类名称,操作公司,创建人,创建人ID,创建时间,使用人,使用人ID,使用时间,联系电话,联系人,有效开始日期,有效结束日期,是否使用");
                DataRow dr;
                int j = 1;
                for (int i = 0; i < list.Count; i++)
                {
                    dr = dtNew.NewRow();
                    dr["序号"] = j++;
                    dr["优惠券序列号"] = list[i].ID;
                    dr["分类名称"] = list[i].CouponCategory_Name;
                    dr["操作公司"] = list[i].Company_Name;
                    dr["创建人"] = list[i].Creator;
                    dr["创建人ID"] = list[i].Creator_ID;
                    dr["创建时间"] = list[i].CreateTime;
                    dr["使用人"] = list[i].Editor;
                    dr["使用人ID"] = list[i].Editor_ID;
                    dr["使用时间"] = list[i].EditTime;
                    dr["联系电话"] = list[i].MobilePhone;
                    dr["联系人"] = list[i].LoginName;
                    dr["有效开始日期"] = list[i].StartTime;
                    dr["有效结束日期"] = list[i].EndTime;
                    dr["是否使用"] = list[i].DELETED == 0?"否":"是";
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
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmCouponCategory));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }

        /// <summary>
        /// 根据查询条件构造查询语句
        /// </summary> 
        private string GetConditionSql()
        {
            SearchCondition condition = new SearchCondition();
            condition.AddCondition("ID", this.txtID.Text, SqlOperator.Like);
            if ((this.txtCategory.SelectedItem as CListItem) != null)
            {
                condition.AddCondition("CouponCategory_ID", (this.txtCategory.SelectedItem as CListItem).Value, SqlOperator.Equal);
            }
            condition.AddCondition("MobilePhone", this.txtMobilePhone.Text, SqlOperator.Like);
            condition.AddCondition("LoginName", this.txtLoginName.Text, SqlOperator.Like);
            if (this.txtEnabled.SelectedIndex != -1)
            { 
                condition.AddCondition("DELETED", this.txtEnabled.SelectedIndex, SqlOperator.Equal);
            }

            string where = condition.BuildConditionSql().Replace("Where", "");
            //如果是单击节点得到的条件，则使用树列表的，否则使用查询条件的
            if (!string.IsNullOrEmpty(treeConditionSql))
            {
                where = treeConditionSql;
            }
            return where;
        }

        /// <summary>
        /// 查询数据操作
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.treeConditionSql = null;
            BindData();
        }

        string treeConditionSql = "";
        /// <summary>
        /// 过滤只有某种优惠券的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!HasFunction("Generatecoupons/search"))
            {
                return;
            }

            if (e.Node != null && e.Node.Tag != null)
            {
                treeConditionSql = string.Format("CouponCategory_ID = '{0}'", e.Node.Tag.ToString());
                BindData();
            }
            else
            {
                treeConditionSql = "";
                BindData();
            }
        }

        /// <summary>
        /// 生成优惠券
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            FrmGenerateCoupon dlg = new FrmGenerateCoupon();
            dlg.Text = Const.Add + dlg.Text;
            dlg.OnDataSaved += new EventHandler(winGridViewPager1_OnRefresh);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindData();
            }
        }

        

        #region 初始化界面

        /// <summary>
        /// 编写初始化窗体的实现，可以用于刷新
        /// </summary>
        public override void FormOnLoad()
        {
            // 加载分类数据
            InitTree();

            Init_Function();
        }

        private void Init_Function()
        {
            btnGenerate.Enabled = HasFunction("Generatecoupons/add");
            btnSearch.Enabled = HasFunction("Generatecoupons/search");
            btnExport.Enabled = HasFunction("Generatecoupons/export");

            // 初始化查询界面分类下拉框
            // 初始化类别 
            this.txtCategory.Properties.Items.Clear();
            List<CouponCategoryInfo> lst = BLLFactory<CouponCategory>.Instance.GetAllCouponCategory();
            foreach (var couponCategory in lst)
            {
                this.txtCategory.Properties.Items.Add(new CListItem(couponCategory.HandNo + "-" + couponCategory.Name, couponCategory.ID));
            }

            #region 添加别名解析
            // Company_Name,
            this.winGridViewPager1.DisplayColumns = "ID,CouponCategory_Name,Company_Name,Creator,Creator_ID,CreateTime,Editor,Editor_ID, EditTime,MobilePhone,LoginName,StartTime,EndTime,DELETED";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<Coupon>.Instance.GetColumnNameAlias();//字段列显示名称转义
            #endregion


        }
        #endregion

        #region 分类右击事件处理
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuTree_Add_Click(object sender, EventArgs e)
        {
            if (!HasFunction("Generatecoupons/addCategory"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            FrmEditCouponCategory dlg = new FrmEditCouponCategory();
            dlg.Text = Const.Add +dlg.Text;
            dlg.OnDataSaved += new EventHandler(menuTree_Refresh_Click);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                InitTree();
            }
        }

        /// <summary>
        /// 刷新分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuTree_Refresh_Click(object sender, EventArgs e)
        {
            InitTree();
        }

        /// <summary>
        /// 绑定分类数据
        /// </summary>
        private void InitTree()
        {
            this.treeView1.BeginUpdate();
            this.treeView1.Nodes.Clear();
            TreeNode rootNode = new TreeNode("所有分类", 0, 0);
            this.treeView1.Nodes.Add(rootNode);

            //下面在添加系统类型节点
            List<CouponCategoryInfo> couponCategoryList = BLLFactory<CouponCategory>.Instance.GetAllCouponCategory();
            foreach (CouponCategoryInfo couponCategory in couponCategoryList)
            {
                TreeNode treeNode = new TreeNode(couponCategory.HandNo + "-" + couponCategory.Name, 1, 1);
                treeNode.Tag = couponCategory.ID;
                treeView1.Nodes.Add(treeNode);
            }

            this.treeView1.ExpandAll();
            this.treeView1.EndUpdate();
        }

        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindData()
        {
            string where = GetConditionSql();
            List<CouponInfo> list = BLLFactory<Coupon>.Instance.FindWithPager(where, this.winGridViewPager1.PagerInfo);
            this.winGridViewPager1.DataSource = new SortableBindingList<CouponInfo>(list);
            this.winGridViewPager1.PrintTitle = "优惠券信息数据";
        }

        /// <summary>
        /// 双击分类内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!HasFunction("Generatecoupons/editCategory"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            FrmEditCouponCategory dlg = new FrmEditCouponCategory();
            dlg.ID = e.Node.Tag.ToString();
            dlg.Text = Const.Edit + dlg.Text;
            dlg.OnDataSaved += new EventHandler(menuTree_Refresh_Click);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                InitTree();
            }
        }
        #endregion 
    }
}
