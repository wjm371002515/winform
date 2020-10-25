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
	/// 对象号: 000203
	/// 系统功能定义(Function)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2018-02-08 13:59:20.773
	/// </summary>
	public partial class Function : BaseDALSQLServer<FunctionInfo>, IFunction
	{
		#region 对象实例及构造函数
		public static Function Instance
		{
			get
			{
				return new Function();
			}
		}

		public Function() : base(SQLServerPortal.gc._securityTablePre + "Function", "Gid")
		{
			this.sortField = "Seq";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override FunctionInfo DataReaderToEntity(IDataReader dataReader)
		{
			FunctionInfo info = new FunctionInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Gid = reader.GetString("Gid"); 	 //GUID对应的ID序号
			info.Pgid = reader.GetString("Pgid"); 	 //父节点GUID对应的ID序号
			info.Name = reader.GetString("Name"); 	 //名称
			info.DllPath = reader.GetString("DllPath"); 	 //映射路径
			info.SystemtypeId = reader.GetString("SystemtypeId"); 	 //系统编号
			info.Seq = reader.GetString("Seq"); 	 //排序
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(FunctionInfo obj)
		{
			FunctionInfo info = obj as FunctionInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Gid", info.Gid); 	 //GUID对应的ID序号
			hash.Add("Pgid", info.Pgid); 	 //父节点GUID对应的ID序号
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("DllPath", info.DllPath); 	 //映射路径
			hash.Add("SystemtypeId", info.SystemtypeId); 	 //系统编号
			hash.Add("Seq", info.Seq); 	 //排序
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
			dict.Add("Pgid", "父节点GUID对应的ID序号");
			dict.Add("Name", "名称");
			dict.Add("DllPath", "映射路径");
			dict.Add("SystemtypeId", "系统编号");
			dict.Add("Seq", "排序");
			#endregion
			return dict;
		}
		
		/// <summary>
        /// 重写删除操作，把下面的节点提到上一级
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool DeleteByUser(object key, Int32 userId, DbTransaction trans = null)
        {
            FunctionInfo info = this.FindById(key, trans);
            if (info != null)
            {
                string sql = string.Format("UPDATE {2} SET Pgid='{0}' Where Pgid='{1}' ", info.Pgid, key, tableName);
                SqlExecute(sql, trans);

                base.DeleteByUser(key, userId, trans);

                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除指定节点及其子节点。如果该节点含有子节点，子节点也会一并删除
        /// </summary>
        /// <param name="mainID">节点ID</param>
        /// <returns></returns>
        public bool DeleteWithSubNode(string mainId, Int32 userId)
        {
            //只获取ID、PID所需字段，提高效率
            string sql = string.Format("SELECT Gid,Pgid From {0} Order By Pgid ", tableName);
            DataTable dt = SqlTable(sql);

            List<string> list = new List<string>();
            list.AddRange(GetSubNodeIdList(mainId, dt));
            list.Add(mainId);

            string idList = string.Join(",", list);
            //根据返回的ID集合，逐一删除
            foreach (string id in list)
            {
                base.DeleteByUser(id, userId);
            }
            return true;
        }

        /// <summary>
        /// 递归获取指定PID的子节点的ID集合
        /// </summary>
        /// <param name="pid">PID</param>
        /// <param name="dt">所有集合，包含ID、PID</param>
        /// <returns></returns>
        private List<string> GetSubNodeIdList(string pgid, DataTable dt)
        {
            List<string> list = new List<string>();

            DataRow[] dataRows = dt.Select(string.Format(" Pgid = '{0}'", pgid));
            for (int i = 0; i < dataRows.Length; i++)
            {
                string id = dataRows[i]["Gid"].ToString();
                list.Add(id);

                list.AddRange(GetSubNodeIdList(id, dt));//递归获取
            }
            return list;
        }

        public List<FunctionInfo> GetFunctions(string roleIds, string systemtypeId)
        {
            string sql = string.Format(@"SELECT distinct a.Gid, a.Pgid, a.Name, a.DllPath, a.SystemtypeId, a.Seq 
                                           FROM {0}Function a
                                     INNER JOIN {0}Role_Function b On a.Gid=b.FunctionGid WHERE b.RoleId IN ({1})", SQLServerPortal.gc._securityTablePre, roleIds);
            if (systemtypeId.Length > 0)
            {
                sql = sql + string.Format(" AND SystemtypeId='{0}' ", systemtypeId);
            }
            return this.GetList(sql, null);
        }

        public List<FunctionNodeInfo> GetFunctionNodes(string roleIds, string typeId)
        {
            string sql = string.Format(@"SELECT distinct a.Gid, a.Pgid, a.Name, a.DllPath, a.SystemtypeId, a.Seq  
                                           FROM {0}Function a
                                     INNER JOIN {0}Role_Function b On a.Gid=b.FunctionGid WHERE b.RoleId IN ({1})", SQLServerPortal.gc._securityTablePre, roleIds);
            if (typeId.Length > 0)
            {
                sql = sql + string.Format(" AND SystemtypeId='{0}' ", typeId);
            }

            List<FunctionNodeInfo> arrReturn = new List<FunctionNodeInfo>();
            DataTable dt = base.SqlTable(sql);
            string seq = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dataRows = dt.Select(string.Format(" Pgid = '{0}' ", -1), seq);
            for (int i = 0; i < dataRows.Length; i++)
            {
                string id = dataRows[i]["Gid"].ToString();
                FunctionNodeInfo menuNodeInfo = GetNode(id, dt);
                arrReturn.Add(menuNodeInfo);
            }

            return arrReturn;
        }

        public List<FunctionInfo> GetFunctionsByRoleId(Int32 roleId)
        {
            string sql = string.Format(@"SELECT a.Gid, a.Pgid, a.Name, a.DllPath, a.SystemtypeId, a.Seq 
                                           FROM {0}Function a
                                      LEFT JOIN {0}Role_Function b On a.Gid=b.FunctionGid WHERE b.RoleId = {1}", SQLServerPortal.gc._securityTablePre, roleId);
            return this.GetList(sql, null);
        }

        /// <summary>
        /// 获取树形结构的功能列表
        /// </summary>
        public List<FunctionNodeInfo> GetTree(string systemtypeId)
        {
            string condition = !string.IsNullOrEmpty(systemtypeId) ? string.Format("Where SystemtypeId='{0}'", systemtypeId) : "";
            List<FunctionNodeInfo> arrReturn = new List<FunctionNodeInfo>();
            string sql = string.Format("Select * From {0} {1} Order By Pgid, Name ", tableName, condition);

            DataTable dt = base.SqlTable(sql);
            string seq = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dataRows = dt.Select(string.Format(" Pgid = '{0}' ", -1), seq);
            for (int i = 0; i < dataRows.Length; i++)
            {
                string id = dataRows[i]["Gid"].ToString();
                FunctionNodeInfo menuNodeInfo = GetNode(id, dt);
                arrReturn.Add(menuNodeInfo);
            }

            return arrReturn;
        }

        /// <summary>
        /// 根据角色获取树形结构的功能列表
        /// </summary>
        public List<FunctionNodeInfo> GetTreeWithRole(string systemtypeId, List<Int32> roleList)
        {
            List<FunctionNodeInfo> list = new List<FunctionNodeInfo>();
            if (roleList.Count > 0)
            {
                string roleString = string.Join(",", roleList);

                string sql = string.Format(@"SELECT distinct F.* FROM {0}Function AS F 
                                         INNER JOIN {0}Role_Function AS RF ON F.Gid = RF.FunctionGid
                                              WHERE RF.RoleId IN ({1}) AND F.SystemtypeId = '{2}' Order By Pgid, Name ", SQLServerPortal.gc._securityTablePre, roleString, systemtypeId);

                DataTable dt = base.SqlTable(sql);
                string seq = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
                DataRow[] dataRows = dt.Select(string.Format(" Pgid = '{0}' ", -1), seq);
                for (int i = 0; i < dataRows.Length; i++)
                {
                    string id = dataRows[i]["Gid"].ToString();
                    FunctionNodeInfo menuNodeInfo = GetNode(id, dt);
                    list.Add(menuNodeInfo);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取指定功能下面的树形列表
        /// </summary>
        /// <param name="id">指定功能ID</param>
        public List<FunctionNodeInfo> GetTreeById(string mainId)
        {
            List<FunctionNodeInfo> arrReturn = new List<FunctionNodeInfo>();
            string sql = string.Format("Select * From {0} Order By Pgid, Name ", tableName);

            DataTable dt = SqlTable(sql);
            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dataRows = dt.Select(string.Format(" Pgid = '{0}'", mainId), sort);
            for (int i = 0; i < dataRows.Length; i++)
            {
                string id = dataRows[i]["Gid"].ToString();
                FunctionNodeInfo menuNodeInfo = GetNode(id, dt);
                arrReturn.Add(menuNodeInfo);
            }

            return arrReturn;
        }

        private FunctionNodeInfo GetNode(string id, DataTable dt)
        {
            FunctionInfo functionInfo = this.FindById(id);
            FunctionNodeInfo functionInfoNodeInfo = new FunctionNodeInfo(functionInfo);

            string seq = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dChildRows = dt.Select(string.Format(" Pgid='{0}'", id), seq);

            for (int i = 0; i < dChildRows.Length; i++)
            {
                string childId = dChildRows[i]["Gid"].ToString();
                FunctionNodeInfo childNodeInfo = GetNode(childId, dt);
                functionInfoNodeInfo.Children.Add(childNodeInfo);
            }
            return functionInfoNodeInfo;
        }

        /// <summary>
        /// 根据指定的父ID获取其下面一级（仅限一级）的菜单列表
        /// </summary>
        /// <param name="PID">菜单父ID</param>
        public List<FunctionInfo> GetFunctionByPgid(string pgid)
        {
            string sql = string.Format(@"Select t.*,case Pgid when '-1' then '0' else Pgid end as parentId From {1} t 
                                         Where  Pgid='{0}' Order By Seq ", pgid, tableName);
            return GetList(sql, null);
        }
	}
}