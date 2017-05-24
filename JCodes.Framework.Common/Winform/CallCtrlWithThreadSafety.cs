using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.Common.Winform
{
    /// <summary>
    /// 多线程中数据的绑定和赋值辅助类
    /// </summary>
    public class CallCtrlWithThreadSafety
    {
        #region 跨线程的控件安全访问方式
        delegate void SetTextCallback2(ToolStripStatusLabel objCtrl, string text, Form winf);
        delegate void SetTextCallback(System.Windows.Forms.Control objCtrl, string text, Form winf);
        delegate void SetEnableCallback(System.Windows.Forms.Control objCtrl, bool enable, Form winf);
        delegate void SetFocusCallback(System.Windows.Forms.Control objCtrl, Form winf);
        delegate void SetCheckedCallback(System.Windows.Forms.CheckBox objCtrl, bool isCheck,Form winf);

        /// <summary>
        /// 设置控件的文本属性
        /// </summary>
        /// <typeparam name="TObject">控件对象类型</typeparam>
        /// <param name="objCtrl">控件对象</param>
        /// <param name="text">文本信息</param>
        /// <param name="winf">所在窗体</param>
        public static void SetText<TObject>(TObject objCtrl, string text, Form winf) where TObject : System.Windows.Forms.Control
        {
            if (objCtrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, text, winf });
            }
            else
            {
                objCtrl.Text = text;
            }
        }
        
        /// <summary>
        /// 设置控件的可用状态
        /// </summary>
        /// <typeparam name="TObject">控件对象类型</typeparam>
        /// <param name="objCtrl">控件对象</param>
        /// <param name="enable">控件是否可用</param>
        /// <param name="winf">所在窗体</param>
        public static void SetEnable<TObject>(TObject objCtrl, bool enable, Form winf) where TObject : System.Windows.Forms.Control
        {
            if (objCtrl.InvokeRequired)
            {
                SetEnableCallback d = new SetEnableCallback(SetEnable);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, enable,winf });
            }
            else
            {
                objCtrl.Enabled = enable;
            }
        }
        
        /// <summary>
        /// 设置控件的焦点定位
        /// </summary>
        /// <typeparam name="TObject">控件对象类型</typeparam>
        /// <param name="objCtrl">控件对象</param>
        /// <param name="winf">所在窗体</param>
        public static void SetFocus<TObject>(TObject objCtrl, Form winf) where TObject : System.Windows.Forms.Control
        {
            if (objCtrl.InvokeRequired)
            {
                SetFocusCallback d = new SetFocusCallback(SetFocus);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, winf });
            }
            else
            {
                objCtrl.Focus();
            }
        }
        
        /// <summary>
        /// 设置控件的选择状态
        /// </summary>
        /// <typeparam name="TObject">控件对象类型</typeparam>
        /// <param name="objCtrl">控件对象</param>
        /// <param name="isChecked">是否选择</param>
        /// <param name="winf">所在窗体</param>
        public static void SetChecked<TObject>(TObject objCtrl, bool isChecked,Form winf) where TObject : System.Windows.Forms.CheckBox
        {
            if (objCtrl.InvokeRequired)
            {
                SetCheckedCallback d = new SetCheckedCallback(SetChecked);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, isChecked, winf });
            }
            else
            {
                objCtrl.Checked = isChecked;
            }
        }
        
        /// <summary>
        /// 设置控件的可见状态
        /// </summary>
        /// <typeparam name="TObject">控件对象类型</typeparam>
        /// <param name="objCtrl">控件对象</param>
        /// <param name="isVisible">是否可见</param>
        /// <param name="winf">所在窗体</param>
        public static void SetVisible<TObject>(TObject objCtrl, bool isVisible, Form winf) where TObject : System.Windows.Forms.Control
        {
            if (objCtrl.InvokeRequired)
            {
                SetCheckedCallback d = new SetCheckedCallback(SetChecked);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, isVisible, winf });
            }
            else
            {
                objCtrl.Visible = isVisible;
            }
        }

        /// <summary>
        /// 设置工具状态条的文本内容
        /// </summary>
        /// <typeparam name="TObject">控件对象类型</typeparam>
        /// <param name="objCtrl">控件对象</param>
        /// <param name="text">文本信息</param>
        /// <param name="winf">所在窗体</param>
        public static void SetText2<TObject>(TObject objCtrl, string text, Form winf) where TObject : ToolStripStatusLabel
        {
            if (objCtrl.Owner.InvokeRequired)
            {
                SetTextCallback2 d = new SetTextCallback2(SetText2);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, text, winf });
            }
            else
            {
                objCtrl.Text = text;
            }
        }
        #endregion
    }
}
