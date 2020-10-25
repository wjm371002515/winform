using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JCodes.Framework.Common;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.CommonControl.Other.Images;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.CommonControl.BaseUI
{
    /// <summary>
    /// 编辑界面基类
    /// </summary>
    public partial class BaseEditForm : BaseDock
    {
        public Int32 Id;  // 记录主键
        public List<Int32> IdList = new List<Int32>();//所有待展示的ID列表

        public BaseEditForm()
        {
            InitializeComponent();

            this.dataNavigator1.PositionChanged += new PostionChangedEventHandler(dataNavigator1_PositionChanged);
        }

        private void dataNavigator1_PositionChanged(object sender, EventArgs e)
        {
            this.Id = IdList[this.dataNavigator1.CurrentIndex];
            DisplayData();
        }

        public override void FormOnLoad()
        {
            base.FormOnLoad();

            // 特殊处理 如果是当前用户信息 则修改此值
            if (base.Name == "FrmEditUser" && base.ParentForm != null && base.ParentForm.Name == "MainForm")
            {
                Id = LoginUserInfo.Id;
            }

            if (!this.DesignMode)
            {
                if (Id>0)
                {
                    if (!this.Text.Contains("编辑"))
                    {
                        this.Text = "编辑 " + this.Text;
                    }
                    this.btnAdd.Visible = false;//如果是编辑，则屏蔽添加按钮
                }
                else
                {
                    if (!this.Text.Contains("新建"))
                    {
                        this.Text = "新建 " + this.Text;
                    }
                }

                this.dataNavigator1.IdList = IdList;
                //this.dataNavigator1.CurrentIndex = Convert.ToInt32( IDList.IndexOf(Id));
                if (IdList == null || IdList.Count == 0)
                {
                    this.dataNavigator1.Visible = false;
                    DisplayData();//CurrentIndex = -1的时候需要主动调用
                }

                //由于上面设置this.dataNavigator1.CurrentIndex，导致里面触发dataNavigator1_PositionChanged
                //从而调用了DisplayData，所以下面的代码不用重复调用，否则执行了两次。
                //DisplayData();
            }
        }

        private void BaseEditForm_Load(object sender, EventArgs e)
        {
        }                
 
        /// <summary>
        /// 显示数据到控件上
        /// </summary>
        public virtual void DisplayData()
        {
        } 
        
        /// <summary>
        /// 检查输入的有效性
        /// </summary>
        /// <returns>有效</returns>
        public virtual bool CheckInput()
        {
            return true;
        }
               
        /// <summary>
        /// 清除屏幕
        /// </summary>
        public virtual void ClearScreen()
        {
            this.Id = 0;////需要设置为空，表示新增
            ClearControlValue(this);
            this.FormOnLoad();
        }

        /// <summary>
        /// 清除容器里面某些控件的值
        /// </summary>
        /// <param name="parContainer">容器类控件</param>
        public void ClearControlValue(System.Windows.Forms.Control ctrl)
        {
            ClearSinglelValue(ctrl);
            if (ctrl.Controls.Count > 0)
            {
                // 如果是容器类控件，递归调用自己
                foreach (System.Windows.Forms.Control control in ctrl.Controls)
                {
                    ClearControlValue(control);
                }
            }
        }

        /// <summary>
        /// 清除容器里面某些控件的值
        /// </summary>
        /// <param name="ctrl">容器类控件</param>
        public virtual void ClearSinglelValue(System.Windows.Forms.Control ctrl)
        {
            switch (ctrl.GetType().Name)
            {
                case "TextEdit":
                case "MemoEdit":
                case "MemoExEdit":
                    ctrl.Text = "";
                    break;
                case "SpinEdit":
                    ((SpinEdit)ctrl).Value = 0M;
                    break;

                case "CheckEdit":
                    ((CheckEdit)ctrl).Checked = false;
                    break;

                case "ComboBoxEdit":
                    ((ComboBoxEdit)ctrl).Text = "";
                    break;
                case "SearchLookUpEdit":
                    ((SearchLookUpEdit)ctrl).Text = "";
                    break;
                case "GridLookUpEdit":
                    ((GridLookUpEdit)ctrl).Text = "";
                    break;
            }
        }
                
        /// <summary>
        /// 保存数据（新增和编辑的保存）
        /// </summary>
        public virtual bool SaveEntity()
        {
            bool result = false;
            if(Id>0)
            {
                //编辑的保存
                result = SaveUpdated();
            }
            else
            {
                //新增的保存
                result = SaveAddNew();
            }

            return result;
        }

        /// <summary>
        /// 更新已有的数据
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveUpdated()
        {
            return true;
        }

        /// <summary>
        /// 保存新增的数据
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveAddNew()
        {
            return true;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="close">关闭窗体</param>
        private void SaveEntity(bool close)
        {
            // 检查输入的有效性
            if (this.CheckInput())
            {
                // 设置鼠标繁忙状态
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    if (this.SaveEntity())
                    {
                        ProcessDataSaved(this.btnOK, new EventArgs());

                        MessageDxUtil.ShowTips("保存成功");
                        if (close)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            this.ClearScreen();
                        }                           
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(BaseEditForm));
                    MessageDxUtil.ShowError(ex.Message);
                }
                finally
                {
                    // 设置鼠标默认状态
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.SaveEntity(false);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.SaveEntity(true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {       
            if (keyData == Keys.F5)
            {
                this.FormOnLoad();
            }

            if (!(ActiveControl is Button))
            {
                if (keyData == Keys.Down || keyData == Keys.Enter)
                {
                    return this.SelectNextControl(this.ActiveControl, true, true, true, true);
                }
                else if (keyData == Keys.Up)
                {
                    return this.SelectNextControl(this.ActiveControl, false, true, true, true);
                }
                if (keyData == Keys.Enter)
                {
                    System.Windows.Forms.SendKeys.Send("{TAB}");
                    return true;
                }
                //if (keyData == Keys.Down)
                //{
                //    System.Windows.Forms.SendKeys.Send("{TAB}");
                //}
                //else
                //{
                //    SendKeys.Send("+{Tab}");
                //}

                return false;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void picPrint_Click(object sender, EventArgs e)
        {
            PrintFormHelper.Print(this);
        }
    }
}
