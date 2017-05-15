using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
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
    public partial class FrmEditDistrict : BaseForm
    {        
        public string ID = string.Empty;
        public string LoginID = "";//登陆用户ID 
        private DistrictInfo tempInfo = new DistrictInfo();

        public FrmEditDistrict()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            tempInfo.DistrictName = this.txtDistrict.Text;
            tempInfo.CityID = Convert.ToInt32(this.txtCity.Tag.ToString());

            try
            {
                bool succeed = false;
                if (string.IsNullOrEmpty(ID))
                {
                    succeed = BLLFactory<District>.Instance.Insert(tempInfo);
                }
                else
                {
                    succeed = BLLFactory<District>.Instance.Update(tempInfo, tempInfo.ID);
                }

                ProcessDataSaved(this.btnOK, new EventArgs());
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                LogTextHelper.Error(ex);
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        private void FrmEditCityDistrict_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                DistrictInfo info = BLLFactory<District>.Instance.FindByID(ID);
                if (info != null)
                {
                    tempInfo = info;
                    this.txtDistrict.Text = info.DistrictName;
                }
            }
        }
    }
}
