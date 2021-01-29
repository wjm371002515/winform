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
			info.FileChunk = reader.GetInt32("FileChunk"); 	 //当前文件块编号
			info.FileChunks = reader.GetInt32("FileChunks"); 	 //文件块总数量
			info.Md5Value = reader.GetString("Md5Value"); 	 //MD5值
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
			hash.Add("FileChunk", info.FileChunk); 	 //当前文件块编号
			hash.Add("FileChunks", info.FileChunks); 	 //文件块总数量
			hash.Add("Md5Value", info.Md5Value); 	 //MD5值
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
			dict.Add("FileChunk", "当前文件块编号");
			dict.Add("FileChunks", "文件块总数量");
			dict.Add("Md5Value", "MD5值");
			#endregion
			return dict;
		}

        public List<FileUploadInfo> GetAllByUserId(int userId, bool isSuperAdmin, IsDelete isDelete)
        {
            throw new NotImplementedException();
        }

        public List<FileUploadInfo> GetAllByUserId(int userId, AttachmentType attachmentType, PagerInfo pagerInfo)
        {
            throw new NotImplementedException();
        }

        public List<FileUploadInfo> GetByAttachGid(string attachmentGid, PagerInfo pagerInfo)
        {
            throw new NotImplementedException();
        }

        public List<FileUploadInfo> GetByAttachGid(string attachmentGid)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByFilePath(string savePath, int userId)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetFileNames(string attachmentGid)
        {
            throw new NotImplementedException();
        }

        public bool SetDeleteFlag(string Id)
        {
            throw new NotImplementedException();
        }
    }
}