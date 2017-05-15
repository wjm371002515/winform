using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// CheckListBox列表控件选择项操作辅助类
    /// </summary>
    public class CheckBoxListUtil
    {
        /// <summary>
        /// 设置列表选择项，如果列表值在字符串中，则选中
        /// </summary>
        /// <param name="cblItems">列表控件</param>
        /// <param name="valueList">值列表，逗号分开各个值</param>
        public static void SetCheck(CheckedListBox cblItems, string valueList)
        {
            string[] strtemp = valueList.Split(',');
            foreach (string str in strtemp)
            {
                for (int i = 0; i < cblItems.Items.Count; i++)
                {
                    if (cblItems.GetItemText(cblItems.Items[i]) == str)
                    {
                        cblItems.SetItemChecked(i, true);
                    }
                }
            }
        }

        /// <summary>
        /// 获取列表控件的选中的值，各值通过逗号分开
        /// </summary>
        /// <param name="cblItems">列表控件</param>
        /// <returns></returns>
        public static string GetCheckedItems(CheckedListBox cblItems)
        {
            string resultList = "";
            for (int i = 0; i < cblItems.CheckedItems.Count; i++)
            {
                if (cblItems.GetItemChecked(i))
                {
                    resultList += string.Format("{0},", cblItems.GetItemText(cblItems.Items[i]));
                }
            }
            return resultList.Trim(',');
        }

        /// <summary>
        /// 如果值列表中有的,根据内容勾选GroupBox里面的成员.
        /// </summary>
        /// <param name="group">包含CheckBox控件组的GroupBox控件</param>
        /// <param name="valueList">逗号分隔的值列表</param>
        public static void SetCheck(GroupBox group, string valueList)
        {
            string[] strtemp = valueList.Split(',');
            foreach (string str in strtemp)
            {
                foreach (Control control in group.Controls)
                {
                    CheckBox chk = control as CheckBox;
                    if (chk != null && chk.Text == str)
                    {
                        chk.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// 获取GroupBox控件成员勾选的值
        /// </summary>
        /// <param name="group">包含CheckBox控件组的GroupBox控件</param>
        /// <returns>返回逗号分隔的值列表</returns>
        public static string GetCheckedItems(GroupBox group)
        {
            string resultList = "";
            foreach (Control control in group.Controls)
            {
                CheckBox chk = control as CheckBox;
                if (chk != null && chk.Checked)
                {
                    resultList += string.Format("{0},", chk.Text);
                }
            }
            return resultList.Trim(',');
        }
    }
}
