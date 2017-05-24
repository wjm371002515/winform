using JCodes.Framework.AddIn.Other;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.UI.WareHouse
{
    /// <summary>
    /// 设置库房对应关系
    /// </summary>
    public partial class SetWareHouseAlias : BaseForm
    {
        public Dictionary<string, string> WareHouseDict = new Dictionary<string, string>();

        public SetWareHouseAlias()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool found = false;
            this.DialogResult = DialogResult.None;
            foreach (System.Windows.Forms.Control control in this.tableLayoutPanel1.Controls)
            {
                ComboBox txtAlias = control as ComboBox;
                if (txtAlias != null && txtAlias.Text.Length == 0)
                {
                    found = true;
                    txtAlias.Focus();
                    break;
                }
            }
            if (found)
            {
                MessageDxUtil.ShowTips("请设置库房对应关系");
                this.DialogResult = DialogResult.None;
            }
            else
            {
                if (MessageDxUtil.ShowYesNoAndWarning("您确定库房关系正确，并提交数据？") == DialogResult.Yes)
                {
                    foreach (System.Windows.Forms.Control control in this.tableLayoutPanel1.Controls)
                    {
                        ComboBox txtAlias = control as ComboBox;
                        if (txtAlias != null && txtAlias.Text.Length > 0)
                        {
                            WareHouseDict[txtAlias.Name] = txtAlias.Text;
                        }
                    }

                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void SetColumnAliasForm_Load(object sender, EventArgs e)
        {
            //SetColumnAliasForm form = new SetColumnAliasForm();
            const int heightEvery = 25;//每行元素的高度
            const int reservedLength = 60;//最后按钮修改预留的高度
            int elementsHeight = WareHouseDict.Count * heightEvery;

            int newHeight = Size.Height;
            if ((elementsHeight + reservedLength) > Size.Height)
            {
                newHeight = elementsHeight + reservedLength;
            }
            Size = new System.Drawing.Size(Size.Width, newHeight);

            foreach(string key in WareHouseDict.Keys)
            {
                Label lblColumName = new Label();
                lblColumName.Font =  new Font("宋体", 11f, FontStyle.Bold);
                lblColumName.ForeColor = Color.Blue;
                lblColumName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top)
                        | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
                lblColumName.Text = string.Format("库房编号：{0}", key);

                ComboBox txtWareHouse = new ComboBox();
                txtWareHouse.DropDownStyle = ComboBoxStyle.DropDownList;
                txtWareHouse.Name = key;
                txtWareHouse.Tag = key;
                txtWareHouse.Items.Clear();
                txtWareHouse.Items.AddRange(WareHouseHelper.GetWareHouse(LoginUserInfo.ID, LoginUserInfo.Name).ToArray());
                //txtWareHouse.SelectedIndex = 0;

                txtWareHouse.Size = new System.Drawing.Size(200, 25);
                txtWareHouse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top)
                        | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));

                tableLayoutPanel1.Controls.Add(lblColumName);
                tableLayoutPanel1.Controls.Add(txtWareHouse);
            }
        }

    }
}