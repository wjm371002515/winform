using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 地区业务类
    /// </summary>
	public class District : BaseBLL<DistrictInfo>
    {
        public District() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 根据城市ID获取对应的地区列表
        /// </summary>
        /// <param name="cityId">城市ID</param>
        /// <returns></returns>
        public List<DistrictInfo> GetDistrictByCity(string cityId)
        {
            string condition = string.Format("CityID={0}", cityId);
            return Find(condition);
        }

        /// <summary>
        /// 根据城市名获取对应的行政区划
        /// </summary>
        /// <param name="cityName">城市名</param>
        /// <returns></returns>
        public List<DistrictInfo> GetDistrictByCityName(string cityName)
        {
            IDistrict dal = baseDal as IDistrict;
            return dal.GetDistrictByCityName(cityName);
        }
    }
}
