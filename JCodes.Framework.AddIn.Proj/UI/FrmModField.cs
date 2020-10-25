using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Network;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace JCodes.Framework.AddIn.Proj
{
    public partial class FrmModField : BaseDock
    {
        private StdFieldInfo _beforemodstdFieldInfo = null;
        private List<StdFieldInfo> _lstDataTypeInfo = null;

        public FrmModField(StdFieldInfo stdFieldInfo, List<StdFieldInfo> lstDataTypeInfo)
        {
            InitializeComponent();

            this._beforemodstdFieldInfo = stdFieldInfo;
            this._lstDataTypeInfo = lstDataTypeInfo;

            InitView();
        }

        private void InitView() 
        {
            this.txtName.Text = _beforemodstdFieldInfo.Name;
            this.txtChineseName.Text = _beforemodstdFieldInfo.ChineseName;

            #region 绑定标准字段
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

            lueFieldType.EditValue = "Value";
            lueFieldType.Properties.ValueMember = "Value";
            lueFieldType.Properties.DisplayMember = "Text";
            lueFieldType.Properties.DataSource = dataTypeInfoList;
            //填充列
            lueFieldType.Properties.PopulateColumns();
            #endregion

            lueFieldType.EditValue = _beforemodstdFieldInfo.DataType;
        }

        private void FrmModField_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.txtName.Dispose();//显式关闭空间，防止错误出现
        }

        private void btnModField_Click(object sender, EventArgs e)
        {
            StdFieldInfo aftermodstdFieldInfo = new StdFieldInfo();
            aftermodstdFieldInfo.Gid = _beforemodstdFieldInfo.Gid;
            aftermodstdFieldInfo.Name = txtName.Text.Trim();
            aftermodstdFieldInfo.ChineseName = txtChineseName.Text.Trim();
            aftermodstdFieldInfo.DataType = lueFieldType.EditValue.ToString();

            StringBuilder content = new StringBuilder();

            #region 检查一下字段是否存在
            foreach (StdFieldInfo dataTypeInfo in _lstDataTypeInfo)
            {
                if (string.IsNullOrEmpty(dataTypeInfo.Gid))
                    continue;

                // 与别的字段重名，不允许修改
                if (string.Equals(aftermodstdFieldInfo.Name, dataTypeInfo.Name) && !string.Equals(_beforemodstdFieldInfo.Name, dataTypeInfo.Name))
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, "该标准字段已存在，不允许修改", typeof(FrmModField));
                    MessageDxUtil.ShowWarning("该标准字段已存在，不允许修改");
                    return;
                }
            }
            #endregion

            #region 取project.xml 判断当前数据库
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
            #endregion

             #region 生成SQL文件
            if (!string.Equals(_beforemodstdFieldInfo.Name, aftermodstdFieldInfo.Name) || !string.Equals(_beforemodstdFieldInfo.DataType, aftermodstdFieldInfo.DataType))
            {
                // SQLServer 修改字段 　　exec sp_rename '表名.列名','新列名' -- 注意，单引号不可省略。
                // SQLServer 修改字段类型 alter table 表名 alter column 字段名 type not null
                // MySql 修改字段 ALTER TABLE  表名 CHANGE  旧字段名 新字段名 字段类型 
                // MySql 修改字段类型 ALTER TABLE  表名 CHANGE  字段名 字段名 新字段类型
                string saveFile = FileDialogHelper.SaveText("更新字段.sql", "C:\\myares\\");
                if (!string.IsNullOrEmpty(saveFile))
                {
                    String[] sqltablefileNames = DirectoryUtil.GetFileNames(@"XML\", "*.table", true);
                    foreach (string tablefileName in sqltablefileNames)
                    {
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("开始变更{0}文件中变更前字段为{1}", tablefileName, _beforemodstdFieldInfo.Name), typeof(FrmModField));
                        XmlHelper xmltablehelper = new XmlHelper(tablefileName);
                        try
                        {
                            XmlNodeList xmlNodeLst = xmltablehelper.Read("datatype/fieldsinfo");
                            for (Int32 i = 0; i < xmlNodeLst.Count; i++)
                            {
                                // 字段变更
                                if (string.Equals(xmlNodeLst[i].ChildNodes.Item(0).InnerText, _beforemodstdFieldInfo.Name) && !string.Equals(_beforemodstdFieldInfo.Name, aftermodstdFieldInfo.Name))
                                {
                                    #region 读取Table.xml 配置信息

                                    XmlHelper xmltableshelper = new XmlHelper(@"XML\tables.xml");
                                    XmlNodeList xmlNodeLst22 = xmltableshelper.Read("datatype/tabletype");
                                    Dictionary<string, string> guidGroup = new Dictionary<string, string>();
                                    Dictionary<string, string> tableGroup = new Dictionary<string, string>();
                                    guidGroup.Clear();
                                    tableGroup.Clear();
                                    foreach (XmlNode xn1 in xmlNodeLst22)
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

                                    string tableName = xmltablehelper.Read("datatype/basicinfo/item/name").Item(0).InnerText;
                                    content.Append(string.Format("exec sp_rename '{0}.{1}','{2}';\r\nGO\r\n\r\n", tableGroup[tableName] + tableName, _beforemodstdFieldInfo.Name, aftermodstdFieldInfo.Name));
                                }

                                if (string.Equals(xmlNodeLst[i].ChildNodes.Item(0).InnerText, _beforemodstdFieldInfo.Name) && !string.Equals(_beforemodstdFieldInfo.DataType, aftermodstdFieldInfo.DataType))
                                {
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
                                    #endregion

                                    #region 读取Table.xml 配置信息
                                   
                                    XmlHelper xmltableshelper = new XmlHelper(@"XML\tables.xml");
                                    XmlNodeList xmlNodeLst22 = xmltableshelper.Read("datatype/tabletype");
                                    Dictionary<string, string> guidGroup = new Dictionary<string, string>();
                                    Dictionary<string, string> tableGroup = new Dictionary<string, string>();
                                    guidGroup.Clear();
                                    tableGroup.Clear();
                                    foreach (XmlNode xn1 in xmlNodeLst22)
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

                                    string tableName = xmltablehelper.Read("datatype/basicinfo/item/name").Item(0).InnerText;
                                    content.Append(string.Format("alter table {0} alter column {1} {2};\r\nGO\r\n\r\n", tableGroup[tableName] + tableName, aftermodstdFieldInfo.Name, dict[aftermodstdFieldInfo.DataType]));
                                }
                            }
                            xmltablehelper.Save(false);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmModField));
                            MessageDxUtil.ShowError(ex.Message);
                        }
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("结束变更{0}文件中变更前字段为{1}", tablefileName, _beforemodstdFieldInfo.Name), typeof(FrmModField));
                    }

                    FileUtil.WriteText(saveFile, content.ToString(), Encoding.UTF8);

                    if (MessageDxUtil.ShowYesNoAndTips("保存成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveFile);
                    }
                }
            }
            #endregion

            #region 修改*.table 字段
            String[] tablefileNames = DirectoryUtil.GetFileNames(@"XML\", "*.table", true);
            foreach (string tablefileName in tablefileNames) {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("开始变更{0}文件中变更前字段为{1}", tablefileName, _beforemodstdFieldInfo.Name), typeof(FrmModField));
                XmlHelper xmltablehelper = new XmlHelper(tablefileName);
                try
                {
                    #region 更新数据
                    if (!string.Equals(_beforemodstdFieldInfo.Name, aftermodstdFieldInfo.Name)) {
                        XmlNodeList xmlNodeLst = xmltablehelper.Read("datatype/fieldsinfo");
                        for (Int32 i = 0; i < xmlNodeLst.Count; i++)
                        {
                            if (string.Equals(xmlNodeLst[i].ChildNodes.Item(0).InnerText, _beforemodstdFieldInfo.Name)) {
                                xmlNodeLst[i].ChildNodes.Item(0).InnerText = aftermodstdFieldInfo.Name;
                            }
                        }  
                    }
                    xmltablehelper.Save(false);
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmModField));
                    MessageDxUtil.ShowError(ex.Message);
                }
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("结束变更{0}文件中变更前字段为{1}", tablefileName, _beforemodstdFieldInfo.Name), typeof(FrmModField));
            }
            #endregion

            #region 修改*.entity 字段
            String[] entityfileNames = DirectoryUtil.GetFileNames(@"XML\", "*.entity", true);
            foreach (string entityfileName in entityfileNames)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("开始变更{0}文件中变更前字段为{1}", entityfileName, _beforemodstdFieldInfo.Name), typeof(FrmModField));
                XmlHelper xmlentityhelper = new XmlHelper(entityfileName);
                try
                {
                    #region 更新数据
                    if (!string.Equals(_beforemodstdFieldInfo.Name, aftermodstdFieldInfo.Name))
                    {
                        XmlNodeList xmlNodeLst = xmlentityhelper.Read("datatype/fieldsinfo");
                        for (Int32 i = 0; i < xmlNodeLst.Count; i++)
                        {
                            if (string.Equals(xmlNodeLst[i].ChildNodes.Item(0).InnerText, _beforemodstdFieldInfo.Name))
                            {
                                xmlNodeLst[i].ChildNodes.Item(0).InnerText = aftermodstdFieldInfo.Name;
                            }
                        }
                    }
                    xmlentityhelper.Save(false);
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmModField));
                    MessageDxUtil.ShowError(ex.Message);
                }
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, string.Format("结束变更{0}文件中变更前字段为{1}", entityfileName, _beforemodstdFieldInfo.Name), typeof(FrmModField));
            }
            #endregion

            #region 修改stdfield.xml文件
            XmlHelper xmlstdfieldhelper = new XmlHelper(@"XML\stdfield.xml");
            try
            {
                #region 更新数据
                if (!string.Equals(_beforemodstdFieldInfo.Name, aftermodstdFieldInfo.Name))
                    xmlstdfieldhelper.Replace(string.Format("datatype/dataitem/item[@gid=\"{0}\"]/name", aftermodstdFieldInfo.Gid), aftermodstdFieldInfo.Name);
                if (!string.Equals(_beforemodstdFieldInfo.ChineseName, aftermodstdFieldInfo.ChineseName))
                    xmlstdfieldhelper.Replace(string.Format("datatype/dataitem/item[@gid=\"{0}\"]/chineseName", aftermodstdFieldInfo.Gid), aftermodstdFieldInfo.ChineseName);
                if (!string.Equals(_beforemodstdFieldInfo.DataType, aftermodstdFieldInfo.DataType))
                    xmlstdfieldhelper.Replace(string.Format("datatype/dataitem/item[@gid=\"{0}\"]/datatype", aftermodstdFieldInfo.Gid), aftermodstdFieldInfo.DataType);
                xmlstdfieldhelper.Save(false);
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmModField));
                MessageDxUtil.ShowError(ex.Message);
            }
            #endregion

           

            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
