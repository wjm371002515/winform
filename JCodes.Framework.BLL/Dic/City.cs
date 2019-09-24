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
	public class City : BaseBLL<CityInfo>
    {
        public City() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 根据省份ID获取对应的城市列表
        /// </summary>
        /// <param name="provinceID">省份ID</param>
        /// <returns></returns>
        public List<CityInfo> GetCitysByProvinceId(Int32 provinceId)
        {
            string condition = string.Format("ProvinceId ={0} ", provinceId);
            return baseDal.Find(condition);
        }

        /// <summary>
        /// 根据省份名称获取对应的城市列表
        /// </summary>
        /// <param name="provinceName">省份名称</param>
        /// <returns></returns>
        public List<CityInfo> GetCitysByProvinceName(string provinceName)
        {
            ICity dal = baseDal as ICity;
            return dal.GetCitysByProvinceName(provinceName);
        }
    }
}
