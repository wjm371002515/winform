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
using JCodes.Framework.Common;
using DevExpress.XtraTreeList;

// 参考文档 http://www.cnblogs.com/a1656344531/archive/2012/11/28/2792863.html

namespace JCodes.Framework.AddIn.Proj
{
    /// <summary>
    /// 登陆系统的黑白名单列表
    /// </summary>	
    public partial class FrmSysFunction : BaseDock
    {
        private XmlHelper xmlhelper = new XmlHelper(@"XML\function.xml");

        private Dictionary<string, string> lstName = new Dictionary<string, string>();

        private string xmlModel = "<gid>{0}</gid><pgid>{1}</pgid><name>{2}</name><functiongid>{3}</functiongid><systemtypeid>{4}</systemtypeid><seq>{5}</seq>";

        private Int32 _errCount = 0;
        private List<CListItem> _errlst = new List<CListItem>();
        private Int32 _warnCount = 0;
        private List<CListItem> _warnlst = new List<CListItem>();
        private Int32 _infoCount = 0;
        private List<CListItem> _infolst = new List<CListItem>();

        public FrmSysFunction()
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
            FilterNodeOperation operation = new FilterNodeOperation(txtSearch.Text);
            treelstFunction.NodesIterator.DoOperation(operation);  
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype");
            List<SysFunctionInfo> functionInfoList = new List<SysFunctionInfo>();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                SysFunctionInfo functionInfo = new SysFunctionInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
              
                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                functionInfo.Gid = xnl0.Item(0).InnerText;
                functionInfo.Pgid = xnl0.Item(1).InnerText;
                functionInfo.Name = xnl0.Item(2).InnerText;
                functionInfo.FunctionGid = xnl0.Item(3).InnerText;
                functionInfo.SystemtypeId = xnl0.Item(4).InnerText;
                functionInfo.Seq = xnl0.Item(5).InnerText;
                functionInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();

                functionInfoList.Add(functionInfo);
            }

            treelstFunction.KeyFieldName = "Gid";
            treelstFunction.ParentFieldName = "Pgid";
            treelstFunction.DataSource = functionInfoList;

            treelstFunction.Columns["lstInfo"].Visible = false;

            treelstFunction.OptionsBehavior.DragNodes = true;

            treelstFunction.ForceInitialize();
            treelstFunction.BestFitColumns();
        }

        /// <summary>
        /// 校验加载的数据是否存在异常的
        /// </summary>
        private void ValidateData()
        {
            // 查询是否存在2个键值的数据
            List<SysFunctionInfo> lstSysFunctionInfo = treelstFunction.DataSource as List<SysFunctionInfo>;

            // 查找重复的Name的值
            Dictionary<string, string> tmpName = new Dictionary<string, string>();
            foreach (SysFunctionInfo sysFunctionInfo in lstSysFunctionInfo)
            {
                if (lstName.ContainsKey(sysFunctionInfo.Name) && lstName[sysFunctionInfo.Name] == sysFunctionInfo.Pgid)
                {
                    if (!tmpName.ContainsKey(sysFunctionInfo.Name))
                        tmpName.Add(sysFunctionInfo.Name, sysFunctionInfo.Pgid);
                }
                else
                {
                    if (!lstName.ContainsKey(sysFunctionInfo.Name))
                        lstName.Add(sysFunctionInfo.Name, sysFunctionInfo.Pgid);
                }
            }

            foreach (SysFunctionInfo sysFunctionInfo in lstSysFunctionInfo)
            {
                // 判断重复的 类型名
                if (tmpName.ContainsKey(sysFunctionInfo.Name) && (tmpName[sysFunctionInfo.Name] == sysFunctionInfo.Pgid))
                {
                    if (sysFunctionInfo.lstInfo.ContainsKey("Name"))
                    {
                        sysFunctionInfo.lstInfo["Name"].ErrorText = sysFunctionInfo.lstInfo["Name"].ErrorText + "\r\n存在键值相同的显示名称";
                        sysFunctionInfo.lstInfo["Name"].ErrorType = sysFunctionInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? sysFunctionInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        sysFunctionInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("存在键值相同的显示名称", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("存在键值相同的显示名称", "显示名称" + sysFunctionInfo.Name));
                    }
                }

                // 判断显示名称是否为空
                if (string.IsNullOrEmpty(sysFunctionInfo.Name))
                {
                    if (sysFunctionInfo.lstInfo.ContainsKey("Name"))
                    {
                        sysFunctionInfo.lstInfo["Name"].ErrorText = sysFunctionInfo.lstInfo["Name"].ErrorText + "\r\n显示名称不能为空";
                        sysFunctionInfo.lstInfo["Name"].ErrorType = sysFunctionInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? sysFunctionInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        sysFunctionInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("显示名称不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("显示名称不能为空", "显示名称" + sysFunctionInfo.Name));
                    }
                }
                // 判断排序是否为空
                if (string.IsNullOrEmpty(sysFunctionInfo.Seq))
                {
                    if (sysFunctionInfo.lstInfo.ContainsKey("Seq"))
                    {
                        sysFunctionInfo.lstInfo["Seq"].ErrorText = sysFunctionInfo.lstInfo["Seq"].ErrorText + "\r\n排序不能为空";
                        sysFunctionInfo.lstInfo["Seq"].ErrorType = sysFunctionInfo.lstInfo["Seq"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? sysFunctionInfo.lstInfo["Seq"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        sysFunctionInfo.lstInfo.Add("Seq", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("排序不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("排序不能为空", "排序" + sysFunctionInfo.Name));
                    }
                }
                // 判断功能ID是否为空
                if (string.IsNullOrEmpty(sysFunctionInfo.FunctionGid))
                {
                    if (sysFunctionInfo.lstInfo.ContainsKey("FunctionGid"))
                    {
                        sysFunctionInfo.lstInfo["FunctionGid"].ErrorText = sysFunctionInfo.lstInfo["FunctionGid"].ErrorText + "\r\n功能ID不能为空";
                        sysFunctionInfo.lstInfo["FunctionGid"].ErrorType = sysFunctionInfo.lstInfo["FunctionGid"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? sysFunctionInfo.lstInfo["FunctionGid"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        sysFunctionInfo.lstInfo.Add("FunctionGid", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("功能ID不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("功能ID不能为空", "功能ID" + sysFunctionInfo.Name));
                    }
                }
                // 判断系统编号是否为空
                if (string.IsNullOrEmpty(sysFunctionInfo.SystemtypeId))
                {
                    if (sysFunctionInfo.lstInfo.ContainsKey("SystemtypeId"))
                    {
                        sysFunctionInfo.lstInfo["SystemtypeId"].ErrorText = sysFunctionInfo.lstInfo["SystemtypeId"].ErrorText + "\r\n系统编号不能为空";
                        sysFunctionInfo.lstInfo["SystemtypeId"].ErrorType = sysFunctionInfo.lstInfo["SystemtypeId"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? sysFunctionInfo.lstInfo["SystemtypeId"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        sysFunctionInfo.lstInfo.Add("SystemtypeId", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("系统编号不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("系统编号不能为空", "系统编号" + sysFunctionInfo.SystemtypeId));
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
            List<SysFunctionInfo> lstSysFunctionInfo = treelstFunction.DataSource as List<SysFunctionInfo>;

            foreach (SysFunctionInfo sysFunctionInfo in lstSysFunctionInfo)
            {
                if (sysFunctionInfo.lstInfo == null)
                    sysFunctionInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
                else
                    sysFunctionInfo.lstInfo.Clear();
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

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (treelstFunction.FocusedNode != null)
            {
                string delID = treelstFunction.FocusedNode.GetValue("Gid").ToString();
                deleteNodeXML(treelstFunction.FocusedNode);
                // 删除节点
                xmlhelper = new XmlHelper(@"XML\function.xml");
                
                xmlhelper.DeleteByPathNode("datatype/item[gid=\"" + delID + "\"]");
                xmlhelper.Save(false);

                treelstFunction.DeleteNode(treelstFunction.FocusedNode);
            }
        }

        private bool deleteNodeXML(DevExpress.XtraTreeList.Nodes.TreeListNode nodes)
        {
            try
            {
                bool result = true;

                // 先查找子节点
                if (nodes.HasChildren) {
                    var childNodes = nodes.Nodes;
                    for (Int32 i = 0; i < childNodes.Count; i++)
                    {
                        string delID = childNodes[i].GetValue("Gid").ToString();

                        // 递归
                        result = result & deleteNodeXML(childNodes[i]);

                        // 删除节点
                        xmlhelper = new XmlHelper(@"XML\function.xml");
                        xmlhelper.DeleteByPathNode("datatype/item[gid=\"" + delID + "\"]");
                        xmlhelper.Save(false);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageDxUtil.ShowError(ex.Message);
                return false;
            }
        }

        private void ErgodicNode(DevExpress.XtraTreeList.Nodes.TreeListNode nodes, string preStr, DataTable dt)
        {
            if (nodes.Nodes.Count != Const.Num_Zero)
            {
                for (Int32 i = 0; i < nodes.Nodes.Count; i++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = preStr + nodes.Nodes[i].GetValue("Name");
                    row[1] = nodes.Nodes[i].GetValue("FunctionGid");
                    row[2] = nodes.Nodes[i].GetValue("SystemtypeId");
                    row[3] = nodes.Nodes[i].GetValue("Seq");

                    dt.Rows.Add(row);

                    //Console.WriteLine(preStr + nodes.Nodes[i].GetValue("Name"));
                    if (nodes.Nodes[i].HasChildren)
                    {
                        preStr += "﹂";
                        ErgodicNode(nodes.Nodes[i], preStr, dt);

                        if (preStr.Length > 0)
                            preStr = preStr.Substring(0, preStr.Length - 1);
                    }
                }
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = DataTableHelper.CreateTable("显示名称,功能ID,系统编号,排序");

            if (treelstFunction.Nodes.Count != Const.Num_Zero)
            {
                for (Int32 i = 0; i < treelstFunction.Nodes.Count; i++)
                {
                    DataRow row = dt.NewRow();

                    row[0] = "" + treelstFunction.Nodes[i].GetValue("Name");
                    row[1] = treelstFunction.Nodes[i].GetValue("FunctionGid");
                    row[2] = treelstFunction.Nodes[i].GetValue("SystemtypeId");
                    row[3] = treelstFunction.Nodes[i].GetValue("Seq");
                    dt.Rows.Add(row);

                    //Console.WriteLine("" + treelstMenu.Nodes[i].GetValue("Name"));
                    if (treelstFunction.Nodes[i].HasChildren)
                    {
                        ErgodicNode(treelstFunction.Nodes[i], "﹂", dt);
                    }
                }
            }

            string saveFile = FileDialogHelper.SaveExcel("系统功能.xls", "C:\\");
            if (!string.IsNullOrEmpty(saveFile))
            {
                MyXlsHelper.Export(dt, saveFile, "系统功能", 1, 1);

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
            if (treelstFunction.Nodes.Count != Const.Num_Zero)
            {
                if (MessageDxUtil.ShowYesNoAndTips("系统功能有原始数据，此次导入会清空原始数据，是否继续？") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            string importFile = FileDialogHelper.OpenExcel(false);
            if (!string.IsNullOrEmpty(importFile))
            {
                // 判断文件是否被占用
                if (FileUtil.FileIsUsing(importFile))
                {
                    MessageDxUtil.ShowWarning(string.Format("文件[{0}]被占用，请先关闭文件后再重试!", importFile));
                    return;
                }

                DataTable dt = MyXlsHelper.Import(importFile, "系统功能", 2, 1);
                List<SysFunctionInfo> lstsysFunctionInfo = new List<SysFunctionInfo>();

                // 如果没有结果集就不在继续
                if (dt == null) return;

                Int32 addRows = 0;
                List<String> pushFunction = new List<string>();
                pushFunction.Add("-1");

                // 先清除全部节点
                Int32 rowCount = xmlhelper.Read("datatype").Count;
                for (Int32 i = 0; i < rowCount; i++)
                {
                    xmlhelper.DeleteByPathNode("datatype/item");
                    xmlhelper.Save(false);
                }

                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    var sysFunctionInfo = new SysFunctionInfo();
                    sysFunctionInfo.Gid = Guid.NewGuid().ToString();
                    sysFunctionInfo.Pgid = pushFunction.Last<string>();
                    sysFunctionInfo.Name = dt.Rows[i][0].ToString().TrimStart('﹂');
                    sysFunctionInfo.FunctionGid = dt.Rows[i][1].ToString();
                    sysFunctionInfo.SystemtypeId = dt.Rows[i][2].ToString();
                    sysFunctionInfo.Seq = dt.Rows[i][3].ToString();
                    sysFunctionInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();

                    if ((i + 1) < dt.Rows.Count && dt.Rows[i][0].ToString().LastIndexOf("﹂") < dt.Rows[i + 1][0].ToString().LastIndexOf("﹂"))
                    {
                        pushFunction.Add(sysFunctionInfo.Gid);
                    }

                    // 返回到了某个父节点
                    if ((i + 1) < dt.Rows.Count && dt.Rows[i][0].ToString().LastIndexOf("﹂") > dt.Rows[i + 1][0].ToString().LastIndexOf("﹂"))
                    { 
                        // 需要多次弹出操作
                        for (Int32 j = 0; j < (dt.Rows[i][0].ToString().LastIndexOf("﹂") - dt.Rows[i+1][0].ToString().LastIndexOf("﹂")); j++)
                        {
                            pushFunction.RemoveAt(pushFunction.Count - 1);
                        }
                    }

                    addRows++;

                    lstsysFunctionInfo.Add(sysFunctionInfo);
                    xmlhelper.InsertElement("datatype", "item", string.Format(xmlModel, sysFunctionInfo.Gid, sysFunctionInfo.Pgid, sysFunctionInfo.Name, sysFunctionInfo.FunctionGid, sysFunctionInfo.SystemtypeId, sysFunctionInfo.Seq));
                    xmlhelper.Save(false);
                    
                }

                treelstFunction.DataSource = lstsysFunctionInfo;
                treelstFunction.Refresh();
                ClearValidate();
                treelstFunction.ForceInitialize();
                treelstFunction.BestFitColumns();

                MessageDxUtil.ShowTips(string.Format("成功导入功能数据{0}条数据", addRows));
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
        
        /// <summary>
        /// 添加同级节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRoot_Click(object sender, EventArgs e)
        {
            SysFunctionInfo functionInfo = new SysFunctionInfo();
            if (string.Equals(treelstFunction.FocusedNode.GetValue("Pgid").ToString(), Const.Num_MinusOne.ToString()))
            {
                functionInfo.Gid = Guid.NewGuid().ToString();
                functionInfo.Pgid = Const.Num_MinusOne.ToString();
                functionInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
                treelstFunction.FocusedNode = treelstFunction.AppendNode(functionInfo, null);
                treelstFunction.FocusedNode.SetValue("Gid", functionInfo.Gid);
                treelstFunction.FocusedNode.SetValue("Pgid", functionInfo.Pgid);
                treelstFunction.FocusedNode.SetValue("lstInfo", functionInfo.lstInfo);
            }
            else
            {
                functionInfo.Gid = Guid.NewGuid().ToString();
                functionInfo.Pgid = treelstFunction.FocusedNode.GetValue("Pgid").ToString();
                functionInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
                treelstFunction.FocusedNode = treelstFunction.AppendNode(functionInfo, treelstFunction.FocusedNode.ParentNode);
                treelstFunction.FocusedNode.SetValue("Gid", functionInfo.Gid);
                treelstFunction.FocusedNode.SetValue("Pgid", functionInfo.Pgid);
                treelstFunction.FocusedNode.SetValue("lstInfo", functionInfo.lstInfo);
            }
            xmlhelper.InsertElement("datatype", "item", string.Format(xmlModel, functionInfo.Gid, functionInfo.Pgid, functionInfo.Name, functionInfo.FunctionGid, functionInfo.SystemtypeId, functionInfo.Seq));

            xmlhelper.Save(false);
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnAddNodes_Click(object sender, EventArgs e)
        {
            if (treelstFunction.FocusedNode != null)
            {
                SysFunctionInfo functionInfo = new SysFunctionInfo();
                functionInfo.Gid = Guid.NewGuid().ToString();
                functionInfo.Pgid = treelstFunction.FocusedNode.GetValue("Gid").ToString();
                functionInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
                treelstFunction.FocusedNode = treelstFunction.AppendNode(null, treelstFunction.FocusedNode);
                treelstFunction.FocusedNode.SetValue("Gid", functionInfo.Gid);
                treelstFunction.FocusedNode.SetValue("Pgid", functionInfo.Pgid);

                xmlhelper.InsertElement("datatype", "item", string.Format(xmlModel, functionInfo.Gid, functionInfo.Pgid, functionInfo.Name, functionInfo.FunctionGid, functionInfo.SystemtypeId, functionInfo.Seq));

                xmlhelper.Save(false);
            }
        }

        /// <summary>
        /// 功能的拖拽功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treelstFunction_DragDrop(object sender, DragEventArgs e)
        {
            DXDragEventArgs args = treelstFunction.GetDXDragEventArgs(e);
            if (args.DragInsertPosition == DragInsertPosition.After)
            {
                // 放到之后去则与TargetNode 的节点的父节点一致
                //Console.WriteLine(args.TargetNode.GetValue("PID"));
                string dragId = args.Node.GetValue("Gid").ToString();
                xmlhelper = new XmlHelper(@"XML\function.xml");
                var xmlNodes = xmlhelper.Read("datatype");
                for (Int32 i = 0; i < xmlNodes.Count; i++)
                {
                    if (string.Equals(xmlNodes[i].ChildNodes[0].InnerText, dragId))
                    {
                        xmlNodes[i].ChildNodes[1].InnerText = args.TargetNode.GetValue("Pgid").ToString();
                        break;
                    }
                }
            }
            else if(args.DragInsertPosition == DragInsertPosition.AsChild)
            {
                // 放到目标节点之中，则父节点就是TargetNode;
                //Console.WriteLine(args.TargetNode.GetValue("ID"));
                string dragId = args.Node.GetValue("Gid").ToString();
                xmlhelper = new XmlHelper(@"XML\function.xml");
                var xmlNodes = xmlhelper.Read("datatype");
                for (Int32 i = 0; i < xmlNodes.Count; i++)
                {
                    if (string.Equals(xmlNodes[i].ChildNodes[0].InnerText, dragId))
                    {
                        xmlNodes[i].ChildNodes[1].InnerText = args.TargetNode.GetValue("Gid").ToString();
                        break;
                    }
                }
            }
            xmlhelper.Save(false);
        }

        /// <summary>
        /// 编辑菜单内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treelstFunction_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            string changId = treelstFunction.FocusedNode.GetValue("Gid").ToString();
            xmlhelper = new XmlHelper(@"XML\function.xml");
            var xmlNodes = xmlhelper.Read("datatype");
            for (Int32 i = 0; i < xmlNodes.Count; i++)
            {
                if (string.Equals(xmlNodes[i].ChildNodes[0].InnerText, changId))
                {
                    Int32 idx = -1;
                    switch (treelstFunction.FocusedColumn.FieldName)
                    {
                        case "Name":
                            idx = 2;
                            break;
                        case "FunctionGid":
                            idx = 3;
                            break;
                        case "SystemtypeId":
                            idx = 4;
                            break;
                        case "Seq":
                            idx = 5;
                            break;
                    }

                    if (idx == -1)
                    {
                        break;
                    }

                    xmlNodes[i].ChildNodes[idx].InnerText = treelstFunction.FocusedNode.GetValue(treelstFunction.FocusedColumn.FieldName).ToString();

                    xmlhelper.Save(false);
                }
            }
            
        }

        /// <summary>
        /// 添加完成之后需要验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treelstFunction_ValidateNode(object sender, ValidateNodeEventArgs e)
        {
            ClearValidate();

            ValidateData();

            InitView();
        }
    }
}
