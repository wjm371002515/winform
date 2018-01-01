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

namespace JCodes.Framework.OracleDAL
{
    /// <summary>
    /// 角色信息
    /// </summary>
    public class Role : BaseDALOracle<RoleInfo>, IRole
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
            : base(OraclePortal.gc._securityTablePre + "Role", "ID")
        {
            this.SeqName = string.Format("SEQ_{0}", tableName);//数值型主键，通过序列生成
            this.sortField = "Seq";
            this.isDescending = false;
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

            info.ID = reader.GetInt32("ID");
            info.PID = reader.GetInt32("PID");
            info.HandNo = reader.GetString("HandNo");
            info.Name = reader.GetString("Name");
            info.Note = reader.GetString("Note");
            info.Seq = reader.GetString("Seq");
            info.Category = reader.GetString("Category");
            info.Company_ID = reader.GetString("Company_ID");
            info.CompanyName = reader.GetString("CompanyName");
            info.Creator = reader.GetString("Creator");
            info.Creator_ID = reader.GetString("Creator_ID");
            info.CreateTime = reader.GetDateTime("CreateTime");
            info.Editor = reader.GetString("Editor");
            info.Editor_ID = reader.GetString("Editor_ID");
            info.EditTime = reader.GetDateTime("EditTime");
            info.Deleted = reader.GetInt32("Deleted") > 0;
            info.Enabled = reader.GetInt32("Enabled") > 0;

            return info;
        }

        /// <summary>
        /// 将实体对象的属性值转化为Hashtable对应的键值
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(RoleInfo obj)
        {
            RoleInfo info = obj as RoleInfo;
            Hashtable hash = new Hashtable();

            hash.Add("ID", info.ID);
            hash.Add("PID", info.PID);
            hash.Add("HandNo", info.HandNo);
            hash.Add("Name", info.Name);
            hash.Add("Note", info.Note);
            hash.Add("Seq", info.Seq);
            hash.Add("Category", info.Category);
            hash.Add("Company_ID", info.Company_ID);
            hash.Add("CompanyName", info.CompanyName);
            hash.Add("Creator", info.Creator);
            hash.Add("Creator_ID", info.Creator_ID);
            hash.Add("CreateTime", info.CreateTime);
            hash.Add("Editor", info.Editor);
            hash.Add("Editor_ID", info.Editor_ID);
            hash.Add("EditTime", info.EditTime);
            hash.Add("Deleted", info.Deleted ? 1 : 0);
            hash.Add("Enabled", info.Enabled ? 1 : 0);

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
            dict.Add("HandNo", "角色编码");
            dict.Add("Name", "角色名称");
            dict.Add("Note", "备注");
            dict.Add("Seq", "排序");
            dict.Add("Category", "角色分类");
            dict.Add("Company_ID", "所属公司ID");
            dict.Add("CompanyName", "所属公司名称");
            dict.Add("Creator", "创建人");
            dict.Add("Creator_ID", "创建人ID");
            dict.Add("CreateTime", "创建时间");
            dict.Add("Editor", "编辑人");
            dict.Add("Editor_ID", "编辑人ID");
            dict.Add("EditTime", "编辑时间");
            dict.Add("Deleted", "是否已删除");
            dict.Add("Enabled", "有效标志");
            #endregion

            return dict;
        }

        public void AddFunction(string functionID, int roleID)
        {
            string commandText = string.Format("INSERT INTO {0}Role_Function(Function_ID, Role_ID) VALUES('{1}',{2})", OraclePortal.gc._securityTablePre, functionID, roleID);

            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public void AddOU(int ouID, int roleID)
        {
            string commandText = string.Format("INSERT INTO {0}OU_Role(OU_ID, Role_ID) VALUES({1},{2})", OraclePortal.gc._securityTablePre, ouID, roleID);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public void AddUser(int userID, int roleID)
        {
            string commandText = string.Format("INSERT INTO {0}User_Role(User_ID, Role_ID) VALUES({1},{2})", OraclePortal.gc._securityTablePre, userID, roleID);
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
        public bool EditRoleUsers(int roleID, List<int> newUserList)
        {
            string sql = string.Format("Delete from {0}User_Role where Role_ID = {1} ", OraclePortal.gc._securityTablePre, roleID);
            base.SqlExecute(sql);

            foreach (int userId in newUserList)
            {
                AddUser(userId, roleID);
            }
            return true;
        }

        /// <summary>
        /// 为角色指定新的操作功能列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="newFunctionList">功能列表</param>
        /// <returns></returns>
        public bool EditRoleFunctions(int roleID, List<string> newFunctionList)
        {
            string sql = string.Format("Delete from {0}Role_Function where Role_ID = {1} ", OraclePortal.gc._securityTablePre, roleID);
            base.SqlExecute(sql);

            foreach (string functionId in newFunctionList)
            {
                AddFunction(functionId, roleID);
            }
            return true;
        }

        /// <summary>
        /// 为角色指定新的机构列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="newOUList">机构列表</param>
        /// <returns></returns>
        public bool EditRoleOUs(int roleID, List<int> newOUList)
        {
            string sql = string.Format("Delete from {0}OU_Role where Role_ID = {1} ", OraclePortal.gc._securityTablePre, roleID);
            base.SqlExecute(sql);

            foreach (int ouId in newOUList)
            {
                AddOU(ouId, roleID);
            }
            return true;
        }

        public List<RoleInfo> GetRolesByFunction(string functionID)
        {
            string sql = string.Format(@"SELECT * FROM {0}Role 
            INNER JOIN {0}Role_Function On {0}Role.ID={0}Role_Function.Role_ID WHERE Function_ID ='{1}' ", OraclePortal.gc._securityTablePre, functionID);
            return this.GetList(sql, null);
        }

        public List<RoleInfo> GetRolesByOU(int ouID)
        {
            string sql = string.Format( "SELECT * FROM {0}Role INNER JOIN {0}OU_Role ON {0}Role.ID=Role_ID WHERE OU_ID = {1}", OraclePortal.gc._securityTablePre, ouID);
            return this.GetList(sql, null);
        }

        public List<RoleInfo> GetRolesByUser(int userID)
        {
            string sql = string.Format("SELECT * FROM {0}Role INNER JOIN {0}User_Role On {0}Role.ID={0}User_Role.Role_ID WHERE User_ID = {1}",OraclePortal.gc._securityTablePre, userID);
            return this.GetList(sql, null);
        }

        public void RemoveFunction(string functionID, int roleID)
        {
            string commandText = string.Format("DELETE FROM {0}Role_Function WHERE Function_ID='{1}' AND Role_ID={2}", OraclePortal.gc._securityTablePre, functionID, roleID);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public void RemoveOU(int ouID, int roleID)
        {
            string commandText = string.Format("DELETE FROM {0}OU_Role WHERE OU_ID={1} AND Role_ID={2}", OraclePortal.gc._securityTablePre, ouID, roleID);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public void RemoveUser(int userID, int roleID)
        {
            string commandText = string.Format("DELETE FROM {0}User_Role WHERE User_ID={1} AND Role_ID={2}",OraclePortal.gc._securityTablePre, userID, roleID);
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
        public bool SetDeletedFlag(object id, bool deleted = true, DbTransaction trans = null)
        {
            int intDeleted = deleted ? 1 : 0;
            string sql = string.Format("Update {0} Set Deleted={1} Where ID = {2} ", tableName, intDeleted, id);
            return SqlExecute(sql, trans) > 0;
        }
    }
}