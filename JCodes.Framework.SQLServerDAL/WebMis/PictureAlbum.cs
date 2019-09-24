using System.Collections;
using System.Data;
using System.Collections.Generic;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.IDAL;

namespace JCodes.Framework.SQLServerDAL
{
    /// <summary>
    /// 图片相册
    /// </summary>
    public class PictureAlbum : BaseDALSQLServer<PictureAlbumInfo>, IPictureAlbum
	{
		#region 对象实例及构造函数

		public static PictureAlbum Instance
		{
			get
			{
				return new PictureAlbum();
			}
		}
		public PictureAlbum() : base("TB_PictureAlbum","ID")
		{
		}

		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override PictureAlbumInfo DataReaderToEntity(IDataReader dataReader)
		{
			PictureAlbumInfo info = new PictureAlbumInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);

            /*info.Id = reader.GetInt32("Id");
            info.Pid = reader.GetInt32("Pid");
			info.Name = reader.GetString("Name");
            info.Remark = reader.GetString("Remark");
            info.EditorId = reader.GetInt32("EditorId");
            info.LastUpdateTime = reader.GetDateTime("LastUpdateTime");
            info.CreatorId = reader.GetInt32("CreatorId");
            info.CreatorTime = reader.GetDateTime("CreatorTime");*/
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(PictureAlbumInfo obj)
		{
		    PictureAlbumInfo info = obj as PictureAlbumInfo;
			Hashtable hash = new Hashtable();

            /*hash.Add("Id", info.Id);
            hash.Add("Pid", info.Pid);
 			hash.Add("Name", info.Name);
            hash.Add("Remark", info.Remark);
            hash.Add("EditorId", info.EditorId);
            hash.Add("LastUpdateTime", info.LastUpdateTime);
            hash.Add("CreatorId", info.CreatorId);
            hash.Add("CreatorTime", info.CreatorTime);*/
 				
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
           /* //dict.Add("ID", "编号");
            dict.Add("Id", "");
            dict.Add("Pid", "父ID");
            dict.Add("Name", "名称");
            dict.Add("Remark", "备注");
            dict.Add("EditorId", "编辑人");
            dict.Add("LastUpdateTime", "编辑时间");
            dict.Add("CreatorId", "创建人");
            dict.Add("CreatorTime", "创建时间");*/
            #endregion

            return dict;
        }

    }
}