using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 城市业务对象类
    /// </summary>
	public class CouponCategory : BaseBLL<CouponCategoryInfo>
    {
        public CouponCategory()
            : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 获取全部分组的数据
        /// </summary>
        /// <returns></returns>
        public List<CouponCategoryInfo> GetAllCouponCategory()
        {
            ICouponCategory dal = baseDal as ICouponCategory;
            return dal.GetAllCouponCategory();
        }
    }
}
