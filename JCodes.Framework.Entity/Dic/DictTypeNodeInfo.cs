using System;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    [Serializable]
    [DataContract]
    public class DictTypeNodeInfo : DictTypeInfo
    {
        private List<DictTypeNodeInfo> m_Children = new List<DictTypeNodeInfo>();

		/// <summary>
		/// 子菜单实体类对象集合
		/// </summary>
        [DataMember]
        public List<DictTypeNodeInfo> Children
		{
			get { return m_Children; }
			set { m_Children = value; }
		}

		public DictTypeNodeInfo()
		{
            this.m_Children = new List<DictTypeNodeInfo>();
		}

        public DictTypeNodeInfo(DictTypeInfo typeInfo)
		{
			base.Id = typeInfo.Id;
            base.Name = typeInfo.Id+"_"+typeInfo.Name;
			base.Remark = typeInfo.Remark;
			base.Seq = typeInfo.Seq;
			base.Pid = typeInfo.Pid;
			base.EditorId = typeInfo.EditorId;
            base.LastUpdated = typeInfo.LastUpdated;
		}
    }
}