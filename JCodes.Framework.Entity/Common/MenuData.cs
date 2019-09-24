using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 定义菜单Json的相关数据，方便控制器生成Json数据进行传递(MenuData)
	/// 对象号: 100026
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class MenuData
	{
		#region Field Members

		/// <summary>
		/// GUID对应的ID序号
		/// </summary>
		private String m_Gid = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		private String m_Name = string.Empty;

		/// <summary>
		/// icon图标路径
		/// </summary>
		private String m_Icon = string.Empty;

		/// <summary>
		/// URL地址
		/// </summary>
		private String m_Url = string.Empty;

		/// <summary>
		/// 子节点集合
		/// </summary>
		private List<MenuData> m_menus = new List<MenuData>();
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
		/// URL地址
		/// </summary>
		[DataMember]
		[DisplayName("URL地址")]
		public virtual String Url
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
		/// 子节点集合
		/// </summary>
		[DataMember]
		public virtual List<MenuData> menus
		{
			get
			{
				return this.m_menus;
			}
			set
			{
				this.m_menus = value;
			}
		}

		/// <summary>
		/// 无参构造函数
		/// </summary>
		public  MenuData() 
		{
			this.menus = new List<MenuData>();
            this.m_Icon = "icon-view";
		}

		/// <summary>
		/// 带参构造函数
		/// </summary>
		public  MenuData(string menuid, string menuname, string icon = "icon-view", string url=null):this()
		{
			this.m_Gid = menuid;
            this.m_Name = menuname;
            this.m_Icon = icon;
            this.m_Url = url;
		}
		#endregion
	}
}