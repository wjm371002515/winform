using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Text.RegularExpressions;
namespace JCodes.Framework.Entity
{
	/// <summary>
	/// 字符串键值对(CListItem)
	/// 对象号: 100005
	/// 备注信息: 
	/// </summary>
	[Serializable]
	[DataContract]
	public partial class CListItem
	{
		#region Field Members

		/// <summary>
		/// 键
		/// </summary>
		private String m_Value = string.Empty;

		/// <summary>
		/// 值
		/// </summary>
		private String m_Text = string.Empty;
		#endregion

		#region Property Members

		/// <summary>
		/// 键
		/// </summary>
		[DataMember]
		public virtual String Value
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
		/// 显示内容
		/// </summary>
		public override string ToString()
		{
			if (Regex.IsMatch(this.m_Value, "[A-F0-9]{8}(-[A-F0-9]{4}){3}-[A-F0-9]{12}|[A-F0-9]{32}", RegexOptions.IgnoreCase))
            {
                return this.m_Text;
            }
            else
            {
                return string.Format("{0}-{1}", m_Value, m_Text);
            }
		}

		/// <summary>
		/// 参数化构造CListItem对象
		/// 20170901 wjm 调整key 和value的顺序
		/// </summary>
		public  CListItem(string value, string text)
		{
			 this.m_Text = text;
            this.m_Value = value;
		}
		#endregion
	}
}