using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 托盘图标辅助类
    /// </summary>
    public class NotifyIconHelper
    {
        /// <summary>
        /// 状态枚举：离线、在线、闪烁
        /// </summary>
        

        Status status;
        NotifyIcon notifyIcon;
        Timer timer;
        bool isBlank = false;

        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="notifyIcon">指定图标</param>
        public NotifyIconHelper(NotifyIcon notifyIcon)
        {
            this.notifyIcon = notifyIcon;
            this.NotifyStatus = Status.Offline;
            this.timer = new Timer();
            this.timer.Interval = 500;
            this.timer.Tick += new EventHandler(timer_Tick);
        }

        /// <summary>
        /// 未连接图标（如网络断开）
        /// </summary>
        public Icon Icon_UnConntect { get; set;}
        
        /// <summary>
        /// 已连接图标（如网络连接）
        /// </summary>
        public Icon Icon_Conntected { get; set; }

        /// <summary>
        /// 未连接上的托盘文本提示
        /// </summary>
        public string Text_UnConntect { get; set;}
        
        /// <summary>
        /// 连接后文本提示
        /// </summary>
        public string Text_Conntected { get; set;}
        
        /// <summary>
        /// 闪烁图标1
        /// </summary>
        public Icon Icon_Shrink1 { get; set;}
        
        /// <summary>
        /// 闪烁图标2
        /// </summary>
        public Icon Icon_Shrink2 { get; set;}

        /// <summary>
        /// 获取或设置托盘的状态
        /// </summary>
        public Status NotifyStatus
        {
            get { return this.status; }
            set
            {
                if (value == this.status) return;

                if (status == Status.TwinkleNotice && timer != null)
                {
                    timer.Stop();
                }

                this.status = value;

                Refresh();
            }
        }
        

        /// <summary>
        /// 刷新托盘图标的状态
        /// </summary>
        public void Refresh()
        {
            switch (status)
            {
                case Status.Offline:
                    notifyIcon.Icon = Icon_UnConntect;
                    notifyIcon.Text = Text_UnConntect;
                    break;

                case Status.Online:
                    notifyIcon.Icon = Icon_Conntected;
                    notifyIcon.Text = Text_Conntected;
                    break;

                case Status.TwinkleNotice:
                    timer.Start();
                    break;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            notifyIcon.Icon = isBlank ? Icon_Shrink1 : Icon_Shrink2;
            isBlank = !isBlank;
        }
    }
}
