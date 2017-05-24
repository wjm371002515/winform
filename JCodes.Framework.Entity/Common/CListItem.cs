using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 框架用来记录字典键值的类，用于Comobox等控件对象的值传递
    /// </summary>
    [Serializable]
    [DataContract]
    public class CListItem
    {
        /// <summary>
        /// 参数化构造CListItem对象
        /// </summary>
        /// <param name="text">显示的内容</param>
        /// <param name="value">实际的值内容</param>
        public CListItem(string text, string value)
        {
            this.text = text;
            this.value = value;
        }

        /// <summary>
        /// 参数化构造CListItem对象
        /// </summary>
        /// <param name="text">显示的内容</param>
        public CListItem(string text)
        {
            this.text = text;
            this.value = text;
        }

        private string text;
        private string value;

        /// <summary>
        /// 显示内容
        /// </summary>
        [DataMember]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>
        /// 实际值内容
        /// </summary>
        [DataMember]
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>
        /// 返回显示的内容
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text.ToString();
        }

    }
}
