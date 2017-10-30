using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Xml;

namespace JCodes.Framework.AddIn.Proj
{
    public partial class FrmEditGroupName : BaseEditForm
    {

        public string strGroupName = string.Empty;

        public FrmEditGroupName()
        {
            InitializeComponent();
        }

        public override bool CheckInput()
        {
            bool result = true;//默认是可以通过
            #region MyRegion
            if (this.txtGroupName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblGroupName.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtGroupName.Focus();
                result = false;
            }
            #endregion

            return result;
        }
        public override void DisplayData()
        {
            btnAdd.Visible = false;
            if (!string.IsNullOrEmpty(ID))
            {
                txtGuid.Text = ID;

                XmlHelper xmltableshelper = new XmlHelper(@"XML\tables.xml");
                XmlNodeList xmlNodeLst = xmltableshelper.Read(string.Format("datatype/tabletype"));
                foreach (XmlNode xn1 in xmlNodeLst)
                {
                    // 将节点转换为元素，便于得到节点的属性值
                    XmlElement xe = (XmlElement)xn1;

                    // 得到DataTypeInfo节点的所有子节点
                    XmlNodeList xnl0 = xe.ChildNodes;

                    if (ID == xe.Attributes["guid"].Value)
                    {
                        txtGroupName.Text = xe.Attributes["name"].Value;
                        txtCreateDate.Text = xe.Attributes["createdate"].Value;
                    }
                }
            }
            else
            {
                txtGuid.Text = Guid.NewGuid().ToString();
                txtCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            this.txtGroupName.Focus();
        }

        public override void ClearScreen()
        {
            txtGroupName.Text = string.Empty;
            txtCreateDate.Text = string.Empty;
            base.ClearScreen();
        }

        public override bool SaveAddNew()
        {
            TablesTypeInfo info = new TablesTypeInfo();

            SetInfo(info);

            try
            {
                #region 新增数据


                XmlHelper xmltableshelper = new XmlHelper(@"XML\tables.xml");

                var objXmlDoc = xmltableshelper.GetXmlDoc();
                XmlNode objNode = objXmlDoc.SelectSingleNode("datatype/tabletype");
                XmlElement objElement = objXmlDoc.CreateElement("item");
                objElement.SetAttribute("guid", info.GUID);
                objElement.SetAttribute("createdate", info.CreateDate);
                objElement.SetAttribute("name", info.Name);
                objElement.InnerXml = string.Empty;
                objNode.AppendChild(objElement);
              
                xmltableshelper.Save();
                strGroupName = txtGroupName.Text.Trim();
                ID = info.GUID;

                return true;
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditGroupName));
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }

        public override bool SaveUpdated()
        {
            TablesTypeInfo info = new TablesTypeInfo();
            if (info != null)
            {
                SetInfo(info);
                try
                {
                    #region 更新数据
                    strGroupName = txtGroupName.Text.Trim();

                    XmlHelper xmltableshelper = new XmlHelper(@"XML\tables.xml");
                    XmlNodeList xmlNodeLst = xmltableshelper.Read(string.Format("datatype/tabletype"));
                    foreach (XmlNode xn1 in xmlNodeLst)
                    {
                        // 将节点转换为元素，便于得到节点的属性值
                        XmlElement xe = (XmlElement)xn1;

                        // 得到DataTypeInfo节点的所有子节点
                        XmlNodeList xnl0 = xe.ChildNodes;

                        if (ID == xe.Attributes["guid"].Value)
                        {
                            xe.Attributes["name"].Value = txtGroupName.Text;
                        }
                    }

                    xmltableshelper.Save();
                    return true;
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditGroupName));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
        }

        private void SetInfo(TablesTypeInfo info)
        {
            info.GUID = txtGuid.Text.Trim();
            info.Name = txtGroupName.Text.Trim();
            info.CreateDate = txtCreateDate.Text.Trim();
        }
    }
}
