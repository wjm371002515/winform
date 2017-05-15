using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    [DataContract]
    [Serializable]
    public class FileUploadInfo : BaseEntity
    {    
        #region Field Members

        private string m_ID = System.Guid.NewGuid().ToString();
        private string m_Owner_ID; //附件组所属记录ID        
        private string m_AttachmentGUID; //附件GUID          
        private string m_FileName; //文件名          
        private string m_BasePath; //基础路径          
        private string m_SavePath; //文件保存相对路径          
        private string m_Category; //文件分类          
        private int m_FileSize = 0; //文件大小          
        private string m_FileExtend; //文件扩展名          
        private string m_Editor; //所属用户          
        private DateTime m_AddTime = System.DateTime.Now; //添加时间          
        private int m_DeleteFlag = 0; //删除标志，1为删除，0为正常 
        private byte[] m_FileData; //文件流

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
        /// 拥有者ID  
        /// </summary>
        [DataMember]
        public string Owner_ID
        {
            get { return m_Owner_ID; }
            set { m_Owner_ID = value; }
        }

        /// <summary>
        /// 附件GUID
        /// </summary>
		[DataMember]
        public virtual string AttachmentGUID
        {
            get
            {
                return this.m_AttachmentGUID;
            }
            set
            {
                this.m_AttachmentGUID = value;
            }
        }

        /// <summary>
        /// 文件名
        /// </summary>
		[DataMember]
        public virtual string FileName
        {
            get
            {
                return this.m_FileName;
            }
            set
            {
                this.m_FileName = value;
            }
        }

        /// <summary>
        /// 基础路径
        /// </summary>
		[DataMember]
        public virtual string BasePath
        {
            get
            {
                return this.m_BasePath;
            }
            set
            {
                this.m_BasePath = value;
            }
        }

        /// <summary>
        /// 文件保存相对路径
        /// </summary>
		[DataMember]
        public virtual string SavePath
        {
            get
            {
                return this.m_SavePath;
            }
            set
            {
                this.m_SavePath = value;
            }
        }

        /// <summary>
        /// 文件分类
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
        /// 文件大小
        /// </summary>
		[DataMember]
        public virtual int FileSize
        {
            get
            {
                return this.m_FileSize;
            }
            set
            {
                this.m_FileSize = value;
            }
        }

        /// <summary>
        /// 文件扩展名
        /// </summary>
		[DataMember]
        public virtual string FileExtend
        {
            get
            {
                return this.m_FileExtend;
            }
            set
            {
                this.m_FileExtend = value;
            }
        }

        /// <summary>
        /// 所属用户
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
        /// 添加时间
        /// </summary>
		[DataMember]
        public virtual DateTime AddTime
        {
            get
            {
                return this.m_AddTime;
            }
            set
            {
                this.m_AddTime = value;
            }
        }

        /// <summary>
        /// 删除标志，1为删除，0为正常
        /// </summary>
		[DataMember]
        public virtual int DeleteFlag
        {
            get
            {
                return this.m_DeleteFlag;
            }
            set
            {
                this.m_DeleteFlag = value;
            }
        }

        /// <summary>
        /// 文件流数据
        /// </summary>
        [DataMember]
        public byte[] FileData
        {
            get
            {
                return this.m_FileData;
            }
            set
            {
                this.m_FileData = value;
            }
        }

        #endregion

    }
}