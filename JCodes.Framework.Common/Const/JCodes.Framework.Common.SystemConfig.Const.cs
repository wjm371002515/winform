using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Common
{
    public partial class Const
    {
        #region 主程序常量
        /// <summary>
        /// 设置jcodes 对应的版本号
        /// </summary>
        public const string jCodes_VERSION = "V1.0.0.0";

        /// <summary>
        /// 访问的官网
        /// </summary>
        public const string SystemWebUrl = "http://www.jcodes.cn";

        /// <summary>
        /// 反馈的邮箱 TODO
        /// </summary>
        public const string Feedback_Mail = "http://www.jcodes.cn";

        /// <summary>
        /// 帮助文档
        /// </summary>
        public const string HelpFile = "Help.chm";

        /// <summary>
        /// 系统编号ID TODO 待删除
        /// </summary>
        public const string SystemTypeID = "071bafed-4634-4083-bb34-86dda58edfc4";

        /// <summary>
        /// 数据库表前缀
        /// </summary>
        public const string TablePre = "T_";

        /// <summary>
        /// 界面皮肤空间（菜单界面中的 WinformClass）
        /// </summary>
        public const string RgbiSkins = "RibbonGalleryBarItem";

        /// <summary>
        /// 超链接指向http，本地的文本文件等（菜单界面中的 WinformClass）
        /// </summary>
        public const string BtnLink = "Hyperlink";

        /// <summary>
        /// 分组按钮前增加加分割线
        /// </summary>
        public const string BeginGroup = "BeginGroup";
        
        /// <summary>
        /// 启动应用程序加载内容
        /// </summary>
        public const string StartAppText = "应用程序已经在运行中...";
        
        /// <summary>
        /// 系统提示
        /// </summary>
        public const string SystemTipInfo = "系统提示";

        /// <summary>
        /// 系统错误
        /// </summary>
        public const string SystemErrInfo = "系统错误";

        /// <summary>
        /// 系统警告
        /// </summary>
        public const string SystemWarnInfo = "系统警告";

        /// <summary>
        /// 致命错误
        /// </summary>
        public const string SystemCritInfo = "致命错误";

        /// <summary>
        /// 新增
        /// </summary>
        public const string Add = "新增";

        /// <summary>
        /// 修改
        /// </summary>
        public const string Edit = "修改";

        /// <summary>
        /// 删除
        /// </summary>
        public const string Del = "删除";

        /// <summary>
        /// 重启参数
        /// </summary>
        public const string Restart = "restart";

        /// <summary>
        /// 日期格式
        /// </summary>
        public const string DateformatString = "yyyy-MM-dd";

        /// <summary>
        /// 时间格式
        /// </summary>
        public const string TimeformatString = "hh:mm:ss";



        /// <summary>
        /// 没有权限提示信息
        /// </summary>
        public const string NoAuthMsg = "您没有此权限，请与管理员联系!";

        public const string ForbidOperMsg = "不允许此操作，请与管理员联系！";

        public const string CopyOkMsg = "复制成功";

        public const string MsgCheckInput = "请输入";

        public const string MsgCheckSign = "(*)";

        public const string MsgErrFormatByNum = "格式不正确，请输入数字";

        public const string MsgIsExistMsg = "该值已存在";

        #endregion
    }
}
