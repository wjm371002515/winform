using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.CommonControl
{
    /// <summary>
    /// 询问一个输入字符串的窗体
    /// </summary>
    public partial class QueryInputDialog : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 是否需要对输入框进行屏蔽显示（用于密码信息）
        /// </summary>
        public bool IsEncryptInput { get; set; }

        /// <summary>
        /// 构造器
        /// </summary>
        public QueryInputDialog()
        {
            InitializeComponent();
        }

        private void QueryInputDialog_Load(object sender, EventArgs e)
        {
            if (this.IsEncryptInput)
            {
                this.txtInput.Properties.PasswordChar = '*';
                this.txtInput.Properties.UseSystemPasswordChar = true;
            }
        }
    }
}
