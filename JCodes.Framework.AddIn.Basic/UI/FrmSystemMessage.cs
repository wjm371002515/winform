using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.Basic
{
    public partial class FrmSystemMessage : BaseDock
    {
        public FrmSystemMessage()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // TODO
            /*string wareHouse = BLLFactory<WareHouse>.Instance.GetAllWareHouse()[0].Value;

            //低库存预警检查
            bool lowWarning = BLLFactory<Stock>.Instance.CheckStockLowWarning(wareHouse);
            if (lowWarning)
            {
                string message = string.Format("{0} 库存已经处于低库存预警状态\r\n请及时补充库存", wareHouse);
                Portal.gc.Notify(string.Format("{0} 低库存预警", wareHouse), message);
            }

            //超库存预警检查
            bool highWarning = BLLFactory<Stock>.Instance.CheckStockHighWarning(wareHouse);
            if (highWarning)
            {
                string message = string.Format("{0} 库存量已经高过超预警库存量\r\n请注意减少库存积压", wareHouse);
                Portal.gc.Notify(string.Format("{0} 超库存预警", wareHouse), message);
            }

            if (!lowWarning && !highWarning)
            {
                string message = string.Format("暂无相关的系统提示信息");
                Portal.gc.Notify(message, message);
            }*/
        }
    }
}
