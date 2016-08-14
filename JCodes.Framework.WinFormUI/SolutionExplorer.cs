using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;

namespace JCodes.Framework.WinFormUI
{
    public partial class SolutionExplorer : System.Windows.Forms.UserControl {
        public SolutionExplorer() {
            InitializeComponent();
            InitTreeView(treeView1);
            treeView1.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(treeView1_CustomDrawNodeCell);
            treeView1.AfterCollapse += new DevExpress.XtraTreeList.NodeEventHandler(treeView1_AfterCollapse);
            treeView1.AfterExpand += new DevExpress.XtraTreeList.NodeEventHandler(treeView1_AfterExpand);
            AddAllNodes(iShow.Down);
        }
        public static void InitTreeView(DevExpress.XtraTreeList.TreeList treeView) {
            TreeListColumn column = treeView.Columns.Add();
            column.Visible = true;
            treeView.OptionsView.ShowColumns = false;
            treeView.OptionsView.ShowIndicator = false;
            treeView.OptionsView.ShowVertLines = false;
            treeView.OptionsView.ShowHorzLines = false;
            treeView.OptionsBehavior.Editable = false;
            treeView.OptionsSelection.EnableAppearanceFocusedCell = false;
        }
        void treeView1_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e) {
            if(e.Node.Id == 1) e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
        }

        private void SetIndex(TreeListNode node, int index, bool expand) {
            int newIndex = expand ? index - 1 : index + 1;
            if(node.StateImageIndex == index)
                node.StateImageIndex = newIndex;
        }

        void treeView1_AfterExpand(object sender, DevExpress.XtraTreeList.NodeEventArgs e) {
            SetIndex(e.Node, 7, true);
            SetIndex(e.Node, 9, true);
        }

        void treeView1_AfterCollapse(object sender, DevExpress.XtraTreeList.NodeEventArgs e) {
            SetIndex(e.Node, 6, false);
            SetIndex(e.Node, 8, false);
        }

        private void AddAllNodes(bool showAll) {
            treeView1.Nodes.Clear();
            treeView1.AppendNode(new object[] { "Solution \'DockingDemo\' (1 project)" }, -1, -1, -1, 3);
            treeView1.AppendNode(new object[] { "DockingDemo" }, -1, -1, -1, 4);
            treeView1.AppendNode(new object[] { "References" }, 1, -1, -1, 7);
            treeView1.AppendNode(new object[] { "DevExpress.Utils" }, 2, -1, -1, 5);
            treeView1.AppendNode(new object[] { "DevExpress.XtraBars" }, 2, -1, -1, 5);
            treeView1.AppendNode(new object[] { "DevExpress.XtraEditors" }, 2, -1, -1, 5);
            treeView1.AppendNode(new object[] { "System" }, 2, -1, -1, 5);
            treeView1.AppendNode(new object[] { "System.Drawing" }, 2, -1, -1, 5);
            treeView1.AppendNode(new object[] { "System.Windows.Forms" }, 2, -1, -1, 5);
            if(showAll) {
                treeView1.AppendNode(new object[] { "bin" }, 1, -1, -1, 9);
                treeView1.AppendNode(new object[] { "Debug" }, 9, -1, -1, 9);
                treeView1.AppendNode(new object[] { "Release" }, 9, -1, -1, 9);
                treeView1.AppendNode(new object[] { "obj" }, 1, -1, -1, 9);
                treeView1.AppendNode(new object[] { "Debug" }, 12, -1, -1, 9);
                treeView1.AppendNode(new object[] { "Release" }, 12, -1, -1, 9);
            }
            treeView1.AppendNode(new object[] { "AssemblyInfo.cs" }, 1, -1, -1, 10);
            treeView1.AppendNode(new object[] { "frmMain.cs" }, 1, -1, -1, 11);
            treeView1.AppendNode(new object[] { "SolutionExplorer.cs" }, 1, -1, -1, 12);
            if(showAll) {
                treeView1.AppendNode(new object[] { "frmMain.resx" }, 16, -1, -1, 13);
                treeView1.AppendNode(new object[] { "SolutionExplorer.resx" }, 17, -1, -1, 13);
            }
            treeView1.ExpandAll();
        }

        private void iShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            AddAllNodes(((DevExpress.XtraBars.BarButtonItem)e.Item).Down);
        }

        public event EventHandler PropertiesItemClick;
        private void iProperties_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(PropertiesItemClick != null) PropertiesItemClick(sender, EventArgs.Empty);

        }
        public event EventHandler TreeViewItemClick;
        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e) {
                if(TreeViewItemClick != null) TreeViewItemClick(sender, EventArgs.Empty);
        }
    }
}
