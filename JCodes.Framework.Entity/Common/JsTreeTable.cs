using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// JsTree的数据模型(datatable类型)(JsTreeTable)
	/// 对象号: 100024
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class JsTreeTable
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
		private JsTreeStatus m_JsTreeDataStatus;
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
		public virtual JsTreeStatus JsTreeDataStatus
		{
			get
			{
				return this.m_JsTreeDataStatus;
			}
			set
			{
				this.m_JsTreeDataStatus = value;
			}
		}

		/// <summary>
		/// 无参构造函数
		/// </summary>
		public  JsTreeTable()
		{
			this.Pgid = "#"; //#表示为根节点
            this.JsTreeDataStatus = new JsTreeStatus();
		}

		/// <summary>
		/// 有参构造函数
		/// </summary>
		public  JsTreeTable(string id, string text, string icon = null, string parent = "#")
		{
			this.m_Gid = id;
            this.m_Text = text;
            this.m_Icon = icon;

            this.Pgid = parent;
            this.JsTreeDataStatus = new JsTreeStatus();
		}
		#endregion
	}
}