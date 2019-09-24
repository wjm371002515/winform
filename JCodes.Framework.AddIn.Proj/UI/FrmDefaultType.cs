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
    public partial class FrmDefaultType : BaseDock
    {
        private DefaultTypeInfo tmpdataTypeInfo = null;

        private XmlHelper xmlhelper = new XmlHelper(@"XML\defaulttype.xml");

        private List<string> lstName = new List<string>();

        private string xmlModel = "<name>{0}</name><chineseName>{1}</chineseName><oracle>{2}</oracle><mysql>{3}</mysql><db2>{4}</db2><sqlserver>{5}</sqlserver><sqlite>{6}</sqlite><access>{7}</access><cshort>{8}</cshort>";

        private Int32 _errCount = 0;
        private List<CListItem> _errlst = new List<CListItem>();
        private Int32 _warnCount = 0;
        private List<CListItem> _warnlst = new List<CListItem>();
        private Int32 _infoCount = 0;
        private List<CListItem> _infolst = new List<CListItem>();

        public FrmDefaultType()
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
                string filter = string.Format("Name Like '%{0}%' or ChineseName Like '%{0}%' or Oracle Like '%{0}%' or  Mysql Like '%{0}%' or  DB2 Like '%{0}%' or  SqlServer Like '%{0}%' or Sqlite Like '%{0}%' or Access Like '%{0}%' or CShort Like '%{0}%'", txtSearch.Text);
                gridView1.ActiveFilterString = filter;
            }
            
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype");
            List<DefaultTypeInfo> dataTypeInfoList = new List<DefaultTypeInfo>();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                DefaultTypeInfo dataTypeInfo = new DefaultTypeInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值
                dataTypeInfo.Gid = xe.GetAttribute("gid").ToString();

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                dataTypeInfo.Name = xnl0.Item(0).InnerText;
                dataTypeInfo.ChineseName = xnl0.Item(1).InnerText;
                dataTypeInfo.Oracle = xnl0.Item(2).InnerText;
                dataTypeInfo.Mysql = xnl0.Item(3).InnerText;
                dataTypeInfo.DB2 = xnl0.Item(4).InnerText;
                dataTypeInfo.SqlServer = xnl0.Item(5).InnerText;
                dataTypeInfo.Sqlite = xnl0.Item(6).InnerText;
                dataTypeInfo.Access = xnl0.Item(7).InnerText;
                dataTypeInfo.CShort = xnl0.Item(8).InnerText;
                dataTypeInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();

                dataTypeInfoList.Add(dataTypeInfo);
            }

            // 添加一行空行
            dataTypeInfoList.Add(new DefaultTypeInfo());
            gridControl1.DataSource = dataTypeInfoList;

            gridView1.Columns["Gid"].Visible = false;
            gridView1.Columns["lstInfo"].Visible = false;
        }

        /// <summary>
        /// 校验加载的数据是否存在异常的
        /// </summary>
        private void ValidateData()
        {
            // 查询是否存在2个键值的数据
            List<DefaultTypeInfo> lstDataTypeInfo = gridControl1.DataSource as List<DefaultTypeInfo>;

            // 查找重复的Name的值

            List<String> tmpName = new List<string>();
            foreach (DefaultTypeInfo dataTypeInfo in lstDataTypeInfo)
            {
                if (string.IsNullOrEmpty(dataTypeInfo.Gid))
                    continue;

                if (lstName.Contains(dataTypeInfo.Name))
                {
                    tmpName.Add(dataTypeInfo.Name);
                }

                lstName.Add(dataTypeInfo.Name);
            }

            foreach (DefaultTypeInfo dataTypeInfo in lstDataTypeInfo)
            {
                if (string.IsNullOrEmpty(dataTypeInfo.Gid))
                    continue;

                // 判断重复的 类型名
                if (tmpName.Contains(dataTypeInfo.Name))
                {
                    if (dataTypeInfo.lstInfo.ContainsKey("Name"))
                    {
                        dataTypeInfo.lstInfo["Name"].ErrorText = dataTypeInfo.lstInfo["Name"].ErrorText + "\r\n存在键值相同的类型名";
                        dataTypeInfo.lstInfo["Name"].ErrorType = dataTypeInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? dataTypeInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        dataTypeInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("存在键值相同的类型名", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem( "存在键值相同的类型名", "类型名" + dataTypeInfo.Name));
                    }
                }
                // 类型名判断大驼峰
                if (dataTypeInfo.Name.Length >= 1)
                {
                    if (!string.Equals(dataTypeInfo.Name[0].ToString(), dataTypeInfo.Name[0].ToString().ToUpper(), StringComparison.CurrentCulture))
                    {
                        if (dataTypeInfo.lstInfo.ContainsKey("Name"))
                        {
                            dataTypeInfo.lstInfo["Name"].ErrorText = dataTypeInfo.lstInfo["Name"].ErrorText + "\r\n类型名以大驼峰命名规范";
                            dataTypeInfo.lstInfo["Name"].ErrorType = dataTypeInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning ? dataTypeInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                        }
                        else
                        {
                            dataTypeInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("类型名以大驼峰命名规范", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning));
                            _warnCount++;
                            // 20170901 wjm 调整key 和value的顺序
                            _warnlst.Add(new CListItem("类型名以大驼峰命名规范", "类型名" + dataTypeInfo.Name));
                        }
                    }
                }

                // 判断类型名是否为空
                if (string.IsNullOrEmpty(dataTypeInfo.Name))
                {
                    if (dataTypeInfo.lstInfo.ContainsKey("Name"))
                    {
                        dataTypeInfo.lstInfo["Name"].ErrorText = dataTypeInfo.lstInfo["Name"].ErrorText + "\r\n类型名不能为空";
                        dataTypeInfo.lstInfo["Name"].ErrorType = dataTypeInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? dataTypeInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        dataTypeInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("类型名不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("类型名不能为空", "类型名" + dataTypeInfo.Name));
                    }
                }
                // 判断名称是否为空
                if (string.IsNullOrEmpty(dataTypeInfo.ChineseName))
                {
                    if (dataTypeInfo.lstInfo.ContainsKey("ChineseName"))
                    {
                        dataTypeInfo.lstInfo["ChineseName"].ErrorText = dataTypeInfo.lstInfo["ChineseName"].ErrorText + "\r\n名称不能为空";
                        dataTypeInfo.lstInfo["ChineseName"].ErrorType = dataTypeInfo.lstInfo["ChineseName"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? dataTypeInfo.lstInfo["ChineseName"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        dataTypeInfo.lstInfo.Add("ChineseName", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("名称不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("名称不能为空", "名称" + dataTypeInfo.Name));
                    }
                }
                // 判断Oracle是否为空
                if (string.IsNullOrEmpty(dataTypeInfo.Oracle))
                {
                    if (dataTypeInfo.lstInfo.ContainsKey("Oracle"))
                    {
                        dataTypeInfo.lstInfo["Oracle"].ErrorText = dataTypeInfo.lstInfo["Oracle"].ErrorText + "\r\nOracle不能为空";
                        dataTypeInfo.lstInfo["Oracle"].ErrorType = dataTypeInfo.lstInfo["Oracle"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? dataTypeInfo.lstInfo["Oracle"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        dataTypeInfo.lstInfo.Add("Oracle", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("Oracle不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("Oracle不能为空", "Oracle" + dataTypeInfo.Name));
                    }
                }
                // 判断Mysql是否为空
                if (string.IsNullOrEmpty(dataTypeInfo.Mysql))
                {
                    if (dataTypeInfo.lstInfo.ContainsKey("Mysql"))
                    {
                        dataTypeInfo.lstInfo["Mysql"].ErrorText = dataTypeInfo.lstInfo["Mysql"].ErrorText + "\r\nMysql不能为空";
                        dataTypeInfo.lstInfo["Mysql"].ErrorType = dataTypeInfo.lstInfo["Mysql"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? dataTypeInfo.lstInfo["Mysql"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        dataTypeInfo.lstInfo.Add("Mysql", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("Mysql不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("Mysql不能为空", "Mysql" + dataTypeInfo.Name));
                    }
                }
                // 判断DB2是否为空
                if (string.IsNullOrEmpty(dataTypeInfo.DB2))
                {
                    if (dataTypeInfo.lstInfo.ContainsKey("DB2"))
                    {
                        dataTypeInfo.lstInfo["DB2"].ErrorText = dataTypeInfo.lstInfo["DB2"].ErrorText + "\r\nDB2不能为空";
                        dataTypeInfo.lstInfo["DB2"].ErrorType = dataTypeInfo.lstInfo["DB2"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? dataTypeInfo.lstInfo["DB2"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        dataTypeInfo.lstInfo.Add("DB2", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("DB2不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem( "DB2不能为空", "DB2" + dataTypeInfo.Name));
                    }
                }
                // 判断SqlServer是否为空
                if (string.IsNullOrEmpty(dataTypeInfo.SqlServer))
                {
                    if (dataTypeInfo.lstInfo.ContainsKey("SqlServer"))
                    {
                        dataTypeInfo.lstInfo["SqlServer"].ErrorText = dataTypeInfo.lstInfo["SqlServer"].ErrorText + "\r\nSqlServer不能为空";
                        dataTypeInfo.lstInfo["SqlServer"].ErrorType = dataTypeInfo.lstInfo["SqlServer"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? dataTypeInfo.lstInfo["SqlServer"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        dataTypeInfo.lstInfo.Add("SqlServer", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("SqlServer不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("SqlServer不能为空", "SqlServer" + dataTypeInfo.Name));
                    }
                }
                // 判断Sqlite是否为空
                if (string.IsNullOrEmpty(dataTypeInfo.Sqlite))
                {
                    if (dataTypeInfo.lstInfo.ContainsKey("Sqlite"))
                    {
                        dataTypeInfo.lstInfo["Sqlite"].ErrorText = dataTypeInfo.lstInfo["Sqlite"].ErrorText + "\r\nSqlite不能为空";
                        dataTypeInfo.lstInfo["Sqlite"].ErrorType = dataTypeInfo.lstInfo["Sqlite"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? dataTypeInfo.lstInfo["Sqlite"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        dataTypeInfo.lstInfo.Add("Sqlite", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("Sqlite不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("Sqlite不能为空", "Sqlite" + dataTypeInfo.Name));
                    }
                }
                // 判断Access是否为空
                if (string.IsNullOrEmpty(dataTypeInfo.Access))
                {
                    if (dataTypeInfo.lstInfo.ContainsKey("Access"))
                    {
                        dataTypeInfo.lstInfo["Access"].ErrorText = dataTypeInfo.lstInfo["Access"].ErrorText + "\r\nAccess不能为空";
                        dataTypeInfo.lstInfo["Access"].ErrorType = dataTypeInfo.lstInfo["Access"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? dataTypeInfo.lstInfo["Access"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        dataTypeInfo.lstInfo.Add("Access", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("Access不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("Access不能为空", "Access" + dataTypeInfo.Name));
                    }
                }
                if (string.IsNullOrEmpty(dataTypeInfo.CShort))
                {
                    if (dataTypeInfo.lstInfo.ContainsKey("CShort"))
                    {
                        dataTypeInfo.lstInfo["CShort"].ErrorText = dataTypeInfo.lstInfo["CShort"].ErrorText + "\r\nCShort不能为空";
                        dataTypeInfo.lstInfo["CShort"].ErrorType = dataTypeInfo.lstInfo["CShort"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? dataTypeInfo.lstInfo["CShort"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        dataTypeInfo.lstInfo.Add("CShort", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("CShort不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("CShort不能为空", "CShort" + dataTypeInfo.Name));
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
            List<DefaultTypeInfo> lstDataTypeInfo = gridControl1.DataSource as List<DefaultTypeInfo>;

            foreach (DefaultTypeInfo dataTypeInfo in lstDataTypeInfo)
            {
                if (string.IsNullOrEmpty(dataTypeInfo.Gid))
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

            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype/item[@gid=\"" + tmpdataTypeInfo.Gid + "\"]");
            Int32 idx = -1;

            switch (e.Column.ToString())
            {
                case "类型名":
                    idx = 0;
                    break;
                case "名称":
                    idx = 1;
                    break;
                case "Oracle":
                    idx = 2;
                    break;
                case "Mysql":
                    idx = 3;
                    break;
                case "DB2":
                    idx = 4;
                    break;
                case "Sql Server":
                    idx = 5;
                    break;
                case "Sqlite":
                    idx = 6;
                    break;
                case "Access":
                    idx = 7;
                    break;
                case "CShort":
                    idx = 8;
                    break;
            }
            
           if (idx == -1)
                return;

            xmlNodeLst.Item(idx).InnerText = e.Value.ToString();
            xmlhelper.Save(false);

            tmpdataTypeInfo = null;
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty((gridView1.GetFocusedRow() as DefaultTypeInfo).Gid) && (gridView1.FocusedRowHandle + 1 == gridView1.RowCount))
            {
                btnAdd_Click(null, null);
            }

            tmpdataTypeInfo = gridView1.GetRow(e.RowHandle) as DefaultTypeInfo;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var datatypeInfo = new DefaultTypeInfo();
            datatypeInfo.Gid = System.Guid.NewGuid().ToString();
            datatypeInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();

            xmlhelper.InsertElement("datatype", "item", "gid", datatypeInfo.Gid, string.Format(xmlModel, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty));
            xmlhelper.Save(false);

            (gridView1.DataSource as List<DefaultTypeInfo>).Insert(gridView1.RowCount - 1, datatypeInfo);
            gridView1.RefreshData();
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsert_Click(object sender, EventArgs e)
        {
            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype/item[@gid=\"" + (gridView1.GetFocusedRow() as DefaultTypeInfo).Gid + "\"]");
            var datatypeInfo = new DefaultTypeInfo();
            datatypeInfo.Gid = System.Guid.NewGuid().ToString();
            datatypeInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
            xmlhelper.InsertElement("item", "gid", datatypeInfo.Gid, string.Format(xmlModel, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty), xmlNodeLst.Item(0).ParentNode);
            xmlhelper.Save(false);

            (gridView1.DataSource as List<DefaultTypeInfo>).Insert(gridView1.FocusedRowHandle, datatypeInfo);
            gridView1.RefreshData();
        }

        /// <summary>
        /// 删除标准类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            // 20171106 wjm 修复没有数据删除报错问题
            // 20170824 如果是最后一行空行则不再继续操作
            if (gridView1.GetFocusedRow() as DefaultTypeInfo == null || string.IsNullOrEmpty((gridView1.GetFocusedRow() as DefaultTypeInfo).Gid))
                return;

            xmlhelper.DeleteByPathNode("datatype/item[@gid=\"" + gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "Gid") + "\"]");
            xmlhelper.Save(false);

            // 20170924 wjm 删除lstName 对应的值保存导入的时候缓存问题
            lstName.Remove(gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "Name"));

            (gridView1.DataSource as List<DefaultTypeInfo>).RemoveAt(gridView1.FocusedRowHandle);
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

            DefaultTypeInfo cudataTypeInfo = gridView1.GetFocusedRow() as DefaultTypeInfo;
            DefaultTypeInfo predataTypeInfo = gridView1.GetRow(gridView1.FocusedRowHandle - 1) as DefaultTypeInfo;
            // 深拷贝
            DefaultTypeInfo tmpdataTypeInfo = new DefaultTypeInfo();
            tmpdataTypeInfo.Gid = cudataTypeInfo.Gid;
            tmpdataTypeInfo.Name = cudataTypeInfo.Name;
            tmpdataTypeInfo.ChineseName = cudataTypeInfo.ChineseName;
            tmpdataTypeInfo.Oracle = cudataTypeInfo.Oracle;
            tmpdataTypeInfo.Mysql = cudataTypeInfo.Mysql;
            tmpdataTypeInfo.DB2 = cudataTypeInfo.DB2;
            tmpdataTypeInfo.SqlServer = cudataTypeInfo.SqlServer;
            tmpdataTypeInfo.Sqlite = cudataTypeInfo.Sqlite;
            tmpdataTypeInfo.Access = cudataTypeInfo.Access;
            tmpdataTypeInfo.CShort = cudataTypeInfo.CShort;
            tmpdataTypeInfo.lstInfo = cudataTypeInfo.lstInfo;

            // 更新内容
            cudataTypeInfo.Gid = predataTypeInfo.Gid;
            cudataTypeInfo.Name = predataTypeInfo.Name;
            cudataTypeInfo.ChineseName = predataTypeInfo.ChineseName;
            cudataTypeInfo.Oracle = predataTypeInfo.Oracle;
            cudataTypeInfo.Mysql = predataTypeInfo.Mysql;
            cudataTypeInfo.DB2 = predataTypeInfo.DB2;
            cudataTypeInfo.SqlServer = predataTypeInfo.SqlServer;
            cudataTypeInfo.Sqlite = predataTypeInfo.Sqlite;
            cudataTypeInfo.Access = predataTypeInfo.Access;
            cudataTypeInfo.CShort = predataTypeInfo.CShort;
            cudataTypeInfo.lstInfo = predataTypeInfo.lstInfo;

            predataTypeInfo.Gid = tmpdataTypeInfo.Gid;
            predataTypeInfo.Name = tmpdataTypeInfo.Name;
            predataTypeInfo.ChineseName = tmpdataTypeInfo.ChineseName;
            predataTypeInfo.Oracle = tmpdataTypeInfo.Oracle;
            predataTypeInfo.Mysql = tmpdataTypeInfo.Mysql;
            predataTypeInfo.DB2 = tmpdataTypeInfo.DB2;
            predataTypeInfo.SqlServer = tmpdataTypeInfo.SqlServer;
            predataTypeInfo.Sqlite = tmpdataTypeInfo.Sqlite;
            predataTypeInfo.Access = tmpdataTypeInfo.Access;
            predataTypeInfo.CShort = tmpdataTypeInfo.CShort;
            predataTypeInfo.lstInfo = tmpdataTypeInfo.lstInfo;

            // 更细XML内容
            string cuXMLStr = xmlhelper.ReadInnerXML("datatype/item[@gid=\"" + cudataTypeInfo.Gid + "\"]");
            string preXMLStr = xmlhelper.ReadInnerXML("datatype/item[@gid=\"" + predataTypeInfo.Gid + "\"]");
            xmlhelper.Replace("datatype/item[@gid=\"" + cudataTypeInfo.Gid + "\"]", preXMLStr);
            xmlhelper.Replace("datatype/item[@gid=\"" + predataTypeInfo.Gid + "\"]", cuXMLStr);
            // 更新 gid
            var cuattribute = xmlhelper.Read("datatype/item[@gid=\"" + cudataTypeInfo.Gid + "\"]").Item(0).ParentNode.Attributes["gid"];
            var preattribute = xmlhelper.Read("datatype/item[@gid=\"" + predataTypeInfo.Gid + "\"]").Item(0).ParentNode.Attributes["gid"];
            cuattribute.Value = predataTypeInfo.Gid;
            preattribute.Value = cudataTypeInfo.Gid;
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

            DefaultTypeInfo cudataTypeInfo = gridView1.GetFocusedRow() as DefaultTypeInfo;
            DefaultTypeInfo nextdataTypeInfo = gridView1.GetRow(gridView1.FocusedRowHandle + 1) as DefaultTypeInfo;

            // 深拷贝
            DefaultTypeInfo tmpdataTypeInfo = new DefaultTypeInfo();
            tmpdataTypeInfo.Gid = cudataTypeInfo.Gid;
            tmpdataTypeInfo.Name = cudataTypeInfo.Name;
            tmpdataTypeInfo.ChineseName = cudataTypeInfo.ChineseName;
            tmpdataTypeInfo.Oracle = cudataTypeInfo.Oracle;
            tmpdataTypeInfo.Mysql = cudataTypeInfo.Mysql;
            tmpdataTypeInfo.DB2 = cudataTypeInfo.DB2;
            tmpdataTypeInfo.SqlServer = cudataTypeInfo.SqlServer;
            tmpdataTypeInfo.Sqlite = cudataTypeInfo.Sqlite;
            tmpdataTypeInfo.Access = cudataTypeInfo.Access;
            tmpdataTypeInfo.CShort = cudataTypeInfo.CShort;
            tmpdataTypeInfo.lstInfo = cudataTypeInfo.lstInfo;

            // 更新内容
            cudataTypeInfo.Gid = nextdataTypeInfo.Gid;
            cudataTypeInfo.Name = nextdataTypeInfo.Name;
            cudataTypeInfo.ChineseName = nextdataTypeInfo.ChineseName;
            cudataTypeInfo.Oracle = nextdataTypeInfo.Oracle;
            cudataTypeInfo.Mysql = nextdataTypeInfo.Mysql;
            cudataTypeInfo.DB2 = nextdataTypeInfo.DB2;
            cudataTypeInfo.SqlServer = nextdataTypeInfo.SqlServer;
            cudataTypeInfo.Sqlite = nextdataTypeInfo.Sqlite;
            cudataTypeInfo.Access = nextdataTypeInfo.Access;
            cudataTypeInfo.CShort = nextdataTypeInfo.CShort;
            cudataTypeInfo.lstInfo = nextdataTypeInfo.lstInfo;

            nextdataTypeInfo.Gid = tmpdataTypeInfo.Gid;
            nextdataTypeInfo.Name = tmpdataTypeInfo.Name;
            nextdataTypeInfo.ChineseName = tmpdataTypeInfo.ChineseName;
            nextdataTypeInfo.Oracle = tmpdataTypeInfo.Oracle;
            nextdataTypeInfo.Mysql = tmpdataTypeInfo.Mysql;
            nextdataTypeInfo.DB2 = tmpdataTypeInfo.DB2;
            nextdataTypeInfo.SqlServer = tmpdataTypeInfo.SqlServer;
            nextdataTypeInfo.Sqlite = tmpdataTypeInfo.Sqlite;
            nextdataTypeInfo.Access = tmpdataTypeInfo.Access;
            nextdataTypeInfo.CShort = tmpdataTypeInfo.CShort;
            nextdataTypeInfo.lstInfo = tmpdataTypeInfo.lstInfo;

            // 更细XML内容
            string cuXMLStr = xmlhelper.ReadInnerXML("datatype/item[@gid=\"" + cudataTypeInfo.Gid + "\"]");
            string preXMLStr = xmlhelper.ReadInnerXML("datatype/item[@gid=\"" + nextdataTypeInfo.Gid + "\"]");
            xmlhelper.Replace("datatype/item[@gid=\"" + cudataTypeInfo.Gid + "\"]", preXMLStr);
            xmlhelper.Replace("datatype/item[@gid=\"" + nextdataTypeInfo.Gid + "\"]", cuXMLStr);
            // 更新 gid
            var cuattribute = xmlhelper.Read("datatype/item[@gid=\"" + cudataTypeInfo.Gid + "\"]").Item(0).ParentNode.Attributes["gid"];
            var nextattribute = xmlhelper.Read("datatype/item[@gid=\"" + nextdataTypeInfo.Gid + "\"]").Item(0).ParentNode.Attributes["gid"];
            cuattribute.Value = nextdataTypeInfo.Gid;
            nextattribute.Value = cudataTypeInfo.Gid;
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
            DataTable dt = DataTableHelper.CreateTable("类型名,名称,Oracle,Mysql,DB2,SqlServer,Sqlite,Access");
            var lst = (gridView1.DataSource as List<DefaultTypeInfo>);
            for (int i = 0; i < lst.Count; i++)
            {
                DataRow row = dt.NewRow();
                row[0] = lst[i].Name;
                row[1] = lst[i].ChineseName;
                row[2] = lst[i].Oracle;
                row[3] = lst[i].Mysql;
                row[4] = lst[i].DB2;
                row[5] = lst[i].SqlServer;
                row[6] = lst[i].Sqlite;
                row[7] = lst[i].Access;
                row[8] = lst[i].CShort;
                dt.Rows.Add(row);
            }

            string saveFile = FileDialogHelper.SaveExcel("标准数据类型.xls", "C:\\");
            if (!string.IsNullOrEmpty(saveFile))
            {
                MyXlsHelper.Export(dt, saveFile, "标准数据类型", 1, 1);

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

                DataTable dt = MyXlsHelper.Import(importFile, "标准数据类型", 2, 1);

                // 如果没有结果集就不在继续
                if (dt == null) return;

                Int32 addRows = 0;
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                { 
                    // 判断是否存在不存在则添加
                    if (!lstName.Contains(dt.Rows[i][0].ToString()))
                    {
                        var datatypeInfo = new DefaultTypeInfo();
                        datatypeInfo.Gid = System.Guid.NewGuid().ToString();
                        datatypeInfo.Name = dt.Rows[i][0].ToString();
                        datatypeInfo.ChineseName = dt.Rows[i][1].ToString();
                        datatypeInfo.Oracle = dt.Rows[i][2].ToString();
                        datatypeInfo.Mysql = dt.Rows[i][3].ToString();
                        datatypeInfo.DB2 = dt.Rows[i][4].ToString();
                        datatypeInfo.SqlServer = dt.Rows[i][5].ToString();
                        datatypeInfo.Sqlite = dt.Rows[i][6].ToString();
                        datatypeInfo.Access = dt.Rows[i][7].ToString();
                        datatypeInfo.CShort = dt.Rows[i][8].ToString();

                        xmlhelper.InsertElement("datatype", "item", "gid", datatypeInfo.Gid, string.Format(xmlModel, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString(), dt.Rows[i][7].ToString(), dt.Rows[i][8].ToString()));

                        (gridView1.DataSource as List<DefaultTypeInfo>).Insert(gridView1.RowCount - 1, datatypeInfo);
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
