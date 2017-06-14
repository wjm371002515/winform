using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.UI.WareHouse
{
    public partial class FrmAddPurchaseItem : BaseDock
    {
        public string WareHourse = "";
        public string HandNumber = "";
        public Dictionary<string, PurchaseDetailInfo> detailDict = new Dictionary<string, PurchaseDetailInfo>();
        private decimal amountMoney = 0.0M;
        private double allQuantity = 0;
        public bool IsPurchase = false;//入库或者出库

        private BackgroundWorker treeWork = null;

        public FrmAddPurchaseItem()
        {
            InitializeComponent();

            treeWork = new BackgroundWorker();
            treeWork.DoWork += new DoWorkEventHandler(treeWork_DoWork);
            treeWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(treeWork_RunWorkerCompleted);
        }

        void treeWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        void treeWork_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void FrmAddPurchaseItem_Load(object sender, EventArgs e)
        {
            this.txtName.Focus();
            this.lblWareHouse.Text = WareHourse;
            this.groupConsumeList.Text = string.Format("{0} {1}", WareHourse, this.groupConsumeList.Text);

            ShowGoodListView();
            ShowGoodsTreeView();
            BindData();
        }

        private void BindData()
        {
            amountMoney = OnShowStatus(HandNumber);
            this.lblAmount.Text = string.Format("清单总金额：{0:C}", amountMoney);
            this.lblQuantity.Text = string.Format("清单总数量：{0}个", allQuantity);
        }

        private decimal OnShowStatus(string handNo)
        {
            decimal allMoney = 0.0M;
            allQuantity = 0;

            #region 更新消费记录
            if (!string.IsNullOrEmpty(handNo))
            {
                this.lvwDetail.Items.Clear();
                int i = 1;
                foreach (PurchaseDetailInfo info in detailDict.Values)
                {
                    ListViewItem item = new ListViewItem(info.ItemNo);
                    item.SubItems.Add(info.ItemName);
                    item.SubItems.Add(info.ItemBigType);
                    item.SubItems.Add(info.ItemType);
                    item.SubItems.Add(info.Unit);
                    item.SubItems.Add(info.Price.ToString("C2"));
                    item.SubItems.Add(info.Quantity.ToString());
                    item.SubItems.Add(info.Amount.ToString("C2"));
                    item.Tag = info;

                    this.lvwDetail.Items.Add(item);
                    allMoney += info.Amount;
                    allQuantity += info.Quantity;
                    i++;
                }
            }
            #endregion

            this.amountMoney = allMoney;
            return allMoney;
        }

        private void ShowGoodListView()
        {
            this.lvwGoods.Items.Clear();

            #region 项目信息列表

            List<ItemDetailInfo> itemDetailList = new List<ItemDetailInfo>();
            if (this.txtName.Text.Length > 0 || this.txtItemNo.Text.Length > 0)
            {
                itemDetailList = BLLFactory<ItemDetail>.Instance.FindByNameAndNo(this.txtName.Text, this.txtItemNo.Text, this.WareHourse);
            }
            else
            {
                itemDetailList = BLLFactory<ItemDetail>.Instance.GetAllByWareHouse(this.WareHourse);
            }

            foreach (ItemDetailInfo info in itemDetailList)
            {
                ListViewItem item = new ListViewItem(info.ItemNo);
                item.SubItems.Add(info.ItemName);

                StockInfo stockInfo = BLLFactory<Stock>.Instance.FindByItemNo(info.ItemNo);
                int quantity = 0;
                if (stockInfo != null)
                {
                    quantity = stockInfo.StockQuantity;
                }
                item.SubItems.Add(quantity.ToString());

                item.SubItems.Add(info.ItemBigType);
                item.SubItems.Add(info.ItemType);
                item.SubItems.Add(info.Price.ToString("C2"));
                item.SubItems.Add(info.Manufacture);
                item.SubItems.Add(info.MapNo);
                item.SubItems.Add(info.Specification);
                item.SubItems.Add(info.Material);
                item.SubItems.Add(info.Unit);
                item.SubItems.Add(info.Source);
                item.SubItems.Add(info.StoragePos);
                item.SubItems.Add(info.UsagePos);
                item.SubItems.Add(info.Note);
                item.SubItems.Add(info.WareHouse);
                item.SubItems.Add(info.Dept);

                item.Tag = info;
                this.lvwGoods.Items.Add(item);
            }

            #endregion
        }

        private void ShowGoodsTreeView()
        {
            /*this.treeGoods.Nodes.Clear();
            this.treeGoods.BeginUpdate();

            #region 项目类型信息明细类别

            CListItem[] dict = DictItemUtil.GetDictByDictType("备件类别");
            foreach (CListItem item in dict)
            {
                TreeNode typeNode = new TreeNode(item.Value, 0, 0);
                typeNode.Tag = item.Value;
                this.treeGoods.Nodes.Add(typeNode);
                //typeNode.Tag = info;               
            }
            #endregion

            this.treeGoods.EndUpdate();
            this.treeGoods.ExpandAll();*/
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            ShowGoodListView();

            if (this.lvwGoods.Items.Count > 0)
            {
                ListViewItem item = this.lvwGoods.Items[0];
                if (item != null)
                {
                    TreeNode node = FindNode(item);
                    this.treeGoods.ExpandAll();
                    this.treeGoods.SelectedNode = node;
                }
            }
        }

        private TreeNode FindNode(ListViewItem item)
        {
            foreach (TreeNode typeNode in this.treeGoods.Nodes)
            {
                foreach (TreeNode node in typeNode.Nodes)
                {
                    if (node.Tag != null)
                    {
                        ItemDetailInfo info = item.Tag as ItemDetailInfo;
                        ItemDetailInfo ItemDetailInfo = node.Tag as ItemDetailInfo;
                        if (ItemDetailInfo != null &&
                            ItemDetailInfo.ID == info.ID)
                        {
                            return node;
                        }
                    }
                }
            }
            return null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(this.txtQuantity.Text);
            if (count <= 0)
            {
                MessageDxUtil.ShowTips("数量必须大于0");
                this.txtQuantity.Focus();
                return;
            }

            if (this.lvwGoods.SelectedItems.Count > 0)
            {
                ListViewItem item = this.lvwGoods.SelectedItems[0];//按钮默认加第一个
                if (item != null)
                {
                    ItemDetailInfo info = item.Tag as ItemDetailInfo;
                    if (info != null)
                    {
                        //出库检查数量是否超过库存
                        if (!IsPurchase)
                        {
                            int stockQuantity = BLLFactory<Stock>.Instance.GetStockQuantity(info.ItemNo, this.WareHourse);
                            if (stockQuantity < count)
                            {
                                MessageDxUtil.ShowTips(string.Format("库存数量小于出库数量，请调整出库数量。\r\n该备件最大库存量为 {0} 。", stockQuantity));
                                this.txtQuantity.Focus();
                                return;
                            }
                        }

                        InsertOnItem(info);
                    }
                }

                BindData();            
            }
        }

        private void lvwGoods_DoubleClick(object sender, EventArgs e)
        {
            foreach(ListViewItem item in this.lvwGoods.SelectedItems)
            {
                ItemDetailInfo info = item.Tag as ItemDetailInfo;
                if (info != null)
                {
                    //出库检查数量是否超过库存
                    if (!IsPurchase)
                    {
                        int count = Convert.ToInt32(this.txtQuantity.Text);
                        int stockQuantity = BLLFactory<Stock>.Instance.GetStockQuantity(info.ItemNo, this.WareHourse);
                        if (stockQuantity < count)
                        {
                            MessageDxUtil.ShowTips(string.Format("库存数量小于出库数量，请调整出库数量。\r\n该备件最大库存量为 {0} 。", stockQuantity));
                            this.txtQuantity.Focus();
                            return;
                        }
                    }

                    InsertOnItem(info);
                }
            }
            BindData();
        }

        #region 写入到字典列表中

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
            //detailInfo.PurchaseHead_ID = 
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

        private void treeGoods_DoubleClick(object sender, EventArgs e)
        {
            if (this.treeGoods.SelectedNode != null)
            {
                TreeNode item = this.treeGoods.SelectedNode;//按钮默认加第一个
                ItemDetailInfo info = item.Tag as ItemDetailInfo;
                int count = Convert.ToInt32(this.txtQuantity.Text);
                if (info != null)
                {
                    InsertOnItem(info);
                }

                BindData();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvwDetail_DoubleClick(object sender, EventArgs e)
        {
            if (this.lvwDetail.SelectedItems.Count == 0) return;

            ListViewItem item = this.lvwDetail.SelectedItems[0];
            if (item != null)
            {
                PurchaseDetailInfo info = item.Tag as PurchaseDetailInfo;
                
                FrmSetPurchaseQuantity dlg = new FrmSetPurchaseQuantity();
                dlg.txtItemNo.Text = info.ItemNo;
                dlg.txtItemName.Text = info.ItemName;
                dlg.txtQuantity.Text = info.Quantity.ToString();
                dlg.txtPrice.Text = info.Price.ToString("f2");
                // 20170512 wjm 暂时注释掉
                dlg.txtPrice.Enabled = !IsPurchase;
                //dlg.txtPrice.ReadOnly = !IsPurchase;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    item.SubItems[6].Text = dlg.txtQuantity.Text;
                    int quntity = Convert.ToInt32(dlg.txtQuantity.Text);
                    decimal price = Convert.ToDecimal(dlg.txtPrice.Text);
                    item.SubItems[7].Text = (price * quntity).ToString("C2");

                    //入库的时候，数量，单价可以修改，因此需要重新获取单价信息，作为标准单价
                    PurchaseDetailInfo tempInfo = detailDict[info.ItemNo];
                    tempInfo.Amount = price * quntity;
                    tempInfo.Quantity = quntity;
                    tempInfo.Price = price;

                    //重新设置Tag的信息
                    item.Tag = tempInfo;

                    BindData();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //消费退单
            if (this.lvwDetail.SelectedItems.Count == 0) return;

            ListViewItem item = this.lvwDetail.SelectedItems[0];
            PurchaseDetailInfo info = item.Tag as PurchaseDetailInfo;
            if (info != null)
            {
                if(detailDict.ContainsKey(info.ItemNo))
                {
                    detailDict.Remove(info.ItemNo);
                }
                BindData();
            }
        }


        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(null, null);
            }
        }

        private void menuAdd_Add_Click(object sender, EventArgs e)
        {
            btnAdd_Click(null, null);
        }

        private void menu_SetQuantityPrice_Click(object sender, EventArgs e)
        {
            lvwDetail_DoubleClick(null, null);
        }

        private void treeGoods_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Node.Nodes.Clear();
                string itemType = e.Node.Tag.ToString();
                List<ItemDetailInfo> goods = BLLFactory<ItemDetail>.Instance.FindByItemType(itemType, this.WareHourse);
                foreach (ItemDetailInfo itemInfo in goods)
                {
                    string display = string.Format("{0}({1}) {2}", itemInfo.ItemName, itemInfo.ItemNo, itemInfo.Price);
                    TreeNode subNode = new TreeNode(display, 1, 1);
                    subNode.Tag = itemInfo;
                    e.Node.Nodes.Add(subNode);
                    e.Node.ExpandAll();
                }
            }
        }

    }
}
