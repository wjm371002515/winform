using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 淘宝财务数据处理(MCaiwuInfo)
	/// 对象号: 100088
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class MCaiwuInfo
	{
		#region Field Members

		/// <summary>
		/// 年
		/// </summary>
		private string m_年;

		/// <summary>
		/// 月
		/// </summary>
		private string m_月;

		/// <summary>
		/// 生效日期
		/// </summary>
		private string m_生效日期;

		/// <summary>
		/// 出库机构代码
		/// </summary>
		private string m_出库机构代码;

		/// <summary>
		/// 出库机构
		/// </summary>
		private string m_出库机构;

		/// <summary>
		/// 客户名称代码
		/// </summary>
		private string m_客户名称代码;

		/// <summary>
		/// 客户名称
		/// </summary>
		private string m_客户名称;

		/// <summary>
		/// 供应商
		/// </summary>
		private string m_供应商;

		/// <summary>
		/// 单据编号
		/// </summary>
		private string m_单据编号;

		/// <summary>
		/// 单据类型
		/// </summary>
		private string m_单据类型;

		/// <summary>
		/// 商品编码
		/// </summary>
		private string m_商品编码;

		/// <summary>
		/// 商品名称
		/// </summary>
		private string m_商品名称;

		/// <summary>
		/// 规格
		/// </summary>
		private string m_规格;

		/// <summary>
		/// 单位
		/// </summary>
		private string m_单位;

		/// <summary>
		/// 产地
		/// </summary>
		private string m_产地;

		/// <summary>
		/// 数量
		/// </summary>
		private string m_数量;

		/// <summary>
		/// 进价金额
		/// </summary>
		private string m_进价金额;

		/// <summary>
		/// 备注
		/// </summary>
		private string m_备注;

		/// <summary>
		/// 配送金额
		/// </summary>
		private string m_配送金额;

		/// <summary>
		/// 底价金额
		/// </summary>
		private string m_底价金额;

		/// <summary>
		/// 零售金额
		/// </summary>
		private string m_零售金额;
		#endregion

		#region Property Members

		/// <summary>
		/// 年
		/// </summary>
		[DataMember]
		public virtual string 年
		{
			get
			{
				return this.m_年;
			}
			set
			{
				this.m_年 = value;
			}
		}

		/// <summary>
		/// 月
		/// </summary>
		[DataMember]
		public virtual string 月
		{
			get
			{
				return this.m_月;
			}
			set
			{
				this.m_月 = value;
			}
		}

		/// <summary>
		/// 生效日期
		/// </summary>
		[DataMember]
		public virtual string 生效日期
		{
			get
			{
				return this.m_生效日期;
			}
			set
			{
				this.m_生效日期 = value;
			}
		}

		/// <summary>
		/// 出库机构代码
		/// </summary>
		[DataMember]
		public virtual string 出库机构代码
		{
			get
			{
				return this.m_出库机构代码;
			}
			set
			{
				this.m_出库机构代码 = value;
			}
		}

		/// <summary>
		/// 出库机构
		/// </summary>
		[DataMember]
		public virtual string 出库机构
		{
			get
			{
				return this.m_出库机构;
			}
			set
			{
				this.m_出库机构 = value;
			}
		}

		/// <summary>
		/// 客户名称代码
		/// </summary>
		[DataMember]
		public virtual string 客户名称代码
		{
			get
			{
				return this.m_客户名称代码;
			}
			set
			{
				this.m_客户名称代码 = value;
			}
		}

		/// <summary>
		/// 客户名称
		/// </summary>
		[DataMember]
		public virtual string 客户名称
		{
			get
			{
				return this.m_客户名称;
			}
			set
			{
				this.m_客户名称 = value;
			}
		}

		/// <summary>
		/// 供应商
		/// </summary>
		[DataMember]
		public virtual string 供应商
		{
			get
			{
				return this.m_供应商;
			}
			set
			{
				this.m_供应商 = value;
			}
		}

		/// <summary>
		/// 单据编号
		/// </summary>
		[DataMember]
		public virtual string 单据编号
		{
			get
			{
				return this.m_单据编号;
			}
			set
			{
				this.m_单据编号 = value;
			}
		}

		/// <summary>
		/// 单据类型
		/// </summary>
		[DataMember]
		public virtual string 单据类型
		{
			get
			{
				return this.m_单据类型;
			}
			set
			{
				this.m_单据类型 = value;
			}
		}

		/// <summary>
		/// 商品编码
		/// </summary>
		[DataMember]
		public virtual string 商品编码
		{
			get
			{
				return this.m_商品编码;
			}
			set
			{
				this.m_商品编码 = value;
			}
		}

		/// <summary>
		/// 商品名称
		/// </summary>
		[DataMember]
		public virtual string 商品名称
		{
			get
			{
				return this.m_商品名称;
			}
			set
			{
				this.m_商品名称 = value;
			}
		}

		/// <summary>
		/// 规格
		/// </summary>
		[DataMember]
		public virtual string 规格
		{
			get
			{
				return this.m_规格;
			}
			set
			{
				this.m_规格 = value;
			}
		}

		/// <summary>
		/// 单位
		/// </summary>
		[DataMember]
		public virtual string 单位
		{
			get
			{
				return this.m_单位;
			}
			set
			{
				this.m_单位 = value;
			}
		}

		/// <summary>
		/// 产地
		/// </summary>
		[DataMember]
		public virtual string 产地
		{
			get
			{
				return this.m_产地;
			}
			set
			{
				this.m_产地 = value;
			}
		}

		/// <summary>
		/// 数量
		/// </summary>
		[DataMember]
		public virtual string 数量
		{
			get
			{
				return this.m_数量;
			}
			set
			{
				this.m_数量 = value;
			}
		}

		/// <summary>
		/// 进价金额
		/// </summary>
		[DataMember]
		public virtual string 进价金额
		{
			get
			{
				return this.m_进价金额;
			}
			set
			{
				this.m_进价金额 = value;
			}
		}

		/// <summary>
		/// 备注
		/// </summary>
		[DataMember]
		public virtual string 备注
		{
			get
			{
				return this.m_备注;
			}
			set
			{
				this.m_备注 = value;
			}
		}

		/// <summary>
		/// 配送金额
		/// </summary>
		[DataMember]
		public virtual string 配送金额
		{
			get
			{
				return this.m_配送金额;
			}
			set
			{
				this.m_配送金额 = value;
			}
		}

		/// <summary>
		/// 底价金额
		/// </summary>
		[DataMember]
		public virtual string 底价金额
		{
			get
			{
				return this.m_底价金额;
			}
			set
			{
				this.m_底价金额 = value;
			}
		}

		/// <summary>
		/// 零售金额
		/// </summary>
		[DataMember]
		public virtual string 零售金额
		{
			get
			{
				return this.m_零售金额;
			}
			set
			{
				this.m_零售金额 = value;
			}
		}
		#endregion
	}
}