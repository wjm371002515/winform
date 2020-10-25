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
	/// 对象号: 000206
	/// 用户操作记录表(OperationLog)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2018-02-08 14:27:05.376
	/// </summary>
	public partial class OperationLog : BaseDALSQLServer<OperationLogInfo>, IOperationLog
	{
		#region 对象实例及构造函数
		public static OperationLog Instance
		{
			get
			{
				return new OperationLog();
			}
		}

		public OperationLog() : base(SQLServerPortal.gc._securityTablePre + "OperationLog", "Id")
		{
			this.sortField = "Id";
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
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.UserId = reader.GetInt32("UserId"); 	 //用户Id
			info.Name = reader.GetString("Name"); 	 //名称
			info.LoginName = reader.GetString("LoginName"); 	 //登录名
			info.CompanyId = reader.GetInt32("CompanyId"); 	 //公司Id
			info.CompanyName = reader.GetString("CompanyName"); 	 //公司名字
			info.TableName = reader.GetString("TableName"); 	 //表名
			info.OperationType = reader.GetInt16("OperationType"); 	 //操作类型
			info.Remark = reader.GetString("Remark"); 	 //备注
			info.IP = reader.GetString("IP"); 	 //IP地址
			info.Mac = reader.GetString("Mac"); 	 //Mac地址
			info.CreatorTime = reader.GetDateTime("CreatorTime"); 	 //创建时间
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(OperationLogInfo obj)
		{
			OperationLogInfo info = obj as OperationLogInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("UserId", info.UserId); 	 //用户Id
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("LoginName", info.LoginName); 	 //登录名
			hash.Add("CompanyId", info.CompanyId); 	 //公司Id
			hash.Add("CompanyName", info.CompanyName); 	 //公司名字
			hash.Add("TableName", info.TableName); 	 //表名
			hash.Add("OperationType", info.OperationType); 	 //操作类型
			hash.Add("Remark", info.Remark); 	 //备注
			hash.Add("IP", info.IP); 	 //IP地址
			hash.Add("Mac", info.Mac); 	 //Mac地址
			hash.Add("CreatorTime", info.CreatorTime); 	 //创建时间
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
			dict.Add("UserId", "用户Id");
			dict.Add("Name", "名称");
			dict.Add("LoginName", "登录名");
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