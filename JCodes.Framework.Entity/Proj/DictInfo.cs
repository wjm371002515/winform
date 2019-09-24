using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 字典信息(DictInfo)
	/// 对象号: 100041
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class DictInfo : IComparable<DictInfo>
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 父节点ID序号
		/// </summary>
		private Int32 m_Pid = 0;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

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
		/// 父节点ID序号
		/// </summary>
		[DataMember]
		[DisplayName("父节点ID序号")]
		public virtual Int32 Pid
		{
			get
			{
				return this.m_Pid;
			}
			set
			{
				this.m_Pid = value;
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
		/// Compares to.
		/// </summary>
		public Int32 CompareTo(DictInfo other)
		{
			if (other == null) return -1;
            if (Id > other.Id)
            {
                return 1;
            }
            return -1;
		}
		#endregion
	}
}