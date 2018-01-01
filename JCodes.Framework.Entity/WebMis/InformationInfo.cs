using System;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    public enum InformationCategory { 通知公告, 政策法规, 行业动态, 图片新闻, 其他 };

    /// <summary>
    /// 政策法规公告动态
    /// </summary>
    [DataContract]
    public class InformationInfo : BaseEntity
    {    
        #region Field Members

        private string m_ID = System.Guid.NewGuid().ToString(); //          
        private string m_Title; //标题          
        private string m_Content; //内容          
        private string m_Attachment_GUID = System.Guid.NewGuid().ToString(); //附件GUID          
        private InformationCategory m_Category = InformationCategory.其他; //大类名称          
        private string m_SubType; //子类名称          
        private string m_Editor; //编辑者          
        private DateTime m_EditTime = DateTime.Now; //编辑时间          
        private int m_IsChecked = 0; //是否审批通过          
        private string m_CheckUser; //审批者          
        private DateTime m_CheckTime; //审批时间          
        private int m_ForceExpire = 0; //是否强制过期          
        private DateTime m_TimeOut; //过期截止时间          

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
        /// 标题
        /// </summary>
		[DataMember]
        public virtual string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
		[DataMember]
        public virtual string Content
        {
            get
            {
                return this.m_Content;
            }
            set
            {
                this.m_Content = value;
            }
        }

        /// <summary>
        /// 附件GUID
        /// </summary>
		[DataMember]
        public virtual string Attachment_GUID
        {
            get
            {
                return this.m_Attachment_GUID;
            }
            set
            {
                this.m_Attachment_GUID = value;
            }
        }

        /// <summary>
        /// 大类名称
        /// </summary>
		[DataMember]
        public virtual InformationCategory Category
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
        /// 子类名称
        /// </summary>
		[DataMember]
        public virtual string SubType
        {
            get
            {
                return this.m_SubType;
            }
            set
            {
                this.m_SubType = value;
            }
        }

        /// <summary>
        /// 编辑者
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
        /// 是否审批通过
        /// </summary>
		[DataMember]
        public virtual int IsChecked
        {
            get
            {
                return this.m_IsChecked;
            }
            set
            {
                this.m_IsChecked = value;
            }
        }

        /// <summary>
        /// 审批者
        /// </summary>
		[DataMember]
        public virtual string CheckUser
        {
            get
            {
                return this.m_CheckUser;
            }
            set
            {
                this.m_CheckUser = value;
            }
        }

        /// <summary>
        /// 审批时间
        /// </summary>
		[DataMember]
        public virtual DateTime CheckTime
        {
            get
            {
                return this.m_CheckTime;
            }
            set
            {
                this.m_CheckTime = value;
            }
        }

        /// <summary>
        /// 是否强制过期
        /// </summary>
		[DataMember]
        public virtual int ForceExpire
        {
            get
            {
                return this.m_ForceExpire;
            }
            set
            {
                this.m_ForceExpire = value;
            }
        }

        /// <summary>
        /// 过期截止时间
        /// </summary>
		[DataMember]
        public virtual DateTime TimeOut
        {
            get
            {
                return this.m_TimeOut;
            }
            set
            {
                this.m_TimeOut = value;
            }
        }


        #endregion

    }
}