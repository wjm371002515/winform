using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 用户对指定内容的操作状态记录
    /// </summary>
    [DataContract]
    public class InformationStatusInfo : BaseEntity
    {    
        #region Field Members

        private string m_ID = System.Guid.NewGuid().ToString(); //          
        private string m_Category; //信息类型          
        private string m_Information_ID; //信息ID          
        private int m_Status = 0; //阅读状态（0：未读，1：已读，其他待定）          
        private string m_User_ID; //用户ID          

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
        /// 信息类型
        /// </summary>
		[DataMember]
        public virtual string Category
        {
            get
            {
                return this.m_Category;
            }
            set
            {
                this.m_Category = value;
            }
        }

        /// <summary>
        /// 信息ID
        /// </summary>
		[DataMember]
        public virtual string Information_ID
        {
            get
            {
                return this.m_Information_ID;
            }
            set
            {
                this.m_Information_ID = value;
            }
        }

        /// <summary>
        /// 阅读状态（0：未读，1：已读，其他待定）
        /// </summary>
		[DataMember]
        public virtual int Status
        {
            get
            {
                return this.m_Status;
            }
            set
            {
                this.m_Status = value;
            }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
		[DataMember]
        public virtual string User_ID
        {
            get
            {
                return this.m_User_ID;
            }
            set
            {
                this.m_User_ID = value;
            }
        }


        #endregion

    }
}