using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.AccessDAL
{
    /// <summary>
    /// 相关附件信息
    /// </summary>
	public class FileUploads : BaseDALAccess<FileUploadInfo>, IFileUploads
	{
		#region 对象实例及构造函数

		public static FileUploads Instance
		{
			get
			{
				return new FileUploads();
			}
		}
        public FileUploads()
            : base("TB_FileUpload", "ID")
        {
            this.sortField = "AddTime";
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

            info.Gid = reader.GetString("Gid");
            info.CreatorId = reader.GetInt32("CreatorId");
            info.AttachmentGid = reader.GetString("AttachmentGid");
            info.Name = reader.GetString("Name");
            info.BasePath = reader.GetString("BasePath");
            info.SavePath = reader.GetString("SavePath");
            info.CategoryCode = reader.GetString("CategoryCode");
            info.FileSize = reader.GetInt32("FileSize");
            info.FileExtend = reader.GetString("FileExtend");
            info.EditorId = reader.GetInt32("EditorId");
            info.AddTime = reader.GetDateTime("AddTime");
            info.IsDelete = reader.GetInt16("IsDelete");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(FileUploadInfo obj)
		{
		    FileUploadInfo info = obj as FileUploadInfo;
			Hashtable hash = new Hashtable();

            hash.Add("Gid", info.Gid);
            hash.Add("CreatorId", info.CreatorId);
            hash.Add("AttachmentGid", info.AttachmentGid);
            hash.Add("Name", info.Name);
            hash.Add("BasePath", info.BasePath);
            hash.Add("SavePath", info.SavePath);
            hash.Add("CategoryCode", info.CategoryCode);
            hash.Add("FileSize", info.FileSize);
            hash.Add("FileExtend", info.FileExtend);
            hash.Add("EditorId", info.EditorId);
            hash.Add("AddTime", info.AddTime);
            hash.Add("IsDelete", info.IsDelete);
 				
			return hash;
		}

        /// <summary>
        /// 获取指定用户的上传信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetAllByUser(Int32 userId)
        {
            if (userId != 0)
            {
                string condition = string.Format("Editor ='{0}' ", userId);
                return Find(condition);
            }
            else
            {
                return GetAll();
            }
        }

        /// <summary>
        /// 获取指定用户的上传信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="category">附件分类：个人附件，业务附件</param>
        /// <param name="pagerInfo">分页信息</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetAllByUser(Int32 userId, string category, PagerInfo pagerInfo)
        {
            SearchCondition cond = new SearchCondition();
            cond.AddCondition("Editor", userId, SqlOperator.Equal)
                .AddCondition("Category", category, SqlOperator.Equal);

            string condition = cond.BuildConditionSql(DatabaseType.Access).Replace("Where", "");

            return FindWithPager(condition, pagerInfo);
        }
        
        /// <summary>
        /// 获取指定附件组GUID的附件信息
        /// </summary>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <param name="pagerInfo">分页信息</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByAttachGUID(string attachmentGUID, PagerInfo pagerInfo)
        {
            string condition = string.Format("AttachmentGUID='{0}' ", attachmentGUID);
            return FindWithPager(condition, pagerInfo);
        }

        /// <summary>
        /// 获取指定附件组GUID的附件信息
        /// </summary>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <returns></returns>
        public List<FileUploadInfo> GetByAttachGUID(string attachmentGUID)
        {
            if (string.IsNullOrEmpty(attachmentGUID))
            {
                throw new ArgumentException("附件组GUID不能为空", attachmentGUID);
            }
            else
            {
                string condition = string.Format("AttachmentGUID='{0}' ", attachmentGUID);
                return Find(condition);
            }
        }

        /// <summary>
        /// 根据文件的相对路径，删除文件
        /// </summary>
        /// <param name="relativeFilePath"></param>
        /// <returns></returns>
        public bool DeleteByFilePath(string relativeFilePath, string userId)
        {
            string condition = string.Format("SavePath ='{0}' and Editor ='{1}' ", relativeFilePath, userId);
            return base.DeleteByCondition(condition);
        }

        /// <summary>
        /// 根据附件组GUID获取对应的文件名列表，方便列出文件名
        /// </summary>
        /// <param name="attachmentGUID">附件组GUID</param>
        /// <returns>返回ID和文件名的列表</returns>
        public Dictionary<string, string> GetFileNames(string attachmentGUID)
        {
            string sql = string.Format("Select ID,FileName from {0} WHERE AttachmentGUID='{1}' ", tableName, attachmentGUID);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);

            Dictionary<string, string> dict = new Dictionary<string, string>();
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    string id = dr["ID"].ToString();
                    dict.Add(id, dr["FileName"].ToString());
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
            string sql = string.Format("Update {0} set DeleteFlag = 1 WHERE ID='{1}' ", tableName, id);
            Database db = CreateDatabase();
            DbCommand command = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(command) > 0;
        }
    }
}