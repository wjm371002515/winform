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
using JCodes.Framework.AddIn.Other;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.Other;

namespace JCodes.Framework.AddIn.UI.BizControl
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
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "1", typeof(CompanyControl));
            DataTable dt = DataTableHelper.CreateTable("ImageIndex|int,ID,PID,Name");
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "2", typeof(CompanyControl));
            List<OUInfo> list = new List<OUInfo>();
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "3", typeof(CompanyControl));
            if(Portal.gc.UserInRole(RoleInfo.SuperAdminName))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "4", typeof(CompanyControl));
                list = BLLFactory<OU>.Instance.GetGroupCompany();
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "5", typeof(CompanyControl));
            }
            else
            {
                try
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "6", typeof(CompanyControl));
                    OUInfo myCompanyInfo = BLLFactory<OU>.Instance.FindByID(Portal.gc.UserInfo.Company_ID);
                    if (myCompanyInfo != null)
                    {
                        list.Add(myCompanyInfo);
                    }
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "7", typeof(CompanyControl));
                }
                catch (Exception ex)
                {
                    MessageDxUtil.ShowWarning(ex.Message);
                }
            }

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "8", typeof(CompanyControl));
            DataRow dr = null;
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "9", typeof(CompanyControl));
            foreach (OUInfo info in list)
            {
                dr = dt.NewRow();
                dr["ImageIndex"] = Portal.gc.GetImageIndex(info.Category);
                dr["ID"] = info.ID.ToString();
                dr["PID"] = info.PID.ToString();
                dr["Name"] = info.Name;
                dt.Rows.Add(dr);
            }
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "10", typeof(CompanyControl));

            ////增加一行空的
            //dr = dt.NewRow();
            //dr["ID"] = "0";
            //dr["PID"] = "-1";
            //dr["Name"] = "无";
            //dt.Rows.InsertAt(dr, 0);

            //设置图形序号

            this.treeListLookUpEdit1TreeList.SelectImageList = this.imageList2;
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "11", typeof(CompanyControl));
            this.treeListLookUpEdit1TreeList.StateImageList = this.imageList2;
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "12", typeof(CompanyControl));

            this.txtCompany.Properties.TreeList.KeyFieldName = "ID";
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "13", typeof(CompanyControl));
            this.txtCompany.Properties.TreeList.ParentFieldName = "PID";
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "14", typeof(CompanyControl));
            this.txtCompany.Properties.DataSource = dt;
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "15", typeof(CompanyControl));
            this.txtCompany.Properties.ValueMember = "ID";
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "16", typeof(CompanyControl));
            this.txtCompany.Properties.DisplayMember = "Name";
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_SQL, "17", typeof(CompanyControl));
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
