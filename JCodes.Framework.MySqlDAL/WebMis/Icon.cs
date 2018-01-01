using System.Collections;
using System.Data;
using System.Collections.Generic;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.MySqlDAL
{
    /// <summary>
    /// 系统图表库
    /// </summary>
    public class Icon : BaseDALMySql<IconInfo>, IIcon
	{
		#region 对象实例及构造函数

		public static Icon Instance
		{
			get
			{
				return new Icon();
			}
		}
		public Icon() : base("TB_Icon","ID")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override IconInfo DataReaderToEntity(IDataReader dataReader)
		{
			IconInfo info = new IconInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.ID = reader.GetString("ID");
			info.IconCls = reader.GetString("IconCls");
			info.IconUrl = reader.GetString("IconUrl");
			info.IconSize = reader.GetInt32("IconSize");
			info.CreateTime = reader.GetDateTime("CreateTime");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(IconInfo obj)
		{
		    IconInfo info = obj as IconInfo;
			Hashtable hash = new Hashtable(); 
			
			hash.Add("ID", info.ID);
 			hash.Add("IconCls", info.IconCls);
 			hash.Add("IconUrl", info.IconUrl);
 			hash.Add("IconSize", info.IconSize);
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
             dict.Add("IconCls", "样式名称");
             dict.Add("IconUrl", "URL地址");
             dict.Add("IconSize", "尺寸");
             dict.Add("CreateTime", "创建时间");
             #endregion

            return dict;
        }

    }
}