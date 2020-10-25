using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 基于BootStrap的图标
    /// </summary>
	public class BootstrapIcon : BaseBLL<BootstrapIconInfo>
    {
        public BootstrapIcon() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 删除指定类型的图标记录
        /// </summary>
        /// <param name="type">图标类型</param>
        /// <returns></returns>
        public bool DeleteBySourceType(string type)
        {
            string condition = string.Format("SourceType='{0}'", type);
            return baseDal.DeleteByCondition(condition);
        }

    }
}
