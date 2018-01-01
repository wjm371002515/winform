using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.SQLServerDAL
{
    /// <summary>
    /// 基于BootStrap的图标
    /// </summary>
    public class BootstrapIcon : BaseDALSQLServer<BootstrapIconInfo>, IBootstrapIcon
	{
		#region 对象实例及构造函数

		public static BootstrapIcon Instance
		{
			get
			{
				return new BootstrapIcon();
			}
		}
		public BootstrapIcon() : base("TB_BootstrapIcon","ID")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override BootstrapIconInfo DataReaderToEntity(IDataReader dataReader)
		{
			BootstrapIconInfo info = new BootstrapIconInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.ID = reader.GetString("ID");
			info.DisplayName = reader.GetString("DisplayName");
			info.ClassName = reader.GetString("ClassName");
			info.SourceType = reader.GetString("SourceType");
			info.CreateTime = reader.GetDateTime("CreateTime");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(BootstrapIconInfo obj)
		{
		    BootstrapIconInfo info = obj as BootstrapIconInfo;
			Hashtable hash = new Hashtable(); 
			
			hash.Add("ID", info.ID);
 			hash.Add("DisplayName", info.DisplayName);
 			hash.Add("ClassName", info.ClassName);
 			hash.Add("SourceType", info.SourceType);
 			hash.Add("CreateTime", info.CreateTime);
 				
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
             dict.Add("DisplayName", "显示名称");
             dict.Add("ClassName", "样式名称");
             dict.Add("SourceType", "来源");
             dict.Add("CreateTime", "创建时间");
             #endregion

            return dict;
        }

    }
}