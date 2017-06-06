using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.SQLiteDAL
{
    /// <summary>
    /// 角色的数据权限
    /// </summary>
    public class RoleData : BaseDALSQLite<RoleDataInfo>, IRoleData
    {
        #region 对象实例及构造函数

        public static RoleData Instance
        {
            get
            {
                return new RoleData();
            }
        }
        public RoleData()
            : base(SQLitePortal.gc._securityTablePre+"RoleData", "ID")
        {
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

            info.ID = reader.GetString("ID");
            info.Role_ID = reader.GetInt32("Role_ID");
            info.BelongCompanys = reader.GetString("BelongCompanys");
            info.BelongDepts = reader.GetString("BelongDepts");
            info.ExcludeDepts = reader.GetString("ExcludeDepts");
            info.Note = reader.GetString("Note");

            return info;
        }

        /// <summary>
        /// 将实体对象的属性值转化为Hashtable对应的键值
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(RoleDataInfo obj)
        {
            RoleDataInfo info = obj as RoleDataInfo;
            Hashtable hash = new Hashtable();

            hash.Add("ID", info.ID);
            hash.Add("Role_ID", info.Role_ID);
            hash.Add("BelongCompanys", info.BelongCompanys);
            hash.Add("BelongDepts", info.BelongDepts);
            hash.Add("ExcludeDepts", info.ExcludeDepts);
            hash.Add("Note", info.Note);

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
            //dict.Add("ID", "编号");
            dict.Add("ID", "");
            dict.Add("Role_ID", "角色ID");
            dict.Add("BelongCompanys", "所属公司");
            dict.Add("BelongDepts", "所属部门");
            dict.Add("ExcludeDepts", "排除部门");
            dict.Add("Note", "备注");
            #endregion

            return dict;
        }
    }
}