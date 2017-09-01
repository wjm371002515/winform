using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.SQLServerDAL
{
	/// <summary>
	/// WareHouse 的摘要说明。
	/// </summary>
    public class Client : BaseDALSQLServer<ClientInfo>, IClient
	{
		#region 对象实例及构造函数

        public static Client Instance
		{
			get
			{
                return new Client();
			}
		}
        public Client()
            : base(SQLServerPortal.gc._wareHouseTablePre + "Client", "ID")
        {
            this.sortField = "ID";
            this.isDescending = false;
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override ClientInfo DataReaderToEntity(IDataReader dataReader)
		{
            ClientInfo info = new ClientInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);

            info.ID = reader.GetInt32("ID");
            info.Name = reader.GetString("Name");
            info.Code = reader.GetString("Code");
            info.Phone = reader.GetString("Phone");
            info.Address = reader.GetString("Address");
            info.Note = reader.GetString("Note");
            return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(ClientInfo obj)
		{
            ClientInfo info = obj as ClientInfo;
			Hashtable hash = new Hashtable(); 
			
 			hash.Add("Name", info.Name);
            hash.Add("Code", info.Code);
 			hash.Add("Phone", info.Phone);
 			hash.Add("Address", info.Address);
 			hash.Add("Note", info.Note);
 				
			return hash;
		}

        public List<CListItem> GetAllClientDic()
        {
            string sql = string.Format("select ID, Code+'('+Name+')'  as Name from {0}Client order by ID", SQLServerPortal.gc._wareHouseTablePre);
            List<CListItem> lst = new List<CListItem>();
            DataTable dt = SqlTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                CListItem cl = new CListItem(dr["ID"].ToString(), dr["Name"].ToString());
                lst.Add(cl);
            }
            return lst;
        }
    }
}