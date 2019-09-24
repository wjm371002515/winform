using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 框架用来记录字典键值的类，用于Comobox等控件对象的值传递(CDicKeyValue)
	/// 对象号: 100006
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class CDicKeyValue
	{
		#region Field Members

		/// <summary>
		/// 键
		/// </summary>
		private Int32 m_Value = 0;

		/// <summary>
		/// 值
		/// </summary>
		private string m_Text;
		#endregion

		#region Property Members

		/// <summary>
		/// 键
		/// </summary>
		[DataMember]
		public virtual Int32 Value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		/// <summary>
		/// 值
		/// </summary>
		[DataMember]
		public virtual string Text
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
		/// 
		/// 构造函数
		/// </summary>
		public  CDicKeyValue(Int32 value, string text)
		{
			this.m_Text = text;
            this.m_Value = value;
		}

		/// <summary>
		/// 
		/// 返回显示的内容
		/// </summary>
		public override string ToString()
		{
			return string.Format("{0}-{1}", this.m_Value, this.m_Text);
		}

		#endregion
	}
}