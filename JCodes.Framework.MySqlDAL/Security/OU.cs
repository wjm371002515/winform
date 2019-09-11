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

namespace JCodes.Framework.MySqlDAL
{
    /// <summary>
    /// 机构（部门）信息
    /// </summary>
    public class OU : BaseDALMySql<OUInfo>, IOU
    {
        #region 系统所需函数
        #region 对象实例及构造函数

        public static OU Instance
        {
            get
            {
                return new OU();
            }
        }
        public OU()
            : base(MySqlPortal.gc._securityTablePre + "OU", "Id")
        {
            this.sortField = "Seq";
            this.isDescending = false;
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
            info.Id = reader.GetInt32("Id");
            info.Pid = reader.GetInt32("Pid");
            info.OuCode = reader.GetString("OuCode");
            info.Name = reader.GetString("Name");
            info.Seq = reader.GetString("Seq");
            info.OuType = reader.GetInt32("OuType");
            info.Address = reader.GetString("Address");
            info.OutPhone = reader.GetString("OutPhone");
            info.InnerPhone = reader.GetString("InnerPhone");
            info.Remark = reader.GetString("Remark");
            info.CreatorId = reader.GetInt32("CreatorId");
            info.CreatorTime = reader.GetDateTime("CreatorTime");
            info.EditorId = reader.GetInt32("EditorId");
            info.LastUpdateTime = reader.GetDateTime("LastUpdateTime");
            info.IsDelete = reader.GetInt32("IsDelete");
            info.IsForbid = reader.GetInt32("IsForbid");
            info.CompanyId = reader.GetInt32("CompanyId");

            return info;
        }

        /// <summary>
        /// 将实体对象的属性值转化为Hashtable对应的键值
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(OUInfo obj)
        {
            OUInfo info = obj as OUInfo;
            Hashtable hash = new Hashtable();
            hash.Add("Pid", info.Pid);
            hash.Add("OuCode", info.OuCode);
            hash.Add("Name", info.Name);
            hash.Add("Seq", info.Seq);
            hash.Add("OuType", info.OuType);
            hash.Add("Address", info.Address);
            hash.Add("OutPhone", info.OutPhone);
            hash.Add("InnerPhone", info.InnerPhone);
            hash.Add("Remark", info.Remark);
            hash.Add("CreatorId", info.CreatorId);
            hash.Add("CreatorTime", info.CreatorTime);
            hash.Add("EditorId", info.EditorId);
            hash.Add("LastUpdateTime", info.LastUpdateTime);
            hash.Add("IsDelete", info.IsDelete);
            hash.Add("IsForbid", info.IsForbid);
            hash.Add("CompanyId", info.CompanyId);
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
            dict.Add("ID", "编号");
            dict.Add("PID", "父ID");
            dict.Add("OuCode", "机构编码");
            dict.Add("Name", "机构名称");
            dict.Add("Seq", "排序");
            dict.Add("OuType", "机构分类");
            dict.Add("Address", "机构地址");
            dict.Add("OutPhone", "外线电话");
            dict.Add("InnerPhone", "内线电话");
            dict.Add("Remark", "备注");
            dict.Add("CreatorId", "创建人ID");
            dict.Add("CreatorTime", "创建时间");
            dict.Add("EditorId", "编辑人ID");
            dict.Add("LastUpdateTime", "编辑时间");
            dict.Add("IsDelete", "是否已删除");
            dict.Add("IsForbid", "有效标志");
            dict.Add("CompanyId", "所属公司ID");
            #endregion

            return dict;
        }

        #endregion

        /// <summary>
        /// 获取机构的名称
        /// </summary>
        /// <param name="id">机构ID</param>
        /// <returns></returns>
        public string GetName(int id, DbTransaction trans = null)
        {
            string sql = string.Format("Select Name from {0} where ID ={1} ", tableName, id);
            string result = SqlValueList(sql, trans);
            return result;
        }

        /// <summary>
        /// 为机构制定新的人员列表
        /// </summary>
        /// <param name="ouID">机构ID</param>
        /// <param name="newUserList">人员列表</param>
        /// <returns></returns>
        public bool EditOuUsers(int ouID, List<int> newUserList)
        {
            string sql = string.Format("Delete from {0}OU_User where OU_ID = {1} ",MySqlPortal.gc._securityTablePre, ouID);
            base.SqlExecute(sql);

            foreach (int userId in newUserList)
            {
                AddUser(userId, ouID);
            }
            return true;
        }

        public void AddUser(int userID, int ouID)
        {
            string commandText = string.Format("INSERT INTO {0}OU_User(User_ID, OU_ID) VALUES({1},{2})", MySqlPortal.gc._securityTablePre, userID, ouID);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public void RemoveUser(int userID, int ouID)
        {
            string commandText = string.Format("DELETE FROM {0}OU_User WHERE User_ID={1} AND OU_ID={2}", MySqlPortal.gc._securityTablePre, userID, ouID);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public override bool DeleteByUser(object key, Int32 userId, DbTransaction trans = null)
        {
            OUInfo info = this.FindByID(key, trans);
            if (info != null)
            {
                string sql = string.Format("UPDATE {0}OU SET PID={1} Where PID={2}", MySqlPortal.gc._securityTablePre, info.Pid, key);
                SqlExecute(sql, trans);

                sql = string.Format("Delete From {0}OU Where ID={1}", MySqlPortal.gc._securityTablePre, key);
                SqlExecute(sql, trans);
            }

            return true;
        }

        public List<OUInfo> GetOUsByRole(int roleID)
        {
            string sql = string.Format("SELECT * FROM {0}OU INNER JOIN {0}OU_Role On {0}OU.ID={0}OU_Role.OU_ID WHERE Role_ID = {1}", MySqlPortal.gc._securityTablePre, roleID);
            return this.GetList(sql, null);
        }

        public List<OUInfo> GetOUsByUser(int userID)
        {
            string sql = string.Format( "SELECT * FROM {0}OU INNER JOIN {0}OU_User On {0}OU.ID={0}OU_User.OU_ID WHERE User_ID = {1}",MySqlPortal.gc._securityTablePre, userID);
            return this.GetList(sql, null);
        }

        /// <summary>
        /// 根据指定机构节点ID，获取其下面所有机构列表
        /// </summary>
        /// <param name="parentId">指定机构节点ID</param>
        /// <returns></returns>
        public List<OUInfo> GetAllOUsByParent(int parentId)
        {
            List<OUInfo> list = new List<OUInfo>();
            string sql = string.Format("Select * From {0} Where IsDelete <> 1 Order By PID, Name ", tableName);

            DataTable dt = SqlTable(sql);
            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dataRows = dt.Select(string.Format(" PID = {0}", parentId), sort);
            for (int i = 0; i < dataRows.Length; i++)
            {
                string id = dataRows[i]["ID"].ToString();
                list.AddRange(GetOU(id, dt));
            }

            return list;
        }

        private List<OUInfo> GetOU(string id, DataTable dt)
        {
            List<OUInfo> list = new List<OUInfo>();

            OUInfo ouInfo = this.FindByID(id);
            list.Add(ouInfo);

            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dChildRows = dt.Select(string.Format(" PID={0} ", id), sort);
            for (int i = 0; i < dChildRows.Length; i++)
            {
                string childId = dChildRows[i]["ID"].ToString();
                List<OUInfo> childList = GetOU(childId, dt);
                list.AddRange(childList);
            }
            return list;
        }

        /// <summary>
        /// 获取树形结构的机构列表
        /// </summary>
        public List<OUNodeInfo> GetTree()
        {
            List<OUNodeInfo> arrReturn = new List<OUNodeInfo>();
            string sql = string.Format("Select * From {0} Order By PID, Name ", tableName);

            DataTable dt = base.SqlTable(sql);
            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dataRows = dt.Select(string.Format(" PID = {0} ", -1),sort);
            for (int i = 0; i < dataRows.Length; i++)
            {
                string id = dataRows[i]["ID"].ToString();
                OUNodeInfo menuNodeInfo = GetNode(id, dt);
                arrReturn.Add(menuNodeInfo);
            }

            return arrReturn;
        }

        private OUNodeInfo GetNode(string id, DataTable dt)
        {
            OUInfo ouInfo = this.FindByID(id);
            OUNodeInfo ouNodeInfo = new OUNodeInfo(ouInfo);

            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dChildRows = dt.Select(string.Format(" PID={0} ", id), sort);

            for (int i = 0; i < dChildRows.Length; i++)
            {
                string childId = dChildRows[i]["ID"].ToString();
                OUNodeInfo childNodeInfo = GetNode(childId, dt);
                ouNodeInfo.Children.Add(childNodeInfo);
            }
            return ouNodeInfo;
        }

        /// <summary>
        /// 获取指定机构下面的树形列表
        /// </summary>
        /// <param name="mainOUID">指定机构ID</param>
        public List<OUNodeInfo> GetTreeByID(int mainOUID)
        {
            List<OUNodeInfo> arrReturn = new List<OUNodeInfo>();
            string sql = string.Format("Select * From {0} Order By PID, Name ", tableName);

            DataTable dt = SqlTable(sql);
            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dataRows = dt.Select(string.Format(" PID = {0}", mainOUID), sort);
            for (int i = 0; i < dataRows.Length; i++)
            {
                string id = dataRows[i]["ID"].ToString();
                OUNodeInfo menuNodeInfo = GetNode(id, dt);
                arrReturn.Add(menuNodeInfo);
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
        public bool SetDeletedFlag(object id, bool deleted = true, DbTransaction trans = null)
        {
            int intDeleted = deleted ? 1 : 0;
            string sql = string.Format("Update {0} Set IsDelete={1} Where ID = {2} ", tableName, intDeleted, id);
            return SqlExecute(sql, trans) > 0;
        }
    }
}