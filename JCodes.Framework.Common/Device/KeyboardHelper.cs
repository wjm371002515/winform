using System.Security.Permissions;
using System.Windows.Forms;
using SendKeysProxy = System.Windows.Forms.SendKeys;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 键盘操作辅助类，提供属性访问敲击那个键，以及发送软键盘消息等操作。
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public class KeyboardHelper
    {
        #region 键盘属性

        /// <summary>
        /// 判断ALT建是否按下
        /// </summary>
        public static bool AltKeyDown
        {
            get
            {
                return ((Control.ModifierKeys & Keys.Alt) > Keys.None);
            }
        }

        /// <summary>
        /// 判断Caps Lock大写键是否打开
        /// </summary>
        public static bool CapsLock
        {
            get
            {
                return ((UnsafeNativeMethods.GetKeyState(20) & 1) > 0);
            }
        }

        /// <summary>
        /// 判断CTRL键是否按下
        /// </summary>
        public static bool CtrlKeyDown
        {
            get
            {
                return ((Control.ModifierKeys & Keys.Control) > Keys.None);
            }
        }

        /// <summary>
        /// 判断Num Lock 数字键是否打开
        /// </summary>
        public static bool NumLock
        {
            get
            {
                return ((UnsafeNativeMethods.GetKeyState(0x90) & 1) > 0);
            }
        }

        /// <summary>
        /// 判断Scroll Lock滚动锁定键是否打开
        /// </summary>
        public static bool ScrollLock
        {
            get
            {
                return ((UnsafeNativeMethods.GetKeyState(0x91) & 1) > 0);
            }
        }

        /// <summary>
        /// 判断Shift键是否按下
        /// </summary>
        public static bool ShiftKeyDown
        {
            get
            {
                return ((Control.ModifierKeys & Keys.Shift) > Keys.None);
            }
        }


        #endregion

        #region 操作方法

        /// <summary>
        /// 发送一个或多个击键到活动窗口。
        /// </summary>
        /// <param name="keys">定义发送键的字符串。特殊控制键代码SHIFT(+),CTRL(^), ALT(%)。Send("{SPACE}")、Send("+{TAB}")</param>
        public static void SendKeys(string keys)
        {
            SendKeys(keys, false);
        }

        /// <summary>
        /// 发送一个或多个击键到活动窗口
        /// </summary>
        /// <param name="keys">定义发送键的字符串.特殊控制键代码SHIFT(+),CTRL(^), ALT(%)。Send("{SPACE}")、Send("+{TAB}")</param>
        /// <param name="wait">指定是否要等待应用程序继续之前得到处理的击键。默认为True。
        /// </param>
        public static void SendKeys(string keys, bool wait)
        {
            if (wait)
            {
                SendKeysProxy.SendWait(keys);
            }
            else
            {
                SendKeysProxy.Send(keys);
            }
        }

        #endregion
    }
}
