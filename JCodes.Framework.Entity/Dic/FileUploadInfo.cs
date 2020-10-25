using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 文件上传信息(FileUploadInfo)
	/// 对象号: 100003
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class FileUploadInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		private String m_Gid = string.Empty;

		/// <summary>
		/// 创建人编号
		/// </summary>
		private Int32 m_CreatorId = 0;

		/// <summary>
		/// 附件GUID
		/// </summary>
		private String m_AttachmentGid = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 基础路径
		/// </summary>
		private String m_BasePath = string.Empty;

		/// <summary>
		/// 文件保存相对路径
		/// </summary>
		private String m_SavePath = string.Empty;

		/// <summary>
		/// 上传文件分类
		/// </summary>
		private Int16 m_FileUploadType = 0;

		/// <summary>
		/// 文件大小
		/// </summary>
		private Int32 m_FileSize = 0;

		/// <summary>
		/// 文件扩展名
		/// </summary>
		private String m_FileExtend = string.Empty;

		/// <summary>
		/// 编辑人编号
		/// </summary>
		private Int32 m_EditorId = 0;

		/// <summary>
		/// 最后更新时间
		/// </summary>
		private DateTime m_LastUpdateTime = DateTime.Now;

		/// <summary>
		/// 是否删除
		/// </summary>
		private Int16 m_IsDelete = 0;

		/// <summary>
		/// 数据类型
		/// </summary>
		private byte[]  m_FileData;
		#endregion

		#region Property Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		[DataMember]
		[DisplayName("GUID对应的ID序号")]
		public virtual String Gid
		{
			get
			{
				return this.m_Gid;
			}
			set
			{
				this.m_Gid = value;
			}
		}

		/// <summary>
		/// 创建人编号
		/// </summary>
		[DataMember]
		[DisplayName("创建人编号")]
		public virtual Int32 CreatorId
		{
			get
			{
				return this.m_CreatorId;
			}
			set
			{
				this.m_CreatorId = value;
			}
		}

		/// <summary>
		/// 附件GUID
		/// </summary>
		[DataMember]
		[DisplayName("附件GUID")]
		public virtual String AttachmentGid
		{
			get
			{
				return this.m_AttachmentGid;
			}
			set
			{
				this.m_AttachmentGid = value;
			}
		}

		/// <summary>
		/// 名称
		/// </summary>
		[DataMember]
		[DisplayName("名称")]
		public virtual String Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		/// <summary>
		/// 基础路径
		/// </summary>
		[DataMember]
		[DisplayName("基础路径")]
		public virtual String BasePath
		{
			get
			{
				return this.m_BasePath;
			}
			set
			{
				this.m_BasePath = value;
			}
		}

		/// <summary>
		/// 文件保存相对路径
		/// </summary>
		[DataMember]
		[DisplayName("文件保存相对路径")]
		public virtual String SavePath
		{
			get
			{
				return this.m_SavePath;
			}
			set
			{
				this.m_SavePath = value;
			}
		}

		/// <summary>
		/// 上传文件分类
		/// 1-动态信息,
		/// 2-多媒体文件,
		/// 3-数据导入文件,
		/// 4-图片相册,
		/// 5-行业动态,
		/// 6-政策法规
		/// </summary>
		[DataMember]
		[DisplayName("上传文件分类")]
		public virtual Int16 FileUploadType
		{
			get
			{
				return this.m_FileUploadType;
			}
			set
			{
				this.m_FileUploadType = value;
			}
		}

		/// <summary>
		/// 文件大小
		/// </summary>
		[DataMember]
		[DisplayName("文件大小")]
		public virtual Int32 FileSize
		{
			get
			{
				return this.m_FileSize;
			}
			set
			{
				this.m_FileSize = value;
			}
		}

		/// <summary>
		/// 文件扩展名
		/// </summary>
		[DataMember]
		[DisplayName("文件扩展名")]
		public virtual String FileExtend
		{
			get
			{
				return this.m_FileExtend;
			}
			set
			{
				this.m_FileExtend = value;
			}
		}

		/// <summary>
		/// 编辑人编号
		/// </summary>
		[DataMember]
		[DisplayName("编辑人编号")]
		public virtual Int32 EditorId
		{
			get
			{
				return this.m_EditorId;
			}
			set
			{
				this.m_EditorId = value;
			}
		}

		/// <summary>
		/// 最后更新时间
		/// </summary>
		[DataMember]
		[DisplayName("最后更新时间")]
		public virtual DateTime LastUpdateTime
		{
			get
			{
				return this.m_LastUpdateTime;
			}
			set
			{
				this.m_LastUpdateTime = value;
			}
		}

		/// <summary>
		/// 是否删除
		/// 1-是,
		/// 2-否
		/// </summary>
		[DataMember]
		[DisplayName("是否删除")]
		public virtual Int16 IsDelete
		{
			get
			{
				return this.m_IsDelete;
			}
			set
			{
				this.m_IsDelete = value;
			}
		}

		/// <summary>
		/// 数据类型
		/// </summary>
		[DataMember]
		public virtual byte[]  FileData
		{
			get
			{
				return this.m_FileData;
			}
			set
			{
				this.m_FileData = value;
			}
		}
		#endregion
	}
}