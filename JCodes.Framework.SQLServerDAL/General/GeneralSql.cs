using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.SQLServerDAL
{
	/// <summary>
	/// City 的摘要说明。
	/// </summary>
    public class GeneralSql : BaseDALSQLServer<BaseEntity>, IGeneralSql
	{
		#region 对象实例及构造函数

		public static GeneralSql Instance
		{
			get
			{
				return new GeneralSql();
			}
		}

		#endregion
    }
}