using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;

namespace JCodes.Framework.SQLServerDAL
{
    /// <summary>
    /// 机构（部门）信息
    /// </summary>
    public class OU : BaseDALSQLServer<OUInfo>, IOU
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
        public OU()  : base("T_ACL_OU", "ID")
        {
            this.sortField = "SortCode";
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

            info.ID = reader.GetInt32("ID");
            info.PID = reader.GetInt32("PID");
            info.HandNo = reader.GetString("HandNo");
            info.Name = reader.GetString("Name");
            info.SortCode = reader.GetString("SortCode");
            info.Category = reader.GetString("Category");
            info.Address = reader.GetString("Address");
            info.OuterPhone = reader.GetString("OuterPhone");
            info.InnerPhone = reader.GetString("InnerPhone");
            info.Note = reader.GetString("Note");
            info.Creator = reader.GetString("Creator");
            info.Creator_ID = reader.GetString("Creator_ID");
            info.CreateTime = reader.GetDateTime("CreateTime");
            info.Editor = reader.GetString("Editor");
            info.Editor_ID = reader.GetString("Editor_ID");
            info.EditTime = reader.GetDateTime("EditTime");
            info.Deleted = reader.GetInt32("Deleted") > 0;
            info.Enabled = reader.GetInt32("Enabled") > 0;
            info.Company_ID = reader.GetString("Company_ID");
            info.CompanyName = reader.GetString("CompanyName");

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

            hash.Add("PID", info.PID);
            hash.Add("HandNo", info.HandNo);
            hash.Add("Name", info.Name);
            hash.Add("SortCode", info.SortCode);
            hash.Add("Category", info.Category);
            hash.Add("Address", info.Address);
            hash.Add("OuterPhone", info.OuterPhone);
            hash.Add("InnerPhone", info.InnerPhone);
            hash.Add("Note", info.Note);
            hash.Add("Creator", info.Creator);
            hash.Add("Creator_ID", info.Creator_ID);
            hash.Add("CreateTime", info.CreateTime);
            hash.Add("Editor", info.Editor);
            hash.Add("Editor_ID", info.Editor_ID);
            hash.Add("EditTime", info.EditTime);
            hash.Add("Deleted", info.Deleted ? 1 : 0);
            hash.Add("Enabled", info.Enabled ? 1 : 0);
            hash.Add("Company_ID", info.Company_ID);
            hash.Add("CompanyName", info.CompanyName);
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
            dict.Add("HandNo", "机构编码");
            dict.Add("Name", "机构名称");
            dict.Add("SortCode", "排序码");
            dict.Add("Category", "机构分类");
            dict.Add("Address", "机构地址");
            dict.Add("OuterPhone", "外线电话");
            dict.Add("InnerPhone", "内线电话");
            dict.Add("Note", "备注");
            dict.Add("Creator", "创建人");
            dict.Add("Creator_ID", "创建人ID");
            dict.Add("CreateTime", "创建时间");
            dict.Add("Editor", "编辑人");
            dict.Add("Editor_ID", "编辑人ID");
            dict.Add("EditTime", "编辑时间");
            dict.Add("Deleted", "是否已删除");
            dict.Add("Enabled", "有效标志");    
            dict.Add("Company_ID", "所属公司ID");
            dict.Add("CompanyName", "所属公司名称");
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
            string sql = string.Format("Delete from T_ACL_OU_User where OU_ID = {0} ", ouID);
            base.SqlExecute(sql);

            foreach (int userId in newUserList)
            {
                AddUser(userId, ouID);
            }
            return true;
        }

        public void AddUser(int userID, int ouID)
        {
            string commandText = string.Format("INSERT INTO T_ACL_OU_User(User_ID, OU_ID) VALUES({0},{1})", userID, ouID);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public void RemoveUser(int userID, int ouID)
        {
            string commandText = string.Format("DELETE FROM T_ACL_OU_User WHERE User_ID={0} AND OU_ID={1}", userID, ouID);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public override bool Delete(object key, DbTransaction trans = null)
        {
            OUInfo info = this.FindByID(key, trans);
            if (info != null)
            {
                string sql = string.Format("UPDATE [T_ACL_OU] SET PID={0} Where PID={1}", info.PID, key);
                SqlExecute(sql, trans);

                sql = string.Format("Delete From [T_ACL_OU] Where ID={0}", key);
                SqlExecute(sql, trans);
            }
            return true;
        }

        public List<OUInfo> GetOUsByRole(int roleID)
        {
            string sql = "SELECT * FROM T_ACL_OU INNER JOIN T_ACL_OU_Role On [T_ACL_OU].ID=T_ACL_OU_Role.OU_ID WHERE Role_ID = " + roleID;
            return this.GetList(sql, null);
        }

        public List<OUInfo> GetOUsByUser(int userID)
        {
            string sql = "SELECT * FROM T_ACL_OU INNER JOIN T_ACL_OU_User On [T_ACL_OU].ID=T_ACL_OU_User.OU_ID WHERE User_ID = " + userID;
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
            string sql = string.Format("Select * From {0} Where Deleted <> 1 Order By PID, Name ", tableName);

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
            DataRow[] dataRows = dt.Select(string.Format(" PID = {0} ", -1), sort);
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
            string sql = string.Format("Update {0} Set Deleted={1} Where ID = {2} ", tableName, intDeleted, id);
            return SqlExecute(sql, trans) > 0;
        }
    }
}