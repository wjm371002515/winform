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
    /// 用户关键操作记录
    /// </summary>
    public class OperationLog : BaseDALOracle<OperationLogInfo>, IOperationLog
	{
		#region 对象实例及构造函数

		public static OperationLog Instance
		{
			get
			{
				return new OperationLog();
			}
		}
		public OperationLog() : base(OraclePortal.gc._securityTablePre+"OperationLog","ID")
        {
            this.SeqName = "";//由于字符型组件，不需要序列
            this.SortField = "CreatorTime";
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
            info.Id = reader.GetInt32("ID");
            info.UserId = reader.GetInt32("User_ID");
            info.LoginName = reader.GetString("LoginName");
            info.FullName = reader.GetString("FullName");
            info.CompanyId = reader.GetInt32("CompanyId");
            info.CompanyName = reader.GetString("CompanyName");
            info.TableName = reader.GetString("TableName");
            info.OperationType = reader.GetString("OperationType");
            info.Remark = reader.GetString("Remark");
            info.IP = reader.GetString("IP");
            info.Mac = reader.GetString("Mac");
            info.CreatorTime = reader.GetDateTime("CreatorTime");     

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
            hash.Add("Id", info.Id);
            hash.Add("UserId", info.UserId);
            hash.Add("LoginName", info.LoginName);
            hash.Add("FullName", info.FullName);
            hash.Add("CompanyId", info.CompanyId);
            hash.Add("CompanyName", info.CompanyName);
            hash.Add("TableName", info.TableName);
            hash.Add("OperationType", info.OperationType);
            hash.Add("Remark", info.Remark);
            hash.Add("IP", info.IP);
            hash.Add("Mac", info.Mac);
            hash.Add("CreatorTime", info.CreatorTime);
 				
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
            dict.Add("Id", "编号");
            dict.Add("UserId", "用户Id");
            dict.Add("LoginName", "登录名");
            dict.Add("FullName", "真实名");
            dict.Add("CompanyId", "公司Id");
            dict.Add("CompanyName", "公司名字");
            dict.Add("TableName", "表名");
            dict.Add("OperationType", "操作类型");
            dict.Add("Remark", "备注");
            dict.Add("IP", "IP地址");
            dict.Add("Mac", "Mac地址");
            dict.Add("CreatorTime", "创建时间");
            #endregion

            return dict;
        }

    }
}