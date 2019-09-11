using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JCodes.Framework.CommonControl.PlugInInterface
{
    /// <summary>
    /// 登陆用户的基础信息
    /// </summary>
    public class LoginUserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 所属部门ID
        /// </summary>
        public Int32 DeptId { get; set; }

        /// <summary>
        /// 所属公司ID
        /// </summary>
        public Int32 CompanyId { get; set; }

        /// <summary>
        /// 用户登陆名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户全名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 用户性别
        /// </summary>
        public Int32 Gender  { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 移动电话   
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public Int32 QQ { get; set; }

    }
}
