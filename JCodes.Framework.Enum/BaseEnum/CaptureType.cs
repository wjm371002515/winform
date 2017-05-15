
namespace JCodes.Framework.jCodesenum.BaseEnum
{
    /// <summary>
    /// 定义屏幕抓取类型
    /// </summary>
    public enum CaptureType
    {
        /// <summary>
        /// 捕获完整的虚拟屏幕（多显示器应用程序的所有屏幕）
        /// </summary>
        VirtualScreen,

        /// <summary>
        /// 截图主显示器工作区域，并包含任务栏部分
        /// </summary>
        PrimaryScreen,

        /// <summary>
        /// 截图主显示器工作区域，不包含任务栏部分
        /// </summary>
        WorkingArea,

        /// <summary>
        /// 在多显示器屏幕中，截取所有屏幕到图片列表中
        /// </summary>
        AllScreens
    };
}
