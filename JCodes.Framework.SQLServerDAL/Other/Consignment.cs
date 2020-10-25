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
	/// 对象号: 801
	/// 代销信息数据(Consignment)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2019-11-23 10:01:05.015
	/// </summary>
	public partial class Consignment : BaseDALSQLServer<ConsignmentInfo>, IConsignment
	{
		#region 对象实例及构造函数
		public static Consignment Instance
		{
			get
			{
				return new Consignment();
			}
		}

		public Consignment() : base(SQLServerPortal.gc._otherTablePre + "Consignment", "Id")
		{
			this.sortField = "Id";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override ConsignmentInfo DataReaderToEntity(IDataReader dataReader)
		{
			ConsignmentInfo info = new ConsignmentInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.StrValue = reader.GetString("StrValue"); 	 //值
			info.SysValue = reader.GetString("SysValue"); 	 //系统键
			info.Name = reader.GetString("Name"); 	 //名称
			info.EnableStatus = reader.GetInt16("EnableStatus"); 	 //启用状态
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(ConsignmentInfo obj)
		{
			ConsignmentInfo info = obj as ConsignmentInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("StrValue", info.StrValue); 	 //值
			hash.Add("SysValue", info.SysValue); 	 //系统键
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("EnableStatus", info.EnableStatus); 	 //启用状态
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
			dict.Add("StrValue", "值");
			dict.Add("SysValue", "系统键");
			dict.Add("Name", "名称");
			dict.Add("EnableStatus", "启用状态");
			#endregion
			return dict;
		}

        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="consignmentInfo"></param>
        /// <returns></returns>
        public Int32 UpdateConsignmentById(ConsignmentInfo consignmentInfo) {
            Database db = CreateDatabase();

            // 记录修改操作 
            OperationLogOfUpdate(consignmentInfo, consignmentInfo.Id, null);//根据设置记录操作日志
            // StrValue, Name, EnableStatus, SysValue
            string sql = string.Format("UPDATE {0}Consignment set StrValue='{1}', Name='{2}', EnableStatus='{3}',SysValue='{4}' WHERE Id={5}", SQLServerPortal.gc._otherTablePre, consignmentInfo.StrValue, consignmentInfo.Name, consignmentInfo.EnableStatus, consignmentInfo.SysValue, consignmentInfo.Id);
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(dbCommand);
        }

        public Int32 InsertConsignment(ConsignmentInfo consignmentInfo)
        {
            Database db = CreateDatabase();

            // 记录修改操作 
            OperationLogOfUpdate(consignmentInfo, consignmentInfo.Id, null);//根据设置记录操作日志
            // StrValue, Name, EnableStatus, SysValue
            string sql = string.Format("INSERT INTO {0}Consignment(Id, StrValue, Name, EnableStatus, SysValue) VALUES({1}, '{2}', '{3}', {4}, '{5}')", SQLServerPortal.gc._otherTablePre, consignmentInfo.Id, consignmentInfo.StrValue, consignmentInfo.Name, consignmentInfo.EnableStatus, consignmentInfo.SysValue);
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(dbCommand);
        }

        public Int32 GetMaxId()
        {
            
            //string sql = string.Format("select ISNULL(MAX(ID),0) as MAX_ID from {0}Consignment", SQLServerPortal.gc._otherTablePre);
            return base.GetMaxId();
        }
	}
}