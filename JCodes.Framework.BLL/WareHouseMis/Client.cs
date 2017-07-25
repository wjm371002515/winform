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
	public class Client : BaseBLL<ClientInfo>
    {
        public Client()
            : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        public List<ClientInfo> GetMangedList(string Code)
        {
            List<ClientInfo> list = new List<ClientInfo>();
            string condition = string.Format("Code like '%{0}%' ", Code);
            return base.Find(condition);
        }

        public override bool DeleteByUser(object key, string userId, DbTransaction trans = null)
        {
            string condition = string.Format("ID ={0}", key);
            return DeleteByCondition(condition, trans);
        }

        public override bool DeleteByCondition(string condition, DbTransaction trans = null)
        {
            string newCondition = string.Format("{0}", condition);
            return base.DeleteByCondition(newCondition, trans);
        }

        public List<CListItem> GetAllClientDic()
        {
            IClient dal = baseDal as IClient;
            return dal.GetAllClientDic();
        }
    }
}
