using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 字典节点类型信息(DictTypeNodeInfo)
	/// 对象号: 100018
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class DictTypeNodeInfo : DictTypeInfo
	{
		#region Field Members

		/// <summary>
		/// 子菜单实体类对象集合
		/// </summary>
		private List<DictTypeNodeInfo> m_Children = new List<DictTypeNodeInfo>();
		#endregion

		#region Property Members

		/// <summary>
		/// 子菜单实体类对象集合
		/// </summary>
		[DataMember]
		public virtual List<DictTypeNodeInfo> Children
		{
			get
			{
				return this.m_Children;
			}
			set
			{
				this.m_Children = value;
			}
		}

		/// <summary>
		/// 无参构造函数
		/// </summary>
		public  DictTypeNodeInfo()
		{
			this.m_Children = new List<DictTypeNodeInfo>();
		}

		/// <summary>
		/// 带参构造函数
		/// </summary>
		public  DictTypeNodeInfo(DictTypeInfo typeInfo)
		{
			base.Id = typeInfo.Id;
            base.Name = typeInfo.Id+"_"+typeInfo.Name;
			base.Remark = typeInfo.Remark;
			base.Seq = typeInfo.Seq;
			base.Pid = typeInfo.Pid;
			base.EditorId = typeInfo.EditorId;
            base.LastUpdateTime = typeInfo.LastUpdateTime;
		}
		#endregion
	}
}