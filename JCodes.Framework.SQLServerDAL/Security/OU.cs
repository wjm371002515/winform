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
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.SQLServerDAL
{
	/// <summary>
	/// 对象号: 000208
	/// 机构信息表(OU)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2018-02-08 23:24:50.166
	/// </summary>
	public partial class OU : BaseDALSQLServer<OUInfo>, IOU
	{
		#region 对象实例及构造函数
		public static OU Instance
		{
			get
			{
				return new OU();
			}
		}

		public OU() : base(SQLServerPortal.gc._securityTablePre + "OU", "Id")
		{
			this.sortField = "Seq";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override OUInfo DataReaderToEntity(IDataReader dataReader)
		{
			OUInfo info = new OUInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.Pid = reader.GetInt32("Pid"); 	 //父节点ID序号
			info.OuCode = reader.GetString("OuCode"); 	 //机构编码
			info.Name = reader.GetString("Name"); 	 //名称
			info.Seq = reader.GetString("Seq"); 	 //排序
			info.OuType = reader.GetInt16("OuType"); 	 //机构分类
			info.Address = reader.GetString("Address"); 	 //地址
			info.OutPhone = reader.GetString("OutPhone"); 	 //外线电话
			info.InnerPhone = reader.GetString("InnerPhone"); 	 //内线电话
			info.Remark = reader.GetString("Remark"); 	 //备注
			info.CreatorId = reader.GetInt32("CreatorId"); 	 //创建人编号
			info.CreatorTime = reader.GetDateTime("CreatorTime"); 	 //创建时间
			info.EditorId = reader.GetInt32("EditorId"); 	 //编辑人编号
			info.LastUpdateTime = reader.GetDateTime("LastUpdateTime"); 	 //最后更新时间
			info.IsDelete = reader.GetInt16("IsDelete"); 	 //是否删除
			info.IsForbid = reader.GetInt16("IsForbid"); 	 //是否禁用
			info.CompanyId = reader.GetInt32("CompanyId"); 	 //公司Id
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(OUInfo obj)
		{
			OUInfo info = obj as OUInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("Pid", info.Pid); 	 //父节点ID序号
			hash.Add("OuCode", info.OuCode); 	 //机构编码
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("Seq", info.Seq); 	 //排序
			hash.Add("OuType", info.OuType); 	 //机构分类
			hash.Add("Address", info.Address); 	 //地址
			hash.Add("OutPhone", info.OutPhone); 	 //外线电话
			hash.Add("InnerPhone", info.InnerPhone); 	 //内线电话
			hash.Add("Remark", info.Remark); 	 //备注
			hash.Add("CreatorId", info.CreatorId); 	 //创建人编号
			hash.Add("CreatorTime", info.CreatorTime); 	 //创建时间
			hash.Add("EditorId", info.EditorId); 	 //编辑人编号
			hash.Add("LastUpdateTime", info.LastUpdateTime); 	 //最后更新时间
			hash.Add("IsDelete", info.IsDelete); 	 //是否删除
			hash.Add("IsForbid", info.IsForbid); 	 //是否禁用
			hash.Add("CompanyId", info.CompanyId); 	 //公司Id
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
			dict.Add("Pid", "父节点ID序号");
			dict.Add("OuCode", "机构编码");
			dict.Add("Name", "名称");
			dict.Add("Seq", "排序");
			dict.Add("OuType", "机构分类");
			dict.Add("Address", "地址");
			dict.Add("OutPhone", "外线电话");
			dict.Add("InnerPhone", "内线电话");
			dict.Add("Remark", "备注");
			dict.Add("CreatorId", "创建人编号");
			dict.Add("CreatorTime", "创建时间");
			dict.Add("EditorId", "编辑人编号");
			dict.Add("LastUpdateTime", "最后更新时间");
			dict.Add("IsDelete", "是否删除");
			dict.Add("IsForbid", "是否禁用");
			dict.Add("CompanyId", "公司Id");
			#endregion
			return dict;
		}

        /// <summary>
        /// 获取机构的名称
        /// </summary>
        /// <param name="id">机构ID</param>
        /// <returns></returns>
        public string GetName(Int32 Id, DbTransaction trans = null)
        {
            string sql = string.Format("Select Name from {0} where Id ={1} ", tableName, Id);
            string result = SqlValueList(sql, trans);
            return result;
        }

        /// <summary>
        /// 为机构制定新的人员列表
        /// </summary>
        /// <param name="ouID">机构ID</param>
        /// <param name="newUserList">人员列表</param>
        /// <returns></returns>
        public bool EditOuUsers(Int32 ouId, List<Int32> newUserList)
        {
            string sql = string.Format("Delete from {0}OU_User where OuId = {1} ", SQLServerPortal.gc._securityTablePre, ouId);
            base.SqlExecute(sql);

            foreach (Int32 userId in newUserList)
            {
                AddUser(userId, ouId);
            }
            return true;
        }

        public void AddUser(Int32 userId, Int32 ouId)
        {
            string commandText = string.Format("INSERT INTO {0}OU_User(UserId, OuId) VALUES({1},{2})", SQLServerPortal.gc._securityTablePre, userId, ouId);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public void RemoveUser(Int32 userId, Int32 ouId)
        {
            string commandText = string.Format("DELETE FROM {0}OU_User WHERE UserId={1} AND OuId={2}", SQLServerPortal.gc._securityTablePre, userId, ouId);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public override bool DeleteByUser(object key, Int32 userId, DbTransaction trans = null)
        {
            OUInfo info = this.FindById(key, trans);
            if (info != null)
            {
                string sql = string.Format("UPDATE [{0}OU] SET IsDelete={1} Where Id ={0} ", key, (short)IsDelete.是);
                SqlExecute(sql, trans);
            }
            return true;
        }

        public List<OUInfo> GetOUsByRoleId(Int32 roleId, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否)
        {
            string sql = string.Format("SELECT * FROM {0}OU a INNER JOIN {0}OU_Role On a.Id={0}OU_Role.OuId WHERE RoleId = {1} and  (a.IsDelete = {2} or 0 = {2}) and (a.IsForbid = {3} or 0 = {3})", SQLServerPortal.gc._securityTablePre, roleId, (short)isDelete, (short)isForbid);
            return this.GetList(sql, null);
        }

        public List<OUInfo> GetOUsByUser(Int32 userId, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否)
        {
            string sql = string.Format("SELECT * FROM {0}OU a INNER JOIN {0}OU_User On a.Id={0}OU_User.OuId WHERE UserId = {1} and (a.IsDelete = {2} or 0 = {2}) and (a.IsForbid = {3} or 0 = {3})", SQLServerPortal.gc._securityTablePre, userId, (short)isDelete, (short)isForbid);
            return this.GetList(sql, null);
        }

        /// <summary>
        /// 根据指定机构节点ID，获取其下面所有机构列表
        /// </summary>
        /// <param name="parentId">指定机构节点ID</param>
        /// <returns></returns>
        public List<OUInfo> GetAllOUsByParent(Int32 parentId, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否)
        {
            List<OUInfo> list = new List<OUInfo>();
            string sql = string.Format("Select * From {0} Where (IsDelete = {1} or 0 = {1}) and (IsForbid = {2} or 0 = {2}) Order By Pid, Name ", tableName, (short)isDelete, (short)isForbid);

            DataTable dt = SqlTable(sql);
            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dataRows = dt.Select(string.Format(" Pid = {0}", parentId), sort);
            for (Int32 i = 0; i < dataRows.Length; i++)
            {
                Int32 id = dataRows[i]["Id"].ToString().ToInt32();
                list.AddRange(GetOU(id, dt));
            }

            return list;
        }

        private List<OUInfo> GetOU(Int32 Id, DataTable dt, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否)
        {
            List<OUInfo> list = new List<OUInfo>();

            OUInfo ouInfo = this.FindById(Id);
            list.Add(ouInfo);

            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dChildRows = dt.Select(string.Format(" Pid={0} and (IsDelete = {1} or 0 = {1}) and (IsForbid = {2} or 0 = {2})", Id, (short)isDelete, (short)isForbid), sort);
            for (Int32 i = 0; i < dChildRows.Length; i++)
            {
                Int32 childId = dChildRows[i]["Id"].ToString().ToInt32();
                List<OUInfo> childList = GetOU(childId, dt);
                list.AddRange(childList);
            }
            return list;
        }

        /// <summary>
        /// 获取树形结构的机构列表
        /// </summary>
        public List<OUNodeInfo> GetTree(IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否)
        {
            List<OUNodeInfo> arrReturn = new List<OUNodeInfo>();
            string sql = string.Format("Select * From {0} Order By Pid, Name ", tableName);

            DataTable dt = base.SqlTable(sql);
            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dataRows = dt.Select(string.Format(" Pid = {0} and (IsDelete = {1} or 0 = {1}) and (IsForbid = {2} or 0 = {2})", -1, (short)isDelete, (short)isForbid), sort);
            for (int i = 0; i < dataRows.Length; i++)
            {
                Int32 id = dataRows[i]["Id"].ToString().ToInt32();
                OUNodeInfo menuNodeInfo = GetNode(id, dt);
                arrReturn.Add(menuNodeInfo);
            }

            return arrReturn;
        }

        private OUNodeInfo GetNode(Int32 Id, DataTable dt, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否)
        {
            OUInfo ouInfo = this.FindById(Id);
            OUNodeInfo ouNodeInfo = new OUNodeInfo(ouInfo);

            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dChildRows = dt.Select(string.Format(" Pid={0} and (IsDelete = {1} or 0 = {1}) and (IsForbid = {2} or 0 = {2})", Id, (short)isDelete, (short)isForbid), sort);

            for (int i = 0; i < dChildRows.Length; i++)
            {
                Int32 childId = dChildRows[i]["Id"].ToString().ToInt32();
                OUNodeInfo childNodeInfo = GetNode(childId, dt);
                ouNodeInfo.Children.Add(childNodeInfo);
            }
            return ouNodeInfo;
        }

        /// <summary>
        /// 获取指定机构下面的树形列表
        /// </summary>
        /// <param name="mainOUId">指定机构Id</param>
        public List<OUNodeInfo> GetTreeById(int mainOUId, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否)
        {
            string sql = string.Format("Select * From {0} WHERE (IsDelete = {1} or 0 = {1}) and (IsForbid = {2} or 0 = {2}) Order By Pid, Name ", tableName, (short)isDelete, (short)isForbid);

            DataTable dt = SqlTable(sql);

            return GetRecursionOu(dt, mainOUId);
        }

        private List<OUNodeInfo> GetRecursionOu(DataTable dt, Int32 mainOUId, IsDelete isDelete = IsDelete.否, IsForbid isForbid = IsForbid.否)
        {
            List<OUNodeInfo> arrReturn = new List<OUNodeInfo>();
            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dataRows = dt.Select(string.Format(" Pid = {0} and (IsDelete = {1} or 0 = {1}) and (IsForbid = {2} or 0 = {2})", mainOUId, (short)isDelete, (short)isForbid), sort);
            for (int i = 0; i < dataRows.Length; i++)
            {
                Int32 Id = dataRows[i]["Id"].ToString().ToInt32();
                OUNodeInfo menuNodeInfo = GetNode(Id, dt);
                arrReturn.Add(menuNodeInfo);

                // 判断是否存在子节点，如果存在递归
                DataRow[] dataRowsChildNodes = dt.Select(string.Format(" Pid = {0} and (IsDelete = {1} or 0 = {1}) and (IsForbid = {2} or 0 = {2})", Id, (short)isDelete, (short)isForbid), sort);
                if (dataRowsChildNodes.Length > 0) menuNodeInfo.Children = GetRecursionOu(dt, Id);
            }

            return arrReturn;
        }

        /// <summary>
        /// 设置删除标志
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <param name="deleted">是否删除</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public bool SetDeletedFlag(Int32 Id, DbTransaction trans = null)
        {
            string sql = string.Format("Update {0} Set IsDelete={1} Where Id = {2} ", tableName, (short)IsDelete.是, Id);
            return SqlExecute(sql, trans) > 0;
        }

	}
}