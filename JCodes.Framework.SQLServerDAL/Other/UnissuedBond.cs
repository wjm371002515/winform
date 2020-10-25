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
	/// 对象号: 000805
	/// 已取得批文未发行债券项目(UnissuedBond)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2020-08-15 21:45:22.031
	/// </summary>
    public partial class UnissuedBond : BaseDALSQLServer<UnissuedBondInfo>, IUnissuedBond
	{
		#region 对象实例及构造函数
		public static UnissuedBond Instance
		{
			get
			{
				return new UnissuedBond();
			}
		}

        public UnissuedBond()
            : base(SQLServerPortal.gc._otherTablePre + "UnissuedBond", "Id")
		{
            this.sortField = "Id";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override UnissuedBondInfo DataReaderToEntity(IDataReader dataReader)
		{
			UnissuedBondInfo info = new UnissuedBondInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.ProjectName = reader.GetString("ProjectName"); 	 //报监管部门在审的项目名称
			info.RaisedAmount = reader.GetString("RaisedAmount"); 	 //募集资金预计额（亿）
			info.ProjectType = reader.GetString("ProjectType"); 	 //项目类型
			info.ProjectLeader = reader.GetString("ProjectLeader"); 	 //项目负责人
			info.Managers = reader.GetString("Managers"); 	 //承销商/管理人
			info.DocNum = reader.GetString("DocNum"); 	 //交易所确认文件文号
			info.ProjectStatusDetail = reader.GetString("ProjectStatusDetail"); 	 //项目状态跟踪
			info.DeptId = reader.GetInt32("DeptId"); 	 //部门Id
			info.DeptName = reader.GetString("DeptName"); 	 //部门名字
			info.ProjectProgress = reader.GetString("ProjectProgress"); 	 //项目进度
			info.ProjectStatus = reader.GetString("ProjectStatus"); 	 //最新项目状态
			info.DeclareTime = reader.GetString("DeclareTime"); 	 //申报时间
			info.From = reader.GetString("From"); 	 //信息来源
			info.Remark = reader.GetString("Remark"); 	 //备注
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(UnissuedBondInfo obj)
		{
			UnissuedBondInfo info = obj as UnissuedBondInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("ProjectName", info.ProjectName); 	 //报监管部门在审的项目名称
			hash.Add("RaisedAmount", info.RaisedAmount); 	 //募集资金预计额（亿）
			hash.Add("ProjectType", info.ProjectType); 	 //项目类型
			hash.Add("ProjectLeader", info.ProjectLeader); 	 //项目负责人
			hash.Add("Managers", info.Managers); 	 //承销商/管理人
			hash.Add("DocNum", info.DocNum); 	 //交易所确认文件文号
			hash.Add("ProjectStatusDetail", info.ProjectStatusDetail); 	 //项目状态跟踪
			hash.Add("DeptId", info.DeptId); 	 //部门Id
			hash.Add("DeptName", info.DeptName); 	 //部门名字
			hash.Add("ProjectProgress", info.ProjectProgress); 	 //项目进度
			hash.Add("ProjectStatus", info.ProjectStatus); 	 //最新项目状态
			hash.Add("DeclareTime", info.DeclareTime); 	 //申报时间
			hash.Add("From", info.From); 	 //信息来源
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
			dict.Add("ProjectName", "报监管部门在审的项目名称");
			dict.Add("RaisedAmount", "募集资金预计额（亿）");
			dict.Add("ProjectType", "项目类型");
			dict.Add("ProjectLeader", "项目负责人");
			dict.Add("Managers", "承销商/管理人");
			dict.Add("DocNum", "交易所确认文件文号");
			dict.Add("ProjectStatusDetail", "项目状态跟踪");
			dict.Add("DeptId", "部门Id");
			dict.Add("DeptName", "部门名字");
			dict.Add("ProjectProgress", "项目进度");
			dict.Add("ProjectStatus", "最新项目状态");
			dict.Add("DeclareTime", "申报时间");
			dict.Add("From", "信息来源");
			dict.Add("Remark", "备注");
			#endregion
			return dict;
		}
	}
}