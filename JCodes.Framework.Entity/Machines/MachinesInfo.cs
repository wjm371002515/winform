using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 服务器信息(MachinesInfo)
	/// 对象号: 100036
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class MachinesInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_ID = 0;

		/// <summary>
		/// 管理人
		/// </summary>
		private string m_GLY;

		/// <summary>
		/// 过保日期
		/// </summary>
		private string m_GBRQ;

		/// <summary>
		/// 公网端口
		/// </summary>
		private string m_GWFWDK;

		/// <summary>
		/// 公网IP
		/// </summary>
		private string m_GWIP;

		/// <summary>
		/// 机柜位置
		/// </summary>
		private string m_JGWZ;

		/// <summary>
		/// 机器用途
		/// </summary>
		private string m_JQRT;

		/// <summary>
		/// 机器型号
		/// </summary>
		private string m_JQXH;

		/// <summary>
		/// 内网开放端口
		/// </summary>
		private string m_NWFWDK;

		/// <summary>
		/// 内网IP
		/// </summary>
		private string m_NWIP;

		/// <summary>
		/// 表格类型
		/// </summary>
		private string m_TABTYPE;

		/// <summary>
		/// 数据来源
		/// </summary>
		private string m_WJLY;

		/// <summary>
		/// 外网开放端口
		/// </summary>
		private string m_WWFWDK;

		/// <summary>
		/// 外网IP
		/// </summary>
		private string m_WWIP;

		/// <summary>
		/// 系统版本
		/// </summary>
		private string m_XTBB;

		/// <summary>
		/// 硬件序列号
		/// </summary>
		private string m_YJXLH;

		/// <summary>
		/// 应用版本
		/// </summary>
		private string m_YYBB;

		/// <summary>
		/// 主管
		/// </summary>
		private string m_ZG;

		/// <summary>
		/// 修改备注
		/// </summary>
		private string m_MODIFYMARK;
		#endregion

		#region Property Members

		/// <summary>
		/// ID序号
		/// </summary>
		[DataMember]
		public virtual Int32 ID
		{
			get
			{
				return this.m_ID;
			}
			set
			{
				this.m_ID = value;
			}
		}

		/// <summary>
		/// 管理人
		/// </summary>
		[DataMember]
		public virtual string GLY
		{
			get
			{
				return this.m_GLY;
			}
			set
			{
				this.m_GLY = value;
			}
		}

		/// <summary>
		/// 过保日期
		/// </summary>
		[DataMember]
		public virtual string GBRQ
		{
			get
			{
				return this.m_GBRQ;
			}
			set
			{
				this.m_GBRQ = value;
			}
		}

		/// <summary>
		/// 公网端口
		/// </summary>
		[DataMember]
		public virtual string GWFWDK
		{
			get
			{
				return this.m_GWFWDK;
			}
			set
			{
				this.m_GWFWDK = value;
			}
		}

		/// <summary>
		/// 公网IP
		/// </summary>
		[DataMember]
		public virtual string GWIP
		{
			get
			{
				return this.m_GWIP;
			}
			set
			{
				this.m_GWIP = value;
			}
		}

		/// <summary>
		/// 机柜位置
		/// </summary>
		[DataMember]
		public virtual string JGWZ
		{
			get
			{
				return this.m_JGWZ;
			}
			set
			{
				this.m_JGWZ = value;
			}
		}

		/// <summary>
		/// 机器用途
		/// </summary>
		[DataMember]
		public virtual string JQRT
		{
			get
			{
				return this.m_JQRT;
			}
			set
			{
				this.m_JQRT = value;
			}
		}

		/// <summary>
		/// 机器型号
		/// </summary>
		[DataMember]
		public virtual string JQXH
		{
			get
			{
				return this.m_JQXH;
			}
			set
			{
				this.m_JQXH = value;
			}
		}

		/// <summary>
		/// 内网开放端口
		/// </summary>
		[DataMember]
		public virtual string NWFWDK
		{
			get
			{
				return this.m_NWFWDK;
			}
			set
			{
				this.m_NWFWDK = value;
			}
		}

		/// <summary>
		/// 内网IP
		/// </summary>
		[DataMember]
		public virtual string NWIP
		{
			get
			{
				return this.m_NWIP;
			}
			set
			{
				this.m_NWIP = value;
			}
		}

		/// <summary>
		/// 表格类型
		/// </summary>
		[DataMember]
		public virtual string TABTYPE
		{
			get
			{
				return this.m_TABTYPE;
			}
			set
			{
				this.m_TABTYPE = value;
			}
		}

		/// <summary>
		/// 数据来源
		/// </summary>
		[DataMember]
		public virtual string WJLY
		{
			get
			{
				return this.m_WJLY;
			}
			set
			{
				this.m_WJLY = value;
			}
		}

		/// <summary>
		/// 外网开放端口
		/// </summary>
		[DataMember]
		public virtual string WWFWDK
		{
			get
			{
				return this.m_WWFWDK;
			}
			set
			{
				this.m_WWFWDK = value;
			}
		}

		/// <summary>
		/// 外网IP
		/// </summary>
		[DataMember]
		public virtual string WWIP
		{
			get
			{
				return this.m_WWIP;
			}
			set
			{
				this.m_WWIP = value;
			}
		}

		/// <summary>
		/// 系统版本
		/// </summary>
		[DataMember]
		public virtual string XTBB
		{
			get
			{
				return this.m_XTBB;
			}
			set
			{
				this.m_XTBB = value;
			}
		}

		/// <summary>
		/// 硬件序列号
		/// </summary>
		[DataMember]
		public virtual string YJXLH
		{
			get
			{
				return this.m_YJXLH;
			}
			set
			{
				this.m_YJXLH = value;
			}
		}

		/// <summary>
		/// 应用版本
		/// </summary>
		[DataMember]
		public virtual string YYBB
		{
			get
			{
				return this.m_YYBB;
			}
			set
			{
				this.m_YYBB = value;
			}
		}

		/// <summary>
		/// 主管
		/// </summary>
		[DataMember]
		public virtual string ZG
		{
			get
			{
				return this.m_ZG;
			}
			set
			{
				this.m_ZG = value;
			}
		}

		/// <summary>
		/// 修改备注
		/// </summary>
		[DataMember]
		public virtual string MODIFYMARK
		{
			get
			{
				return this.m_MODIFYMARK;
			}
			set
			{
				this.m_MODIFYMARK = value;
			}
		}
		#endregion
	}
}