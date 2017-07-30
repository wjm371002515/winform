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
using JCodes.Framework.AddIn.Basic;
using JCodes.Framework.BLL._50Go;

namespace JCodes.Framework.AddIn._50Go
{
    /// <summary>
    /// 登陆系统的黑白名单列表
    /// </summary>	
    public partial class FrmCoupon : BaseDock
    {
        /// <summary>
        /// 设置私有变量，让其标识此券是否已被使用
        /// </summary>
        private CouponInfo couponInfo = null;

        public FrmCoupon()
        {
            InitializeComponent();
            this.winGridViewPager1.OnEditSelected += new EventHandler(winGridViewPager1_OnEditSelected);
            this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);
            this.winGridViewPager1.AppendedMenu = this.contextMenuStrip1;
            this.winGridViewPager1.ShowLineNumber = true;
            this.winGridViewPager1.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置
            this.winGridViewPager1.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);
            //关联回车键进行查询
            foreach (Control control in this.layoutControl1.Controls)
            {
                control.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            }

            InitControl();
        }

        private void InitControl()
        {
            if (!HasFunction("Exchangecoupons/search"))
            {
                btnSearch.Enabled = false;
            }
        }
       
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
        /// 分页控件编辑项操作
        /// </summary>
        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            if (!HasFunction("Exchangecoupons/exchange"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            if (!CheckCoupon())
            {
                return;
            }

            if (MessageDxUtil.ShowYesNoAndTips("确定要使用优惠券吗？此操作不可逆") == DialogResult.No)
            {
                return;
            }

            string ID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID");

            if (this.winGridViewPager1.gridView1.RowCount == 0 || this.winGridViewPager1.gridView1.RowCount > 1)
                return;
            
            // 直接更新编辑人和状态;
            BLLFactory<Coupon>.Instance.UseCoupon(ID, Portal.gc.UserInfo.Creator, Portal.gc.UserInfo.Creator_ID, DateTime.Now);

            MessageDxUtil.ShowYesNoAndTips("操作成功");
       
        }

        private bool CheckCoupon()
        {
            if (couponInfo == null)
                return false;

            // 检查权限
            // 如果此操作员不是这个公司下面的
            if (couponInfo.Company_ID != Portal.gc.UserInfo.Company_ID)
            { 
                // 再次确认此操作员是否挂在集团下面 TODO 暂时不允许这么大的人操作
                MessageDxUtil.ShowTips("此操作员不允许跨公司使用优惠券");
                return false;
            }
            // 检查券的有效期
            DateTime dt = DateTime.Now;
            if (dt < couponInfo.StartTime || dt > couponInfo.EndTime)
            {
                MessageDxUtil.ShowTips("此优惠券不在使用时间范围内");
                return false;
            }
            // 是否使用
            if (couponInfo.DELETED == 1)
            {
                MessageDxUtil.ShowTips("此优惠券已被使用，不可以重复使用");
                return false;
            }

            return true;
        }
  
        /// <summary>
        /// 根据查询条件构造查询语句
        /// </summary> 
        private string GetConditionSql()
        {
            //如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
            SearchCondition condition = new SearchCondition();
            condition.AddCondition("ID", this.txtID.Text.Trim(), SqlOperator.Equal);
            condition.AddCondition("FullName", this.txtFullName.Text.Trim(), SqlOperator.Equal);
            condition.AddCondition("MobilePhone", this.txtMobilePhone.Text.Trim(), SqlOperator.Equal);
            string where = condition.BuildConditionSql().Replace("Where", "");
            return where;
        }
        
        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindData()
        {
            this.winGridViewPager1.DisplayColumns = "ID,CouponCategory_Name,Company_Name,Creator,Creator_ID,CreateTime,Editor,Editor_ID, EditTime,MobilePhone,FullName,StartTime,EndTime,DELETED";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<Coupon>.Instance.GetColumnNameAlias();//字段列显示名称转义

            string where = GetConditionSql();
            List<CouponInfo> list = BLLFactory<Coupon>.Instance.Find(where);
            this.winGridViewPager1.DataSource = new SortableBindingList<CouponInfo>(list);
            this.winGridViewPager1.PrintTitle = "优惠券信息数据";

            if (list.Count == 1)
                couponInfo = list[0];
        }
        
        /// <summary>
        /// 查询数据操作
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            couponInfo = null;

            if (this.txtID.Text.Length == 0)
            {
                MessageDxUtil.ShowTips("请输入兑换码");
                this.txtID.Focus();
                return;
            }
            else if (this.txtFullName.Text.Length == 0)
            {
                MessageDxUtil.ShowTips("请输入联系人");
                this.txtFullName.Focus();
                return;
            }
            else if (this.txtMobilePhone.Text.Length == 0)
            {
                MessageDxUtil.ShowTips("请输入联系方式");
                this.txtMobilePhone.Focus();
                return;
            }

            BindData();

            CheckCoupon();
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
