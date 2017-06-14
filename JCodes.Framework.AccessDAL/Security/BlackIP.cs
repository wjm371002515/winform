using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.IDAL;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;


namespace JCodes.Framework.AccessDAL
{
    /// <summary>
    /// 登陆系统的黑白名单列表
    /// </summary>
    public class BlackIP : BaseDALAccess<BlackIPInfo>, IBlackIP
    {
        #region 对象实例及构造函数

        public static BlackIP Instance
        {
            get
            {
                return new BlackIP();
            }
        }
        public BlackIP()
            : base(AccessPortal.gc._securityTablePre+"BlackIP", "ID")
        {
            this.sortField = "CreateTime";
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

            info.ID = reader.GetString("ID");
            info.Name = reader.GetString("Name");
            info.AuthorizeType = reader.GetInt32("AuthorizeType");
            info.Forbid = reader.GetInt32("Forbid") > 0;
            info.IPStart = reader.GetString("IPStart");
            info.IPEnd = reader.GetString("IPEnd");
            info.Note = reader.GetString("Note");
            info.Creator = reader.GetString("Creator");
            info.Creator_ID = reader.GetString("Creator_ID");
            info.CreateTime = reader.GetDateTime("CreateTime");
            info.Editor = reader.GetString("Editor");
            info.Editor_ID = reader.GetString("Editor_ID");
            info.EditTime = reader.GetDateTime("EditTime");

            return info;
        }

        /// <summary>
        /// 将实体对象的属性值转化为Hashtable对应的键值
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(BlackIPInfo obj)
        {
            BlackIPInfo info = obj as BlackIPInfo;
            Hashtable hash = new Hashtable();

            hash.Add("ID", info.ID);
            hash.Add("Name", info.Name);
            hash.Add("AuthorizeType", info.AuthorizeType);
            hash.Add("Forbid", info.Forbid ? 1 : 0);
            hash.Add("IPStart", info.IPStart);
            hash.Add("IPEnd", info.IPEnd);
            hash.Add("Note", info.Note);
            hash.Add("Creator", info.Creator);
            hash.Add("Creator_ID", info.Creator_ID);
            hash.Add("CreateTime", info.CreateTime);
            hash.Add("Editor", info.Editor);
            hash.Add("Editor_ID", info.Editor_ID);
            hash.Add("EditTime", info.EditTime);

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
            dict.Add("Name", "显示名称");
            dict.Add("AuthorizeType", "授权类型");
            dict.Add("Forbid", "是否禁用");
            dict.Add("IPStart", "IP起始地址");
            dict.Add("IPEnd", "IP结束地址");
            dict.Add("Note", "备注");
            dict.Add("Creator", "创建人");
            dict.Add("Creator_ID", "创建人ID");
            dict.Add("CreateTime", "创建时间");
            dict.Add("Editor", "编辑人");
            dict.Add("Editor_ID", "编辑人ID");
            dict.Add("EditTime", "编辑时间");
            #endregion

            return dict;
        }

        /// <summary>
        /// 根据黑名单ID获取对应的用户ID列表
        /// </summary>
        /// <param name="id">黑名单ID</param>
        /// <returns></returns>
        public string GetUserIdList(string id)
        {
            string sql = string.Format(@"SELECT USER_ID FROM {0}BLACKIP_USER m INNER JOIN {0}BLACKIP t
            ON m.BLACKIP_ID=t.ID WHERE t.ID = '{1}' ", AccessPortal.gc._securityTablePre, id);
            return SqlValueList(sql);
        }

        public void AddUser(int userID, string blackID)
        {
            string commandText = string.Format("INSERT INTO {0}BLACKIP_USER(User_ID, BLACKIP_ID) VALUES({1}, '{2}')", AccessPortal.gc._securityTablePre, userID, blackID);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public void RemoveUser(int userID, string blackID)
        {
            string commandText = string.Format("DELETE FROM {0}BLACKIP_USER WHERE User_ID={1} AND BLACKIP_ID='{2}'", AccessPortal.gc._securityTablePre, userID, blackID);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(commandText);
            db.ExecuteNonQuery(command);
        }

        public void RemoveUserByBlackId(string blackID)
        {
            string commandText = string.Format("DELETE FROM {0}BLACKIP_USER WHERE BLACKIP_ID='{1}'", AccessPortal.gc._securityTablePre, blackID);
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
        public List<BlackIPInfo> FindByUser(int userId, AuthrizeType type)
        {
            string sql = string.Format(@"SELECT t.* FROM {0}BLACKIP t INNER JOIN {0}BLACKIP_USER m
            ON t.ID=m.BLACKIP_ID WHERE m.User_ID = {1} and t.AuthorizeType={2} and t.Forbid=0 ", AccessPortal.gc._securityTablePre, userId, (int)type);
            return GetList(sql, null);
        }
    }
}