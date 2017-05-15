
namespace JCodes.Framework.jCodesenum.BaseEnum
{
    /// <summary>
    /// 图片缩放模式
    /// </summary>
    public enum ScaleMode
    {
        /// <summary>
        /// 指定高宽缩放（可能变形）
        /// </summary>
        HW,
        /// <summary>
        /// 指定宽，高按比例
        /// </summary>
        W,
        /// <summary>
        /// 指定高，宽按比例
        /// </summary>
        H,
        /// <summary>
        /// 指定高宽裁减（不变形）
        /// </summary>
        Cut
    };
}
