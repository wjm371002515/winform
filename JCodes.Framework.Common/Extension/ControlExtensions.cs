using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// Control的扩展方法类
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// 结束当前编辑状态
        /// </summary>
        /// <param name="control"></param>
        public static void EndCurrentEdit(this Control control)
        {
            foreach (DictionaryEntry entry in control.BindingContext)
            {
                WeakReference weakRef = (WeakReference)entry.Value;
                if (weakRef.IsAlive)
                {
                    ((BindingManagerBase)weakRef.Target).EndCurrentEdit();
                }
            }
        }

        /// <summary>
        /// 绑定Text到数据源
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dataSource"></param>
        /// <param name="dataMember"></param>
        public static void Bind(this Control control, object dataSource, string dataMember)
        {
            Binding bd = new Binding("Text", dataSource, dataMember, false);
            control.DataBindings.Add(bd);
        }

        /// <summary>
        /// 绑定Text到数据源
        /// </summary>
        /// <param name="control"></param>
        /// <param name="property"></param>
        /// <param name="dataSource"></param>
        /// <param name="dataMember"></param>
        public static void Bind(this Control control, string property, object dataSource, string dataMember)
        {
            Binding bd = new Binding(property, dataSource, dataMember, false);
            control.DataBindings.Add(bd);
        }

        /// <summary>
        /// 绑定指定属性到数据源
        /// </summary>
        /// <param name="control"></param>
        /// <param name="property"></param>
        /// <param name="dataSource"></param>
        /// <param name="dataMember"></param>
        /// <param name="valueIsBool"></param>
        public static void Bind(this Control control, string property, object dataSource, string dataMember, bool valueIsBool)
        {
            Binding bd = new Binding(property, dataSource, dataMember, true);
            control.DataBindings.Add(bd);
            if (valueIsBool)
            {
                bd.Format += (sender, e) =>
                {
                    e.Value = ConvertFromSqlBoolean(e.Value);
                };
                bd.Parse += (sender, e) =>
                {
                    e.Value = ConvertToSqlBoolean((bool)e.Value);
                };
            }
        }

        /// <summary>
        /// 把数据库的布尔值转换为.net的布尔类型
        /// </summary>
        /// <param name="sqlBoolean"></param>
        /// <returns></returns>
        private static bool ConvertFromSqlBoolean(object sqlBoolean)
        {
            if (sqlBoolean == null || sqlBoolean == DBNull.Value) 
                return false;

            string value = sqlBoolean.ToString().ToLower();
            return value == "1" || value == "true" || value == "y";
        }
        /// <summary>
        /// 把.net的布尔类型转换为数据库的布尔值
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private static object ConvertToSqlBoolean(bool b)
        {
            return b ? "1" : "0";
        }
    }

    /// <summary>
    /// 数据集相关的扩展函数
    /// </summary>
    public static class DataSetExtensions
    {
        /// <summary>
        /// 转化为bool
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="colName">列名</param>
        /// <param name="defaultValue">字段为空时的默认值</param>
        /// <returns></returns>
        public static bool ToBool(this DataRow row, string colName, bool defaultValue = default(bool))
        {
            if (row.IsNull(colName))
            {
                return defaultValue;
            }
            string value = row[colName].ToString().ToLower();
            return value == "1" || value == "true" || value == "y";
        }

        /// <summary>
        /// 转化为Int
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="colName">列名</param>
        /// <param name="defaultValue">字段为空时的默认值</param>
        /// <returns></returns>
        public static int ToInt(this DataRow row, string colName, int defaultValue = default(int))
        {
            if (row.IsNull(colName))
            {
                return defaultValue;
            }
            return Convert.ToInt32(row[colName]);
        }

        /// <summary>
        /// 转化为Str
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="colName">列名</param>
        /// <param name="defaultValue">字段为空时的默认值</param>
        /// <returns></returns>
        public static string ToStr(this DataRow row, string colName, string defaultValue = "")
        {
            if (row.IsNull(colName))
            {
                return defaultValue;
            }
            return row[colName].ToString();
        }

        /// <summary>
        /// 转化为Double
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="colName">列名</param>
        /// <param name="defaultValue">字段为空时的默认值</param>
        /// <returns></returns>
        public static double ToDouble(this DataRow row, string colName, double defaultValue = default(double))
        {
            if (row.IsNull(colName))
            {
                return defaultValue;
            }
            return Convert.ToDouble(row[colName]);
        }


        /// <summary>
        /// 转化为Decimal
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="colName">列名</param>
        /// <param name="defaultValue">字段为空时的默认值</param>
        /// <returns></returns>
        public static Decimal ToDecimal(this DataRow row, string colName, Decimal defaultValue = default(Decimal))
        {
            if (row.IsNull(colName))
            {
                return defaultValue;
            }
            return Convert.ToDecimal(row[colName]);
        }

        /// <summary>
        /// 转化为Datetime
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="colName">列名</param>
        /// <param name="defaultValue">字段为空时的默认值</param>
        /// <returns></returns>
        public static DateTime ToDatetime(this DataRow row, string colName, DateTime defaultValue = default(DateTime))
        {
            if (row.IsNull(colName))
            {
                return defaultValue;
            }
            return Convert.ToDateTime(row[colName]);
        }

    }
}
