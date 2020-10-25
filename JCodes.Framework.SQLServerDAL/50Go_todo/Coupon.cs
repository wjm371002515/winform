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
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.SQLServerDAL
{
	/// <summary>
	/// City 的摘要说明。
	/// </summary>
    public class Coupon : BaseDALSQLServer<CouponInfo>, ICoupon
	{
		#region 对象实例及构造函数

		public static Coupon Instance
		{
			get
			{
				return new Coupon();
			}
		}
        public Coupon()
            : base(SQLServerPortal.gc._50GoTablePre + "Coupon", "ID")
		{
            IsDescending = false;
		}

		#endregion

        /// <summary>
        /// 将实体对象的属性值转化为Hashtable对应的键值
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(CouponInfo obj)
        {
            CouponInfo info = obj as CouponInfo;
            Hashtable hash = new Hashtable();
            hash.Add("Id", info.Id);
            hash.Add("CouponCategoryId", info.CouponCategoryId);
            hash.Add("CouponCategoryName", info.CouponCategoryName);
            hash.Add("CompanyId", info.CompanyId);
            hash.Add("CreatorId", info.CreatorId);
            hash.Add("CreatorTime", info.CreatorTime);
            hash.Add("EditorId", info.EditorId);
            hash.Add("LastUpdateTime", info.LastUpdateTime);
            hash.Add("MobilePhone", info.MobilePhone);
            hash.Add("Contacts", info.Contacts);
            hash.Add("StartTime", info.StartTime);
            hash.Add("EndTime", info.EndTime);
            hash.Add("IsDelete", info.IsDelete);
            return hash;
        }

        /// <summary>
        /// 获取字段中文别名（用于界面显示）的字典集合
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetColumnNameAlias()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            #region 添加别名解析
            dict.Add("ID", "优惠券序列号");
            dict.Add("CouponCategory_Name", "分类名称");
            dict.Add("Company_Name", "操作公司");
            dict.Add("Creator", "创建人");
            dict.Add("Creator_ID", "创建人ID");
            dict.Add("CreateTime", "创建时间");
            dict.Add("Editor", "使用人");
            dict.Add("Editor_ID", "使用人ID");
            dict.Add("EditTime", "使用时间");
            dict.Add("MobilePhone", "联系电话");
            dict.Add("FullName", "联系人");
            dict.Add("StartTime", "有效开始日期");
            dict.Add("EndTime", "有效结束日期");
            dict.Add("DELETED", "是否使用");
            #endregion

            return dict;
        }

        public List<CouponInfo> GetAllCoupon()
        {
            string sql = string.Format(@"select ID, CouponCategory_ID, Creator, Creator_ID, CreateTime,
	                                           Editor, Editor_ID, EditTime, MobilePhone, FullName, 
	                                           StartTime, EndTime, DELETED
                                         from {0}Coupon", SQLServerPortal.gc._50GoTablePre);
            return base.GetList(sql, null);
        }

        public List<CouponInfo> GetCouponByCategory(string CouponCategoryID)
        {
            string sql = string.Format(@"select ID, CouponCategory_ID, Creator, Creator_ID, CreateTime,
	                                           Editor, Editor_ID, EditTime, MobilePhone, FullName, 
	                                           StartTime, EndTime, DELETED
                                         from {0}Coupon where CouponCategory_ID='{1}'", SQLServerPortal.gc._50GoTablePre, CouponCategoryID);
            return base.GetList(sql, null);
        }

        public void UseCoupon(string ID, string Editor, string Editor_ID, DateTime EditTime)
        {
            string sql = string.Format(@"update {0}Coupon set Editor='{1}', Editor_ID='{2}', EditTime='{3}', DELETED = 1 where ID='{4}'", SQLServerPortal.gc._50GoTablePre, Editor, Editor_ID, EditTime, ID);
            base.SqlExecute(sql, null);
        }
    }
}