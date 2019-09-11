using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraNavBar;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Entity;
using System.Xml;
using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Text;
using JCodes.Framework.jCodesenum.BaseEnum;
using System.ComponentModel;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Common;
using System.Data;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.AddIn.Proj
{
    public partial class FrmGeneralSql : BaseDock
    {
        #region 控件集合
        private NavBarControl navBar;
        private NavBarGroup selectedGroup;
        private NavBarItemLink selectedLink;
        private UserControl control;
        ProgressBarControl progressBar;
        #endregion

        #region 数据缓存
        private List<DictInfo> dictTypeInfoList = null;
        private Dictionary<string, string> guidGroup = new Dictionary<string, string>();
        private Dictionary<string, string> tableGroup = new Dictionary<string, string>();
        #endregion

        #region 其他
        //第一步：定义BackgroundWorker对象，并注册事件（执行线程主体、执行UI更新事件）
        private BackgroundWorker backgroundWorker1 = null;
        #endregion

        public FrmGeneralSql()
        {
            InitializeComponent();

            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            //设置报告进度更新
            backgroundWorker1.WorkerReportsProgress = true;
            //注册线程主体方法
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            //注册更新UI方法
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        void frmMain_Load(object sender, System.EventArgs e) {
            LoadDicData();

            BeginInvoke(new MethodInvoker(InitDemo));
        }
       
        void InitDemo() {
            // 创建一个NarBar;
            navBar = new NavBarControl();
            ((System.ComponentModel.ISupportInitialize)(navBar)).BeginInit();
            navBar.Dock = System.Windows.Forms.DockStyle.Fill;
            navBar.DragDropFlags = ((DevExpress.XtraNavBar.NavBarDragDrop)(((DevExpress.XtraNavBar.NavBarDragDrop.AllowDrag | DevExpress.XtraNavBar.NavBarDragDrop.AllowDrop)
            | DevExpress.XtraNavBar.NavBarDragDrop.AllowOuterDrop)));
            navBar.Location = new System.Drawing.Point(0, 0);
            navBar.Margin = new System.Windows.Forms.Padding(2);
            navBar.Name = "navBar";
            navBar.OptionsNavPane.ExpandedWidth = 139;
            navBar.Size = new System.Drawing.Size(139, 373);
            navBar.StoreDefaultPaintStyleName = true;
            navBar.TabIndex = 3;
            navBar.Text = "navBar";

            navBar.MouseDown += (sender, e) =>
            {
                if (e.Button != MouseButtons.Right) return;
                var navBarControl1 = sender as NavBarControl;
                NavBarHitInfo hi = navBarControl1.CalcHitInfo(new Point(e.X, e.Y));
                selectedGroup = hi.Group;
                if (hi.InLink)
                {
                    selectedLink = hi.Link;
                    return;
                }
            };
            ucToolbox1.Controls.Add(navBar);
            ((System.ComponentModel.ISupportInitialize)(navBar)).EndInit();

            #region Init NavBarGroup 数据 && NavBarItem 数据
            List<CListItem> navBarGroups = new List<CListItem>();
            navBarGroups.Add(new CListItem("Table", "表结构"));
            navBarGroups.Add(new CListItem("TableData", "数据脚本"));
            navBarGroups.Add(new CListItem("SysConst", "系统常量"));
            navBarGroups.Add(new CListItem("ErrorNo", "标准错误号"));

            Dictionary<string, List<CListItem>> DicnavBarItems = new Dictionary<string, List<CListItem>>();
            List<CListItem> lstTable = new List<CListItem>();
            lstTable.Add(new CListItem("GenerTableSql", "数据库脚本"));
            DicnavBarItems.Add("Table", lstTable);

            List<CListItem> lstTableData = new List<CListItem>();
            lstTableData.Add(new CListItem("TableDataSql", "基础数据脚本"));
            lstTableData.Add(new CListItem("DictionarySql", "数据字典数据脚本"));
            lstTableData.Add(new CListItem("MenuSql", "系统菜单数据脚本"));
            lstTableData.Add(new CListItem("FunctionSql", "系统功能数据脚本"));
            DicnavBarItems.Add("TableData", lstTableData);

            foreach (CListItem navBarGroup in navBarGroups)
            {
                NavBarGroup standardGroup = navBar.Groups.Add();
                standardGroup.Caption = navBarGroup.Text;
                standardGroup.Tag = navBarGroup.Value;
                standardGroup.Expanded = true;
                // 如果没有子项则继续
                if (!DicnavBarItems.ContainsKey(navBarGroup.Value)) continue;
                
                List<CListItem> narBarItems = DicnavBarItems[navBarGroup.Value];
                foreach (CListItem narBarItem in narBarItems)
                {
                    NavBarItem item = new NavBarItem();
                    item.Caption = narBarItem.Text;
                    item.Tag = narBarItem.Value;
                    item.Name = narBarItem.Value;
                    item.Hint = narBarItem.Text;
                    item.LinkClicked += Item_LinkClicked;
                    navBar.Items.Add(item);
                    standardGroup.ItemLinks.Add(item);
                }
            }
            #endregion
        }

        /// <summary>
        /// 点击表信息得到的结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            var item = sender as NavBarItem;

            // 判断是否已经存在了如果存在了则改为选中
            for (System.Int32 i = 0; i < tabbedView.Documents.Count; i++)
            {
                // 找到 选中
                if (string.Equals(tabbedView.Documents[i].Tag, item.Hint))
                {
                    tabbedView.Controller.Activate(tabbedView.Documents[i]);
                    dockPanel6.HideSliding();
                    return;
                }
            }
            tabbedView.BeginUpdate();
            control = new UserControl();

            control.Name = item.Name;
            control.Text = item.Hint;

            List<object> args = new List<object>();

            #region 读取Table.xml 配置信息
            XmlHelper xmltableshelper = new XmlHelper(@"XML\tables.xml");
            XmlNodeList xmlNodeLst = xmltableshelper.Read("datatype/tabletype");
            guidGroup.Clear();
            tableGroup.Clear();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;

                // 获取字符串中的英文字母 [a-zA-Z]+
                string GroupEnglishName = CRegex.GetText(xe.GetAttribute("name").ToString(), "[a-zA-Z]+", 0);

                guidGroup.Add(xe.GetAttribute("guid").ToString(), string.Format("{0}{1}_", Const.TablePre, GroupEnglishName));
            }

            XmlNodeList xmlNodeLst2 = xmltableshelper.Read("datatype/dataitem");
            foreach (XmlNode xn1 in xmlNodeLst2)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值

                // 得到ConstantInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;

                tableGroup.Add(xnl0.Item(0).InnerText, guidGroup[xnl0.Item(3).InnerText]);
            }
            #endregion

            #region 根据Name 去动态生成控件
            switch (control.Name)
            { 
                case "GenerTableSql":
                    #region 加载.table数据信息
                    string[] filenames = Directory.GetFiles("./XML/", "*.table", SearchOption.TopDirectoryOnly);
                    #endregion 

                    Panel p1 = new Panel();
                    p1.Dock = DockStyle.Top;
                    p1.Height = 30;
                    Panel p2 = new Panel();
                    p2.Dock = DockStyle.Right;
                    p2.Width = 10;
                    progressBar = new ProgressBarControl();
                    //设置一个最小值
                    progressBar.Properties.Minimum = 0;
                    //设置一个最大值
                    progressBar.Properties.Maximum = filenames.Length;
                    //设置步长，即每次增加的数
                    progressBar.Properties.Step = 1;
                    //设置进度条的样式
                    progressBar.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                    progressBar.Dock = DockStyle.Fill;
                    progressBar.Properties.ShowTitle = true;

                    SimpleButton btn = new SimpleButton();
                    btn.Dock = DockStyle.Right;
                    btn.Name = "btnde";
                    btn.Size = new System.Drawing.Size(90, 25);
                    btn.Text = "执行";
                    btn.Click += (sender1, e1) => {
                        progressBar.Position = 0;
                        args.Clear();
                        args.Add(control.Name);
                        args.Add(filenames);
                        this.backgroundWorker1.RunWorkerAsync(args);
                    };

                    p1.Controls.Add(progressBar);
                    p1.Controls.Add(p2);
                    p1.Controls.Add(btn);
                    control.Controls.Add(p1);
                    break;

                case "TableDataSql":
                    #region 加载.basicdata数据信息
                    string[] filenames2 = Directory.GetFiles("./XML/", "*.basicdata", SearchOption.TopDirectoryOnly);
                    #endregion 

                    Panel p3 = new Panel();
                    p3.Dock = DockStyle.Top;
                    p3.Height = 30;
                    Panel p4 = new Panel();
                    p4.Dock = DockStyle.Right;
                    p4.Width = 10;
                    progressBar = new ProgressBarControl();
                    //设置一个最小值
                    progressBar.Properties.Minimum = 0;
                    //设置一个最大值
                    progressBar.Properties.Maximum = filenames2.Length;
                    //设置步长，即每次增加的数
                    progressBar.Properties.Step = 1;
                    //设置进度条的样式
                    progressBar.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                    progressBar.Dock = DockStyle.Fill;
                    progressBar.Properties.ShowTitle = true;

                    SimpleButton btn2 = new SimpleButton();
                    btn2.Dock = DockStyle.Right;
                    btn2.Name = "btnde";
                    btn2.Size = new System.Drawing.Size(90, 25);
                    btn2.Text = "执行";
                    btn2.Click += (sender1, e1) =>
                    {
                        progressBar.Position = 0;
                        args.Clear();
                        args.Add(control.Name);
                        args.Add(filenames2);
                        this.backgroundWorker1.RunWorkerAsync(args);
                    };

                    p3.Controls.Add(progressBar);
                    p3.Controls.Add(p4);
                    p3.Controls.Add(btn2);
                    control.Controls.Add(p3);
                    break;

                case "DictionarySql":
                    Panel p5 = new Panel();
                    p5.Dock = DockStyle.Top;
                    p5.Height = 30;
                    Panel p6 = new Panel();
                    p6.Dock = DockStyle.Right;
                    p6.Width = 10;
                    progressBar = new ProgressBarControl();
                    //设置一个最小值
                    progressBar.Properties.Minimum = 0;
                    //设置一个最大值
                    progressBar.Properties.Maximum = 2;
                    //设置步长，即每次增加的数
                    progressBar.Properties.Step = 1;
                    //设置进度条的样式
                    progressBar.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                    progressBar.Dock = DockStyle.Fill;
                    progressBar.Properties.ShowTitle = true;

                    SimpleButton btn3 = new SimpleButton();
                    btn3.Dock = DockStyle.Right;
                    btn3.Name = "btnde";
                    btn3.Size = new System.Drawing.Size(90, 25);
                    btn3.Text = "执行";
                    btn3.Click += (sender1, e1) =>
                    {
                        progressBar.Position = 0;
                        args.Clear();
                        args.Add(control.Name);
                        this.backgroundWorker1.RunWorkerAsync(args);
                    };

                    p5.Controls.Add(progressBar);
                    p5.Controls.Add(p6);
                    p5.Controls.Add(btn3);
                    control.Controls.Add(p5);
                    break;
                case "MenuSql":
                    Panel p7 = new Panel();
                    p7.Dock = DockStyle.Top;
                    p7.Height = 30;
                    Panel p8 = new Panel();
                    p8.Dock = DockStyle.Right;
                    p8.Width = 10;
                    progressBar = new ProgressBarControl();
                    //设置一个最小值
                    progressBar.Properties.Minimum = 0;
                    //设置一个最大值
                    progressBar.Properties.Maximum = 1;
                    //设置步长，即每次增加的数
                    progressBar.Properties.Step = 1;
                    //设置进度条的样式
                    progressBar.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                    progressBar.Dock = DockStyle.Fill;
                    progressBar.Properties.ShowTitle = true;

                    SimpleButton btn4 = new SimpleButton();
                    btn4.Dock = DockStyle.Right;
                    btn4.Name = "btnde";
                    btn4.Size = new System.Drawing.Size(90, 25);
                    btn4.Text = "执行";
                    btn4.Click += (sender1, e1) =>
                    {
                        progressBar.Position = 0;
                        args.Clear();
                        args.Add(control.Name);
                        this.backgroundWorker1.RunWorkerAsync(args);
                    };

                    p7.Controls.Add(progressBar);
                    p7.Controls.Add(p8);
                    p7.Controls.Add(btn4);
                    control.Controls.Add(p7);
                    break;
                case "FunctionSql":
                    break;
            }

            
            #endregion

            BaseDocument document = tabbedView.AddDocument(control);
            document.Footer = Directory.GetCurrentDirectory();
            document.Tag = item.Hint;

            tabbedView.EndUpdate();
            tabbedView.Controller.Activate(document);

            dockPanel6.HideSliding();
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
                dictInfo.Id = xnl0.Item(0).InnerText.ToInt32();
                dictInfo.Pid = xnl0.Item(1).InnerText.ToInt32();
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

                dictInfo.Remark = sb.ToString().TrimEnd(",\r\n".ToCharArray());

                dictTypeInfoList.Add(dictInfo);
            }
            #endregion
        }

        //第二步：定义执行线程主体事件
        //线程主体方法
        public void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> args = e.Argument as List<object>;

            #region 根据项目属性获取数据类型
            XmlHelper xmlprojectthelper = new XmlHelper(@"XML\project.xml");
            XmlNodeList xmlprejectNodeLst = xmlprojectthelper.Read("datatype");

            if (xmlprejectNodeLst.Count == 0)
                return;

            XmlNode xn1project = xmlprejectNodeLst[0];

            // 将节点转换为元素，便于得到节点的属性值
            XmlElement xeproject = (XmlElement)xn1project;

            // 得到DataTypeInfo节点的所有子节点
            XmlNodeList xnl0project = xeproject.ChildNodes;

            string dbType = xnl0project.Item(4).InnerText;
            string filePath = xnl0project.Item(9).InnerText;
            #endregion

            switch (args[0].ToString())
            { 
                case "GenerTableSql":
                    string[] filenames = args[1] as string[];
                    Dictionary<string, string> constrainttypelst = new Dictionary<string, string>();
                    constrainttypelst.Add("0", "主键");
                    constrainttypelst.Add("1", "索引");
                    constrainttypelst.Add("2", "唯一索引");

                    //在线程中更新UI（通过ReportProgress方法）
                    //...执行线程任务
 
                    #region 先读取datatype.xml 在读取defaulttype.xml 然后Linq 查询保存到数据字典dic中
                    XmlHelper xmldatatypehelper = new XmlHelper(@"XML\datatype.xml");
                    XmlNodeList xmldatatypeNodeLst = xmldatatypehelper.Read("datatype");
                    List<DataTypeInfo> dataTypeInfoList = new List<DataTypeInfo>();
                    foreach (XmlNode xn1 in xmldatatypeNodeLst)
                    {
                        DataTypeInfo dataTypeInfo = new DataTypeInfo();
                        // 将节点转换为元素，便于得到节点的属性值
                        XmlElement xe = (XmlElement)xn1;
                        // 得到Type和ISBN两个属性的属性值
                        dataTypeInfo.GUID = xe.GetAttribute("guid").ToString();

                        // 得到DataTypeInfo节点的所有子节点
                        XmlNodeList xnl0 = xe.ChildNodes;
                        dataTypeInfo.Name = xnl0.Item(0).InnerText;
                        dataTypeInfo.StdType = xnl0.Item(2).InnerText;
                        dataTypeInfo.Length = xnl0.Item(3).InnerText;
                        dataTypeInfo.Precision = xnl0.Item(4).InnerText;

                        dataTypeInfoList.Add(dataTypeInfo);
                    }

                    XmlHelper defaulttypexmlHelper = new XmlHelper(@"XML\defaulttype.xml");
                    XmlNodeList defaulttypexmlNodeLst = defaulttypexmlHelper.Read("datatype");
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    foreach (var dataTypeInfo in dataTypeInfoList)
                    {
                        foreach (XmlNode xn1 in defaulttypexmlNodeLst)
                        {
                            // 将节点转换为元素，便于得到节点的属性值
                            XmlElement xe = (XmlElement)xn1;
                            // 得到DataTypeInfo节点的所有子节点
                            XmlNodeList xnl0 = xe.ChildNodes;
                            string value = string.Empty;
                            if (dbType == "Oracle")
                                value = xnl0.Item(2).InnerText;
                            else if (dbType == "Mysql")
                                value = xnl0.Item(3).InnerText;
                            else if (dbType == "DB2")
                                value = xnl0.Item(4).InnerText;
                            else if (dbType == "SqlServer")
                                value = xnl0.Item(5).InnerText;
                            else if (dbType == "Sqlite")
                                value = xnl0.Item(6).InnerText;
                            else if (dbType == "Access")
                                value = xnl0.Item(7).InnerText;

                            // 找到匹配记录
                            if (dataTypeInfo.StdType == xnl0.Item(0).InnerText)
                            {
                                if (value.Contains("$L"))
                                {
                                    if (String.Empty == dataTypeInfo.Length)
                                        value = value.Replace("$L", "0");
                                    else
                                        value = value.Replace("$L", dataTypeInfo.Length);

                                }
                                if (value.Contains("$P"))
                                {
                                    if (String.Empty == dataTypeInfo.Precision)
                                        value = value.Replace("$P", "0");
                                    else
                                        value = value.Replace("$P", dataTypeInfo.Precision);
                                }
                                dict.Add(dataTypeInfo.Name, value);
                            }
                        }
                    }

                    XmlHelper stdfieldxmlHelper = new XmlHelper(@"XML\stdfield.xml");
                    XmlNodeList stdfieldxmlNodeLst = stdfieldxmlHelper.Read("datatype/dataitem");

                    List<StdFieldComboBox> stdFieldInfoList = new List<StdFieldComboBox>();
                    foreach (XmlNode xn1 in stdfieldxmlNodeLst)
                    {
                        // 将节点转换为元素，便于得到节点的属性值
                        XmlElement xe = (XmlElement)xn1;
                        // 得到DataTypeInfo节点的所有子节点
                        XmlNodeList xnl0 = xe.ChildNodes;
                        StdFieldComboBox listItem = new StdFieldComboBox();
                        listItem.Name = xnl0.Item(0).InnerText;
                        listItem.ChineseName = xnl0.Item(1).InnerText;
                        listItem.DataType = xnl0.Item(2).InnerText;
                        listItem.DictNo = xnl0.Item(3).InnerText.ToInt32();
                        if (dictTypeInfoList != null)
                        {
                            var dictType = dictTypeInfoList.Find(new Predicate<DictInfo>(dictinfo => dictinfo.Id == xnl0.Item(3).InnerText.ToInt32()));
                            if (dictType != null) listItem.DictNameLst = dictType.Remark;
                        }

                        stdFieldInfoList.Add(listItem);
                    }
                    #endregion

                    if (FileUtil.IsExistFile(filePath + "\\TableInit.sql"))
                        FileUtil.DeleteFile(filePath + "\\TableInit.sql");

                    FileUtil.CreateFile(filePath + "\\TableInit.sql");

                    #region 处理每个Table脚本
                    for (Int32 ii = 0; ii < filenames.Length; ii++)
                    {
                        XmlHelper xmltablesinfohelper = new XmlHelper(filenames[ii]);

                        XmlNodeList xmlbasicsLst = xmltablesinfohelper.Read(string.Format("datatype/basicinfo"));
                        XmlNodeList xmlbasicsOne = ((XmlElement)xmlbasicsLst[0]).ChildNodes;
                        string englishName = xmlbasicsOne.Item(1).InnerText;
                        string chineseName = xmlbasicsOne.Item(2).InnerText;
                        Boolean checkHis = Convert.ToInt32(xmlbasicsOne.Item(3).InnerText) == 0 ? false : true;

                        XmlNodeList xmlfieldsLst = xmltablesinfohelper.Read(string.Format("datatype/fieldsinfo"));
                        List<TableFieldsInfo> FieldsInfoLst = new List<TableFieldsInfo>();
                        foreach (XmlNode xn1 in xmlfieldsLst)
                        {
                            TableFieldsInfo tablefieldInfo = new TableFieldsInfo();

                            // 将节点转换为元素，便于得到节点的属性值
                            XmlElement xe = (XmlElement)xn1;

                            tablefieldInfo.GUID = xe.GetAttribute("guid").ToString();

                            // 得到DataTypeInfo节点的所有子节点
                            XmlNodeList xnl0 = xe.ChildNodes;

                            for (Int32 i = 0; i < stdFieldInfoList.Count; i++)
                            {
                                if (string.Equals(stdFieldInfoList[i].Name, xnl0.Item(0).InnerText))
                                {
                                    tablefieldInfo.FieldName = stdFieldInfoList[i].Name;
                                    tablefieldInfo.ChineseName = stdFieldInfoList[i].ChineseName;
                                    tablefieldInfo.FieldType = stdFieldInfoList[i].DataType;
                                    tablefieldInfo.FieldInfo = stdFieldInfoList[i].DictNameLst;
                                    break;
                                }
                            }

                            tablefieldInfo.IsNull = xnl0.Item(1).InnerText == "0" ? false : true;
                            tablefieldInfo.Remark = xnl0.Item(2).InnerText;
                            tablefieldInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
                            FieldsInfoLst.Add(tablefieldInfo);
                        }

                        // 如果没有字段则写日志继续
                        if (FieldsInfoLst.Count == 0)
                        {
                            backgroundWorker1.ReportProgress((Int32)(progressBar.Position + 1) / progressBar.Properties.Maximum, string.Format("{0} 表[{1}({2})]不存在字段,请检查,处理完成.\r\n", LogLevel.LOG_LEVEL_ERR, chineseName, englishName));
                            continue;
                        }

                        XmlNodeList xmlindexLst = xmltablesinfohelper.Read(string.Format("datatype/indexsinfo"));

                        List<TableIndexsInfo> IndexsInfoLst = new List<TableIndexsInfo>();

                        foreach (XmlNode xn1 in xmlindexLst)
                        {
                            TableIndexsInfo tableindexsInfo = new TableIndexsInfo();

                            // 将节点转换为元素，便于得到节点的属性值
                            XmlElement xe = (XmlElement)xn1;
                            tableindexsInfo.GUID = xe.GetAttribute("guid").ToString();

                            // 得到DataTypeInfo节点的所有子节点
                            XmlNodeList xnl0 = xe.ChildNodes;
                            tableindexsInfo.IndexName = xnl0.Item(0).InnerText;
                            tableindexsInfo.IndexFieldLst = xnl0.Item(1).InnerText;
                            tableindexsInfo.ConstraintType = constrainttypelst[xnl0.Item(2).InnerText];
                            tableindexsInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
                            IndexsInfoLst.Add(tableindexsInfo);
                        }

                        FileUtil.AppendText(filePath + "\\TableInit.sql", JCodes.Framework.Common.Proj.SqlOperate.initTableInfo(dbType, tableGroup[englishName]+englishName, chineseName, checkHis, FieldsInfoLst, IndexsInfoLst, dict), Encoding.UTF8);

                        backgroundWorker1.ReportProgress((Int32)(progressBar.Position + 1) / progressBar.Properties.Maximum, string.Format("{0} [{1}({2})]处理完成.\r\n", LogLevel.LOG_LEVEL_INFO, chineseName, englishName));
                    }
                    #endregion
                    break;
                case "TableDataSql":
                    string[] filenames2 = args[1] as string[];

                    //在线程中更新UI（通过ReportProgress方法）
                    if (FileUtil.IsExistFile(filePath + "\\TableDataSql.sql"))
                        FileUtil.DeleteFile(filePath + "\\TableDataSql.sql");

                    FileUtil.CreateFile(filePath + "\\TableDataSql.sql");

                    #region 处理每个Table脚本
                    for (Int32 ii = 0; ii < filenames2.Length; ii++)
                    {
                        DataTable dt = new DataTable();
                        // 先读取表结构信息
                        XmlHelper xmltablefieldhelper = new XmlHelper(filenames2[ii].Replace("basicdata", "table"));

                        XmlNodeList xmlfieldsbasicLst = xmltablefieldhelper.Read(string.Format("datatype/basicinfo"));

                        XmlNodeList xnl0basic = ((XmlElement)xmlfieldsbasicLst[0]).ChildNodes;
                        string englishName = xnl0basic.Item(1).InnerText;
                        string chineseName = xnl0basic.Item(2).InnerText;

                        XmlNodeList xmlfieldsLst = xmltablefieldhelper.Read(string.Format("datatype/fieldsinfo"));

                        foreach (XmlNode xn1 in xmlfieldsLst)
                        {
                            // 将节点转换为元素，便于得到节点的属性值
                            XmlElement xe = (XmlElement)xn1;
                            //tablefieldInfo.GUID = xe.GetAttribute("guid").ToString();
                            // 得到DataTypeInfo节点的所有子节点
                            XmlNodeList xnl0 = xe.ChildNodes;

                            dt.Columns.Add(xnl0.Item(0).InnerText);
                        }

                        // 读取数据信息
                        XmlHelper xmltabledatahelper = new XmlHelper(filenames2[ii]);
                        XmlNodeList xmlNodeLst = xmltabledatahelper.Read("datatype/dataitem");

                        foreach (XmlNode xn1 in xmlNodeLst)
                        {
                            DataRow dr = dt.NewRow();
                            // 将节点转换为元素，便于得到节点的属性值
                            XmlElement xe = (XmlElement)xn1;

                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                if (xe.Attributes[dt.Columns[i].ColumnName.ToLower()] != null)
                                {
                                    dr[dt.Columns[i].ColumnName] = xe.GetAttribute(dt.Columns[i].ColumnName.ToLower()).ToString();
                                }
                                else
                                {
                                    xe.SetAttribute(dt.Columns[i].ColumnName.ToLower(), string.Empty);
                                }
                            }
                            xmltabledatahelper.Save(false);
                            dt.Rows.Add(dr);

                            // 根据属性去反查是否存在不存在则删除
                            List<String> tmp = new List<string>();
                            foreach (XmlAttribute a in xe.Attributes)
                            {
                                if (dt.Columns[a.Name] == null && !string.Equals(a.Name, "guid"))
                                    tmp.Add(a.Name);
                            }
                            foreach (var s in tmp)
                            {
                                xe.RemoveAttribute(s);
                            }
                            xmltabledatahelper.Save(false);
                        }

                        FileUtil.AppendText(filePath + "\\TableDataSql.sql", JCodes.Framework.Common.Proj.SqlOperate.initTableDataInfo(dbType, tableGroup[englishName] + englishName, chineseName, dt), Encoding.Default);

                        backgroundWorker1.ReportProgress((Int32)(progressBar.Position + 1) / progressBar.Properties.Maximum, string.Format("{0} [{1}({2})]处理完成.\r\n", LogLevel.LOG_LEVEL_INFO, chineseName, englishName));
                    }
                    #endregion
                    break;
                case "DictionarySql":
                    //在线程中更新UI（通过ReportProgress方法）
                    if (FileUtil.IsExistFile(filePath + "\\Dict.sql"))
                        FileUtil.DeleteFile(filePath + "\\Dict.sql");

                    FileUtil.CreateFile(filePath + "\\Dict.sql");

                    #region 处理每个Table脚本
                    XmlHelper xmldicthelper = new XmlHelper(@"XML\dict.xml");
                    XmlNodeList xmlNodeLst2 = xmldicthelper.Read("datatype/dataitem");
                    List<DictInfo> dictTypeInfoList2 = new List<DictInfo>();
                    List<DictDataInfo> dictDetailInfoList = new List<DictDataInfo>();
                    foreach (XmlNode xn1 in xmlNodeLst2)
                    {
                        DictInfo dictInfo = new DictInfo();
                        // 将节点转换为元素，便于得到节点的属性值
                        XmlElement xe = (XmlElement)xn1;
             
                        // 得到DataTypeInfo节点的所有子节点
                        XmlNodeList xnl0 = xe.ChildNodes;
                        dictInfo.Id = Convert.ToInt32(xnl0.Item(0).InnerText);
                        dictInfo.Pid = Convert.ToInt32(xnl0.Item(1).InnerText);
                        dictInfo.Name = xnl0.Item(2).InnerText;
                        dictInfo.Remark = xnl0.Item(3).InnerText;

                        if (!string.IsNullOrEmpty(xnl0.Item(4).InnerXml))
                        { 
                            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();//新建对象
                            doc.LoadXml("<tmp>" + xnl0.Item(4).InnerXml + "</tmp>");//符合xml格式的字符串
                            var nodes = doc.DocumentElement.ChildNodes;
                            foreach (var node in nodes)
                            {
                                DictDataInfo dicdetailInfo = new DictDataInfo();
                                var dNode = ((XmlElement)node).ChildNodes;
                                dicdetailInfo.Value = dNode.Item(0).InnerText;
                                dicdetailInfo.Name = dNode.Item(1).InnerText;
                                dicdetailInfo.Seq = dNode.Item(2).InnerText;
                                dicdetailInfo.Remark = dNode.Item(3).InnerText;
                                dicdetailInfo.DicttypeID = dictInfo.Id;

                                dictDetailInfoList.Add(dicdetailInfo);
                            }
                        }

                        dictTypeInfoList2.Add(dictInfo);
                    }

                    // T_Basic_DictType
                    // T_Basic_DictData
                    FileUtil.AppendText(filePath + "\\Dict.sql", JCodes.Framework.Common.Proj.SqlOperate.initDictTypeInfo(dbType, tableGroup["DictType"] + "DictType", "数据字典类型", dictTypeInfoList2), Encoding.Default);

                    backgroundWorker1.ReportProgress((Int32)(progressBar.Position + 1) / progressBar.Properties.Maximum, string.Format("{0} [{1}({2})]处理完成.\r\n", LogLevel.LOG_LEVEL_INFO, "数据字典类型", "DictType"));

                    FileUtil.AppendText(filePath + "\\Dict.sql", JCodes.Framework.Common.Proj.SqlOperate.initDictDataInfo(dbType, tableGroup["DictData"] + "DictData", "数据字典明细", dictDetailInfoList), Encoding.Default);

                    backgroundWorker1.ReportProgress((Int32)(progressBar.Position + 1) / progressBar.Properties.Maximum, string.Format("{0} [{1}({2})]处理完成.\r\n", LogLevel.LOG_LEVEL_INFO, "数据字典明细", "DictData"));
 
                    #endregion
                        
                    break;
                case "MenuSql":
                    //在线程中更新UI（通过ReportProgress方法）
                    if (FileUtil.IsExistFile(filePath + "\\Menu.sql"))
                        FileUtil.DeleteFile(filePath + "\\Menu.sql");

                    FileUtil.CreateFile(filePath + "\\Menu.sql");

                    #region 处理每个Table脚本
                    XmlHelper xmlmenuhelper = new XmlHelper(@"XML\menu.xml");
                    XmlNodeList xmlNodeLst3 = xmlmenuhelper.Read("datatype/dataitem");
                    List<MenuInfo> menuInfolst = new List<MenuInfo>();
                    foreach (XmlNode xn1 in xmlNodeLst3)
                    {
                        MenuInfo menuinfo = new MenuInfo();
                        // 将节点转换为元素，便于得到节点的属性值
                        XmlElement xe = (XmlElement)xn1;

                        // 得到DataTypeInfo节点的所有子节点
                        XmlNodeList xnl0 = xe.ChildNodes;
                        menuinfo.Gid = xnl0.Item(0).InnerText;
                        menuinfo.Pgid = xnl0.Item(1).InnerText;
                        menuinfo.Name = xnl0.Item(2).InnerText;
                        menuinfo.Icon = xnl0.Item(3).InnerText;
                        menuinfo.Seq = xnl0.Item(4).InnerText;
                        menuinfo.AuthGid = xnl0.Item(5).InnerText;
                        menuinfo.IsVisable = Convert.ToInt32(xnl0.Item(6).InnerText) == Const.Num_Zero ? false : true;
                        menuinfo.WinformClass = xnl0.Item(7).InnerText;
                        menuinfo.Url = xnl0.Item(8).InnerText;
                        menuinfo.WebIcon = xnl0.Item(9).InnerText;
                        menuinfo.SystemtypeId = xnl0.Item(10).InnerText;
                        menuinfo.CreatorId = Convert.ToInt32( xnl0.Item(11).InnerText);
                        menuinfo.CreateTime = Convert.ToDateTime( xnl0.Item(12).InnerText);
                        menuinfo.EditorId = Convert.ToInt32(xnl0.Item(13).InnerText);
                        menuinfo.LastUpdateTime  = Convert.ToDateTime( xnl0.Item(14).InnerText);
                        menuinfo.IsDelete = Convert.ToInt32(xnl0.Item(15).InnerText) == Const.Num_Zero ? false : true;
                        menuInfolst.Add(menuinfo);
                    }

                    FileUtil.AppendText(filePath + "\\Menu.sql", JCodes.Framework.Common.Proj.SqlOperate.initMenuInfo(dbType, tableGroup["Menu"] + "Menu", "系统菜单", menuInfolst), Encoding.Default);

                    backgroundWorker1.ReportProgress((Int32)(progressBar.Position + 1) / progressBar.Properties.Maximum, string.Format("{0} [{1}({2})]处理完成.\r\n", LogLevel.LOG_LEVEL_INFO, "系统菜单", "Menu"));
                    #endregion
                    break;
                case "FunctionSql":
                    //在线程中更新UI（通过ReportProgress方法）
                    if (FileUtil.IsExistFile(filePath + "\\Function.sql"))
                        FileUtil.DeleteFile(filePath + "\\Function.sql");

                    FileUtil.CreateFile(filePath + "\\Function.sql");

                    #region 处理每个Table脚本
                    XmlHelper xmlfunctionhelper = new XmlHelper(@"XML\function.xml");
                    XmlNodeList xmlNodeLst4 = xmlfunctionhelper.Read("datatype/dataitem");
                    List<FunctionInfo> functionInfolst = new List<FunctionInfo>();
                    foreach (XmlNode xn1 in xmlNodeLst4)
                    {
                        FunctionInfo functioninfo = new FunctionInfo();
                        // 将节点转换为元素，便于得到节点的属性值
                        XmlElement xe = (XmlElement)xn1;

                        // 得到DataTypeInfo节点的所有子节点
                        XmlNodeList xnl0 = xe.ChildNodes;
                        functioninfo.ID = xnl0.Item(0).InnerText;
                        functioninfo.PID = xnl0.Item(1).InnerText;
                        functioninfo.Name = xnl0.Item(2).InnerText;
                        functioninfo.FunctionId = xnl0.Item(3).InnerText;
                        functioninfo.SystemType_ID = xnl0.Item(4).InnerText;
                        functioninfo.Seq = xnl0.Item(5).InnerText;
                        functionInfolst.Add(functioninfo);
                    }

                    FileUtil.AppendText(filePath + "\\Function.sql", JCodes.Framework.Common.Proj.SqlOperate.initFunctionInfo(dbType, tableGroup["Function"] + "Function", "系统功能", functionInfolst), Encoding.Default);

                    #endregion
                    break;
            }
            //...执行线程其他任务

            backgroundWorker1.ReportProgress(100, string.Format("{0} 已全部生成完毕.\r\n", LogLevel.LOG_LEVEL_INFO));
        }

        //第三步：定义执行UI更新事件
        //UI更新方法
        public void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            richText.Text += e.UserState.ToString();
            richText.Document.CaretPosition = richText.Document.Range.End;
            richText.ScrollToCaret();

            if (e.UserState.ToString().Contains("处理完成") && progressBar != null)
            {
                progressBar.Position += 1; 
            }
        }
    }
}
