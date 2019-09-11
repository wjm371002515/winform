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
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.AddIn.Basic.BizControl
{
    /// <summary>
    /// 菜单显示控件
    /// </summary>
    public partial class MenuControl : UserControl
    {
        /// <summary>
        /// 选择的值发生变化的时候
        /// </summary>
        public event EventHandler EditValueChanged;

        public MenuControl()
        {
            InitializeComponent();

            this.txtMenu.EditValueChanged += new EventHandler(txtMenu_EditValueChanged);
        }

        void txtMenu_EditValueChanged(object sender, EventArgs e)
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
            DataTable dt = DataTableHelper.CreateTable("ImageIndex|int,ID,PID,Name,FunctionId,SystemType_ID,Seq");
            List<MenuInfo> list = BLLFactory<Menus>.Instance.GetAll();
            DataRow dr = null;
            foreach (MenuInfo info in list)
            {
                dr = dt.NewRow();
                dr["ImageIndex"] = 0;
                dr["Gid"] = info.Gid.ToString();
                dr["Pgid"] = info.Pgid.ToString();
                dr["Name"] = info.Name;
                dr["AuthGid"] = info.AuthGid;
                dr["SystemtypeId"] = info.SystemtypeId;
                dr["Seq"] = info.Seq;
                dt.Rows.Add(dr);
            }
            //增加一行空的
            dr = dt.NewRow();
            dr["ID"] = "0"; //使用0代替-1，避免出现节点的嵌套显示，因为-1已经作为了一般节点的顶级标识
            dr["PID"] = "-1";
            dr["Name"] = "无";
            dt.Rows.InsertAt(dr, 0);

            //设置图形序号
            this.treeListLookUpEdit1TreeList.SelectImageList = this.imageList2;
            this.treeListLookUpEdit1TreeList.StateImageList = this.imageList2;

            this.txtMenu.Properties.TreeList.KeyFieldName = "ID";
            this.txtMenu.Properties.TreeList.ParentFieldName = "PID";
            this.txtMenu.Properties.DataSource = dt;
            this.txtMenu.Properties.ValueMember = "ID";
            this.txtMenu.Properties.DisplayMember = "Name";
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public override string Text
        {
            get
            {
                return this.txtMenu.Text;
            }
            set
            {
                this.txtMenu.Text = value;
            }
        }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public string Value
        {
            // 值为“0”的时候，是默认的“无”行记录
            get
            {
                string result = "-1";
                if (this.txtMenu.EditValue == null || this.txtMenu.EditValue.ToString() == "0")
                {
                    result = "-1";
                }
                else
                {
                    result = this.txtMenu.EditValue.ToString();
                }
                return result;
            }
            set
            {
                if (value == "-1")
                {
                    this.txtMenu.EditValue = "0";
                }
                else
                {
                    this.txtMenu.EditValue = value;
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
    }
}
