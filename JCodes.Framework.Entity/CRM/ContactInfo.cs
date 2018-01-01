using System;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 客户联系人
    /// </summary>
    [DataContract]
    public class ContactInfo : BaseEntity
    {    
        #region Field Members

        private string m_ID = System.Guid.NewGuid().ToString(); //          
        private string m_HandNo; //编号          
        private string m_Name; //姓名          
        private string m_IDCarNo; //身份证号码          
        private DateTime m_Birthday; //出生日期          
        private string m_Sex; //性别          
        private string m_OfficePhone; //办公电话          
        private string m_HomePhone; //家庭电话          
        private string m_Fax; //传真          
        private string m_Mobile; //联系人手机          
        private string m_Address; //联系人地址          
        private string m_ZipCode; //邮政编码          
        private string m_Email; //电子邮件          
        private string m_QQ; //QQ号码          
        private string m_Note; //备注          
        private string m_Seq; //排序序号          
        private string m_AttachGUID = System.Guid.NewGuid().ToString(); //附件组别ID          
        private string m_Province; //所在省份          
        private string m_City; //城市          
        private string m_District; //所在行政区          
        private string m_Hometown; //籍贯          
        private string m_HomeAddress; //家庭住址          
        private string m_Nationality; //民族          
        private string m_Eduction; //教育程度          
        private string m_GraduateSchool; //毕业学校          
        private string m_Political; //政治面貌          
        private string m_JobType; //职业类型          
        private string m_Titles; //职称          
        private string m_Rank; //职务          
        private string m_Department; //所在部门          
        private string m_Hobby; //爱好          
        private string m_Animal; //属相          
        private string m_Constellation; //星座          
        private string m_MarriageStatus; //婚姻状态          
        private string m_HealthCondition; //健康状况          
        private string m_Importance; //重要级别        
        private string m_Recognition; //认可程度          
        private string m_RelationShip; //关系          
        private string m_ResponseDemand; //负责需求          
        private string m_CareFocus; //关心重点          
        private string m_InterestDemand; //利益诉求          
        private string m_BodyType; //体型          
        private string m_Smoking; //吸烟          
        private string m_Drink; //喝酒          
        private string m_Height; //身高          
        private string m_Weight; //体重          
        private string m_Vision; //视力          
        private string m_Introduce; //个人简述          
        private string m_Creator; //创建人          
        private DateTime m_CreateTime; //创建时间          
        private string m_Editor; //编辑人          
        private DateTime m_EditTime; //编辑时间          
        private bool m_Deleted = false; //是否已删除          
        private string m_Dept_ID; //所属部门
        private string m_Company_ID; //所属公司
        #endregion

        #region Property Members
        
		[DataMember]
        public virtual string ID
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
        /// 编号
        /// </summary>
		[DataMember]
        public virtual string HandNo
        {
            get
            {
                return this.m_HandNo;
            }
            set
            {
                this.m_HandNo = value;
            }
        }

        /// <summary>
        /// 姓名
        /// </summary>
		[DataMember]
        public virtual string Name
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
        /// 身份证号码
        /// </summary>
		[DataMember]
        public virtual string IDCarNo
        {
            get
            {
                return this.m_IDCarNo;
            }
            set
            {
                this.m_IDCarNo = value;
            }
        }

        /// <summary>
        /// 出生日期
        /// </summary>
		[DataMember]
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
        /// </summary>
		[DataMember]
        public virtual string Sex
        {
            get
            {
                return this.m_Sex;
            }
            set
            {
                this.m_Sex = value;
            }
        }

        /// <summary>
        /// 办公电话
        /// </summary>
		[DataMember]
        public virtual string OfficePhone
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
        public virtual string HomePhone
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
        /// 传真
        /// </summary>
		[DataMember]
        public virtual string Fax
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
        /// 联系人手机
        /// </summary>
		[DataMember]
        public virtual string Mobile
        {
            get
            {
                return this.m_Mobile;
            }
            set
            {
                this.m_Mobile = value;
            }
        }

        /// <summary>
        /// 联系人地址
        /// </summary>
		[DataMember]
        public virtual string Address
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
        public virtual string ZipCode
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
        /// 电子邮件
        /// </summary>
		[DataMember]
        public virtual string Email
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
        /// QQ号码
        /// </summary>
		[DataMember]
        public virtual string QQ
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
        public virtual string Note
        {
            get
            {
                return this.m_Note;
            }
            set
            {
                this.m_Note = value;
            }
        }

        /// <summary>
        /// 排序序号
        /// </summary>
		[DataMember]
        public virtual string Seq
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
        /// 附件组别ID
        /// </summary>
		[DataMember]
        public virtual string AttachGUID
        {
            get
            {
                return this.m_AttachGUID;
            }
            set
            {
                this.m_AttachGUID = value;
            }
        }

        /// <summary>
        /// 所在省份
        /// </summary>
		[DataMember]
        public virtual string Province
        {
            get
            {
                return this.m_Province;
            }
            set
            {
                this.m_Province = value;
            }
        }

        /// <summary>
        /// 城市
        /// </summary>
		[DataMember]
        public virtual string City
        {
            get
            {
                return this.m_City;
            }
            set
            {
                this.m_City = value;
            }
        }

        /// <summary>
        /// 所在行政区
        /// </summary>
		[DataMember]
        public virtual string District
        {
            get
            {
                return this.m_District;
            }
            set
            {
                this.m_District = value;
            }
        }

        /// <summary>
        /// 籍贯
        /// </summary>
		[DataMember]
        public virtual string Hometown
        {
            get
            {
                return this.m_Hometown;
            }
            set
            {
                this.m_Hometown = value;
            }
        }

        /// <summary>
        /// 家庭住址
        /// </summary>
		[DataMember]
        public virtual string HomeAddress
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
        public virtual string Nationality
        {
            get
            {
                return this.m_Nationality;
            }
            set
            {
                this.m_Nationality = value;
            }
        }

        /// <summary>
        /// 教育程度
        /// </summary>
		[DataMember]
        public virtual string Eduction
        {
            get
            {
                return this.m_Eduction;
            }
            set
            {
                this.m_Eduction = value;
            }
        }

        /// <summary>
        /// 毕业学校
        /// </summary>
		[DataMember]
        public virtual string GraduateSchool
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
        public virtual string Political
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
        public virtual string JobType
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
        public virtual string Titles
        {
            get
            {
                return this.m_Titles;
            }
            set
            {
                this.m_Titles = value;
            }
        }

        /// <summary>
        /// 职务
        /// </summary>
		[DataMember]
        public virtual string Rank
        {
            get
            {
                return this.m_Rank;
            }
            set
            {
                this.m_Rank = value;
            }
        }

        /// <summary>
        /// 所在部门
        /// </summary>
		[DataMember]
        public virtual string Department
        {
            get
            {
                return this.m_Department;
            }
            set
            {
                this.m_Department = value;
            }
        }

        /// <summary>
        /// 爱好
        /// </summary>
		[DataMember]
        public virtual string Hobby
        {
            get
            {
                return this.m_Hobby;
            }
            set
            {
                this.m_Hobby = value;
            }
        }

        /// <summary>
        /// 属相
        /// </summary>
		[DataMember]
        public virtual string Animal
        {
            get
            {
                return this.m_Animal;
            }
            set
            {
                this.m_Animal = value;
            }
        }

        /// <summary>
        /// 星座
        /// </summary>
		[DataMember]
        public virtual string Constellation
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
        /// </summary>
		[DataMember]
        public virtual string MarriageStatus
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
        /// 健康状况
        /// </summary>
		[DataMember]
        public virtual string HealthCondition
        {
            get
            {
                return this.m_HealthCondition;
            }
            set
            {
                this.m_HealthCondition = value;
            }
        }

        /// <summary>
        /// 重要级别
        /// </summary>
		[DataMember]
        public virtual string Importance
        {
            get
            {
                return this.m_Importance;
            }
            set
            {
                this.m_Importance = value;
            }
        }

        /// <summary>
        /// 认可程度
        /// </summary>
		[DataMember]
        public virtual string Recognition
        {
            get
            {
                return this.m_Recognition;
            }
            set
            {
                this.m_Recognition = value;
            }
        }

        /// <summary>
        /// 关系
        /// </summary>
		[DataMember]
        public virtual string RelationShip
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
        public virtual string ResponseDemand
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
        public virtual string CareFocus
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
        public virtual string InterestDemand
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
        /// </summary>
		[DataMember]
        public virtual string BodyType
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
        /// 吸烟
        /// </summary>
		[DataMember]
        public virtual string Smoking
        {
            get
            {
                return this.m_Smoking;
            }
            set
            {
                this.m_Smoking = value;
            }
        }

        /// <summary>
        /// 喝酒
        /// </summary>
		[DataMember]
        public virtual string Drink
        {
            get
            {
                return this.m_Drink;
            }
            set
            {
                this.m_Drink = value;
            }
        }

        /// <summary>
        /// 身高
        /// </summary>
		[DataMember]
        public virtual string Height
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
        public virtual string Weight
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
        public virtual string Vision
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
        public virtual string Introduce
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
        /// 创建人
        /// </summary>
		[DataMember]
        public virtual string Creator
        {
            get
            {
                return this.m_Creator;
            }
            set
            {
                this.m_Creator = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
		[DataMember]
        public virtual DateTime CreateTime
        {
            get
            {
                return this.m_CreateTime;
            }
            set
            {
                this.m_CreateTime = value;
            }
        }

        /// <summary>
        /// 编辑人
        /// </summary>
		[DataMember]
        public virtual string Editor
        {
            get
            {
                return this.m_Editor;
            }
            set
            {
                this.m_Editor = value;
            }
        }

        /// <summary>
        /// 编辑时间
        /// </summary>
		[DataMember]
        public virtual DateTime EditTime
        {
            get
            {
                return this.m_EditTime;
            }
            set
            {
                this.m_EditTime = value;
            }
        }

        /// <summary>
        /// 是否已删除
        /// </summary>
		[DataMember]
        public virtual bool Deleted
        {
            get
            {
                return this.m_Deleted;
            }
            set
            {
                this.m_Deleted = value;
            }
        }

        /// <summary>
        /// 所属部门
        /// </summary>
        [DataMember]
        public virtual string Dept_ID
        {
            get
            {
                return this.m_Dept_ID;
            }
            set
            {
                this.m_Dept_ID = value;
            }
        }

        /// <summary>
        /// 所属公司
        /// </summary>
        [DataMember]
        public virtual string Company_ID
        {
            get
            {
                return this.m_Company_ID;
            }
            set
            {
                this.m_Company_ID = value;
            }
        }

        #endregion

    }
}