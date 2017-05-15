using JCodes.Framework.CommonControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn
{
    public partial class FrmStatisticReport : BaseDock
    {
        public FrmStatisticReport()
        {
            InitializeComponent();
        }

        private void FrmStatisticReport_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            #region 备件信息统计
            this.treeItemDetail.Nodes.Clear();
            TreeNode node;

            node = new TreeNode("备件属类统计", 0, 0);
            node.Tag = "ItemBigType";
            this.treeItemDetail.Nodes.Add(node);

            node = new TreeNode("备件类别统计", 0, 0);
            node.Tag = "ItemType";
            this.treeItemDetail.Nodes.Add(node);

            node = new TreeNode("备件材质统计", 0, 0);
            node.Tag = "Material";
            this.treeItemDetail.Nodes.Add(node);

            node = new TreeNode("备件名称统计", 0, 0);
            node.Tag = "ItemName";
            this.treeItemDetail.Nodes.Add(node);

            node = new TreeNode("所属库房统计", 0, 0);
            node.Tag = "WareHouse";
            this.treeItemDetail.Nodes.Add(node);

            node = new TreeNode("所属部门统计", 0, 0);
            node.Tag = "Dept";
            this.treeItemDetail.Nodes.Add(node);

            //自定义输入字段统计
            node = new TreeNode("备件数据动态统计", 0, 0);
            node.Tag = "Customed";
            this.treeItemDetail.Nodes.Add(node);
 
            #endregion

            #region 库存报表统计
            this.treeHistory.Nodes.Clear();

            node = new TreeNode("备件库存统计", 0, 0);
            node.Tag = "ItemName";
            this.treeHistory.Nodes.Add(node);

            node = new TreeNode("备件库存属类统计", 0, 0);
            node.Tag = "ItemBigType";
            this.treeHistory.Nodes.Add(node);

            node = new TreeNode("备件出入库统计", 0, 0);
            node.Tag = "Purchase";
            this.treeHistory.Nodes.Add(node);

            #endregion
        }

        private void treeItemDetail_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {                
                string fieldName = e.Node.Tag.ToString();
                if (!string.IsNullOrEmpty(fieldName))
                {
                    this.panelControl1.Controls.Clear();

                    if (fieldName == "Customed")
                    {
                        //自定义动态项目统计图表
                        FrmMultiItemReport control = new FrmMultiItemReport();
                        control.ReportTitle = e.Node.Text;
                        control.Dock = DockStyle.Fill;
                        this.panelControl1.Controls.Add(control);
                    }
                    else
                    {
                        //普通统计图表项目
                        FrmCategoryReport control = new FrmCategoryReport();
                        control.ReportTitle = e.Node.Text;
                        control.FieldName = fieldName;
                        control.Dock = DockStyle.Fill;
                        this.panelControl1.Controls.Add(control);
                    }
                }
            }
        }

        private void treeHistory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                string fieldName = e.Node.Tag.ToString();
                if (!string.IsNullOrEmpty(fieldName))
                {
                    this.panelControl1.Controls.Clear();

                    if (fieldName == "Purchase")
                    {
                        FrmLineHistoryReport control = new FrmLineHistoryReport();
                        control.ReportTitle = e.Node.Text;
                        control.Dock = DockStyle.Fill;
                        this.panelControl1.Controls.Add(control);
                    }
                    else
                    {
                        FrmStockReport control = new FrmStockReport();
                        control.ReportTitle = e.Node.Text;
                        control.FieldName = fieldName;
                        control.Dock = DockStyle.Fill;
                        this.panelControl1.Controls.Add(control);
                    }
                }
            }
        }
    }
}
