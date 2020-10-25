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
    public partial class FrmErrorno : BaseDock
    {
        private ErrornoInfo tmperrornoInfo = null;

        private XmlHelper xmlhelper = new XmlHelper(@"XML\errorno.xml");

        private List<string> lstName = new List<string>();

        private string xmlModel = "<name>{0}</name><chineseName>{1}</chineseName><errType>{2}</errType><remark>{3}</remark>";

        private Int32 _errCount = 0;
        private List<CListItem> _errlst = new List<CListItem>();
        private Int32 _warnCount = 0;
        private List<CListItem> _warnlst = new List<CListItem>();
        private Int32 _infoCount = 0;
        private List<CListItem> _infolst = new List<CListItem>();

        public FrmErrorno()
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
                string filter = string.Format("Name Like '%{0}%' or ChineseName Like '%{0}%' or LogLevel Like '%{0}%' or Remark Like '%{0}%' ", txtSearch.Text);
                gridView1.ActiveFilterString = filter;
            }
            
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype");
            List<ErrornoInfo> errornoInfoList = new List<ErrornoInfo>();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                ErrornoInfo errornoInfo = new ErrornoInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值
                errornoInfo.Gid = xe.GetAttribute("gid").ToString();

                // 得到ErrornoInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                errornoInfo.Name = xnl0.Item(0).InnerText;
                errornoInfo.ChineseName = xnl0.Item(1).InnerText;
                errornoInfo.LogLevel = string.IsNullOrEmpty(xnl0.Item(2).InnerText ) ? (short)-1 : Convert.ToInt16( xnl0.Item(2).InnerText );
                errornoInfo.Remark = xnl0.Item(3).InnerText;
                errornoInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();

                errornoInfoList.Add(errornoInfo);
            }

            // 添加一行空行
            errornoInfoList.Add(new ErrornoInfo());
            gridControl1.DataSource = errornoInfoList;

            #region 绑定错误级别 数据源 
            // 1 - LOG_LEVEL_EMERG
            // 2 - LOG_LEVEL_ALERT
            // 3 - LOG_LEVEL_CRIT
            // 4 - LOG_LEVEL_ERR
            // 5 - LOG_LEVEL_WARN
            // 6 - LOG_LEVEL_NOTICE
            // 7 - LOG_LEVEL_INFO
            // 8 - LOG_LEVEL_DEBUG
            // 9 - LOG_LEVEL_SQL 
            List<CListItem> errTypeInfoList = new List<CListItem>();
            errTypeInfoList.Add(new CListItem("1", "LOG_LEVEL_EMERG"));
            errTypeInfoList.Add(new CListItem("2", "LOG_LEVEL_ALERT"));
            errTypeInfoList.Add(new CListItem("3", "LOG_LEVEL_CRIT"));
            errTypeInfoList.Add(new CListItem("4", "LOG_LEVEL_ERR"));
            errTypeInfoList.Add(new CListItem("5", "LOG_LEVEL_WARN"));
            errTypeInfoList.Add(new CListItem("6", "LOG_LEVEL_NOTICE"));
            errTypeInfoList.Add(new CListItem("7", "LOG_LEVEL_INFO"));
            errTypeInfoList.Add(new CListItem("8", "LOG_LEVEL_DEBUG"));
            errTypeInfoList.Add(new CListItem("9", "LOG_LEVEL_SQL"));

            repositoryItemLookUpEditStdType.DataSource = errTypeInfoList;
            #endregion

            gridView1.Columns["Gid"].Visible = false;
            gridView1.Columns["lstInfo"].Visible = false;
            gridView1.Columns["LogLevel"].ColumnEdit = repositoryItemLookUpEditStdType;
        }



        /// <summary>
        /// 校验加载的数据是否存在异常的
        /// </summary>
        private void ValidateData()
        {
            // 查询是否存在2个键值的数据
            List<ErrornoInfo> lstErrornoInfo = gridControl1.DataSource as List<ErrornoInfo>;

            // 查找重复的Name的值

            List<String> tmpName = new List<string>();
            foreach (ErrornoInfo errornoInfo in lstErrornoInfo)
            {
                if (string.IsNullOrEmpty(errornoInfo.Gid))
                    continue;

                if (lstName.Contains(errornoInfo.Name))
                {
                    tmpName.Add(errornoInfo.Name);
                }

                lstName.Add(errornoInfo.Name);
            }

            foreach (ErrornoInfo errornoInfo in lstErrornoInfo)
            {
                if (string.IsNullOrEmpty(errornoInfo.Gid))
                    continue;

                // 判断重复的 错误号
                if (tmpName.Contains(errornoInfo.Name))
                {
                    if (errornoInfo.lstInfo.ContainsKey("Name"))
                    {
                        errornoInfo.lstInfo["Name"].ErrorText = errornoInfo.lstInfo["Name"].ErrorText + "\r\n存在键值相同的错误号";
                        errornoInfo.lstInfo["Name"].ErrorType = errornoInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? errornoInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        errornoInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("存在键值相同的错误号", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("存在键值相同的错误号", "错误号" + errornoInfo.Name));
                    }
                }
                // 类型名判断大驼峰
                if (errornoInfo.Name.Length >= 1)
                {
                    if (!string.Equals(errornoInfo.Name[0].ToString(), errornoInfo.Name[0].ToString().ToUpper(), StringComparison.CurrentCulture))
                    {
                        if (errornoInfo.lstInfo.ContainsKey("Name"))
                        {
                            errornoInfo.lstInfo["Name"].ErrorText = errornoInfo.lstInfo["Name"].ErrorText + "\r\n错误号以大驼峰命名规范";
                            errornoInfo.lstInfo["Name"].ErrorType = errornoInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning ? errornoInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                        }
                        else
                        {
                            errornoInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("错误号以大驼峰命名称", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning));
                            _warnCount++;
                            // 20170901 wjm 调整key 和value的顺序
                            _warnlst.Add(new CListItem("错误号以大驼峰命名规范", "错误号" + errornoInfo.Name));
                        }
                    }
                }

                // 判断类型名是否为空
                if (string.IsNullOrEmpty(errornoInfo.Name))
                {
                    if (errornoInfo.lstInfo.ContainsKey("Name"))
                    {
                        errornoInfo.lstInfo["Name"].ErrorText = errornoInfo.lstInfo["Name"].ErrorText + "\r\n错误号不能为空";
                        errornoInfo.lstInfo["Name"].ErrorType = errornoInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? errornoInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        errornoInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("错误号不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("错误号不能为空", "错误号" + errornoInfo.Name));
                    }
                }

                // 错误提示说明
                if (string.IsNullOrEmpty(errornoInfo.ChineseName))
                {
                    if (errornoInfo.lstInfo.ContainsKey("ChineseName"))
                    {
                        errornoInfo.lstInfo["ChineseName"].ErrorText = errornoInfo.lstInfo["ChineseName"].ErrorText + "\r\n错误提示说明不能为空";
                        errornoInfo.lstInfo["ChineseName"].ErrorType = errornoInfo.lstInfo["ChineseName"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? errornoInfo.lstInfo["ChineseName"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        errornoInfo.lstInfo.Add("ChineseName", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("错误提示说明不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("错误提示说明不能为空", "错误提示说明" + errornoInfo.ChineseName));
                    }
                }

                // 错误提示说明
                /*if (string.IsNullOrEmpty(errornoInfo.LogLevel))
                {
                    if (errornoInfo.lstInfo.ContainsKey("LogLevel"))
                    {
                        errornoInfo.lstInfo["LogLevel"].ErrorText = errornoInfo.lstInfo["LogLevel"].ErrorText + "\r\n错误级别不能为空";
                        errornoInfo.lstInfo["LogLevel"].ErrorType = errornoInfo.lstInfo["LogLevel"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? errornoInfo.lstInfo["Value"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        errornoInfo.lstInfo.Add("LogLevel", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("错误级别不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("错误级别不能为空", "错误级别" + errornoInfo.Name));
                    }
                }*/
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
            List<ErrornoInfo> lstErrornoInfo = gridControl1.DataSource as List<ErrornoInfo>;

            foreach (ErrornoInfo errornoInfo in lstErrornoInfo)
            {
                if (string.IsNullOrEmpty(errornoInfo.Gid))
                    continue;

                errornoInfo.lstInfo.Clear();
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
            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype/item[@gid=\"" + tmperrornoInfo.Gid + "\"]");
            Int32 idx = -1;

            switch (e.Column.ToString())
            {
                case "名称":
                    idx = 0;
                    break;
                case "中文名称":
                    idx = 1;
                    break;
                case "日志级别":
                    idx = 2;
                    break;
                case "备注":
                    idx = 3;
                    break;
            }
            
           if (idx == -1)
                return;

            xmlNodeLst.Item(idx).InnerText = e.Value.ToString();
            xmlhelper.Save(false);

            tmperrornoInfo = null;
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty((gridView1.GetFocusedRow() as ErrornoInfo).Gid) && (gridView1.FocusedRowHandle + 1 == gridView1.RowCount))
            {
                btnAdd_Click(null, null);
            }

            tmperrornoInfo = gridView1.GetRow(e.RowHandle) as ErrornoInfo;
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

            var errornoInfo = new ErrornoInfo();
            errornoInfo.Gid = System.Guid.NewGuid().ToString();
            errornoInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();

            xmlhelper.InsertElement("datatype", "item", "gid", errornoInfo.Gid, string.Format(xmlModel, string.Empty, string.Empty, string.Empty, string.Empty));
            xmlhelper.Save(false);

            (gridView1.DataSource as List<ErrornoInfo>).Insert((gridView1.DataSource as List<ErrornoInfo>).Count - 1, errornoInfo);
            gridView1.RefreshData();
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsert_Click(object sender, EventArgs e)
        {
            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype/item[@gid=\"" + (gridView1.GetFocusedRow() as ErrornoInfo).Gid + "\"]");
            var errornoInfo = new ErrornoInfo();
            errornoInfo.Gid = System.Guid.NewGuid().ToString();
            errornoInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
            xmlhelper.InsertElement("item", "gid", errornoInfo.Gid, string.Format(xmlModel, string.Empty, string.Empty, string.Empty, string.Empty), xmlNodeLst.Item(0).ParentNode);
            xmlhelper.Save(false);

            (gridView1.DataSource as List<ErrornoInfo>).Insert(gridView1.FocusedRowHandle, errornoInfo);
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
            if (string.IsNullOrEmpty((gridView1.GetFocusedRow() as ErrornoInfo).Gid))
                return;

            xmlhelper.DeleteByPathNode("datatype/item[@gid=\"" + gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "Gid") + "\"]");
            xmlhelper.Save(false);

            // 20170924 wjm 删除lstName 对应的值保存导入的时候缓存问题
            lstName.Remove(gridView1.GetRowCellDisplayText(gridView1.FocusedRowHandle, "Name"));
            (gridView1.DataSource as List<ErrornoInfo>).RemoveAt(gridView1.FocusedRowHandle);
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

            ErrornoInfo cuerrornoInfo = gridView1.GetFocusedRow() as ErrornoInfo;
            ErrornoInfo preerrornoInfo = gridView1.GetRow(gridView1.FocusedRowHandle - 1) as ErrornoInfo;
            // 深拷贝
            ErrornoInfo tmperrornoInfo = new ErrornoInfo();
            tmperrornoInfo.Gid = cuerrornoInfo.Gid;
            tmperrornoInfo.Name = cuerrornoInfo.Name;
            tmperrornoInfo.ChineseName = cuerrornoInfo.ChineseName;
            tmperrornoInfo.LogLevel = cuerrornoInfo.LogLevel;
            tmperrornoInfo.Remark = cuerrornoInfo.Remark;
            tmperrornoInfo.lstInfo = cuerrornoInfo.lstInfo;

            // 更新内容
            cuerrornoInfo.Gid = preerrornoInfo.Gid;
            cuerrornoInfo.Name = preerrornoInfo.Name;
            cuerrornoInfo.ChineseName = preerrornoInfo.ChineseName;
            cuerrornoInfo.LogLevel = preerrornoInfo.LogLevel;
            cuerrornoInfo.Remark = preerrornoInfo.Remark;
            cuerrornoInfo.lstInfo = preerrornoInfo.lstInfo;

            preerrornoInfo.Gid = tmperrornoInfo.Gid;
            preerrornoInfo.Name = tmperrornoInfo.Name;
            preerrornoInfo.ChineseName = tmperrornoInfo.ChineseName;
            preerrornoInfo.LogLevel = tmperrornoInfo.LogLevel;
            preerrornoInfo.Remark = tmperrornoInfo.Remark;
            preerrornoInfo.lstInfo = tmperrornoInfo.lstInfo;

            // 更细XML内容
            string cuXMLStr = xmlhelper.ReadInnerXML("datatype/item[@gid=\"" + cuerrornoInfo.Gid + "\"]");
            string preXMLStr = xmlhelper.ReadInnerXML("datatype/item[@gid=\"" + preerrornoInfo.Gid + "\"]");
            xmlhelper.Replace("datatype/item[@gid=\"" + cuerrornoInfo.Gid + "\"]", preXMLStr);
            xmlhelper.Replace("datatype/item[@gid=\"" + preerrornoInfo.Gid + "\"]", cuXMLStr);
            // 更新 Gid
            var cuattribute = xmlhelper.Read("datatype/item[@gid=\"" + cuerrornoInfo.Gid + "\"]").Item(0).ParentNode.Attributes["gid"];
            var preattribute = xmlhelper.Read("datatype/item[@gid=\"" + preerrornoInfo.Gid + "\"]").Item(0).ParentNode.Attributes["gid"];
            cuattribute.Value = preerrornoInfo.Gid;
            preattribute.Value = cuerrornoInfo.Gid;
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

            ErrornoInfo cuerrornoInfo = gridView1.GetFocusedRow() as ErrornoInfo;
            ErrornoInfo nexterrornoInfo = gridView1.GetRow(gridView1.FocusedRowHandle + 1) as ErrornoInfo;

            // 深拷贝
            ErrornoInfo tmperrornoInfo = new ErrornoInfo();
            tmperrornoInfo.Gid = cuerrornoInfo.Gid;
            tmperrornoInfo.Name = cuerrornoInfo.Name;
            tmperrornoInfo.ChineseName = cuerrornoInfo.ChineseName;
            tmperrornoInfo.LogLevel = cuerrornoInfo.LogLevel;
            tmperrornoInfo.Remark = cuerrornoInfo.Remark;
            tmperrornoInfo.lstInfo = cuerrornoInfo.lstInfo;

            // 更新内容
            cuerrornoInfo.Gid = nexterrornoInfo.Gid;
            cuerrornoInfo.Name = nexterrornoInfo.Name;
            cuerrornoInfo.ChineseName = nexterrornoInfo.ChineseName;
            cuerrornoInfo.LogLevel = nexterrornoInfo.LogLevel;
            cuerrornoInfo.Remark = nexterrornoInfo.Remark;
            cuerrornoInfo.lstInfo = nexterrornoInfo.lstInfo;

            nexterrornoInfo.Gid = tmperrornoInfo.Gid;
            nexterrornoInfo.Name = tmperrornoInfo.Name;
            nexterrornoInfo.ChineseName = tmperrornoInfo.ChineseName;
            nexterrornoInfo.LogLevel = tmperrornoInfo.LogLevel;
            nexterrornoInfo.Remark = tmperrornoInfo.Remark;
            nexterrornoInfo.lstInfo = tmperrornoInfo.lstInfo;

            // 更细XML内容
            string cuXMLStr = xmlhelper.ReadInnerXML("datatype/item[@gid=\"" + cuerrornoInfo.Gid + "\"]");
            string preXMLStr = xmlhelper.ReadInnerXML("datatype/item[@gid=\"" + nexterrornoInfo.Gid + "\"]");
            xmlhelper.Replace("datatype/item[@gid=\"" + cuerrornoInfo.Gid + "\"]", preXMLStr);
            xmlhelper.Replace("datatype/item[@gid=\"" + nexterrornoInfo.Gid + "\"]", cuXMLStr);
            // 更新 Gid
            var cuattribute = xmlhelper.Read("datatype/item[@gid=\"" + cuerrornoInfo.Gid + "\"]").Item(0).ParentNode.Attributes["gid"];
            var nextattribute = xmlhelper.Read("datatype/item[@gid=\"" + nexterrornoInfo.Gid + "\"]").Item(0).ParentNode.Attributes["gid"];
            cuattribute.Value = nexterrornoInfo.Gid;
            nextattribute.Value = cuerrornoInfo.Gid;
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
            DataTable dt = DataTableHelper.CreateTable("错误号,错误提示信息,错误级别,说明");
            var lst = (gridView1.DataSource as List<ErrornoInfo>);
            for (int i = 0; i < lst.Count; i++)
            {
                DataRow row = dt.NewRow();
                row[0] = lst[i].Name;
                row[1] = lst[i].ChineseName;
                row[2] = IntToErrInfo(lst[i].LogLevel);
                row[3] = lst[i].Remark;
                dt.Rows.Add(row);
            }

            string saveFile = FileDialogHelper.SaveExcel("标准错误号.xls", "C:\\");
            if (!string.IsNullOrEmpty(saveFile))
            {
                MyXlsHelper.Export(dt, saveFile, "标准错误号", 1, 1);

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

                DataTable dt = MyXlsHelper.Import(importFile, "标准错误号", 2, 1);

                // 如果没有结果集就不在继续
                if (dt == null) return;

                Int32 addRows = 0;
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                { 
                    // 判断是否存在不存在则添加
                    if (!lstName.Contains(dt.Rows[i][0].ToString()))
                    {
                        var errornoInfo = new ErrornoInfo();
                        errornoInfo.Gid = System.Guid.NewGuid().ToString();
                        errornoInfo.Name = dt.Rows[i][0].ToString();
                        errornoInfo.ChineseName = dt.Rows[i][1].ToString();
                        errornoInfo.LogLevel = Convert.ToInt16(ErrInfoToInt(dt.Rows[i][2].ToString()));
                        errornoInfo.Remark = dt.Rows[i][3].ToString();

                        xmlhelper.InsertElement("datatype", "item", "gid", errornoInfo.Gid, string.Format(xmlModel, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString()));

                        (gridView1.DataSource as List<ErrornoInfo>).Insert((gridView1.DataSource as List<ErrornoInfo>).Count - 1, errornoInfo);
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

        private string IntToErrInfo(Int16 strerridx)
        {
            string result = string.Empty;
     
            // 1 - LOG_LEVEL_EMERG
            // 2 - LOG_LEVEL_ALERT
            // 3 - LOG_LEVEL_CRIT
            // 4 - LOG_LEVEL_ERR
            // 5 - LOG_LEVEL_WARN
            // 6 - LOG_LEVEL_NOTICE
            // 7 - LOG_LEVEL_INFO
            // 8 - LOG_LEVEL_DEBUG
            // 9 - LOG_LEVEL_SQL 
            switch (strerridx)
            {
                case 1:
                    result = "LOG_LEVEL_EMERG";
                    break;
                case 2:
                    result = "LOG_LEVEL_ALERT";
                    break;
                case 3:
                    result = "LOG_LEVEL_CRIT";
                    break;
                case 4:
                    result = "LOG_LEVEL_ERR";
                    break;
                case 5:
                    result = "LOG_LEVEL_WARN";
                    break;
                case 6:
                    result = "LOG_LEVEL_NOTICE";
                    break;
                case 7:
                    result = "LOG_LEVEL_INFO";
                    break;
                case 8:
                    result = "LOG_LEVEL_DEBUG";
                    break;
                case 9:
                    result = "LOG_LEVEL_SQL";
                    break;
            }
            return result;
        }

        private Int32 ErrInfoToInt(string errinfo)
        {
            Int32 result = 0;
            // 1 - LOG_LEVEL_EMERG
            // 2 - LOG_LEVEL_ALERT
            // 3 - LOG_LEVEL_CRIT
            // 4 - LOG_LEVEL_ERR
            // 5 - LOG_LEVEL_WARN
            // 6 - LOG_LEVEL_NOTICE
            // 7 - LOG_LEVEL_INFO
            // 8 - LOG_LEVEL_DEBUG
            // 9 - LOG_LEVEL_SQL 
            switch (errinfo)
            {
                case "LOG_LEVEL_EMERG":
                    result = 1;
                    break;
                case "LOG_LEVEL_ALERT":
                    result = 2;
                    break;
                case "LOG_LEVEL_CRIT":
                    result = 3;
                    break;
                case "LOG_LEVEL_ERR":
                    result = 4;
                    break;
                case "LOG_LEVEL_WARN":
                    result = 5;
                    break;
                case "LOG_LEVEL_NOTICE":
                    result = 6;
                    break;
                case "LOG_LEVEL_INFO":
                    result = 7;
                    break;
                case "LOG_LEVEL_DEBUG":
                    result = 8;
                    break;
                case "LOG_LEVEL_SQL":
                    result = 9;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 复制错误信息到剪贴板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmirealoadcache_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            /*
             * result.ErrorCode = 000000;           // ErrorCode 表示登陆成功
               result.LogLevel = 1;
               result.ErrorMessage = "操作成功";
               result.Data1 = "普通管理员";
             */
            if (gridView1.RowCount > 0 && gridView1.FocusedRowHandle >= 0)
            {
                ErrornoInfo errornoInfo = gridView1.GetFocusedRow() as ErrornoInfo;
                if (null != errornoInfo)
                {
                    sb.Append("// 系统自动生成错误信息\r\n");
                    sb.Append(string.Format("result.ErrorCode = {0};\r\n", errornoInfo.Name));
                    sb.Append(string.Format("result.LogLevel = {0};\r\n", errornoInfo.LogLevel));
                    sb.Append(string.Format("result.ErrorMessage = \"{0}[{1}]\";\r\n", errornoInfo.Remark, errornoInfo.ChineseName));
                }
            }

            Clipboard.SetDataObject(sb.ToString());
        }
    }
}
