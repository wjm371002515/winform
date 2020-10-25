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
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.SQLServerDAL
{
	/// <summary>
	/// 对象号: 000001
	/// 数据字典类型(DictType)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2018-02-08 13:06:37.334
	/// </summary>
	public partial class DictType : BaseDALSQLServer<DictTypeInfo>, IDictType
	{
		#region 对象实例及构造函数
		public static DictType Instance
		{
			get
			{
				return new DictType();
			}
		}

		public DictType() : base(SQLServerPortal.gc._dicTablePre + "DictType", "Id")
		{
			this.sortField = "Seq";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override DictTypeInfo DataReaderToEntity(IDataReader dataReader)
		{
			DictTypeInfo info = new DictTypeInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Id = reader.GetInt32("Id"); 	 //ID序号
			info.Pid = reader.GetInt32("Pid"); 	 //父节点ID序号
			info.Name = reader.GetString("Name"); 	 //名称
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
		protected override Hashtable GetHashByEntity(DictTypeInfo obj)
		{
			DictTypeInfo info = obj as DictTypeInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Id", info.Id); 	 //ID序号
			hash.Add("Pid", info.Pid); 	 //父节点ID序号
			hash.Add("Name", info.Name); 	 //名称
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
			dict.Add("Pid", "父节点ID序号");
			dict.Add("Name", "名称");
			dict.Add("Remark", "备注");
			dict.Add("Seq", "排序");
			dict.Add("EditorId", "编辑人编号");
			dict.Add("LastUpdateTime", "最后更新时间");
			#endregion
			return dict;
		}
		
		/// <summary>
        /// 获取所有字典类型的列表集合(Key为名称，Value为ID值）
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns></returns>
        public Dictionary<Int32, string> GetAllType(Int32 Pid)
        {
            string sql = string.Format("select Id, Name from {0}DictType where Pid ={3} order by {1} {2}",
                SQLServerPortal.gc._dicTablePre, sortField, IsDescending ? "DESC" : "ASC", Pid);

            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);

            Dictionary<Int32, string> list = new Dictionary<Int32, string>();
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    string name = dr["Name"].ToString();
                    Int32 value = dr["Id"].ToString().ToInt32();
                    if (!list.ContainsKey(value))
                    {
                        list.Add(value, name);
                    }
                }
            }
            return list;
        }


        public List<DictTypeNodeInfo> GetTree()
        {
            List<DictTypeNodeInfo> typeNodeList = new List<DictTypeNodeInfo>();
            string sql = string.Format("Select * From {0}DictType Order By Pid, Seq ", SQLServerPortal.gc._dicTablePre);
            Database db = CreateDatabase();
            DbCommand cmdWrapper = db.GetSqlStringCommand(sql);

            DataSet ds = db.ExecuteDataSet(cmdWrapper);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                DataRow[] dataRows = dt.Select(string.Format(" Pid = {0} ", -1));
                for (int i = 0; i < dataRows.Length; i++)
                {
                    Int32 id = Convert.ToInt32( dataRows[i]["Id"]);
                    DictTypeNodeInfo DictTypeNodeInfo = GetNode(id, dt);
                    typeNodeList.Add(DictTypeNodeInfo);
                }
            }

            return typeNodeList;
        }

        private DictTypeNodeInfo GetNode(Int32 id, DataTable dt)
        {
            DictTypeInfo DictTypeInfo = this.FindById(id);
            DictTypeNodeInfo DictTypeNodeInfo = new DictTypeNodeInfo(DictTypeInfo);

            DataRow[] dChildRows = dt.Select(string.Format(" Pid={0} ", id));

            for (int i = 0; i < dChildRows.Length; i++)
            {
                Int32 childId = dChildRows[i]["Id"].ToString().ToInt32();
                DictTypeNodeInfo childNodeInfo = GetNode(childId, dt);
                DictTypeNodeInfo.Children.Add(childNodeInfo);
            }
            return DictTypeNodeInfo;
        }
	}
}