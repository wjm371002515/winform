using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 系统信息(SysparameterInfo)
	/// 对象号: 100021
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class SysparameterInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 系统参数Id
		/// </summary>
		private Int16 m_SysId = 0;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 系统键
		/// </summary>
		private String m_SysValue = string.Empty;

		/// <summary>
		/// 控件类型
		/// </summary>
		private Int16 m_ControlType = 0;

		/// <summary>
		/// 数据字典编号
		/// </summary>
		private Int32 m_DicNo = 0;

		/// <summary>
		/// 整形长度
		/// </summary>
		private Int32 m_NumLen = 0;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;

		/// <summary>
		/// 排序
		/// </summary>
		private String m_Seq = string.Empty;

		/// <summary>
		/// 编辑人编号
		/// </summary>
		private Int32 m_EditorId = 0;

		/// <summary>
		/// 最后更新时间
		/// </summary>
		private DateTime m_LastUpdateTime = DateTime.Now;
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
		/// 系统参数Id
		/// 1-系统参数,
		/// 99-其他
		/// </summary>
		[DataMember]
		[DisplayName("系统参数Id")]
		public virtual Int16 SysId
		{
			get
			{
				return this.m_SysId;
			}
			set
			{
				this.m_SysId = value;
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
		/// 系统键
		/// </summary>
		[DataMember]
		[DisplayName("系统键")]
		public virtual String SysValue
		{
			get
			{
				return this.m_SysValue;
			}
			set
			{
				this.m_SysValue = value;
			}
		}

		/// <summary>
		/// 控件类型
		/// 1-文本框,
		/// 2-整数框,
		/// 3-下拉单选框,
		/// 4-下拉多选框,
		/// 5-勾选框,
		/// 6-日期,
		/// 7-密码,
		/// 8-小数框,
		/// 9-时间框
		/// </summary>
		[DataMember]
		[DisplayName("控件类型")]
		public virtual Int16 ControlType
		{
			get
			{
				return this.m_ControlType;
			}
			set
			{
				this.m_ControlType = value;
			}
		}

		/// <summary>
		/// 数据字典编号
		/// </summary>
		[DataMember]
		[DisplayName("数据字典编号")]
		public virtual Int32 DicNo
		{
			get
			{
				return this.m_DicNo;
			}
			set
			{
				this.m_DicNo = value;
			}
		}

		/// <summary>
		/// 整形长度
		/// </summary>
		[DataMember]
		[DisplayName("整形长度")]
		public virtual Int32 NumLen
		{
			get
			{
				return this.m_NumLen;
			}
			set
			{
				this.m_NumLen = value;
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
		#endregion
	}
}