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
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.SQLServerDAL
{
	/// <summary>
	/// 对象号: 000011
	/// 上传文件表(FileUpload)
	/// 版本: 1.0.0.0
	/// 表结构最后更新时间: 2019-10-06 00:45:08.347
	/// </summary>
	public partial class FileUpload : BaseDALSQLServer<FileUploadInfo>, IFileUpload
	{
		#region 对象实例及构造函数
		public static FileUpload Instance
		{
			get
			{
				return new FileUpload();
			}
		}

		public FileUpload() : base(SQLServerPortal.gc._dicTablePre + "FileUpload", "Gid")
		{
			this.sortField = "Gid";
		}
		#endregion

		/// <summary>
		/// 将DataReader的属性值转化为实体类的属性值，返回实体类
		/// </summary>
		/// <param name="dr">有效的DataReader对象</param>
		/// <returns>实体类对象</returns>
		protected override FileUploadInfo DataReaderToEntity(IDataReader dataReader)
		{
			FileUploadInfo info = new FileUploadInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			info.Gid = reader.GetString("Gid"); 	 //GUID对应的ID序号
			info.CreatorId = reader.GetInt32("CreatorId"); 	 //创建人编号
			info.AttachmentGid = reader.GetString("AttachmentGid"); 	 //附件GUID
			info.Name = reader.GetString("Name"); 	 //名称
			info.BasePath = reader.GetString("BasePath"); 	 //基础路径
			info.SavePath = reader.GetString("SavePath"); 	 //文件保存相对路径
			info.FileUploadType = reader.GetInt16("FileUploadType"); 	 //上传文件分类
			info.FileSize = reader.GetInt32("FileSize"); 	 //文件大小
			info.FileExtend = reader.GetString("FileExtend"); 	 //文件扩展名
			info.EditorId = reader.GetInt32("EditorId"); 	 //编辑人编号
			info.LastUpdateTime = reader.GetDateTime("LastUpdateTime"); 	 //最后更新时间
			info.IsDelete = reader.GetInt16("IsDelete"); 	 //是否删除
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="dr">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
		protected override Hashtable GetHashByEntity(FileUploadInfo obj)
		{
			FileUploadInfo info = obj as FileUploadInfo;
			Hashtable hash = new Hashtable();
			hash.Add("Gid", info.Gid); 	 //GUID对应的ID序号
			hash.Add("CreatorId", info.CreatorId); 	 //创建人编号
			hash.Add("AttachmentGid", info.AttachmentGid); 	 //附件GUID
			hash.Add("Name", info.Name); 	 //名称
			hash.Add("BasePath", info.BasePath); 	 //基础路径
			hash.Add("SavePath", info.SavePath); 	 //文件保存相对路径
			hash.Add("FileUploadType", info.FileUploadType); 	 //上传文件分类
			hash.Add("FileSize", info.FileSize); 	 //文件大小
			hash.Add("FileExtend", info.FileExtend); 	 //文件扩展名
			hash.Add("EditorId", info.EditorId); 	 //编辑人编号
			hash.Add("LastUpdateTime", info.LastUpdateTime); 	 //最后更新时间
			hash.Add("IsDelete", info.IsDelete); 	 //是否删除
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
			dict.Add("CreatorId", "创建人编号");
			dict.Add("AttachmentGid", "附件GUID");
			dict.Add("Name", "名称");
			dict.Add("BasePath", "基础路径");
			dict.Add("SavePath", "文件保存相对路径");
			dict.Add("FileUploadType", "上传文件分类");
			dict.Add("FileSize", "文件大小");
			dict.Add("FileExtend", "文件扩展名");
			dict.Add("EditorId", "编辑人编号");
			dict.Add("LastUpdateTime", "最后更新时间");
			dict.Add("IsDelete", "是否删除");
			#endregion
			return dict;
		}
		
		/// <summary>
        /// 获取指定用户的上传信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetAllByUserId(Int32 userId, bool isSuperAdmin, IsDelete isDelete)
        {
            if (!isSuperAdmin)
            {
                string condition = string.Format("EditorId ={0} and IsDelete = {1}", userId, (short)isDelete);
                return Find(condition);
            }
            else
            {
                string condition = string.Format("IsDelete = {1}", userId, (short)isDelete);
                return Find(condition);
            }
        }

        /// <summary>
        /// 获取指定用户的上传信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="FileUploadType">附件分类：个人附件，业务附件</param>
        /// <param name="pagerInfo">分页信息</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetAllByUserId(Int32 userId, AttachmentType attachmentType, PagerInfo pagerInfo)
        {
            SearchCondition cond = new SearchCondition();
            cond.AddCondition("EditorId", userId, SqlOperator.Equal)
                .AddCondition("FileUploadType", (short)attachmentType, SqlOperator.Equal);

            string condition = cond.BuildConditionSql(DatabaseType.SqlServer).Replace("Where", "");

            return FindWithPager(condition, pagerInfo);
        }

        /// <summary>
        /// 获取指定附件组GUID的附件信息
        /// </summary>
        /// <param name="attachmentGid">附件组GUID</param>
        /// <param name="pagerInfo">分页信息</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByAttachGid(string attachmentGid, PagerInfo pagerInfo)
        {
            if (string.IsNullOrEmpty(attachmentGid))
            {
                throw new ArgumentException("附件组GUID不能为空", attachmentGid);
            }

            string condition = string.Format("AttachmentGid='{0}' ", attachmentGid);
            return FindWithPager(condition, pagerInfo);
        }

        /// <summary>
        /// 获取指定附件组GUID的附件信息
        /// </summary>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByAttachGid(string attachmentGid)
        {
            if (string.IsNullOrEmpty(attachmentGid))
            {
                throw new ArgumentException("附件组GUID不能为空", attachmentGid);
            }
            else
            {
                string condition = string.Format("AttachmentGid='{0}' ", attachmentGid);
                return Find(condition);
            }
        }

        /// <summary>
        /// 根据文件的相对路径，删除文件
        /// </summary>
        /// <param name="relativeFilePath"></param>
        /// <returns></returns>
        public bool DeleteByFilePath(string savePath, Int32 userId)
        {
            string condition = string.Format("SavePath ='{0}' and EditorId ={1} ", savePath, userId);
            return base.DeleteByCondition(condition);
        }

        /// <summary>
        /// 根据附件组GUID获取对应的文件名列表，方便列出文件名
        /// </summary>
        /// <param name="attachmentGid">附件组GUID</param>
        /// <returns>返回ID和文件名的列表</returns>
        public Dictionary<string, string> GetFileNames(string attachmentGid)
        {
            string sql = string.Format("Select Gid, Name from {0} WHERE AttachmentGid='{1}' ", tableName, attachmentGid);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);

            Dictionary<string, string> dict = new Dictionary<string, string>();
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    string id = dr["Gid"].ToString();
                    dict.Add(id, dr["Name"].ToString());
                }
            }
            return dict;
        }

        /// <summary>
        /// 标记为删除（不直接删除)
        /// </summary>
        /// <param name="id">文件的ID</param>
        /// <returns></returns>
        public bool SetDeleteFlag(string id)
        {
            string sql = string.Format("Update {0} set IsDelete = {2} WHERE Gid='{1}' ", tableName, id, (short)IsDelete.是);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(command) > 0;
        }
	}
}