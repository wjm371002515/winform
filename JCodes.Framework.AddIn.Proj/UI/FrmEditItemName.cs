using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Xml;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.Common.Format;
using System.Text;

namespace JCodes.Framework.AddIn.Proj
{
    public partial class FrmEditItemName : BaseEditForm
    {
        public string strItemName = string.Empty;

        public string strFunction = string.Empty;

        public string strChineseName = string.Empty;

        public string strGuid = string.Empty;

        private string tablesModel = "<name>{0}</name><chineseName>{1}</chineseName><functionId>{2}</functionId><typeguid>{3}</typeguid><path>{4}</path>";

        private string tablesDetailModel = "<?xml version=\"1.0\" encoding=\"utf-8\"?><datatype><histories></histories><basicinfo><item guid=\"{0}\"><functionId>{1}</functionId><name>{2}</name><chineseName>{3}</chineseName><existhistable>{4}</existhistable><dbtype>{5}</dbtype><version>{6}</version><lasttime>{7}</lasttime><remark>{8}</remark></item></basicinfo><fieldsinfo></fieldsinfo><indexsinfo></indexsinfo></datatype>";

        /// <summary>
        /// 临时保存Name的值，用于修改英文名后重命名table文件
        /// </summary>
        private string tmpName = string.Empty;

        public FrmEditItemName()
        {
            InitializeComponent();
        }

        public override bool CheckInput()
        {
            bool result = true;//默认是可以通过
            #region MyRegion
            if (this.txtTableName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblTableName.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtTableName.Focus();
                result = false;
            }

            if (this.txtChineseName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblChineseName.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtChineseName.Focus();
                result = false;
            }

            if (this.txtFunctionId.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblFunctionId.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtFunctionId.Focus();
                result = false;
            }

            if ((cbbTypeGuid.SelectedItem as CListItem).Value.Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblTypeGuid.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtFunctionId.Focus();
                result = false;
            }

            // 检查新增的表是否已经存在
            if (string.IsNullOrEmpty(ID))
            {
                XmlHelper xmltableshelper = new XmlHelper(@"XML\tables.xml");
                XmlNodeList xmlNodeLst = xmltableshelper.Read(string.Format("datatype/dataitem"));
                foreach (XmlNode xn1 in xmlNodeLst)
                {
                    // 将节点转换为元素，便于得到节点的属性值
                    XmlElement xe = (XmlElement)xn1;

                    // 得到DataTypeInfo节点的所有子节点
                    XmlNodeList xnl0 = xe.ChildNodes;

                    if (string.Equals(xnl0.Item(0).InnerText, txtTableName.Text.Trim()))
                    {
                        MessageDxUtil.ShowWarning(Const.MsgIsExistMsg + lblTableName.Text.Replace(Const.MsgCheckSign, string.Empty));
                        this.txtTableName.Focus();
                        result = false;
                    }

                    if (string.Equals(xnl0.Item(1).InnerText, txtChineseName.Text.Trim()))
                    {
                        MessageDxUtil.ShowWarning(Const.MsgIsExistMsg + lblChineseName.Text.Replace(Const.MsgCheckSign, string.Empty));
                        this.txtChineseName.Focus();
                        result = false;
                    }

                    if (string.Equals(xnl0.Item(2).InnerText, txtFunctionId.Text.Trim()))
                    {
                        MessageDxUtil.ShowWarning(Const.MsgIsExistMsg + lblFunctionId.Text.Replace(Const.MsgCheckSign, string.Empty));
                        this.txtFunctionId.Focus();
                        result = false;
                    }
                }
            }
            #endregion

            return result;
        }
        public override void DisplayData()
        {
            btnAdd.Visible = false;

            // 绑定下拉框的值
            List<CListItem> typeGuidList = new List<CListItem>();
            XmlHelper xmltableshelper = new XmlHelper(@"XML\tables.xml");
            XmlNodeList xmlNodeLst2 = xmltableshelper.Read(string.Format("datatype/tabletype"));
            foreach (XmlNode xn1 in xmlNodeLst2)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                typeGuidList.Add(new CListItem(xe.Attributes["guid"].Value, xe.Attributes["name"].Value));
            }
            cbbTypeGuid.BindDictItems(typeGuidList);

            if (!string.IsNullOrEmpty(ID))
            {
                txtGUID.Text = ID;

                XmlNodeList xmlNodeLst = xmltableshelper.Read(string.Format("datatype/dataitem"));
                foreach (XmlNode xn1 in xmlNodeLst)
                {
                    // 将节点转换为元素，便于得到节点的属性值
                    XmlElement xe = (XmlElement)xn1;

                    // 得到DataTypeInfo节点的所有子节点
                    XmlNodeList xnl0 = xe.ChildNodes;

                    if (ID == xe.Attributes["guid"].Value)
                    {
                        txtTableName.Text = xnl0.Item(0).InnerText;
                        txtChineseName.Text = xnl0.Item(1).InnerText;
                        txtFunctionId.Text = xnl0.Item(2).InnerText;
                        cbbTypeGuid.SetComboBoxItem(xnl0.Item(3).InnerText);

                        // 临时保存文件名
                        tmpName = txtTableName.Text;
                    }
                }
            }
            else
            {
                txtGUID.Text = Guid.NewGuid().ToString();
                cbbTypeGuid.SetComboBoxItem(strGuid);
            }
            this.txtTableName.Focus();
        }

        public override void ClearScreen()
        {
            txtGUID.Text = string.Empty;
            txtTableName.Text = string.Empty;
            txtChineseName.Text = string.Empty;
            txtFunctionId.Text = string.Empty;
            cbbTypeGuid.SelectedItem = -1;
            base.ClearScreen();
        }

        public override bool SaveAddNew()
        {
            TablesInfo info = new TablesInfo();

            SetInfo(info);

            try
            {
                #region 新增数据
                // 获取数据库数据类型
                XmlHelper xmlprojhelper = new XmlHelper(@"XML\project.xml");
                XmlNodeList xmlprejectNodeLst = xmlprojhelper.Read("datatype");

                if (xmlprejectNodeLst.Count == 0)
                    return false;

                XmlNode xn1project = xmlprejectNodeLst[0];
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xeproject = (XmlElement)xn1project;

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0project = xeproject.ChildNodes;
               
                string DbType = xnl0project.Item(4).InnerText;

                XmlHelper xmltableshelper = new XmlHelper(@"XML\tables.xml");
                xmltableshelper.InsertElement("datatype/dataitem", "item", "guid", info.GUID, string.Format(tablesModel, info.Name, info.ChineseName, info.FunctionId, info.TypeGuid, info.Path));

                xmltableshelper.Save();

                // 新增表名.table文件
                FileUtil.AppendText(string.Format(@"XML\{0}.table", info.Name), string.Format(tablesDetailModel, System.Guid.NewGuid(), info.FunctionId, info.Name, info.ChineseName, Const.Zero, DbType, "1.0.0.0", DateTimeHelper.GetServerDateTime(), string.Empty), Encoding.UTF8);

                strItemName = info.Name;
                strFunction = info.FunctionId;
                strChineseName = info.ChineseName;
                ID = info.GUID;
                return true;
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditItemName));
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }

        public override bool SaveUpdated()
        {
            TablesInfo info = new TablesInfo();
            if (info != null)
            {
                SetInfo(info);
                #region 更新数据
                // 获取数据库数据类型
                XmlHelper xmltableshelper = new XmlHelper(@"XML\tables.xml");
                // 更新操作
                xmltableshelper.Replace(string.Format("datatype/dataitem/item[@guid=\"{0}\"]/name", ID), info.Name);
                xmltableshelper.Replace(string.Format("datatype/dataitem/item[@guid=\"{0}\"]/chineseName", ID), info.ChineseName);
                xmltableshelper.Replace(string.Format("datatype/dataitem/item[@guid=\"{0}\"]/functionId", ID), info.FunctionId);
                xmltableshelper.Replace(string.Format("datatype/dataitem/item[@guid=\"{0}\"]/typeguid", ID), info.TypeGuid);
                xmltableshelper.Replace(string.Format("datatype/dataitem/item[@guid=\"{0}\"]/path", ID), info.Path);
                xmltableshelper.Save();

                if (!string.Equals(tmpName, info.Name))
                {
                    // 重命名
                    if (FileUtil.IsExistFile(string.Format(@"XML\{0}.table", tmpName)))
                        System.IO.File.Move(string.Format(@"XML\{0}.table", tmpName), string.Format(@"XML\{0}.table", info.Name));
                }

                // 新增表名.table文件
                XmlHelper xmltablesdetailhelper = new XmlHelper(info.Path);
                xmltablesdetailhelper.Replace("datatype/basicinfo/item/functionId", info.FunctionId);
                xmltablesdetailhelper.Replace("datatype/basicinfo/item/name", info.Name);
                xmltablesdetailhelper.Replace("datatype/basicinfo/item/chineseName", info.ChineseName);
                xmltablesdetailhelper.Save();

                strItemName = info.Name;
                strFunction = info.FunctionId;
                strChineseName = info.ChineseName;
                ID = info.GUID;
                return true;
                #endregion
            }
            return false;
        }

        private void SetInfo(TablesInfo info)
        {
            info.GUID = txtGUID.Text;
            info.Name = txtTableName.Text;
            info.ChineseName = txtChineseName.Text;
            info.FunctionId = txtFunctionId.Text;
            info.TypeGuid = (cbbTypeGuid.SelectedItem as CListItem).Value;
            info.Path = string.Format(@"XML\{0}.table", info.Name);
            // 如果是新增 则赋值路径
            if (string.IsNullOrEmpty(ID))
            {
                // 删除table文件
                if (FileUtil.FileIsExist(info.Path))
                {
                    FileUtil.DeleteFile(info.Path);
                }

                FileUtil.CreateFile(info.Path);
            }
        }
    }
}
