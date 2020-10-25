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
using JCodes.Framework.jCodesenum;
using JCodes.Framework.Common.Format;
using DevExpress.XtraEditors.Controls;

namespace JCodes.Framework.AddIn.UI.BizControl
{
    /// <summary>
    /// 部门显示控件
    /// </summary>
    public partial class DictControl : UserControl, ISupportStyleController
    {
        #region 自定义属性
        private Int32 m_DicNo = 0;

        /// <summary>
        /// 选择的值发生变化的时候
        /// </summary>
        public event EventHandler EditValueChanged;

        [Description("数据字典值"), Category("自定义")]
        public Int32 DicNo
        {
            get { return m_DicNo; }
            set
            {
                m_DicNo = value;
               
                InitData();
            }
        }

        [Description("获取选中的内容"), Category("自定义")]
        public object SelectedDataRow
        {
            get
            {
                return this.lueDic.GetSelectedDataRow();
            }
        }

        public object EditValue
        {
            set
            {
                this.lueDic.EditValue = value;
            }
            get
            {
                return this.lueDic.EditValue;
            }
        }

        #endregion

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData() {
            if (!this.DesignMode && m_DicNo > 0)
            {
                List<DicKeyValueInfo> dics = BLLFactory<DictData>.Instance.GetDictByTypeId(m_DicNo);
                this.lueDic.Properties.DataSource = dics;
                this.lueDic.Properties.ValueMember = "DicttypeValue";
                this.lueDic.Properties.DisplayMember = "Name";
                this.lueDic.Properties.NullText = "";
                this.lueDic.Properties.Columns.Clear();  //防止刷新出现重复列
                this.lueDic.Properties.Columns.Add(new LookUpColumnInfo("DicttypeValue", "数据字典值"));
                this.lueDic.Properties.Columns.Add(new LookUpColumnInfo("Name", "数据字典内容"));
            }
        }

        public DictControl()
        {
            InitializeComponent();

            // 变更
            this.lueDic.EditValueChanged += new EventHandler(lueDic_EditValueChanged);
        }

        private void lueDic_EditValueChanged(object sender, EventArgs e)
        {
            if (EditValueChanged != null)
            {
                EditValueChanged(sender, e);
            }
        }

        private void DictControl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                InitData();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                InitData();
            }
        }

        public IStyleController StyleController
        {
            get
            {
                return lueDic.StyleController;
            }
            set
            {
                lueDic.StyleController = value;
            }
        }
     
    }
}
