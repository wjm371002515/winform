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
using JCodes.Framework.Common.Format;

namespace JCodes.Framework.SQLiteDAL
{
    /// <summary>
    /// 系统用户信息
    /// </summary>
    public class User : BaseDALSQLite<UserInfo>, IUser
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
            : base(SQLitePortal.gc._securityTablePre+"User", "Id")
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
        protected override UserInfo DataReaderToEntity(IDataReader dataReader)
        {
            UserInfo info = new UserInfo();
            SmartDataReader reader = new SmartDataReader(dataReader);

            info.Id = reader.GetInt32("Id");
            info.UserCode = reader.GetString("UserCode");
            info.Name = reader.GetString("Name");
            info.FullName = reader.GetString("FullName");
            info.Password = reader.GetString("Password");
            info.MobilePhone = reader.GetString("MobilePhone");
            info.Email = reader.GetString("Email");
            info.Pid = reader.GetInt32("Pid");
            info.LoginName = reader.GetString("LoginName");
            info.IsExpire = reader.GetInt32("IsExpire");
            info.Remark = reader.GetString("Remark");
            info.IdCard = reader.GetString("IdCard");
            info.OfficePhone = reader.GetString("OfficePhone");
            info.HomePhone = reader.GetString("HomePhone");
            info.Address = reader.GetString("Address");
            info.WorkAddress = reader.GetString("WorkAddress");
            info.Gender = reader.GetInt32("Gender");
            info.Birthday = reader.GetDateTime("Birthday");
            info.QQ = reader.GetInt32("QQ");
            info.Signature = reader.GetString("Signature");
            info.AuditStatus = reader.GetInt32("AuditStatus");
            info.Portrait = reader.GetBytes("Portrait");
            info.DeptId = reader.GetInt32("DeptId");
            info.CompanyId = reader.GetInt32("CompanyId");
            info.Seq = reader.GetString("Seq");
            info.CreatorId = reader.GetInt32("CreatorId");
            info.CreateTime = reader.GetDateTime("CreateTime");
            info.EditorId = reader.GetInt32("EditorId");
            info.EditTime = reader.GetDateTime("EditTime");
            info.IsDelete = reader.GetInt32("IsDelete");
            info.Question1 = reader.GetString("Question1");
            info.Question2 = reader.GetString("Question2");
            info.Question3 = reader.GetString("Question3");
            info.Answer1 = reader.GetString("Answer1");
            info.Answer2 = reader.GetString("Answer2");
            info.Answer3 = reader.GetString("Answer3");
            info.LastLoginIp = reader.GetString("LastLoginIp");
            info.LastLoginMac = reader.GetString("LastLoginMac");
            info.LastLoginTime = reader.GetDateTime("LastLoginTime");
            info.LastChangePwdTime = reader.GetDateTime("LastChangePwdTime");

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

            hash.Add("UserCode", info.UserCode);
            hash.Add("Name", info.Name);
            hash.Add("FullName", info.FullName);
            hash.Add("Password", info.Password);
            hash.Add("MobilePhone", info.MobilePhone);
            hash.Add("Email", info.Email);
            hash.Add("LoginName", info.LoginName);
            hash.Add("Pid", info.Pid);
            hash.Add("Remark", info.Remark);
            hash.Add("IdCard", info.IdCard);
            hash.Add("OfficePhone", info.OfficePhone);
            hash.Add("HomePhone", info.HomePhone);
            hash.Add("Address", info.Address);
            hash.Add("WorkAddress", info.WorkAddress);
            hash.Add("Gender", info.Gender);
            hash.Add("Birthday", info.Birthday);
            hash.Add("QQ", info.QQ);
            hash.Add("Signature", info.Signature);
            hash.Add("AuditStatus", info.AuditStatus);
            hash.Add("Portrait", info.Portrait);
            hash.Add("DeptId", info.DeptId);
            hash.Add("CompanyId", info.CompanyId);
            hash.Add("Seq", info.Seq);
            hash.Add("CreatorId", info.CreatorId);
            hash.Add("CreateTime", info.CreateTime);
            hash.Add("EditorId", info.EditorId);
            hash.Add("EditTime", info.EditTime);
            hash.Add("IsDelete", info.IsDelete);
            hash.Add("Question1", info.Question1);
            hash.Add("Question2", info.Question2);
            hash.Add("Question3", info.Question3);
            hash.Add("Answer1", info.Answer1);
            hash.Add("Answer2", info.Answer2);
            hash.Add("Answer3", info.Answer3);
            hash.Add("LastLoginIp", info.LastLoginIp);
            hash.Add("LastLoginMac", info.LastLoginMac);
            hash.Add("LastLoginTime", info.LastLoginTime);
            hash.Add("LastChangePwdTime", info.LastChangePwdTime);

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
            dict.Add("UserCode", "用户编码");
            dict.Add("Name", "名称");
            dict.Add("FullName", "真实名");
            dict.Add("Password", "密码");
            dict.Add("Email", "Email邮箱");
            dict.Add("LoginName", "登录名");
            dict.Add("Remark", "备注");
            dict.Add("IdCard", "身份证");
            dict.Add("OfficePhone", "办公电话");
            dict.Add("HomePhone", "家庭电话");
            dict.Add("MobilePhone", "移动电话");
            dict.Add("Address", "住址");
            dict.Add("WorkAddress", "工作地址");
            dict.Add("Gender", "性别");
            dict.Add("Birthday", "出生日期");
            dict.Add("Qq", "QQ号码");
            dict.Add("Signature", "个性签名");
            dict.Add("AuditStatus", "审核状态");
            dict.Add("Portrait", "个人图片");
            dict.Add("DeptName", "默认部门名称");
            dict.Add("CompanyID", "所属公司ID");
            dict.Add("Seq", "排序");
            dict.Add("CreatorId", "创建人ID");
            dict.Add("CreateTime", "创建时间");
            dict.Add("EditorId", "编辑人ID");
            dict.Add("EditTime", "编辑时间");
            dict.Add("IsDelete", "是否已删除");
            dict.Add("Question1", "问题1");
            dict.Add("Question2", "问题2");
            dict.Add("Question3", "问题3");
            dict.Add("Answer1", "回答1");
            dict.Add("Answer2", "回答2");
            dict.Add("Answer3", "回答3");
            dict.Add("LastLoginIp", "最后登录IP");
            dict.Add("LastLoginMac", "最后登录Mac");
            dict.Add("LastLoginTime", "最后登录日期");
            dict.Add("LastChangePwdTime", "最后修改密码时间");
            #endregion

            return dict;
        }

        /// <summary>
        /// 重写删除操作，删除关联的信息
        /// </summary>
        /// <param name="key">ID值</param>
        /// <returns></returns>
        public override bool DeleteByUser(object key, Int32 userId, DbTransaction trans = null)
        {
            UserInfo info = this.FindByID(key, trans);
            if (info != null)
            {
                string sql = string.Format("UPDATE {2} SET PID={0} Where PID={1}", info.Pid, key, tableName);
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
                    info.Id = dr.GetInt32("Id");
                    info.Name = dr.GetString("Name");
                    info.Password = dr.GetString("Password");
                    info.FullName = dr.GetString("FullName");
                    info.UserCode = dr.GetString("UserCode");
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
            string sql = string.Format("Select ID,Name,Password,FullName,UserCode,MobilePhone,Email From {0} Where IsDelete = 0 ", tableName);
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
            string sql = string.Format(@"Select ID,Name,Password,FullName,UserCode,MobilePhone,Email From {1} 
            Inner Join {0}OU_User ON {1}.ID=User_ID Where IsDelete = 0 AND {0}OU_User.OU_ID = {2}", SQLitePortal.gc._securityTablePre, tableName, ouID);
            return FillSimpleUsers(sql);
        }

        public List<SimpleUserInfo> GetSimpleUsersByRole(int roleID)
        {
            string sql = string.Format(@"Select ID,Name,Password,FullName,UserCode,MobilePhone,Email From {1} 
            INNER JOIN {0}User_Role ON {1}.ID=User_ID Where IsDelete = 0 AND {0}User_Role.Role_ID ={2} ", SQLitePortal.gc._securityTablePre, tableName, roleID);

            return this.FillSimpleUsers(sql);
        }

        public List<UserInfo> GetUsersByOU(int ouID)
        {
            string sql = string.Format(@"SELECT * FROM {1} INNER JOIN {0}OU_User On {1}.ID={0}OU_User.User_ID 
            WHERE IsDelete = 0 AND {0}OU_User.OU_ID ={2} ", SQLitePortal.gc._securityTablePre, tableName, ouID);
            return GetList(sql, null);
        }

        public List<UserInfo> GetUsersByRole(int roleID)
        {
            string sql = string.Format(@"SELECT * FROM {1} INNER JOIN {0}User_Role On {1}.ID={0}User_Role.User_ID 
            WHERE IsDelete = 0 AND {0}User_Role.Role_ID = {2}", SQLitePortal.gc._securityTablePre, tableName, roleID);
            return this.GetList(sql, null);
        }

        /// <summary>
        /// 根据用户ID获取用户全名称
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public string GetFullNameByID(int userID)
        {
            string sql = string.Format("Select FullName from {0} Where ID={1}", tableName, userID);
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
            string fieldName = GetFieldNameByImageType(imagetype);

            string sql = string.Format("Select {0} from {1} where Id = {2} ", fieldName, tableName, userId);
            Database db = CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);

            byte[] imageBytes = null;
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    imageBytes = (reader.IsDBNull(reader.GetOrdinal(fieldName))) ? null : (byte[])reader[0];
                }
            }

            return imageBytes;
        }

        /// <summary>
        /// 根据图片枚举类型获取对应的字段名称
        /// </summary>
        /// <param name="imageType">图片枚举类型</param>
        /// <returns></returns>
        private string GetFieldNameByImageType(UserImageType imageType)
        {
            string fieldName = "Portrait";
            switch (imageType)
            {
                case UserImageType.个人肖像:
                    fieldName = "Portrait";
                    break;
                case UserImageType.身份证照片1:
                    fieldName = "IDPhoto1";
                    break;
                case UserImageType.身份证照片2:
                    fieldName = "IDPhoto2";
                    break;
                case UserImageType.名片1:
                    fieldName = "BusinessCard1";
                    break;
                case UserImageType.名片2:
                    fieldName = "BusinessCard2";
                    break;
            }
            return fieldName;
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
            string fieldName = GetFieldNameByImageType(imagetype);

            string sql = string.Format("update {0} set {1}=@image where Id = {2} ", tableName, fieldName, userId);
            Database db = CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "image", DbType.Binary, imageBytes);
            return db.ExecuteNonQuery(dbCommand) > 0;
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

            sql = string.Format("Update {0} Set CurrentLoginIP='{1}',CurrentMacAddress='{2}', CurrentLoginTime=@CurrentLoginTime Where ID = {3}", tableName, ip, macAddr, id);
            dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "CurrentLoginTime", DbType.DateTime, DateTimeHelper.GetServerDateTime2());
            return db.ExecuteNonQuery(dbCommand) > 0;
        }
    }
}