using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace JCodes.Framework.CommonControl.Framework
{
    /// <summary>
    /// 邮箱设置
    /// </summary>
    public class EmailParameter
    {
        /// <summary>
        /// 邮件账号
        /// </summary>
        //[DefaultValue("wuhuacong@163.com")]
        public string Email { get; set; }

        /// <summary>
        /// POP3服务器
        /// </summary>
        [DefaultValue("pop.163.com")]
        public string Pop3Server { get; set; }

        /// <summary>
        /// POP3端口
        /// </summary>
        [DefaultValue(110)]
        public int Pop3Port { get; set; }

        /// <summary>
        /// SMTP服务器
        /// </summary>
        [DefaultValue("smtp.163.com")]
        public string SmtpServer { get; set; }

        /// <summary>
        /// SMTP端口
        /// </summary>
        [DefaultValue(25)]
        public int SmtpPort { get; set; }

        /// <summary>
        /// 登陆账号
        /// </summary>
        public string LoginId { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 使用SSL加密
        /// </summary>
        [DefaultValue(false)]
        public bool UseSSL { get; set; }
    }
}
