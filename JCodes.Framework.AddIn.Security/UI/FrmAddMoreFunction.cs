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
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.AddIn.Basic;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.AddIn.Security
{
    public partial class FrmAddMoreFunction : BaseDock
    {
        public FrmAddMoreFunction()
        {
            InitializeComponent();
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
            else if (this.txtDllPath.Text == "")
            {
                MessageDxUtil.ShowTips("映射路径不能为空");
                this.txtDllPath.Focus();
                return;
            }
            else if (this.txtSystemType.Visible && this.txtSystemType.Text.Length == 0)
            {
                MessageDxUtil.ShowTips("系统类型编号不能为空");
                this.txtSystemType.Focus();
                return;
            }

            string pid = this.functionControl1.Value;
            FunctionInfo functionInfo = BLLFactory<Function>.Instance.FindById(pid) as FunctionInfo;

            // 校验功能菜单是否存在 如果不存在则不允许创建
            MenuInfo menuInfo = BLLFactory<JCodes.Framework.BLL.Menu>.Instance.FindSingle(string.Format("Name = '{0}' and DllPath = '{1}' and SystemtypeId = '{2}'", txtName.Text, txtDllPath.Text, functionInfo.SystemtypeId));
            if (menuInfo == null)
            {
                MessageDxUtil.ShowTips("请在菜单部分维护其对应菜单后操作");
                return;
            }

            #endregion

            // 20200522 在新增菜单的时候加了功能
            /*if (functionInfo != null)
            {
                string filter = string.Format("DllPath='{0}' and SystemtypeId='{1}'", this.txtDllPath.Text, functionInfo.SystemtypeId);
                bool isExist = BLLFactory<Function>.Instance.IsExistRecord(filter);
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
                functionInfo.Gid = "-1";
                functionInfo.SystemtypeId = this.txtSystemType.Text;
                functionInfo.DllPath = this.txtDllPath.Text;

                string filter = string.Format("DllPath='{0}' and SystemtypeId='{1}'", this.txtDllPath.Text, functionInfo.SystemtypeId);
                bool isExist = BLLFactory<Function>.Instance.IsExistRecord(filter);
                if (isExist)
                {
                    MessageDxUtil.ShowTips("指定功能控制ID重复，请重新输入！");
                    this.txtName.Focus();
                    return;
                }
            }*/

            FunctionInfo mainInfo = new FunctionInfo();
            mainInfo.Gid = menuInfo.Gid;
            mainInfo = SetFunction(mainInfo);
            mainInfo.SystemtypeId = functionInfo.SystemtypeId;//和父节点的SystemType_ID一样。
            using (DbTransaction trans = BLLFactory<Function>.Instance.CreateTransaction())
            {
                try
                {
                    if (trans != null)
                    {
                        //bool sucess = BLLFactory<Function>.Instance.Insert(mainInfo, trans);
                        bool sucess = true;
                        if (sucess)
                        {
                            FunctionInfo subInfo = null;
                            int seqIndex = 1;

                            #region 子功能操作
                            if (chkAdd.Checked)
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.Seq = (seqIndex++).ToString("D2");
                                subInfo.DllPath = string.Format("{0}/{1}Add", mainInfo.DllPath, mainInfo.DllPath.Split('/')[0]);
                                subInfo.Name = string.Format("{0}_添加", mainInfo.Name);

                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            if (chkDelete.Checked)
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.Seq = (seqIndex++).ToString("D2");
                                subInfo.DllPath = string.Format("{0}/{1}Delete", mainInfo.DllPath, mainInfo.DllPath.Split('/')[0]);
                                subInfo.Name = string.Format("{0}_删除", mainInfo.Name);
                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            if (chkModify.Checked)
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.Seq = (seqIndex++).ToString("D2");
                                subInfo.DllPath = string.Format("{0}/{1}Edit", mainInfo.DllPath, mainInfo.DllPath.Split('/')[0]);
                                subInfo.Name = string.Format("{0}_修改", mainInfo.Name);
                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            if (chkView.Checked)
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.Seq = (seqIndex++).ToString("D2");
                                subInfo.DllPath = string.Format("{0}/{1}View", mainInfo.DllPath, mainInfo.DllPath.Split('/')[0]);
                                subInfo.Name = string.Format("{0}_查看", mainInfo.Name);
                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            if (chkImport.Checked)
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.Seq = (seqIndex++).ToString("D2");
                                subInfo.DllPath = string.Format("{0}/{1}Import", mainInfo.DllPath, mainInfo.DllPath.Split('/')[0]);
                                subInfo.Name = string.Format("{0}_导入", mainInfo.Name);
                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            if (chkExport.Checked)
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.Seq = (seqIndex++).ToString("D2");
                                subInfo.DllPath = string.Format("{0}/{1}Export", mainInfo.DllPath, mainInfo.DllPath.Split('/')[0]);
                                subInfo.Name = string.Format("{0}_导出", mainInfo.Name);
                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            #endregion

                            trans.Commit();
                            ProcessDataSaved(this.btnSave, new EventArgs());
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
            subInfo.Gid = Guid.NewGuid().ToString();
            subInfo.Pgid = mainInfo.Gid;
            subInfo.SystemtypeId = mainInfo.SystemtypeId;
            return subInfo;
        }

        private FunctionInfo SetFunction(FunctionInfo info)
        {
            info.Name = this.txtName.Text;
            info.Pgid = this.functionControl1.Value;
            info.DllPath = this.txtDllPath.Text;
            info.CurrentLoginUserId = Portal.gc.UserInfo.Id;
            return info;
        }
    }
}
