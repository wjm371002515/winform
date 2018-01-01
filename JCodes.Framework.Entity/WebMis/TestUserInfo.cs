using System;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 为测试用的数据表
    /// </summary>
    [DataContract]
    public class TestUserInfo : BaseEntity
    { 
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
        public TestUserInfo()
        {
            this.ID = System.Guid.NewGuid().ToString();
            this.Age = 0;
            this.Height = 0;

        }

        #region Property Members
        
		[DataMember]
        public virtual string ID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
		[DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [DataMember]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        public virtual string Email { get; set; }

        /// <summary>
        /// 主页
        /// </summary>
        [DataMember]
        public virtual string Homepage { get; set; }

        /// <summary>
        /// 兴趣爱好
        /// </summary>
		[DataMember]
        public virtual string Hobby { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
		[DataMember]
        public virtual string Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
		[DataMember]
        public virtual int Age { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
		[DataMember]
        public virtual DateTime BirthDate { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
		[DataMember]
        public virtual decimal Height { get; set; }

        /// <summary>
        /// 肖像
        /// </summary>
		[DataMember]
        public virtual byte[] Portrait { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
		[DataMember]
        public virtual string Note { get; set; }

        /// <summary>
        /// 编辑人
        /// </summary>
		[DataMember]
        public virtual string Editor { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
		[DataMember]
        public virtual DateTime EditTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
		[DataMember]
        public virtual string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
		[DataMember]
        public virtual DateTime CreateTime { get; set; }


        #endregion

    }
}