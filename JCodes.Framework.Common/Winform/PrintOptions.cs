using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 打印选项
    /// </summary>
    public partial class PrintOptions : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PrintOptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="availableFields">可见字段列表</param>
        public PrintOptions(List<string> availableFields)
        {
            InitializeComponent();

            foreach (string field in availableFields)
                     chklst.Items.Add(field, true);
        }

        private void PrintOtions_Load(object sender, EventArgs e)
        {
            // Initialize some controls
            rdoAllRows.Checked = true;
            chkFitToPageWidth.Checked = true; 
            this.txtTitle.Text = this.PrintTitle;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 获取选择的列
        /// </summary>
        /// <returns></returns>
        public List<string> GetSelectedColumns()
        {
            List<string> lst = new List<string>();
            foreach (object item in chklst.CheckedItems)
                    lst.Add(item.ToString());
            return lst;
        }

        /// <summary>
        /// 打印标题
        /// </summary>
        public string PrintTitle
        {
            get { return txtTitle.Text; }
            set { this.txtTitle.Text = value;}
        }

        /// <summary>
        /// 是否打印所有行
        /// </summary>
        public bool PrintAllRows
        {
            get { return rdoAllRows.Checked; }
        }

        /// <summary>
        /// 是否适宽
        /// </summary>
        public bool FitToPageWidth
        {
            get { return chkFitToPageWidth.Checked; }
        }

    }
}