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
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace JCodes.Framework.SQLServerDAL
{
	/// <summary>
	/// City 的摘要说明。
	/// </summary>
    public class CouponCategory : BaseDALSQLServer<CouponCategoryInfo>, ICouponCategory
	{
		#region 对象实例及构造函数

		public static CouponCategory Instance
		{
			get
			{
				return new CouponCategory();
			}
		}
        public CouponCategory()
            : base(SQLServerPortal.gc._50GoTablePre + "CouponCategory", "ID")
		{
            IsDescending = false;
		}

		#endregion

        public List<CouponCategoryInfo> GetAllCouponCategory()
        {
            string sql = string.Format(@"select ID, HandNo, Name, BelongCompanys, Creator, 
	                                            Creator_ID, CreateTime, Editor, Editor_ID, EditTime,
	                                            Enabled
                                           from {0}CouponCategory t
                                       order by HandNo", SQLServerPortal.gc._50GoTablePre);
            return base.GetList(sql, null);
        }

        /// <summary>
        /// 将实体对象的属性值转化为Hashtable对应的键值
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(CouponCategoryInfo obj)
        {
            CouponCategoryInfo info = obj as CouponCategoryInfo;
            Hashtable hash = new Hashtable();

            hash.Add("ID", info.ID);
            hash.Add("HandNo", info.HandNo);
            hash.Add("Name", info.Name);
            hash.Add("BelongCompanys", info.BelongCompanys);
            hash.Add("Creator", info.Creator);
            hash.Add("Creator_ID", info.Creator_ID);
            hash.Add("CreateTime", info.CreateTime);
            hash.Add("Editor", info.Editor);
            hash.Add("Editor_ID", info.Editor_ID);
            hash.Add("EditTime", info.EditTime);
            hash.Add("Enabled", info.Enabled);
            return hash;
        }
    }
}