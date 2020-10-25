using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 功能菜单节点对象(MenuNodeInfo)
	/// 对象号: 100067
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class MenuNodeInfo : MenuInfo
	{
		#region Field Members

		/// <summary>
		/// 子节点
		/// </summary>
		private List<MenuNodeInfo> m_Children = new List<MenuNodeInfo>();
		#endregion

		#region Property Members

		/// <summary>
		/// 子节点
		/// </summary>
		[DataMember]
		public virtual List<MenuNodeInfo> Children
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
		public  MenuNodeInfo()
		{
			this.m_Children = new List<MenuNodeInfo>();
		}

		/// <summary>
		/// 带参构造函数
		/// </summary>
		public  MenuNodeInfo(MenuInfo menuInfo)
		{
			base.Gid = menuInfo.Gid;
            base.Pgid = menuInfo.Pgid;
            base.Name = menuInfo.Name;
            base.Seq = menuInfo.Seq;
            base.IsVisable = menuInfo.IsVisable;
            base.AuthGid = menuInfo.AuthGid;
            base.Icon = menuInfo.Icon;
            base.WebIcon = menuInfo.WebIcon;
            base.WinformClass = menuInfo.WinformClass;
			base.DllPath = menuInfo.DllPath;
            base.Url = menuInfo.Url;
            base.SystemtypeId = menuInfo.SystemtypeId;
            base.CreatorId = menuInfo.CreatorId;
            base.CreatorTime = menuInfo.CreatorTime;
            base.EditorId = menuInfo.EditorId;
            base.LastUpdateTime = menuInfo.LastUpdateTime;
		}
		#endregion
	}
}