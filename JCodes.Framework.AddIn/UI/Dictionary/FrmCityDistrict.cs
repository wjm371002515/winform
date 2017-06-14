using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.CommonControl.Pager.Others;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.UI.Dictionary
{
    public partial class FrmCityDistrict : BaseDock
    {
        private string SelectedProvinceId = "";
        private string SelectedCityId = "";

        public FrmCityDistrict()
        {
            InitializeComponent();

            InitProvinceTree();

            this.winGridViewPager1.OnStartExport += new EventHandler(winGridViewPager1_OnStartExport);
            this.winGridViewPager1.OnEditSelected += new EventHandler(winGridViewPager1_OnEditSelected);
            this.winGridViewPager1.OnAddNew += new EventHandler(winGridViewPager1_OnAddNew);
            this.winGridViewPager1.OnDeleteSelected += new EventHandler(winGridViewPager1_OnDeleteSelected);
            this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);
            this.winGridViewPager1.AppendedMenu = this.contextMenuStrip1;
            this.winGridViewPager1.ShowLineNumber = true;
            this.winGridViewPager1.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置
            this.winGridViewPager1.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
            this.winGridViewPager1.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);
            this.winGridViewPager1.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView1_RowCellStyle);

        }

        void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.Column.FieldName == "OrderStatus")
            //{
            //    string status = this.winGridViewPager1.gridView1.GetRowCellValue(e.RowHandle, "OrderStatus").ToString();
            //    Color color = Color.White;
            //    if (status == "已审核")
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //        e.Appearance.BackColor2 = Color.LightCyan;
            //    }
            //}
        }
        void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.ColumnType == typeof(DateTime))
            {
                string columnName = e.Column.FieldName;
                if (e.Value != null)
                {
                    if (Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
                    {
                        e.DisplayText = "";
                    }
                    else
                    {
                        e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm");//yyyy-MM-dd
                    }
                }
            }
            //else if (e.Column.FieldName == "Age")
            //{
            //    e.DisplayText = string.Format("{0}岁", e.Value);
            //}
            //else if (Column.FieldName == "ReceivedMoney")
            //{
            //    if (e.Value != null)
            //    {
            //        e.DisplayText = e.Value.ToString().ToDecimal().ToString("C");
            //    }
            //}
        }

        /// <summary>
        /// 绑定数据后，分配各列的宽度
        /// </summary>
        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.winGridViewPager1.gridView1.Columns.Count > 0 && this.winGridViewPager1.gridView1.RowCount > 0)
            {
                //统一设置100宽度
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in this.winGridViewPager1.gridView1.Columns)
                {
                    column.Width = 100;
                }

                //可特殊设置特别的宽度
                SetGridColumWidth("DistrictName", 400);
            }
        }

        private void SetGridColumWidth(string columnName, int width)
        {
            DevExpress.XtraGrid.Columns.GridColumn column = this.winGridViewPager1.gridView1.Columns.ColumnByFieldName(columnName);
            if (column != null)
            {
                column.Width = width;
            }
        }

        private void FrmCityDistrict_Load(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 初始化省份列表内容
        /// </summary>
        private void InitProvinceTree()
        {
            //初始化代码
            this.treeCity.Nodes.Clear();
            this.treeProvince.Nodes.Clear();

            this.treeProvince.BeginUpdate();
            List<ProvinceInfo> provinceList = BLLFactory<Province>.Instance.GetAll();
            foreach (ProvinceInfo info in provinceList)
            {
                TreeNode node = new TreeNode(info.ProvinceName);
                node.Tag = info.ID;

                this.treeProvince.Nodes.Add(node);
            }
            this.treeProvince.EndUpdate();

        }

        /// <summary>
        /// 初始化城市列表
        /// </summary>
        private void InitCityTree()
        {
            TreeNode selectedNode = this.treeProvince.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                this.SelectedProvinceId = selectedNode.Tag.ToString();

                this.treeCity.Nodes.Clear();
                this.treeCity.BeginUpdate();

                List<CityInfo> cityList = BLLFactory<City>.Instance.GetCitysByProvinceID(selectedNode.Tag.ToString());
                foreach (CityInfo info in cityList)
                {
                    TreeNode node = new TreeNode(info.CityName, 1, 1);
                    node.Tag = info.ID;
                    this.treeCity.Nodes.Add(node);
                }

                this.treeCity.EndUpdate();
            }
        }

        /// <summary>
        /// 分页控件刷新操作
        /// </summary>
        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 分页控件删除操作
        /// </summary>
        private void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
        {
            if (MessageDxUtil.ShowYesNoAndTips("您确定删除选定的记录么？") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");
                BLLFactory<District>.Instance.DeleteByUser(ID, LoginUserInfo.ID.ToString());
            }

            BindData();
        }

        /// <summary>
        /// 分页控件编辑项操作
        /// </summary>
        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SelectedCityId))
            {
                MessageDxUtil.ShowTips("请先选择城市");
                return;
            }

            string ID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID");
            if (!string.IsNullOrEmpty(ID))
            {

                FrmEditDistrict dlg = new FrmEditDistrict();
                dlg.txtCity.Text = lblCityName.Text;
                dlg.txtCity.Tag = lblCityName.Tag;
                dlg.ID = ID;
                dlg.OnDataSaved += new EventHandler(District_OnDataSaved);

                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);

                if (DialogResult.OK == dlg.ShowDialog())
                {
                    BindData();
                }
            }
        }

        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 分页控件新增操作
        /// </summary>        
        private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
        {
            btnBatchAdd_Click(null, null);
        }

        /// <summary>
        /// 分页控件全部导出操作前的操作
        /// </summary> 
        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            List<DistrictInfo> list = BLLFactory<District>.Instance.GetDistrictByCity(SelectedCityId);
            this.winGridViewPager1.AllToExport = list;
        }

        /// <summary>
        /// 分页控件翻页的操作
        /// </summary> 
        private void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindData()
        {
            //entity
            this.winGridViewPager1.DisplayColumns = "DistrictName";

            #region 添加别名解析

            this.winGridViewPager1.AddColumnAlias("DistrictName", "区县名称");

            #endregion

            List<DistrictInfo> list = BLLFactory<District>.Instance.GetDistrictByCity(SelectedCityId);
            this.winGridViewPager1.DataSource = new SortableBindingList<DistrictInfo>(list);
            this.winGridViewPager1.PrintTitle = "District报表";
        }

        /// <summary>
        /// 查询数据操作
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 提供给控件回车执行查询的操作
        /// </summary>
        private void SearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }

        private void treeProvince_AfterSelect(object sender, TreeViewEventArgs e)
        {            
            InitCityTree();
        }

        private void treeCity_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                this.SelectedCityId = e.Node.Tag.ToString();
                this.lblCityName.Text = e.Node.Text;
                this.lblCityName.Tag = e.Node.Tag.ToString();

                BindData();
            }
        }

        private void treeCity_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            menuCity_Edit_Click(null, null);
        }

        private void menuTree_ExpandAll_Click(object sender, EventArgs e)
        {
            this.treeProvince.ExpandAll();
        }

        private void menuTree_Clapase_Click(object sender, EventArgs e)
        {
            this.treeProvince.CollapseAll();
        }

        private void menuTree_Refresh_Click(object sender, EventArgs e)
        {
            InitProvinceTree();
        }

        private void menuCity_ExpandAll_Click(object sender, EventArgs e)
        {
            this.treeCity.ExpandAll();
        }

        private void menuCity_Clapse_Click(object sender, EventArgs e)
        {
            this.treeCity.CollapseAll();
        }

        private void menuCity_Refresh_Click(object sender, EventArgs e)
        {
            InitCityTree();
        }

        private void menuCity_AddNew_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SelectedProvinceId))
            {
                MessageDxUtil.ShowTips("请先选择省份");
                return;
            }

            ProvinceInfo info = BLLFactory<Province>.Instance.FindByID(SelectedProvinceId);
            if (info != null)
            {
                FrmEditCity dlg = new FrmEditCity();
                dlg.txtProvince.Text = info.ProvinceName;
                dlg.txtProvince.Tag = info.ID;
                dlg.OnDataSaved += new EventHandler(dlgCity_OnDataSaved);
                dlg.ShowDialog();
            }
        }

        void dlgCity_OnDataSaved(object sender, EventArgs e)
        {
            InitCityTree();
        }

        private void menuCity_Edit_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeCity.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                ProvinceInfo info = BLLFactory<Province>.Instance.FindByID(SelectedProvinceId);
                if (info != null)
                {
                    FrmEditCity dlg = new FrmEditCity();
                    dlg.txtProvince.Text = info.ProvinceName;
                    dlg.txtProvince.Tag = info.ID;
                    dlg.ID = selectedNode.Tag.ToString();
                    dlg.OnDataSaved += new EventHandler(dlgCity_OnDataSaved);
                    dlg.ShowDialog();
                }
            }
        }

        private void menuCity_Delete_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeCity.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                string message = "您确认要删除选定的记录吗";
                if (MessageDxUtil.ShowYesNoAndWarning(message) == System.Windows.Forms.DialogResult.Yes)
                {
                    BLLFactory<City>.Instance.DeleteByUser(selectedNode.Tag.ToString(), LoginUserInfo.ID.ToString());
                }
            }
        }

        private void btnBatchAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SelectedCityId))
            {
                MessageDxUtil.ShowTips("请先选择城市");
                return;
            }

            FrmBatchAddDistrict dlg = new FrmBatchAddDistrict();
            dlg.txtCity.Text = lblCityName.Text;
            dlg.txtCity.Tag = lblCityName.Tag;
            dlg.OnDataSaved += new EventHandler(District_OnDataSaved);
            dlg.ShowDialog();
        }
        void District_OnDataSaved(object sender, EventArgs e)
        {
            BindData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnEditSelected(this.winGridViewPager1.gridView1, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SelectedCityId))
            {
                MessageDxUtil.ShowTips("请先选择城市");
                return;
            }

            winGridViewPager1_OnDeleteSelected(this.winGridViewPager1.gridView1, null);
        }


    }
}
