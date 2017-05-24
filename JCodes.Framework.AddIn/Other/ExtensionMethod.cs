using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.Other
{
    /// <summary>
    /// 扩展函数封装
    /// </summary>
    public static class ExtensionMethod
    {

        #region 控件设计状态判断

        /// <summary>
        /// 判断控件是否在设计模式下
        /// </summary>
        public static bool IsInDesignMode(this Control control)
        {
            return ResolveDesignMode(control);
        }

        /// <summary>
        /// 检查控件或者它的父控件是否在设计模式
        /// </summary>
        /// <param name="control">控件对象</param>
        /// <returns>如果是设计模式，返回true，否则为false</returns>
        private static bool ResolveDesignMode(Control control)
        {
            System.Reflection.PropertyInfo designModeProperty;
            bool designMode;

            designModeProperty = control.GetType().GetProperty("DesignMode", BindingFlags.Instance | BindingFlags.NonPublic);
            designMode = (bool)designModeProperty.GetValue(control, null);

            if (control.Parent != null)
            {
                designMode |= ResolveDesignMode(control.Parent);
            }
            return designMode;
        }
        #endregion

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

        #region 查询控件扩展函数
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
        /// 添加开始日期和结束日期的查询操作
        /// </summary>
        /// <param name="condition">SearchCondition对象</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public static SearchCondition AddDateCondition(this SearchCondition condition, string fieldName, string startDate, string endDate)
        {
            DateTime date;
            if (!string.IsNullOrEmpty(startDate) && DateTime.TryParse(startDate, out date))
            {
                condition.AddCondition(fieldName, date, SqlOperator.MoreThanOrEqual);
            }

            if (!string.IsNullOrEmpty(endDate) && DateTime.TryParse(endDate, out date))
            {
                condition.AddCondition(fieldName, date.AddDays(1), SqlOperator.LessThan);
            }
            return condition;
        }

        /// <summary>
        /// 添加开始日期和结束日期的查询操作
        /// </summary>
        /// <param name="condition">SearchCondition对象</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public static SearchCondition AddDateCondition(this SearchCondition condition, string fieldName, DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue)
            {
                condition.AddCondition(fieldName, startDate.Value, SqlOperator.MoreThanOrEqual);
            }
            if (endDate.HasValue)
            {
                condition.AddCondition(fieldName, endDate.Value.AddDays(1), SqlOperator.LessThan);
            }
            return condition;
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
        public static int SetDropDownValue(this ComboBoxEdit combo, string value)
        {
            int result = -1;
            for (int i = 0; i < combo.Properties.Items.Count; i++)
            {
                if (combo.Properties.Items[i].ToString() == value)
                {
                    combo.SelectedIndex = i;
                    result = i;
                    break;
                }
            }
            return result;
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

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        public static void BindDictItems(this ComboBoxEdit combo, string dictTypeName)
        {
            BindDictItems(combo, dictTypeName, null);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        /// <param name="defaultValue">控件默认值</param>
        public static void BindDictItems(this ComboBoxEdit combo, string dictTypeName, string defaultValue)
        {
            Dictionary<string, string> dict = BLLFactory<DictData>.Instance.GetDictByDictType(dictTypeName);
            List<CListItem> itemList = new List<CListItem>();
            foreach (string key in dict.Keys)
            {
                itemList.Add(new CListItem(key, dict[key]));
            }

            BindDictItems(combo, itemList, defaultValue);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="itemList">数据字典列表</param>
        public static void BindDictItems(this ComboBoxEdit combo, List<CListItem> itemList)
        {
            BindDictItems(combo, itemList, null);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="itemList">数据字典列表</param>
        /// <param name="defaultValue">控件默认值</param>
        public static void BindDictItems(this ComboBoxEdit combo, List<CListItem> itemList, string defaultValue)
        {
            combo.Properties.BeginUpdate();//可以加快
            combo.Properties.Items.Clear();
            combo.Properties.Items.AddRange(itemList);

            if (!string.IsNullOrEmpty(defaultValue))
            {
                combo.SetComboBoxItem(defaultValue);
            }

            combo.Properties.EndUpdate();//可以加快
        }
        #endregion

        #region 单选框组RadioGroup
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
        /// 绑定单选框组为指定的数据字典列表
        /// </summary>
        /// <param name="radGroup">单选框组</param>
        /// <param name="dictTypeName">字典大类</param>
        public static void BindDictItems(this RadioGroup radGroup, string dictTypeName)
        {
            BindDictItems(radGroup, dictTypeName, null);
        }

        /// <summary>
        /// 绑定单选框组为指定的数据字典列表
        /// </summary>
        /// <param name="radGroup">单选框组</param>
        /// <param name="dictTypeName">字典大类</param>
        /// <param name="defaultValue">控件默认值</param>
        public static void BindDictItems(this RadioGroup radGroup, string dictTypeName, string defaultValue)
        {
            Dictionary<string, string> dict = BLLFactory<DictData>.Instance.GetDictByDictType(dictTypeName);
            List<RadioGroupItem> groupList = new List<RadioGroupItem>();
            foreach (string key in dict.Keys)
            {
                groupList.Add(new RadioGroupItem(dict[key], key));
            }

            radGroup.Properties.BeginUpdate();//可以加快
            radGroup.Properties.Items.Clear();
            radGroup.Properties.Items.AddRange(groupList.ToArray());//可以加快

            if (!string.IsNullOrEmpty(defaultValue))
            {
                SetRaidioGroupItem(radGroup, defaultValue);
            }

            radGroup.Properties.EndUpdate();//可以加快
        }

        /// <summary>
        /// 绑定单选框组为指定的数据字典列表
        /// </summary>
        /// <param name="radGroup">单选框组</param>
        /// <param name="itemList">字典列表</param>
        public static void BindDictItems(this RadioGroup radGroup, List<CListItem> itemList)
        {
            BindDictItems(radGroup, itemList, null);
        }

        /// <summary>
        /// 绑定单选框组为指定的数据字典列表
        /// </summary>
        /// <param name="radGroup">单选框组</param>
        /// <param name="itemList">字典列表</param>
        /// <param name="defaultValue">控件默认值</param>
        public static void BindDictItems(this RadioGroup radGroup, List<CListItem> itemList, string defaultValue)
        {
            List<RadioGroupItem> groupList = new List<RadioGroupItem>();
            foreach (CListItem item in itemList)
            {
                groupList.Add(new RadioGroupItem(item.Value, item.Text));
            }

            radGroup.Properties.BeginUpdate();//可以加快
            radGroup.Properties.Items.Clear();
            radGroup.Properties.Items.AddRange(groupList.ToArray());//可以加快

            if (!string.IsNullOrEmpty(defaultValue))
            {
                SetRaidioGroupItem(radGroup, defaultValue);
            }
            radGroup.Properties.EndUpdate();//可以加快
        }


        #endregion

        #region CheckedComboBoxEdit
        /// <summary>
        /// 设置下拉列表选中指定的值
        /// </summary>
        /// <param name="combo">下拉列表</param>
        /// <param name="value">指定的CListItem中的值</param>
        public static void SetComboBoxItem(this CheckedComboBoxEdit combo, string value)
        {
            combo.SetEditValue(value);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        public static void BindDictItems(this CheckedComboBoxEdit combo, string dictTypeName)
        {
            BindDictItems(combo, dictTypeName, null);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        public static void BindDictItems(this CheckedComboBoxEdit combo, string dictTypeName, string defaultValue)
        {
            List<CListItem> itemList = new List<CListItem>();
            Dictionary<string, string> dict = BLLFactory<DictData>.Instance.GetDictByDictType(dictTypeName);
            foreach (string key in dict.Keys)
            {
                itemList.Add(new CListItem(key, dict[key]));
            }

            BindDictItems(combo, itemList, defaultValue);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="itemList">数据字典列表</param>
        public static void BindDictItems(this CheckedComboBoxEdit combo, List<CListItem> itemList)
        {
            BindDictItems(combo, itemList, null);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="itemList">数据字典列表</param>
        public static void BindDictItems(this CheckedComboBoxEdit combo, List<CListItem> itemList, string defaultValue)
        {
            List<CheckedListBoxItem> checkList = new List<CheckedListBoxItem>();
            foreach (CListItem item in itemList)
            {
                checkList.Add(new CheckedListBoxItem(item.Value, item.Text));
            }

            combo.Properties.BeginUpdate();//可以加快
            combo.Properties.Items.Clear();
            combo.Properties.Items.AddRange(checkList.ToArray());//可以加快

            if (!string.IsNullOrEmpty(defaultValue))
            {
                combo.SetComboBoxItem(defaultValue);
            }

            combo.Properties.EndUpdate();//可以加快
        }
        #endregion

        #region CheckedListBox
        /// <summary>
        /// 设置下拉列表选中指定的值
        /// </summary>
        /// <param name="combo">下拉列表</param>
        /// <param name="value">指定的CListItem中的值</param>
        public static void SetComboBoxItem(this CheckedListBox combo, string value)
        {
            combo.SelectedValue = value;
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="itemList">数据字典列表</param>
        public static void BindDictItems(this CheckedListBox combo, List<CListItem> itemList)
        {
            BindDictItems(combo, itemList, null);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="itemList">数据字典列表</param>
        /// <param name="defaultValue">默认值</param>
        public static void BindDictItems(this CheckedListBox combo, List<CListItem> itemList, string defaultValue)
        {
            combo.BeginUpdate();//可以加快
            combo.Items.Clear();
            combo.Items.AddRange(itemList.ToArray());//可以加快

            if (!string.IsNullOrEmpty(defaultValue))
            {
                combo.SetComboBoxItem(defaultValue);
            }

            combo.EndUpdate();//可以加快
        }

        #endregion


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

        #region 控件布局显示
        /// <summary>
        /// 设置控件组是否显示
        /// </summary>
        /// <returns></returns>
        public static void ToVisibility(this LayoutControlGroup control, bool visible)
        {
            if (visible)
            {
                control.Visibility = LayoutVisibility.Always;
            }
            else
            {
                control.Visibility = LayoutVisibility.Never;
            }
        }

        /// <summary>
        /// 获取控件组是否为显示状态
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool GetVisibility(this LayoutControlGroup control)
        {
            return control.Visibility == LayoutVisibility.Always;
        }
        /// <summary>
        /// 设置控件组是否显示
        /// </summary>
        /// <returns></returns>
        public static void ToVisibility(this LayoutControlItem control, bool visible)
        {
            if (visible)
            {
                control.Visibility = LayoutVisibility.Always;
            }
            else
            {
                control.Visibility = LayoutVisibility.Never;
            }
        }

        /// <summary>
        /// 获取控件组是否为显示状态
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool GetVisibility(this LayoutControlItem control)
        {
            return control.Visibility == LayoutVisibility.Always;
        }
        /// <summary>
        /// 设置控件组是否显示
        /// </summary>
        /// <returns></returns>
        public static void ToVisibility(this EmptySpaceItem control, bool visible)
        {
            if (visible)
            {
                control.Visibility = LayoutVisibility.Always;
            }
            else
            {
                control.Visibility = LayoutVisibility.Never;
            }
        }

        /// <summary>
        /// 获取控件组是否为显示状态
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool GetVisibility(this EmptySpaceItem control)
        {
            return control.Visibility == LayoutVisibility.Always;
        }

        /// <summary>
        /// 设置控件组是否显示
        /// </summary>
        /// <returns></returns>
        public static void ToVisibility(this BarButtonItem control, bool visible)
        {
            if (visible)
            {
                control.Visibility = BarItemVisibility.Always;
            }
            else
            {
                control.Visibility = BarItemVisibility.Never;
            }
        }

        #endregion

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
    }
}
