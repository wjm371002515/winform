using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 用于各个模块传递的联系信息
    /// </summary>
    [DataContract]
    public class NotifyContactInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public string ID { get; set; }

        /// <summary>
        /// 联系名称
        /// </summary>
        [DataMember]
        public string ContactName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [DataMember]
        public string Mobile { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [DataMember]
        public string Note { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public NotifyContactInfo() { }

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        public NotifyContactInfo(string id, string contactName, string mobile, string email = "", string note = "")
        {
            this.ID = id;
            this.ContactName = contactName;
            this.Mobile = mobile;
            this.Email = email;
            this.Note = note;
        }
    }
    
    /// <summary>
    /// 用于各个模块传递的分组联系人信息
    /// </summary>
    [DataContract]
    public class NotifyGroupInfo
    {
        /// <summary>
        /// 通讯录分类
        /// </summary>
        [DataMember]
        public string Category { get; set; }

        /// <summary>
        /// 联系人列表
        /// </summary>
        [DataMember]
        public List<NotifyContactInfo> ContactList { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public NotifyGroupInfo()
        {
            this.ContactList = new List<NotifyContactInfo>();
        }
    }
    
    /// <summary>
    /// 用于各个模块传递的分组联系人信息节点
    /// </summary>
    [DataContract]
    public class NotifyNodeInfo : NotifyGroupInfo
    {
        private List<NotifyNodeInfo> m_Children = new List<NotifyNodeInfo>();

        /// <summary>
        /// 子分组实体类对象集合
        /// </summary>
        [DataMember]
        public List<NotifyNodeInfo> Children
        {
            get { return m_Children; }
            set { m_Children = value; }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public NotifyNodeInfo()
        {
            this.m_Children = new List<NotifyNodeInfo>();
        }

        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="info">NotifyGroupInfo对象</param>
        public NotifyNodeInfo(NotifyGroupInfo info)
        {
            base.Category = info.Category;
            base.ContactList = info.ContactList;
        }
    }
}
