using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.AddIn.Basic;

namespace JCodes.Framework.AddIn.UI.BizControl
{
    /// <summary>
    /// 部门显示控件
    /// </summary>
    public partial class DeptControl : UserControl, ISupportStyleController
    {
        public Int32 ParentOuId = -1;

        /// <summary>
        /// 选择的值发生变化的时候
        /// </summary>
        public event EventHandler EditValueChanged;

        public DeptControl()
        {
            InitializeComponent();

            this.txtDept.EditValueChanged += new EventHandler(cmbUpperOU_EditValueChanged);
        }

        void cmbUpperOU_EditValueChanged(object sender, EventArgs e)
        {
            if (EditValueChanged != null)
            {
                EditValueChanged(sender, e);
            }
        }

        private void DeptControl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                //限定用户的选择级别
                List<OUInfo> list = Portal.gc.GetMyTopGroup();
                foreach (OUInfo ouInfo in list)
                {
                    if (ouInfo != null)
                    {
                        this.ParentOuId = ouInfo.Id;
                    }
                }

                Init();
            }
        }

        /// <summary>
        /// 初始化部门信息
        /// </summary>
        public void Init()
        { 
            DataTable dt = DataTableHelper.CreateTable("ImageIndex|int,Id,Pid,Name,OuCode,OuType,Address,Remark");
            DataRow dr = null;

            if (ParentOuId > -1)
            {
                List<OUInfo> list = BLLFactory<OU>.Instance.GetAllOUsByParent(ParentOuId);
                OUInfo parentInfo = BLLFactory<OU>.Instance.FindById(ParentOuId);
                if (parentInfo != null)
                {
                    list.Insert(0, parentInfo);
                }
                
                foreach (OUInfo info in list)
                {
                    dr = dt.NewRow();
                    dr["ImageIndex"] = info.OuType;
                    dr["Id"] = info.Id;
                    dr["Pid"] = info.Pid;
                    dr["Name"] = info.Name;
                    dr["OuCode"] = info.OuCode;
                    dr["OuType"] = info.OuType;
                    dr["Address"] = info.Address;
                    dr["Remark"] = info.Remark;

                    dt.Rows.Add(dr);
                }
            }
            //增加一行空的
            dr = dt.NewRow();
            dr["Id"] = "0"; //使用0代替-1，避免出现节点的嵌套显示，因为-1已经作为了一般节点的顶级标识
            dr["Pid"] = "-1";
            dr["Name"] = "无";
            dt.Rows.InsertAt(dr, 0);

            //设置图形序号
            this.treeListLookUpEdit1TreeList.SelectImageList = this.imageList2;
            this.treeListLookUpEdit1TreeList.StateImageList = this.imageList2;

            this.txtDept.Properties.TreeList.KeyFieldName = "Id";
            this.txtDept.Properties.TreeList.ParentFieldName = "Pid";
            this.txtDept.Properties.DataSource = dt;
            this.txtDept.Properties.ValueMember = "Id";
            this.txtDept.Properties.DisplayMember = "Name";
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public override string Text
        {
            get
            {
                return this.txtDept.Text;
            }
            set
            {
                this.txtDept.Text = value;
            }
        }

        /// <summary>
        /// 部门ID
        /// </summary>
        public string Value
        {
            // 值为“0”的时候，是默认的“无”行记录
            get
            {
                string result = "-1";
                if (this.txtDept.EditValue == null || this.txtDept.EditValue.ToString() == "0")
                {
                    result = "-1";
                }
                else
                {
                    result = this.txtDept.EditValue.ToString();
                }
                return result;
            }
            set
            {
                if (value == "-1" )
                {
                    this.txtDept.EditValue = "0";
                }
                else
                {
                    this.txtDept.EditValue = value;
                }
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
                return txtDept.StyleController;
            }
            set
            {
                txtDept.StyleController = value;
            }
        }
    }
}
