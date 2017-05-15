using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using DevExpress.XtraEditors;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common;

namespace JCodes.Framework.CommonControl
{
    /// <summary>
    /// 扩展函数封装
    /// </summary>
    public static class ExtensionMethod
    {
        #region 日期控件
        /// <summary>
        /// 设置时间格式有效显示，如果大于默认时间，赋值给控件；否则不赋值
        /// </summary>
        /// <param name="control">DateEdit控件对象</param>
        /// <param name="dateTime">日期对象</param>
        public static void SetDateTime(this DateEdit control, DateTime dateTime)
        {
            if (dateTime > Convert.ToDateTime("1900-1-1"))
            {
                control.DateTime = dateTime;
            }
            else
            {
                control.Text = "";
            }
        }

        /// <summary>
        /// 获取时间的显示内容，如果小于默认时间（1900-1-1），则为空
        /// </summary>
        /// <param name="dateTime">时间对象</param>
        /// <param name="formatString">默认格式为yyyy-MM-dd</param>
        /// <returns></returns>
        public static string GetDateTimeString(this DateTime dateTime, string formatString = "yyyy-MM-dd")
        {
            string result = "";
            if (dateTime > Convert.ToDateTime("1900-1-1"))
            {
                result = dateTime.ToString(formatString);
            }
            return result;
        }
        #endregion

        #region ComboBoxEdit控件
        /// <summary>
        /// 获取下拉列表的值
        /// </summary>
        /// <param name="combo">下拉列表</param>
        /// <returns></returns>
        public static string GetComboBoxValue(this ComboBoxEdit combo)
        {
            CListItem item = combo.SelectedItem as CListItem;
            if (item != null)
            {
                return item.Value;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 设置下拉列表选中指定的值
        /// </summary>
        /// <param name="combo">下拉列表</param>
        /// <param name="value">指定的CListItem中的值</param>
        public static void SetComboBoxItem(this ComboBoxEdit combo, string value)
        {
            for (int i = 0; i < combo.Properties.Items.Count; i++)
            {
                CListItem item = combo.Properties.Items[i] as CListItem;
                if (item != null && item.Value == value)
                {
                    combo.SelectedIndex = i;
                }
            }
        }

        #endregion

        /// <summary>
        /// 设置单选框组的选定内容
        /// </summary>
        /// <param name="radGroup">单选框组</param>
        /// <param name="value">选定内容</param>
        public static void SetRaidioGroupItem(this RadioGroup radGroup, string value)
        {
            radGroup.SelectedIndex = radGroup.Properties.Items.GetItemIndexByValue(value);
        }

        /// <summary>
        /// 添加开始日期和结束日期的查询操作
        /// </summary>
        /// <param name="condition">SearchCondition对象</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="startCtrl">开始日期控件</param>
        /// <param name="endCtrl">结束日期控件</param>
        /// <returns></returns>
        public static SearchCondition AddDateCondition(this SearchCondition condition, string fieldName, DateEdit startCtrl, DateEdit endCtrl)
        {
            if (startCtrl.Text.Length > 0)
            {
                condition.AddCondition(fieldName, startCtrl.DateTime, SqlOperator.MoreThanOrEqual);
            }
            if (endCtrl.Text.Length > 0)
            {
                condition.AddCondition(fieldName, endCtrl.DateTime.AddDays(1), SqlOperator.LessThan);
            }
            return condition;
        }

        /// <summary>
        /// 已分隔符显示多项内容
        /// </summary>
        /// <param name="dict">Dictionary对象</param>
        /// <returns></returns>
        public static string ToDiplayString(this Dictionary<string, string> dict)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string key in dict.Keys)
            {
                string info = dict[key];
                if (!string.IsNullOrEmpty(info))
                {
                    sb.AppendFormat("{0} /", info);
                }
            }
            return sb.ToString().Trim('/');
        }

        /// <summary>
        /// 克隆一个新的对象
        /// </summary>
        /// <param name="dict">待克隆的Dict对象</param>
        /// <returns></returns>
        public static Dictionary<string, string> Clone(this Dictionary<string, string> dict)
        {
            Dictionary<string, string> newDict = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in dict)
            {
                newDict.Add(pair.Key, pair.Value);
            }
            return newDict;
        }

        /// <summary>
        /// IP地址转换为INT类型
        /// </summary>
        /// <param name="IP">IP地址</param>
        /// <returns></returns>
        public static int ToInteger(this IPAddress IP)
        {
            int result = 0;

            byte[] bytes = IP.GetAddressBytes();
            result = (int)(bytes[0] << 24 | bytes[1] << 16 | bytes[2] << 8 | bytes[3]);

            return result;
        }

        /// <summary>
        /// 比较两个IP的大小。如果相等返回0，如果IP1大于IP2返回1，如果IP1小于IP2返回-1。
        /// </summary>
        /// <param name="IP1">IP地址1</param>
        /// <param name="IP2">IP地址2</param>
        /// <returns>如果相等返回0，如果IP1大于IP2返回1，如果IP1小于IP2返回-1。</returns>
        public static int Compare(this IPAddress IP1, IPAddress IP2)
        {
            int ip1 = IP1.ToInteger();
            int ip2 = IP2.ToInteger();
            return (((ip1 - ip2) >> 0x1F) | (int)((uint)(-(ip1 - ip2)) >> 0x1F));
        }
    }
}
