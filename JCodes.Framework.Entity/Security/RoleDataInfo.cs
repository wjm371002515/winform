using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 角色的数据权限(RoleDataInfo)
	/// 对象号: 100062
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class RoleDataInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 角色Id
		/// </summary>
		private Int16 m_RoleId = 0;

		/// <summary>
		/// 公司列表
		/// </summary>
		private String m_CompanyLst = string.Empty;

		/// <summary>
		/// 部门列表
		/// </summary>
		private String m_DeptLst = string.Empty;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;
		#endregion

		#region Property Members

		/// <summary>
		/// ID序号
		/// </summary>
		[DataMember]
		[DisplayName("ID序号")]
		public virtual Int32 Id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
			}
		}

		/// <summary>
		/// 角色Id
		/// </summary>
		[DataMember]
		[DisplayName("角色Id")]
		public virtual Int16 RoleId
		{
			get
			{
				return this.m_RoleId;
			}
			set
			{
				this.m_RoleId = value;
			}
		}

		/// <summary>
		/// 公司列表
		/// </summary>
		[DataMember]
		[DisplayName("公司列表")]
		public virtual String CompanyLst
		{
			get
			{
				return this.m_CompanyLst;
			}
			set
			{
				this.m_CompanyLst = value;
			}
		}

		/// <summary>
		/// 部门列表
		/// </summary>
		[DataMember]
		[DisplayName("部门列表")]
		public virtual String DeptLst
		{
			get
			{
				return this.m_DeptLst;
			}
			set
			{
				this.m_DeptLst = value;
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
		#endregion
	}
}