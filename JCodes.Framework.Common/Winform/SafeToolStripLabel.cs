using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 为ToolStripStatusLabel提供一个线程安全的包装类
    /// </summary>
    public class SafeToolStripLabel : ToolStripStatusLabel
    {
        delegate void SetText(string text);
        delegate string GetString();

        /// <summary>
        /// 文本信息
        /// </summary>
        public override string Text
        {
            get
            {
                if ((base.Parent != null) &&        // Make sure that the container is already built
                    (base.Parent.InvokeRequired))   // Is Invoke required?
                {
                    GetString getTextDel = delegate()
                                            {
                                                return base.Text;
                                            };
                    string text = String.Empty;
                    try
                    {
                        // Invoke the SetText operation from the Parent of the ToolStripStatusLabel
                        text = (string)base.Parent.Invoke(getTextDel, null);
                    }
                    catch
                    {
                    }

                    return text;
                }
                else
                {
                    return base.Text;
                }
            }

            set
            {
                // Get from the container if Invoke is required
                if ((base.Parent != null) &&        // Make sure that the container is already built
                    (base.Parent.InvokeRequired))   // Is Invoke required?
                {
                    SetText setTextDel = delegate(string text)
                    {
                        base.Text = text;
                    };

                    try
                    {
                        // Invoke the SetText operation from the Parent of the ToolStripStatusLabel
                        base.Parent.Invoke(setTextDel, new object[] { value });
                    }
                    catch
                    {
                    }
                }
                else
                {
                    base.Text = value;
                }
            }
        }
    }
}
