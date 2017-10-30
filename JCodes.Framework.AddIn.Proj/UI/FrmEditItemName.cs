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

namespace JCodes.Framework.AddIn.Proj
{
    public partial class FrmEditItemName : BaseEditForm
    {
        public string strItemName = string.Empty;

        public string strFunction = string.Empty;

        public string strChineseName = string.Empty;

        public string strGuid = string.Empty;

        private string tablesModel = "<name>{0}</name><chineseName>{1}</chineseName><functionId>{2}</functionId><typeguid>{3}</typeguid><path>{4}</path>";

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
                        // TODO
                        cbbTypeGuid.SelectedItem = xnl0.Item(3).InnerText;
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
                XmlHelper xmltableshelper = new XmlHelper(@"XML\tables.xml");
                xmltableshelper.InsertElement("datatype/dataitem", "item", "guid", info.GUID, string.Format(tablesModel, info.Name, info.ChineseName, info.FunctionId, info.TypeGuid, info.Path));

                xmltableshelper.Save();
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

                #endregion
                return false;
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
            // 如果是新增 则赋值路径
            if (string.IsNullOrEmpty(ID))
            {
                info.Path = string.Format(@"XML\{0}.table", info.Name);
                // 删除table文件
                if (FileUtil.FileIsExist(info.Path))
                {
                    FileUtil.DeleteFile(info.Path);
                }
            }
        }
    }
}
