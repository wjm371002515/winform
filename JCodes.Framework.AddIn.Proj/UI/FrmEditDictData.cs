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
    public partial class FrmEditDictData : BaseEditForm
    {
        public FrmEditDictData()
        {
            InitializeComponent();
        }

        public override bool CheckInput()
        {
            bool result = true;//默认是可以通过
            #region MyRegion
            if (this.txtDictType.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblDictType.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtDictType.Focus();
                result = false;
            }
            if (this.txtValue.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblValue.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtValue.Focus();
                result = false;
            }
            if (this.txtName.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblName.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtName.Focus();
                result = false;
            }
            if (this.txtSeq.Text.Trim().Length == 0)
            {
                MessageDxUtil.ShowWarning(Const.MsgCheckInput + lblSeq.Text.Replace(Const.MsgCheckSign, string.Empty));
                this.txtSeq.Focus();
                result = false;
            }

            Int32 Id = txtValue.Text.ToInt32();
            if (result)
            {
                if (Id == 0)
                {
                    MessageDxUtil.ShowWarning(lblValue.Text.Replace(Const.MsgCheckSign, string.Empty) + Const.MsgErrFormatByNum);
                    txtValue.Focus();
                    result = false;
                }
            }

            // 检查对应的值是否已经存在数据库了
            if (result && Id == 0)
            {
                XmlHelper xmldicthelper = new XmlHelper(@"XML\dict.xml");
                XmlNodeList xmlNodeLst = xmldicthelper.Read(string.Format("datatype/dataitem/item[id=\"{0}\"]/subdic", this.txtDictType.Tag));

                foreach (XmlNode xn1 in xmlNodeLst)
                {
                    // 将节点转换为元素，便于得到节点的属性值
                    XmlElement xe = (XmlElement)xn1;

                    // 得到DataTypeInfo节点的所有子节点
                    XmlNodeList xnl0 = xe.ChildNodes;
                    if (string.Equals(Id, xnl0.Item(0).InnerText))
                    {
                        MessageDxUtil.ShowTips(string.Format("已存在此值域数据[字典大类编号:{0},字典值:{1},字典名称:{2}]", this.txtDictType.Tag, xnl0.Item(0).InnerText, xnl0.Item(1).InnerText));
                        this.txtValue.Focus();
                        result = false;
                        break;
                    }
                }
            }
            #endregion

            return result;
        }

        public override void DisplayData()
        {
            if (Id > 0)
            {
                this.txtValue.Enabled = false;

                XmlHelper xmldicthelper = new XmlHelper(@"XML\dict.xml");
                XmlNodeList xmlNodeLst = xmldicthelper.Read(string.Format("datatype/dataitem/item[id=\"{0}\"]/subdic", this.txtDictType.Tag));

                foreach (XmlNode xn1 in xmlNodeLst)
                {
                    // 将节点转换为元素，便于得到节点的属性值
                    XmlElement xe = (XmlElement)xn1;

                    // 得到DataTypeInfo节点的所有子节点
                    XmlNodeList xnl0 = xe.ChildNodes;
                    if (xnl0.Item(0).InnerText.ToInt32() == Id)
                    {
                        this.txtDictType.Enabled = false;
                        this.txtValue.Text = Id.ToString();
                        this.txtName.Text = xnl0.Item(1).InnerText;
                        this.txtSeq.Text = xnl0.Item(2).InnerText;
                        this.txtNote.Text = xnl0.Item(3).InnerText; 
                        break;
                    }
                }
            }

            this.txtName.Focus();
        }

        private void SetInfo(DictDataInfo info)
        {
            info.DicttypeId = this.txtDictType.Tag.ToString().ToInt32();
            info.DicttypeValue = this.txtValue.Text.Trim().ToInt32();
            info.Name = this.txtName.Text.Trim();
            info.Seq = this.txtSeq.Text;
            info.Remark = this.txtNote.Text.Trim();
            //info.EditorId = LoginUserInfo.Id;
            //info.CurrentLoginUserId = LoginUserInfo.Id;
            info.LastUpdateTime = DateTimeHelper.GetServerDateTime2();
        }

        public override void ClearScreen()
        {
            txtValue.Text = string.Empty;
            txtName.Text = string.Empty;
            txtSeq.Text = string.Empty;
            txtNote.Text = string.Empty;

            base.ClearScreen();
        }

        public override bool SaveAddNew()
        {
            DictDataInfo info = new DictDataInfo();

            SetInfo(info);

            try
            {
                #region 新增数据
                XmlHelper xmldicthelper = new XmlHelper(@"XML\dict.xml");
                xmldicthelper.InsertElement(string.Format("datatype/dataitem/item[id=\"{0}\"]/subdic", this.txtDictType.Tag), "item", string.Format("<value>{0}</value><name>{1}</name><seq>{2}</seq><remark>{3}</remark>", info.DicttypeValue, info.Name, info.Seq, info.Remark));
                xmldicthelper.Save(false);
                return true;
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditDictData));
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }

        public override bool SaveUpdated()
        {
            DictDataInfo info = new DictDataInfo();
            XmlHelper xmldicthelper = new XmlHelper(@"XML\dict.xml");
            XmlNodeList xmlNodeLst = xmldicthelper.Read(string.Format("datatype/dataitem/item[id=\"{0}\"]/subdic/item[value=\"{1}\"]", txtDictType.Tag, Id));

            info.DicttypeValue = Id;
            info.Name = xmlNodeLst[1].ChildNodes.Item(0) == null ? string.Empty : xmlNodeLst[1].ChildNodes.Item(0).InnerText;
            info.Seq = xmlNodeLst[2].ChildNodes.Item(0) == null ? string.Empty : xmlNodeLst[2].ChildNodes.Item(0).InnerText;
            info.Remark = xmlNodeLst[3].ChildNodes.Item(0) == null ? string.Empty : xmlNodeLst[3].ChildNodes.Item(0).InnerText;

            if (info != null)
            {
                SetInfo(info);
                try
                {
                    #region 更新数据
                    xmldicthelper.Replace(string.Format("datatype/dataitem/item[id=\"{0}\"]/subdic/item[value=\"{1}\"]/name", txtDictType.Tag, Id), info.Name);
                    xmldicthelper.Replace(string.Format("datatype/dataitem/item[id=\"{0}\"]/subdic/item[value=\"{1}\"]/seq", txtDictType.Tag, Id), info.Seq);
                    xmldicthelper.Replace(string.Format("datatype/dataitem/item[id=\"{0}\"]/subdic/item[value=\"{1}\"]/remark", txtDictType.Tag, Id), info.Remark);
                    xmldicthelper.Save(false);
                    return true;
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmEditDictData));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
            return false;
           
        }
    }
}
