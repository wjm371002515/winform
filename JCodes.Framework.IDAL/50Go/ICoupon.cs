using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;

namespace JCodes.Framework.IDAL
{
	/// <summary>
	/// ICity 的摘要说明。
	/// </summary>
    public interface ICoupon : IBaseDAL<CouponInfo>
	{
        List<CouponInfo> GetAllCoupon();

        List<CouponInfo> GetCouponByCategory(string CouponCategoryID);

        void UseCoupon(string ID, string Editor, string Editor_ID, DateTime EditTime);
    }
}