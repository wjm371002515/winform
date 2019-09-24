using JCodes.Framework.AddIn.Proj;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.CommonControl.PlugInInterface;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.TestWinForm.Basic
{
    public partial class InitDataFrm : BaseDock
    {
        #region 其他
        //第一步：定义BackgroundWorker对象，并注册事件（执行线程主体、执行UI更新事件）
        private BackgroundWorker backgroundWorker1 = null;
        #endregion

        private List<DictInfo> dictTypeInfoList = null;
        private Dictionary<string, string> guidGroup = new Dictionary<string, string>();
        private Dictionary<string, string> tableGroup = new Dictionary<string, string>();

        public InitDataFrm()
        {
            InitializeComponent();

            LoadDicData();

            InitData();

            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            //设置报告进度更新
            backgroundWorker1.WorkerReportsProgress = true;
            //注册线程主体方法
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            //注册更新UI方法
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        private void InitData()
        {
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

                guidGroup.Add(xe.GetAttribute("gid").ToString(), string.Format("{0}{1}_", Const.TablePre, GroupEnglishName));
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
                        dataTypeInfo.Gid = xe.GetAttribute("gid").ToString();

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

                            tablefieldInfo.Gid = xe.GetAttribute("gid").ToString();

                            // 得到DataTypeInfo节点的所有子节点
                            XmlNodeList xnl0 = xe.ChildNodes;

                            for (Int32 i = 0; i < stdFieldInfoList.Count; i++)
                            {
                                if (string.Equals(stdFieldInfoList[i].Name, xnl0.Item(0).InnerText))
                                {
                                    tablefieldInfo.FieldName = stdFieldInfoList[i].Name;
                                    tablefieldInfo.ChineseName = stdFieldInfoList[i].ChineseName;
                                    tablefieldInfo.DataType = stdFieldInfoList[i].DataType;
                                    tablefieldInfo.FieldInfo = stdFieldInfoList[i].DictNameLst;
                                    break;
                                }
                            }

                            tablefieldInfo.IsNull = Convert.ToInt16 (xnl0.Item(1).InnerText);
                            tablefieldInfo.Remark = xnl0.Item(2).InnerText;
                            tablefieldInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
                            FieldsInfoLst.Add(tablefieldInfo);
                        }

                        // 如果没有字段则写日志继续
                        if (FieldsInfoLst.Count == 0)
                        {
                            continue;
                        }

                        XmlNodeList xmlindexLst = xmltablesinfohelper.Read(string.Format("datatype/indexsinfo"));

                        List<TableIndexsInfo> IndexsInfoLst = new List<TableIndexsInfo>();

                        foreach (XmlNode xn1 in xmlindexLst)
                        {
                            TableIndexsInfo tableindexsInfo = new TableIndexsInfo();

                            // 将节点转换为元素，便于得到节点的属性值
                            XmlElement xe = (XmlElement)xn1;
                            tableindexsInfo.Gid = xe.GetAttribute("gid").ToString();

                            // 得到DataTypeInfo节点的所有子节点
                            XmlNodeList xnl0 = xe.ChildNodes;
                            tableindexsInfo.IndexName = xnl0.Item(0).InnerText;
                            tableindexsInfo.IndexFieldLst = xnl0.Item(1).InnerText;
                            tableindexsInfo.ConstraintType = (short)constrainttypelst[xnl0.Item(2).InnerText].ToInt32();
                            tableindexsInfo.lstInfo = new Dictionary<string, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo>();
                            IndexsInfoLst.Add(tableindexsInfo);
                        }

                        FileUtil.AppendText(filePath + "\\TableInit.sql", JCodes.Framework.Common.Proj.SqlOperate.initTableInfo(dbType, tableGroup[englishName] + englishName, chineseName, checkHis, FieldsInfoLst, IndexsInfoLst, dict), Encoding.UTF8);
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
                                dicdetailInfo.DicttypeValue = dNode.Item(0).InnerText.ToInt32();
                                dicdetailInfo.Name = dNode.Item(1).InnerText;
                                dicdetailInfo.Seq = dNode.Item(2).InnerText;
                                dicdetailInfo.Remark = dNode.Item(3).InnerText;
                                dicdetailInfo.DicttypeId = dictInfo.Id;

                                dictDetailInfoList.Add(dicdetailInfo);
                            }
                        }

                        dictTypeInfoList2.Add(dictInfo);
                    }

                    // T_Basic_DictType
                    // T_Basic_DictData
                    FileUtil.AppendText(filePath + "\\Dict.sql", JCodes.Framework.Common.Proj.SqlOperate.initDictTypeInfo(dbType, tableGroup["DictType"] + "DictType", "数据字典类型", dictTypeInfoList2), Encoding.Default);

                    FileUtil.AppendText(filePath + "\\Dict.sql", JCodes.Framework.Common.Proj.SqlOperate.initDictDataInfo(dbType, tableGroup["DictData"] + "DictData", "数据字典明细", dictDetailInfoList), Encoding.Default);

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
                        menuinfo.IsVisable = Convert.ToInt16(xnl0.Item(6).InnerText);
                        menuinfo.WinformClass = xnl0.Item(7).InnerText;
                        menuinfo.Url = xnl0.Item(8).InnerText;
                        menuinfo.WebIcon = xnl0.Item(9).InnerText;
                        menuinfo.SystemtypeId = xnl0.Item(10).InnerText;
                        menuinfo.CreatorId = Convert.ToInt32(xnl0.Item(11).InnerText);
                        menuinfo.CreatorTime = Convert.ToDateTime(xnl0.Item(12).InnerText);
                        menuinfo.EditorId = Convert.ToInt32(xnl0.Item(13).InnerText);
                        menuinfo.LastUpdateTime = Convert.ToDateTime(xnl0.Item(14).InnerText);
                        menuinfo.IsDelete = Convert.ToInt16(xnl0.Item(15).InnerText);
                        menuInfolst.Add(menuinfo);
                    }

                    FileUtil.AppendText(filePath + "\\Menu.sql", JCodes.Framework.Common.Proj.SqlOperate.initMenuInfo(dbType, tableGroup["Menu"] + "Menu", "系统菜单", menuInfolst), Encoding.Default);

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
                        functioninfo.Gid = xnl0.Item(0).InnerText;
                        functioninfo.Pgid = xnl0.Item(1).InnerText;
                        functioninfo.Name = xnl0.Item(2).InnerText;
                        functioninfo.DllPath = xnl0.Item(3).InnerText;
                        functioninfo.SystemtypeId = xnl0.Item(4).InnerText;
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
            /*richText.Text += e.UserState.ToString();
            richText.Document.CaretPosition = richText.Document.Range.End;
            richText.ScrollToCaret();

            if (e.UserState.ToString().Contains("处理完成") && progressBar != null)
            {
                progressBar.Position += 1;
            }*/
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            List<object> args = new List<object>();
            args.Clear();
            args.Add("MenuSql");
            this.backgroundWorker1.RunWorkerAsync(args);
        }

        private void btnFunction_Click(object sender, EventArgs e)
        {
            List<object> args = new List<object>();
            args.Clear();
            args.Add("FunctionSql");
            this.backgroundWorker1.RunWorkerAsync(args);
        }

        private void btnEntity_Click(object sender, EventArgs e)
        {
            (new FrmBasicEntity()).Show();
        }

        private void btnTables_Click(object sender, EventArgs e)
        {
            (new FrmTables()).Show();
        }

        private void btnBasicData_Click(object sender, EventArgs e)
        {
            (new FrmBaseDataManage()).Show();
        }

        private void btnCShortGeneral_Click(object sender, EventArgs e)
        {
            General_EntityDiyField();

            MessageDxUtil.ShowTips("完成");
        }

        private void General_EntityDiyField()
        {
            // 获取实体类生成路径
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
            string filePath = xnl0project.Item(9).InnerText;

            #endregion

            #region 先读取datatype.xml 在读取defaulttype.xml 然后Linq 查询保存到数据字典dic中
            // 写死dbtype 为CShort
            string dbType = "CShort";
            XmlHelper xmldatatypehelper = new XmlHelper(@"XML\datatype.xml");
            XmlNodeList xmldatatypeNodeLst = xmldatatypehelper.Read("datatype");
            List<DataTypeInfo> dataTypeInfoList = new List<DataTypeInfo>();
            foreach (XmlNode xn1 in xmldatatypeNodeLst)
            {
                DataTypeInfo dataTypeInfo = new DataTypeInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值
                dataTypeInfo.Gid = xe.GetAttribute("gid").ToString();

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
                    else if (dbType == "CShort")
                        value = xnl0.Item(8).InnerText;

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

            // 在读取字段保存到数据字典中
            XmlHelper stdfieldxmlHelper = new XmlHelper(@"XML\stdfield.xml");
            XmlNodeList stdfieldxmlNodeLst = stdfieldxmlHelper.Read("datatype/dataitem");
            Dictionary<string, StdFieldInfo> stdfielddict = new Dictionary<string, StdFieldInfo>();
            foreach (var xn1 in stdfieldxmlNodeLst)
            {
                StdFieldInfo stdfield = new StdFieldInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                stdfield.Name = xnl0.Item(0).InnerText;
                stdfield.ChineseName = xnl0.Item(1).InnerText;
                stdfield.DataType = xnl0.Item(2).InnerText;
                stdfield.DictNo = xnl0.Item(3).InnerText.ToInt32();
                if (dictTypeInfoList != null)
                {
                    var dictType = dictTypeInfoList.Find(new Predicate<DictInfo>(dictinfo => dictinfo.Id == stdfield.DictNo));
                    if (dictType != null) stdfield.DictNameLst = dictType.Remark;
                }
                // 用来保存CShort 对应的值
                stdfield.Remark = dict[stdfield.DataType];

                stdfielddict.Add(stdfield.Name, stdfield);
            }
            #endregion

            // 先读取全部字段的数据保存到List中
            List<TableFieldEntity> fieldLst = new List<TableFieldEntity>();
            XmlHelper xmltablefieldhelper = new XmlHelper(@"XML\entity.xml");
            XmlNodeList xmltablefieldNodeLst = xmltablefieldhelper.Read("datatype/dataitem");

            string functionId = string.Empty;
            string name = string.Empty;
            string chineseName = string.Empty;
            string classnamespace = string.Empty;
            string version = string.Empty;
            string folder = string.Empty;
            string baseclass = string.Empty;
            string lasttime = string.Empty;
            string remark = string.Empty;
            

            foreach (XmlNode xn1 in xmltablefieldNodeLst)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                XmlNodeList xnl0 = xe.ChildNodes;
                string entityfilepath = xnl0.Item(3).InnerText;
                XmlHelper oneentityxmlhelper = new XmlHelper(entityfilepath);
                XmlNodeList xmlentityNodeLst = oneentityxmlhelper.Read("datatype/basicinfo");

                string filepath = string.Empty;
                StringBuilder sbcontent = new StringBuilder();
                StringBuilder sboropertys = new StringBuilder();
                StringBuilder sbfields = new StringBuilder();
                sboropertys.Append("\r\n\t\t#region Property Members\r\n");
                sbfields.Append("\r\n\t\t#region Field Members\r\n");

                foreach (XmlNode xn2 in xmlentityNodeLst)
                {
                    XmlElement xe2 = (XmlElement)xn2;
                    XmlNodeList xn20 = xe2.ChildNodes;

                    functionId = xn20.Item(0).InnerText;
                    name = xn20.Item(1).InnerText;
                    chineseName = xn20.Item(2).InnerText;
                    classnamespace = xn20.Item(3).InnerText;
                    version = xn20.Item(4).InnerText;
                    folder = xn20.Item(5).InnerText;
                    baseclass = xn20.Item(6).InnerText;
                    lasttime = xn20.Item(7).InnerText;
                    remark = xn20.Item(8).InnerText;

                    if (!string.IsNullOrEmpty(folder) && !DirectoryUtil.IsExistDirectory(string.Format("{0}//Entity//{1}//", filePath, folder)))
                    {
                        DirectoryUtil.CreateDirectory(string.Format(string.Format("{0}//Entity//{1}//", filePath, folder)));
                    }

                    filepath = string.Format("{0}//Entity//{1}//{2}.cs", filePath, folder, name);

                    if (FileUtil.IsExistFile(filepath))
                        FileUtil.DeleteFile(filepath);

                    FileUtil.CreateFile(filepath);

                    // 加载头文件信息
                    sbcontent.Append("using System;\r\n");
                    sbcontent.Append("using System.Runtime.Serialization;\r\n");
                    sbcontent.Append("using System.ComponentModel;\r\n");

                    // 额外的命名空间
                    if (!string.IsNullOrEmpty(classnamespace))
                    {
                        sbcontent.Append(classnamespace);
                    }

                    sbcontent.Append("\r\n");
                    sbcontent.Append("namespace JCodes.Framework.Entity\r\n");
                    sbcontent.Append("{\r\n");
                    sbcontent.Append("\t/// <summary>\r\n");
                    sbcontent.Append(string.Format("\t/// {0}({1})\r\n", chineseName, name));
                    sbcontent.Append(string.Format("\t/// 对象号: {0}\r\n", functionId));
                    sbcontent.Append(string.Format("\t/// 备注信息: {0}\r\n", remark));
                    sbcontent.Append("\t/// </summary>\r\n");
                    sbcontent.Append("\t[Serializable]\r\n");
                    sbcontent.Append("\t[DataContract]\r\n");

                    sbcontent.Append(string.Format("\tpublic partial class {0}", name));
                    // 如果未空则不需要继承
                    if (!string.IsNullOrEmpty(baseclass))
                    {
                        sbcontent.Append(" : ");
                        sbcontent.Append(baseclass.Replace("&lt;", "<").Replace( "&gt;", ">"));
                    }
                    sbcontent.Append("\r\n\t{");
                }

                xmlentityNodeLst = oneentityxmlhelper.Read("datatype/fieldsinfo");
                foreach (XmlNode xn2 in xmlentityNodeLst)
                {
                    // 得到DataTypeInfo节点的所有子节点
                    XmlNodeList xn20 = xn2.ChildNodes;
                    string fieldname = xn20.Item(0).InnerText;
                    string fieldremark = xn20.Item(1).InnerText;

                    #region Field Members
                    sbfields.Append("\r\n");
                    sbfields.Append("\t\t/// <summary>\r\n");
                    sbfields.Append(string.Format("\t\t/// {0}\r\n", stdfielddict[fieldname].ChineseName));
                    sbfields.Append("\t\t/// </summary>\r\n");
                    if (stdfielddict[fieldname].Remark.Contains("Int"))
                    {
                        sbfields.Append(string.Format("\t\tprivate {0} m_{1} = 0;\r\n", stdfielddict[fieldname].Remark, fieldname));
                    }
                    else if (stdfielddict[fieldname].Remark.Equals("String"))
                    {
                        sbfields.Append(string.Format("\t\tprivate {0} m_{1} = string.Empty;\r\n", stdfielddict[fieldname].Remark, fieldname));
                    }
                    else if (stdfielddict[fieldname].Remark.Equals("DateTime"))
                    {
                        sbfields.Append(string.Format("\t\tprivate {0} m_{1} = DateTime.Now;\r\n", stdfielddict[fieldname].Remark, fieldname));
                    }
                    else if (stdfielddict[fieldname].Remark.Equals("Double"))
                    {
                        sbfields.Append(string.Format("\t\tprivate {0} m_{1} = 0.0;\r\n", stdfielddict[fieldname].Remark, fieldname));
                    }
                    #endregion

                    #region Property Members
                    sboropertys.Append("\r\n");
                    sboropertys.Append("\t\t/// <summary>\r\n");
                    sboropertys.Append(string.Format("\t\t/// {0}\r\n", stdfielddict[fieldname].ChineseName));
                    if (!string.IsNullOrEmpty(fieldremark))
                    {
                        sboropertys.Append(string.Format("\t\t/// {0}\r\n", fieldremark));
                    }
                    sboropertys.Append("\t\t/// </summary>\r\n");
                    sboropertys.Append("\t\t[DataMember]\r\n");
                    sboropertys.Append(string.Format("\t\t[DisplayName(\"{0}\")]\r\n", stdfielddict[fieldname].ChineseName));
                    sboropertys.Append(string.Format("\t\tpublic virtual {0} {1}\r\n", stdfielddict[fieldname].Remark, fieldname));
                    sboropertys.Append("\t\t{\r\n");
                    sboropertys.Append("\t\t\tget\r\n");
                    sboropertys.Append("\t\t\t{\r\n");
                    sboropertys.Append(string.Format("\t\t\t\treturn this.m_{0};\r\n", fieldname));
                    sboropertys.Append("\t\t\t}\r\n");
                    sboropertys.Append("\t\t\tset\r\n");
                    sboropertys.Append("\t\t\t{\r\n");
                    sboropertys.Append(string.Format("\t\t\t\tthis.m_{0} = value;\r\n", fieldname));
                    sboropertys.Append("\t\t\t}\r\n");
                    sboropertys.Append("\t\t}\r\n");
                    #endregion
                }

                xmlentityNodeLst = oneentityxmlhelper.Read("datatype/diyfieldinfo");
                foreach (XmlNode xn2 in xmlentityNodeLst)
                {
                    // 得到DataTypeInfo节点的所有子节点
                    XmlNodeList xn20 = xn2.ChildNodes;
                    string fieldname = xn20.Item(0).InnerText;
                    string chinesename = xn20.Item(1).InnerText;
                    string fieldtype = xn20.Item(2).InnerText;
                    string fieldcontent = xn20.Item(3).InnerText;
                    string fieldremark = xn20.Item(4).InnerText;

                    #region Field Members
                   

                    // 如果字段为空 则不记录
                    if (!string.IsNullOrEmpty(fieldname) && !fieldname.Contains("(")){
                        sbfields.Append("\r\n");
                        sbfields.Append("\t\t/// <summary>\r\n");
                        sbfields.Append(string.Format("\t\t/// {0}\r\n", chinesename));
                        sbfields.Append("\t\t/// </summary>\r\n");
                        if (fieldtype.Contains("List<"))
                        {
                            sbfields.Append(string.Format("\t\tprivate {0} m_{1} = new {0}();\r\n", fieldtype, fieldname));
                        }
                        else if (fieldtype.Contains("Int"))
                        {
                            sbfields.Append(string.Format("\t\tprivate {0} m_{1} = 0;\r\n", fieldtype, fieldname));
                        }
                        else if (fieldtype.Equals("String"))
                        {
                            sbfields.Append(string.Format("\t\tprivate {0} m_{1} = string.Empty;\r\n", fieldtype, fieldname));
                        }
                        else if (fieldtype.Equals("DateTime"))
                        {
                            sbfields.Append(string.Format("\t\tprivate {0} m_{1} = DateTime.Now;\r\n", fieldtype, fieldname));
                        }
                        else
                        {
                            sbfields.Append(string.Format("\t\tprivate {0} m_{1};\r\n", fieldtype, fieldname));
                        }
                    }
                    #endregion

                    #region Property Members
                    sboropertys.Append("\r\n");

                    if (!string.IsNullOrEmpty(fieldname) && !fieldname.Contains("(")) {
                        sboropertys.Append("\t\t/// <summary>\r\n");
                        sboropertys.Append(string.Format("\t\t/// {0}\r\n", chinesename));
                        if (!string.IsNullOrEmpty(fieldremark))
                        {
                            sboropertys.Append(string.Format("\t\t/// {0}\r\n", fieldremark));
                        }
                        sboropertys.Append("\t\t/// </summary>\r\n");

                        sboropertys.Append("\t\t[DataMember]\r\n");
                        sboropertys.Append(string.Format("\t\tpublic virtual {0} {1}\r\n", fieldtype, fieldname));
                        sboropertys.Append("\t\t{\r\n");
                        sboropertys.Append("\t\t\tget\r\n");
                        sboropertys.Append("\t\t\t{\r\n");
                        sboropertys.Append(string.Format("\t\t\t\treturn this.m_{0};\r\n", fieldname));
                        sboropertys.Append("\t\t\t}\r\n");
                        sboropertys.Append("\t\t\tset\r\n");
                        sboropertys.Append("\t\t\t{\r\n");
                        sboropertys.Append(string.Format("\t\t\t\tthis.m_{0} = value;\r\n", fieldname));
                        sboropertys.Append("\t\t\t}\r\n");
                        sboropertys.Append("\t\t}\r\n");
                    }
                    else if (!string.IsNullOrEmpty(fieldname) && fieldname.Contains("(") && (fieldname.Equals("IDXDataErrorInfo.GetPropertyError(string propertyName, ErrorInfo info)") || fieldname.Equals("IDXDataErrorInfo.GetError(ErrorInfo info)")))
                    {
                        sboropertys.Append("\t\t/// <summary>\r\n");
                        sboropertys.Append(string.Format("\t\t/// {0}\r\n", chinesename));
                        if (!string.IsNullOrEmpty(fieldremark))
                        {
                            sboropertys.Append(string.Format("\t\t/// {0}\r\n", fieldremark));
                        }
                        sboropertys.Append("\t\t/// </summary>\r\n");

                        sboropertys.Append(string.Format("\t\t{0} {1}\r\n", fieldtype, fieldname));
                        sboropertys.Append("\t\t{\r\n");
                        sboropertys.Append("\t\t\t");
                        sboropertys.Append(fieldcontent);
                        sboropertys.Append("\n\t\t}\r\n");
                    }
                    else if (!string.IsNullOrEmpty(fieldname) && fieldname.Contains("("))
                    {
                        sboropertys.Append("\t\t/// <summary>\r\n");
                        sboropertys.Append(string.Format("\t\t/// {0}\r\n", chinesename));
                        if (!string.IsNullOrEmpty(fieldremark))
                        {
                            sboropertys.Append(string.Format("\t\t/// {0}\r\n", fieldremark));
                        }
                        sboropertys.Append("\t\t/// </summary>\r\n");

                        sboropertys.Append(string.Format("\t\tpublic {0} {1}\r\n", fieldtype, fieldname));
                        sboropertys.Append("\t\t{\r\n");
                        sboropertys.Append("\t\t\t");
                        sboropertys.Append(fieldcontent);
                        sboropertys.Append("\n\t\t}\r\n");
                    }
                   
                    #endregion
                }
                sboropertys.Append("\t\t#endregion\r\n");
                sbfields.Append("\t\t#endregion\r\n");

                FileUtil.AppendText(filepath, sbcontent.ToString() + sbfields.ToString() + sboropertys.ToString() + "\t}\r\n}", Encoding.UTF8);
            }
        }

        private void General_TableField() {
            // 获取实体类生成路径
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

            string filePath = xnl0project.Item(9).InnerText;
            #endregion

            #region 先读取datatype.xml 在读取defaulttype.xml 然后Linq 查询保存到数据字典dic中
            // 写死dbtype 为CShort
            string dbType = "CShort";
            XmlHelper xmldatatypehelper = new XmlHelper(@"XML\datatype.xml");
            XmlNodeList xmldatatypeNodeLst = xmldatatypehelper.Read("datatype");
            List<DataTypeInfo> dataTypeInfoList = new List<DataTypeInfo>();
            foreach (XmlNode xn1 in xmldatatypeNodeLst)
            {
                DataTypeInfo dataTypeInfo = new DataTypeInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值
                dataTypeInfo.Gid = xe.GetAttribute("gid").ToString();

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
                    else if (dbType == "CShort")
                        value = xnl0.Item(8).InnerText;

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

            // 在读取字段保存到数据字典中
            XmlHelper stdfieldxmlHelper = new XmlHelper(@"XML\stdfield.xml");
            XmlNodeList stdfieldxmlNodeLst = stdfieldxmlHelper.Read("datatype/dataitem");
            Dictionary<string, StdFieldInfo> stdfielddict = new Dictionary<string, StdFieldInfo>();
            foreach (var xn1 in stdfieldxmlNodeLst)
            {
                StdFieldInfo stdfield = new StdFieldInfo();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                stdfield.Name = xnl0.Item(0).InnerText;
                stdfield.ChineseName = xnl0.Item(1).InnerText;
                stdfield.DataType = xnl0.Item(2).InnerText;
                stdfield.DictNo = xnl0.Item(3).InnerText.ToInt32();
                if (dictTypeInfoList != null)
                {
                    var dictType = dictTypeInfoList.Find(new Predicate<DictInfo>(dictinfo => dictinfo.Id == stdfield.DictNo));
                    if (dictType != null) stdfield.DictNameLst = dictType.Remark;
                }
                // 用来保存CShort 对应的值
                stdfield.Remark = dict[stdfield.DataType];

                stdfielddict.Add(stdfield.Name, stdfield);
            }
            #endregion

            if (DirectoryUtil.IsExistDirectory(string.Format("{0}//Entity//", filePath)))
            {
                DirectoryUtil.DeleteDirectory(string.Format("{0}//Entity//", filePath));
            }

            DirectoryUtil.CreateDirectory(string.Format("{0}//Entity//", filePath));

            // 先读取全部字段的数据保存到List中
            List<TableFieldEntity> fieldLst = new List<TableFieldEntity>();
            XmlHelper xmltablefieldhelper = new XmlHelper(@"XML\entity.xml");
            XmlNodeList xmltablefieldNodeLst = xmltablefieldhelper.Read("datatype/dataitem");
            foreach (XmlNode xn1 in xmltablefieldNodeLst)
            {

                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                XmlNodeList xnl0 = xe.ChildNodes;
                //xnl0.Item(0).InnerText; name
                //xnl0.Item(1).InnerText; chineseName
                //xnl0.Item(2).InnerText; functionId
                string diyfilePath = xnl0.Item(3).InnerText;
                // 文件存在
                if (FileUtil.FileIsExist(diyfilePath))
                {
                    /*#region 开头处理
                    if (!string.IsNullOrEmpty(basicEntity.FolderName) && !DirectoryUtil.IsExistDirectory(string.Format("{0}//Entity{1}//", filePath, string.Format("//{0}", basicEntity.FolderName))))
                    {
                        DirectoryUtil.CreateDirectory(string.Format("{0}//Entity{1}//", filePath, string.Format("//{0}", basicEntity.FolderName)));
                    }

                    string filepath = string.Format("{0}//Entity{2}//{1}.cs", filePath, basicEntity.TableName, string.Format("//{0}", basicEntity.FolderName));

                    if (FileUtil.IsExistFile(filepath))
                        FileUtil.DeleteFile(filepath);

                    FileUtil.CreateFile(filepath);

                    StringBuilder sbheader = new StringBuilder();
                    // 加载头文件信息
                    sbheader.Append("using System;\r\n");
                    sbheader.Append("using System.Runtime.Serialization;\r\n");
                    string inheritstr = string.Empty;
                    if (basicEntity.IsIDXDataError == Const.Num_One)
                    {
                        if (string.IsNullOrEmpty(inheritstr))
                        {
                            inheritstr = " : IDXDataErrorInfo";
                            sbheader.Append("using DevExpress.XtraEditors.DXErrorProvider;\r\n");
                        }
                        else
                        {
                            inheritstr = " , IDXDataErrorInfo";
                            sbheader.Append("using DevExpress.XtraEditors.DXErrorProvider;\r\n");
                        }
                    }

                    if (basicEntity.IsBaseEntity == Const.Num_One)
                    {
                        if (string.IsNullOrEmpty(inheritstr))
                        {
                            inheritstr = " : BaseEntity";
                            sbheader.Append("using JCodes.Framework.Entity;\r\n");
                        }
                        else
                        {
                            inheritstr = " , BaseEntity";
                            sbheader.Append("using JCodes.Framework.Entity;\r\n");
                        }
                    }

                    if (!string.IsNullOrEmpty(basicEntity.CustomParentClass))
                    {
                        if (string.IsNullOrEmpty(basicEntity.CustomParentClass))
                        {
                            inheritstr = " : " + basicEntity.CustomParentClass;
                        }
                        else
                        {
                            inheritstr = " , " + basicEntity.CustomParentClass;
                        }
                    }

                    if (!string.IsNullOrEmpty(basicEntity.CustomNamespace))
                    {
                        sbheader.Append(basicEntity.CustomNamespace);
                    }
                    sbheader.Append("\r\n");
                    sbheader.Append("namespace JCodes.Framework.Entity\r\n");
                    sbheader.Append("{\r\n");
                    sbheader.Append("\t/// <summary>\r\n");
                    sbheader.Append(string.Format("\t/// {0}\r\n", basicEntity.Remark));
                    sbheader.Append("\t/// </summary>\r\n");
                    sbheader.Append("\t[Serializable]\r\n");
                    sbheader.Append("\t[DataContract]\r\n");


                    basicEntity.IsIDXDataError = Convert.ToInt32(xe.Attributes["isidxdataerror"].Value);
                    basicEntity.IsBaseEntity = Convert.ToInt32(xe.Attributes["isbaseentity"].Value);
                    sbheader.Append(string.Format("\tpublic partial class {0} : BaseEntity \r\n", basicEntity.TableName));
                    sbheader.Append("\t{");

                    // 遍历全部字段
                    var fields = fieldLst.FindAll(n => n.TableName == basicEntity.TableName).OrderBy(n => n.Seq);
                    StringBuilder sboropertys = new StringBuilder();
                    StringBuilder sbfields = new StringBuilder();
                    sboropertys.Append("\r\n\t\t#region Property Members\r\n");
                    sbfields.Append("\r\n\t\t#region Field Members\r\n");

                    if (!string.IsNullOrEmpty(basicEntity.ConstructContent))
                    {
                        sboropertys.Append("\t\t/// <summary>\r\n");
                        sboropertys.Append("\t\t/// 构造函数\r\n");
                        sboropertys.Append("\t\t/// </summary>\r\n");
                        sboropertys.Append(string.Format("\t\tpublic {0}{1}\r\n", basicEntity.TableName, basicEntity.ConstructContent.Replace("\\t", "\t").Replace("\\r\\n", "\r\n")));
                    }
                    #endregion*/
                   
               
                    XmlHelper xmldiytablefieldhelper = new XmlHelper(diyfilePath);
                    XmlNodeList xmlfieldNodeLst = xmldiytablefieldhelper.Read("datatype/fieldsinfo");




                    XmlNodeList xmldiyfieldNodeLst = xmldiytablefieldhelper.Read("datatype/diyfieldinfo");
                }
            }
            // 取出数据
            /*XmlHelper xmlhelper = new XmlHelper(@"XML\EntityTable.basicdata");
            XmlNodeList xmlNodeLst = xmlhelper.Read("datatype/dataitem");

            foreach (XmlNode xn1 in xmlNodeLst)
            {
                TableEntity basicEntity = new TableEntity();

                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                basicEntity.TableName = xe.Attributes["tablename"].Value;
                basicEntity.Remark = xe.Attributes["remark"].Value;
                basicEntity.FolderName = xe.Attributes["foldername"].Value;
                basicEntity.IsIDXDataError = Convert.ToInt32(xe.Attributes["isidxdataerror"].Value);
                basicEntity.IsBaseEntity = Convert.ToInt32(xe.Attributes["isbaseentity"].Value);
                basicEntity.CustomParentClass = xe.Attributes["customparentclass"].Value;
                basicEntity.ToStringContent = xe.Attributes["tostringcontent"].Value;
                basicEntity.ConstructContent = xe.Attributes["constructcontent"].Value;
                basicEntity.CustomContent = xe.Attributes["customcontent"].Value;
                basicEntity.CustomNamespace = xe.Attributes["customnamespace"].Value;

               

                foreach (var field in fields)
                {
                    #region Field Members
                    sbfields.Append("\r\n");
                    sbfields.Append("\t\t/// <summary>\r\n");
                    sbfields.Append(string.Format("\t\t/// {0}\r\n", stdfielddict[field.TableField].ChineseName));
                    sbfields.Append("\t\t/// </summary>\r\n");
                    if (stdfielddict[field.TableField].Remark.Contains("Int"))
                    {
                        sbfields.Append(string.Format("\t\tprivate {0} m_{1} = 0;\r\n", stdfielddict[field.TableField].Remark, field.TableField));
                    }
                    else if (stdfielddict[field.TableField].Remark.Equals("String"))
                    {
                        sbfields.Append(string.Format("\t\tprivate {0} m_{1} = string.Empty;\r\n", stdfielddict[field.TableField].Remark, field.TableField));
                    }
                    else if (stdfielddict[field.TableField].Remark.Equals("DateTime"))
                    {
                        sbfields.Append(string.Format("\t\tprivate {0} m_{1} = DateTime.Now;\r\n", stdfielddict[field.TableField].Remark, field.TableField));
                    }


                    #endregion

                    #region Property Members
                    sboropertys.Append("\r\n");
                    sboropertys.Append("\t\t/// <summary>\r\n");
                    sboropertys.Append(string.Format("\t\t/// {0}\r\n", stdfielddict[field.TableField].ChineseName));
                    sboropertys.Append("\t\t/// </summary>\r\n");
                    sboropertys.Append("\t\t[DataMember]\r\n");
                    sboropertys.Append(string.Format("\t\tpublic virtual {0} {1}\r\n", stdfielddict[field.TableField].Remark, field.TableField));
                    sboropertys.Append("\t\t{\r\n");
                    sboropertys.Append("\t\t\tget\r\n");
                    sboropertys.Append("\t\t\t{\r\n");
                    sboropertys.Append(string.Format("\t\t\t\treturn this.m_{0};\r\n", field.TableField));
                    sboropertys.Append("\t\t\t}\r\n");
                    sboropertys.Append("\t\t\tset\r\n");
                    sboropertys.Append("\t\t\t{\r\n");
                    sboropertys.Append(string.Format("\t\t\t\tthis.m_{0} = value;\r\n", field.TableField));
                    sboropertys.Append("\t\t\t}\r\n");
                    sboropertys.Append("\t\t}\r\n");
                    #endregion
                }

                // 新增自定义部分
                if (!string.IsNullOrEmpty(basicEntity.ToStringContent))
                {
                    sboropertys.Append("\r\n");
                    sboropertys.Append("\t\t/// <summary>\r\n");
                    sboropertys.Append("\t\t/// 显示内容\r\n");
                    sboropertys.Append("\t\t/// </summary>\r\n");
                    sboropertys.Append("\t\tpublic override string ToString()");
                    sboropertys.Append("\t\t{\r\n");
                    sboropertys.Append(string.Format("\t\t{0}\r\n", basicEntity.ToStringContent));
                    sboropertys.Append("\t\t}\r\n");
                }
                if (!string.IsNullOrEmpty(basicEntity.CustomContent))
                {
                    sboropertys.Append("\r\n");
                    sboropertys.Append(string.Format("\t\t{0}", basicEntity.CustomContent.Replace("\\t", "\t").Replace("\\r\\n", "\r\n")));
                }

                sboropertys.Append("\t\t#endregion\r\n");
                sbfields.Append("\t\t#endregion\r\n");

                FileUtil.AppendText(filepath, sbheader.ToString() + sbfields.ToString() + sboropertys.ToString() + "\t}\r\n}", Encoding.UTF8);
            }*/
        }

        private void btnDefaultType_Click(object sender, EventArgs e)
        {
            (new FrmDefaultType()).Show();
        }

        private void btnStdField_Click(object sender, EventArgs e)
        {
            (new FrmStdField()).Show();
        }

        private void btnDic_Click(object sender, EventArgs e)
        {
            this.LoginUserInfo = new LoginUserInfo();
            LoginUserInfo.Id = 1;
            LoginUserInfo.Name = "吴建明";
            LoginUserInfo.DeptId = 1;
            LoginUserInfo.CompanyId = 1;
            (new FrmDictionary()).Show();
        }

        private void btnConvertText_Click(object sender, EventArgs e)
        {
            (new ConvertTextFrm()).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Int32 result = textBox1.Text.ToInt32();
            Console.WriteLine("result:" + result);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new FrmErrorno()).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (new FrmConstant()).Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            (new FrmDataType()).Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            (new FrmDefaultType()).Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            (new FrmDictionary()).Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            (new FrmGeneralSql()).Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            (new FrmProj()).Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            (new FrmSysFunction()).Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            (new FrmSysMenu()).Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            (new FrmClassEntity()).Show();
        }
    }
}
