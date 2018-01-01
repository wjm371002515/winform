using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Pager;
using JCodes.Framework.Common.Winform;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.CommonControl.Pager.Others;
using JCodes.Framework.CommonControl.Controls;

namespace JCodes.Framework.AddIn.Dictionary
{
    public partial class FrmDictionary : BaseDock
    {
        public FrmDictionary()
        {
            InitializeComponent();

            TreeViewDrager drager = new TreeViewDrager(this.treeView1);
            drager.TreeImageList = this.imageList1;
            drager.ProcessDragNode += new ProcessDragNodeEventHandler(drager_ProcessDragNode);
        }

        private void FrmDictionary_Load(object sender, EventArgs e)
        {
            InitTreeView();
            this.lblDictType.Text = "";
            BindData();

            this.winGridViewPager1.OnPageChanged += new EventHandler(winGridViewPager1_OnPageChanged);
            this.winGridViewPager1.OnStartExport += new EventHandler(winGridViewPager1_OnStartExport);
            this.winGridViewPager1.OnEditSelected += new EventHandler(winGridViewPager1_OnEditSelected);
            this.winGridViewPager1.OnAddNew += new EventHandler(winGridViewPager1_OnAddNew);
            this.winGridViewPager1.OnDeleteSelected += new EventHandler(winGridViewPager1_OnDeleteSelected);
            this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);
            this.winGridViewPager1.AppendedMenu = this.contextMenuStrip2;
            this.winGridViewPager1.BestFitColumnWith = false;
            this.winGridViewPager1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);

            Init_Function();
        }

        void Init_Function()
        {
            btnAdd.Enabled = HasFunction("DicSet/DictDataAdd");
            btnEdit.Enabled = HasFunction("DicSet/DictDataEdit");
            btnDelete.Enabled = HasFunction("DicSet/DictDataDel");
        }

        #region 分类数据

        private void InitTreeView()
        {
            this.treeView1.Nodes.Clear();
            this.treeView1.BeginUpdate();
            List<DictTypeNodeInfo> typeNodeList = BLLFactory<DictType>.Instance.GetTree();
            foreach (DictTypeNodeInfo info in typeNodeList)
            {
                AddTree(null, info);
            }
            this.treeView1.EndUpdate();
            this.treeView1.ExpandAll();
        }

        private void AddTree(TreeNode pNode, DictTypeNodeInfo info)
        {
            TreeNode node = null;
            if (info.PID == -1)
            {
                node = new TreeNode(info.Name, 1, 1);
                node.Tag = info.ID;
                this.treeView1.Nodes.Add(node);
            }
            else
            {
                node = new TreeNode(info.Name, 1, 1);
                node.Tag = info.ID;
                pNode.Nodes.Add(node);
            }

            foreach (DictTypeNodeInfo subInfo in info.Children)
            {
                AddTree(node, subInfo);
            }
        }

        private Int32 GetParentNodeIndex()
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node != null)
            {
                return Convert.ToInt32(node.Tag);
            }
            return -1;
        }

        private void menu_AddType_Click(object sender, EventArgs e)
        {
            if (!HasFunction("DicSet/DictTypeAdd"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            FrmEditDictType dlg = new FrmEditDictType();
            dlg.PID = GetParentNodeIndex();
            dlg.OnDataSaved += new EventHandler(dlg_OnDataTreeSaved);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                InitTreeView();
            }
        }

        void dlg_OnDataTreeSaved(object sender, EventArgs e)
        {
            InitTreeView();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_Refresh_Click(object sender, EventArgs e)
        {
            InitTreeView();
        }

        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_EditType_Click(object sender, EventArgs e)
        {
            if (!HasFunction("DicSet/DictTypeEdit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            TreeNode selectedNode = this.treeView1.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                Int32 typeId = Convert.ToInt32( selectedNode.Tag);
                DictTypeInfo info = BLLFactory<DictType>.Instance.FindByID(typeId);
                if (info != null)
                {
                    FrmEditDictType dlg = new FrmEditDictType();
                    dlg.ID = typeId.ToString();
                    dlg.PID = info.PID;
                    dlg.OnDataSaved += new EventHandler(dlg_OnDataTreeSaved);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        InitTreeView();
                    }
                }
            }
        }

        private void menu_DeleteType_Click(object sender, EventArgs e)
        {
            if (!HasFunction("DicSet/DictTypeDel"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            TreeNode selectedNode = this.treeView1.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                Int32 typeId = Convert.ToInt32(selectedNode.Tag);

                string message = string.Format("您确定要删除节点：{0}，删除将子节点及其数据均一并删除，请谨慎操作。", selectedNode.Text);
                if (MessageDxUtil.ShowYesNoAndWarning(message) == DialogResult.Yes)
                {
                    try
                    {
                        Dictionary<Int32, string> dict = BLLFactory<DictType>.Instance.GetAllType(typeId);
                        dict.Add(typeId, typeId.ToString());//增加一个自己，也需要删除

                        foreach (Int32 key in dict.Keys)
                        {
                            BLLFactory<DictType>.Instance.DeleteByUser(key, LoginUserInfo.ID);

                            string condition = string.Format("DictType_ID={0}", key);
                            BLLFactory<DictData>.Instance.DeleteByCondition(condition);
                        }

                        InitTreeView();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmDictionary));
                        MessageDxUtil.ShowError(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 拖放事件
        /// </summary>
        /// <param name="dragNode"></param>
        /// <param name="dropNode"></param>
        /// <returns></returns>
        bool drager_ProcessDragNode(TreeNode dragNode, TreeNode dropNode)
        {
            if (!HasFunction("DicSet/DictTypeDrag"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return false;
            }

            if (dragNode != null && dragNode.Text == "数据字典管理")
                return false;

            if (dropNode != null && dropNode.Tag != null)
            {
                string dropTypeId = dropNode.Tag.ToString();
                string dragTypeId = dragNode.Tag.ToString();
                //MessageDxUtil.ShowTips(string.Format("dropTypeId:{0} dragTypeId:{1}", dropTypeId, drageTypeId));

                try
                {
                    DictTypeInfo dragTypeInfo = BLLFactory<DictType>.Instance.FindByID(dragTypeId);
                    if (dragTypeInfo != null)
                    {
                        dragTypeInfo.PID = Convert.ToInt32(dropTypeId);
                        BLLFactory<DictType>.Instance.Update(dragTypeInfo, dragTypeInfo.ID);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmDictionary));
                    MessageDxUtil.ShowError(ex.Message);
                    return false;
                }
            }
            return true;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                this.lblDictType.Text = e.Node.Text;
                this.lblDictType.Tag = e.Node.Tag;

                BindData();
            }
        }

        private void menu_ClearData_Click(object sender, EventArgs e)
        {

            if (!HasFunction("DicSet/DictDataClearAll"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            TreeNode selectedNode = this.treeView1.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                Int32 typeId = Convert.ToInt32(selectedNode.Tag);
                int count = BLLFactory<DictData>.Instance.GetDictByTypeID(typeId).Count;
                string message = string.Format("您确定要删除节点：{0}，该节点下面有【{1}】项数据", selectedNode.Text, count);
                if (MessageDxUtil.ShowYesNoAndWarning(message) == DialogResult.Yes)
                {
                    try
                    {
                        BLLFactory<DictData>.Instance.DeleteByCondition(string.Format("DictType_ID='{0}'", typeId));
                        InitTreeView();
                        BindData();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmDictionary));
                        MessageDxUtil.ShowError(ex.Message);
                    }
                }
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            menu_EditType_Click(null, null);
        }
        #endregion

        #region 分类数据字典数据
        private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
        {
            if (!HasFunction("DicSet/DictDataAdd"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            if (this.lblDictType.Text.Length == 0)
            {
                MessageDxUtil.ShowTips("请选择指定的字典大类，然后添加！");
                return;
            }

            FrmEditDictData dlg = new FrmEditDictData();
            dlg.txtDictType.Text = this.lblDictType.Text;
            dlg.txtDictType.Tag = this.lblDictType.Tag;
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);

            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindData();
            }
        }

        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            if (!HasFunction("DicSet/DictDataEdit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            string ID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID");
            if (!string.IsNullOrEmpty(ID))
            {
                FrmEditDictData dlg = new FrmEditDictData();
                dlg.ID = ID;
                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    BindData();
                }
            }
        }

        private void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
        {
            if (!HasFunction("DicSet/DictDataDel"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            if (MessageDxUtil.ShowYesNoAndTips("您确定删除选定的记录么？") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");
                BLLFactory<DictData>.Instance.DeleteByUser(ID, LoginUserInfo.ID);
            }
            BindData();
        }

        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 绑定数据后，分配各列的宽度
        /// </summary>
        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.winGridViewPager1.gridView1.Columns.Count > 0 && this.winGridViewPager1.gridView1.RowCount > 0)
            {
                this.winGridViewPager1.gridView1.Columns["Name"].Width = 200;
                this.winGridViewPager1.gridView1.Columns["Value"].Width = 200;
            }
        }

        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
            {
                this.menu_EditType.Enabled = false;
            }
            else
            {
                this.menu_EditType.Enabled = true;
            }
        }

        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            string condition = GetCondtionSql();
            this.winGridViewPager1.AllToExport = BLLFactory<DictData>.Instance.FindToDataTable(condition);
        }

        private void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private string GetCondtionSql()
        {
            SearchCondition conditon = new SearchCondition();
            if (lblDictType.Tag != null)
            {
                conditon.AddCondition("DictType_ID", Convert.ToInt32(this.lblDictType.Tag), SqlOperator.Equal);
            }
            string sql = conditon.BuildConditionSql().Replace("Where", "");
            return sql;
        }

        private void BindData()
        {
            #region 添加别名解析
            this.winGridViewPager1.DisplayColumns = "Value,Name,Seq,Remark,EditTime";
            this.winGridViewPager1.AddColumnAlias("ID", "编号");
            this.winGridViewPager1.AddColumnAlias("DictType_ID", "字典大类");
            this.winGridViewPager1.AddColumnAlias("Name", "项目名称");
            this.winGridViewPager1.AddColumnAlias("Value", "项目值");
            this.winGridViewPager1.AddColumnAlias("Seq", "字典排序");
            this.winGridViewPager1.AddColumnAlias("Remark", "备注");
            this.winGridViewPager1.AddColumnAlias("Editor", "修改用户");
            this.winGridViewPager1.AddColumnAlias("EditTime", "更新日期");
            #endregion

            if (this.lblDictType.Tag != null)
            {
                string condition = GetCondtionSql();
                List<DictDataInfo> list = BLLFactory<DictData>.Instance.FindWithPager(condition, this.winGridViewPager1.PagerInfo);
                this.winGridViewPager1.DataSource = new SortableBindingList<DictDataInfo>(list);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnAddNew(this.winGridViewPager1.gridView1, null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnEditSelected(this.winGridViewPager1.gridView1, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnDeleteSelected(this.winGridViewPager1.gridView1, null);
        }

        #endregion
    }
}
