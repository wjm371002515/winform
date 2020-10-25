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
    /// 地区业务类
    /// </summary>
	public class District : BaseBLL<DistrictInfo>
    {
        private IDistrict dal = null;

        public District() : base()
        {
            if (isMultiDatabase)
            {
                base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, dicmultiDatabase[this.GetType().Name].ToString());
            }
            else
            {
                base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            }

            baseDal.OnOperationLog += new OperationLogEventHandler(OperationLog.OnOperationLog);//如果需要记录操作日志，则实现这个事件

            dal = baseDal as IDistrict;
        }

        /// <summary>
        /// 根据城市ID获取对应的地区列表
        /// </summary>
        /// <param name="cityId">城市ID</param>
        /// <returns></returns>
        public List<DistrictInfo> GetDistrictByCityId(Int32 cityId)
        {
            string condition = string.Format("CityId={0}", cityId);
            return Find(condition);
        }

        /// <summary>
        /// 根据城市名获取对应的行政区划
        /// </summary>
        /// <param name="cityName">城市名</param>
        /// <returns></returns>
        public List<DistrictInfo> GetDistrictByCityName(string cityName)
        {
            return dal.GetDistrictByCityName(cityName);
        }
    }
}
