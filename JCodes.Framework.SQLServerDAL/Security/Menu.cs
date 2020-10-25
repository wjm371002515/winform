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
	/// 对象号: 000205
	/// 菜单信息表(Menu)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2018-02-08 14:18:18.043
	/// </summary>
	public partial class Menu : BaseDALSQLServer<MenuInfo>, IMenu
	{
		#region 对象实例及构造函数
		public static Menu Instance
		{
			get
			{
				return new Menu();
			}
		}

		public Menu() : base(SQLServerPortal.gc._securityTablePre + "Menu", "Gid")
		{
			this.sortField = "Seq";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override MenuInfo DataReaderToEntity(IDataReader dataReader)
		{
			MenuInfo info = new MenuInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Gid = reader.GetString("Gid"); 	 //GUID对应的ID序号
			info.Pgid = reader.GetString("Pgid"); 	 //父节点GUID对应的ID序号
			info.Name = reader.GetString("Name"); 	 //名称
			info.Icon = reader.GetString("Icon"); 	 //icon图标路径
			info.Seq = reader.GetString("Seq"); 	 //排序
			info.AuthGid = reader.GetString("AuthGid"); 	 //控制标识
			info.IsVisable = reader.GetInt16("IsVisable"); 	 //是否可见
			info.WinformClass = reader.GetString("WinformClass"); 	 //窗体类名
			info.Url = reader.GetString("Url"); 	 //URL地址
			info.WebIcon = reader.GetString("WebIcon"); 	 //Web对应的icon图标路径
			info.SystemtypeId = reader.GetString("SystemtypeId"); 	 //系统编号
			info.CreatorId = reader.GetInt32("CreatorId"); 	 //创建人编号
			info.CreatorTime = reader.GetDateTime("CreatorTime"); 	 //创建时间
			info.EditorId = reader.GetInt32("EditorId"); 	 //编辑人编号
			info.LastUpdateTime = reader.GetDateTime("LastUpdateTime"); 	 //最后更新时间
			info.IsDelete = reader.GetInt16("IsDelete"); 	 //是否删除
			info.DllPath = reader.GetString("DllPath"); 	 //映射路径
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(MenuInfo obj)
		{
			MenuInfo info = obj as MenuInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Gid", info.Gid); 	 //GUID对应的ID序号
			hash.Add("Pgid", info.Pgid); 	 //父节点GUID对应的ID序号
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("Icon", info.Icon); 	 //icon图标路径
			hash.Add("Seq", info.Seq); 	 //排序
			hash.Add("AuthGid", info.AuthGid); 	 //控制标识
			hash.Add("IsVisable", info.IsVisable); 	 //是否可见
			hash.Add("WinformClass", info.WinformClass); 	 //窗体类名
			hash.Add("Url", info.Url); 	 //URL地址
			hash.Add("WebIcon", info.WebIcon); 	 //Web对应的icon图标路径
			hash.Add("SystemtypeId", info.SystemtypeId); 	 //系统编号
			hash.Add("CreatorId", info.CreatorId); 	 //创建人编号
			hash.Add("CreatorTime", info.CreatorTime); 	 //创建时间
			hash.Add("EditorId", info.EditorId); 	 //编辑人编号
			hash.Add("LastUpdateTime", info.LastUpdateTime); 	 //最后更新时间
			hash.Add("IsDelete", info.IsDelete); 	 //是否删除
			hash.Add("DllPath", info.DllPath); 	 //映射路径
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
			dict.Add("Gid", "GUID对应的ID序号");
			dict.Add("Pgid", "父节点GUID对应的ID序号");
			dict.Add("Name", "名称");
			dict.Add("Icon", "icon图标路径");
			dict.Add("Seq", "排序");
			dict.Add("AuthGid", "控制标识");
			dict.Add("IsVisable", "是否可见");
			dict.Add("WinformClass", "窗体类名");
			dict.Add("Url", "URL地址");
			dict.Add("WebIcon", "Web对应的icon图标路径");
			dict.Add("SystemtypeId", "系统编号");
			dict.Add("CreatorId", "创建人编号");
			dict.Add("CreatorTime", "创建时间");
			dict.Add("EditorId", "编辑人编号");
			dict.Add("LastUpdateTime", "最后更新时间");
			dict.Add("IsDelete", "是否删除");
			dict.Add("DllPath", "映射路径");
			#endregion
			return dict;
		}
		
		/// <summary>
        /// 获取树形结构的菜单列表
        /// </summary>
        public List<MenuNodeInfo> GetTree(string systemtypeId, IsVisable isVisable = IsVisable.是, IsDelete isDelete = IsDelete.否)
        {
            string condition = !string.IsNullOrEmpty(systemtypeId) ? string.Format("AND SystemtypeId='{0}'", systemtypeId) : "";
            List<MenuNodeInfo> arrReturn = new List<MenuNodeInfo>();
            string sql = string.Format("Select * From {0} Where (IsVisable = {2} or 0 = {2}) {1} Order By Pgid, Seq ", tableName, condition, (short)isVisable);

            DataTable dt = base.SqlTable(sql);
            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dataRows = dt.Select(string.Format(" Pgid = '{0}' ", -1), sort);
            for (int i = 0; i < dataRows.Length; i++)
            {
                string id = dataRows[i]["Gid"].ToString();
                MenuNodeInfo menuNodeInfo = GetNode(id, dt);
                arrReturn.Add(menuNodeInfo);
            }

            return arrReturn;
        }

        /// <summary>
        /// 获取所有的菜单列表
        /// </summary>
        public List<MenuInfo> GetAllMenu(string systemtypeId, IsVisable isVisable = IsVisable.是, IsDelete isDelete = IsDelete.否)
        {
            string condition = !string.IsNullOrEmpty(systemtypeId) ? string.Format("Where SystemtypeId='{0}' and (IsVisable = {1} or 0 = {1}) and (IsDelete = {2} or 0 = {2})", systemtypeId, (short)isVisable, (short)isDelete) : string.Format("Where (IsVisable = {0} or 0 = {0}) and (IsDelete = {1} or 0 = {1})", (short)isVisable, (short)isDelete);
            string sql = string.Format("Select * From {0} {1} Order  By Pgid, Seq  ", tableName, condition);
            return GetList(sql, null);
        }

        private MenuNodeInfo GetNode(string gid, DataTable dt, IsVisable isVisable = IsVisable.是, IsDelete isDelete = IsDelete.否)
        {
            MenuInfo menuInfo = this.FindById(gid);
            MenuNodeInfo menuNodeInfo = new MenuNodeInfo(menuInfo);

            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dChildRows = dt.Select(string.Format(" Pgid='{0}' and (IsVisable = {1} or 0 = {1}) and (IsDelete = {2} or 0 = {2})", gid, (short)isVisable, (short)isDelete), sort);

            for (int i = 0; i < dChildRows.Length; i++)
            {
                string childId = dChildRows[i]["Gid"].ToString();
                MenuNodeInfo childNodeInfo = GetNode(childId, dt, isVisable);
                menuNodeInfo.Children.Add(childNodeInfo);
            }
            return menuNodeInfo;
        }

        /// <summary>
        /// 获取第一级的菜单列表
        /// </summary>
        public List<MenuInfo> GetTopMenu(string systemtypeId, IsVisable isVisable = IsVisable.是, IsDelete isDelete = IsDelete.否)
        {
            string condition = !string.IsNullOrEmpty(systemtypeId) ? string.Format("AND SystemtypeId='{0}'", systemtypeId) : "";
            string sql = string.Format("Select * From {0} Where (IsVisable = {2} or 0 = {2}) and (IsDelete = {3} or 0 = {3}) and Pgid='-1' {1} Order By Seq  ", tableName, condition, (short)isVisable, (short)isDelete);
            return GetList(sql, null);
        }

        /// <summary>
        /// 获取指定菜单下面的树形列表
        /// </summary>
        /// <param name="mainMenuID">指定菜单ID</param>
        public List<MenuNodeInfo> GetTreeById(string mainMenuId, IsVisable isVisable = IsVisable.是, IsDelete isDelete = IsDelete.否)
        {
            List<MenuNodeInfo> arrReturn = new List<MenuNodeInfo>();
            string sql = string.Format("Select * From {0} Where (IsVisable = {1} or 0 = {1}) Order By Pgid, Seq ", tableName, (short)isVisable);

            DataTable dt = SqlTable(sql);
            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dataRows = dt.Select(string.Format(" Pgid = '{0}' and (IsVisable = {1} or 0 = {1}) and (IsDelete = {2} or 0 = {2})", mainMenuId, (short)isVisable, (short)isDelete), sort);
            for (int i = 0; i < dataRows.Length; i++)
            {
                string id = dataRows[i]["Gid"].ToString();
                MenuNodeInfo menuNodeInfo = GetNode(id, dt);
                arrReturn.Add(menuNodeInfo);
            }

            return arrReturn;
        }

        /// <summary>
        /// 根据指定的父ID获取其下面一级（仅限一级）的菜单列表
        /// </summary>
        /// <param name="PID">菜单父ID</param>
        public List<MenuInfo> GetMenuById(string Pgid, IsVisable isVisable = IsVisable.是, IsDelete isDelete = IsDelete.否)
        {
            string sql = string.Format(@"Select t.*,case Pgid when '-1' then '0' else Pgid end as parentId From {1} t 
                                         Where  Pgid='{0}' and (IsVisable = {2} or 0 = {2}) and (IsDelete = {3} or 0 = {3}) Order By Seq ", Pgid, tableName, (short)isVisable, (short)isDelete);
            return GetList(sql, null);
        }
	}
}