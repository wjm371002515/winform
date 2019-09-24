using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// JsTree的数据模型(JsTreeData)
	/// 对象号: 100023
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class JsTreeData
	{
		#region Field Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		private String m_Gid = string.Empty;

		/// <summary>
		/// 文本信息
		/// </summary>
		private String m_Text = string.Empty;

		/// <summary>
		/// icon图标路径
		/// </summary>
		private String m_Icon = string.Empty;

		/// <summary>
		/// 状态对象
		/// </summary>
		private JsTreeStatus m_JsTreeStatus;

		/// <summary>
		/// 子节点集合
		/// </summary>
		private List<JsTreeData> m_children = new List<JsTreeData>();
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
		/// 文本信息
		/// </summary>
		[DataMember]
		[DisplayName("文本信息")]
		public virtual String Text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				this.m_Text = value;
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
		/// 状态对象
		/// </summary>
		[DataMember]
		public virtual JsTreeStatus JsTreeStatus
		{
			get
			{
				return this.m_JsTreeStatus;
			}
			set
			{
				this.m_JsTreeStatus = value;
			}
		}

		/// <summary>
		/// 子节点集合
		/// </summary>
		[DataMember]
		public virtual List<JsTreeData> children
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
		public  JsTreeData()
		{
			this.JsTreeStatus = new JsTreeStatus();
            this.children = new List<JsTreeData>();
		}

		/// <summary>
		/// 带参构造函数
		/// </summary>
		public  JsTreeData(object id, string text, string icon = null):this()
		{
			this.m_Gid = id.ToString();
            this.m_Text = text;
            this.m_Icon = icon;
		}
		#endregion
	}
}