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
	/// 对象号: 000211
	/// 角色信息表(Role)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2018-02-13 10:50:38.279
	/// </summary>
	public partial class Role : BaseDALSQLServer<RoleInfo>, IRole
	{
		#region 对象实例及构造函数
		public static Role Instance
		{
			get
			{
				return new Role();
			}
		}

        public Role()
            : base(SQLServerPortal.gc._securityTablePre + "Role", "Id")
		{
			this.sortField = "Seq";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override RoleInfo DataReaderToEntity(IDataReader dataReader)
		{
			RoleInfo info = new RoleInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.RoleCode = reader.GetString("RoleCode"); 	 //角色编码
			info.Name = reader.GetString("Name"); 	 //名称
			info.Remark = reader.GetString("Remark"); 	 //备注
			info.Seq = reader.GetString("Seq"); 	 //排序
			info.CompanyId = reader.GetInt32("CompanyId"); 	 //公司Id
			info.CompanyName = reader.GetString("CompanyName"); 	 //公司名字
			info.CreatorId = reader.GetInt32("CreatorId"); 	 //创建人编号
			info.CreatorTime = reader.GetDateTime("CreatorTime"); 	 //创建时间
			info.EditorId = reader.GetInt32("EditorId"); 	 //编辑人编号
			info.LastUpdateTime = reader.GetDateTime("LastUpdateTime"); 	 //最后更新时间
			info.IsDelete = reader.GetInt16("IsDelete"); 	 //是否删除
			info.IsForbid = reader.GetInt16("IsForbid"); 	 //是否禁用
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(RoleInfo obj)
		{
			RoleInfo info = obj as RoleInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("RoleCode", info.RoleCode); 	 //角色编码
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("Remark", info.Remark); 	 //备注
			hash.Add("Seq", info.Seq); 	 //排序
			hash.Add("CompanyId", info.CompanyId); 	 //公司Id
			hash.Add("CompanyName", info.CompanyName); 	 //公司名字
			hash.Add("CreatorId", info.CreatorId); 	 //创建人编号
			hash.Add("CreatorTime", info.CreatorTime); 	 //创建时间
			hash.Add("EditorId", info.EditorId); 	 //编辑人编号
			hash.Add("LastUpdateTime", info.LastUpdateTime); 	 //最后更新时间
			hash.Add("IsDelete", info.IsDelete); 	 //是否删除
			hash.Add("IsForbid", info.IsForbid); 	 //是否禁用
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
			dict.Add("RoleCode", "角色编码");
			dict.Add("Name", "名称");
			dict.Add("Remark", "备注");
			dict.Add("Seq", "排序");
			dict.Add("CompanyId", "公司Id");
			dict.Add("CompanyName", "公司名字");
			dict.Add("CreatorId", "创建人编号");
			dict.Add("CreatorTime", "创建时间");
			dict.Add("EditorId", "编辑人编号");
			dict.Add("LastUpdateTime", "最后更新时间");
			dict.Add("IsDelete", "是否删除");
			dict.Add("IsForbid", "是否禁用");
			#endregion
			return dict;
		}

        public void AddFunction(string functionGid, Int32 roleId)
        {
            string commandText = string.Format("INSERT INTO {0}Role_Function(FunctionGid, RoleId) VALUES('{1}',{2})", SQLServerPortal.gc._securityTablePre, functionGid, roleId);

            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public void AddOU(Int32 ouId, Int32 roleId)
        {
            string commandText = string.Format("INSERT INTO {0}OU_Role(OuId, RoleId) VALUES({1},{2})", SQLServerPortal.gc._securityTablePre, ouId, roleId);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public void AddUser(Int32 userId, Int32 roleId)
        {
            string commandText = string.Format("INSERT INTO {0}User_Role(UserId, RoleId) VALUES({1},{2})", SQLServerPortal.gc._securityTablePre, userId, roleId);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 为角色指定新的人员列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="newUserList">人员列表</param>
        /// <returns></returns>
        public bool EditRoleUsers(Int32 roleId, List<Int32> newUserList)
        {
            string sql = string.Format("Delete from {0}User_Role where RoleId = {1} ", SQLServerPortal.gc._securityTablePre, roleId);
            base.SqlExecute(sql);

            foreach (Int32 userId in newUserList)
            {
                AddUser(userId, roleId);
            }
            return true;
        }

        /// <summary>
        /// 为角色指定新的操作功能列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="newFunctionList">功能列表</param>
        /// <returns></returns>
        public bool EditRoleFunctions(Int32 roleId, List<string> newFunctionList)
        {
            string sql = string.Format("Delete from {0}Role_Function where RoleId = {1} ", SQLServerPortal.gc._securityTablePre, roleId);
            base.SqlExecute(sql);

            foreach (string functionGid in newFunctionList)
            {
                AddFunction(functionGid, roleId);
            }
            return true;
        }

        /// <summary>
        /// 为角色指定新的机构列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="newOUList">机构列表</param>
        /// <returns></returns>
        public bool EditRoleOUs(Int32 roleId, List<Int32> newOUList)
        {
            string sql = string.Format("Delete from {0}OU_Role where RoleId = {1} ", SQLServerPortal.gc._securityTablePre, roleId);
            base.SqlExecute(sql);

            foreach (Int32 ouId in newOUList)
            {
                AddOU(ouId, roleId);
            }
            return true;
        }

        public List<RoleInfo> GetRolesByFunction(string functionGid, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否)
        {
            string sql = string.Format(@"SELECT a.Id, a.RoleCode, a.Name, a.Remark, a.Seq, 
                                                a.CompanyId, a.CompanyName, a.CreatorId, a.CreatorTime, a.EditorId, 
                                                a.LastUpdateTime, a.IsDelete, a.IsForbid 
                                          FROM {0}Role a
                                    INNER JOIN {0}Role_Function b On a.Id=b.RoleId WHERE b.FunctionGid ='{1}' and (a.IsDelete = {2} or 0 = {2}) and (a.IsForbid = {3} or 0 = {3})", SQLServerPortal.gc._securityTablePre, functionGid, (short)isDelete, (short)isForbid);
            return this.GetList(sql, null);
        }

        public List<RoleInfo> GetRolesByOUId(Int32 ouId, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否)
        {
            string sql = string.Format(@"SELECT a.Id, a.RoleCode, a.Name, a.Remark, a.Seq, 
                                                a.CompanyId, a.CompanyName, a.CreatorId, a.CreatorTime, a.EditorId, 
                                                a.LastUpdateTime, a.IsDelete, a.IsForbid 
                                          FROM {0}Role a INNER JOIN {0}OU_Role b ON a.Id=b.RoleId WHERE b.OuId = {1} and (a.IsDelete = {2} or 0 = {2}) and (a.IsForbid = {3} or 0 = {3})", SQLServerPortal.gc._securityTablePre, ouId, (short)isDelete, (short)isForbid);
            return this.GetList(sql, null);
        }

        public List<RoleInfo> GetRolesByUserId(Int32 userId, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否)
        {
            string sql = string.Format(@"SELECT a.Id, a.RoleCode, a.Name, a.Remark, a.Seq, 
                                                a.CompanyId, a.CompanyName, a.CreatorId, a.CreatorTime, a.EditorId, 
                                                a.LastUpdateTime, a.IsDelete, a.IsForbid 
                                          FROM {0}Role a INNER JOIN {0}User_Role b On a.Id=b.RoleId 
                                         WHERE b.UserId = {1} and (a.IsDelete = {2} or 0 = {2}) and (a.IsForbid = {3} or 0 = {3})", SQLServerPortal.gc._securityTablePre, userId, (short)isDelete, (short)isForbid);
            return this.GetList(sql, null);
        }

        public void RemoveFunction(string functionGid, Int32 roleId)
        {
            string commandText = string.Format("DELETE FROM {0}Role_Function WHERE FunctionGid='{1}' AND RoleId={2}", SQLServerPortal.gc._securityTablePre, functionGid, roleId);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public void RemoveOU(Int32 ouId, Int32 roleId)
        {
            string commandText = string.Format("DELETE FROM {0}OU_Role WHERE OuId={1} AND RoleId={2}", SQLServerPortal.gc._securityTablePre, ouId, roleId);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public void RemoveUser(Int32 userId, Int32 roleId)
        {
            string commandText = string.Format("DELETE FROM {0}User_Role WHERE UserId={1} AND RoleId={2}", SQLServerPortal.gc._securityTablePre, userId, roleId);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 设置删除标志
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <param name="deleted">是否删除</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public bool SetDeletedFlag(Int32 id, IsDelete isDelete = IsDelete.是, DbTransaction trans = null)
        {
            string sql = string.Format("Update {0} Set IsDelete={1} Where Id = {2} ", tableName, (short)isDelete, id);
            return SqlExecute(sql, trans) > 0;
        }
	}
}