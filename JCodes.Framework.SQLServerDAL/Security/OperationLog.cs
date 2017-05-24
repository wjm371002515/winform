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

namespace JCodes.Framework.SQLServerDAL
{
    /// <summary>
    /// 用户关键操作记录
    /// </summary>
    public class OperationLog : BaseDALSQLServer<OperationLogInfo>, IOperationLog
	{
		#region 对象实例及构造函数

		public static OperationLog Instance
		{
			get
			{
				return new OperationLog();
			}
		}
        public OperationLog()
            : base(SQLServerPortal.gc._securityTablePre + "OperationLog", "ID")
        {
            this.SortField = "CreateTime";
            this.IsDescending = true;
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override OperationLogInfo DataReaderToEntity(IDataReader dataReader)
		{
			OperationLogInfo info = new OperationLogInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);

            info.ID = reader.GetString("ID");
			info.User_ID = reader.GetString("User_ID");
			info.LoginName = reader.GetString("LoginName");
			info.FullName = reader.GetString("FullName");
			info.Company_ID = reader.GetString("Company_ID");
			info.CompanyName = reader.GetString("CompanyName");
			info.TableName = reader.GetString("TableName");
			info.OperationType = reader.GetString("OperationType");
			info.Note = reader.GetString("Note");
            info.IPAddress = reader.GetString("IPAddress");
            info.MacAddress = reader.GetString("MacAddress");
			info.CreateTime = reader.GetDateTime("CreateTime");

			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(OperationLogInfo obj)
		{
		    OperationLogInfo info = obj as OperationLogInfo;
			Hashtable hash = new Hashtable();

            hash.Add("ID", info.ID);
			hash.Add("User_ID", info.User_ID);
 			hash.Add("LoginName", info.LoginName);
 			hash.Add("FullName", info.FullName);
 			hash.Add("Company_ID", info.Company_ID);
 			hash.Add("CompanyName", info.CompanyName);
 			hash.Add("TableName", info.TableName);
 			hash.Add("OperationType", info.OperationType);
 			hash.Add("Note", info.Note);
            hash.Add("IPAddress", info.IPAddress);
            hash.Add("MacAddress", info.MacAddress);
 			hash.Add("CreateTime", info.CreateTime);
 				
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
             dict.Add("User_ID", "登录用户ID");
             dict.Add("LoginName", "登录名");
             dict.Add("FullName", "真实名称");
             dict.Add("Company_ID", "所属公司ID");
             dict.Add("CompanyName", "所属公司名称");
             dict.Add("TableName", "操作表名称");
             dict.Add("OperationType", "操作类型");
             dict.Add("Note", "日志描述");
             dict.Add("IPAddress", "IP地址");
             dict.Add("MacAddress", "Mac地址");
             dict.Add("CreateTime", "创建时间");
             #endregion

            return dict;
        }

    }
}