using System;
using System.ComponentModel;

namespace JCodes.Framework.jCodesenum.BaseEnum
{
    /// <summary> 
    /// 翻转或者滑动的动画效果方向
    /// </summary> 
    /// <remarks> 
    /// 水平和竖直方向可以组合为对角线动画效果 
    /// </remarks> 
    [Flags()]
    public enum AnimationDirection
    {
        [Description("From left to right.")]
        Right = 0x1,
        [Description("From right to left.")]
        Left = 0x2,
        [Description("From top to bottom.")]
        Down = 0x4,
        [Description("From bottom to top.")]
        Up = 0x8
    }
}
