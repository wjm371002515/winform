using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.AddIn.Basic;

namespace JCodes.Framework.AddIn.Security
{
    /// <summary>
    /// 角色包含机构
    /// </summary>
    public partial class FrmEditRoleOU : BaseDock
    {
        private Dictionary<int, int> m_SelectOUDict = new Dictionary<int, int>();
        /// <summary>
        /// 选择的部门字典数据（实际数据）
        /// </summary>
        public Dictionary<int, int> SelectOUDict
        {
            get
            {
                return m_SelectOUDict;
            }
            set
            {
                m_SelectOUDict = new Dictionary<int, int>(value);
            }
        }

        public FrmEditRoleOU()
        {
            InitializeComponent();
        }

        private void AddDept(List<OUNodeInfo> list, TreeNode treeNode)
        {
            foreach (OUNodeInfo ouInfo in list)
            {
                TreeNode deptNode = new TreeNode();
                deptNode.Text = ouInfo.Name;
                deptNode.Tag = ouInfo.Id;
                deptNode.ImageIndex = ouInfo.OuType;// Portal.gc.GetImageIndex(ouInfo.Category);
                deptNode.SelectedImageIndex = ouInfo.OuType; // Portal.gc.GetImageIndex(ouInfo.Category);
                if (ouInfo.IsDelete == 0)
                {
                    deptNode.ForeColor = Color.Red;
                    continue;//跳过不显示
                }
                deptNode.Checked = SelectOUDict.ContainsKey(ouInfo.Id);//选中的
                treeNode.Nodes.Add(deptNode);

                AddDept(ouInfo.Children, deptNode);
            }
        }

        private void RefreshTreeView()
        {
            this.treeView1.BeginUpdate();
            Cursor.Current = Cursors.WaitCursor;
            this.treeView1.Nodes.Clear();

            List<OUInfo> list = Portal.gc.GetMyTopGroup();
            foreach (OUInfo groupInfo in list)
            {
                if (groupInfo != null)
                {
                    TreeNode topnode = new TreeNode();
                    topnode.Text = groupInfo.Name;
                    topnode.Name = groupInfo.Id.ToString();
                    topnode.Tag = groupInfo.Id;
                    topnode.ImageIndex = groupInfo.OuType;// Portal.gc.GetImageIndex(groupInfo.Category);
                    topnode.SelectedImageIndex = groupInfo.OuType;// Portal.gc.GetImageIndex(groupInfo.Category);
                    topnode.Checked = SelectOUDict.ContainsKey(groupInfo.Id);//选中的

                    List<OUNodeInfo> sublist = BLLFactory<OU>.Instance.GetTreeByID(groupInfo.Id);
                    AddDept(sublist, topnode);

                    this.treeView1.Nodes.Add(topnode);
                }
            }

            this.treeView1.ExpandAll();
            this.treeView1.EndUpdate();
            Cursor.Current = Cursors.Default;
        }

        private void FrmEditRoleOU_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                RefreshTreeView();
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (TreeNode node in this.treeView1.Nodes)
            {
                CheckSelect(node, this.chkAll.Checked);
            }
        }

        private void CheckSelect(TreeNode node, bool selectAll)
        {
            node.Checked = selectAll;
            foreach (TreeNode subNode in node.Nodes)
            {
                CheckSelect(subNode, selectAll);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>();
            foreach (TreeNode node in this.treeView1.Nodes)
            {
                list.AddRange(GetSelected(node));
            }

            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (int id in list)
            {
                if (!dict.ContainsKey(id))
                {
                    dict.Add(id, id);
                }
            }

            this.SelectOUDict = dict;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private List<int> GetSelected(TreeNode node)
        {
            List<int> list = new List<int>();
            foreach (TreeNode subNode in node.Nodes)
            {
                if (subNode.Checked && subNode.Tag != null)
                {
                    list.Add(subNode.Tag.ToString().ToInt32());
                }
                list.AddRange(GetSelected(subNode));
            }
            return list;
        }
    }
}
