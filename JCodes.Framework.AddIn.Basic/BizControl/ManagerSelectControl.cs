using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.BLL;

namespace JCodes.Framework.AddIn.Basic.BizControl
{
    /// <summary>
    /// 库房管理人员的选择控件封装
    /// </summary>
    public partial class ManagerSelectControl : XtraUserControl
    {     
        /// <summary>
        /// 选择项发生变化的事件处理
        /// </summary>
        public event EventHandler SelectedValueChanged;

        public ManagerSelectControl()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                string result = "";
                if (this.txtOperator.EditValue != null)
                {
                    result = this.txtOperator.EditValue.ToString();
                }
                return result;
            }
            set
            {
                this.txtOperator.EditValue = value;
            }
        }

        /// <summary>
        /// 标题：获取一个值，用以指示 System.ComponentModel.Component 当前是否处于设计模式。
        /// 描述：DesignMode 在 Visual Studio 2005 产品中存在 Bug ，使用下面的方式可以解决这个问题。
        ///        详细信息地址：http://support.microsoft.com/?scid=kb;zh-cn;839202&x=10&y=15
        /// </summary>
        protected new bool DesignMode
        {
            get
            {
                bool returnFlag = false;
#if DEBUG
                if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                {
                    returnFlag = true;
                }
                else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper().Equals("DEVENV"))
                {
                    returnFlag = true;
                }
#endif
                return returnFlag;
            }
        }

        private void OperatorSelectControl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                txtOperator.Properties.ValueMember = "Name";
                txtOperator.Properties.DisplayMember = "FullName";
                txtOperator.Properties.DataSource = BLLFactory<User>.Instance.GetSimpleUsers();
            }
        }

        private void txtOperator_EditValueChanged(object sender, EventArgs e)
        {
            if (SelectedValueChanged != null)
            {
                SelectedValueChanged(sender, e);
            }
        }
    }
}
