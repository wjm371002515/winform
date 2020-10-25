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
	/// 对象号: 000006
	/// 系统参数表(Sysparameter)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2018-02-08 13:38:16.246
	/// </summary>
	public partial class Sysparameter : BaseDALSQLServer<SysparameterInfo>, ISysparameter
	{
		#region 对象实例及构造函数
		public static Sysparameter Instance
		{
			get
			{
				return new Sysparameter();
			}
		}

		public Sysparameter() : base(SQLServerPortal.gc._dicTablePre + "Sysparameter", "Id")
		{
			this.sortField = "Seq";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override SysparameterInfo DataReaderToEntity(IDataReader dataReader)
		{
			SysparameterInfo info = new SysparameterInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.SysId = reader.GetInt16("SysId"); 	 //系统参数Id
			info.Name = reader.GetString("Name"); 	 //名称
			info.SysValue = reader.GetString("SysValue"); 	 //系统键
			info.ControlType = reader.GetInt16("ControlType"); 	 //控件类型
			info.DicNo = reader.GetInt32("DicNo"); 	 //数据字典编号
			info.NumLen = reader.GetInt32("NumLen"); 	 //整形长度
			info.Remark = reader.GetString("Remark"); 	 //备注
			info.Seq = reader.GetString("Seq"); 	 //排序
			info.EditorId = reader.GetInt32("EditorId"); 	 //编辑人编号
			info.LastUpdateTime = reader.GetDateTime("LastUpdateTime"); 	 //最后更新时间
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(SysparameterInfo obj)
		{
			SysparameterInfo info = obj as SysparameterInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("SysId", info.SysId); 	 //系统参数Id
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("SysValue", info.SysValue); 	 //系统键
			hash.Add("ControlType", info.ControlType); 	 //控件类型
			hash.Add("DicNo", info.DicNo); 	 //数据字典编号
			hash.Add("NumLen", info.NumLen); 	 //整形长度
			hash.Add("Remark", info.Remark); 	 //备注
			hash.Add("Seq", info.Seq); 	 //排序
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
			dict.Add("SysId", "系统参数Id");
			dict.Add("Name", "名称");
			dict.Add("SysValue", "系统键");
			dict.Add("ControlType", "控件类型");
			dict.Add("DicNo", "数据字典编号");
			dict.Add("NumLen", "整形长度");
			dict.Add("Remark", "备注");
			dict.Add("Seq", "排序");
			dict.Add("EditorId", "编辑人编号");
			dict.Add("LastUpdateTime", "最后更新时间");
			#endregion
			return dict;
		}
		
		public List<SysparameterInfo> GetSysparameterBysysId(Int32 sysId)
        {
            string sql = string.Format("SELECT Id, SysId, Name, SysValue, ControlType, DicNo, Numlen, Remark, Seq, EditorId, LastUpdateTime FROM {0}Sysparameter WHERE sysId={1}", SQLServerPortal.gc._dicTablePre, sysId);
            return base.GetList(sql, null);
        }

        public int UpdateSysparameter(List<SysparameterInfo> info)
        {
            Int32 row_count = 0;
            Database db = CreateDatabase();

            foreach (var sysparameterInfo in info)
            {
                // 记录修改操作
                OperationLogOfUpdate(sysparameterInfo, sysparameterInfo.Id, null);//根据设置记录操作日志
                string sql = string.Format("UPDATE {0}Sysparameter set SysValue='{1}', EditorId='{2}', LastUpdateTime='{3}'  where Id={4} and SysId={5}", SQLServerPortal.gc._dicTablePre, sysparameterInfo.SysValue, sysparameterInfo.EditorId, sysparameterInfo.LastUpdateTime, sysparameterInfo.Id, sysparameterInfo.SysId);
                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                row_count += db.ExecuteNonQuery(dbCommand);
            }
            return row_count;
        }
	}
}