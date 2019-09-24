using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 系统功能节点对象(FunctionNodeInfo)
	/// 对象号: 100068
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class FunctionNodeInfo : FunctionInfo
	{
		#region Field Members

		/// <summary>
		/// 子节点
		/// </summary>
		private List<FunctionNodeInfo> m_Children = new List<FunctionNodeInfo>();
		#endregion

		#region Property Members

		/// <summary>
		/// 子节点
		/// </summary>
		[DataMember]
		public virtual List<FunctionNodeInfo> Children
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
		public  FunctionNodeInfo()
		{
			this.m_Children = new List<FunctionNodeInfo>();
		}

		/// <summary>
		/// 带参构造函数
		/// </summary>
		public  FunctionNodeInfo(FunctionInfo functionInfo)
		{
			base.DllPath = functionInfo.DllPath;
            base.Gid = functionInfo.Gid;
            base.Name = functionInfo.Name;
            base.Pgid = functionInfo.Pgid;
            base.SystemtypeId = functionInfo.SystemtypeId;
            base.Seq = functionInfo.Seq;
		}
		#endregion
	}
}