using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.AddIn
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
        public static bool IsInDesignMode(this System.Windows.Forms.Control control)
        {
            return ResolveDesignMode(control);
        }

        /// <summary>
        /// 检查控件或者它的父控件是否在设计模式
        /// </summary>
        /// <param name="control">控件对象</param>
        /// <returns>如果是设计模式，返回true，否则为false</returns>
        private static bool ResolveDesignMode(System.Windows.Forms.Control control)
        {
            System.Reflection.PropertyInfo designModeProperty;
            bool designMode;

            designModeProperty = control.GetType().GetProperty("DesignMode", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
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
            for(int i =0; i <combo.Properties.Items.Count; i++)
            {
                if(combo.Properties.Items[i].ToString() == value)
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

        #region CustomGridLookUpEdit控件

        /// <summary>
        /// 获取下拉列表的值
        /// </summary>
        /// <param name="combo">下拉列表</param>
        /// <returns></returns>
        public static string GetComboBoxValue(this CustomGridLookUpEdit combo)
        {
            return combo.EditValue as string;
        }

        /// <summary>
        /// 设置下拉列表选中指定的值
        /// </summary>
        /// <param name="combo">下拉列表</param>
        /// <param name="value">指定的CListItem中的值</param>
        public static void SetComboBoxItem(this CustomGridLookUpEdit combo, string value)
        {
            combo.EditValue = value;
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        public static void BindDictItems(this CustomGridLookUpEdit combo, string dictTypeName)
        {
            BindDictItems(combo, dictTypeName, null);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="dictTypeName">数据字典类型名称</param>
        /// <param name="defaultValue">控件默认值</param>
        public static void BindDictItems(this CustomGridLookUpEdit combo, string dictTypeName, string defaultValue)
        {
            string displayName = dictTypeName;
            const string valueName = "值内容";
            const string pinyin = "拼音码";
            DataTable dt = DataTableHelper.CreateTable(string.Format("{0},{1},{2}", displayName, valueName, pinyin));

            Dictionary<string, string> dict = BLLFactory<DictData>.Instance.GetDictByDictType(dictTypeName);
            foreach (string key in dict.Keys)
            {
                DataRow row = dt.NewRow();
                row[displayName] = key;
                row[valueName] = dict[key];
                row[pinyin] = Pinyin.GetFirstPY(key);
                dt.Rows.Add(row);
            }

            combo.Properties.ValueMember = valueName;
            combo.Properties.DisplayMember = displayName;
            combo.Properties.DataSource = dt;
            combo.Properties.PopulateViewColumns();
            combo.Properties.View.Columns[valueName].Visible = false;
            combo.Properties.View.Columns[displayName].Width = 400;
            combo.Properties.View.Columns[pinyin].Width = 200;
            combo.Properties.PopupFormMinSize = new System.Drawing.Size(600, 0);

            if (!string.IsNullOrEmpty(defaultValue))
            {
                combo.EditValue = defaultValue;
            }
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="itemList">数据字典列表</param>
        public static void BindDictItems(this CustomGridLookUpEdit combo, List<CListItem> itemList)
        {
            BindDictItems(combo, itemList, null);
        }

        /// <summary>
        /// 绑定下拉列表控件为指定的数据字典列表
        /// </summary>
        /// <param name="combo">下拉列表控件</param>
        /// <param name="itemList">数据字典列表</param>
        /// <param name="defaultValue">控件默认值</param>
        public static void BindDictItems(this CustomGridLookUpEdit combo, List<CListItem> itemList, string defaultValue)
        {
            string displayName = "显示内容";
            const string valueName = "值内容";
            const string pinyin = "拼音码";
            DataTable dt = DataTableHelper.CreateTable(string.Format("{0},{1},{2}", displayName, valueName, pinyin));

            foreach (CListItem item in itemList)
            {
                DataRow row = dt.NewRow();
                row[displayName] = item.Text;
                row[valueName] = item.Value;
                row[pinyin] = Pinyin.GetFirstPY(item.Text);
                dt.Rows.Add(row);
            }

            combo.Properties.ValueMember = valueName;
            combo.Properties.DisplayMember = displayName;
            combo.Properties.DataSource = dt;
            combo.Properties.PopulateViewColumns();
            combo.Properties.View.Columns[valueName].Visible = false;

            if (!string.IsNullOrEmpty(defaultValue))
            {
                combo.Text = defaultValue;
            }

            if (!string.IsNullOrEmpty(defaultValue))
            {
                combo.EditValue = defaultValue;
            }
        }
        #endregion

        #region 查询相关扩展

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
        public static SearchCondition AddDateCondition(this SearchCondition condition, string fieldName, DateTime startDate, DateTime endDate)
        {
            condition.AddCondition(fieldName, startDate, SqlOperator.MoreThanOrEqual);
            condition.AddCondition(fieldName, endDate.AddDays(1), SqlOperator.LessThan);
            return condition;
        }

        /// <summary>
        /// 添加数值区间的查询操作
        /// </summary>
        /// <param name="condition">SearchCondition对象</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="startCtrl">开始范围控件</param>
        /// <param name="endCtrl">结束范围控件</param>
        /// <returns></returns>
        public static SearchCondition AddNumericCondition(this SearchCondition condition, string fieldName, SpinEdit startCtrl, SpinEdit endCtrl)
        {
            if (startCtrl.Text.Length > 0)
            {
                condition.AddCondition(fieldName, startCtrl.Value, SqlOperator.MoreThanOrEqual);
            }
            if (endCtrl.Text.Length > 0)
            {
                condition.AddCondition(fieldName, endCtrl.Value, SqlOperator.LessThanOrEqual);
            }
            return condition;
        }

        public static SearchCondition AddNumericCondition(this SearchCondition condition, string fieldName, TextEdit startCtrl, TextEdit endCtrl)
        {
            decimal value = 0;
            if (decimal.TryParse(startCtrl.Text.Trim(), out value))
            {
                condition.AddCondition(fieldName, value, SqlOperator.MoreThanOrEqual);
            }
            if (decimal.TryParse(endCtrl.Text.Trim(), out value))
            {
                condition.AddCondition(fieldName, value, SqlOperator.LessThanOrEqual);
            }
            return condition;
        }

        public static SearchCondition AddNumericCondition2(this SearchCondition condition, string fieldName, TextEdit startCtrl, TextEdit endCtrl)
        {
            decimal value = 0;
            int hour = 0;
            int minute = 0;
            decimal hourMinute = 0;
            if (decimal.TryParse(startCtrl.Text.Trim(), out value))
            {
                hour = (int)value;
                hourMinute = hour * 60;
                string[] startValue = startCtrl.Text.Split(new char[] { '.' });
                if (int.TryParse(startValue[1].Trim(), out minute))
                {
                    hourMinute += minute;
                }
                condition.AddCondition(fieldName, hourMinute, SqlOperator.MoreThanOrEqual);
            }
            if (decimal.TryParse(endCtrl.Text.Trim(), out value))
            {
                hour = (int)value;
                hourMinute = hour * 60;
                string[] endValue = endCtrl.Text.Split(new char[] { '.' });
                if (int.TryParse(endValue[1].Trim(), out minute))
                {
                    hourMinute += minute;
                }
                condition.AddCondition(fieldName, hourMinute, SqlOperator.LessThanOrEqual);
            }
            return condition;
        }
        #endregion

        #region 控件布局显示
        /// <summary>
        /// 设置控件组是否显示
        /// </summary>
        /// <returns></returns>
        public static void ToVisibility(this DevExpress.XtraLayout.LayoutControlGroup control, bool visible)
        {
            if (visible)
            {
                control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        /// <summary>
        /// 获取控件组是否为显示状态
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool GetVisibility(this DevExpress.XtraLayout.LayoutControlGroup control)
        {
            return control.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
        /// <summary>
        /// 设置控件组是否显示
        /// </summary>
        /// <returns></returns>
        public static void ToVisibility(this DevExpress.XtraLayout.LayoutControlItem control, bool visible)
        {
            if (visible)
            {
                control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        /// <summary>
        /// 获取控件组是否为显示状态
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool GetVisibility(this DevExpress.XtraLayout.LayoutControlItem control)
        {
            return control.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
        /// <summary>
        /// 设置控件组是否显示
        /// </summary>
        /// <returns></returns>
        public static void ToVisibility(this DevExpress.XtraLayout.EmptySpaceItem control, bool visible)
        {
            if (visible)
            {
                control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                control.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        /// <summary>
        /// 获取控件组是否为显示状态
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool GetVisibility(this DevExpress.XtraLayout.EmptySpaceItem control)
        {
            return control.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }

        /// <summary>
        /// 设置控件组是否显示
        /// </summary>
        /// <returns></returns>
        public static void ToVisibility(this DevExpress.XtraBars.BarButtonItem control, bool visible)
        {
            if (visible)
            {
                control.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                control.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        #endregion

    }
}
