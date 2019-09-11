using System;

namespace JCodes.Framework.jCodesenum
{
    /// <summary>
    /// 数据字典 000000
    /// </summary>
    public enum Dic000000 { 否 = 0, 是 = 1 };

    /// <summary>
    /// 数据字典 000002
    /// </summary>
    public enum Dic000002 { 正常 = 0, 冻结 = 1, 注销 = 2, 删除 = 3 };

    public enum StatisticValueType
    {
        /// <summary>
        /// 本月结存数量
        /// </summary>
        CurrentCount,

        /// <summary>
        /// 本月入库数量
        /// </summary>
        CurrentInCount,

        /// <summary>
        /// 本月出库数量
        /// </summary>
        CurrentOutCount,

        /// <summary>
        /// 上月结存数量
        /// </summary>
        LastCount,

        /// <summary>
        /// 本月入库金额
        /// </summary>
        CurrentInMoney,

        /// <summary>
        /// 本月结存金额
        /// </summary>
        CurrentMoney,

        /// <summary>
        /// 本月出库金额
        /// </summary>
        CurrentOutMoney,

        /// <summary>
        /// 上月结存金额
        /// </summary>
        LastMoney
    }

    /// <summary>
    /// 通讯录类型
    /// </summary>
    public enum AddressType { 个人, 公共 }

    /// <summary>
    /// 机构分类
    /// </summary>
    public enum OUCategoryEnum
    {
        集团 = 0, 公司 = 1, 部门 = 2, 工作组 = 3
    }

    /// <summary>
    /// 角色分类
    /// </summary>
    public enum RoleCategoryEnum
    {
        系统角色 = 0, 业务角色 = 1, 应用角色 = 3
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

    /// <summary>
    /// 个人图片分类
    /// </summary>
    [Serializable]
    public enum UserImageType
    {
        个人肖像, 身份证照片1, 身份证照片2, 名片1, 名片2
    }

    /// <summary>
    /// 采购进货退货方式
    /// </summary>
    //[DataEntity]
    //[Flags]
    public enum PuchaseStatus
    {
        进货,
        退货
    }

    /// <summary>
    /// 收入支出类型
    /// </summary>
    public enum IncomeType
    {
        收入,
        支出
    }

    public enum MonthlyReportType
    {
        库房部门结存 = 1,
        库房结存 = 2,
        所有库房结存 = 3,
        车间成本月报表 = 4,
        全年费用汇总表 = 100
    }

    public enum InformationCategory { 通知公告, 政策法规, 行业动态, 图片新闻, 其他 };

    public enum ABEdge
    {
        ABE_LEFT = 0,
        ABE_TOP,
        ABE_RIGHT,
        ABE_BOTTOM
    }

    public enum ABMsg
    {
        ABM_NEW = 0,
        ABM_REMOVE = 1,
        ABM_QUERYPOS = 2,
        ABM_SETPOS = 3,
        ABM_GETSTATE = 4,
        ABM_GETTASKBARPOS = 5,
        ABM_ACTIVATE = 6,
        ABM_GETAUTOHIDEBAR = 7,
        ABM_SETAUTOHIDEBAR = 8,
        ABM_WINDOWPOSCHANGED = 9,
        ABM_SETSTATE = 10
    }

    public enum ABState
    {
        ABS_MANUAL = 0,
        ABS_AUTOHIDE = 1,
        ABS_ALWAYSONTOP = 2,
        ABS_AUTOHIDEANDONTOP = 3,
    }

    public enum Alignment { NotSet, Left, Right, Center }

    public enum BackgroundStyles { BackwardDiagonalGradient, ForwardDiagonalGradient, HorizontalGradient, VerticalGradient, Solid };
    public enum ClockStates { Opening, Closing, Showing, None };

    public enum DisplayTreeType { OU, Role, User, Function }

    /// <summary>
    /// 文件管理状态
    /// </summary>
    public enum FileManagerStatus
    {
        NotStarted,
        Aborted,
        Complete,
        InProgress
    }
}
