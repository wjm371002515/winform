using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 机构分类
    /// </summary>
    public enum OUCategoryEnum
    {
        集团 = 0, 公司 = 1, 部门 = 2, 工作组=3
    }

    /// <summary>
    /// 角色分类
    /// </summary>
    public enum RoleCategoryEnum
    {
        系统角色 = 0, 业务角色=1, 应用角色=3
    }

    /// <summary>
    /// 黑白名单的授权方式
    /// </summary>
    public enum AuthrizeType
    {
        黑名单 = 0, 白名单 = 1
    }

    /// <summary>
    /// 用户登录的状态
    /// </summary>
    public enum LoginStatus { NotExist, NotMatch, Forbidden, Pass };
}
