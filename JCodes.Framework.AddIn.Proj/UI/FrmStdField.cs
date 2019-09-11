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
using JCodes.Framework.Common.Extension;

// 参考文档 http://www.cnblogs.com/a1656344531/archive/2012/11/28/2792863.html

namespace JCodes.Framework.AddIn.Proj
{
    /// <summary>
    /// 登陆系统的黑白名单列表
    /// </summary>	
    public partial class FrmStdField : BaseDock
    {
        private StdFieldInfo tmpstdFieldInfo = null;

        private XmlHelper xmlhelper = new XmlHelper(@"XML\stdfield.xml");

        private List<string> lstName = new List<string>();

        private List<DictInfo> dictTypeInfoList = null;

        private string xmlModel = "<name>{0}</name><chineseName>{1}</chineseName><datatype>{2}</datatype><dictno>{3}</dictno><remark>{4}</remark>";

        private Int32 _errCount = 0;
        private List<CListItem> _errlst = new List<CListItem>();
        private Int32 _warnCount = 0;
        private List<CListItem> _warnlst = new List<CListItem>();
        private Int32 _infoCount = 0;
        private List<CListItem> _infolst = new List<CListItem>();

        public FrmStdField()
        {
            InitializeComponent();

            LoadDicData();

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
                string filter = string.Format("Name Like '%{0}%' or ChineseName Like '%{0}%' or DataType Like '%{0}%' or  DictNo Like '%{0}%' or  Remark Like '%{0}%' ", txtSearch.Text);
                gridView1.ActiveFilterString = filter;
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype/dataitem");
            List<StdFieldInfo> stdfieldInfoList = new List<StdFieldInfo>();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                StdFieldInfo dataTypeInfo = new StdFieldInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值
                dataTypeInfo.GUID = xe.GetAttribute("guid").ToString();

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                dataTypeInfo.Name = xnl0.Item(0).InnerText;
                dataTypeInfo.ChineseName = xnl0.Item(1).InnerText;
                dataTypeInfo.DataType = xnl0.Item(2).InnerText;
                dataTypeInfo.DictNo = xnl0.Item(3).InnerText.ToInt32();
                if (dictTypeInfoList != null)
                { 
                    var dictType = dictTypeInfoList.Find(new Predicate<DictInfo>(dictinfo => dictinfo.Id == dataTypeInfo.DictNo));
                    if (dictType != null) dataTypeInfo.DictNameLst = dictType.Remark;
                }
                dataTypeInfo.Remark = xnl0.Item(4).InnerText;
                dataTypeInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();

                stdfieldInfoList.Add(dataTypeInfo);
            }

            // 添加一行空行
            stdfieldInfoList.Add(new StdFieldInfo());
            gridControl1.DataSource = stdfieldInfoList;

            #region 绑定stdType 数据源

            XmlHelper datatypexmlHelper = new XmlHelper(@"XML\datatype.xml");
            XmlNodeList datatypexmlNodeLst = datatypexmlHelper.Read("datatype");
            List<CListItem> dataTypeInfoList = new List<CListItem>();
            foreach (XmlNode xn1 in datatypexmlNodeLst)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                CListItem listItem = new CListItem(xnl0.Item(0).InnerText, string.Format("{0}-{1}", xnl0.Item(0).InnerText, xnl0.Item(1).InnerText));

                dataTypeInfoList.Add(listItem);
            }

            repositoryItemLookUpEditStdType.DataSource = dataTypeInfoList;
            #endregion

            gridView1.Columns["GUID"].Visible = false;
            gridView1.Columns["lstInfo"].Visible = false;
            gridView1.Columns["DictNameLst"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["DataType"].ColumnEdit = repositoryItemLookUpEditStdType;
        }

        /// <summary>
        /// 校验加载的数据是否存在异常的
        /// </summary>
        private void ValidateData()
        {
            // 查询是否存在2个键值的数据
            List<StdFieldInfo> lstDataTypeInfo = gridControl1.DataSource as List<StdFieldInfo>;

            // 查找重复的Name的值

            List<String> tmpName = new List<string>();
            foreach (StdFieldInfo dataTypeInfo in lstDataTypeInfo)
            {
                if (string.IsNullOrEmpty(dataTypeInfo.GUID))
                    continue;

                if (lstName.Contains(dataTypeInfo.Name))
                {
                    tmpName.Add(dataTypeInfo.Name);
                }

                lstName.Add(dataTypeInfo.Name);
            }

            foreach (StdFieldInfo dataTypeInfo in lstDataTypeInfo)
            {
                if (string.IsNullOrEmpty(dataTypeInfo.GUID))
                    continue;

                // 判断重复的 类型名
                if (tmpName.Contains(dataTypeInfo.Name))
                {
                    if (dataTypeInfo.lstInfo.ContainsKey("Name"))
                    {
                        dataTypeInfo.lstInfo["Name"].ErrorText = dataTypeInfo.lstInfo["Name"].ErrorText + "\r\n存在键值相同的字段名";
                        dataTypeInfo.lstInfo["Name"].ErrorType = dataTypeInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? dataTypeInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        dataTypeInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("存在键值相同的字段名", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("存在键值相同的字段名", "字段名" + dataTypeInfo.Name));
                    }
                }
                // 类型名判断大驼峰
                if (dataTypeInfo.Name.Length >= 1)
                {
                    if (!string.Equals(dataTypeInfo.Name[0].ToString(), dataTypeInfo.Name[0].ToString().ToUpper(), StringComparison.CurrentCulture))
                    {
                        if (dataTypeInfo.lstInfo.ContainsKey("Name"))
                        {
                            dataTypeInfo.lstInfo["Name"].ErrorText = dataTypeInfo.lstInfo["Name"].ErrorText + "\r\n字段名以大驼峰命名规范";
                            dataTypeInfo.lstInfo["Name"].ErrorType = dataTypeInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning ? dataTypeInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                        }
                        else
                        {
                            dataTypeInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("字段名以大驼峰命名规范", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning));
                            _warnCount++;
                            // 20170901 wjm 调整key 和value的顺序
                            _warnlst.Add(new CListItem("字段名以大驼峰命名规范", "类型名" + dataTypeInfo.Name));
                        }
                    }
                }

                // 判断字段名是否为空
                if (string.IsNullOrEmpty(dataTypeInfo.Name))
                {
                    if (dataTypeInfo.lstInfo.ContainsKey("Name"))
                    {
                        dataTypeInfo.lstInfo["Name"].ErrorText = dataTypeInfo.lstInfo["Name"].ErrorText + "\r\n字段名不能为空";
                        dataTypeInfo.lstInfo["Name"].ErrorType = dataTypeInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? dataTypeInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        dataTypeInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("字段名不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("字段名不能为空", "字段名" + dataTypeInfo.Name));
                    }
                }

                // 判断字段名称是否为空
                if (string.IsNullOrEmpty(dataTypeInfo.ChineseName))
                {
                    if (dataTypeInfo.lstInfo.ContainsKey("ChineseName"))
                    {
                        dataTypeInfo.lstInfo["ChineseName"].ErrorText = dataTypeInfo.lstInfo["ChineseName"].ErrorText + "\r\n字段名称不能为空";
                        dataTypeInfo.lstInfo["ChineseName"].ErrorType = dataTypeInfo.lstInfo["ChineseName"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? dataTypeInfo.lstInfo["ChineseName"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        dataTypeInfo.lstInfo.Add("ChineseName", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("字段名称不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("字段名称不能为空", "字段名称" + dataTypeInfo.ChineseName));
                    }
                }

                // 判断字段类型是否为空
                if (string.IsNullOrEmpty(dataTypeInfo.DataType))
                {
                    if (dataTypeInfo.lstInfo.ContainsKey("DataType"))
                    {
                        dataTypeInfo.lstInfo["DataType"].ErrorText = dataTypeInfo.lstInfo["DataType"].ErrorText + "\r\n字段类型不能为空";
                        dataTypeInfo.lstInfo["DataType"].ErrorType = dataTypeInfo.lstInfo["DataType"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? dataTypeInfo.lstInfo["DataType"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        dataTypeInfo.lstInfo.Add("DataType", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("字段类型不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("字段类型不能为空", "字段类型" + dataTypeInfo.DataType));
                    }
                }

                // 增加校验，如果数据字段不存在则报错
                if (dataTypeInfo.DictNo > 0 && dictTypeInfoList.Find(new Predicate<DictInfo>(dictinfo => dictinfo.Id == dataTypeInfo.DictNo)) == null)
                {
                    if (dataTypeInfo.lstInfo.ContainsKey("DictNo"))
                    {
                        dataTypeInfo.lstInfo["DictNo"].ErrorText = dataTypeInfo.lstInfo["DictNo"].ErrorText + "\r\n字典条目值在数据字典中找不到";
                        dataTypeInfo.lstInfo["DictNo"].ErrorType = dataTypeInfo.lstInfo["DictNo"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? dataTypeInfo.lstInfo["DictNo"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        dataTypeInfo.lstInfo.Add("DictNo", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("字典条目值在数据字典中找不到", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("字典条目值在数据字典中找不到", "字典条目" + dataTypeInfo.DataType));
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
            List<StdFieldInfo> lstDataTypeInfo = gridControl1.DataSource as List<StdFieldInfo>;

            foreach (StdFieldInfo dataTypeInfo in lstDataTypeInfo)
            {
                if (string.IsNullOrEmpty(dataTypeInfo.GUID))
                    continue;

                dataTypeInfo.lstInfo.Clear();
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

            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype/dataitem/item[@guid=\"" + tmpstdFieldInfo.GUID + "\"]");
            Int32 idx = -1;

            switch (e.Column.ToString())
            {
                case "字段名":
                    idx = 0;
                    break;
                case "字段名称":
                    idx = 1;
                    break;
                case "字段类型":
                    idx = 2;
                    break;
                case "字典条目":
                    idx = 3;
                    break;
                case "说明":
                    idx = 4;
                    break;
            }
            
           if (idx == -1)
                return;

            xmlNodeLst.Item(idx).InnerText = e.Value.ToString();
            xmlhelper.Save(false);

            tmpstdFieldInfo = null;

            if (idx == 3 && dictTypeInfoList != null)
            {


                var dictType = dictTypeInfoList.Find(new Predicate<DictInfo>(dictinfo => dictinfo.Id == e.Value.ToString().ToInt32()));

                // 找到选中行的GUID值
                (gridView1.GetFocusedRow() as StdFieldInfo).DictNameLst = dictType.Remark;
                gridView1.RefreshData();
            }
            
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty((gridView1.GetFocusedRow() as StdFieldInfo).GUID) && (gridView1.FocusedRowHandle + 1 == gridView1.RowCount))
            {
                btnAdd_Click(null, null);
            }

            tmpstdFieldInfo = gridView1.GetRow(e.RowHandle) as StdFieldInfo;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var datatypeInfo = new StdFieldInfo();
            datatypeInfo.GUID = System.Guid.NewGuid().ToString();
            datatypeInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();

            xmlhelper.InsertElement("datatype/dataitem", "item", "guid", datatypeInfo.GUID, string.Format(xmlModel, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty));
            xmlhelper.Save(false);

            (gridView1.DataSource as List<StdFieldInfo>).Insert(gridView1.RowCount - 1, datatypeInfo);
            gridView1.RefreshData();
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsert_Click(object sender, EventArgs e)
        {
            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype/dataitem/item[@guid=\"" + (gridView1.GetFocusedRow() as StdFieldInfo).GUID + "\"]");
            var datatypeInfo = new StdFieldInfo();
            datatypeInfo.GUID = System.Guid.NewGuid().ToString();
            datatypeInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
            xmlhelper.InsertElement("item", "guid", datatypeInfo.GUID, string.Format(xmlModel, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty), xmlNodeLst.Item(0).ParentNode);
            xmlhelper.Save(false);

            (gridView1.DataSource as List<StdFieldInfo>).Insert(gridView1.FocusedRowHandle, datatypeInfo);
            gridView1.RefreshData();
        }

        /// <summary>
        /// 删除标准类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            // 20171106 wjm 修复没有数据做删除操作报错问题
            // 20170824 如果是最后一行空行则不再继续操作
            if (gridView1.GetFocusedRow() as StdFieldInfo == null || string.IsNullOrEmpty((gridView1.GetFocusedRow() as StdFieldInfo).GUID))
                return;

            xmlhelper.DeleteByPathNode("datatype/dataitem/item[@guid=\"" + gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "GUID") + "\"]");
            xmlhelper.Save(false);

            // 20170924 wjm 删除lstName 对应的值保存导入的时候缓存问题
            lstName.Remove(gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "Name"));

            (gridView1.DataSource as List<StdFieldInfo>).RemoveAt(gridView1.FocusedRowHandle);
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

            StdFieldInfo cudataTypeInfo = gridView1.GetFocusedRow() as StdFieldInfo;
            StdFieldInfo predataTypeInfo = gridView1.GetRow(gridView1.FocusedRowHandle - 1) as StdFieldInfo;
            // 深拷贝
            StdFieldInfo tmpdataTypeInfo = new StdFieldInfo();
            tmpdataTypeInfo.GUID = cudataTypeInfo.GUID;
            tmpdataTypeInfo.Name = cudataTypeInfo.Name;
            tmpdataTypeInfo.ChineseName = cudataTypeInfo.ChineseName;
            tmpdataTypeInfo.DataType = cudataTypeInfo.DataType;
            tmpdataTypeInfo.DictNo = cudataTypeInfo.DictNo;
            tmpdataTypeInfo.Remark = cudataTypeInfo.Remark;
            tmpdataTypeInfo.lstInfo = cudataTypeInfo.lstInfo;

            // 更新内容
            cudataTypeInfo.GUID = predataTypeInfo.GUID;
            cudataTypeInfo.Name = predataTypeInfo.Name;
            cudataTypeInfo.ChineseName = predataTypeInfo.ChineseName;
            cudataTypeInfo.DataType = predataTypeInfo.DataType;
            cudataTypeInfo.DictNo = predataTypeInfo.DictNo;
            cudataTypeInfo.Remark = predataTypeInfo.Remark;
            cudataTypeInfo.lstInfo = predataTypeInfo.lstInfo;

            predataTypeInfo.GUID = tmpdataTypeInfo.GUID;
            predataTypeInfo.Name = tmpdataTypeInfo.Name;
            predataTypeInfo.ChineseName = tmpdataTypeInfo.ChineseName;
            predataTypeInfo.DataType = tmpdataTypeInfo.DataType;
            predataTypeInfo.DictNo = tmpdataTypeInfo.DictNo;
            predataTypeInfo.Remark = tmpdataTypeInfo.Remark;
            predataTypeInfo.lstInfo = tmpdataTypeInfo.lstInfo;

            // 更细XML内容
            string cuXMLStr = xmlhelper.ReadInnerXML("datatype/dataitem/item[@guid=\"" + cudataTypeInfo.GUID + "\"]");
            string preXMLStr = xmlhelper.ReadInnerXML("datatype/dataitem/item[@guid=\"" + predataTypeInfo.GUID + "\"]");
            xmlhelper.Replace("datatype/dataitem/item[@guid=\"" + cudataTypeInfo.GUID + "\"]", preXMLStr);
            xmlhelper.Replace("datatype/dataitem/item[@guid=\"" + predataTypeInfo.GUID + "\"]", cuXMLStr);
            // 更新GUID
            var cuattribute = xmlhelper.Read("datatype/dataitem/item[@guid=\"" + cudataTypeInfo.GUID + "\"]").Item(0).ParentNode.Attributes["guid"];
            var preattribute = xmlhelper.Read("datatype/dataitem/item[@guid=\"" + predataTypeInfo.GUID + "\"]").Item(0).ParentNode.Attributes["guid"];
            cuattribute.Value = predataTypeInfo.GUID;
            preattribute.Value = cudataTypeInfo.GUID;
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

            StdFieldInfo cudataTypeInfo = gridView1.GetFocusedRow() as StdFieldInfo;
            StdFieldInfo nextdataTypeInfo = gridView1.GetRow(gridView1.FocusedRowHandle + 1) as StdFieldInfo;

            // 深拷贝
            StdFieldInfo tmpdataTypeInfo = new StdFieldInfo();
            tmpdataTypeInfo.GUID = cudataTypeInfo.GUID;
            tmpdataTypeInfo.Name = cudataTypeInfo.Name;
            tmpdataTypeInfo.ChineseName = cudataTypeInfo.ChineseName;
            tmpdataTypeInfo.DataType = cudataTypeInfo.DataType;
            tmpdataTypeInfo.DictNo = cudataTypeInfo.DictNo;
            tmpdataTypeInfo.Remark = cudataTypeInfo.Remark;
            tmpdataTypeInfo.lstInfo = cudataTypeInfo.lstInfo;

            // 更新内容
            cudataTypeInfo.GUID = nextdataTypeInfo.GUID;
            cudataTypeInfo.Name = nextdataTypeInfo.Name;
            cudataTypeInfo.ChineseName = nextdataTypeInfo.ChineseName;
            cudataTypeInfo.DataType = nextdataTypeInfo.DataType;
            cudataTypeInfo.DictNo = nextdataTypeInfo.DictNo;
            cudataTypeInfo.Remark = nextdataTypeInfo.Remark;
            cudataTypeInfo.lstInfo = nextdataTypeInfo.lstInfo;

            nextdataTypeInfo.GUID = tmpdataTypeInfo.GUID;
            nextdataTypeInfo.Name = tmpdataTypeInfo.Name;
            nextdataTypeInfo.ChineseName = tmpdataTypeInfo.ChineseName;
            nextdataTypeInfo.DataType = tmpdataTypeInfo.DataType;
            nextdataTypeInfo.DictNo = tmpdataTypeInfo.DictNo;
            nextdataTypeInfo.Remark = tmpdataTypeInfo.Remark;
            nextdataTypeInfo.lstInfo = tmpdataTypeInfo.lstInfo;

            // 更细XML内容
            string cuXMLStr = xmlhelper.ReadInnerXML("datatype/dataitem/item[@guid=\"" + cudataTypeInfo.GUID + "\"]");
            string preXMLStr = xmlhelper.ReadInnerXML("datatype/dataitem/item[@guid=\"" + nextdataTypeInfo.GUID + "\"]");
            xmlhelper.Replace("datatype/dataitem/item[@guid=\"" + cudataTypeInfo.GUID + "\"]", preXMLStr);
            xmlhelper.Replace("datatype/dataitem/item[@guid=\"" + nextdataTypeInfo.GUID + "\"]", cuXMLStr);
            // 更新GUID
            var cuattribute = xmlhelper.Read("datatype/dataitem/item[@guid=\"" + cudataTypeInfo.GUID + "\"]").Item(0).ParentNode.Attributes["guid"];
            var nextattribute = xmlhelper.Read("datatype/dataitem/item[@guid=\"" + nextdataTypeInfo.GUID + "\"]").Item(0).ParentNode.Attributes["guid"];
            cuattribute.Value = nextdataTypeInfo.GUID;
            nextattribute.Value = cudataTypeInfo.GUID;
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
            DataTable dt = DataTableHelper.CreateTable("字段名,字段名称,字段类型,字段条目,字段条目说明,备注");
            var lst = (gridView1.DataSource as List<StdFieldInfo>);
            for (int i = 0; i < lst.Count; i++)
            {
                DataRow row = dt.NewRow();
                row[0] = lst[i].Name;
                row[1] = lst[i].ChineseName;
                row[2] = lst[i].DataType;
                row[3] = lst[i].DictNo;
                row[4] = lst[i].DictNameLst;
                row[5] = lst[i].Remark;
                dt.Rows.Add(row);
            }

            string saveFile = FileDialogHelper.SaveExcel("标准字段.xls", "C:\\");
            if (!string.IsNullOrEmpty(saveFile))
            {
                MyXlsHelper.Export(dt, saveFile, "标准字段", 1, 1);

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

                DataTable dt = MyXlsHelper.Import(importFile, "标准字段", 2, 1);

                // 如果没有结果集就不在继续
                if (dt == null) return;

                Int32 addRows = 0;
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                { 
                    // 判断是否存在不存在则添加
                    if (!lstName.Contains(dt.Rows[i][0].ToString()))
                    {
                        var datatypeInfo = new StdFieldInfo();
                        datatypeInfo.GUID = System.Guid.NewGuid().ToString();
                        datatypeInfo.Name = dt.Rows[i][0].ToString();
                        datatypeInfo.ChineseName = dt.Rows[i][1].ToString();
                        datatypeInfo.DataType = dt.Rows[i][2].ToString();
                        datatypeInfo.DictNo =  dt.Rows[i][3].ToString().ToInt32();
                        datatypeInfo.Remark = dt.Rows[i][5].ToString();

                        xmlhelper.InsertElement("datatype/dataitem", "item", "guid", datatypeInfo.GUID, string.Format(xmlModel, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][5].ToString()));

                        (gridView1.DataSource as List<StdFieldInfo>).Insert(gridView1.RowCount - 1, datatypeInfo);
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

            LoadDicData();

            ValidateData();

            InitView();
        }

        /// <summary>
        /// 加载数据字典
        /// </summary>
        private void LoadDicData()
        {
            #region 加载数据字典大项
            XmlHelper xmldicthelper = new XmlHelper(@"XML\dict.xml");
            XmlNodeList xmlNodeLst = xmldicthelper.Read("datatype/dataitem");
            dictTypeInfoList = new List<DictInfo>();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                DictInfo dictInfo = new DictInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                dictInfo.Id = Convert.ToInt32(xnl0.Item(0).InnerText);
                dictInfo.Pid = Convert.ToInt32(xnl0.Item(1).InnerText);
                dictInfo.Name = xnl0.Item(2).InnerText;

                StringBuilder sb = new StringBuilder();

                XmlNodeList xmlNodeLst2 = xmldicthelper.Read(string.Format("datatype/dataitem/item[id=\"{0}\"]/subdic", dictInfo.Id));

                List<DictDetailInfo> dictDetailInfoList = new List<DictDetailInfo>();
                foreach (XmlNode xn12 in xmlNodeLst2)
                {
                    // 将节点转换为元素，便于得到节点的属性值
                    XmlElement xe2 = (XmlElement)xn12;

                    // 得到DataTypeInfo节点的所有子节点
                    XmlNodeList xnl02 = xe2.ChildNodes;
                    sb.Append(string.Format("{0}-{1},\r\n", xnl02.Item(0).InnerText, xnl02.Item(1).InnerText));
                }

                dictInfo.Remark = sb.ToString();

                dictTypeInfoList.Add(dictInfo);
            }
            #endregion
        }
    }
}
