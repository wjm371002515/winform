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
	/// 对象号: 000213
	/// 角色数据权限表(RoleData)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2019-09-25 09:55:51.352
	/// </summary>
	public partial class RoleData : BaseDALSQLServer<RoleDataInfo>, IRoleData
	{
		#region 对象实例及构造函数
		public static RoleData Instance
		{
			get
			{
				return new RoleData();
			}
		}

		public RoleData() : base(SQLServerPortal.gc._securityTablePre + "RoleData", "Id")
		{
			this.sortField = "Id";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override RoleDataInfo DataReaderToEntity(IDataReader dataReader)
		{
			RoleDataInfo info = new RoleDataInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.RoleId = reader.GetInt16("RoleId"); 	 //角色Id
			info.CompanyLst = reader.GetString("CompanyLst"); 	 //公司列表
			info.DeptLst = reader.GetString("DeptLst"); 	 //部门列表
			info.Remark = reader.GetString("Remark"); 	 //备注
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(RoleDataInfo obj)
		{
			RoleDataInfo info = obj as RoleDataInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("RoleId", info.RoleId); 	 //角色Id
			hash.Add("CompanyLst", info.CompanyLst); 	 //公司列表
			hash.Add("DeptLst", info.DeptLst); 	 //部门列表
			hash.Add("Remark", info.Remark); 	 //备注
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
			dict.Add("RoleId", "角色Id");
			dict.Add("CompanyLst", "公司列表");
			dict.Add("DeptLst", "部门列表");
			dict.Add("Remark", "备注");
			#endregion
			return dict;
		}
	}
}