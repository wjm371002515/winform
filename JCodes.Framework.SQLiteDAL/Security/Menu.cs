using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;

namespace JCodes.Framework.SQLiteDAL
{
    /// <summary>
    /// 功能菜单
    /// </summary>
    public class Menu : BaseDALSQLite<MenuInfo>, IMenu
    {
        #region 对象实例及构造函数

        public static Menu Instance
        {
            get
            {
                return new Menu();
            }
        }
        public Menu()
            : base("T_ACL_Menu", "ID")
        {
            this.sortField = "Seq";
            this.isDescending = false;
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

            info.ID = reader.GetString("ID");
            info.PID = reader.GetString("PID");
            info.Name = reader.GetString("Name");
            info.Icon = reader.GetString("Icon");
            info.Seq = reader.GetString("Seq");
            info.FunctionId = reader.GetString("FunctionId");
            info.Visible = reader.GetInt32("Visible") > 0;
            info.WinformType = reader.GetString("WinformType");
            info.Url = reader.GetString("Url");
            info.WebIcon = reader.GetString("WebIcon");
            info.SystemType_ID = reader.GetString("SystemType_ID");
            info.Creator = reader.GetString("Creator");
            info.Creator_ID = reader.GetString("Creator_ID");
            info.CreateTime = reader.GetDateTime("CreateTime");
            info.Editor = reader.GetString("Editor");
            info.Editor_ID = reader.GetString("Editor_ID");
            info.EditTime = reader.GetDateTime("EditTime");
            info.Deleted = reader.GetInt32("Deleted") > 0;

            return info;
        }

        /// <summary>
        /// 将实体对象的属性值转化为Hashtable对应的键值
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(MenuInfo obj)
        {
            MenuInfo info = obj as MenuInfo;
            Hashtable hash = new Hashtable();

            hash.Add("ID", info.ID);
            hash.Add("PID", info.PID);
            hash.Add("Name", info.Name);
            hash.Add("Icon", info.Icon);
            hash.Add("Seq", info.Seq);
            hash.Add("FunctionId", info.FunctionId);
            hash.Add("Visible", info.Visible ? 1 : 0);
            hash.Add("WinformType", info.WinformType);
            hash.Add("Url", info.Url);
            hash.Add("WebIcon", info.WebIcon);
            hash.Add("SystemType_ID", info.SystemType_ID);
            hash.Add("Creator", info.Creator);
            hash.Add("Creator_ID", info.Creator_ID);
            hash.Add("CreateTime", info.CreateTime);
            hash.Add("Editor", info.Editor);
            hash.Add("Editor_ID", info.Editor_ID);
            hash.Add("EditTime", info.EditTime);
            hash.Add("Deleted", info.Deleted ? 1 : 0);

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
            dict.Add("ID", "编号");            
            dict.Add("PID", "父ID");
            dict.Add("Name", "显示名称");
            dict.Add("Icon", "图标");
            dict.Add("Seq", "排序");
            dict.Add("FunctionId", "功能ID");
            dict.Add("Visible", "是否可见");
            dict.Add("WinformType", "Winform窗体类型");
            dict.Add("Url", "Web界面Url地址");
            dict.Add("WebIcon", "Web界面的菜单图标");
            dict.Add("SystemType_ID", "系统编号");
            dict.Add("Creator", "创建人");
            dict.Add("Creator_ID", "创建人ID");
            dict.Add("CreateTime", "创建时间");
            dict.Add("Editor", "编辑人");
            dict.Add("Editor_ID", "编辑人ID");
            dict.Add("EditTime", "编辑时间");
            dict.Add("Deleted", "是否已删除");
            #endregion

            return dict;
        }

        /// <summary>
        /// 获取树形结构的菜单列表
        /// </summary>
        public List<MenuNodeInfo> GetTree(string systemType)
        {
            string condition = !string.IsNullOrEmpty(systemType) ? string.Format("AND SystemType_ID='{0}'", systemType) : "";

            List<MenuNodeInfo> arrReturn = new List<MenuNodeInfo>();
            string sql = string.Format("Select * From {0} Where Visible > 0 {1} Order By PID, Seq ", tableName, condition);

            DataTable dt = base.SqlTable(sql);
            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dataRows = dt.Select(string.Format(" PID = '{0}' ", -1), sort);
            for (int i = 0; i < dataRows.Length; i++)
            {
                string id = dataRows[i]["ID"].ToString();
                MenuNodeInfo menuNodeInfo = GetNode(id, dt);
                arrReturn.Add(menuNodeInfo);
            }

            return arrReturn;
        }

        /// <summary>
        /// 获取所有的菜单列表
        /// </summary>
        public List<MenuInfo> GetAllMenu(string systemType)
        {
            string condition = !string.IsNullOrEmpty(systemType) ? string.Format("Where SystemType_ID='{0}'", systemType) : "";
            string sql = string.Format("Select * From {0} {1}  Order  By PID, Seq  ", tableName, condition);
            return GetList(sql, null);
        }

        private MenuNodeInfo GetNode(string id, DataTable dt)
        {
            MenuInfo menuInfo = this.FindByID(id);
            MenuNodeInfo menuNodeInfo = new MenuNodeInfo(menuInfo);

            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dChildRows = dt.Select(string.Format(" PID='{0}'", id), sort);

            for (int i = 0; i < dChildRows.Length; i++)
            {
                string childId = dChildRows[i]["ID"].ToString();
                MenuNodeInfo childNodeInfo = GetNode(childId, dt);
                menuNodeInfo.Children.Add(childNodeInfo);
            }
            return menuNodeInfo;
        }

        /// <summary>
        /// 获取第一级的菜单列表
        /// </summary>
        public List<MenuInfo> GetTopMenu(string systemType)
        {
            string condition = !string.IsNullOrEmpty(systemType) ? string.Format("AND SystemType_ID='{0}'", systemType) : "";
            string sql = string.Format("Select * From {0} Where Visible > 0 and PID='-1' {1} Order By Seq  ", tableName, condition);
            return GetList(sql, null);
        }

        /// <summary>
        /// 获取指定菜单下面的树形列表
        /// </summary>
        /// <param name="mainMenuID">指定菜单ID</param>
        public List<MenuNodeInfo> GetTreeByID(string mainMenuID)
        {
            List<MenuNodeInfo> arrReturn = new List<MenuNodeInfo>();
            string sql = string.Format("Select * From {0} Where Visible > 0 Order By PID, Seq ", tableName);

            DataTable dt = SqlTable(sql);
            string sort = string.Format("{0} {1}", GetSafeFileName(sortField), isDescending ? "DESC" : "ASC");
            DataRow[] dataRows = dt.Select(string.Format(" PID = '{0}'", mainMenuID), sort);
            for (int i = 0; i < dataRows.Length; i++)
            {
                string id = dataRows[i]["ID"].ToString();
                MenuNodeInfo menuNodeInfo = GetNode(id, dt);
                arrReturn.Add(menuNodeInfo);
            }

            return arrReturn;
        }

        /// <summary>
        /// 根据指定的父ID获取其下面一级（仅限一级）的菜单列表
        /// </summary>
        /// <param name="PID">菜单父ID</param>
        public List<MenuInfo> GetMenuByID(string PID)
        {
            string sql = string.Format(@"Select t.*,case pid when '-1' then '0' else pid end as parentId From {1} t 
                                         Where  PID='{0}' Order By Seq ", PID, tableName);
            return GetList(sql, null);
        }
    }
}