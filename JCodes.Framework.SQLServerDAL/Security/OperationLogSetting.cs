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
	/// 对象号: 000207
	/// 操作日志数据配置表(OperationLogSetting)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2018-02-08 16:37:44.013
	/// </summary>
	public partial class OperationLogSetting : BaseDALSQLServer<OperationLogSettingInfo>, IOperationLogSetting
	{
		#region 对象实例及构造函数
		public static OperationLogSetting Instance
		{
			get
			{
				return new OperationLogSetting();
			}
		}

		public OperationLogSetting() : base(SQLServerPortal.gc._securityTablePre + "OperationLogSetting", "Id")
		{
			this.sortField = "Id";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override OperationLogSettingInfo DataReaderToEntity(IDataReader dataReader)
		{
			OperationLogSettingInfo info = new OperationLogSettingInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.IsForbid = reader.GetInt16("IsForbid"); 	 //是否禁用
			info.TableName = reader.GetString("TableName"); 	 //表名
			info.IsInsertLog = reader.GetInt16("IsInsertLog"); 	 //是否插入日志
			info.IsUpdateLog = reader.GetInt16("IsUpdateLog"); 	 //是否更新日志
			info.IsDeleteLog = reader.GetInt16("IsDeleteLog"); 	 //是否删除日志
			info.Remark = reader.GetString("Remark"); 	 //备注
			info.CreatorId = reader.GetInt32("CreatorId"); 	 //创建人编号
			info.CreatorTime = reader.GetDateTime("CreatorTime"); 	 //创建时间
			info.EditorId = reader.GetInt32("EditorId"); 	 //编辑人编号
			info.LastUpdateTime = reader.GetDateTime("LastUpdateTime"); 	 //最后更新时间
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(OperationLogSettingInfo obj)
		{
			OperationLogSettingInfo info = obj as OperationLogSettingInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("IsForbid", info.IsForbid); 	 //是否禁用
			hash.Add("TableName", info.TableName); 	 //表名
			hash.Add("IsInsertLog", info.IsInsertLog); 	 //是否插入日志
			hash.Add("IsUpdateLog", info.IsUpdateLog); 	 //是否更新日志
			hash.Add("IsDeleteLog", info.IsDeleteLog); 	 //是否删除日志
			hash.Add("Remark", info.Remark); 	 //备注
			hash.Add("CreatorId", info.CreatorId); 	 //创建人编号
			hash.Add("CreatorTime", info.CreatorTime); 	 //创建时间
			hash.Add("EditorId", info.EditorId); 	 //编辑人编号
			hash.Add("LastUpdateTime", info.LastUpdateTime); 	 //最后更新时间
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
			dict.Add("IsForbid", "是否禁用");
			dict.Add("TableName", "表名");
			dict.Add("IsInsertLog", "是否插入日志");
			dict.Add("IsUpdateLog", "是否更新日志");
			dict.Add("IsDeleteLog", "是否删除日志");
			dict.Add("Remark", "备注");
			dict.Add("CreatorId", "创建人编号");
			dict.Add("CreatorTime", "创建时间");
			dict.Add("EditorId", "编辑人编号");
			dict.Add("LastUpdateTime", "最后更新时间");
			#endregion
			return dict;
		}
	}
}