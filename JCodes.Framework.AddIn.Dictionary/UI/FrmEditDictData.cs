using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.AddIn.Dictionary
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

            string strId = txtValue.Text;
            if (result)
            {
                if (!ValidateUtil.IsNumeric(strId))
                {
                    MessageDxUtil.ShowWarning(lblValue.Text.Replace(Const.MsgCheckSign, string.Empty) + Const.MsgErrFormatByNum);
                    txtValue.Focus();
                    result = false;
                }
            }

            // 检查对应的值是否已经存在数据库了
            if (result && Id == 0)
            {
                SearchCondition condition = new SearchCondition();
                condition.AddCondition("DicttypeId", Convert.ToInt32(this.txtDictType.Tag), SqlOperator.Equal);
                condition.AddCondition("DicttypeValue", Convert.ToInt32(Id), SqlOperator.Equal);
                string where = condition.BuildConditionSql().Replace("Where", "");
                var lst = BLLFactory<DictData>.Instance.Find(where);
                if (lst.Count > 0)
                {
                    MessageDxUtil.ShowTips(string.Format("已存在此值域数据[字典大类编号:{0},字典值:{1},字典名称:{2}]", lst[0].DicttypeId, lst[0].DicttypeValue, lst[0].Name));
                    this.txtValue.Focus();
                    result = false;
                }
            }
            #endregion

            return result;
        }

        public override void DisplayData()
        {
            if (Id>0)
            {
                string Gid = Tag.ToString();
                DictDataInfo info = BLLFactory<DictData>.Instance.FindById(Gid);
                if (info != null)
                {
                    DictTypeInfo typeInfo = BLLFactory<DictType>.Instance.FindById(info.DicttypeId);

                    this.txtDictType.Text = typeInfo.Name;
                    this.txtDictType.Tag = typeInfo.Id;
                    this.txtDictType.Enabled = false;
                    this.txtName.Text = info.Name;
                    this.txtNote.Text = info.Remark;
                    this.txtSeq.Text = info.Seq;
                    this.txtValue.Text = info.DicttypeValue.ToString();
                }
            }

            this.txtName.Focus();
        }

        private void SetInfo(DictDataInfo info)
        {
            info.DicttypeId = Convert.ToInt32(this.txtDictType.Tag);
            info.DicttypeValue = this.txtValue.Text.Trim().ToInt32();
            info.Name = this.txtName.Text.Trim();
            info.Seq = this.txtSeq.Text;
            info.Remark = this.txtNote.Text.Trim();
            info.EditorId = LoginUserInfo.Id;
            info.LastUpdateTime = DateTimeHelper.GetServerDateTime2();
            info.CurrentLoginUserId = LoginUserInfo.Id;

            if(string.IsNullOrEmpty(info.Gid))
                info.Gid = Guid.NewGuid().ToString();

            if (Tag != null && string.IsNullOrEmpty(Tag.ToString()))
            {
                info.Gid = Tag.ToString();
            }
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

                bool succeed = BLLFactory<DictData>.Instance.Insert(info);
                if (succeed)
                {
                    //可添加其他关联操作
                    return true;
                }
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
            String Gid = Tag.ToString();
            DictDataInfo info = BLLFactory<DictData>.Instance.FindById(Gid);
            if (info != null)
            {
                SetInfo(info);
                try
                {
                    #region 更新数据
                    bool succeed = BLLFactory<DictData>.Instance.Update(info, info.Gid);
                    if (succeed)
                    {
                        //可添加其他关联操作
                        return true;
                    }
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
