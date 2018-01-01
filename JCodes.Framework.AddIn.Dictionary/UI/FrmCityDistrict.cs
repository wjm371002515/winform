using JCodes.Framework.CommonControl.Controls;
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

namespace JCodes.Framework.AddIn.Dictionary
{
    public partial class FrmCityDistrict : BaseDock
    {
        private string SelectedProvinceId = "0";
        private string SelectedCityId = "0";

        #region 框架初始化
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
        }
        #endregion

        #region 省份
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

        private void treeProvince_AfterSelect(object sender, TreeViewEventArgs e)
        {
            InitCityTree();
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

        #endregion

        #region 城市
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
                    TreeNode node = new TreeNode(info.ZipCode+"_"+info.CityName, 1, 1);
                    node.Tag = info.ID;
                    this.treeCity.Nodes.Add(node);
                }

                this.treeCity.EndUpdate();
            }
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
            if (!HasFunction("CityDistrict/CityAdd"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

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

        private void treeCity_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            menuCity_Edit_Click(null, null);
        }

        private void menuCity_Edit_Click(object sender, EventArgs e)
        {
            if (!HasFunction("CityDistrict/CityEdit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

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
            if (!HasFunction("CityDistrict/CityDel"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

            TreeNode selectedNode = this.treeCity.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                string message = "您确认要删除选定的记录吗";
                if (MessageDxUtil.ShowYesNoAndWarning(message) == System.Windows.Forms.DialogResult.Yes)
                {
                    BLLFactory<City>.Instance.DeleteByUser(selectedNode.Tag.ToString(), LoginUserInfo.ID);
                    BLLFactory<District>.Instance.DeleteByCondition(string.Format("CityID={0}", selectedNode.Tag.ToString()));

                    InitCityTree();
                }
            }
        }

        void dlgCity_OnDataSaved(object sender, EventArgs e)
        {
            InitCityTree();
        }

        #endregion

        #region 区乡县
       

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
                winGridViewPager1.gridView1.SetGridColumWidth("DistrictName", 400);
            }
        }

        /// <summary>
        /// 分页控件刷新操作
        /// </summary>
        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
        }

        private void btnBatchAdd_Click(object sender, EventArgs e)
        {
            if (!HasFunction("CityDistrict/DistrictAdd"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

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

        /// <summary>
        /// 分页控件新增操作
        /// </summary>        
        private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
        {
            btnBatchAdd_Click(null, null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnEditSelected(this.winGridViewPager1.gridView1, null);
        }

        /// <summary>
        /// 分页控件编辑项操作
        /// </summary>
        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
            if (!HasFunction("CityDistrict/DistrictEdit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return;
            }

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            winGridViewPager1_OnDeleteSelected(this.winGridViewPager1.gridView1, null);
        }

        /// <summary>
        /// 分页控件删除操作
        /// </summary>
        private void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
        {
            if (!HasFunction("CityDistrict/DistrictDel"))
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
                BLLFactory<District>.Instance.DeleteByUser(ID, LoginUserInfo.ID);
            }

            BindData();
        }

        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            BindData();
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

        
        void District_OnDataSaved(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion
    }
}
