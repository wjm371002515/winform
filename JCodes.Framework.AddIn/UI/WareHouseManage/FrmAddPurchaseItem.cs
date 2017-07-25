using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.AddIn.Other;

namespace JCodes.Framework.AddIn.UI.WareHouseManage
{
    public partial class FrmAddPurchaseItem : BaseDock
    {
        public string WareHourseId = string.Empty;
        public string WareHourse = string.Empty;
        public string HandNumber = string.Empty;
        public Dictionary<string, PurchaseDetailInfo> detailDict = new Dictionary<string, PurchaseDetailInfo>();
        private decimal amountMoney = 0.0M;
        private double allQuantity = 0;
        public bool IsPurchase = false;//入库或者出库

        public FrmAddPurchaseItem()
        {
            InitializeComponent();

            lvwGoods.ShowLineNumber = true;
            lvwGoods.BestFitColumnWith = false;                                                     //使用固定列宽的做法，True为自动适应宽度
            lvwGoods.gridView1.DataSourceChanged += new EventHandler(gridView1_DataSourceChanged);
            lvwGoods.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);

            lvwDetail.ShowLineNumber = true;
            lvwDetail.BestFitColumnWith = false;

            lvwDetail.gridView1.DataSourceChanged += new EventHandler(gridView2_DataSourceChanged);
            lvwDetail.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView2_CustomColumnDisplayText);
            lvwDetail.OnEditSelected += new EventHandler(lvwDetail_OnEditSelected);
            lvwDetail.OnDeleteSelected += new EventHandler(lvwDetail_OnDeleteSelected);
        }

        private void FrmAddPurchaseItem_Load(object sender, EventArgs e)
        {
            this.txtName.Focus();
            this.groupConsumeList.Text = string.Format("{0} {1}", WareHourse, this.groupConsumeList.Text);
            this.txtItemType.BindDictItems(Const.DIC_DEVICECATEGORY);

            ShowGoodListView();

            ShowGoodDetailView();
        }

        #region 设备清单
        /// <summary>
        /// 对显示的字段内容进行转义
        /// </summary>
        void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            string columnName = e.Column.FieldName;
            if (e.Column.ColumnType == typeof(DateTime))
            {
                if (e.Value != null)
                {
                    if (e.Value == DBNull.Value || Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
                    {
                        e.DisplayText = "";
                    }
                    else
                    {
                        e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm");//yyyy-MM-dd
                    }
                }
            }
            else if (columnName == "Price")
            {
                if (e.Value != null)
                {
                    e.DisplayText = e.Value.ToString().ToDecimal().ToString("C");
                }
            }
            else if (columnName == "Manufacture")
            {
                e.DisplayText = BLLFactory<Supplier>.Instance.FindByID(Convert.ToInt32(e.Value)).Name;
            }
            else if (columnName == "WareHouse")
            {
                e.DisplayText = BLLFactory<WareHouse>.Instance.FindByID(Convert.ToInt32(e.Value)).Name;
            }
            else if (columnName == "ItemBigType")
            {
                e.DisplayText = BLLFactory<DictData>.Instance.GetDictName(Const.DIC_DEVICEATTR, Convert.ToInt32(e.Value));
            }
            else if (columnName == "ItemType")
            {
                e.DisplayText = BLLFactory<DictData>.Instance.GetDictName(Const.DIC_DEVICECATEGORY, Convert.ToInt32(e.Value));
            }
            else if (columnName == "Unit")
            {
                e.DisplayText = BLLFactory<DictData>.Instance.GetDictName(Const.DIC_DEVICEUNIT, Convert.ToInt32(e.Value));
            }
        }

        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (lvwGoods.gridView1.Columns.Count > 0 && lvwGoods.gridView1.RowCount > 0)
            {
                //统一设置100宽度
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in lvwGoods.gridView1.Columns)
                {
                    column.Width = 100;
                }

                //可特殊设置特别的宽度
                lvwGoods.gridView1.SetGridColumWidth("Note", 200);
                lvwGoods.gridView1.SetGridColumWidth("ItemNo", 120);
                lvwGoods.gridView1.SetGridColumWidth("ItemBigType", 120);
                lvwGoods.gridView1.SetGridColumWidth("WareHouse", 120);
                lvwGoods.gridView1.SetGridColumWidth("ID", 80);
                lvwGoods.gridView1.SetGridColumWidth("StockQuantity", 80);
                lvwGoods.gridView1.SetGridColumWidth("Unit", 80);
                lvwGoods.gridView1.SetGridColumWidth("Price", 80);
            }
        }

        /// <summary>
        /// 根据查询条件构造查询语句
        /// </summary> 
        private string GetConditionSql()
        {
            //如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
            SearchCondition condition = new SearchCondition();

            condition.AddCondition("ItemNo", this.txtItemNo.Text, SqlOperator.Like)
                .AddCondition("ItemName", this.txtName.Text, SqlOperator.Like)
                .AddCondition("ItemType", this.txtItemType.GetComboBoxIntValue(), SqlOperator.Equal)
                .AddCondition("WareHouse", this.WareHourseId, SqlOperator.Equal);
            string where = condition.BuildConditionSql().Replace("Where", "");
            return where;
        }

        /// <summary>
        /// 所在库房的详细信息
        /// </summary>
        private void ShowGoodListView()
        {
            this.lvwGoods.DisplayColumns = "ItemNo,ItemName,Manufacture,MapNo,Specification,Material,ItemBigType,ItemType,Unit,Price,Source,StoragePos,UsagePos,StockQuantity,AlarmQuantity,Note,Dept,WareHouse";
            this.lvwGoods.ColumnNameAlias = BLLFactory<ItemDetail>.Instance.GetColumnNameAlias();

            #region 项目信息列表
            string where = GetConditionSql();
            List<ItemDetailInfo> itemDetailList = BLLFactory<ItemDetail>.Instance.Find(where);

            lvwGoods.DataSource = itemDetailList;
            lvwGoods.gridView1.BestFitColumns();

            #endregion
        }

        #endregion

        #region 入单详情
        /// <summary>
        /// 对显示的字段内容进行转义
        /// </summary>
        void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            string columnName = e.Column.FieldName;
            if (columnName == "ItemBigType")
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.DisplayText = BLLFactory<DictData>.Instance.GetDictName(Const.DIC_DEVICEATTR, Convert.ToInt32(e.Value));
                }
            }
            else if (columnName == "ItemType")
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.DisplayText = BLLFactory<DictData>.Instance.GetDictName(Const.DIC_DEVICECATEGORY, Convert.ToInt32(e.Value));
                }
            }
        }

        private void gridView2_DataSourceChanged(object sender, EventArgs e)
        {
            if (lvwGoods.gridView1.Columns.Count > 0 && lvwGoods.gridView1.RowCount > 0)
            {
                //统一设置100宽度
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in lvwGoods.gridView1.Columns)
                {
                    column.Width = 100;
                }

                //可特殊设置特别的宽度 ItemNo,ItemName,ItemBigType,ItemType,Unit,Price,Quantity,Amount

                lvwGoods.gridView1.SetGridColumWidth("ItemNo", 120);
                lvwGoods.gridView1.SetGridColumWidth("ItemName", 200);
                lvwGoods.gridView1.SetGridColumWidth("ItemBigType", 120);
                lvwGoods.gridView1.SetGridColumWidth("ItemType", 120);
                lvwGoods.gridView1.SetGridColumWidth("Unit", 80);
                lvwGoods.gridView1.SetGridColumWidth("Price", 80);
                lvwGoods.gridView1.SetGridColumWidth("Quantity", 80);
                lvwGoods.gridView1.SetGridColumWidth("Amount", 80);
            }
        }

        private void ShowGoodDetailView()
        {
            // 项目编号,项目名称,备件属类,备件类型,单位,单价,数量,金额
            lvwDetail.DisplayColumns = "ItemNo,ItemName,ItemBigType,ItemType,Unit,Price,Quantity,Amount";
            lvwDetail.ColumnNameAlias = BLLFactory<ItemDetail>.Instance.GetColumnNameAlias();

            List<PurchaseDetailInfo> lst = new List<PurchaseDetailInfo>();
            amountMoney = 0;
            allQuantity = 0;
            lst.Clear();
            // 计算总数量和总金额
            foreach(var purchaseDetail in detailDict.Values)
            {
                allQuantity += purchaseDetail.Quantity;
                amountMoney = amountMoney + purchaseDetail.Price * Convert.ToInt32(purchaseDetail.Quantity);
                lst.Add(purchaseDetail);
            }

            lvwDetail.DataSource = lst;
            lvwDetail.Refresh();
            this.lblAmount.Text = string.Format("清单总金额：{0:C}", amountMoney);
            this.lblQuantity.Text = string.Format("清单总数量：{0}个", allQuantity);
        }

        private void InsertOnItem(ItemDetailInfo itemDetailInfo)
        {
            int count = Convert.ToInt32(this.txtQuantity.Text);
            if (count <= 0)
            {
                MessageDxUtil.ShowTips("数量必须大于0");
                this.txtQuantity.Focus();
                return;
            }

            #region 构造入库信息
            PurchaseDetailInfo detailInfo = new PurchaseDetailInfo();
            detailInfo.Amount = itemDetailInfo.Price * count;
            detailInfo.ItemName = itemDetailInfo.ItemName;
            detailInfo.ItemNo = itemDetailInfo.ItemNo;
            detailInfo.OperationType = "入库";
            detailInfo.ItemBigType = itemDetailInfo.ItemBigType;
            detailInfo.ItemType = itemDetailInfo.ItemType;
            detailInfo.MapNo = itemDetailInfo.MapNo;
            detailInfo.Material = itemDetailInfo.Material;
            detailInfo.Source = itemDetailInfo.Source;
            detailInfo.Specification = itemDetailInfo.Specification;
            detailInfo.StoragePos = itemDetailInfo.StoragePos;
            detailInfo.UsagePos = itemDetailInfo.UsagePos;
            detailInfo.Price = itemDetailInfo.Price;
            detailInfo.Quantity = count;
            detailInfo.Unit = itemDetailInfo.Unit;
            detailInfo.WareHouse = itemDetailInfo.WareHouse;
            detailInfo.Dept = itemDetailInfo.Dept;

            #endregion

            if (detailDict.ContainsKey(itemDetailInfo.ItemNo))
            {
                PurchaseDetailInfo tempInfo = detailDict[itemDetailInfo.ItemNo];
                tempInfo.Amount += itemDetailInfo.Price * count;
                tempInfo.Quantity += count;
            }
            else
            {
                detailDict.Add(itemDetailInfo.ItemNo, detailInfo);
            }
        }

        #endregion 

        #region 事件处理
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ShowGoodListView();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(this.txtQuantity.Text);
            if (count <= 0)
            {
                MessageDxUtil.ShowTips("数量必须大于0");
                this.txtQuantity.Focus();
                return;
            }

            ItemDetailInfo info = lvwGoods.gridView1.GetFocusedRow() as ItemDetailInfo;

            if (info != null)
            {
                //出库检查数量是否超过库存
                if (!IsPurchase)
                {
                    int stockQuantity = BLLFactory<Stock>.Instance.GetStockQuantity(info.ItemNo, this.WareHourseId);
                    if (stockQuantity < count)
                    {
                        MessageDxUtil.ShowTips(string.Format("库存数量小于出库数量，请调整出库数量。\r\n该备件最大库存量为 {0} 。", stockQuantity));
                        this.txtQuantity.Focus();
                        return;
                    }
                }

                InsertOnItem(info);
            }

            ShowGoodDetailView();            
        }

        private void lvwDetail_OnEditSelected(object sender, EventArgs e)
        {
            PurchaseDetailInfo info = lvwDetail.gridView1.GetFocusedRow() as PurchaseDetailInfo;

            if (info != null)
            {
                FrmSetPurchaseQuantity dlg = new FrmSetPurchaseQuantity();
                dlg.txtItemNo.Text = info.ItemNo;
                dlg.txtItemName.Text = info.ItemName;
                dlg.txtQuantity.Text = info.Quantity.ToString();
                dlg.txtPrice.Text = info.Price.ToString("f2");

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    int quntity = Convert.ToInt32(dlg.txtQuantity.Text);
                    decimal price = Convert.ToDecimal(dlg.txtPrice.Text);

                    info.Price = price;
                    info.Quantity = quntity;
                    lvwGoods.Refresh();

                    //入库的时候，数量，单价可以修改，因此需要重新获取单价信息，作为标准单价
                    PurchaseDetailInfo tempInfo = detailDict[info.ItemNo];
                    tempInfo.Amount = price * quntity;
                    tempInfo.Quantity = quntity;
                    tempInfo.Price = price;

                    ShowGoodDetailView();
                }
            }
        }

        private void lvwDetail_OnDeleteSelected(object sender, EventArgs e)
        {
            ItemDetailInfo info = lvwGoods.gridView1.GetFocusedRow() as ItemDetailInfo;

            if (info != null)
            {
                if (detailDict.ContainsKey(info.ItemNo))
                {
                    detailDict.Remove(info.ItemNo);
                }

                ShowGoodDetailView();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            lvwDetail_OnDeleteSelected(null, null);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
