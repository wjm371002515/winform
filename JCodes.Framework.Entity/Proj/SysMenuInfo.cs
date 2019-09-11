using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections.Generic;
using DevExpress.XtraEditors.DXErrorProvider;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 功能菜单
    /// </summary>
    [Serializable]
    [DataContract]
    public class SysMenuInfo : IDXDataErrorInfo
    {    
        #region Field Members

        private string m_Gid = System.Guid.NewGuid().ToString(); //          
        private string m_Pgid = "-1"; //父ID          
        private string m_Name; //显示名称          
        private string m_Icon; //图标          
        private string m_Seq; //排序          
        private string m_AuthGid; //功能ID          
        private bool m_IsVisable = true; //是否可见          
        private string m_WinformClass; //Winform窗体类型          
        private string m_Url; //Web界面Url地址          
        private string m_WebIcon; //Web界面的菜单图标          
        private string m_SystemtypeId; //系统编号                  
        private Int32 m_CreatorId; //创建人ID          
        private DateTime m_CreateTime = System.DateTime.Now; //创建时间                   
        private Int32 m_EditorId; //编辑人ID          
        private DateTime m_LastUpdateTime = System.DateTime.Now; //编辑时间          
        private bool m_IsDelete = false; //是否已删除     

        #endregion

        #region Property Members

        [DisplayName("菜单ID")]
		[DataMember]
        public virtual string Gid
        {
            get
            {
                return this.m_Gid;
            }
            set
            {
                this.m_Gid = value;
            }
        }

        /// <summary>
        /// 父ID
        /// </summary>
        [DisplayName("菜单父ID")]
		[DataMember]
        public virtual string Pgid
        {
            get
            {
                return this.m_Pgid;
            }
            set
            {
                this.m_Pgid = value;
            }
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        [DisplayName("显示名称")]
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
        /// 图标
        /// </summary>
        [DisplayName("图标")]
		[DataMember]
        public virtual string Icon
        {
            get
            {
                return this.m_Icon;
            }
            set
            {
                this.m_Icon = value;
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
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
        /// 功能ID
        /// </summary>
        [DisplayName("功能ID")]
		[DataMember]
        public virtual string AuthGid
        {
            get
            {
                return this.m_AuthGid;
            }
            set
            {
                this.m_AuthGid = value;
            }
        }

        /// <summary>
        /// 是否可见
        /// </summary>
        [DisplayName("可见")]
		[DataMember]
        public virtual bool IsVisable
        {
            get
            {
                return this.m_IsVisable;
            }
            set
            {
                this.m_IsVisable = value;
            }
        }

        /// <summary>
        /// Winform窗体类型
        /// </summary>
        [DisplayName("Winform窗体类型")]
		[DataMember]
        public virtual string WinformClass
        {
            get
            {
                return this.m_WinformClass;
            }
            set
            {
                this.m_WinformClass = value;
            }
        }

        /// <summary>
        /// Web界面Url地址
        /// </summary>
        [DisplayName("Web界面Url地址")]
		[DataMember]
        public virtual string Url
        {
            get
            {
                return this.m_Url;
            }
            set
            {
                this.m_Url = value;
            }
        }

        /// <summary>
        /// Web界面的菜单图标
        /// </summary>
        [DisplayName("Web界面的菜单图标")]
        [DataMember]
        public virtual string WebIcon
        {
            get
            {
                return this.m_WebIcon;
            }
            set
            {
                this.m_WebIcon = value;
            }
        }

        /// <summary>
        /// 系统编号
        /// </summary>
        [DisplayName("系统编号")]
        [DataMember]
        public virtual string SystemtypeId
        {
            get
            {
                return this.m_SystemtypeId;
            }
            set
            {
                this.m_SystemtypeId = value;
            }
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [DisplayName("创建人ID")]
		[DataMember]
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
        [DisplayName("创建时间")]
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
        /// 编辑人ID
        /// </summary>
        [DisplayName("编辑人ID")]
		[DataMember]
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
        /// 编辑时间
        /// </summary>
        [DisplayName("编辑时间")]
		[DataMember]
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
        /// 是否已删除
        /// </summary>
        [DisplayName("已删除")]
		[DataMember]
        public virtual bool IsDelete
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
        /// 用来保存行数据中字段名，错误信息
        /// </summary>
        public Dictionary<string, ErrorInfo> lstInfo
        {
            get;
            set;
        }

        #region IDXDataErrorInfo Members
        //<gridControl1>
        void IDXDataErrorInfo.GetPropertyError(string propertyName, ErrorInfo info)
        {
            // 添加自定义错误
            if (lstInfo != null && lstInfo.Count > 0 && lstInfo.ContainsKey(propertyName) && !string.IsNullOrEmpty(lstInfo[propertyName].ErrorText))
            {
                info.ErrorText = lstInfo[propertyName].ErrorText;
                info.ErrorType = lstInfo[propertyName].ErrorType;
            }
        }
        void IDXDataErrorInfo.GetError(ErrorInfo info) { }
        //</gridControl1>

        #endregion

        #endregion

    }
}