using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;

namespace JCodes.Framework.AddIn.UI.WareHouse
{
    public partial class FrmEditWareHouse : BaseForm
    {
        public string ID = string.Empty;

        public FrmEditWareHouse()
        {
            InitializeComponent();
            InitDictItem();
        }

        private void InitDictItem()
        {
        }

        private void SetInfo(WareHouseInfo info)
        {
            info.Name = txtName.Text;
            info.Address = txtAddress.Text;
            info.Manager = txtManager.Text;
            info.Phone = txtPhone.Text;
            info.Note = txtNote.Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("库房名称不能为空");
                this.txtName.Focus();
                return;
            }
            if (this.txtManager.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowTips("库房负责人不能为空");
                this.txtManager.Focus();
                return;
            }
            
            if (!string.IsNullOrEmpty(ID))
            {
                WareHouseInfo info = BLLFactory<BLL.WareHouse>.Instance.FindByID(ID);
                if (info != null)
                {
                    SetInfo(info);

                    try
                    {
                        bool succeed = BLLFactory<BLL.WareHouse>.Instance.Update(info, info.ID.ToString());
                        if (succeed)
                        {
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
                WareHouseInfo info = new WareHouseInfo();
                SetInfo(info);

                try
                {
                    bool succeed = BLLFactory<BLL.WareHouse>.Instance.Insert(info);
                    if (succeed)
                    {
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

        private void FrmEditWareHouse_Load(object sender, EventArgs e)
        {
            //this.btnOK.Enabled = HasFunction("BasicInfo/Manufacture");
            if (!string.IsNullOrEmpty(ID))
            {
                this.Text = "编辑 " + this.Text;
                WareHouseInfo info = BLLFactory<BLL.WareHouse>.Instance.FindByID(ID);
                if (info != null)
                {
                    txtName.Text = info.Name;
                    txtAddress.Text = info.Address;
                    txtNote.Text = info.Note;
                    txtManager.Text = info.Manager;
                    txtManager.Tag = info.Manager;
                    txtPhone.Text = info.Phone;
                }
                this.btnOK.Enabled = HasFunction("WareHouse");
            }
            else
            {
                this.Text = "新建 " + this.Text;
            }
        }
    }
}
