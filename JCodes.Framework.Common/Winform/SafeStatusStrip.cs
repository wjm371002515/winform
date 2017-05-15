using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 线程安全状态条包装类
    /// </summary>
    public class SafeStatusStrip : StatusStrip
    {
        delegate void SetText(ToolStripLabel toolStrip, string text);

        /// <summary>
        /// 设置状态条的文本
        /// </summary>
        /// <param name="toolStripLabel">状态条Lable对象</param>
        /// <param name="text">待设置的文本</param>
        public void SafeSetText(ToolStripLabel toolStripLabel, string text)
        {
            if (InvokeRequired)
            {
                SetText setTextDel = delegate(ToolStripLabel toolStrip, string textVal)
                {
                    foreach (ToolStripItem item in base.Items)
                    {
                        if (item == toolStrip)
                        {
                            item.Text = textVal;
                        }
                    }
                };

                try
                {
                    Invoke(setTextDel, new object[] { toolStripLabel, text });
                }
                catch
                {
                }
            }
            else
            {
                foreach (ToolStripItem item in base.Items)
                {
                    if (item == toolStripLabel)
                    {
                        item.Text = text;
                    }
                }
            }
        }
    }
}
