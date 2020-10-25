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
using JCodes.Framework.Common;
using JCodes.Framework.Common.Format;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.SQLServerDAL
{
	/// <summary>
	/// 对象号: 000215
	/// 用户信息(User)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2019-09-25 09:57:17.705
	/// </summary>
	public partial class User : BaseDALSQLServer<UserInfo>, IUser
	{
		#region 对象实例及构造函数
		public static User Instance
		{
			get
			{
				return new User();
			}
		}

		public User() : base(SQLServerPortal.gc._securityTablePre + "User", "Id")
		{
			this.sortField = "Seq";
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
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.UserCode = reader.GetString("UserCode"); 	 //用户编码
			info.Name = reader.GetString("Name"); 	 //名称
			info.Password = reader.GetString("Password"); 	 //密码
			info.LoginName = reader.GetString("LoginName"); 	 //登录名
			info.IsExpire = reader.GetInt16("IsExpire"); 	 //是否过期
			info.IdCard = reader.GetString("IdCard"); 	 //身份证
			info.MobilePhone = reader.GetString("MobilePhone"); 	 //手机
			info.OfficePhone = reader.GetString("OfficePhone"); 	 //办公电话
			info.HomePhone = reader.GetString("HomePhone"); 	 //家庭电话
			info.Email = reader.GetString("Email"); 	 //Email邮箱
			info.Address = reader.GetString("Address"); 	 //地址
			info.WorkAddress = reader.GetString("WorkAddress"); 	 //工作地址
			info.Gender = reader.GetInt16("Gender"); 	 //性别
			info.Birthday = reader.GetDateTime("Birthday"); 	 //生日
			info.QQ = reader.GetInt32("QQ"); 	 //QQ号
			info.Signature = reader.GetString("Signature"); 	 //个性签名
			info.AuditStatus = reader.GetInt16("AuditStatus"); 	 //审核状态
			info.Portrait = reader.GetString("Portrait"); 	 //个人图片
			info.Remark = reader.GetString("Remark"); 	 //备注
			info.DeptId = reader.GetInt32("DeptId"); 	 //部门Id
			info.CompanyId = reader.GetInt32("CompanyId"); 	 //公司Id
			info.CreatorId = reader.GetInt32("CreatorId"); 	 //创建人编号
			info.CreatorTime = reader.GetDateTime("CreatorTime"); 	 //创建时间
			info.EditorId = reader.GetInt32("EditorId"); 	 //编辑人编号
			info.LastUpdateTime = reader.GetDateTime("LastUpdateTime"); 	 //最后更新时间
			info.IsDelete = reader.GetInt16("IsDelete"); 	 //是否删除
			info.Question1 = reader.GetString("Question1"); 	 //问题1
			info.Question2 = reader.GetString("Question2"); 	 //问题2
			info.Question3 = reader.GetString("Question3"); 	 //问题3
			info.Answer1 = reader.GetString("Answer1"); 	 //回答1
			info.Answer2 = reader.GetString("Answer2"); 	 //回答2
			info.Answer3 = reader.GetString("Answer3"); 	 //回答3
			info.LastLoginIp = reader.GetString("LastLoginIp"); 	 //最后登录IP
			info.LastLoginTime = reader.GetDateTime("LastLoginTime"); 	 //最后登录日期
			info.LastLoginMac = reader.GetString("LastLoginMac"); 	 //最后登录Mac
			info.CurLoginIp = reader.GetString("CurLoginIp"); 	 //当前登录IP
			info.CurLoginTime = reader.GetDateTime("CurLoginTime"); 	 //当前登录日期
			info.CurLoginMac = reader.GetString("CurLoginMac"); 	 //当前登录Mac
			info.LastChangePwdTime = reader.GetDateTime("LastChangePwdTime"); 	 //最后修改密码时间
			info.Seq = reader.GetString("Seq"); 	 //排序
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(UserInfo obj)
		{
			UserInfo info = obj as UserInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("UserCode", info.UserCode); 	 //用户编码
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("Password", info.Password); 	 //密码
			hash.Add("LoginName", info.LoginName); 	 //登录名
			hash.Add("IsExpire", info.IsExpire); 	 //是否过期
			hash.Add("IdCard", info.IdCard); 	 //身份证
			hash.Add("MobilePhone", info.MobilePhone); 	 //手机
			hash.Add("OfficePhone", info.OfficePhone); 	 //办公电话
			hash.Add("HomePhone", info.HomePhone); 	 //家庭电话
			hash.Add("Email", info.Email); 	 //Email邮箱
			hash.Add("Address", info.Address); 	 //地址
			hash.Add("WorkAddress", info.WorkAddress); 	 //工作地址
			hash.Add("Gender", info.Gender); 	 //性别
			hash.Add("Birthday", info.Birthday); 	 //生日
			hash.Add("QQ", info.QQ); 	 //QQ号
			hash.Add("Signature", info.Signature); 	 //个性签名
			hash.Add("AuditStatus", info.AuditStatus); 	 //审核状态
			hash.Add("Portrait", info.Portrait); 	 //个人图片
			hash.Add("Remark", info.Remark); 	 //备注
			hash.Add("DeptId", info.DeptId); 	 //部门Id
			hash.Add("CompanyId", info.CompanyId); 	 //公司Id
			hash.Add("CreatorId", info.CreatorId); 	 //创建人编号
			hash.Add("CreatorTime", info.CreatorTime); 	 //创建时间
			hash.Add("EditorId", info.EditorId); 	 //编辑人编号
			hash.Add("LastUpdateTime", info.LastUpdateTime); 	 //最后更新时间
			hash.Add("IsDelete", info.IsDelete); 	 //是否删除
			hash.Add("Question1", info.Question1); 	 //问题1
			hash.Add("Question2", info.Question2); 	 //问题2
			hash.Add("Question3", info.Question3); 	 //问题3
			hash.Add("Answer1", info.Answer1); 	 //回答1
			hash.Add("Answer2", info.Answer2); 	 //回答2
			hash.Add("Answer3", info.Answer3); 	 //回答3
			hash.Add("LastLoginIp", info.LastLoginIp); 	 //最后登录IP
			hash.Add("LastLoginTime", info.LastLoginTime); 	 //最后登录日期
			hash.Add("LastLoginMac", info.LastLoginMac); 	 //最后登录Mac
			hash.Add("CurLoginIp", info.CurLoginIp); 	 //当前登录IP
			hash.Add("CurLoginTime", info.CurLoginTime); 	 //当前登录日期
			hash.Add("CurLoginMac", info.CurLoginMac); 	 //当前登录Mac
			hash.Add("LastChangePwdTime", info.LastChangePwdTime); 	 //最后修改密码时间
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
			dict.Add("Id", "ID序号");
			dict.Add("UserCode", "用户编码");
			dict.Add("Name", "名称");
			dict.Add("Password", "密码");
			dict.Add("LoginName", "登录名");
			dict.Add("IsExpire", "是否过期");
			dict.Add("IdCard", "身份证");
			dict.Add("MobilePhone", "手机");
			dict.Add("OfficePhone", "办公电话");
			dict.Add("HomePhone", "家庭电话");
			dict.Add("Email", "Email邮箱");
			dict.Add("Address", "地址");
			dict.Add("WorkAddress", "工作地址");
			dict.Add("Gender", "性别");
			dict.Add("Birthday", "生日");
			dict.Add("QQ", "QQ号");
			dict.Add("Signature", "个性签名");
			dict.Add("AuditStatus", "审核状态");
			dict.Add("Portrait", "个人图片");
			dict.Add("Remark", "备注");
			dict.Add("DeptId", "部门Id");
			dict.Add("CompanyId", "公司Id");
			dict.Add("CreatorId", "创建人编号");
			dict.Add("CreatorTime", "创建时间");
			dict.Add("EditorId", "编辑人编号");
			dict.Add("LastUpdateTime", "最后更新时间");
			dict.Add("IsDelete", "是否删除");
			dict.Add("Question1", "问题1");
			dict.Add("Question2", "问题2");
			dict.Add("Question3", "问题3");
			dict.Add("Answer1", "回答1");
			dict.Add("Answer2", "回答2");
			dict.Add("Answer3", "回答3");
			dict.Add("LastLoginIp", "最后登录IP");
			dict.Add("LastLoginTime", "最后登录日期");
			dict.Add("LastLoginMac", "最后登录Mac");
			dict.Add("CurLoginIp", "当前登录IP");
			dict.Add("CurLoginTime", "当前登录日期");
			dict.Add("CurLoginMac", "当前登录Mac");
			dict.Add("LastChangePwdTime", "最后修改密码时间");
			dict.Add("Seq", "排序");
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
            UserInfo info = this.FindById(key, trans);
            if (info != null)
            {
                string sql = string.Format("UPDATE {0} set IsDelete={1} Where Id ={0} ", tableName, (short)IsDelete.是, key);
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
                    info.UserCode = dr.GetString("UserCode");
                    info.Name = dr.GetString("Name");
                    info.LoginName = dr.GetString("LoginName");
                    //info.Password = dr.GetString("Password");
                    info.Password = "******";   // 特殊处理 对于密码不显示内容用*代替
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
        public List<SimpleUserInfo> FindSimpleUsers(string condition, IsDelete isDelete = IsDelete.否)
        {
            if (HasInjectionData(condition))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("检测出SQL注入的恶意数据, {0}", condition), typeof(User));
                throw new Exception("检测出SQL注入的恶意数据");
            }

            //串连条件语句为一个完整的Sql语句
            string sql = string.Format("Select Id,UserCode,Name,LoginName,Password,MobilePhone,Email From {0} Where (IsDelete = {1} or 0 = {1}) ", tableName, (short)isDelete);
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
        public List<SimpleUserInfo> GetSimpleUsers(IsDelete isDelete = IsDelete.否)
        {
            return this.FindSimpleUsers(null, isDelete);
        }

        /// <summary>
        /// 获取指定用户Id字符串的简单用户对象列表
        /// </summary>
        /// <param name="userIDs"></param>
        /// <returns></returns>
        public List<SimpleUserInfo> GetSimpleUsers(string userIds, IsDelete isDelete = IsDelete.否)
        {
            string condition = string.Format(" Id In ({0})", userIds);
            return this.FindSimpleUsers(condition, isDelete);
        }

        /// <summary>
        /// 根据机构ID获取对应关系的用户简单对象列表
        /// </summary>
        /// <param name="ouID">机构ID</param>
        /// <returns></returns>
        public List<SimpleUserInfo> GetSimpleUsersByOUId(Int32 ouId, IsDelete isDelete = IsDelete.否)
        {
            string sql = string.Format(@"Select Id,Name,Password,LoginName,UserCode,MobilePhone,Email From {1} 
            Inner Join {0}OU_User ON {1}.Id=UserId Where (IsDelete = {3} or 0 = {3}) AND {0}OU_User.OuId = {2}", SQLServerPortal.gc._securityTablePre, tableName, ouId, (short)isDelete);
            return FillSimpleUsers(sql);
        }

        public List<SimpleUserInfo> GetSimpleUsersByRoleId(Int32 roleId, IsDelete isDelete = IsDelete.否)
        {
            string sql = string.Format(@"Select Id,Name,Password,LoginName,UserCode,MobilePhone,Email From {1} 
            INNER JOIN {0}User_Role ON {1}.Id=UserId Where (IsDelete = {3} or 0 = {3}) AND {0}User_Role.RoleId ={2} ", SQLServerPortal.gc._securityTablePre, tableName, roleId, (short)isDelete);
            return this.FillSimpleUsers(sql);
        }

        public List<UserInfo> GetUsersByOUId(Int32 ouId, IsDelete isDelete = IsDelete.否)
        {
            string sql = string.Format(@"SELECT * FROM {1} INNER JOIN {0}OU_User On {1}.Id={0}OU_User.UserId 
            WHERE (IsDelete = {3} or 0 = {3}) AND {0}OU_User.OuId ={2} ", SQLServerPortal.gc._securityTablePre, tableName, ouId, (short)isDelete);
            return GetList(sql, null);
        }

        public List<UserInfo> GetUsersByRoleId(Int32 roleId, IsDelete isDelete = IsDelete.否)
        {
            string sql = string.Format(@"SELECT * FROM {1} INNER JOIN {0}User_Role On {1}.Id={0}User_Role.UserId 
            WHERE (IsDelete = {3} or 0 = {3}) AND {0}User_Role.RoleId = {2}", SQLServerPortal.gc._securityTablePre, tableName, roleId, (short)isDelete);
            return this.GetList(sql, null);
        }

        /// <summary>
        /// 根据用户ID获取用户全名称
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public string GetNameById(Int32 userId)
        {
            string sql = string.Format("Select Name from {0} Where Id={1}", tableName, userId);
            return SqlValueList(sql);
        }

        /// <summary>
        /// 根据用户登陆名称，获取用户全名
        /// </summary>
        /// <param name="userName">用户登陆名称</param>
        /// <returns></returns>
        public string GetLoginNameByName(string userName)
        {
            string sql = string.Format("Select LoginName from {0} Where Name='{1}' ", tableName, userName);
            return SqlValueList(sql);
        }

        /// <summary>
        /// 根据个人图片枚举类型获取图片数据
        /// </summary>
        /// <param name="imagetype">图片枚举类型</param>
        /// <returns></returns>
        public byte[] GetPersonImageBytes(UserImageType imagetype, Int32 userId)
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
        public bool UpdatePersonImageBytes(UserImageType imagetype, Int32 userId, byte[] imageBytes)
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
        public bool SetDeletedFlag(Int32 Id,  DbTransaction trans = null)
        {
            string sql = string.Format("Update {0} Set IsDelete={1} Where Id = {2} ", tableName, (short)IsDelete.是, Id);
            return SqlExecute(sql, trans) > 0;
        }

        /// <summary>
        /// 更新用户登录的时间和IP地址
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="ip">IP地址</param>
        /// <param name="macAddr">MAC地址</param>
        /// <returns></returns>
        public bool UpdateUserLoginData(Int32 Id, string ip, string mac)
        {
            //先复制最后的登录时间和IP地址
            string sql = string.Format("Update {0} set LastLoginIp=CurLoginIp, LastLoginTime=CurLoginTime, LastLoginMac=CurLoginMac Where Id={1}", tableName, Id);
            Database db = CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.ExecuteNonQuery(dbCommand);

            sql = string.Format("Update {0} Set CurLoginIp='{1}',LastLoginMac='{2}', LastLoginTime=@CurLoginTime Where Id = {3}", tableName, ip, mac, Id);
            dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "CurLoginTime", DbType.DateTime, DateTimeHelper.GetServerDateTime2());
            return db.ExecuteNonQuery(dbCommand) > 0;
        }
    }
}