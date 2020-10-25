using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 客户联系人(ContactInfo)
	/// 对象号: 100011
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class ContactInfo : BaseEntity
	{
		#region Field Members

		/// <summary>
		/// ID序号
		/// </summary>
		private Int32 m_Id = 0;

		/// <summary>
		/// 用户编码
		/// </summary>
		private String m_UserCode = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// 身份证
		/// </summary>
		private String m_IdCard = string.Empty;

		/// <summary>
		/// 生日
		/// </summary>
		private DateTime m_Birthday = DateTime.Now;

		/// <summary>
		/// 性别
		/// </summary>
		private Int16 m_Gender = 0;

		/// <summary>
		/// 办公电话
		/// </summary>
		private String m_OfficePhone = string.Empty;

		/// <summary>
		/// 家庭电话
		/// </summary>
		private String m_HomePhone = string.Empty;

		/// <summary>
		/// 传真号码
		/// </summary>
		private String m_Fax = string.Empty;

		/// <summary>
		/// 手机
		/// </summary>
		private String m_MobilePhone = string.Empty;

		/// <summary>
		/// 地址
		/// </summary>
		private String m_Address = string.Empty;

		/// <summary>
		/// 邮政编码
		/// </summary>
		private String m_ZipCode = string.Empty;

		/// <summary>
		/// Email邮箱
		/// </summary>
		private String m_Email = string.Empty;

		/// <summary>
		/// QQ号
		/// </summary>
		private Int32 m_QQ = 0;

		/// <summary>
		/// 备注
		/// </summary>
		private String m_Remark = string.Empty;

		/// <summary>
		/// 排序
		/// </summary>
		private String m_Seq = string.Empty;

		/// <summary>
		/// 省份名称
		/// </summary>
		private String m_ProvinceName = string.Empty;

		/// <summary>
		/// 城市名字
		/// </summary>
		private String m_CityName = string.Empty;

		/// <summary>
		/// 行政区划
		/// </summary>
		private String m_DistrictName = string.Empty;

		/// <summary>
		/// 籍贯
		/// </summary>
		private String m_NativePlace = string.Empty;

		/// <summary>
		/// 家庭地址
		/// </summary>
		private String m_HomeAddress = string.Empty;

		/// <summary>
		/// 民族
		/// </summary>
		private String m_Nation = string.Empty;

		/// <summary>
		/// 教育
		/// </summary>
		private String m_Education = string.Empty;

		/// <summary>
		/// 毕业学校
		/// </summary>
		private String m_GraduateSchool = string.Empty;

		/// <summary>
		/// 政治面貌
		/// </summary>
		private String m_Political = string.Empty;

		/// <summary>
		/// 职业类型 
		/// </summary>
		private String m_JobType = string.Empty;

		/// <summary>
		/// 职称
		/// </summary>
		private String m_ProfessionalTitle = string.Empty;

		/// <summary>
		/// 职务
		/// </summary>
		private String m_WorkPost = string.Empty;

		/// <summary>
		/// 部门名字
		/// </summary>
		private String m_DeptName = string.Empty;

		/// <summary>
		/// 个人爱好
		/// </summary>
		private String m_PersonalHobby = string.Empty;

		/// <summary>
		/// 生肖
		/// </summary>
		private Int16 m_ChineseZodiac = 0;

		/// <summary>
		/// 星座
		/// </summary>
		private Int16 m_Constellation = 0;

		/// <summary>
		/// 婚姻状态
		/// </summary>
		private Int16 m_MarriageStatus = 0;

		/// <summary>
		/// 健康状态
		/// </summary>
		private Int16 m_HealthStatus = 0;

		/// <summary>
		/// 重要级别
		/// </summary>
		private Int16 m_ImportanceLevel = 0;

		/// <summary>
		/// 认可程度级别
		/// </summary>
		private Int16 m_RecognitionLevel = 0;

		/// <summary>
		/// 关系
		/// </summary>
		private String m_RelationShip = string.Empty;

		/// <summary>
		/// 负责需求
		/// </summary>
		private String m_ResponseDemand = string.Empty;

		/// <summary>
		/// 关心重点
		/// </summary>
		private String m_CareFocus = string.Empty;

		/// <summary>
		/// 利益诉求 
		/// </summary>
		private String m_InterestDemand = string.Empty;

		/// <summary>
		/// 体型
		/// </summary>
		private Int16 m_BodyType = 0;

		/// <summary>
		/// 是否吸烟
		/// </summary>
		private Int16 m_IsSmoking = 0;

		/// <summary>
		/// 是否喝酒
		/// </summary>
		private Int16 m_IsDrink = 0;

		/// <summary>
		/// 身高
		/// </summary>
		private Int32 m_Height = 0;

		/// <summary>
		/// 体重
		/// </summary>
		private Int32 m_Weight = 0;

		/// <summary>
		/// 视力
		/// </summary>
		private String m_Vision = string.Empty;

		/// <summary>
		/// 个人简述
		/// </summary>
		private String m_Introduce = string.Empty;

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
		/// 部门Id
		/// </summary>
		private Int32 m_DeptId = 0;

		/// <summary>
		/// 公司Id
		/// </summary>
		private Int32 m_CompanyId = 0;
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
		/// 用户编码
		/// </summary>
		[DataMember]
		[DisplayName("用户编码")]
		public virtual String UserCode
		{
			get
			{
				return this.m_UserCode;
			}
			set
			{
				this.m_UserCode = value;
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
		/// 传真号码
		/// </summary>
		[DataMember]
		[DisplayName("传真号码")]
		public virtual String Fax
		{
			get
			{
				return this.m_Fax;
			}
			set
			{
				this.m_Fax = value;
			}
		}

		/// <summary>
		/// 手机
		/// </summary>
		[DataMember]
		[DisplayName("手机")]
		public virtual String MobilePhone
		{
			get
			{
				return this.m_MobilePhone;
			}
			set
			{
				this.m_MobilePhone = value;
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
		/// 邮政编码
		/// </summary>
		[DataMember]
		[DisplayName("邮政编码")]
		public virtual String ZipCode
		{
			get
			{
				return this.m_ZipCode;
			}
			set
			{
				this.m_ZipCode = value;
			}
		}

		/// <summary>
		/// Email邮箱
		/// </summary>
		[DataMember]
		[DisplayName("Email邮箱")]
		public virtual String Email
		{
			get
			{
				return this.m_Email;
			}
			set
			{
				this.m_Email = value;
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
		/// 省份名称
		/// </summary>
		[DataMember]
		[DisplayName("省份名称")]
		public virtual String ProvinceName
		{
			get
			{
				return this.m_ProvinceName;
			}
			set
			{
				this.m_ProvinceName = value;
			}
		}

		/// <summary>
		/// 城市名字
		/// </summary>
		[DataMember]
		[DisplayName("城市名字")]
		public virtual String CityName
		{
			get
			{
				return this.m_CityName;
			}
			set
			{
				this.m_CityName = value;
			}
		}

		/// <summary>
		/// 行政区划
		/// </summary>
		[DataMember]
		[DisplayName("行政区划")]
		public virtual String DistrictName
		{
			get
			{
				return this.m_DistrictName;
			}
			set
			{
				this.m_DistrictName = value;
			}
		}

		/// <summary>
		/// 籍贯
		/// </summary>
		[DataMember]
		[DisplayName("籍贯")]
		public virtual String NativePlace
		{
			get
			{
				return this.m_NativePlace;
			}
			set
			{
				this.m_NativePlace = value;
			}
		}

		/// <summary>
		/// 家庭地址
		/// </summary>
		[DataMember]
		[DisplayName("家庭地址")]
		public virtual String HomeAddress
		{
			get
			{
				return this.m_HomeAddress;
			}
			set
			{
				this.m_HomeAddress = value;
			}
		}

		/// <summary>
		/// 民族
		/// </summary>
		[DataMember]
		[DisplayName("民族")]
		public virtual String Nation
		{
			get
			{
				return this.m_Nation;
			}
			set
			{
				this.m_Nation = value;
			}
		}

		/// <summary>
		/// 教育
		/// </summary>
		[DataMember]
		[DisplayName("教育")]
		public virtual String Education
		{
			get
			{
				return this.m_Education;
			}
			set
			{
				this.m_Education = value;
			}
		}

		/// <summary>
		/// 毕业学校
		/// </summary>
		[DataMember]
		[DisplayName("毕业学校")]
		public virtual String GraduateSchool
		{
			get
			{
				return this.m_GraduateSchool;
			}
			set
			{
				this.m_GraduateSchool = value;
			}
		}

		/// <summary>
		/// 政治面貌
		/// </summary>
		[DataMember]
		[DisplayName("政治面貌")]
		public virtual String Political
		{
			get
			{
				return this.m_Political;
			}
			set
			{
				this.m_Political = value;
			}
		}

		/// <summary>
		/// 职业类型 
		/// </summary>
		[DataMember]
		[DisplayName("职业类型 ")]
		public virtual String JobType
		{
			get
			{
				return this.m_JobType;
			}
			set
			{
				this.m_JobType = value;
			}
		}

		/// <summary>
		/// 职称
		/// </summary>
		[DataMember]
		[DisplayName("职称")]
		public virtual String ProfessionalTitle
		{
			get
			{
				return this.m_ProfessionalTitle;
			}
			set
			{
				this.m_ProfessionalTitle = value;
			}
		}

		/// <summary>
		/// 职务
		/// </summary>
		[DataMember]
		[DisplayName("职务")]
		public virtual String WorkPost
		{
			get
			{
				return this.m_WorkPost;
			}
			set
			{
				this.m_WorkPost = value;
			}
		}

		/// <summary>
		/// 部门名字
		/// </summary>
		[DataMember]
		[DisplayName("部门名字")]
		public virtual String DeptName
		{
			get
			{
				return this.m_DeptName;
			}
			set
			{
				this.m_DeptName = value;
			}
		}

		/// <summary>
		/// 个人爱好
		/// </summary>
		[DataMember]
		[DisplayName("个人爱好")]
		public virtual String PersonalHobby
		{
			get
			{
				return this.m_PersonalHobby;
			}
			set
			{
				this.m_PersonalHobby = value;
			}
		}

		/// <summary>
		/// 生肖
		/// 1-鼠,
		/// 2-牛,
		/// 3-虎,
		/// 4-兔,
		/// 5-龙,
		/// 6-蛇,
		/// 7-马,
		/// 8-羊,
		/// 9-猴,
		/// 10-鸡,
		/// 11-狗,
		/// 12-猪
		/// </summary>
		[DataMember]
		[DisplayName("生肖")]
		public virtual Int16 ChineseZodiac
		{
			get
			{
				return this.m_ChineseZodiac;
			}
			set
			{
				this.m_ChineseZodiac = value;
			}
		}

		/// <summary>
		/// 星座
		/// 1-白羊座,
		/// 2-金牛座,
		/// 3-双子座,
		/// 4-巨蟹座,
		/// 5-狮子座,
		/// 6-处女座,
		/// 7-天秤座,
		/// 8-天蝎座,
		/// 9-射手座,
		/// 10-魔羯座,
		/// 11-水瓶座,
		/// 12-双鱼座
		/// </summary>
		[DataMember]
		[DisplayName("星座")]
		public virtual Int16 Constellation
		{
			get
			{
				return this.m_Constellation;
			}
			set
			{
				this.m_Constellation = value;
			}
		}

		/// <summary>
		/// 婚姻状态
		/// 1-未婚,
		/// 2-已婚,
		/// 3-丧偶,
		/// 4-离婚
		/// </summary>
		[DataMember]
		[DisplayName("婚姻状态")]
		public virtual Int16 MarriageStatus
		{
			get
			{
				return this.m_MarriageStatus;
			}
			set
			{
				this.m_MarriageStatus = value;
			}
		}

		/// <summary>
		/// 健康状态
		/// 1-健康,
		/// 2-亚健康,
		/// 3-疾病
		/// </summary>
		[DataMember]
		[DisplayName("健康状态")]
		public virtual Int16 HealthStatus
		{
			get
			{
				return this.m_HealthStatus;
			}
			set
			{
				this.m_HealthStatus = value;
			}
		}

		/// <summary>
		/// 重要级别
		/// 1-重要紧急,
		/// 2-重要不紧急,
		/// 3-不重要紧急,
		/// 4-不重要不紧急
		/// </summary>
		[DataMember]
		[DisplayName("重要级别")]
		public virtual Int16 ImportanceLevel
		{
			get
			{
				return this.m_ImportanceLevel;
			}
			set
			{
				this.m_ImportanceLevel = value;
			}
		}

		/// <summary>
		/// 认可程度级别
		/// 1-极为认可,
		/// 2-较认可,
		/// 3-一般,
		/// 4-较不认可,
		/// 5-非常不认可
		/// </summary>
		[DataMember]
		[DisplayName("认可程度级别")]
		public virtual Int16 RecognitionLevel
		{
			get
			{
				return this.m_RecognitionLevel;
			}
			set
			{
				this.m_RecognitionLevel = value;
			}
		}

		/// <summary>
		/// 关系
		/// </summary>
		[DataMember]
		[DisplayName("关系")]
		public virtual String RelationShip
		{
			get
			{
				return this.m_RelationShip;
			}
			set
			{
				this.m_RelationShip = value;
			}
		}

		/// <summary>
		/// 负责需求
		/// </summary>
		[DataMember]
		[DisplayName("负责需求")]
		public virtual String ResponseDemand
		{
			get
			{
				return this.m_ResponseDemand;
			}
			set
			{
				this.m_ResponseDemand = value;
			}
		}

		/// <summary>
		/// 关心重点
		/// </summary>
		[DataMember]
		[DisplayName("关心重点")]
		public virtual String CareFocus
		{
			get
			{
				return this.m_CareFocus;
			}
			set
			{
				this.m_CareFocus = value;
			}
		}

		/// <summary>
		/// 利益诉求 
		/// </summary>
		[DataMember]
		[DisplayName("利益诉求 ")]
		public virtual String InterestDemand
		{
			get
			{
				return this.m_InterestDemand;
			}
			set
			{
				this.m_InterestDemand = value;
			}
		}

		/// <summary>
		/// 体型
		/// 1-体型纤细,
		/// 2-不胖不瘦,
		/// 3-体型丰满,
		/// 4-体型超重
		/// </summary>
		[DataMember]
		[DisplayName("体型")]
		public virtual Int16 BodyType
		{
			get
			{
				return this.m_BodyType;
			}
			set
			{
				this.m_BodyType = value;
			}
		}

		/// <summary>
		/// 是否吸烟
		/// 1-是,
		/// 2-否
		/// </summary>
		[DataMember]
		[DisplayName("是否吸烟")]
		public virtual Int16 IsSmoking
		{
			get
			{
				return this.m_IsSmoking;
			}
			set
			{
				this.m_IsSmoking = value;
			}
		}

		/// <summary>
		/// 是否喝酒
		/// 1-是,
		/// 2-否
		/// </summary>
		[DataMember]
		[DisplayName("是否喝酒")]
		public virtual Int16 IsDrink
		{
			get
			{
				return this.m_IsDrink;
			}
			set
			{
				this.m_IsDrink = value;
			}
		}

		/// <summary>
		/// 身高
		/// </summary>
		[DataMember]
		[DisplayName("身高")]
		public virtual Int32 Height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		/// <summary>
		/// 体重
		/// </summary>
		[DataMember]
		[DisplayName("体重")]
		public virtual Int32 Weight
		{
			get
			{
				return this.m_Weight;
			}
			set
			{
				this.m_Weight = value;
			}
		}

		/// <summary>
		/// 视力
		/// </summary>
		[DataMember]
		[DisplayName("视力")]
		public virtual String Vision
		{
			get
			{
				return this.m_Vision;
			}
			set
			{
				this.m_Vision = value;
			}
		}

		/// <summary>
		/// 个人简述
		/// </summary>
		[DataMember]
		[DisplayName("个人简述")]
		public virtual String Introduce
		{
			get
			{
				return this.m_Introduce;
			}
			set
			{
				this.m_Introduce = value;
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
		#endregion
	}
}