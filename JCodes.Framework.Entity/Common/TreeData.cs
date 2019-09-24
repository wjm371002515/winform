using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 定义zTree的相关数据，方便控制器生成Json数据进行传递(TreeData)
	/// 对象号: 100030
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class TreeData
	{
		#region Field Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		private String m_Gid = string.Empty;

		/// <summary>
		/// 父节点GUID对应的ID序号
		/// </summary>
		private String m_Pgid = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// icon图标路径
		/// </summary>
		private String m_Icon = string.Empty;

		/// <summary>
		/// 是否打开
		/// </summary>
		private Int16 m_IsOpened = 0;

		/// <summary>
		/// 子节点集合
		/// </summary>
		private List<TreeData> m_children = new List<TreeData>();
		#endregion

		#region Property Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		[DataMember]
		[DisplayName("GUID对应的ID序号")]
		public virtual String Gid
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
		/// 父节点GUID对应的ID序号
		/// </summary>
		[DataMember]
		[DisplayName("父节点GUID对应的ID序号")]
		public virtual String Pgid
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
		/// icon图标路径
		/// </summary>
		[DataMember]
		[DisplayName("icon图标路径")]
		public virtual String Icon
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
		/// 是否打开
		/// </summary>
		[DataMember]
		[DisplayName("是否打开")]
		public virtual Int16 IsOpened
		{
			get
			{
				return this.m_IsOpened;
			}
			set
			{
				this.m_IsOpened = value;
			}
		}

		/// <summary>
		/// 子节点集合
		/// </summary>
		[DataMember]
		public virtual List<TreeData> children
		{
			get
			{
				return this.m_children;
			}
			set
			{
				this.m_children = value;
			}
		}

		/// <summary>
		/// 无参构造函数
		/// </summary>
		public  TreeData()
		{
			this.children = new List<TreeData>();
            this.IsOpened = 0;
		}

		/// <summary>
		/// 带参构造函数
		/// </summary>
		public  TreeData(string id, string pid, string name, string icon = "") : this()
		{
			this.m_Gid = id;
            this.m_Pgid = pid;
            this.Name = name;
            this.Icon = icon;
		}

		/// <summary>
		/// 带参构造函数
		/// </summary>
		public  TreeData(int id, int pid, string name, string icon = "") : this()
		{
			this.m_Gid = id.ToString();
            this.m_Pgid = pid.ToString();
            this.Name = name;
            this.Icon = icon;
		}
		#endregion
	}
}