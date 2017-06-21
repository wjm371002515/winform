using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.UI.Contact
{
    public partial class FrmSelectAddressGroup : BaseDock
    {     
        /// <summary>
        /// 通讯录类型
        /// </summary>
        public AddressType AddressType = AddressType.个人;

        public FrmSelectAddressGroup()
        {
            InitializeComponent();
        }

        private void FrmSelectAddressGroup_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                //BLLFactory<AddressGroup>.Instance.get
                //if (AddressType == AddressType.个人)
                //{ 
                //}
                //else
                //{
                //}
            }
        }
    }
}
