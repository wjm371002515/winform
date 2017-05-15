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

namespace JCodes.Framework.CommonControl
{
    /// <summary>
    /// 公司显示控件
    /// </summary>
    public partial class CompanyControl : XtraUserControl
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
            DataTable dt = DataTableHelper.CreateTable("ImageIndex|int,ID,PID,Name");
            List<OUInfo> list = new List<OUInfo>();
            if(Portal.gc.UserInRole(RoleInfo.SuperAdminName))
            {
                list = BLLFactory<OU>.Instance.GetGroupCompany();
            }
            else
            {
                OUInfo myCompanyInfo = BLLFactory<OU>.Instance.FindByID(Portal.gc.UserInfo.Company_ID);
                if (myCompanyInfo != null)
                {
                    list.Add(myCompanyInfo);
                }
            }

            DataRow dr = null;
            foreach (OUInfo info in list)
            {
                dr = dt.NewRow();
                dr["ImageIndex"] = Portal.gc.GetImageIndex(info.Category);
                dr["ID"] = info.ID.ToString();
                dr["PID"] = info.PID.ToString();
                dr["Name"] = info.Name;
                dt.Rows.Add(dr);
            }

            ////增加一行空的
            //dr = dt.NewRow();
            //dr["ID"] = "0";
            //dr["PID"] = "-1";
            //dr["Name"] = "无";
            //dt.Rows.InsertAt(dr, 0);

            //设置图形序号
            this.treeListLookUpEdit1TreeList.SelectImageList = this.imageList2;
            this.treeListLookUpEdit1TreeList.StateImageList = this.imageList2;

            this.txtCompany.Properties.TreeList.KeyFieldName = "ID";
            this.txtCompany.Properties.TreeList.ParentFieldName = "PID";
            this.txtCompany.Properties.DataSource = dt;
            this.txtCompany.Properties.ValueMember = "ID";
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
        /// 公司ID
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
    }
}
