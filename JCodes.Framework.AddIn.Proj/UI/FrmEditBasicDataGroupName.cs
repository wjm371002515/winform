using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using JCodes.Framework.CommonControl.Controls;
using System.Xml;
using System.Collections.Generic;

namespace JCodes.Framework.AddIn.Proj
{
    public partial class FrmEditBasicDataGroupName : BaseEditForm
    {

        public string strGroupName = string.Empty;

        public FrmEditBasicDataGroupName()
        {
            InitializeComponent();
        }

        public override bool CheckInput()
        {
            bool result = true;//默认是可以通过
            #region MyRegion
            if (this.cbbGroupName.GetComboBoxStrValue().Length == 0 || string.Equals(this.cbbGroupName.GetComboBoxStrValue(), Const.NoSeletValue.ToString()))
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblGroupName.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.cbbGroupName.Focus();
                result = false;
            }
            #endregion

            return result;
        }
        public override void DisplayData()
        {
            btnAdd.Visible = false;

            #region 初始化下拉框的内容
            XmlHelper xmltableshelper = new XmlHelper(@"XML\tables.xml");
            XmlNodeList xmlNodeLst = xmltableshelper.Read("datatype/tabletype");
            List<CListItem> lst = new List<CListItem>();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;

                // 配置的这个节点 basicdata 为1时才加载数据
                if (string.Equals(xe.GetAttribute("basicdata").ToString(), Const.Num_Zero.ToString()))
                {
                    lst.Add(new CListItem(xe.GetAttribute("gid").ToString(), xe.GetAttribute("name").ToString()));
                }
            }
            #endregion

            cbbGroupName.BindDictItems(lst);

            this.cbbGroupName.Focus();
        }

        public override void ClearScreen()
        {
            cbbGroupName.SelectedIndex = -1;
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
                // 修改大的分类basicdata
                objXmlDoc.SelectSingleNode(string.Format("datatype/tabletype/item[@gid=\"{0}\"]", info.Gid)).Attributes["basicdata"].InnerText = "1";
                strGroupName = objXmlDoc.SelectSingleNode(string.Format("datatype/tabletype/item[@gid=\"{0}\"]", info.Gid)).Attributes["name"].InnerText;
                xmltableshelper.Save();
                // TODO
                //Id = info.GUID;
                
                return true;
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditBasicDataGroupName));
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }

        // 不支持修改
        public override bool SaveUpdated()
        {
            return true;
        }

        private void SetInfo(TablesTypeInfo info)
        {
            info.Gid = cbbGroupName.GetComboBoxStrValue();
        }

        /// <summary>
        /// 改变下拉框的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbGroupName_SelectedValueChanged(object sender, EventArgs e)
        {
            txtGuid.Text = cbbGroupName.GetComboBoxStrValue();
        }
    }
}
