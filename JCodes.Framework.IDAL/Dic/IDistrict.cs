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
	/// IDistrict 的摘要说明。
	/// </summary>
	public interface IDistrict : IBaseDAL<DistrictInfo>
	{
        /// <summary>
        /// 根据城市名获取对应的行政区划
        /// </summary>
        /// <param name="cityName">城市名</param>
        /// <returns></returns>
        List<DistrictInfo> GetDistrictByCityName(string cityName);
    }
}