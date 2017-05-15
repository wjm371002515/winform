using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.CommonControl
{
    public partial class FrmEditDictType : BaseForm
    {
        public string ID = string.Empty;
        public string PID = "";
        public string LoginID = "";

        public FrmEditDictType()
        {
            InitializeComponent();
        }
        
        private void FrmEditDictData_Load(object sender, EventArgs e)
        {
            DictTypeInfo parentInfo = BLLFactory<DictType>.Instance.FindByID(PID);
            if (parentInfo != null)
            {
                this.txtParent.Text = parentInfo.Name;
                this.txtParent.Tag = parentInfo.Name;
            }

            if (!string.IsNullOrEmpty(ID))
            {
                this.Text = "编辑 " + this.Text;
                DictTypeInfo info = BLLFactory<DictType>.Instance.FindByID(ID);
                if (info != null)
                {
                    this.txtName.Text = info.Name;
                    this.txtNote.Text = info.Remark;
                    this.txtSeq.Text = info.Seq;
                    if (info.PID == "-1")
                    {
                        this.chkTopItem.Checked = true;
                    }
                }
                //this.btnOK.Enabled = Portal.gc.HasFunction("Product/Modify");
            }
            else
            {
                this.Text = "新建 " + this.Text;
                //this.btnOK.Enabled = Portal.gc.HasFunction("Product/Add");
            }
            this.txtName.Focus();
        }

        private void SetInfo(DictTypeInfo info)
        {
            info.Editor = LoginID;
            info.LastUpdated = DateTime.Now;
            info.Name = this.txtName.Text.Trim();
            info.Remark = this.txtNote.Text.Trim();
            info.Seq = this.txtSeq.Text;
            info.PID = PID;
            if (this.chkTopItem.Checked)
            {
                info.PID = "-1";
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("请输入项目名称");
                this.txtName.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(ID))
            {
                DictTypeInfo info = BLLFactory<DictType>.Instance.FindByID(ID);
                if (info != null)
                {
                    SetInfo(info);

                    try
                    {
                        bool succeed = BLLFactory<DictType>.Instance.Update(info, info.ID.ToString());
                        if (succeed)
                        {
                            ProcessDataSaved(this.btnOK, new EventArgs());
                            MessageDxUtil.ShowTips("保存成功");
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogTextHelper.Error(ex);
                        MessageDxUtil.ShowError(ex.Message);
                    }
                }
            }
            else
            {
                DictTypeInfo info = new DictTypeInfo();
                SetInfo(info);

                try
                {
                    bool succeed = BLLFactory<DictType>.Instance.Insert(info);
                    if (succeed)
                    {
                        ProcessDataSaved(this.btnOK, new EventArgs());
                        MessageDxUtil.ShowTips("保存成功");
                        this.DialogResult = DialogResult.OK;
                    }
                }
                catch (Exception ex)
                {
                    LogTextHelper.Error(ex);
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkTopItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTopItem.Checked || this.txtParent.Tag == null)
            {
                this.txtParent.Text = "无(顶级项目)";
            }
            else
            {
                this.txtParent.Text = this.txtParent.Tag.ToString();
            }
        }

    }
}
