using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 框架实体类的基类(BaseEntity)
	/// 对象号: 100004
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class BaseEntity
	{
		#region Field Members

		/// <summary>
		/// 登陆用户ID
		/// </summary>
		private Int32 m_CurrentLoginUserId = 0;

		/// <summary>
		/// 额外数据1
		/// </summary>
		private String m_Data1 = string.Empty;

		/// <summary>
		/// 额外数据2
		/// </summary>
		private String m_Data2 = string.Empty;

		/// <summary>
		/// 额外数据3(传递非字符串数据)
		/// </summary>
        private String m_Data3 = string.Empty;
		#endregion

		#region Property Members

		/// <summary>
		/// 登陆用户ID
		/// 当前登录用户ID。该字段不保存到数据表中，只用于记录用户的操作日志。
		/// </summary>
		[DataMember]
		[DisplayName("登陆用户ID")]
		public virtual Int32 CurrentLoginUserId
		{
			get
			{
				return this.m_CurrentLoginUserId;
			}
			set
			{
				this.m_CurrentLoginUserId = value;
			}
		}

		/// <summary>
		/// 额外数据1
		/// 用来给实体类传递一些额外的数据，如外键的转义等，该字段不保存到数据表中
		/// </summary>
		[DataMember]
		[DisplayName("额外数据1")]
		public virtual String Data1
		{
			get
			{
				return this.m_Data1;
			}
			set
			{
				this.m_Data1 = value;
			}
		}

		/// <summary>
		/// 额外数据2
		/// 用来给实体类传递一些额外的数据，如外键的转义等，该字段不保存到数据表中
		/// </summary>
		[DataMember]
		[DisplayName("额外数据2")]
		public virtual String Data2
		{
			get
			{
				return this.m_Data2;
			}
			set
			{
				this.m_Data2 = value;
			}
		}

		/// <summary>
		/// 额外数据3
		/// 用来给实体类传递一些额外的数据，如外键的转义等，该字段不保存到数据表中
		/// </summary>
		[DataMember]
		[DisplayName("额外数据3")]
        public virtual String Data3
		{
			get
			{
				return this.m_Data3;
			}
			set
			{
				this.m_Data3 = value;
			}
		}
		#endregion
	}
}