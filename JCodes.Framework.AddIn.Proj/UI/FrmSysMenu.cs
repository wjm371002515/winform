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
using JCodes.Framework.Common.Extension;

// 参考文档 http://www.cnblogs.com/a1656344531/archive/2012/11/28/2792863.html

namespace JCodes.Framework.AddIn.Proj
{
    /// <summary>
    /// 登陆系统的黑白名单列表
    /// </summary>	
    public partial class FrmSysMenu : BaseDock
    {
        private XmlHelper xmlhelper = new XmlHelper(@"XML\menu.xml");

        private Dictionary<string, string> lstName = new Dictionary<string, string>();

        private string xmlModel = "<gid>{0}</gid><pgid>{1}</pgid><name>{2}</name><icon>{3}</icon><seq>{4}</seq><authgid>{5}</authgid><isvisable>{6}</isvisable><winformclass>{7}</winformclass><url>{8}</url><webicon>{9}</webicon><systemtypeid>{10}</systemtypeid><creatorid>{11}</creatorid><creatortime>{12}</creatortime><editorid>{13}</editorid><lastupdatetime>{14}</lastupdatetime><isdelete>{15}</isdelete>";

        private Int32 _errCount = 0;
        private List<CListItem> _errlst = new List<CListItem>();
        private Int32 _warnCount = 0;
        private List<CListItem> _warnlst = new List<CListItem>();
        private Int32 _infoCount = 0;
        private List<CListItem> _infolst = new List<CListItem>();

        public FrmSysMenu()
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
            treelstMenu.NodesIterator.DoOperation(operation);  
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype/dataitem");
            List<SysMenuInfo> menuInfoList = new List<SysMenuInfo>();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                SysMenuInfo menuInfo = new SysMenuInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
              
                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                menuInfo.Gid = xnl0.Item(0).InnerText;
                menuInfo.Pgid = xnl0.Item(1).InnerText;
                menuInfo.Name = xnl0.Item(2).InnerText;
                menuInfo.Icon = xnl0.Item(3).InnerText;
                menuInfo.Seq = xnl0.Item(4).InnerText;
                menuInfo.AuthGid = xnl0.Item(5).InnerText;
                menuInfo.IsVisable = (short)(xnl0.Item(6).InnerText == Const.Num_One.ToString() ? 1 : 0);
                menuInfo.WinformClass = xnl0.Item(7).InnerText;
                menuInfo.Url = xnl0.Item(8).InnerText;
                menuInfo.WebIcon = xnl0.Item(9).InnerText;
                menuInfo.SystemtypeId = xnl0.Item(10).InnerText;
                menuInfo.CreatorId =  string.IsNullOrEmpty(xnl0.Item(11).InnerText) ? 0 : xnl0.Item(11).InnerText.ToInt32();
                menuInfo.CreatorTime = string.IsNullOrEmpty(xnl0.Item(12).InnerText) ? DateTimeHelper.GetServerDateTime2() : Convert.ToDateTime( xnl0.Item(12).InnerText);
                menuInfo.EditorId = string.IsNullOrEmpty(xnl0.Item(13).InnerText) ? 0 : xnl0.Item(13).InnerText.ToInt32();
                menuInfo.LastUpdateTime = string.IsNullOrEmpty(xnl0.Item(14).InnerText) ? DateTimeHelper.GetServerDateTime2() : Convert.ToDateTime(xnl0.Item(14).InnerText);
                menuInfo.IsDelete = (short)(xnl0.Item(15).InnerText == Const.Num_One.ToString() ? 1 : 0);
                menuInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();

                menuInfoList.Add(menuInfo);
            }

            treelstMenu.KeyFieldName = "Gid";
            treelstMenu.ParentFieldName = "Pgid";
            treelstMenu.DataSource = menuInfoList;

            treelstMenu.Columns["lstInfo"].Visible = false;

            treelstMenu.OptionsBehavior.DragNodes = true;

            treelstMenu.ForceInitialize();
            treelstMenu.BestFitColumns();
        }

        /// <summary>
        /// 校验加载的数据是否存在异常的
        /// </summary>
        private void ValidateData()
        {
            // 查询是否存在2个键值的数据
            List<SysMenuInfo> lstSysMenuInfo = treelstMenu.DataSource as List<SysMenuInfo>;

            // 查找重复的Name的值
            Dictionary<string,string> tmpName = new Dictionary<string,string>();
            foreach (SysMenuInfo sysMenuInfo in lstSysMenuInfo)
            {
                if (lstName.ContainsKey(sysMenuInfo.Name) && lstName[sysMenuInfo.Name] == sysMenuInfo.Pgid)
                {
                    if (!tmpName.ContainsKey(sysMenuInfo.Name))
                        tmpName.Add(sysMenuInfo.Name, sysMenuInfo.Pgid);
                }
                else
                {
                    if (!lstName.ContainsKey(sysMenuInfo.Name))
                        lstName.Add(sysMenuInfo.Name, sysMenuInfo.Pgid);
                }
            }

            foreach (SysMenuInfo sysMenuInfo in lstSysMenuInfo)
            {
                // 判断重复的 类型名
                if (tmpName.ContainsKey(sysMenuInfo.Name) && (tmpName[sysMenuInfo.Name] == sysMenuInfo.Pgid))
                {
                    if (sysMenuInfo.lstInfo.ContainsKey("Name"))
                    {
                        sysMenuInfo.lstInfo["Name"].ErrorText = sysMenuInfo.lstInfo["Name"].ErrorText + "\r\n存在键值相同的显示名称";
                        sysMenuInfo.lstInfo["Name"].ErrorType = sysMenuInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? sysMenuInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        sysMenuInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("存在键值相同的显示名称", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("存在键值相同的显示名称", "显示名称" + sysMenuInfo.Name));
                    }
                }

                // 判断显示名称是否为空
                if (string.IsNullOrEmpty(sysMenuInfo.Name))
                {
                    if (sysMenuInfo.lstInfo.ContainsKey("Name"))
                    {
                        sysMenuInfo.lstInfo["Name"].ErrorText = sysMenuInfo.lstInfo["Name"].ErrorText + "\r\n显示名称不能为空";
                        sysMenuInfo.lstInfo["Name"].ErrorType = sysMenuInfo.lstInfo["Name"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? sysMenuInfo.lstInfo["Name"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        sysMenuInfo.lstInfo.Add("Name", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("显示名称不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("显示名称不能为空", "显示名称" + sysMenuInfo.Name));
                    }
                }
                // 判断排序是否为空
                if (string.IsNullOrEmpty(sysMenuInfo.Seq))
                {
                    if (sysMenuInfo.lstInfo.ContainsKey("Seq"))
                    {
                        sysMenuInfo.lstInfo["Seq"].ErrorText = sysMenuInfo.lstInfo["Seq"].ErrorText + "\r\n排序不能为空";
                        sysMenuInfo.lstInfo["Seq"].ErrorType = sysMenuInfo.lstInfo["Seq"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? sysMenuInfo.lstInfo["Seq"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        sysMenuInfo.lstInfo.Add("Seq", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("排序不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("排序不能为空", "排序" + sysMenuInfo.Name));
                    }
                }
                // 判断功能ID是否为空
                if (string.IsNullOrEmpty(sysMenuInfo.AuthGid))
                {
                    if (sysMenuInfo.lstInfo.ContainsKey("AuthGid"))
                    {
                        sysMenuInfo.lstInfo["AuthGid"].ErrorText = sysMenuInfo.lstInfo["AuthGid"].ErrorText + "\r\n功能ID不能为空";
                        sysMenuInfo.lstInfo["AuthGid"].ErrorType = sysMenuInfo.lstInfo["AuthGid"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? sysMenuInfo.lstInfo["AuthGid"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        sysMenuInfo.lstInfo.Add("AuthGid", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("功能ID不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("功能ID不能为空", "功能ID" + sysMenuInfo.Name));
                    }
                }
                // 判断系统编号是否为空
                if (string.IsNullOrEmpty(sysMenuInfo.AuthGid))
                {
                    if (sysMenuInfo.lstInfo.ContainsKey("SystemtypeId"))
                    {
                        sysMenuInfo.lstInfo["SystemtypeId"].ErrorText = sysMenuInfo.lstInfo["SystemtypeId"].ErrorText + "\r\n系统编号不能为空";
                        sysMenuInfo.lstInfo["SystemtypeId"].ErrorType = sysMenuInfo.lstInfo["SystemtypeId"].ErrorType >= DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical ? sysMenuInfo.lstInfo["SystemtypeId"].ErrorType : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    }
                    else
                    {
                        sysMenuInfo.lstInfo.Add("SystemtypeId", new DevExpress.XtraEditors.DXErrorProvider.ErrorInfo("系统编号不能为空", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical));
                        _errCount++;
                        // 20170901 wjm 调整key 和value的顺序
                        _errlst.Add(new CListItem("系统编号不能为空", "系统编号" + sysMenuInfo.AuthGid));
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
            List<SysMenuInfo> lstSysMenuInfo = treelstMenu.DataSource as List<SysMenuInfo>;

            foreach (SysMenuInfo sysMenuInfo in lstSysMenuInfo)
            {
                if (sysMenuInfo.lstInfo == null)
                    sysMenuInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
                else
                    sysMenuInfo.lstInfo.Clear();
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
            if (treelstMenu.FocusedNode != null)
            {
                string delID = treelstMenu.FocusedNode.GetValue("Gid").ToString();
                deleteNodeXML(treelstMenu.FocusedNode);
                // 删除节点
                xmlhelper = new XmlHelper(@"XML\menu.xml");

                xmlhelper.DeleteByPathNode("datatype/dataitem/item[id=\"" + delID + "\"]");
                xmlhelper.Save(false);

                treelstMenu.DeleteNode(treelstMenu.FocusedNode);
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
                        xmlhelper = new XmlHelper(@"XML\menu.xml");
                        xmlhelper.DeleteByPathNode("datatype/dataitem/item[id=\"" + delID + "\"]");
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
                    row[1] = nodes.Nodes[i].GetValue("Icon");
                    row[2] = nodes.Nodes[i].GetValue("Seq");
                    row[3] = nodes.Nodes[i].GetValue("AuthGid");
                    row[4] = Convert.ToBoolean(nodes.Nodes[i].GetValue("IsVisable")) ? "是" : "否"; ;
                    row[5] = nodes.Nodes[i].GetValue("WinformClass");
                    row[6] = nodes.Nodes[i].GetValue("Url");
                    row[7] = nodes.Nodes[i].GetValue("WebIcon");
                    row[8] = nodes.Nodes[i].GetValue("SystemtypeId");
                    row[9] = nodes.Nodes[i].GetValue("CreatorId");
                    row[10] = nodes.Nodes[i].GetValue("CreateTime");
                    row[11] = nodes.Nodes[i].GetValue("EditorId");
                    row[12] = nodes.Nodes[i].GetValue("LastUpdateTime");
                    row[13] = Convert.ToBoolean(nodes.Nodes[i].GetValue("IsDelete")) ? "是" : "否";

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
            DataTable dt = DataTableHelper.CreateTable("显示名称,图标,排序,功能ID,可见,Winform窗体类型,Web界面Url地址,Web界面的菜单图标,系统编号,创建人ID,创建时间,编辑人ID,编辑时间,已删除");

            if (treelstMenu.Nodes.Count != Const.Num_Zero)
            {
                for (Int32 i = 0; i < treelstMenu.Nodes.Count; i++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "" + treelstMenu.Nodes[i].GetValue("Name");
                    row[1] = treelstMenu.Nodes[i].GetValue("Icon");
                    row[2] = treelstMenu.Nodes[i].GetValue("Seq");
                    row[3] = treelstMenu.Nodes[i].GetValue("AuthGi");
                    row[4] = Convert.ToBoolean(treelstMenu.Nodes[i].GetValue("IsVisable")) ? "是" : "否";
                    row[5] = treelstMenu.Nodes[i].GetValue("WinformClass");
                    row[6] = treelstMenu.Nodes[i].GetValue("Url");
                    row[7] = treelstMenu.Nodes[i].GetValue("WebIcon");
                    row[8] = treelstMenu.Nodes[i].GetValue("SystemtypeId");
                    row[9] = treelstMenu.Nodes[i].GetValue("CreatorId");
                    row[10] = treelstMenu.Nodes[i].GetValue("CreateTime");
                    row[11] = treelstMenu.Nodes[i].GetValue("EditorId");
                    row[12] = treelstMenu.Nodes[i].GetValue("LastUpdateTime");
                    row[13] = Convert.ToBoolean(treelstMenu.Nodes[i].GetValue("IsDelete")) ? "是" : "否";

                    dt.Rows.Add(row);

                    //Console.WriteLine("" + treelstMenu.Nodes[i].GetValue("Name"));
                    if (treelstMenu.Nodes[i].HasChildren)
                    {
                        ErgodicNode(treelstMenu.Nodes[i], "﹂", dt);
                    }
                }
            }

            string saveFile = FileDialogHelper.SaveExcel("系统菜单.xls", "C:\\");
            if (!string.IsNullOrEmpty(saveFile))
            {
                MyXlsHelper.Export(dt, saveFile, "系统菜单", 1, 1);

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
            if (treelstMenu.Nodes.Count != Const.Num_Zero)
            {
                if (MessageDxUtil.ShowYesNoAndTips("系统菜单有原始数据，此次导入会清空原始数据，是否继续？") == System.Windows.Forms.DialogResult.No)
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

                DataTable dt = MyXlsHelper.Import(importFile, "系统菜单", 2, 1);
                List<SysMenuInfo> lstsysMenuInfo = new List<SysMenuInfo>();

                // 如果没有结果集就不在继续
                if (dt == null) return;

                Int32 addRows = 0;
                List<String> pushMenu = new List<string>();
                pushMenu.Add("-1");

                // 先清除全部节点
                Int32 rowCount = xmlhelper.Read("datatype/dataitem").Count;
                for (Int32 i = 0; i < rowCount; i++)
                {
                    xmlhelper.DeleteByPathNode("datatype/dataitem/item");
                    xmlhelper.Save(false);
                }

                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    var sysMenuInfo = new SysMenuInfo();
                    sysMenuInfo.Gid = Guid.NewGuid().ToString();
                    sysMenuInfo.Pgid = pushMenu.Last<string>();
                    sysMenuInfo.Name = dt.Rows[i][0].ToString().TrimStart('﹂');
                    sysMenuInfo.Icon = dt.Rows[i][1].ToString();
                    sysMenuInfo.Seq = dt.Rows[i][2].ToString();
                    sysMenuInfo.AuthGid = dt.Rows[i][3].ToString();
                    sysMenuInfo.IsVisable = (short)(dt.Rows[i][4].ToString() == "是" ? 1 : 0);
                    sysMenuInfo.WinformClass = dt.Rows[i][5].ToString();
                    sysMenuInfo.Url = dt.Rows[i][6].ToString();
                    sysMenuInfo.WebIcon = dt.Rows[i][7].ToString();
                    sysMenuInfo.SystemtypeId = dt.Rows[i][8].ToString();
                    sysMenuInfo.CreatorId = Convert.ToInt32(dt.Rows[i][9]);
                    sysMenuInfo.CreatorTime = Convert.ToDateTime( dt.Rows[i][10]);
                    sysMenuInfo.EditorId =Convert.ToInt32( dt.Rows[i][11]);
                    sysMenuInfo.LastUpdateTime = Convert.ToDateTime(dt.Rows[i][12]);
                    sysMenuInfo.IsDelete = (short)(dt.Rows[i][13].ToString() == "是" ? 1 : 0);
                    sysMenuInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();

                    if ((i + 1) < dt.Rows.Count && dt.Rows[i][0].ToString().LastIndexOf("﹂") < dt.Rows[i + 1][0].ToString().LastIndexOf("﹂"))
                    {
                        pushMenu.Add(sysMenuInfo.Gid);
                    }

                    // 返回到了某个父节点
                    if ((i + 1) < dt.Rows.Count && dt.Rows[i][0].ToString().LastIndexOf("﹂") > dt.Rows[i + 1][0].ToString().LastIndexOf("﹂"))
                    { 
                        // 需要多次弹出操作
                        for (Int32 j = 0; j < (dt.Rows[i][0].ToString().LastIndexOf("﹂") - dt.Rows[i+1][0].ToString().LastIndexOf("﹂")); j++)
                        {
                            pushMenu.RemoveAt(pushMenu.Count - 1);
                        }
                    }

                    addRows++;

                    lstsysMenuInfo.Add(sysMenuInfo);
                    xmlhelper.InsertElement("datatype/dataitem", "item", string.Format(xmlModel, sysMenuInfo.Gid, sysMenuInfo.Pgid, sysMenuInfo.Name, sysMenuInfo.Icon, sysMenuInfo.Seq, sysMenuInfo.AuthGid, sysMenuInfo.IsVisable, sysMenuInfo.WinformClass, sysMenuInfo.Url, sysMenuInfo.WebIcon, sysMenuInfo.SystemtypeId, sysMenuInfo.CreatorId, sysMenuInfo.CreatorTime, sysMenuInfo.EditorId, sysMenuInfo.LastUpdateTime, sysMenuInfo.IsDelete));
                    xmlhelper.Save(false);
                    
                }

                treelstMenu.DataSource = lstsysMenuInfo;
                treelstMenu.Refresh();
                ClearValidate();
                treelstMenu.ForceInitialize();
                treelstMenu.BestFitColumns();

                MessageDxUtil.ShowTips(string.Format("成功导入菜单数据{0}条数据", addRows));
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
            SysMenuInfo menuInfo = new SysMenuInfo();
            if (string.Equals(treelstMenu.FocusedNode.GetValue("Pgid").ToString(), Const.Num_MinusOne.ToString()))
            {
                menuInfo.Gid = Guid.NewGuid().ToString();
                menuInfo.Pgid = Const.Num_MinusOne.ToString();
                menuInfo.IsDelete = 0;
                menuInfo.IsVisable = 1;
                menuInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
                treelstMenu.FocusedNode = treelstMenu.AppendNode(menuInfo, null);
                treelstMenu.FocusedNode.SetValue("Gid", menuInfo.Gid);
                treelstMenu.FocusedNode.SetValue("Pgid", menuInfo.Pgid);
                treelstMenu.FocusedNode.SetValue("IsDelete", menuInfo.IsDelete);
                treelstMenu.FocusedNode.SetValue("IsVisable", menuInfo.IsVisable);
                treelstMenu.FocusedNode.SetValue("lstInfo", menuInfo.lstInfo);
            }
            else
            {
                menuInfo.Gid = Guid.NewGuid().ToString();
                menuInfo.Pgid = treelstMenu.FocusedNode.GetValue("Pgid").ToString();
                menuInfo.IsDelete = 0;
                menuInfo.IsVisable = 1;
                menuInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
                treelstMenu.FocusedNode = treelstMenu.AppendNode(menuInfo, treelstMenu.FocusedNode.ParentNode);
                treelstMenu.FocusedNode.SetValue("Gid", menuInfo.Gid);
                treelstMenu.FocusedNode.SetValue("Pgid", menuInfo.Pgid);
                treelstMenu.FocusedNode.SetValue("IsDelete", menuInfo.IsDelete);
                treelstMenu.FocusedNode.SetValue("IsVisable", menuInfo.IsVisable);
                treelstMenu.FocusedNode.SetValue("lstInfo", menuInfo.lstInfo);
            }
            xmlhelper.InsertElement("datatype/dataitem", "item", string.Format(xmlModel, menuInfo.Gid, menuInfo.Pgid, string.Empty, string.Empty, string.Empty, string.Empty, Const.Num_One.ToString(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Const.Num_Zero.ToString()));
            xmlhelper.Save(false);
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnAddNodes_Click(object sender, EventArgs e)
        {
            if (treelstMenu.FocusedNode != null)
            {
                SysMenuInfo menuInfo = new SysMenuInfo();
                menuInfo.Gid = Guid.NewGuid().ToString();
                menuInfo.Pgid = treelstMenu.FocusedNode.GetValue("Gid").ToString();
                menuInfo.IsDelete = 0;
                menuInfo.IsVisable = 1;
                menuInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
                treelstMenu.FocusedNode = treelstMenu.AppendNode(null, treelstMenu.FocusedNode);
                treelstMenu.FocusedNode.SetValue("Gid", menuInfo.Gid);
                treelstMenu.FocusedNode.SetValue("Pgid", menuInfo.Pgid);
                treelstMenu.FocusedNode.SetValue("IsDelete", menuInfo.IsDelete);
                treelstMenu.FocusedNode.SetValue("IsVisable", menuInfo.IsVisable);
                xmlhelper.InsertElement("datatype/dataitem", "item", string.Format(xmlModel, menuInfo.Gid, menuInfo.Pgid, string.Empty, string.Empty, string.Empty, string.Empty, Const.Num_One.ToString(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Const.Num_Zero.ToString()));
                xmlhelper.Save(false);
            }
        }

        /// <summary>
        /// 菜单的拖拽功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treelstMenu_DragDrop(object sender, DragEventArgs e)
        {
            DXDragEventArgs args = treelstMenu.GetDXDragEventArgs(e);
            if (args.DragInsertPosition == DragInsertPosition.After)
            {
                // 放到之后去则与TargetNode 的节点的父节点一致
                //Console.WriteLine(args.TargetNode.GetValue("PID"));
                string dragId = args.Node.GetValue("Gid").ToString();
                xmlhelper = new XmlHelper(@"XML\menu.xml");
                var xmlNodes = xmlhelper.Read("datatype/dataitem");
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
                xmlhelper = new XmlHelper(@"XML\menu.xml");
                var xmlNodes = xmlhelper.Read("datatype/dataitem");
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
        private void treelstMenu_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            string changId = treelstMenu.FocusedNode.GetValue("Gid").ToString();
            xmlhelper = new XmlHelper(@"XML\menu.xml");
            var xmlNodes = xmlhelper.Read("datatype/dataitem");
            for (Int32 i = 0; i < xmlNodes.Count; i++)
            {
                if (string.Equals(xmlNodes[i].ChildNodes[0].InnerText, changId))
                {
                    Int32 idx = -1;
                    switch (treelstMenu.FocusedColumn.FieldName)
                    {
                        case "Name":
                            idx = 2;
                            break;
                        case "Icon":
                            idx = 3;
                            break;
                        case "Seq":
                            idx = 4;
                            break;
                        case "AuthGid":
                            idx = 5;
                            break;
                        case "IsVisable":
                            idx = 6;
                            break;
                        case "WinformClass":
                            idx = 7;
                            break;
                        case "Url":
                            idx = 8;
                            break;
                        case "WebIcon":
                            idx = 9;
                            break;
                        case "SystemtypeId":
                            idx = 10;
                            break;
                        case "CreatorId":
                            idx = 11;
                            break;
                        case "CreateTime":
                            idx = 12;
                            break;
                        case "EditorId":
                            idx = 13;
                            break;
                        case "LastUpdateTime":
                            idx = 14;
                            break;
                        case "IsDelete":
                            idx = 15;
                            break;
                    }

                    if (idx == -1)
                    {
                        break;
                    }

                    if (idx == 15 || idx == 6)
                        xmlNodes[i].ChildNodes[idx].InnerText = Convert.ToBoolean(treelstMenu.FocusedNode.GetValue(treelstMenu.FocusedColumn.FieldName)) ? "1" : "0";
                    else
                        xmlNodes[i].ChildNodes[idx].InnerText = treelstMenu.FocusedNode.GetValue(treelstMenu.FocusedColumn.FieldName).ToString();
                    xmlhelper.Save(false);
                }
            }
            
        }

        /// <summary>
        /// 添加完成之后需要验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treelstMenu_ValidateNode(object sender, ValidateNodeEventArgs e)
        {
            ClearValidate();

            ValidateData();

            InitView();
        }
    }

    /// <summary>
    /// treeList 支持自定义过滤数据
    /// </summary>

    class FilterNodeOperation : DevExpress.XtraTreeList.Nodes.Operations.TreeListOperation
    {
        string pattern;

        public FilterNodeOperation(string _pattern)
        { pattern = _pattern; }

        public override void Execute(DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            if (NodeContainsPattern(node, pattern))
            {
                node.Visible = true;
                if (node.ParentNode != null)
                    node.ParentNode.Visible = true;
            }
            else
                node.Visible = false;
        }

        bool NodeContainsPattern(DevExpress.XtraTreeList.Nodes.TreeListNode node, string pattern)
        {
            foreach (DevExpress.XtraTreeList.Columns.TreeListColumn col in node.TreeList.Columns)
                if (node.GetValue(col).ToString().Contains(pattern))
                    return true;
            return false;
        }
    }  
}
