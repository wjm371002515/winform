using System;
namespace JCodes.Framework.jCodesenum
{
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