using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 表格实体类(TableEntity)
	/// 对象号: 100031
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class TableEntity
	{
		#region Field Members

		/// <summary>
		/// 表名
		/// </summary>
		private String m_TableName = string.Empty;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;

		/// <summary>
		/// 文件夹名字
		/// </summary>
		private String m_FolderName = string.Empty;

		/// <summary>
		/// 是否继承IDXDataError
		/// </summary>
		private Int16 m_IsIDXDataError = 0;

		/// <summary>
		/// 是否继承BaseEntity
		/// </summary>
		private Int16 m_IsBaseEntity = 0;

		/// <summary>
		/// ToString内容
		/// </summary>
		private String m_ToStringContent = string.Empty;

		/// <summary>
		/// 构造函数内容
		/// </summary>
		private String m_ConstructContent = string.Empty;

		/// <summary>
		/// 自定义父类
		/// </summary>
		private String m_CustomParentClass = string.Empty;

		/// <summary>
		/// 自定义文本
		/// </summary>
		private String m_CustomContent = string.Empty;

		/// <summary>
		/// 自定义命名空间
		/// </summary>
		private String m_CustomNamespace = string.Empty;
		#endregion

		#region Property Members

		/// <summary>
		/// 表名
		/// </summary>
		[DataMember]
		[DisplayName("表名")]
		public virtual String TableName
		{
			get
			{
				return this.m_TableName;
			}
			set
			{
				this.m_TableName = value;
			}
		}

		/// <summary>
		/// 备注
		/// </summary>
		[DataMember]
		[DisplayName("备注")]
		public virtual String Remark
		{
			get
			{
				return this.m_Remark;
			}
			set
			{
				this.m_Remark = value;
			}
		}

		/// <summary>
		/// 文件夹名字
		/// </summary>
		[DataMember]
		[DisplayName("文件夹名字")]
		public virtual String FolderName
		{
			get
			{
				return this.m_FolderName;
			}
			set
			{
				this.m_FolderName = value;
			}
		}

		/// <summary>
		/// 是否继承IDXDataError
		/// 1-是,
		/// 2-否
		/// </summary>
		[DataMember]
		[DisplayName("是否继承IDXDataError")]
		public virtual Int16 IsIDXDataError
		{
			get
			{
				return this.m_IsIDXDataError;
			}
			set
			{
				this.m_IsIDXDataError = value;
			}
		}

		/// <summary>
		/// 是否继承BaseEntity
		/// 1-是,
		/// 2-否
		/// </summary>
		[DataMember]
		[DisplayName("是否继承BaseEntity")]
		public virtual Int16 IsBaseEntity
		{
			get
			{
				return this.m_IsBaseEntity;
			}
			set
			{
				this.m_IsBaseEntity = value;
			}
		}

		/// <summary>
		/// ToString内容
		/// </summary>
		[DataMember]
		[DisplayName("ToString内容")]
		public virtual String ToStringContent
		{
			get
			{
				return this.m_ToStringContent;
			}
			set
			{
				this.m_ToStringContent = value;
			}
		}

		/// <summary>
		/// 构造函数内容
		/// </summary>
		[DataMember]
		[DisplayName("构造函数内容")]
		public virtual String ConstructContent
		{
			get
			{
				return this.m_ConstructContent;
			}
			set
			{
				this.m_ConstructContent = value;
			}
		}

		/// <summary>
		/// 自定义父类
		/// </summary>
		[DataMember]
		[DisplayName("自定义父类")]
		public virtual String CustomParentClass
		{
			get
			{
				return this.m_CustomParentClass;
			}
			set
			{
				this.m_CustomParentClass = value;
			}
		}

		/// <summary>
		/// 自定义文本
		/// </summary>
		[DataMember]
		[DisplayName("自定义文本")]
		public virtual String CustomContent
		{
			get
			{
				return this.m_CustomContent;
			}
			set
			{
				this.m_CustomContent = value;
			}
		}

		/// <summary>
		/// 自定义命名空间
		/// </summary>
		[DataMember]
		[DisplayName("自定义命名空间")]
		public virtual String CustomNamespace
		{
			get
			{
				return this.m_CustomNamespace;
			}
			set
			{
				this.m_CustomNamespace = value;
			}
		}
		#endregion
	}
}