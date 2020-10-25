using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.IDAL;

namespace JCodes.Framework.BLL
{
	public class Supplier : BaseBLL<SupplierInfo>
    {
        public Supplier()
            : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        public List<SupplierInfo> GetMangedList(string Code)
        {
            List<SupplierInfo> list = new List<SupplierInfo>();
            string condition = string.Format("Code like '%{0}%' ", Code);
            return base.Find(condition);
        }

        public override bool DeleteByUser(object key, Int32 userId, DbTransaction trans = null)
        {
            string condition = string.Format("ID ={0}", key);
            return DeleteByCondition(condition, trans);
        }

        public override bool DeleteByCondition(string condition, DbTransaction trans = null)
        {
            string newCondition = string.Format("{0}", condition);
            return base.DeleteByCondition(newCondition, trans);
        }

        public List<CListItem> GetAllSupplierDic()
        { 
            ISupplier dal = baseDal as ISupplier;
            return dal.GetAllSupplierDic();
        }
    }
}
