using System;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;
using JCodes.Framework.CommonControl.BaseUI;
using System.Windows.Forms;
using System.Xml;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Entity;
using DevExpress.XtraGrid.Views.Base;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Format;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Office;
using System.Linq;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;

// 参考文档 http://www.cnblogs.com/a1656344531/archive/2012/11/28/2792863.html

namespace JCodes.Framework.AddIn.Proj
{
    /// <summary>
    /// 登陆系统的黑白名单列表
    /// </summary>	
    public partial class FrmConstant : BaseDock
    {
        private ConstantInfo tmpconstantInfo = null;

        private XmlHelper xmlhelper = new XmlHelper(@"XML\constant.xml");

        private List<string> lstName = new List<string>();

        private string xmlModel = "<name>{0}</name><constantvalue>{1}</constantvalue><remark>{2}</remark>";

        private Int32 _errCount = 0;
        private List<CListItem> _errlst = new List<CListItem>();
        private Int32 _warnCount = 0;
        private List<CListItem> _warnlst = new List<CListItem>();
        private Int32 _infoCount = 0;
        private List<CListItem> _infolst = new List<CListItem>();

        public FrmConstant()
        {
            InitializeComponent();

            BindData();

            ValidateData();

            InitView();
        }

        /// <summary>
        /// 回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                gridView1.ActiveFilterString = string.Empty;
            }
            else
            {
                // 查询事件 模糊查询
                string filter = string.Format("Name Like '%{0}%' or ConstantValue Like '%{0}%' or Remark Like '%{0}%'", txtSearch.Text);
                gridView1.ActiveFilterString = filter;
            }
            
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype");
            List<ConstantInfo> constantInfoList = new List<ConstantInfo>();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                ConstantInfo constantInfo = new ConstantInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值
                constantInfo.Gid = xe.GetAttribute("gid").ToString();

                // 得到ConstantInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                constantInfo.Name = xnl0.Item(0).InnerText;
                constantInfo.ConstantValue = xnl0.Item(1).InnerText;
                constantInfo.Remark = xnl0.Item(2).InnerText;
                constantInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();

                constantInfoList.Add(constantInfo);
            }

            // 添加一行空行
            constantInfoList.Add(new ConstantInfo());
            gridControl1.DataSource = constantInfoList;

            gridView1.Columns["gid"].Visible = false;
            gridView1.Columns["lstInfo"].Visible = false;
        }



        /// <summary>
        /// 校验加载的数据是否存在异常的
        /// </summary>
        private void ValidateData()
        {
            // 查询是否存在2个键值的数据
            List<ConstantInfo> lstConstantInfo = gridControl1.DataSource as List<ConstantInfo>;

            // 查找重复的Name的值

            List<String> tmpName = new List<string>();
            foreach (ConstantInfo constantInfo in lstConstantInfo)
            {
                if (string.IsNullOrEmpty(constantInfo.Gid))
                    continue;

                if (lstName.Contains(constantInfo.Name))
                {
                    tmpName.Add(constantInfo.Name);
                }

                lstName.Add(constantInfo.Name);
            }

            foreach (ConstantInfo constantInfo in lstConstantInfo)
            {
                if (string.IsNullOrEmpty(constantInfo.Gid))
                    continue;

                // 判断重复的 类型名
                if (tmpName.Contains(constantInfo.Name))
                {
                    if (constantInfo.lstInfo.ContainsKey("Name"))
                    {
                        constantInfo.lstInfo["Name"].ErrorText = constantInfo.lstInfo["Name"].ErrorText + "\r\n存在键值相同的名称";
                        constantInfo.lstInfo["Name"].ErrorType = constantInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? constantInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        constantInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("存在键值相同的名称", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("存在键值相同的名称", "名称" + constantInfo.Name));
                    }
                }
                // 类型名判断大驼峰
                if (constantInfo.Name.Length >= 1)
                {
                    if (!string.Equals(constantInfo.Name[0].ToString(), constantInfo.Name[0].ToString().ToUpper(), StringComparison.CurrentCulture))
                    {
                        if (constantInfo.lstInfo.ContainsKey("Name"))
                        {
                            constantInfo.lstInfo["Name"].ErrorText = constantInfo.lstInfo["Name"].ErrorText + "\r\n名称以大驼峰命名规范";
                            constantInfo.lstInfo["Name"].ErrorType = constantInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning ? constantInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                        }
                        else
                        {
                            constantInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("类型名以大驼峰命名称", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning));
                            _warnCount++;
                            // 20170901 wjm 调整key 和value的顺序
                            _warnlst.Add(new CListItem("名称以大驼峰命名规范", "名称" + constantInfo.Name));
                        }
                    }
                }

                // 判断类型名是否为空
                if (string.IsNullOrEmpty(constantInfo.Name))
                {
                    if (constantInfo.lstInfo.ContainsKey("Name"))
                    {
                        constantInfo.lstInfo["Name"].ErrorText = constantInfo.lstInfo["Name"].ErrorText + "\r\n名称不能为空";
                        constantInfo.lstInfo["Name"].ErrorType = constantInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? constantInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        constantInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("名称不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("名称不能为空", "名称" + constantInfo.Name));
                    }
                }
                // 判断名称是否为空
                if (string.IsNullOrEmpty(constantInfo.ConstantValue))
                {
                    if (constantInfo.lstInfo.ContainsKey("ConstantValue"))
                    {
                        constantInfo.lstInfo["ConstantValue"].ErrorText = constantInfo.lstInfo["ConstantValue"].ErrorText + "\r\n常量值不能为空";
                        constantInfo.lstInfo["ConstantValue"].ErrorType = constantInfo.lstInfo["ConstantValue"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? constantInfo.lstInfo["ConstantValue"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        constantInfo.lstInfo.Add("ConstantValue", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("常量值不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("常量值不能为空", "常量值" + constantInfo.Name));
                    }
                }
            }
        }

        /// <summary>
        /// 清楚错误的数据
        /// </summary>
        private void ClearValidate()
        {
            // 初始化数据
            _errCount = 0;
            _warnCount = 0;
            _infoCount = 0;
            _errlst.Clear();
            _warnlst.Clear();
            _infolst.Clear();
            lstName.Clear();

              // 查询是否存在2个键值的数据
            List<ConstantInfo> lstConstantInfo = gridControl1.DataSource as List<ConstantInfo>;

            foreach (ConstantInfo constantInfo in lstConstantInfo)
            {
                if (string.IsNullOrEmpty(constantInfo.Gid))
                    continue;

                constantInfo.lstInfo.Clear();
            }
        }

        /// <summary>
        /// 初始化界面元素
        /// </summary>
        private void InitView()
        {
            barErrText.Caption = string.Format("{0} 条错误信息", _errCount);
            barWarningText.Caption = string.Format("{0} 条警告信息", _warnCount);
            barInfoText.Caption = string.Format("{0} 条提示信息", _infoCount);
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            // xmlhelper.Read("bookstore/book[@ISBN=\"7-111-19149-6\"]") Attributes 的属性
            // xmlhelper.Read("bookstore/book[title=\"计算机硬件技术基础\"]") 内部节点
            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype/item[@gid=\"" + tmpconstantInfo.Gid + "\"]");
            Int32 idx = -1;

            switch (e.Column.ToString())
            {
                case "名称":
                    idx = 0;
                    break;
                case "常量值":
                    idx = 1;
                    break;
                case "说明":
                    idx = 2;
                    break;
            }
            
           if (idx == -1)
                return;

            xmlNodeLst.Item(idx).InnerText = e.Value.ToString();
            xmlhelper.Save(false);

            tmpconstantInfo = null;
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty((gridView1.GetFocusedRow() as ConstantInfo).Gid) && (gridView1.FocusedRowHandle + 1 == gridView1.RowCount))
            {
                btnAdd_Click(null, null);
            }

            tmpconstantInfo = gridView1.GetRow(e.RowHandle) as ConstantInfo;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 假如在查询去掉查询在新增
            if (!string.IsNullOrEmpty(gridView1.ActiveFilterString))
                gridView1.ActiveFilterString = "";

            var constantInfo = new ConstantInfo();
            constantInfo.Gid = System.Guid.NewGuid().ToString();
            constantInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();

            xmlhelper.InsertElement("datatype", "item", "gid", constantInfo.Gid, string.Format(xmlModel, string.Empty, string.Empty, string.Empty));
            xmlhelper.Save(false);

            (gridView1.DataSource as List<ConstantInfo>).Insert((gridView1.DataSource as List<ConstantInfo>).Count - 1, constantInfo);
            gridView1.RefreshData();
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsert_Click(object sender, EventArgs e)
        {
            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype/item[@gid=\"" + (gridView1.GetFocusedRow() as ConstantInfo).Gid + "\"]");
            var constantInfo = new ConstantInfo();
            constantInfo.Gid = System.Guid.NewGuid().ToString();
            constantInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
            xmlhelper.InsertElement("item", "gid", constantInfo.Gid, string.Format(xmlModel, string.Empty, string.Empty, string.Empty), xmlNodeLst.Item(0).ParentNode);
            xmlhelper.Save(false);

            (gridView1.DataSource as List<ConstantInfo>).Insert(gridView1.FocusedRowHandle, constantInfo);
            gridView1.RefreshData();
        }

        /// <summary>
        /// 删除标准类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            // 20170824 如果是最后一行空行则不再继续操作
            if (string.IsNullOrEmpty((gridView1.GetFocusedRow() as ConstantInfo).Gid))
                return;

            xmlhelper.DeleteByPathNode("datatype/item[@gid=\"" + gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "gid") + "\"]");
            xmlhelper.Save(false);

            // 20170924 wjm 删除lstName 对应的值保存导入的时候缓存问题
            lstName.Remove(gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "Name"));

            (gridView1.DataSource as List<ConstantInfo>).RemoveAt(gridView1.FocusedRowHandle);
            gridView1.RefreshData();
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle == 0)
                return;

            ConstantInfo cuconstantInfo = gridView1.GetFocusedRow() as ConstantInfo;
            ConstantInfo preconstantInfo = gridView1.GetRow(gridView1.FocusedRowHandle - 1) as ConstantInfo;
            // 深拷贝
            ConstantInfo tmpconstantInfo = new ConstantInfo();
            tmpconstantInfo.Gid = cuconstantInfo.Gid;
            tmpconstantInfo.Name = cuconstantInfo.Name;
            tmpconstantInfo.ConstantValue = cuconstantInfo.ConstantValue;
            tmpconstantInfo.Remark = cuconstantInfo.Remark;
            tmpconstantInfo.lstInfo = cuconstantInfo.lstInfo;

            // 更新内容
            cuconstantInfo.Gid = preconstantInfo.Gid;
            cuconstantInfo.Name = preconstantInfo.Name;
            cuconstantInfo.ConstantValue = preconstantInfo.ConstantValue;
            cuconstantInfo.Remark = preconstantInfo.Remark;
            cuconstantInfo.lstInfo = preconstantInfo.lstInfo;

            preconstantInfo.Gid = tmpconstantInfo.Gid;
            preconstantInfo.Name = tmpconstantInfo.Name;
            preconstantInfo.ConstantValue = tmpconstantInfo.ConstantValue;
            preconstantInfo.Remark = tmpconstantInfo.Remark;
            preconstantInfo.lstInfo = tmpconstantInfo.lstInfo;

            // 更细XML内容
            string cuXMLStr = xmlhelper.ReadInnerXML("datatype/item[@gid=\"" + cuconstantInfo.Gid + "\"]");
            string preXMLStr = xmlhelper.ReadInnerXML("datatype/item[@gid=\"" + preconstantInfo.Gid + "\"]");
            xmlhelper.Replace("datatype/item[@gid=\"" + cuconstantInfo.Gid + "\"]", preXMLStr);
            xmlhelper.Replace("datatype/item[@gid=\"" + preconstantInfo.Gid + "\"]", cuXMLStr);
            // 更新gid
            var cuattribute = xmlhelper.Read("datatype/item[@gid=\"" + cuconstantInfo.Gid + "\"]").Item(0).ParentNode.Attributes["gid"];
            var preattribute = xmlhelper.Read("datatype/item[@gid=\"" + preconstantInfo.Gid + "\"]").Item(0).ParentNode.Attributes["gid"];
            cuattribute.Value = preconstantInfo.Gid;
            preattribute.Value = cuconstantInfo.Gid;
            xmlhelper.Save(false);

            gridView1.RefreshData();
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle - 1;
        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle == (gridView1.RowCount - 2))
                return;

            ConstantInfo cuconstantInfo = gridView1.GetFocusedRow() as ConstantInfo;
            ConstantInfo nextconstantInfo = gridView1.GetRow(gridView1.FocusedRowHandle + 1) as ConstantInfo;

            // 深拷贝
            ConstantInfo tmpconstantInfo = new ConstantInfo();
            tmpconstantInfo.Gid = cuconstantInfo.Gid;
            tmpconstantInfo.Name = cuconstantInfo.Name;
            tmpconstantInfo.ConstantValue = cuconstantInfo.ConstantValue;
            tmpconstantInfo.Remark = cuconstantInfo.Remark;
            tmpconstantInfo.lstInfo = cuconstantInfo.lstInfo;

            // 更新内容
            cuconstantInfo.Gid = nextconstantInfo.Gid;
            cuconstantInfo.Name = nextconstantInfo.Name;
            cuconstantInfo.ConstantValue = nextconstantInfo.ConstantValue;
            cuconstantInfo.Remark = nextconstantInfo.Remark;
            cuconstantInfo.lstInfo = nextconstantInfo.lstInfo;

            nextconstantInfo.Gid = tmpconstantInfo.Gid;
            nextconstantInfo.Name = tmpconstantInfo.Name;
            nextconstantInfo.ConstantValue = tmpconstantInfo.ConstantValue;
            nextconstantInfo.Remark = tmpconstantInfo.Remark;
            nextconstantInfo.lstInfo = tmpconstantInfo.lstInfo;

            // 更细XML内容
            string cuXMLStr = xmlhelper.ReadInnerXML("datatype/item[@gid=\"" + cuconstantInfo.Gid + "\"]");
            string preXMLStr = xmlhelper.ReadInnerXML("datatype/item[@gid=\"" + nextconstantInfo.Gid + "\"]");
            xmlhelper.Replace("datatype/item[@gid=\"" + cuconstantInfo.Gid + "\"]", preXMLStr);
            xmlhelper.Replace("datatype/item[@gid=\"" + nextconstantInfo.Gid + "\"]", cuXMLStr);
            // 更新gid
            var cuattribute = xmlhelper.Read("datatype/item[@gid=\"" + cuconstantInfo.Gid + "\"]").Item(0).ParentNode.Attributes["gid"];
            var nextattribute = xmlhelper.Read("datatype/item[@gid=\"" + nextconstantInfo.Gid + "\"]").Item(0).ParentNode.Attributes["gid"];
            cuattribute.Value = nextconstantInfo.Gid;
            nextattribute.Value = cuconstantInfo.Gid;
            xmlhelper.Save(false);

            gridView1.RefreshData();
            gridView1.FocusedRowHandle = gridView1.FocusedRowHandle + 1;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = DataTableHelper.CreateTable("名称,常量值,说明");
            var lst = (gridView1.DataSource as List<ConstantInfo>);
            for (int i = 0; i < lst.Count; i++)
            {
                DataRow row = dt.NewRow();
                row[0] = lst[i].Name;
                row[1] = lst[i].ConstantValue;
                row[2] = lst[i].Remark;
                dt.Rows.Add(row);
            }

            string saveFile = FileDialogHelper.SaveExcel("用户常量.xls", "C:\\");
            if (!string.IsNullOrEmpty(saveFile))
            {
                MyXlsHelper.Export(dt, saveFile, "用户常量", 1, 1);

                if (MessageDxUtil.ShowYesNoAndTips("保存成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(saveFile);
                }
            }
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            string importFile = FileDialogHelper.OpenExcel(false);
            if (!string.IsNullOrEmpty(importFile))
            {
                // 判断文件是否被占用
                if (FileUtil.FileIsUsing(importFile))
                {
                    MessageDxUtil.ShowWarning(string.Format("文件[{0}]被占用，请先关闭文件后再重试!", importFile));
                    return;
                }

                DataTable dt = MyXlsHelper.Import(importFile, "用户常量", 2, 1);

                // 如果没有结果集就不在继续
                if (dt == null) return;

                Int32 addRows = 0;
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                { 
                    // 判断是否存在不存在则添加
                    if (!lstName.Contains(dt.Rows[i][0].ToString()))
                    {
                        var constantInfo = new ConstantInfo();
                        constantInfo.Gid = System.Guid.NewGuid().ToString();
                        constantInfo.Name = dt.Rows[i][0].ToString();
                        constantInfo.ConstantValue = dt.Rows[i][1].ToString();
                        constantInfo.Remark = dt.Rows[i][2].ToString();

                        xmlhelper.InsertElement("datatype", "item", "gid", constantInfo.Gid, string.Format(xmlModel, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString()));

                        (gridView1.DataSource as List<ConstantInfo>).Insert((gridView1.DataSource as List<ConstantInfo>).Count - 1, constantInfo);
                        addRows++;
                        lstName.Add(dt.Rows[i][0].ToString());
                    }
                }
                xmlhelper.Save(false);

                gridView1.RefreshData();
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barErrText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var infoDetail = new FrmInfoDetail(0, _errlst);
            infoDetail.ShowDialog();
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barWarningText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var infoDetail = new FrmInfoDetail(1, _warnlst);
            infoDetail.ShowDialog();
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barInfoText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var infoDetail = new FrmInfoDetail(2, _infolst);
            infoDetail.ShowDialog();
        }

        private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            ClearValidate();

            ValidateData();

            InitView();
        }
    }
}
