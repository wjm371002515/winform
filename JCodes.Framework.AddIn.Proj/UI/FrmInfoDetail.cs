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
using JCodes.Framework.CommonControl.Controls;

namespace JCodes.Framework.AddIn.Proj
{
    public partial class FrmInfoDetail : BaseEditForm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="infoType">0 错误， 1警告， 2提示信息</param>
        public FrmInfoDetail(Int32 infoType, List<CListItem> lst)
        {
            InitializeComponent();

            InitView(infoType, lst);
        }

        private void InitView(Int32 infoType, List<CListItem> lst)
        {
            if (infoType == 0)
                this.Text = "错误信息明细";
            if (infoType == 1)
                this.Text = "警告信息明细";
            if (infoType == 2)
                this.Text = "提示信息明细";

            winGridView1.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置
            winGridView1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
            winGridView1.DisplayColumns = "Text,Value";
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Text", "描述");
            dict.Add("Value", "错误信息");
            winGridView1.ColumnNameAlias = dict;
            winGridView1.DataSource = lst;
        }

        /// <summary>
        /// 绑定数据后，分配各列的宽度
        /// </summary>
        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (winGridView1.gridView1.Columns.Count > 0 && winGridView1.gridView1.RowCount > 0)
            {
                //可特殊设置特别的宽度 
                winGridView1.gridView1.SetGridColumWidth("Text", 150);
                winGridView1.gridView1.SetGridColumWidth("Value", 280);
            }
        }
    }
}
