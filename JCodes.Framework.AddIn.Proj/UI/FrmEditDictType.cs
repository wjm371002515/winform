using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Format;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Xml;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.AddIn.Proj
{
    public partial class FrmEditDictType : BaseEditForm
    {
        public Int32 Pid = -1;
        private Int32 parentPid = -1;
        private string parentStr = string.Empty;

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

            Int32 Id = txtID.Text.ToInt32();
            if (result)
            {
                if (Id == 0)
                {
                    MessageDxUtil.ShowWarning(txtID.Text.Replace(Const.MsgCheckSign, string.Empty) + Const.MsgErrFormatByNum);
                    txtID.Focus();
                    result = false;
                }
            }

            if (result && Id == 0)
            {
                #region 加载数据字典大项
                XmlHelper xmldicthelper = new XmlHelper(@"XML\dict.xml");
                XmlNodeList xmlNodeLst = xmldicthelper.Read("datatype/dataitem");

                for (Int32 i = 0; i < xmlNodeLst.Count; i++)
                {
                    // 将节点转换为元素，便于得到节点的属性值
                    XmlElement xe = (XmlElement)xmlNodeLst[i];

                    // 得到DataTypeInfo节点的所有子节点
                    XmlNodeList xnl0 = xe.ChildNodes;

                    if (Id == xnl0.Item(0).InnerText.ToString().ToInt32())
                    {
                        MessageDxUtil.ShowTips(string.Format("已存在类别编号[{0}],类别名称[{1}]", Id, xnl0.Item(2).InnerText));
                        txtID.Focus();
                        result = false;
                    }

                    if (Pid != -1 && this.txtParent.Tag.ToString().ToInt32() >= 1)
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

                if (Pid != -1 && Pid == xnl0.Item(0).InnerText.ToString().ToInt32())
                {
                    this.txtParent.Text = string.Format("{0}-{1}", xnl0.Item(0).InnerText, xnl0.Item(2).InnerText);
                    // 保存上一个节点的PID 值
                    this.txtParent.Tag = xnl0.Item(1).InnerText;
                }

                if (xnl0.Item(0).InnerText.ToString().ToInt32() == Id)
                {
                    info = new DictTypeInfo();
                    info.Id = xnl0.Item(0).InnerText.ToString().ToInt32();
                    info.Pid = xnl0.Item(1).InnerText.ToString().ToInt32();
                    info.Name = xnl0.Item(2).InnerText;
                    info.Remark = xnl0.Item(3).InnerText;
                }
            }

            if (Id > 0)
            {
                this.chkTopItem.Enabled = false;
                if (info != null)
                {
                    this.txtID.Text = info.Id.ToString();
                    this.txtID.Enabled = false;
                    this.txtName.Text = info.Name;
                    this.txtNote.Text = info.Remark;

                    if (info.Pid == -1)
                    {
                        this.chkTopItem.Checked = true;
                    }
                }
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
                xmldicthelper.InsertElement("datatype/dataitem", "item", string.Format(dicttypeModel, info.Id, info.Pid, info.Name, info.Remark, string.Empty));
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
            XmlNodeList xmlNodeLst = xmldicthelper.Read(string.Format("datatype/dataitem/item[id=\"{0}\"]", Id));
           
            info.Id = xmlNodeLst[0].ChildNodes.Item(0).InnerText.ToString().ToInt32();
            info.Pid = xmlNodeLst[1].ChildNodes.Item(0).InnerText.ToString().ToInt32();
            info.Name = xmlNodeLst[2].ChildNodes.Item(0) == null ? string.Empty : xmlNodeLst[2].ChildNodes.Item(0).InnerText;
            info.Seq = xmlNodeLst[0].ChildNodes.Item(0) == null ? string.Empty : xmlNodeLst[0].ChildNodes.Item(0).InnerText;
            info.Remark = xmlNodeLst[3].ChildNodes.Item(0) == null ? string.Empty : xmlNodeLst[3].ChildNodes.Item(0).InnerText;

            if (info != null)
            {
                SetInfo(info);
                try
                {
                    #region 更新数据
                    xmldicthelper.Replace(string.Format("datatype/dataitem/item[id=\"{0}\"]/pid", Id), info.Pid.ToString());
                    xmldicthelper.Replace(string.Format("datatype/dataitem/item[id=\"{0}\"]/name", Id), info.Name);
                    xmldicthelper.Replace(string.Format("datatype/dataitem/item[id=\"{0}\"]/remark", Id), info.Remark);
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
            info.Id = txtID.Text.ToInt32();
            //info.EditorId = LoginUserInfo.Id;
            //info.CurrentLoginUserId = LoginUserInfo.Id;
            info.LastUpdateTime = DateTimeHelper.GetServerDateTime2();
            info.Name = this.txtName.Text.Trim();
            info.Remark = this.txtNote.Text.Trim();
            info.Pid = Pid;
            if (this.chkTopItem.Checked)
            {
                info.Pid = -1;
            } 
        }

        private void chkTopItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTopItem.Checked || this.txtParent.Tag == null)
            {
                parentStr = this.txtParent.Text ;
                parentPid = txtParent.Tag.ToString().ToInt32();
                this.txtParent.Text = "无(顶级项目)";
                this.txtParent.Tag = "-1";
            }
            else
            {
                this.txtParent.Text = parentStr;
                this.txtParent.Tag = parentPid.ToString();
            }
        }

    }
}
