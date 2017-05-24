using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.CommonControl.BaseUI
{
    public delegate void PostionChangedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// 记录导航控件
    /// </summary>
    public partial class DataNavigator : DevExpress.XtraEditors.XtraUserControl
    {
        public event PostionChangedEventHandler PositionChanged;
        private int m_CurrentIndex = 0;//当前的位置
        public List<string> IDList = new List<string>();

        /// <summary>
        /// 获取或设置索引值
        /// </summary>
        public int CurrentIndex
        {
            get { return m_CurrentIndex; }
            set 
            {
                m_CurrentIndex = value;
                ChangePosition(value);
            }
        }
        public DataNavigator()
        {
            InitializeComponent();            
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            ChangePosition(0);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            ChangePosition(m_CurrentIndex - 1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ChangePosition(m_CurrentIndex + 1);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            ChangePosition(IDList.Count - 1);
        }

        private void EnableControl(bool enable)
        {
            this.btnFirst.Enabled = enable;
            this.btnLast.Enabled = enable;
            this.btnNext.Enabled = enable;
            this.btnPrevious.Enabled = enable;
        }

        private void ChangePosition(int newPos)
        {
            int count = IDList.Count;
            if (count == 0)
            {
                EnableControl(false);
                this.txtInfo.Text = "";
            }
            else
            {
                EnableControl(true);

                newPos = (newPos < 0) ? 0 : newPos;
                m_CurrentIndex = ((count - 1) > newPos) ? newPos : (count - 1);
                this.btnPrevious.Enabled = (m_CurrentIndex > 0);
                this.btnNext.Enabled = m_CurrentIndex < (count - 1);
                this.txtInfo.Text = string.Format("{0}/{1}", m_CurrentIndex + 1, count);

                if (PositionChanged != null)
                {
                    PositionChanged(this, new EventArgs());
                }
            }
        }

        private void DataNavigator_Load(object sender, EventArgs e)
        {
        }
    }
}
