using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 浙商证券EB 实体类（第二版）(MEBInfo)
	/// 对象号: 100089
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class MEBInfo
	{
		#region Field Members

		/// <summary>
		/// 机构名或姓名
		/// </summary>
		private string m_OrganizeName;

		/// <summary>
		/// 证券账户户名（上海）
		/// </summary>
		private string m_AccountName;

		/// <summary>
		/// 证券账户代码（上海）
		/// </summary>
		private string m_AccountCode;

		/// <summary>
		/// 托管席位号
		/// </summary>
		private string m_Seat;

		/// <summary>
		/// 身份证明号码（如营业执照注册号等）
		/// </summary>
		private string m_CardId;

		/// <summary>
		/// 到期赎回价格
		/// </summary>
		private string m_Rate;

		/// <summary>
		/// 申购金额
		/// </summary>
		private string m_Balance;

		/// <summary>
		/// 退款汇入行全称
		/// </summary>
		private string m_BankName;

		/// <summary>
		/// 退款收款人全称
		/// </summary>
		private string m_ClientName;

		/// <summary>
		/// 退款收款人账号
		/// </summary>
		private string m_BankAccount;

		/// <summary>
		/// 大额支付系统号
		/// </summary>
		private string m_SystemId;

		/// <summary>
		/// 退款汇入行省份
		/// </summary>
		private string m_BankProvince;

		/// <summary>
		/// 退款汇入行地市
		/// </summary>
		private string m_BankCity;

		/// <summary>
		/// 标识
		/// </summary>
		private string m_Ident;
		#endregion

		#region Property Members

		/// <summary>
		/// 机构名或姓名
		/// </summary>
		[DataMember]
		public virtual string OrganizeName
		{
			get
			{
				return this.m_OrganizeName;
			}
			set
			{
				this.m_OrganizeName = value;
			}
		}

		/// <summary>
		/// 证券账户户名（上海）
		/// </summary>
		[DataMember]
		public virtual string AccountName
		{
			get
			{
				return this.m_AccountName;
			}
			set
			{
				this.m_AccountName = value;
			}
		}

		/// <summary>
		/// 证券账户代码（上海）
		/// </summary>
		[DataMember]
		public virtual string AccountCode
		{
			get
			{
				return this.m_AccountCode;
			}
			set
			{
				this.m_AccountCode = value;
			}
		}

		/// <summary>
		/// 托管席位号
		/// </summary>
		[DataMember]
		public virtual string Seat
		{
			get
			{
				return this.m_Seat;
			}
			set
			{
				this.m_Seat = value;
			}
		}

		/// <summary>
		/// 身份证明号码（如营业执照注册号等）
		/// </summary>
		[DataMember]
		public virtual string CardId
		{
			get
			{
				return this.m_CardId;
			}
			set
			{
				this.m_CardId = value;
			}
		}

		/// <summary>
		/// 到期赎回价格
		/// </summary>
		[DataMember]
		public virtual string Rate
		{
			get
			{
				return this.m_Rate;
			}
			set
			{
				this.m_Rate = value;
			}
		}

		/// <summary>
		/// 申购金额
		/// </summary>
		[DataMember]
		public virtual string Balance
		{
			get
			{
				return this.m_Balance;
			}
			set
			{
				this.m_Balance = value;
			}
		}

		/// <summary>
		/// 退款汇入行全称
		/// </summary>
		[DataMember]
		public virtual string BankName
		{
			get
			{
				return this.m_BankName;
			}
			set
			{
				this.m_BankName = value;
			}
		}

		/// <summary>
		/// 退款收款人全称
		/// </summary>
		[DataMember]
		public virtual string ClientName
		{
			get
			{
				return this.m_ClientName;
			}
			set
			{
				this.m_ClientName = value;
			}
		}

		/// <summary>
		/// 退款收款人账号
		/// </summary>
		[DataMember]
		public virtual string BankAccount
		{
			get
			{
				return this.m_BankAccount;
			}
			set
			{
				this.m_BankAccount = value;
			}
		}

		/// <summary>
		/// 大额支付系统号
		/// </summary>
		[DataMember]
		public virtual string SystemId
		{
			get
			{
				return this.m_SystemId;
			}
			set
			{
				this.m_SystemId = value;
			}
		}

		/// <summary>
		/// 退款汇入行省份
		/// </summary>
		[DataMember]
		public virtual string BankProvince
		{
			get
			{
				return this.m_BankProvince;
			}
			set
			{
				this.m_BankProvince = value;
			}
		}

		/// <summary>
		/// 退款汇入行地市
		/// </summary>
		[DataMember]
		public virtual string BankCity
		{
			get
			{
				return this.m_BankCity;
			}
			set
			{
				this.m_BankCity = value;
			}
		}

		/// <summary>
		/// 标识
		/// </summary>
		[DataMember]
		public virtual string Ident
		{
			get
			{
				return this.m_Ident;
			}
			set
			{
				this.m_Ident = value;
			}
		}
		#endregion
	}
}