using JCodes.Framework.Common;
using JCodes.Framework.Common.Network;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestControlUtil
{
    public partial class FrmADSLDialer : Form
    {
        ADSLDialerUtil util = new ADSLDialerUtil();

        public FrmADSLDialer()
        {
            InitializeComponent();

            util.OnHandle += new ADSLHandleDelegate(util_OnHandle);
        }

        void util_OnHandle(string result)
        {
            tsTips.Text = result;
        }

        private void FrmADSLDialer_Load(object sender, EventArgs e)
        {
            this.txtRasName.Items.Clear();
            CListItem item = new CListItem("", "请选择一个链接...");
            this.txtRasName.Items.Add(item);

            List<CListItem> list = util.InitRAS();
            this.txtRasName.Items.AddRange(list.ToArray());
        }

        private void btnRedail_Click(object sender, EventArgs e)
        {
            CListItem item = this.txtRasName.SelectedItem as CListItem;
            if (item != null && !string.IsNullOrEmpty(item.Value))
            {
                util.ReConnect(item.Text);
            }
        }

        private void txtRasName_SelectedIndexChanged(object sender, EventArgs e)
        {
            CListItem item = this.txtRasName.SelectedItem as CListItem;
            if (item != null && !string.IsNullOrEmpty(item.Value))
            {
                this.txtIP.Text = util.GetNewIP(item.Value);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            util.DisConnect();
        }
    }
}
