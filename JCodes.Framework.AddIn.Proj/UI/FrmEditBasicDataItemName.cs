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
    public partial class FrmEditBasicDataItemName : BaseEditForm
    {

        public string strGuid = string.Empty;
        public string strItemName = string.Empty;

        public FrmEditBasicDataItemName()
        {
            InitializeComponent();
        }

        public override bool CheckInput()
        {
            bool result = true;//默认是可以通过
            #region MyRegion
            if (this.cbbItemName.GetComboBoxStrValue().Length == 0 || string.Equals(this.cbbItemName.GetComboBoxStrValue(), Const.NoSeletValue.ToString()))
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblGroupName.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.cbbItemName.Focus();
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
            XmlNodeList xmlNodeLst = xmltableshelper.Read("datatype/dataitem");
            List<CListItem> lst = new List<CListItem>();
            foreach (XmlNode xn1 in xmlNodeLst)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;

                // 配置的这个节点 basicdata 为1时才加载数据
                if (string.Equals(strGuid, xnl0.Item(3).InnerText) && string.IsNullOrEmpty(xnl0.Item(5).InnerText))
                {
                    lst.Add(new CListItem(xe.GetAttribute("gid").ToString(), string.Format("{0}-({1} {2})", xnl0.Item(2).InnerText, xnl0.Item(1).InnerText, xnl0.Item(0).InnerText)));
                }
            }
            #endregion

            cbbItemName.BindDictItems(lst);

            this.cbbItemName.Focus();
        }

        public override void ClearScreen()
        {
            cbbItemName.SelectedIndex = -1;
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
                xmltableshelper.Replace(string.Format("datatype/dataitem/item[@gid=\"{0}\"]/basicdatapath", info.Gid), info.BasicdataPath);
                xmltableshelper.Save();

                if (FileUtil.FileIsExist(info.BasicdataPath))
                {
                    FileUtil.DeleteFile(info.BasicdataPath);
                }

                string tablesdataDetailModel = "<?xml version=\"1.0\" encoding=\"utf-8\"?><datatype><dataitem></dataitem></datatype>";
                FileUtil.AppendText(info.BasicdataPath, tablesdataDetailModel, System.Text.Encoding.UTF8);

                //ID = info.GUID;
                strItemName = cbbItemName.SelectedText;
                return true;
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditBasicDataItemName));
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }

        // 不支持修改
        public override bool SaveUpdated()
        {
            return true;
        }

        private void SetInfo(TablesInfo info)
        {
            info.Gid = cbbItemName.GetComboBoxStrValue();
            info.Name = cbbItemName.SelectedText.Split('(')[1].Split(' ')[1].TrimEnd(')');
            info.BasicdataPath = string.Format(@"XML\{0}.basicdata", info.Name);
        }

        /// <summary>
        /// 改变下拉框的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbGroupName_SelectedValueChanged(object sender, EventArgs e)
        {
            txtGuid.Text = cbbItemName.GetComboBoxStrValue();
        }
    }
}
