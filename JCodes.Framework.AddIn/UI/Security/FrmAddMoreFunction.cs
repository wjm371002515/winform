using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.Entity;
using JCodes.Framework.CommonControl;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.Other;

namespace JCodes.Framework.AddIn.UI.Security
{
    public partial class FrmAddMoreFunction : BaseForm
    {
        public FrmAddMoreFunction()
        {
            InitializeComponent();

            this.functionControl1.EditValueChanged += new EventHandler(functionControl1_EditValueChanged);

            InitDictItem();
        }

        void functionControl1_EditValueChanged(object sender, EventArgs e)
        {
            string item = this.functionControl1.Value;
            if (!string.IsNullOrEmpty(item) && item == "-1")
            {
                SetSystemTypeVisible(true);
            }
            else
            {
                SetSystemTypeVisible(false);
            }
        }


        private void SetSystemTypeVisible(bool visible)
        {
            this.txtSystemType.Visible = visible;
            this.lblSystemType.Visible = visible;
        }

        private void InitDictItem()
        {

        }

        public void SetUpperFunction(string value)
        {
            this.functionControl1.Value = value;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.functionControl1.Text.Length == 0)
                return;

            #region 验证用户输入
            if (this.txtName.Text == "")
            {
                MessageDxUtil.ShowTips("功能名称不能为空");
                this.txtName.Focus();
                return;
            }
            else if (this.txtFunctionID.Text == "")
            {
                MessageDxUtil.ShowTips("功能ID不能为空");
                this.txtFunctionID.Focus();
                return;
            }
            else if (this.txtSystemType.Visible && this.txtSystemType.Text.Length == 0)
            {
                MessageDxUtil.ShowTips("系统类型编号不能为空");
                this.txtSystemType.Focus();
                return;
            }

            #endregion

            string pid = this.functionControl1.Value;
            FunctionInfo functionInfo = BLLFactory<Functions>.Instance.FindByID(pid) as FunctionInfo;

            if (functionInfo != null)
            {
                string filter = string.Format("ControlID='{0}' and SystemType_ID='{1}'", this.txtFunctionID.Text, functionInfo.SystemType_ID);
                bool isExist = BLLFactory<Functions>.Instance.IsExistRecord(filter);
                if (isExist)
                {
                    MessageDxUtil.ShowTips("指定功能控制ID重复，请重新输入！");
                    this.txtName.Focus();
                    return;
                }
            }
            else
            {
                //当新建系统类型的时候
                functionInfo = new FunctionInfo();
                functionInfo.PID = "-1";
                functionInfo.SystemType_ID = this.txtSystemType.Text;
                functionInfo.ControlID = this.txtFunctionID.Text;

                string filter = string.Format("ControlID='{0}' and SystemType_ID='{1}'", this.txtFunctionID.Text, functionInfo.SystemType_ID);
                bool isExist = BLLFactory<Functions>.Instance.IsExistRecord(filter);
                if (isExist)
                {
                    MessageDxUtil.ShowTips("指定功能控制ID重复，请重新输入！");
                    this.txtName.Focus();
                    return;
                }
            }

            FunctionInfo mainInfo = new FunctionInfo();
            mainInfo = SetFunction(mainInfo);
            mainInfo.SystemType_ID = functionInfo.SystemType_ID;//和父节点的SystemType_ID一样。
            using (DbTransaction trans = BLLFactory<Functions>.Instance.CreateTransaction())
            {
                try
                {
                    if (trans != null)
                    {
                        bool sucess = BLLFactory<Functions>.Instance.Insert(mainInfo, trans);
                        if (sucess)
                        {
                            FunctionInfo subInfo = null;
                            int sortCodeIndex = 1;

                            #region 子功能操作
                            if (chkAdd.Checked)
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                                subInfo.ControlID = string.Format("{0}/Add", mainInfo.ControlID);
                                subInfo.Name = string.Format("添加{0}", mainInfo.Name);

                                BLLFactory<Functions>.Instance.Insert(subInfo, trans);
                            }
                            if (chkDelete.Checked)
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                                subInfo.ControlID = string.Format("{0}/Delete", mainInfo.ControlID);
                                subInfo.Name = string.Format("删除{0}", mainInfo.Name);
                                BLLFactory<Functions>.Instance.Insert(subInfo, trans);
                            }
                            if (chkModify.Checked)
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                                subInfo.ControlID = string.Format("{0}/Edit", mainInfo.ControlID);
                                subInfo.Name = string.Format("修改{0}", mainInfo.Name);
                                BLLFactory<Functions>.Instance.Insert(subInfo, trans);
                            }
                            if (chkView.Checked)
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                                subInfo.ControlID = string.Format("{0}/View", mainInfo.ControlID);
                                subInfo.Name = string.Format("查看{0}", mainInfo.Name);
                                BLLFactory<Functions>.Instance.Insert(subInfo, trans);
                            }
                            if (chkImport.Checked)
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                                subInfo.ControlID = string.Format("{0}/Import", mainInfo.ControlID);
                                subInfo.Name = string.Format("导入{0}", mainInfo.Name);
                                BLLFactory<Functions>.Instance.Insert(subInfo, trans);
                            }
                            if (chkExport.Checked)
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                                subInfo.ControlID = string.Format("{0}/Export", mainInfo.ControlID);
                                subInfo.Name = string.Format("导出{0}", mainInfo.Name);
                                BLLFactory<Functions>.Instance.Insert(subInfo, trans);
                            }
                            #endregion

                            trans.Commit();
                            ProcessDataSaved(this.btnSave, new EventArgs());

                            //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            MessageDxUtil.ShowTips("保存成功");
                        }
                        else
                        {
                            MessageDxUtil.ShowTips("保存失败");
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }

                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmAddMoreFunction));
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
        }

        private FunctionInfo CreateSubFunction(FunctionInfo mainInfo)
        {
            FunctionInfo subInfo = new FunctionInfo();
            subInfo.PID = mainInfo.ID;
            subInfo.SystemType_ID = mainInfo.SystemType_ID;
            return subInfo;
        }

        private FunctionInfo SetFunction(FunctionInfo info)
        {
            info.Name = this.txtName.Text;
            info.PID = this.functionControl1.Value;
            info.ControlID = this.txtFunctionID.Text;
            return info;
        }
    }
}
