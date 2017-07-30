using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.CommonControl.Controls
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

        #region ComboBoxEdit控件

        /// <summary>
        /// 获取下拉列表的值
        /// </summary>
        /// <param name="combo">下拉列表</param>
        /// <returns></returns>
        public static string GetComboBoxStrValue(this ComboBoxEdit combo)
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
        /// 获取下拉列表的值
        /// </summary>
        /// <param name="combo">下拉列表</param>
        /// <returns></returns>
        public static Int32? GetComboBoxIntValue(this ComboBoxEdit combo)
        {
            CDicKeyValue item = combo.SelectedItem as CDicKeyValue;
            if (item != null)
            {
                return item.Value;
            }
            else
            {
                return null;
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

        /// <summary>
        /// 设置下拉列表选中指定的值
        /// </summary>
        /// <param name="combo">下拉列表</param>
        /// <param name="value">指定的CListItem中的值</param>
        public static void SetComboBoxItem(this ComboBoxEdit combo, Int32? value)
        {
            for (int i = 0; i < combo.Properties.Items.Count; i++)
            {
                CDicKeyValue item = combo.Properties.Items[i] as CDicKeyValue;
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
        /// <param name="dictTypeId">数据字典编号</param>
        public static void BindDictItems(this ComboBoxEdit combo, Int32 dictTypeId)
        {
            BindDictItems(combo, dictTypeId, null);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表（数据字典值允许 key(Int)=>valu(string)）
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        /// <param name="defaultValue">控件默认值</param>
        public static void BindDictItems(this ComboBoxEdit combo, Int32 dictTypeId, Int32? defaultValue)
        {
            var cacheDictData = Cache.Instance["DictData"] as List<DicKeyValueInfo>;
            if (cacheDictData != null)
            {
                return;
            }

            var lst = cacheDictData.FindAll(s => s.DictType_ID == dictTypeId);
            combo.Properties.BeginUpdate();//可以加快
            combo.Properties.Items.Clear();
            combo.Properties.Items.Add(new CDicKeyValue(Const.NoSeletValue, Const.NoSelectMsg));
            foreach (var one in lst)
            {
                combo.Properties.Items.Add(new CDicKeyValue(one.Value, one.Name));
            }

            if (defaultValue != null)
                SetComboBoxItem(combo, defaultValue);
            combo.Properties.EndUpdate();//可以加快
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表 如果下拉框出现 Key(string) => Value(String) 调用此方法
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
            combo.Properties.Items.Add(new CListItem(Const.NoSelectMsg, Const.NoSeletValue.ToString()));
            combo.Properties.Items.AddRange(itemList);
            /*for (Int32 i = 0; i < itemList.Count; i++)
            { 
                combo.Properties.Items.Add(itemList[i]);
            }*/
              
            if (!string.IsNullOrEmpty(defaultValue))
            {
                combo.SetComboBoxItem(defaultValue);
            }
            combo.Properties.EndUpdate();//可以加快
        }
        #endregion

        #region CheckedComboBoxEdit

        /// <summary>
        /// 获取下拉列表的值
        /// </summary>
        /// <param name="combo">下拉列表</param>
        /// <returns></returns>
        public static string GetCheckedComboBoxValue(this CheckedComboBoxEdit combo) 
        {
            if (combo.Properties.Items.Count <= 0)
            {
                return string.Empty;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                for (Int32 i = 0; i < combo.Properties.Items.Count; i ++)
                {
                    if (combo.Properties.Items[i].CheckState == CheckState.Checked)
                    {
                        sb.Append(combo.Properties.Items[i].Value + Const.Comma);
                    }
                }
                if (sb.Length > 0)
                    return sb.Remove(sb.Length - 1, 1).ToString();
                else
                    return sb.ToString();
            }
        }

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
        public static void BindDictItems(this CheckedComboBoxEdit combo, Int32 dictTypeId)
        {
            BindDictItems(combo, dictTypeId, null);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="dictTypeId">数据字典值</param>
        public static void BindDictItems(this CheckedComboBoxEdit combo, Int32 dictTypeId, string defaultValue)
        {
            List<CheckedListBoxItem> dataSourcre = new List<CheckedListBoxItem>();

            var cacheDictData = Cache.Instance["DictData"] as List<DicKeyValueInfo>;
            if (cacheDictData != null)
            {
                return;
            }

            var lst = cacheDictData.FindAll(s => s.DictType_ID == dictTypeId);
            combo.Properties.BeginUpdate();//可以加快
            combo.Properties.Items.Clear();
            foreach (DicKeyValueInfo one in lst)
            {
                dataSourcre.Add(new CheckedListBoxItem() { Value = one.Value, Description = one.Value + Const.Minus + one.Name });
            }
            combo.Properties.Items.AddRange(dataSourcre.ToArray());

            if (!string.IsNullOrEmpty(defaultValue))
            {
                combo.SetComboBoxItem(defaultValue);
            }

            combo.Properties.EndUpdate();//可以加快
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

        #region DateEdit 日期控件
        /// <summary>
        /// 设置时间格式有效显示，如果大于默认时间，赋值给控件；否则不赋值
        /// </summary>
        /// <param name="control">DateEdit控件对象</param>
        /// <param name="dateTime">日期对象</param>
        public static void SetDateTime(this DateEdit control, DateTime dateTime)
        {
            if (dateTime > Convert.ToDateTime("1900-1-1"))
            {
                control.EditValue = dateTime;
            }
            else
            {
                control.EditValue = string.Empty;
            }
        }

        /// <summary>
        /// 获取时间的显示内容，如果小于默认时间（1900-1-1），则为空
        /// </summary>
        /// <param name="dateTime">时间对象</param>
        /// <param name="formatString">默认格式为yyyy-MM-dd</param>
        /// <returns></returns>
        public static string GetDateTimeString(this DateEdit control, string formatString = Const.DateformatString)
        {
            string result = "";

            if (string.IsNullOrEmpty(control.EditValue.ToString()))
                return string.Empty;

            if (Convert.ToDateTime(control.EditValue) > Convert.ToDateTime("1900-1-1"))
            {
                result = Convert.ToDateTime(control.EditValue).ToString(formatString);
            }
            return result;
        }
        #endregion

        #region TimeEdit 时间控件
        /// <summary>
        /// 设置时间格式有效显示，如果大于默认时间，赋值给控件；否则不赋值
        /// </summary>
        /// <param name="control">DateEdit控件对象</param>
        /// <param name="dateTime">日期对象</param>
        public static void SetTime(this TimeEdit control, DateTime dateTime)
        {
            if (dateTime > Convert.ToDateTime("1900-1-1"))
            {
                control.EditValue = dateTime;
            }
            else
            {
                control.EditValue = string.Empty;
            }
        }

        /// <summary>
        /// 获取时间的显示内容，如果小于默认时间（1900-1-1），则为空
        /// </summary>
        /// <param name="dateTime">时间对象</param>
        /// <param name="formatString">默认格式为yyyy-MM-dd</param>
        /// <returns></returns>
        public static string GetTimeString(this TimeEdit control, string formatString = Const.TimeformatString)
        {
            string result = "";

            if (string.IsNullOrEmpty(control.EditValue.ToString()))
                return string.Empty;

            if (Convert.ToDateTime(control.EditValue) > Convert.ToDateTime("1900-1-1"))
            {
                result = Convert.ToDateTime(control.EditValue).ToString(formatString);
            }
            return result;
        }
        #endregion

        #region GridView 控件
        public static void SetGridColumWidth(this GridView gridView, string columnName, int width)
        {
            DevExpress.XtraGrid.Columns.GridColumn column = gridView.Columns.ColumnByFieldName(columnName);
            if (column != null)
            {
                column.Width = width;
            }
            else
            {
                //如果是数据库获取的Datatable可能字段为大写
                column = gridView.Columns.ColumnByFieldName(columnName.ToUpper());
                if (column != null)
                {
                    column.Width = width;
                }
            }
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
            //BindDictItems(radGroup, dictTypeName, null);
        }

        /// <summary>
        /// 绑定单选框组为指定的数据字典列表
        /// </summary>
        /// <param name="radGroup">单选框组</param>
        /// <param name="dictTypeName">字典大类</param>
        /// <param name="defaultValue">控件默认值</param>
        public static void BindDictItems(this RadioGroup radGroup, string dictTypeName, string defaultValue)
        {
            /*Dictionary<string, string> dict = BLLFactory<DictData>.Instance.GetDictByDictType(dictTypeName);
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

            radGroup.Properties.EndUpdate();//可以加快*/
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
