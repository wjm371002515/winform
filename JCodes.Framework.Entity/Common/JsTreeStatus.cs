using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// JsTree的状态对象(JsTreeStatus)
	/// 对象号: 100025
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class JsTreeStatus
	{
		#region Field Members

		/// <summary>
		/// 是否打开
		/// </summary>
		private Int16 m_IsOpened = 0;

		/// <summary>
		/// 是否选中
		/// </summary>
		private Int16 m_IsSelected = 0;

		/// <summary>
		/// 是否可用
		/// </summary>
		private Int16 m_IsDisabled = 0;
		#endregion

		#region Property Members

		/// <summary>
		/// 是否打开
		/// </summary>
		[DataMember]
		[DisplayName("是否打开")]
		public virtual Int16 IsOpened
		{
			get
			{
				return this.m_IsOpened;
			}
			set
			{
				this.m_IsOpened = value;
			}
		}

		/// <summary>
		/// 是否选中
		/// </summary>
		[DataMember]
		[DisplayName("是否选中")]
		public virtual Int16 IsSelected
		{
			get
			{
				return this.m_IsSelected;
			}
			set
			{
				this.m_IsSelected = value;
			}
		}

		/// <summary>
		/// 是否可用
		/// </summary>
		[DataMember]
		[DisplayName("是否可用")]
		public virtual Int16 IsDisabled
		{
			get
			{
				return this.m_IsDisabled;
			}
			set
			{
				this.m_IsDisabled = value;
			}
		}

		/// <summary>
		/// 无参构造函数
		/// </summary>
		public  JsTreeStatus()
		{
			this.m_IsOpened = 0;
            this.m_IsSelected = 0;
            this.m_IsDisabled = 0;
		}

		/// <summary>
		/// 带参构造函数
		/// </summary>
		public  JsTreeStatus(Int16 isopened = 0, Int16 isselected = 0, Int16 isdisabled = 0)
		{
			 this.m_IsOpened = isopened;
            this.m_IsSelected = isselected;
            this.m_IsDisabled = isdisabled;
		}
		#endregion
	}
}