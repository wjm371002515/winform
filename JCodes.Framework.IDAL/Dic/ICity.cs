using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;

namespace JCodes.Framework.IDAL
{
	/// <summary>
	/// ICity 的摘要说明。
	/// </summary>
	public interface ICity : IBaseDAL<CityInfo>
	{
        List<CityInfo> GetCitysByProvinceName(string provinceName);
    }
}