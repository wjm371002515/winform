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
	/// 对象号: 000201
	/// 黑白名单信息表(BlackIP)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2018-02-08 13:43:39.800
	/// </summary>
	public partial class BlackIP : BaseDALSQLServer<BlackIPInfo>, IBlackIP
	{
		#region 对象实例及构造函数
		public static BlackIP Instance
		{
			get
			{
				return new BlackIP();
			}
		}

		public BlackIP() : base(SQLServerPortal.gc._securityTablePre + "BlackIP", "Id")
		{
			this.sortField = "Id";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override BlackIPInfo DataReaderToEntity(IDataReader dataReader)
		{
			BlackIPInfo info = new BlackIPInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.Name = reader.GetString("Name"); 	 //名称
			info.AuthorizeType = reader.GetInt16("AuthorizeType"); 	 //授权类型
			info.IsForbid = reader.GetInt16("IsForbid"); 	 //是否禁用
			info.IPStart = reader.GetString("IPStart"); 	 //IP起始地址
			info.IPEnd = reader.GetString("IPEnd"); 	 //IP结束地址
			info.Remark = reader.GetString("Remark"); 	 //备注
			info.CreatorId = reader.GetInt32("CreatorId"); 	 //创建人编号
			info.CreatorTime = reader.GetDateTime("CreatorTime"); 	 //创建时间
			info.EditorId = reader.GetInt32("EditorId"); 	 //编辑人编号
			info.LastUpdateTime = reader.GetDateTime("LastUpdateTime"); 	 //最后更新时间
			info.StartTime = reader.GetDateTime("StartTime"); 	 //开始时间
			info.EndTime = reader.GetDateTime("EndTime"); 	 //结束时间
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(BlackIPInfo obj)
		{
			BlackIPInfo info = obj as BlackIPInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("AuthorizeType", info.AuthorizeType); 	 //授权类型
			hash.Add("IsForbid", info.IsForbid); 	 //是否禁用
			hash.Add("IPStart", info.IPStart); 	 //IP起始地址
			hash.Add("IPEnd", info.IPEnd); 	 //IP结束地址
			hash.Add("Remark", info.Remark); 	 //备注
			hash.Add("CreatorId", info.CreatorId); 	 //创建人编号
			hash.Add("CreatorTime", info.CreatorTime); 	 //创建时间
			hash.Add("EditorId", info.EditorId); 	 //编辑人编号
			hash.Add("LastUpdateTime", info.LastUpdateTime); 	 //最后更新时间
			hash.Add("StartTime", info.StartTime); 	 //开始时间
			hash.Add("EndTime", info.EndTime); 	 //结束时间
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
			dict.Add("Id", "ID序号");
			dict.Add("Name", "名称");
			dict.Add("AuthorizeType", "授权类型");
			dict.Add("IsForbid", "是否禁用");
			dict.Add("IPStart", "IP起始地址");
			dict.Add("IPEnd", "IP结束地址");
			dict.Add("Remark", "备注");
			dict.Add("CreatorId", "创建人编号");
			dict.Add("CreatorTime", "创建时间");
			dict.Add("EditorId", "编辑人编号");
			dict.Add("LastUpdateTime", "最后更新时间");
			dict.Add("StartTime", "开始时间");
			dict.Add("EndTime", "结束时间");
			#endregion
			return dict;
		}

        /// <summary>
        /// 根据黑名单ID获取对应的用户ID列表
        /// </summary>
        /// <param name="id">黑名单ID</param>
        /// <returns></returns>
        public string GetUserIdList(Int32 Id, IsForbid isForbid =  IsForbid.否)
        {
            string sql = string.Format(@"SELECT UserId FROM {0}BlackIP_User m INNER JOIN {0}BlackIP t
            ON m.BlackId=t.Id WHERE t.Id = '{1}' and (t.IsForbid = {2} or 0 = {2}) and t.StartTime <= GETDATE() and t.EndTime >= GETDATE()", SQLServerPortal.gc._securityTablePre, Id, (short)isForbid);
            return SqlValueList(sql);
        }

        public void AddUser(Int32 userId, Int32 blackId)
        {
            string commandText = string.Format("INSERT INTO {0}BlackIP_User(UserId, BlackId) VALUES({1}, '{2}')", SQLServerPortal.gc._securityTablePre, userId, blackId);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public void RemoveUser(Int32 userId, Int32 blackId)
        {
            string commandText = string.Format("DELETE FROM {0}BlackIP_User WHERE UserId={1} AND BlackId='{2}'", SQLServerPortal.gc._securityTablePre, userId, blackId);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public void RemoveUserByBlackId(Int32 blackId)
        {
            string commandText = string.Format("DELETE FROM {0}BlackIP_User WHERE BlackId='{1}'", SQLServerPortal.gc._securityTablePre, blackId);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 根据用户ID和授权类型获取列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="type">授权类型</param>
        /// <returns></returns>
        public List<BlackIPInfo> FindByUser(Int32 userId, AuthorizeType authorizeType, IsForbid isForbid = IsForbid.否)
        {
            string sql = string.Format(@"SELECT t.* FROM {0}BlackIP t INNER JOIN {0}BlackIP_User m
            ON t.Id=m.BlackId WHERE m.UserId = {1} and t.AuthorizeType={2} and (t.IsForbid = {3} or 0 = {3}) ", SQLServerPortal.gc._securityTablePre, userId, (short)authorizeType, (short)isForbid);
            return GetList(sql, null);
        }
	}
}