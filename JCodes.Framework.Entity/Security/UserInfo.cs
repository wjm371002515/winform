using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 用户信息(UserInfo)
	/// 对象号: 100066
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class UserInfo : SimpleUserInfo
	{
		#region Field Members

		/// <summary>
		/// 登录名
		/// </summary>
		private String m_LoginName = string.Empty;

		/// <summary>
		/// 是否过期
		/// </summary>
		private Int16 m_IsExpire = 0;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;

		/// <summary>
		/// 身份证
		/// </summary>
		private String m_IdCard = string.Empty;

		/// <summary>
		/// 办公电话
		/// </summary>
		private String m_OfficePhone = string.Empty;

		/// <summary>
		/// 家庭电话
		/// </summary>
		private String m_HomePhone = string.Empty;

		/// <summary>
		/// 地址
		/// </summary>
		private String m_Address = string.Empty;

		/// <summary>
		/// 工作地址
		/// </summary>
		private String m_WorkAddress = string.Empty;

		/// <summary>
		/// 性别
		/// </summary>
		private Int16 m_Gender = 0;

		/// <summary>
		/// 生日
		/// </summary>
		private DateTime m_Birthday = DateTime.Now;

		/// <summary>
		/// QQ号
		/// </summary>
		private Int32 m_QQ = 0;

		/// <summary>
		/// 个性签名
		/// </summary>
		private String m_Signature = string.Empty;

		/// <summary>
		/// 审核状态
		/// </summary>
		private Int16 m_AuditStatus = 0;

		/// <summary>
		/// 个人图片
		/// </summary>
		private String m_Portrait = string.Empty;

		/// <summary>
		/// 部门Id
		/// </summary>
		private Int32 m_DeptId = 0;

		/// <summary>
		/// 公司Id
		/// </summary>
		private Int32 m_CompanyId = 0;

		/// <summary>
		/// 排序
		/// </summary>
		private String m_Seq = string.Empty;

		/// <summary>
		/// 创建人编号
		/// </summary>
		private Int32 m_CreatorId = 0;

		/// <summary>
		/// 创建时间
		/// </summary>
		private DateTime m_CreatorTime = DateTime.Now;

		/// <summary>
		/// 编辑人编号
		/// </summary>
		private Int32 m_EditorId = 0;

		/// <summary>
		/// 最后更新时间
		/// </summary>
		private DateTime m_LastUpdateTime = DateTime.Now;

		/// <summary>
		/// 是否删除
		/// </summary>
		private Int16 m_IsDelete = 0;

		/// <summary>
		/// 问题1
		/// </summary>
		private String m_Question1 = string.Empty;

		/// <summary>
		/// 问题2
		/// </summary>
		private String m_Question2 = string.Empty;

		/// <summary>
		/// 问题3
		/// </summary>
		private String m_Question3 = string.Empty;

		/// <summary>
		/// 回答1
		/// </summary>
		private String m_Answer1 = string.Empty;

		/// <summary>
		/// 回答2
		/// </summary>
		private String m_Answer2 = string.Empty;

		/// <summary>
		/// 回答3
		/// </summary>
		private String m_Answer3 = string.Empty;

		/// <summary>
		/// 最后登录IP
		/// </summary>
		private String m_LastLoginIp = string.Empty;

		/// <summary>
		/// 最后登录Mac
		/// </summary>
		private String m_LastLoginMac = string.Empty;

		/// <summary>
		/// 最后登录日期
		/// </summary>
		private DateTime m_LastLoginTime = DateTime.Now;

		/// <summary>
		/// 最后修改密码时间
		/// </summary>
		private DateTime m_LastChangePwdTime = DateTime.Now;

		/// <summary>
		/// 当前登录IP
		/// </summary>
		private String m_CurLoginIp = string.Empty;

		/// <summary>
		/// 当前登录Mac
		/// </summary>
		private String m_CurLoginMac = string.Empty;

		/// <summary>
		/// 当前登录日期
		/// </summary>
		private DateTime m_CurLoginTime = DateTime.Now;
		#endregion

		#region Property Members

		/// <summary>
		/// 登录名
		/// </summary>
		[DataMember]
		[DisplayName("登录名")]
		public virtual String LoginName
		{
			get
			{
				return this.m_LoginName;
			}
			set
			{
				this.m_LoginName = value;
			}
		}

		/// <summary>
		/// 是否过期
		/// 1-是,
		/// 2-否
		/// </summary>
		[DataMember]
		[DisplayName("是否过期")]
		public virtual Int16 IsExpire
		{
			get
			{
				return this.m_IsExpire;
			}
			set
			{
				this.m_IsExpire = value;
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
		/// 身份证
		/// </summary>
		[DataMember]
		[DisplayName("身份证")]
		public virtual String IdCard
		{
			get
			{
				return this.m_IdCard;
			}
			set
			{
				this.m_IdCard = value;
			}
		}

		/// <summary>
		/// 办公电话
		/// </summary>
		[DataMember]
		[DisplayName("办公电话")]
		public virtual String OfficePhone
		{
			get
			{
				return this.m_OfficePhone;
			}
			set
			{
				this.m_OfficePhone = value;
			}
		}

		/// <summary>
		/// 家庭电话
		/// </summary>
		[DataMember]
		[DisplayName("家庭电话")]
		public virtual String HomePhone
		{
			get
			{
				return this.m_HomePhone;
			}
			set
			{
				this.m_HomePhone = value;
			}
		}

		/// <summary>
		/// 地址
		/// </summary>
		[DataMember]
		[DisplayName("地址")]
		public virtual String Address
		{
			get
			{
				return this.m_Address;
			}
			set
			{
				this.m_Address = value;
			}
		}

		/// <summary>
		/// 工作地址
		/// </summary>
		[DataMember]
		[DisplayName("工作地址")]
		public virtual String WorkAddress
		{
			get
			{
				return this.m_WorkAddress;
			}
			set
			{
				this.m_WorkAddress = value;
			}
		}

		/// <summary>
		/// 性别
		/// 1-男,
		/// 2-女,
		/// 2-保密
		/// </summary>
		[DataMember]
		[DisplayName("性别")]
		public virtual Int16 Gender
		{
			get
			{
				return this.m_Gender;
			}
			set
			{
				this.m_Gender = value;
			}
		}

		/// <summary>
		/// 生日
		/// </summary>
		[DataMember]
		[DisplayName("生日")]
		public virtual DateTime Birthday
		{
			get
			{
				return this.m_Birthday;
			}
			set
			{
				this.m_Birthday = value;
			}
		}

		/// <summary>
		/// QQ号
		/// </summary>
		[DataMember]
		[DisplayName("QQ号")]
		public virtual Int32 QQ
		{
			get
			{
				return this.m_QQ;
			}
			set
			{
				this.m_QQ = value;
			}
		}

		/// <summary>
		/// 个性签名
		/// </summary>
		[DataMember]
		[DisplayName("个性签名")]
		public virtual String Signature
		{
			get
			{
				return this.m_Signature;
			}
			set
			{
				this.m_Signature = value;
			}
		}

		/// <summary>
		/// 审核状态
		/// 1-未审核,
		/// 2-已审核,
		/// 3-审核中
		/// </summary>
		[DataMember]
		[DisplayName("审核状态")]
		public virtual Int16 AuditStatus
		{
			get
			{
				return this.m_AuditStatus;
			}
			set
			{
				this.m_AuditStatus = value;
			}
		}

		/// <summary>
		/// 个人图片
		/// </summary>
		[DataMember]
		[DisplayName("个人图片")]
		public virtual String Portrait
		{
			get
			{
				return this.m_Portrait;
			}
			set
			{
				this.m_Portrait = value;
			}
		}

		/// <summary>
		/// 部门Id
		/// </summary>
		[DataMember]
		[DisplayName("部门Id")]
		public virtual Int32 DeptId
		{
			get
			{
				return this.m_DeptId;
			}
			set
			{
				this.m_DeptId = value;
			}
		}

		/// <summary>
		/// 公司Id
		/// </summary>
		[DataMember]
		[DisplayName("公司Id")]
		public virtual Int32 CompanyId
		{
			get
			{
				return this.m_CompanyId;
			}
			set
			{
				this.m_CompanyId = value;
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
		/// 创建人编号
		/// </summary>
		[DataMember]
		[DisplayName("创建人编号")]
		public virtual Int32 CreatorId
		{
			get
			{
				return this.m_CreatorId;
			}
			set
			{
				this.m_CreatorId = value;
			}
		}

		/// <summary>
		/// 创建时间
		/// </summary>
		[DataMember]
		[DisplayName("创建时间")]
		public virtual DateTime CreatorTime
		{
			get
			{
				return this.m_CreatorTime;
			}
			set
			{
				this.m_CreatorTime = value;
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

		/// <summary>
		/// 是否删除
		/// 1-是,
		/// 2-否
		/// </summary>
		[DataMember]
		[DisplayName("是否删除")]
		public virtual Int16 IsDelete
		{
			get
			{
				return this.m_IsDelete;
			}
			set
			{
				this.m_IsDelete = value;
			}
		}

		/// <summary>
		/// 问题1
		/// </summary>
		[DataMember]
		[DisplayName("问题1")]
		public virtual String Question1
		{
			get
			{
				return this.m_Question1;
			}
			set
			{
				this.m_Question1 = value;
			}
		}

		/// <summary>
		/// 问题2
		/// </summary>
		[DataMember]
		[DisplayName("问题2")]
		public virtual String Question2
		{
			get
			{
				return this.m_Question2;
			}
			set
			{
				this.m_Question2 = value;
			}
		}

		/// <summary>
		/// 问题3
		/// </summary>
		[DataMember]
		[DisplayName("问题3")]
		public virtual String Question3
		{
			get
			{
				return this.m_Question3;
			}
			set
			{
				this.m_Question3 = value;
			}
		}

		/// <summary>
		/// 回答1
		/// </summary>
		[DataMember]
		[DisplayName("回答1")]
		public virtual String Answer1
		{
			get
			{
				return this.m_Answer1;
			}
			set
			{
				this.m_Answer1 = value;
			}
		}

		/// <summary>
		/// 回答2
		/// </summary>
		[DataMember]
		[DisplayName("回答2")]
		public virtual String Answer2
		{
			get
			{
				return this.m_Answer2;
			}
			set
			{
				this.m_Answer2 = value;
			}
		}

		/// <summary>
		/// 回答3
		/// </summary>
		[DataMember]
		[DisplayName("回答3")]
		public virtual String Answer3
		{
			get
			{
				return this.m_Answer3;
			}
			set
			{
				this.m_Answer3 = value;
			}
		}

		/// <summary>
		/// 最后登录IP
		/// </summary>
		[DataMember]
		[DisplayName("最后登录IP")]
		public virtual String LastLoginIp
		{
			get
			{
				return this.m_LastLoginIp;
			}
			set
			{
				this.m_LastLoginIp = value;
			}
		}

		/// <summary>
		/// 最后登录Mac
		/// </summary>
		[DataMember]
		[DisplayName("最后登录Mac")]
		public virtual String LastLoginMac
		{
			get
			{
				return this.m_LastLoginMac;
			}
			set
			{
				this.m_LastLoginMac = value;
			}
		}

		/// <summary>
		/// 最后登录日期
		/// </summary>
		[DataMember]
		[DisplayName("最后登录日期")]
		public virtual DateTime LastLoginTime
		{
			get
			{
				return this.m_LastLoginTime;
			}
			set
			{
				this.m_LastLoginTime = value;
			}
		}

		/// <summary>
		/// 最后修改密码时间
		/// </summary>
		[DataMember]
		[DisplayName("最后修改密码时间")]
		public virtual DateTime LastChangePwdTime
		{
			get
			{
				return this.m_LastChangePwdTime;
			}
			set
			{
				this.m_LastChangePwdTime = value;
			}
		}

		/// <summary>
		/// 当前登录IP
		/// </summary>
		[DataMember]
		[DisplayName("当前登录IP")]
		public virtual String CurLoginIp
		{
			get
			{
				return this.m_CurLoginIp;
			}
			set
			{
				this.m_CurLoginIp = value;
			}
		}

		/// <summary>
		/// 当前登录Mac
		/// </summary>
		[DataMember]
		[DisplayName("当前登录Mac")]
		public virtual String CurLoginMac
		{
			get
			{
				return this.m_CurLoginMac;
			}
			set
			{
				this.m_CurLoginMac = value;
			}
		}

		/// <summary>
		/// 当前登录日期
		/// </summary>
		[DataMember]
		[DisplayName("当前登录日期")]
		public virtual DateTime CurLoginTime
		{
			get
			{
				return this.m_CurLoginTime;
			}
			set
			{
				this.m_CurLoginTime = value;
			}
		}
		#endregion
	}
}