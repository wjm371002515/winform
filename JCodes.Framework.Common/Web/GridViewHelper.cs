using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace JCodes.Framework.Common.Web
{
    /// <summary>
    /// GridView和DataGridView常用的操作辅助类
    /// </summary>
    public class GridViewHelper
    {
        /// <summary>
        /// 从GridView的数据生成DataTable
        /// </summary>
        /// <param name="gv">GridView对象</param>
        public static DataTable GridView2DataTable(GridView gv)
        {
            DataTable table = new DataTable();
            int rowIndex = 0;
            List<string> cols = new List<string>();
            if (!gv.ShowHeader && gv.Columns.Count == 0)
            {
                return table;
            }
            GridViewRow headerRow = gv.HeaderRow;
            int columnCount = headerRow.Cells.Count;
            for (int i = 0; i < columnCount; i++)
            {
                string text = GetCellText(headerRow.Cells[i]);
                cols.Add(text);
            }
            foreach (GridViewRow r in gv.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    DataRow row = table.NewRow();
                    int j = 0;
                    for (int i = 0; i < columnCount; i++)
                    {
                        string text = GetCellText(r.Cells[i]);
                        if (!String.IsNullOrEmpty(text))
                        {
                            if (rowIndex == 0)
                            {
                                string columnName = cols[i];
                                if (String.IsNullOrEmpty(columnName))
                                {
                                    continue;
                                }
                                if (table.Columns.Contains(columnName))
                                {
                                    continue;
                                }
                                DataColumn dc = table.Columns.Add();
                                dc.ColumnName = columnName;
                                dc.DataType = typeof(string);
                            }
                            row[j] = text;
                            j++;
                        }
                    }
                    rowIndex++;
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        /// <summary>
        /// 获取DataGrid控件中选择的项目的ID字符串(要求DataGrid设置datakeyfield="ID")
        /// </summary>
        /// <param name="dg">DataGrid控件</param>
        /// <returns>如果没有选择, 那么返回为空字符串, 否则返回逗号分隔的ID字符串(如1,2,3)</returns>
        public static string GetDatagridItems(DataGrid dg)
        {
            return GetDatagridItems(dg, false);
        }

        /// <summary>
        /// 获取DataGrid控件中选择的项目的ID字符串(要求DataGrid设置datakeyfield="ID")
        /// </summary>
        /// <param name="dg">DataGrid控件</param>
        /// <param name="UseSemicolon">是否使用''将ID括起</param>
        /// <returns>如果没有选择, 那么返回为空字符串, 否则返回逗号分隔的ID字符串(如1,2,3)</returns>
        public static string GetDatagridItems(DataGrid dg, bool UseSemicolon)
        {
            string idstring = string.Empty;
            foreach (DataGridItem item in dg.Items)
            {
                string key = dg.DataKeys[item.ItemIndex].ToString();
                bool isSelected = ((CheckBox)item.FindControl("cbxDelete")).Checked;
                if (isSelected)
                {
                    if (UseSemicolon)
                    {
                        idstring += "'" + key + "',";
                    }
                    else
                    {
                        idstring += key + ",";
                    }
                }
            }
            idstring = idstring.Trim(',');

            return idstring;
        }

        /// <summary>
        /// 设置下拉列表的值
        /// </summary>
        /// <param name="control">下拉列表控件</param>
        /// <param name="strValue">待显示的值</param>
        public static void SetDropDownListItem(DropDownList control, string strValue)
        {
            if (!string.IsNullOrEmpty(strValue))
            {
                //control.SelectedIndex = -1;
                control.ClearSelection();
                ListItem item = control.Items.FindByValue(strValue);
                if (item != null)
                {
                    control.SelectedValue = item.Value;
                }
            }
        }

        #region 私有方法
        /// <summary>
        /// 截取内容长度
        /// </summary>
        /// <param name="o_Str">原字符串</param>
        /// <param name="len">截取长度</param>
        /// <returns>截取后字符串</returns>
        private static string GetStrPartly(string o_Str, int len)
        {
            if (len == 0)
            {
                return o_Str;
            }
            else
            {
                if (o_Str.Length > len)
                {
                    return o_Str.Substring(0, len) + "..";
                }
                else
                {
                    return o_Str;
                }
            }
        }

        /// <summary>
        /// 获取单元格内容
        /// </summary>
        /// <param name="cell">TableCell</param>
        /// <returns>内容</returns>
        private static string GetCellText(TableCell cell)
        {
            string text = cell.Text;
            if (!string.IsNullOrEmpty(text))
            {
                return text;
            }
            foreach (Control control in cell.Controls)
            {
                if (control != null && control is IButtonControl)
                {
                    IButtonControl btn = control as IButtonControl;
                    text = btn.Text.Replace("\r\n", "").Trim();
                    break;
                }
                if (control != null && control is ITextControl)
                {
                    LiteralControl lc = control as LiteralControl;
                    if (lc != null)
                    {
                        continue;
                    }
                    ITextControl l = control as ITextControl;
                    text = l.Text.Replace("\r\n", "").Trim();
                    break;
                }
            }
            return text;
        }

        /// <summary>
        /// 设置单元格内容
        /// </summary>
        /// <param name="cell">TableCell</param>
        /// <param name="maxLen">最大长度</param>
        private static void SetCellText(TableCell cell, int maxLen)
        {
            string text = cell.Text;
            if (!string.IsNullOrEmpty(text))
            {
                cell.Text = GetStrPartly(text, maxLen);
            }
            foreach (Control control in cell.Controls)
            {
                if (control != null && control is IButtonControl)
                {
                    IButtonControl btn = control as IButtonControl;
                    text = btn.Text.Replace("\r\n", "").Trim();
                    btn.Text = GetStrPartly(text, maxLen);
                    break;
                }
                if (control != null && control is ITextControl)
                {
                    LiteralControl lc = control as LiteralControl;
                    if (lc != null)
                    {
                        continue;
                    }
                    ITextControl l = control as ITextControl;
                    text = l.Text.Replace("\r\n", "").Trim();
                    if (l is DataBoundLiteralControl)
                    {
                        cell.Text = GetStrPartly(text, maxLen);
                        break;
                    }
                    else
                    {
                        l.Text = GetStrPartly(text, maxLen);
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
