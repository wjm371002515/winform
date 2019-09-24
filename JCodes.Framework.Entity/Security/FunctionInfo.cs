using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 系统功能定义(FunctionInfo)
	/// 对象号: 100056
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class FunctionInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		private String m_Gid = string.Empty;

		/// <summary>
		/// 父节点GUID对应的ID序号
		/// </summary>
		private String m_Pgid = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 映射路径
		/// </summary>
		private String m_DllPath = string.Empty;

		/// <summary>
		/// 系统编号
		/// </summary>
		private String m_SystemtypeId = string.Empty;

		/// <summary>
		/// 排序
		/// </summary>
		private String m_Seq = string.Empty;
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
		/// 父节点GUID对应的ID序号
		/// </summary>
		[DataMember]
		[DisplayName("父节点GUID对应的ID序号")]
		public virtual String Pgid
		{
			get
			{
				return this.m_Pgid;
			}
			set
			{
				this.m_Pgid = value;
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
		/// 映射路径
		/// </summary>
		[DataMember]
		[DisplayName("映射路径")]
		public virtual String DllPath
		{
			get
			{
				return this.m_DllPath;
			}
			set
			{
				this.m_DllPath = value;
			}
		}

		/// <summary>
		/// 系统编号
		/// </summary>
		[DataMember]
		[DisplayName("系统编号")]
		public virtual String SystemtypeId
		{
			get
			{
				return this.m_SystemtypeId;
			}
			set
			{
				this.m_SystemtypeId = value;
			}
		}

		/// <summary>
		/// 排序
		/// </summary>
		[DataMember]
		[DisplayName("排序")]
		public virtual String Seq
		{
			get
			{
				return this.m_Seq;
			}
			set
			{
				this.m_Seq = value;
			}
		}
		#endregion
	}
}