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
	/// TODO
	/// </summary>
	public partial class DongnanhaiVotes : BaseDALSQLServer<DongnanhaiVotesInfo>, IDongnanhaiVotes
	{
		#region 对象实例及构造函数
		public static DongnanhaiVotes Instance
		{
			get
			{
				return new DongnanhaiVotes();
			}
		}

		public DongnanhaiVotes() : base(SQLServerPortal.gc._dongnanhaiTablePre + "DongnanhaiVotes", "")
		{
			this.sortField = "";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override DongnanhaiVotesInfo DataReaderToEntity(IDataReader dataReader)
		{
			DongnanhaiVotesInfo info = new DongnanhaiVotesInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.TestName1 = reader.GetInt32("TestName1"); 	 //测试字段
			info.Name = reader.GetString("Name"); 	 //名称
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(DongnanhaiVotesInfo obj)
		{
			DongnanhaiVotesInfo info = obj as DongnanhaiVotesInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("TestName1", info.TestName1); 	 //测试字段
			hash.Add("Name", info.Name); 	 //名称
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
			dict.Add("TestName1", "测试字段");
			dict.Add("Name", "名称");
			#endregion
			return dict;
		}
		
		public List<DongnanhaiVotesInfo> GetVotesBylouzhuang(string louzhuang) {
            string sql = string.Format("select * from {0}dongnanhaiVotes where zhuang='{1}' order by ceng,util,fanghao", SQLServerPortal.gc._dongnanhaiTablePre, louzhuang);
            return base.GetList(sql, null);
        }

        public Int32 MaxCengHuShu(string louzhuang) {
            string sql = string.Format("select max(t.row_count) as max_hushu from (SELECT count(1) as row_count FROM `{0}dongnanhaiVotes` WHERE zhuang='{1}' group by ceng) t", SQLServerPortal.gc._dongnanhaiTablePre, louzhuang);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);
            return base.GetExecuteScalarValue(db, command);
        }

        public void UpdateFlag(string fanghao, string util, string zhuang, string yuan, Int32 flag) {
            string sql = string.Format("set autocommit=0;Update {0}dongnanhaiVotes set flag='{1}' where fanghao='{2}' and util='{3}' and zhuang='{4}' and yuan='{5}'", SQLServerPortal.gc._dongnanhaiTablePre, flag, fanghao, util, zhuang, yuan);
            base.SqlExecute(sql);
        }

        /// <summary>
        /// 根据条件过滤
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Int32 GetXianchangAndPhoneShu(string condition) {
            /*string sql = string.Format("select max(t.row_count) as max_hushu from (SELECT count(1) as row_count FROM `{0}dongnanhaiVotes` WHERE zhuang='{1}' group by ceng) t", SQLServerPortal.gc._dongnanhaiTablePre, louzhuang);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);*/
            return 0;
        }
	}
}