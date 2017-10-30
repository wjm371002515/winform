using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Xml;

namespace JCodes.Framework.AddIn.Proj
{
    public partial class FrmEditDictType : BaseEditForm
    {
        public Int32 PID = -1;

        private string parentStr = string.Empty;
        private Int32 parentPID = -1;

        private string dicttypeModel = @"<id>{0}</id><pid>{1}</pid><name>{2}</name><remark>{3}</remark><subdic>{4}</subdic>";

        public FrmEditDictType()
        {
            InitializeComponent();
        }

        public override bool CheckInput()
        {
            bool result = true;//默认是可以通过
            #region MyRegion
            if (txtID.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblID.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtID.Focus();
                result = false;
            }

            if (txtName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblName.Text.Replace(Const.MsgCheckSign, string.Empty));
                txtName.Focus();
                result = false;
            }

            if (txtParent.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblParent.Text.Replace(Const.MsgCheckSign, string.Empty));
                txtParent.Focus();
                result = false;
            }

            string Id = txtID.Text;
            if (result)
            {
                if (!ValidateUtil.IsNumeric(Id))
                {
                    MessageDxUtil.ShowWarning(txtID.Text.Replace(Const.MsgCheckSign, string.Empty) + Const.MsgErrFormatByNum);
                    txtID.Focus();
                    result = false;
                }
            }

            if (result && string.IsNullOrEmpty(ID))
            {
                Int32 NumId = Convert.ToInt32(Id);

                #region 加载数据字典大项
                XmlHelper xmldicthelper = new XmlHelper(@"XML\dict.xml");
                XmlNodeList xmlNodeLst = xmldicthelper.Read("datatype/dataitem");

                for (Int32 i = 0; i < xmlNodeLst.Count; i++)
                {
                    // 将节点转换为元素，便于得到节点的属性值
                    XmlElement xe = (XmlElement)xmlNodeLst[i];

                    // 得到DataTypeInfo节点的所有子节点
                    XmlNodeList xnl0 = xe.ChildNodes;

                    if (NumId == Convert.ToInt32(xnl0.Item(0).InnerText))
                    {
                         MessageDxUtil.ShowTips(string.Format("已存在类别编号[{0}],类别名称[{1}]",NumId , xnl0.Item(2).InnerText));
                        txtID.Focus();
                        result = false;
                    }

                    if (PID != -1 && Convert.ToInt32(this.txtParent.Tag) >= 1)
                    {
                        MessageDxUtil.ShowTips("数据类型只允许2级目录");
                        txtID.Focus();
                        result = false;
                    }
                }
                #endregion
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 数据显示的函数
        /// </summary>
        public override void DisplayData()
        {
            XmlHelper xmldicthelper = new XmlHelper(@"XML\dict.xml");
            XmlNodeList xmlNodeLst = xmldicthelper.Read("datatype/dataitem");

            this.txtParent.Text = string.Empty;
            this.txtParent.Tag = string.Empty;
            DictTypeInfo info = null;

            for (Int32 i = 0; i < xmlNodeLst.Count; i++)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xmlNodeLst[i];

                // 得到DataTypeInfo节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;

                if (PID != -1 && PID == Convert.ToInt32(xnl0.Item(0).InnerText))
                {
                    this.txtParent.Text = string.Format("{0}-{1}", xnl0.Item(0).InnerText, xnl0.Item(2).InnerText);
                    // 保存上一个节点的PID 值
                    this.txtParent.Tag = xnl0.Item(1).InnerText;
                }

                if (string.Equals(ID,  xnl0.Item(0).InnerText))
                {
                    info = new DictTypeInfo();
                    info.ID = Convert.ToInt32(xnl0.Item(0).InnerText);
                    info.PID = Convert.ToInt32(xnl0.Item(1).InnerText);
                    info.Name = xnl0.Item(2).InnerText;
                    info.Remark = xnl0.Item(3).InnerText;
                }
            }

            if (!string.IsNullOrEmpty(ID))
            {
                this.Text = "编辑 " + this.Text;
                this.chkTopItem.Enabled = false;
                if (info != null)
                {
                    this.txtID.Text = info.ID.ToString();
                    this.txtID.Enabled = false;
                    this.txtName.Text = info.Name;
                    this.txtNote.Text = info.Remark;

                    if (info.PID == -1)
                    {
                        this.chkTopItem.Checked = true;
                    }
                }
            }
            else
            {
                this.Text = "新建 " + this.Text;
            }
            this.txtName.Focus();
        }

        public override void ClearScreen()
        {
            this.txtID.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtNote.Text = string.Empty;

            base.ClearScreen();
        }

        public override bool SaveAddNew()
        {
            DictTypeInfo info = new DictTypeInfo();

            SetInfo(info);

            try
            {
                #region 新增数据
                XmlHelper xmldicthelper = new XmlHelper(@"XML\dict.xml");
                xmldicthelper.InsertElement("datatype/dataitem", "item", string.Format(dicttypeModel, info.ID, info.PID, info.Name, info.Remark, string.Empty));
                xmldicthelper.Save(false);
                return true;
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditDictType));
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }

        public override bool SaveUpdated()
        {
            DictTypeInfo info = new DictTypeInfo();
            XmlHelper xmldicthelper = new XmlHelper(@"XML\dict.xml");
            XmlNodeList xmlNodeLst = xmldicthelper.Read(string.Format("datatype/dataitem/item[id=\"{0}\"]", ID));
           
            info.ID = Convert.ToInt32(xmlNodeLst[0].ChildNodes.Item(0).InnerText);
            info.PID = Convert.ToInt32(xmlNodeLst[1].ChildNodes.Item(0).InnerText);
            info.Name = xmlNodeLst[2].ChildNodes.Item(0) == null ? string.Empty : xmlNodeLst[2].ChildNodes.Item(0).InnerText;
            info.Seq = xmlNodeLst[0].ChildNodes.Item(0) == null ? string.Empty : xmlNodeLst[0].ChildNodes.Item(0).InnerText;
            info.Remark = xmlNodeLst[3].ChildNodes.Item(0) == null ? string.Empty : xmlNodeLst[3].ChildNodes.Item(0).InnerText;

            if (info != null)
            {
                SetInfo(info);
                try
                {
                    #region 更新数据
                    xmldicthelper.Replace(string.Format("datatype/dataitem/item[id=\"{0}\"]/pid", ID), info.PID.ToString());
                    xmldicthelper.Replace(string.Format("datatype/dataitem/item[id=\"{0}\"]/name", ID), info.Name);
                    xmldicthelper.Replace(string.Format("datatype/dataitem/item[id=\"{0}\"]/remark", ID), info.Remark);
                    xmldicthelper.Save(false);
                    return true;
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditDictType));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
        }


        private void SetInfo(DictTypeInfo info)
        {
            info.ID = Convert.ToInt32(txtID.Text);
            info.Editor = LoginUserInfo.ID.ToString();
            info.LastUpdated = DateTimeHelper.GetServerDateTime2();
            info.Name = this.txtName.Text.Trim();
            info.Remark = this.txtNote.Text.Trim();
            info.PID = PID;
            if (this.chkTopItem.Checked)
            {
                info.PID = -1;
            }

            info.CurrentLoginUserId = LoginUserInfo.ID.ToString();
        }

        private void chkTopItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTopItem.Checked || this.txtParent.Tag == null)
            {
                parentStr = this.txtParent.Text ;
                parentPID = Convert.ToInt32(txtParent.Tag);
                this.txtParent.Text = "无(顶级项目)";
                this.txtParent.Tag = "-1";
            }
            else
            {
                this.txtParent.Text = parentStr;
                this.txtParent.Tag = parentPID.ToString();
            }
        }

    }
}
