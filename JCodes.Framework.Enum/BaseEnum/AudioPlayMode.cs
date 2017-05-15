
namespace JCodes.Framework.jCodesenum.BaseEnum
{
    /// <summary>
    /// 声音播放的方式
    /// </summary>
    public enum AudioPlayMode
    {
        /// <summary>
        /// 播放声音，并等待，直到它完成
        /// </summary>
        WaitToComplete,

        /// <summary>
        /// 在后台播放声音。调用代码继续执行。
        /// </summary>
        Background,

        /// <summary>
        /// 在后台播放声音，直到调用stop方法。调用代码继续执行。
        /// </summary>
        BackgroundLoop
    }
}
