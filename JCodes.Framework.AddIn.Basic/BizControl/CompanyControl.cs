using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.BLL;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.AddIn.Basic.BizControl
{
    /// <summary>
    /// 公司显示控件
    /// </summary>
    public partial class CompanyControl : UserControl, ISupportStyleController
    {
        /// <summary>
        /// 选择的值发生变化的时候
        /// </summary>
        public event EventHandler EditValueChanged;

        public CompanyControl()
        {
            InitializeComponent();

            this.txtCompany.EditValueChanged += new EventHandler(txtCompany_EditValueChanged);
        }

        void txtCompany_EditValueChanged(object sender, EventArgs e)
        {
            if (EditValueChanged != null)
            {
                EditValueChanged(sender, e);
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void Init()
        {
            DataTable dt = DataTableHelper.CreateTable("ImageIndex|int,Id,Pid,Name");
            List<OUInfo> list = new List<OUInfo>();
            if (Portal.gc.IsSuperAdmin)
            {
                list = BLLFactory<OU>.Instance.GetGroupCompany();
            }
            else
            {
                try
                {
                    string condition = string.Format("Id={0} AND IsDelete={1} AND IsForbid={2} AND OuType = {3}", Portal.gc.UserInfo.CompanyId, (short)IsDelete.否, (short)IsForbid.否, (short)OuType.公司);
                    list = BLLFactory<OU>.Instance.Find(condition);
                }
                catch (Exception ex)
                {
                    MessageDxUtil.ShowWarning(ex.Message);
                }
            }

            DataRow dr = null;
            foreach (OUInfo info in list)
            {
                dr = dt.NewRow();
                dr["ImageIndex"] = Portal.gc.GetImageIndex((OuType)info.OuType);
                dr["Id"] = info.Id;
                dr["Pid"] = info.Pid;
                dr["Name"] = info.Name;
                dt.Rows.Add(dr);
            }

            //设置图形序号
            this.treeListLookUpEdit1TreeList.SelectImageList = this.imageList2;
            this.treeListLookUpEdit1TreeList.StateImageList = this.imageList2;
            this.txtCompany.Properties.TreeList.KeyFieldName = "Id";
            this.txtCompany.Properties.TreeList.ParentFieldName = "Pid";
            this.txtCompany.Properties.DataSource = dt;
            this.txtCompany.Properties.ValueMember = "Id";
            this.txtCompany.Properties.DisplayMember = "Name";
        }

        /// <summary>
        /// 公司名称
        /// </summary>
        public override string Text
        {
            get
            {
                return this.txtCompany.Text;
            }
            set
            {
                this.txtCompany.Text = value;
            }
        }

        /// <summary>
        /// 公司Id
        /// </summary>
        public string Value
        {
            get
            {
                string result = "-1";
                if (this.txtCompany.EditValue == null || this.txtCompany.EditValue.ToString() == "0")
                {
                    result = "-1";
                }
                else
                {                  
                    result = this.txtCompany.EditValue.ToString();
                }
                return result;
            }
            set
            {
                this.txtCompany.EditValue = value;
            }
        }

        private void CompanyControl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                Init();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                Init();
            }
        }

        public IStyleController StyleController
        {
            get
            {
                return txtCompany.StyleController;
            }
            set
            {
                txtCompany.StyleController = value;
            }
        }
    }
}
