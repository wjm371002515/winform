using System.ComponentModel;

namespace JCodes.Framework.jCodesenum.BaseEnum
{
    /// <summary> 
    /// 动画效果的枚举
    /// </summary> 
    public enum AnimationMethod
    {
        [Description("Default animation method. Rolls out from edge when showing and into edge when hiding. Requires a direction.")]
        Roll = 0x0,
        [Description("Expands out from centre when showing and collapses into centre when hiding.")]
        Centre = 0x10,
        [Description("Slides out from edge when showing and slides into edge when hiding. Requires a direction.")]
        Slide = 0x40000,
        [Description("Fades from transaprent to opaque when showing and from opaque to transparent when hiding.")]
        Blend = 0x80000
    }
}
