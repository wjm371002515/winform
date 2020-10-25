using System;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common;

namespace JCodes.Framework.AddIn.Basic.BizControl
{
    public partial class UserNameControl : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void DeleteEventHandler(Int32 Id);
        public event DeleteEventHandler OnDeleteItem;

        public UserNameControl()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OnDeleteItem != null)
            {
                if (this.lblInfo.Tag != null)
                {
                    OnDeleteItem(ConvertHelper.ToInt32(lblInfo.Tag, Const.Num_Zero));
                }
            }
        }

        public void BindData(Int32 Id, string Name)
        {
            this.lblInfo.Text = Name;
            this.lblInfo.Tag = Id;
            this.btnDelete.Tag = Id;
        }
    }
}
