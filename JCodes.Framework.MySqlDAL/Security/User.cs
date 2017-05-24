using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.MySqlDAL
{
    /// <summary>
    /// 系统用户信息
    /// </summary>
    public class User : BaseDALMySql<UserInfo>, IUser
    {
        #region 对象实例及构造函数

        public static User Instance
        {
            get
            {
                return new User();
            }
        }
        public User()
            : base("T_ACL_User", "ID")
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
        protected override UserInfo DataReaderToEntity(IDataReader dataReader)
        {
            UserInfo info = new UserInfo();
            SmartDataReader reader = new SmartDataReader(dataReader);

            info.ID = reader.GetInt32("ID");
            info.PID = reader.GetInt32("PID");
            info.HandNo = reader.GetString("HandNo");
            info.Name = reader.GetString("Name");
            info.Password = reader.GetString("Password");
            info.FullName = reader.GetString("FullName");
            info.Nickname = reader.GetString("Nickname");
            info.IsExpire = reader.GetBoolean("IsExpire");
            info.Title = reader.GetString("Title");
            info.IdentityCard = reader.GetString("IdentityCard");
            info.MobilePhone = reader.GetString("MobilePhone");
            info.OfficePhone = reader.GetString("OfficePhone");
            info.HomePhone = reader.GetString("HomePhone");
            info.Email = reader.GetString("Email");
            info.Address = reader.GetString("Address");
            info.WorkAddr = reader.GetString("WorkAddr");
            info.Gender = reader.GetString("Gender");
            info.Birthday = reader.GetDateTime("Birthday");
            info.QQ = reader.GetString("QQ");
            info.Signature = reader.GetString("Signature");
            info.AuditStatus = reader.GetString("AuditStatus");
            //info.Portrait = reader.GetBytes("Portrait");
            info.Note = reader.GetString("Note");
            info.CustomField = reader.GetString("CustomField");
            info.Dept_ID = reader.GetString("Dept_ID");
            info.DeptName = reader.GetString("DeptName");
            info.Company_ID = reader.GetString("Company_ID");
            info.CompanyName = reader.GetString("CompanyName");
            info.SortCode = reader.GetString("SortCode");
            info.Creator = reader.GetString("Creator");
            info.Creator_ID = reader.GetString("Creator_ID");
            info.CreateTime = reader.GetDateTime("CreateTime");
            info.Editor = reader.GetString("Editor");
            info.Editor_ID = reader.GetString("Editor_ID");
            info.EditTime = reader.GetDateTime("EditTime");
            info.Deleted = reader.GetInt32("Deleted") > 0;
			info.CurrentLoginIP = reader.GetString("CurrentLoginIP");
			info.CurrentLoginTime = reader.GetDateTime("CurrentLoginTime");
			info.CurrentMacAddress = reader.GetString("CurrentMacAddress");

            return info;
        }

        /// <summary>
        /// 将实体对象的属性值转化为Hashtable对应的键值
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(UserInfo obj)
        {
            UserInfo info = obj as UserInfo;
            Hashtable hash = new Hashtable();

            hash.Add("PID", info.PID);
            hash.Add("HandNo", info.HandNo);
            hash.Add("Name", info.Name);
            hash.Add("Password", info.Password);
            hash.Add("FullName", info.FullName);
            hash.Add("Nickname", info.Nickname);
            hash.Add("IsExpire", info.IsExpire);
            hash.Add("Title", info.Title);
            hash.Add("IdentityCard", info.IdentityCard);
            hash.Add("MobilePhone", info.MobilePhone);
            hash.Add("OfficePhone", info.OfficePhone);
            hash.Add("HomePhone", info.HomePhone);
            hash.Add("Email", info.Email);
            hash.Add("Address", info.Address);
            hash.Add("WorkAddr", info.WorkAddr);
            hash.Add("Gender", info.Gender);
            hash.Add("Birthday", info.Birthday);
            hash.Add("QQ", info.QQ);
            hash.Add("Signature", info.Signature);
            hash.Add("AuditStatus", info.AuditStatus);
            //hash.Add("Portrait", info.Portrait);
            hash.Add("Note", info.Note);
            hash.Add("CustomField", info.CustomField);
            hash.Add("Dept_ID", info.Dept_ID);
            hash.Add("DeptName", info.DeptName);
            hash.Add("Company_ID", info.Company_ID);
            hash.Add("CompanyName", info.CompanyName);
            hash.Add("SortCode", info.SortCode);
            hash.Add("Creator", info.Creator);
            hash.Add("Creator_ID", info.Creator_ID);
            hash.Add("CreateTime", info.CreateTime);
            hash.Add("Editor", info.Editor);
            hash.Add("Editor_ID", info.Editor_ID);
            hash.Add("EditTime", info.EditTime);
            hash.Add("Deleted", info.Deleted ? 1 : 0);
            hash.Add("CurrentLoginIP", info.CurrentLoginIP);
            hash.Add("CurrentLoginTime", info.CurrentLoginTime);
            hash.Add("CurrentMacAddress", info.CurrentMacAddress);

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
            dict.Add("HandNo", "用户编码");
            dict.Add("Name", "用户名/登录名");
            dict.Add("Password", "用户密码");
            dict.Add("FullName", "用户全名");
            dict.Add("Nickname", "用户呢称");
            dict.Add("IsExpire", "是否过期");
            dict.Add("Title", "职务头衔");
            dict.Add("IdentityCard", "身份证号码");
            dict.Add("MobilePhone", "移动电话");
            dict.Add("OfficePhone", "办公电话");
            dict.Add("HomePhone", "家庭电话");
            dict.Add("Email", "邮件地址");
            dict.Add("Address", "住址");
            dict.Add("WorkAddr", "办公地址");
            dict.Add("Gender", "性别");
            dict.Add("Birthday", "出生日期");
            dict.Add("Qq", "QQ号码");
            dict.Add("Signature", "个性签名");
            dict.Add("AuditStatus", "审核状态");
            dict.Add("Portrait", "个人图片");
            dict.Add("Note", "备注");
            dict.Add("CustomField", "自定义字段");
            dict.Add("Dept_ID", "默认部门ID");
            dict.Add("DeptName", "默认部门名称");
            dict.Add("Company_ID", "所属公司ID");
            dict.Add("CompanyName", "所属公司名称");
            dict.Add("SortCode", "排序码");
            dict.Add("Creator", "创建人");
            dict.Add("Creator_ID", "创建人ID");
            dict.Add("CreateTime", "创建时间");
            dict.Add("Editor", "编辑人");
            dict.Add("Editor_ID", "编辑人ID");
            dict.Add("EditTime", "编辑时间");
            dict.Add("Deleted", "是否已删除");
            dict.Add("Question", "密保：提示问题");
            dict.Add("Answer", "密保:问题答案");
            dict.Add("LastLoginIP", "最后登录IP");
            dict.Add("LastLoginTime", "最后登录时间");
            dict.Add("CurrentLoginIP", "当前登录IP");
            dict.Add("CurrentLoginTime", "当前登录时间");
            dict.Add("LastPasswordTime", "最后修改密码日期");
            #endregion

            return dict;
        }

        /// <summary>
        /// 重写删除操作，删除关联的信息
        /// </summary>
        /// <param name="key">ID值</param>
        /// <returns></returns>
        public override bool Delete(object key, DbTransaction trans = null)
        {
            UserInfo info = this.FindByID(key, trans);
            if (info != null)
            {
                string sql = string.Format("UPDATE {2} SET PID={0} Where PID={1}", info.PID, key, tableName);
                SqlExecute(sql, trans);

                sql = string.Format("Delete From {1} Where ID ={0} ", key, tableName);
                SqlExecute(sql, trans);
            }
            return true;
        }

        /// <summary>
        /// 构造一个简单用户信息类集合
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<SimpleUserInfo> FillSimpleUsers(string sql)
        {
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);

            List<SimpleUserInfo> list = new List<SimpleUserInfo>();
            using (IDataReader reader = db.ExecuteReader(command))
            {
                SmartDataReader dr = new SmartDataReader(reader);
                while (reader.Read())
                {
                    SimpleUserInfo info = new SimpleUserInfo();
                    info.ID = dr.GetInt32("ID");
                    info.Name = dr.GetString("Name");
                    info.Password = dr.GetString("Password");
                    info.FullName = dr.GetString("FullName");
                    info.HandNo = dr.GetString("HandNo");
                    info.MobilePhone = dr.GetString("MobilePhone");
                    info.Email = dr.GetString("Email");
                    list.Add(info);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据查询条件获取简单用户对象列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public List<SimpleUserInfo> FindSimpleUsers(string condition)
        {
            if (HasInjectionData(condition))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("检测出SQL注入的恶意数据, {0}", condition), typeof(User));
                throw new Exception("检测出SQL注入的恶意数据");
            }

            //串连条件语句为一个完整的Sql语句
            string sql = string.Format("Select ID,Name,Password,FullName,HandNo,MobilePhone,Email From {0} Where Deleted = 0 ", tableName);
            if (!string.IsNullOrEmpty(condition))
            {
                sql += string.Format(" AND {0} ", condition);
            }
            sql += string.Format(" Order by {0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");

            return FillSimpleUsers(sql);
        }

        /// <summary>
        /// 获取全部的简单用户对象列表
        /// </summary>
        /// <returns></returns>
        public List<SimpleUserInfo> GetSimpleUsers()
        {
            return this.FindSimpleUsers(null);
        }

        /// <summary>
        /// 获取指定用户Id字符串的简单用户对象列表
        /// </summary>
        /// <param name="userIDs"></param>
        /// <returns></returns>
        public List<SimpleUserInfo> GetSimpleUsers(string userIDs)
        {
            string condition = string.Format(" ID In ({0})", userIDs);
            return this.FindSimpleUsers(condition);
        }

        /// <summary>
        /// 根据机构ID获取对应关系的用户简单对象列表
        /// </summary>
        /// <param name="ouID">机构ID</param>
        /// <returns></returns>
        public List<SimpleUserInfo> GetSimpleUsersByOU(int ouID)
        {
            string sql = string.Format(@"Select ID,Name,Password,FullName,HandNo,MobilePhone,Email From {0} 
            Inner Join T_ACL_OU_User ON {0}.ID=User_ID Where Deleted = 0 AND T_ACL_OU_User.OU_ID = {1}", tableName, ouID);
            return FillSimpleUsers(sql);
        }

        public List<SimpleUserInfo> GetSimpleUsersByRole(int roleID)
        {
            string sql = string.Format(@"Select ID,Name,Password,FullName,HandNo,MobilePhone,Email From {0} 
            INNER JOIN T_ACL_User_Role ON {0}.ID=User_ID Where Deleted = 0 AND T_ACL_User_Role.Role_ID ={1} ", tableName, roleID);

            return this.FillSimpleUsers(sql);
        }

        public List<UserInfo> GetUsersByOU(int ouID)
        {
            string sql = string.Format(@"SELECT * FROM {0} INNER JOIN T_ACL_OU_User On {0}.ID=T_ACL_OU_User.User_ID 
            WHERE Deleted = 0 AND T_ACL_OU_User.OU_ID ={1} ", tableName, ouID);
            return GetList(sql, null);
        }

        public List<UserInfo> GetUsersByRole(int roleID)
        {
            string sql = string.Format(@"SELECT * FROM {0} INNER JOIN T_ACL_User_Role On {0}.ID=T_ACL_User_Role.User_ID 
            WHERE Deleted = 0 AND T_ACL_User_Role.Role_ID = {1}", tableName, roleID);
            return this.GetList(sql, null);
        }

        /// <summary>
        /// 根据用户ID获取用户全名称
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public string GetFullNameByID(int userID)
        {
            string sql = string.Format("Select FullName from {0} Where ID={1} ", tableName, userID);
            return SqlValueList(sql);
        }

        /// <summary>
        /// 根据用户登陆名称，获取用户全名
        /// </summary>
        /// <param name="userName">用户登陆名称</param>
        /// <returns></returns>
        public string GetFullNameByName(string userName)
        {
            string sql = string.Format("Select FullName from {0} Where Name='{1}' ", tableName, userName);
            return SqlValueList(sql);
        }

        /// <summary>
        /// 根据个人图片枚举类型获取图片数据
        /// </summary>
        /// <param name="imagetype">图片枚举类型</param>
        /// <returns></returns>
        public byte[] GetPersonImageBytes(UserImageType imagetype, int userId)
        {
            return null;
        }

        /// <summary>
        /// 更新个人相关图片数据
        /// </summary>
        /// <param name="imagetype">图片类型</param>
        /// <param name="userId">用户ID</param>
        /// <param name="imageBytes">图片字节数组</param>
        /// <returns></returns>
        public bool UpdatePersonImageBytes(UserImageType imagetype, int userId, byte[] imageBytes)
        {
            return false;
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

        /// <summary>
        /// 更新用户登录的时间和IP地址
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="ip">IP地址</param>
        /// <param name="macAddr">MAC地址</param>
        /// <returns></returns>
        public bool UpdateUserLoginData(int id, string ip, string macAddr)
        {
            //先复制最后的登录时间和IP地址
            string sql = string.Format("Update {0} set LastLoginIP=CurrentLoginIP,LastLoginTime=CurrentLoginTime,LastMacAddress=CurrentMacAddress Where ID={1}", tableName, id);
            Database db = CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.ExecuteNonQuery(dbCommand);

            sql = string.Format("Update {0} Set CurrentLoginIP='{1}',CurrentMacAddress='{2}', CurrentLoginTime={4}CurrentLoginTime Where ID = {3}", tableName, ip, macAddr, id, parameterPrefix);
            dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "CurrentLoginTime", DbType.DateTime, DateTime.Now);
            return db.ExecuteNonQuery(dbCommand) > 0;
        }
    }
}