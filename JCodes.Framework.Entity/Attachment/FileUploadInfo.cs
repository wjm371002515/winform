using System;
using System.Runtime.Serialization;
using JCodes.Framework.Entity;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 文件上传信息
	/// </summary>
	[Serializable]
	[DataContract]
	public class FileUploadInfo : BaseEntity 
	{
		#region Field Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		private String m_Gid = string.Empty;

		/// <summary>
		/// 创建人ID
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
		/// 分类编码
		/// </summary>
		private String m_CategoryCode = string.Empty;

		/// <summary>
		/// 文件大小
		/// </summary>
		private Int32 m_FileSize = 0;

		/// <summary>
		/// 文件扩展名
		/// </summary>
		private String m_FileExtend = string.Empty;

		/// <summary>
		/// 编辑人ID
		/// </summary>
		private Int32 m_EditorId = 0;

		/// <summary>
		/// 添加时间
		/// </summary>
		private DateTime m_AddTime = DateTime.Now;

		/// <summary>
		/// 是否删除
		/// </summary>
		private Int16 m_IsDelete = 0;
		#endregion

		#region Property Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		[DataMember]
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
		/// 创建人ID
		/// </summary>
		[DataMember]
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
		/// 分类编码
		/// </summary>
		[DataMember]
		public virtual String CategoryCode
		{
			get
			{
				return this.m_CategoryCode;
			}
			set
			{
				this.m_CategoryCode = value;
			}
		}

		/// <summary>
		/// 文件大小
		/// </summary>
		[DataMember]
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
		/// 编辑人ID
		/// </summary>
		[DataMember]
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
		/// 添加时间
		/// </summary>
		[DataMember]
		public virtual DateTime AddTime
		{
			get
			{
				return this.m_AddTime;
			}
			set
			{
				this.m_AddTime = value;
			}
		}

		/// <summary>
		/// 是否删除
		/// </summary>
		[DataMember]
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
		/// 文件流
		/// </summary>
		private byte[] m_FileData;
		/// <summary>
		/// 文件流数据
		/// </summary>
		[DataMember]
		public byte[] FileData
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