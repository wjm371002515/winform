using DevExpress.Utils;
using DevExpress.XtraEditors;
using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.CommonControl.Settings;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraEditors.Controls;
using JCodes.Framework.CommonControl.Controls;
using JCodes.Framework.AddIn.Basic;
using JCodes.Framework.Common.Format;

namespace JCodes.Framework.AddIn.Dictionary
{
    public partial class FrmSysparameter : PropertyPage
    {
        private Int32 _SysId = 0;
        private string _titleName = string.Empty;
        private Int32 _currPosition_X = 0;
        private Int32 _currPosition_Y = 0;
        private Int32 _screenWidth = 0;
        private Int32 _screenHeight = 0;
        private List<SysparameterInfo> _info = new List<SysparameterInfo>();
        private string _beforeStr = string.Empty;
        //定义delegate
        public delegate void MEDelegeteHandler(string InputStr);
        //用event 关键字声明事件对象
        public event MEDelegeteHandler MeEvent;

        public FrmSysparameter(Int32 SysId, string TitleName)
        {
            InitializeComponent();

            _SysId = SysId;
            _titleName = TitleName;
            _screenWidth = this.Width;
            _screenHeight = this.Height;
        }

        #region 重写方法
        /// <summary>
        /// 控件文本显示
        /// </summary>
        public override string Text
        {
            get { return _titleName; }
        }

        /// <summary>
        /// 控件图片对象
        /// </summary>
        public override Image Image
        {
            get { return null; }
        }

        /// <summary>
        /// 初始化代码
        /// </summary>
        public override void OnInit()
        {
            var lst = BLLFactory<Sysparameter>.Instance.GetSysparameterBysysId(_SysId);
            _currPosition_Y = _currPosition_Y + 5;
            foreach (var sysparameter in lst)
            {
                InitControls(sysparameter.ID, sysparameter.Name, sysparameter.Value, sysparameter.ControlType, sysparameter.DicNo, sysparameter.Numlen);
            }
         
            this.IsInit = true;
        }

        /// <summary>
        /// 初始化界面如果遇到非法的创建控件则跳过此空间不创建
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <param name="ControlType"></param>
        /// <param name="DicNo"></param>
        private void InitControls(Int32 ID, string Name, string Value, Int16 ControlType, Int32 DicNo, Int32 Numlen)
        {
            xscControls.SuspendLayout();
            // 是否成功创建控件
            bool _createControl = true;
            LabelControl lc = new LabelControl();
            TextEdit te = null;
            ComboBoxEdit cbb = null;
            CheckedComboBoxEdit ccbb = null;
            CheckEdit ck = null;
            DateEdit de = null;
            TimeEdit time = null;

            try {
                lc.Width = Const.LabelControlWidth;
                lc.Height = Const.LabelControlHeight;
                lc.Text = Name + Const.Colon;
                lc.Left = _currPosition_X;
                lc.Top = _currPosition_Y;
                lc.AutoSizeMode = LabelAutoSizeMode.None;
                lc.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                lc.Name = string.Format("lbl{0}", ID) ;

                _currPosition_X = _currPosition_X + lc.Width + Const.ControlInterval;

                switch (ControlType)
                {
                    case 0:
                        #region 文本控件
                        te = new TextEdit();
                        ((System.ComponentModel.ISupportInitialize)(te.Properties)).BeginInit();
                        te.Width = Const.TextEditWidth;
                        te.Height = Const.TextEditHeight;
                        te.Text = Value;
                        te.Left = _currPosition_X;
                        te.Top = _currPosition_Y + Const.Three;
                        te.Tag = Name;             // 控件的tag 保存其中文名
                        te.TabIndex = ID;          // 控件的TabIndex 保存其数据库中的ID
                        te.Name = string.Format("ctl{0}", ID);

                        te.Enter += (sender, e) =>
                        {
                            _beforeStr = (sender as TextEdit).Text;
                        };
                        te.Leave += (sender, e) =>
                        {
                            var c = sender as TextEdit;
                            if (c == null)
                                return;
                            string currStr = c.Text;
                            // 此文本框有修改
                            if (!string.Equals(currStr, _beforeStr))
                            {
                                if (MeEvent != null) MeEvent(string.Format("系统参数[{0}]-{1} 修改前:{2} → 修改后:{3}", c.TabIndex, c.Tag, _beforeStr, currStr));
                                _beforeStr = string.Empty;
                            }
                            SetInfo(c.TabIndex, _SysId, currStr);
                        };
                        ((System.ComponentModel.ISupportInitialize)(te.Properties)).EndInit();
                        #endregion
                        break;
                    case 1:
                        #region 整数控件
                        te = new TextEdit();
                        ((System.ComponentModel.ISupportInitialize)(te.Properties)).BeginInit();
                        te.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                        te.Properties.Mask.EditMask = "[0-9]*";
                        te.Properties.Mask.UseMaskAsDisplayFormat = true;
                        te.Width = Const.TextEditWidth;
                        te.Height = Const.TextEditHeight;
                        te.Text = Value;
                        te.Left = _currPosition_X;
                        te.Top = _currPosition_Y + Const.Three;
                        te.Tag = Name;             // 控件的tag 保存其中文名
                        te.TabIndex = ID;          // 控件的TabIndex 保存其数据库中的ID
                        te.Name = string.Format("ctl{0}", ID);
                        if (Numlen > 0)
                        {
                            te.Properties.MaxLength = Numlen;
                        }

                        te.Enter += (sender, e) =>
                        {
                            _beforeStr = (sender as TextEdit).Text;
                        };
                        te.Leave += (sender, e) =>
                        {
                            var c = sender as TextEdit;
                            if (c == null)
                                return;
                            string currStr = c.Text;
                            // 此文本框有修改
                            if (!string.Equals(currStr, _beforeStr))
                            {
                                if (MeEvent != null) MeEvent(string.Format("系统参数[{0}]-{1} 修改前:{2} → 修改后:{3}", c.TabIndex, c.Tag, _beforeStr, currStr));
                                _beforeStr = string.Empty;
                            }
                            SetInfo(c.TabIndex, _SysId, currStr);
                        };
                        ((System.ComponentModel.ISupportInitialize)(te.Properties)).EndInit();
                        #endregion
                        break;
                    case 2:
                        #region 单选下拉框
                        cbb = new ComboBoxEdit();
                        ((System.ComponentModel.ISupportInitialize)(cbb.Properties)).BeginInit();
                        cbb.Width = Const.TextEditWidth;
                        cbb.Height = Const.TextEditHeight;
                        cbb.Name = string.Format("ctl{0}", ID);
                        cbb.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo)});
                        cbb.BindDictItems(DicNo);
                        if (!string.IsNullOrEmpty(Value)) cbb.SetComboBoxItem(Convert.ToInt32(Value));
                        cbb.Left = _currPosition_X;
                        cbb.Top = _currPosition_Y + Const.Three;
                        cbb.Tag = Name;
                        cbb.TabIndex = ID;
                        cbb.Enter += (sender, e) =>
                        {
                            _beforeStr = (sender as ComboBoxEdit).GetComboBoxIntValue().ToString();
                        };
                        cbb.Leave += (sender, e) =>
                        {
                            var c = sender as ComboBoxEdit;
                            if (c == null)
                                return;
                            string currStr = c.GetComboBoxIntValue().ToString();

                            if (currStr == Const.NoSeletValue.ToString())
                            {
                                currStr = string.Empty;
                            }

                            // 此文本框有修改
                            if (!string.Equals(currStr, _beforeStr))
                            {
                                if (MeEvent != null) MeEvent(string.Format("系统参数[{0}]-{1} 修改前:{2} → 修改后:{3}", c.TabIndex, c.Tag, _beforeStr, currStr));
                                _beforeStr = string.Empty;
                            }
                            SetInfo(c.TabIndex, _SysId, currStr);
                        };
                        ((System.ComponentModel.ISupportInitialize)(cbb.Properties)).EndInit();
                        #endregion
                        break;
                    case 3:
                        #region 多选下拉框
                        ccbb = new CheckedComboBoxEdit();
                        ((System.ComponentModel.ISupportInitialize)(ccbb.Properties)).BeginInit();
                        ccbb.Width = Const.TextEditWidth;
                        ccbb.Height = Const.TextEditHeight;
                        ccbb.Name = string.Format("ctl{0}", ID);
                        ccbb.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
                        ccbb.BindDictItems(DicNo);
                        ccbb.SetComboBoxItem(Value);
                        ccbb.Left = _currPosition_X;
                        ccbb.Top = _currPosition_Y + Const.Three;
                        ccbb.Tag = Name;
                        ccbb.TabIndex = ID;
                        ccbb.Enter += (sender, e) =>
                        {
                            _beforeStr = (sender as CheckedComboBoxEdit).GetCheckedComboBoxValue();
                        };
                        ccbb.Leave += (sender, e) =>
                        {
                            var c = sender as CheckedComboBoxEdit;
                            if (c == null)
                                return;
                            string currStr = c.GetCheckedComboBoxValue();

                            if (currStr == Const.NoSeletValue.ToString())
                            {
                                currStr = string.Empty;
                            }

                            // 此文本框有修改
                            if (!string.Equals(currStr, _beforeStr))
                            {
                                if (MeEvent != null) MeEvent(string.Format("系统参数[{0}]-{1} 修改前:{2} → 修改后:{3}", c.TabIndex, c.Tag, _beforeStr, currStr));
                                _beforeStr = string.Empty;
                            }
                            SetInfo(c.TabIndex, _SysId, currStr);
                        };
                        ((System.ComponentModel.ISupportInitialize)(ccbb.Properties)).EndInit();
                        #endregion
                        break;
                    case 4:
                        #region 勾选框
                        ck = new CheckEdit();
                        ((System.ComponentModel.ISupportInitialize)(ck.Properties)).BeginInit();

                        ck.Width = Const.TextEditWidth;
                        ck.Height = Const.TextEditHeight;
                        ck.Checked = Value == Convert.ToString(Const.One) ? true : false;
                        ck.Left = _currPosition_X;
                        ck.Top = _currPosition_Y + Const.Three;
                        ck.Tag = Name;             // 控件的tag 保存其中文名
                        ck.TabIndex = ID;          // 控件的TabIndex 保存其数据库中的ID
                        ck.Name = string.Format("ctl{0}", ID);
                        ck.Text = string.Empty;
                        ck.EditValueChanging += (sender, e) =>
                        {
                            _beforeStr = (sender as CheckEdit).Checked ? "1" : "0";
                        };
                        ck.EditValueChanged += (sender, e) =>
                        {
                            var c = sender as CheckEdit;
                            if (c == null)
                                return;
                            string currStr = (sender as CheckEdit).Checked ? "1" : "0";
                            // 此文本框有修改
                            if (!string.Equals(currStr, _beforeStr))
                            {
                                if (MeEvent != null) MeEvent(string.Format("系统参数[{0}]-{1} 修改前:{2} → 修改后:{3}", c.TabIndex, c.Tag, _beforeStr, currStr));
                                _beforeStr = string.Empty;
                            }
                            SetInfo(c.TabIndex, _SysId, currStr);
                        };
                        ((System.ComponentModel.ISupportInitialize)(ck.Properties)).EndInit();
                        #endregion
                        break;
                    case 5:
                        #region 日期
                        de = new DateEdit();
                        ((System.ComponentModel.ISupportInitialize)(de.Properties.CalendarTimeProperties)).BeginInit();
                        ((System.ComponentModel.ISupportInitialize)(de.Properties)).BeginInit();
                        de.Width = Const.TextEditWidth;
                        de.Height = Const.TextEditHeight;
                        if (!string.IsNullOrEmpty(Value)) de.SetDateTime(Convert.ToDateTime(Value));
                        de.Left = _currPosition_X;
                        de.Top = _currPosition_Y + Const.Three;
                        de.Tag = Name;             // 控件的tag 保存其中文名
                        de.TabIndex = ID;          // 控件的TabIndex 保存其数据库中的ID
                        de.Name = string.Format("ctl{0}", ID);
                        de.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo)});
                        de.Properties.CalendarTimeProperties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo)});
                        de.Properties.DisplayFormat.FormatString = Const.DateformatString;
                        de.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        de.Properties.EditFormat.FormatString = Const.DateformatString;
                        de.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        de.Properties.Mask.EditMask = Const.DateformatString;
                        de.EditValueChanging += (sender, e) =>
                        {
                            _beforeStr = (sender as DateEdit).GetDateTimeString();
                        };
                        de.EditValueChanged += (sender, e) =>
                        {
                            var c = sender as DateEdit;
                            if (c == null)
                                return;
                            string currStr = (sender as DateEdit).GetDateTimeString();
                            // 此文本框有修改
                            if (!string.Equals(currStr, _beforeStr))
                            {
                                if (MeEvent != null) MeEvent(string.Format("系统参数[{0}]-{1} 修改前:{2} → 修改后:{3}", c.TabIndex, c.Tag, _beforeStr, currStr));
                                _beforeStr = string.Empty;
                            }
                            SetInfo(c.TabIndex, _SysId, currStr);
                        };
                        ((System.ComponentModel.ISupportInitialize)(de.Properties.CalendarTimeProperties)).EndInit();
                        ((System.ComponentModel.ISupportInitialize)(de.Properties)).EndInit();
                        #endregion
                        break;
                    case 6:
                        #region 密码
                        te = new TextEdit();
                        ((System.ComponentModel.ISupportInitialize)(te.Properties)).BeginInit();
                        te.Width = Const.TextEditWidth;
                        te.Height = Const.TextEditHeight;
                        te.Text = Value;
                        te.Left = _currPosition_X;
                        te.Top = _currPosition_Y + Const.Three;
                        te.Tag = Name;             // 控件的tag 保存其中文名
                        te.TabIndex = ID;          // 控件的TabIndex 保存其数据库中的ID
                        te.Properties.PasswordChar = '*';
                        te.Name = string.Format("ctl{0}", ID);

                        te.Enter += (sender, e) =>
                        {
                            _beforeStr = (sender as TextEdit).Text;
                        };
                        te.Leave += (sender, e) =>
                        {
                            var c = sender as TextEdit;
                            if (c == null)
                                return;
                            string currStr = c.Text;
                            // 此文本框有修改
                            if (!string.Equals(currStr, _beforeStr))
                            {
                                if (MeEvent != null) MeEvent(string.Format("系统参数[{0}]-{1} 修改前:{2} → 修改后:{3}", c.TabIndex, c.Tag, _beforeStr, currStr));
                                _beforeStr = string.Empty;
                            }
                            SetInfo(c.TabIndex, _SysId, currStr);
                        };
                        ((System.ComponentModel.ISupportInitialize)(te.Properties)).EndInit();
                        #endregion
                        break;
                    case 7:
                        #region 小数框
                        te = new TextEdit();
                        ((System.ComponentModel.ISupportInitialize)(te.Properties)).BeginInit();
                        te.Properties.Mask.EditMask = string.Format("f{0}",Numlen);
                        te.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                        te.Width = Const.TextEditWidth;
                        te.Height = Const.TextEditHeight;
                        te.Text = Value;
                        te.Left = _currPosition_X;
                        te.Top = _currPosition_Y + Const.Three;
                        te.Tag = Name;             // 控件的tag 保存其中文名
                        te.TabIndex = ID;          // 控件的TabIndex 保存其数据库中的ID
                        te.Name = string.Format("ctl{0}", ID);

                        te.Enter += (sender, e) =>
                        {
                            _beforeStr = (sender as TextEdit).Text;
                        };
                        te.Leave += (sender, e) =>
                        {
                            var c = sender as TextEdit;
                            if (c == null)
                                return;
                            string currStr = c.Text;
                            // 此文本框有修改
                            if (!string.Equals(currStr, _beforeStr))
                            {
                                if (MeEvent != null) MeEvent(string.Format("系统参数[{0}]-{1} 修改前:{2} → 修改后:{3}", c.TabIndex, c.Tag, _beforeStr, currStr));
                                _beforeStr = string.Empty;
                            }
                            SetInfo(c.TabIndex, _SysId, currStr);
                        };
                        ((System.ComponentModel.ISupportInitialize)(te.Properties)).EndInit();
                        #endregion
                        break;
                    case 8:
                        #region 时间框
                        time = new TimeEdit();
                        ((System.ComponentModel.ISupportInitialize)(time.Properties)).BeginInit();
                        time.Width = Const.TextEditWidth;
                        time.Height = Const.TextEditHeight;
                        if (!string.IsNullOrEmpty(Value)) time.SetTime(Convert.ToDateTime(Value));
                        time.Left = _currPosition_X;
                        time.Top = _currPosition_Y + Const.Three;
                        time.Tag = Name;             // 控件的tag 保存其中文名
                        time.TabIndex = ID;          // 控件的TabIndex 保存其数据库中的ID
                        time.Name = string.Format("ctl{0}", ID);
                        time.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });

                        time.Enter += (sender, e) =>
                        {
                            _beforeStr = (sender as TimeEdit).GetTimeString();
                        };
                        time.Leave += (sender, e) =>
                        {
                            var c = sender as TimeEdit;
                            if (c == null)
                                return;
                            string currStr = c.GetTimeString();
                            // 此文本框有修改
                            if (!string.Equals(currStr, _beforeStr))
                            {
                                if (MeEvent != null) MeEvent(string.Format("系统参数[{0}]-{1} 修改前:{2} → 修改后:{3}", c.TabIndex, c.Tag, _beforeStr, currStr));
                                _beforeStr = string.Empty;
                            }
                            SetInfo(c.TabIndex, _SysId, currStr);
                        };
                        ((System.ComponentModel.ISupportInitialize)(time.Properties)).EndInit();
                        #endregion
                        break;
                }
                _currPosition_X = _currPosition_X + Const.TextEditWidth + Const.InfoInterval;

                // 如果屏幕宽度不够则换到下一行
                if (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 200  < _currPosition_X + Const.TextEditWidth + Const.InfoInterval)
                {
                    _currPosition_X = 0;
                    _currPosition_Y = _currPosition_Y + Const.Position_Y; 
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmSysparameter));
                _createControl = false;
                _currPosition_X = _currPosition_X - lc.Width - Const.ControlInterval;
            }

            if (te == null && cbb == null && ccbb == null && ck == null && de == null && time == null) _createControl = false;

            if (_createControl)
            {
                xscControls.Controls.Add(lc);
                if (te != null)
                    xscControls.Controls.Add(te);
                if (cbb != null)
                    xscControls.Controls.Add(cbb);
                if (ccbb != null)
                    xscControls.Controls.Add(ccbb);
                if (ck != null)
                    xscControls.Controls.Add(ck);
                if (de != null)
                    xscControls.Controls.Add(de);
                if (time != null)
                    xscControls.Controls.Add(time);
            }
            xscControls.ResumeLayout(false);
        }

        private void SetInfo(Int32 Id, Int32 sysId, string newValue)
        {
            bool isExist = false;
            foreach(var i in _info)
            {
                // 找到了
                if (i.ID == Id)
                {
                    i.sysId = sysId;
                    i.Value = newValue;
                    i.Editor = LoginUserInfo.ID.ToString();
                    i.EditorTime = DateTimeHelper.GetServerDateTime2(); //创建时间   
                    i.CurrentLoginUserId = Portal.gc.UserInfo.ID;
                    isExist = true;
                    break;
                }
            }

            if (isExist == false)
            {
                SysparameterInfo info = new SysparameterInfo();
                info.ID = Id;
                info.sysId = sysId;
                info.Value = newValue;
                info.Editor = LoginUserInfo.ID.ToString();
                info.EditorTime = DateTimeHelper.GetServerDateTime2(); //创建时间   
                info.CurrentLoginUserId = Portal.gc.UserInfo.ID;
                _info.Add(info);
            }
        }

        /// <summary>
        /// 页面激活的处理
        /// </summary>
        public override void OnSetActive()
        {
        }

        /// <summary>
        /// 保存数据事件
        /// </summary>
        /// <returns></returns>
        public override bool OnApply()
        {
            if (!HasFunction("Sysparameter/Edit"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return false;
            }

            if (Visible == false && _info.Count <= 0)
                return true;

            if (_info.Count <= 0){ MessageDxUtil.ShowWarning("无修改内容");}
            else
            {
                Int32 row_count = BLLFactory<Sysparameter>.Instance.UpdateSysparameter(_info);
                if (row_count <= 0)
                {
                    MessageDxUtil.ShowWarning("无修改内容");
                }
                else
                {
                    _info.Clear();
                    MessageDxUtil.ShowWarning(string.Format("[{0}]界面成功修改系统参数{1}条记录",Text, row_count));
                }
            }

            return true;
        }

        public override bool OnSearch()
        {
            if (!HasFunction("Sysparameter/search"))
            {
                MessageDxUtil.ShowError(Const.NoAuthMsg);
                return false;
            }

            if (Visible == false)
                return true;
 
            var dialog = new FrmSearchSysparameter();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var controls = xscControls.Controls;

                if (controls == null || controls.Count == 0) return false;

                for(Int32 i = 0; i < controls.Count; i ++)
                {
                    var c = controls[i];
                    if (c.Name.Contains("lbl"))
                    {
                        c.ForeColor = Color.Black;
                    }

                    if (string.Equals(string.Format("lbl{0}", dialog.ID), c.Name))
                    {
                        c.ForeColor = Color.Red;
                    }

                    if (string.Equals(string.Format("ctl{0}", dialog.ID), c.Name))
                    {
                        c.Focus();
                    }
                }
            }

            dialog.Close();
            dialog.Dispose();
            return true;
        }
        #endregion
    }
}
