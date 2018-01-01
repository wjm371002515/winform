using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;

namespace JCodes.Framework.IDAL
{
    /// <summary>
    /// 为测试用的数据表
    /// </summary>
	public interface ITestUser : IBaseDAL<TestUserInfo>
	{
        /// <summary>
        /// 根据个人图片类型获取图片数据
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <param name="imagetype">图片类型</param>
        /// <returns></returns>
        byte[] GetPersonImageBytes(string id, string imagetype = "个人肖像");

        /// <summary>
        /// 更新个人相关图片数据
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="imageBytes">图片字节数组</param>
        /// <param name="imagetype">图片类型</param>
        /// <returns></returns>
        bool UpdatePersonImageBytes(string id, byte[] imageBytes, string imagetype = "个人肖像");
    }
}