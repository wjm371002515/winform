using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.jCodesenum;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace JCodes.Framework.SQLServerDAL
{
	/// <summary>
	/// 对象号: 000214
	/// 系统标识信息(SystemType)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2019-09-25 09:56:51.435
	/// </summary>
	public partial class SystemType : BaseDALSQLServer<SystemTypeInfo>, ISystemType
	{
		#region 对象实例及构造函数
		public static SystemType Instance
		{
			get
			{
				return new SystemType();
			}
		}

		public SystemType() : base(SQLServerPortal.gc._securityTablePre + "SystemType", "Gid")
		{
			this.sortField = "Gid";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override SystemTypeInfo DataReaderToEntity(IDataReader dataReader)
		{
			SystemTypeInfo info = new SystemTypeInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Gid = reader.GetString("Gid"); 	 //GUID对应的ID序号
			info.Name = reader.GetString("Name"); 	 //名称
			info.ConsumerCode = reader.GetString("ConsumerCode"); 	 //客户编码
			info.Licence = reader.GetString("Licence"); 	 //许可证
			info.Remark = reader.GetString("Remark"); 	 //备注
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(SystemTypeInfo obj)
		{
			SystemTypeInfo info = obj as SystemTypeInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Gid", info.Gid); 	 //GUID对应的ID序号
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("ConsumerCode", info.ConsumerCode); 	 //客户编码
			hash.Add("Licence", info.Licence); 	 //许可证
			hash.Add("Remark", info.Remark); 	 //备注
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
			dict.Add("Gid", "GUID对应的ID序号");
			dict.Add("Name", "名称");
			dict.Add("ConsumerCode", "客户编码");
			dict.Add("Licence", "许可证");
			dict.Add("Remark", "备注");
			#endregion
			return dict;
		}

        /// <summary>
        /// 根据系统OID获取对应的系统信息
        /// </summary>
        /// <param name="oid">系统OID</param>
        /// <returns></returns>
        public SystemTypeInfo FindByGid(string gid)
        {
            string condition = string.Format("Gid='{0}'", gid);
            return base.FindSingle(condition);
        }

        public bool VerifySystem(string licence, string systemtypeId, Int32 authorizeAmount)
        {
            Database db = CreateDatabase();
            DbCommand command = null;

            bool flag = false;
            string sql = string.Format("SELECT Count(Id) As Records FROM {0}SystemAuthorize WHERE SystemtypeId='{1}' ", SQLServerPortal.gc._securityTablePre, systemtypeId);
            command = db.GetSqlStringCommand(sql);
            int num = Convert.ToInt32(db.ExecuteScalar(command).ToString());
            if (num <= authorizeAmount)
            {
                sql = string.Format("SELECT * FROM {0}SystemAuthorize WHERE Licence='{1}'  And SystemtypeId='{2}' ", SQLServerPortal.gc._securityTablePre, licence, systemtypeId);

                command = db.GetSqlStringCommand(sql);
                using (IDataReader reader = db.ExecuteReader(command))
                {
                    flag = reader.Read();
                    reader.Close();
                }

                if (!flag)
                {
                    flag = num < authorizeAmount;
                    if (flag)
                    {
                        sql = string.Format("INSERT INTO {0}SystemAuthorize (SystemtypeId, Licence) VALUES ('{1}', '{2}') ", SQLServerPortal.gc._securityTablePre, systemtypeId, licence);
                        command = db.GetSqlStringCommand(sql);
                        db.ExecuteNonQuery(command);
                    }
                }
            }
            return flag;
        }
	}
}