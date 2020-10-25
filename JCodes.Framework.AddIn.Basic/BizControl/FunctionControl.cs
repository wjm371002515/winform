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

namespace JCodes.Framework.AddIn.UI.BizControl
{
    /// <summary>
    /// 功能显示控件
    /// </summary>
    public partial class FunctionControl : UserControl, ISupportStyleController
    {
        /// <summary>
        /// 选择的值发生变化的时候
        /// </summary>
        public event EventHandler EditValueChanged;

        public FunctionControl()
        {
            InitializeComponent();

            this.txtFunction.EditValueChanged += new EventHandler(txtFunction_EditValueChanged);
        }

        void txtFunction_EditValueChanged(object sender, EventArgs e)
        {
            if (EditValueChanged != null)
            {
                EditValueChanged(sender, e);
            }
        }

        private void FunctionControl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                Init();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void Init()
        {
            //InitUpperFunction
            DataTable dt = DataTableHelper.CreateTable("ImageIndex|int,Gid,Pgid,Name,DllPath,SystemtypeId,Seq");
            List<FunctionInfo> list = BLLFactory<Function>.Instance.GetAll();
            DataRow dr = null;
            foreach (FunctionInfo info in list)
            {
                dr = dt.NewRow();
                dr["ImageIndex"] = 0;
                dr["Gid"] = info.Gid;
                dr["Pgid"] = info.Pgid;
                dr["Name"] = info.Name;
                dr["DllPath"] = info.DllPath;
                dr["SystemtypeId"] = info.SystemtypeId;
                dr["Seq"] = info.Seq;
                dt.Rows.Add(dr);
            }
            //增加一行空的
            dr = dt.NewRow();
            dr["Gid"] = "0"; //使用0代替-1，避免出现节点的嵌套显示，因为-1已经作为了一般节点的顶级标识
            dr["Pgid"] = "-1";
            dr["Name"] = "无";
            dt.Rows.InsertAt(dr, 0);

            //设置图形序号
            this.treeListLookUpEdit1TreeList.SelectImageList = this.imageList2;
            this.treeListLookUpEdit1TreeList.StateImageList = this.imageList2;

            this.txtFunction.Properties.TreeList.KeyFieldName = "Gid";
            this.txtFunction.Properties.TreeList.ParentFieldName = "Pgid";
            this.txtFunction.Properties.DataSource = dt;
            this.txtFunction.Properties.ValueMember = "Gid";
            this.txtFunction.Properties.DisplayMember = "Name";
        }

        /// <summary>
        /// 功能名称
        /// </summary>
        public override string Text
        {
            get
            {
                return this.txtFunction.Text;
            }
            set
            {
                this.txtFunction.Text = value;
            }
        }

        /// <summary>
        /// 功能ID
        /// </summary>
        public string Value
        {
            // 值为“0”的时候，是默认的“无”行记录
            get
            {
                string result = "-1";
                if (this.txtFunction.EditValue == null || this.txtFunction.EditValue.ToString() == "0")
                {
                    result = "-1";
                }
                else
                {
                    result = this.txtFunction.EditValue.ToString();
                }
                return result;
            }
            set
            {
                if (value == "-1")
                {
                    this.txtFunction.EditValue = "0";
                }
                else
                {
                    this.txtFunction.EditValue = value;
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
                return txtFunction.StyleController;
            }
            set
            {
                txtFunction.StyleController = value;
            }
        }
    }
}
