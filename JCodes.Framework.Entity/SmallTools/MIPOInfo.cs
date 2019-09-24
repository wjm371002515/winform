using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 客户基本信息(MIPOInfo)
	/// 对象号: 100091
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class MIPOInfo
	{
		#region Field Members

		/// <summary>
		/// 委托序号
		/// </summary>
		private string m_WTXH;

		/// <summary>
		/// 发行流水号
		/// </summary>
		private string m_FXLSH;

		/// <summary>
		/// 询价对象名称
		/// </summary>
		private string m_XJDXMC;

		/// <summary>
		/// 配售对象ID
		/// </summary>
		private string m_PSDXID;

		/// <summary>
		/// 配售对象名称
		/// </summary>
		private string m_PSDXMC;

		/// <summary>
		/// 配售对象类型
		/// </summary>
		private string m_PSDXLX;

		/// <summary>
		/// 证券代码
		/// </summary>
		private string m_ZQDM;

		/// <summary>
		/// 申购数量
		/// </summary>
		private string m_SGSL;

		/// <summary>
		/// 申购价格
		/// </summary>
		private string m_SGJG;

		/// <summary>
		/// 发行价格
		/// </summary>
		private string m_FXJG;

		/// <summary>
		/// 无内容
		/// </summary>
		private string m_REMARK1;

		/// <summary>
		/// 无内容2
		/// </summary>
		private string m_REMARK2;
		#endregion

		#region Property Members

		/// <summary>
		/// 委托序号
		/// </summary>
		[DataMember]
		public virtual string WTXH
		{
			get
			{
				return this.m_WTXH;
			}
			set
			{
				this.m_WTXH = value;
			}
		}

		/// <summary>
		/// 发行流水号
		/// </summary>
		[DataMember]
		public virtual string FXLSH
		{
			get
			{
				return this.m_FXLSH;
			}
			set
			{
				this.m_FXLSH = value;
			}
		}

		/// <summary>
		/// 询价对象名称
		/// </summary>
		[DataMember]
		public virtual string XJDXMC
		{
			get
			{
				return this.m_XJDXMC;
			}
			set
			{
				this.m_XJDXMC = value;
			}
		}

		/// <summary>
		/// 配售对象ID
		/// </summary>
		[DataMember]
		public virtual string PSDXID
		{
			get
			{
				return this.m_PSDXID;
			}
			set
			{
				this.m_PSDXID = value;
			}
		}

		/// <summary>
		/// 配售对象名称
		/// </summary>
		[DataMember]
		public virtual string PSDXMC
		{
			get
			{
				return this.m_PSDXMC;
			}
			set
			{
				this.m_PSDXMC = value;
			}
		}

		/// <summary>
		/// 配售对象类型
		/// </summary>
		[DataMember]
		public virtual string PSDXLX
		{
			get
			{
				return this.m_PSDXLX;
			}
			set
			{
				this.m_PSDXLX = value;
			}
		}

		/// <summary>
		/// 证券代码
		/// </summary>
		[DataMember]
		public virtual string ZQDM
		{
			get
			{
				return this.m_ZQDM;
			}
			set
			{
				this.m_ZQDM = value;
			}
		}

		/// <summary>
		/// 申购数量
		/// </summary>
		[DataMember]
		public virtual string SGSL
		{
			get
			{
				return this.m_SGSL;
			}
			set
			{
				this.m_SGSL = value;
			}
		}

		/// <summary>
		/// 申购价格
		/// </summary>
		[DataMember]
		public virtual string SGJG
		{
			get
			{
				return this.m_SGJG;
			}
			set
			{
				this.m_SGJG = value;
			}
		}

		/// <summary>
		/// 发行价格
		/// </summary>
		[DataMember]
		public virtual string FXJG
		{
			get
			{
				return this.m_FXJG;
			}
			set
			{
				this.m_FXJG = value;
			}
		}

		/// <summary>
		/// 无内容
		/// </summary>
		[DataMember]
		public virtual string REMARK1
		{
			get
			{
				return this.m_REMARK1;
			}
			set
			{
				this.m_REMARK1 = value;
			}
		}

		/// <summary>
		/// 无内容2
		/// </summary>
		[DataMember]
		public virtual string REMARK2
		{
			get
			{
				return this.m_REMARK2;
			}
			set
			{
				this.m_REMARK2 = value;
			}
		}
		#endregion
	}
}