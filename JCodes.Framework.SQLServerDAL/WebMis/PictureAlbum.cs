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
			
			info.ID = reader.GetString("ID");
			info.PID = reader.GetString("PID");
			info.Name = reader.GetString("Name");
			info.Note = reader.GetString("Note");
			info.Editor = reader.GetString("Editor");
			info.EditTime = reader.GetDateTime("EditTime");
			info.Creator = reader.GetString("Creator");
			info.CreateTime = reader.GetDateTime("CreateTime");
			
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
			
			hash.Add("ID", info.ID);
 			hash.Add("PID", info.PID);
 			hash.Add("Name", info.Name);
 			hash.Add("Note", info.Note);
 			hash.Add("Editor", info.Editor);
 			hash.Add("EditTime", info.EditTime);
 			hash.Add("Creator", info.Creator);
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
             dict.Add("PID", "父ID");
             dict.Add("Name", "名称");
             dict.Add("Note", "备注");
             dict.Add("Editor", "编辑人");
             dict.Add("EditTime", "编辑时间");
             dict.Add("Creator", "创建人");
             dict.Add("CreateTime", "创建时间");
             #endregion

            return dict;
        }

    }
}