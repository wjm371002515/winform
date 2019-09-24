using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Collections;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.CommonControl.AdvanceSearch
{
    public partial class FrmAdvanceSearch : DevExpress.XtraEditors.XtraForm
    {
        public delegate void ConditionChangedEventHandler(SearchCondition condition);

        /// <summary>
        /// 查询条件触发的事件
        /// </summary>
        public event ConditionChangedEventHandler ConditionChanged;
        /// <summary>
        /// 清除数据触发事件
        /// </summary>
        public event EventHandler DataClear;    

        #region 字段设置

        private DataTable dtFieldTypeTable;//用于高级查询用途的表字段名称、类型列表
        private string displayColumns = "";
        private Dictionary<string, int> columnDict = new Dictionary<string, int>();
        private Dictionary<string, string> columnNameAlias = new Dictionary<string, string>();//字段别名字典集合
        private Dictionary<string, List<CListItem>> listItemDict = new Dictionary<string, List<CListItem>>();
        private DataTable dtAdvance;

        /// <summary>
        /// 用于高级查询用途的表字段名称、类型列表
        /// </summary>
        public DataTable FieldTypeTable
        {
            get { return dtFieldTypeTable; }
            set { dtFieldTypeTable = value; }
        }
        /// <summary>
        /// 显示的列内容，需要指定以防止GridView乱序
        /// 使用"|"或者","分开每个列，如“ID,Name”
        /// </summary>
        public string DisplayColumns
        {
            get { return displayColumns; }
            set
            {
                displayColumns = value;
                columnDict = new Dictionary<string, int>();
                string[] items = displayColumns.Split(new char[] { '|', ',' });
                for (int i = 0; i < items.Length; i++)
                {
                    string str = items[i];
                    if (!string.IsNullOrEmpty(str))
                    {
                        str = str.Trim();
                        if (!columnDict.ContainsKey(str.ToUpper()))
                        {
                            columnDict.Add(str.ToUpper(), i);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 列名的别名字典集合
        /// </summary>
        public Dictionary<string, string> ColumnNameAlias
        {
            get { return columnNameAlias; }
            set
            {
                if (value != null)
                {
                    foreach (string key in value.Keys)
                    {
                        AddColumnAlias(key, value[key]);
                    }
                }
            }
        }

        /// <summary>
        /// 添加列名的别名（显示名称）
        /// </summary>
        /// <param name="key">列的原始名称</param>
        /// <param name="alias">列的别名（显示名称）</param>
        public void AddColumnAlias(string key, string alias)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(alias))
            {
                if (!columnNameAlias.ContainsKey(key.ToUpper()))
                {
                    columnNameAlias.Add(key.ToUpper(), alias);
                }
                else
                {
                    columnNameAlias[key.ToUpper()] = alias;
                }
            }
        }

        /// <summary>
        /// 为指定列设置下拉列表默认值
        /// </summary>
        /// <param name="key">列的原始名称</param>
        /// <param name="listItems">列的别名列表集合</param>
        public void AddColumnListItem(string key, ICollection<CListItem> listItems)
        {
            if (!string.IsNullOrEmpty(key) && listItems != null)
            {
                if (!listItemDict.ContainsKey(key.ToUpper()))
                {
                    List<CListItem> list = new List<CListItem>();
                    if (listItems != null)
                    {
                        list.AddRange(listItems);
                    }
                    listItemDict.Add(key.ToUpper(), list);
                }
            }
        }

        /// <summary>
        /// 为指定列设置下拉列表默认值
        /// </summary>
        /// <param name="key">列的原始名称</param>
        /// <param name="listItems">列的别名列表集合</param>
        public void AddColumnListItem(string key, List<CListItem> listItems)
        {
            if (!string.IsNullOrEmpty(key) && listItems != null)
            {
                if (!listItemDict.ContainsKey(key.ToUpper()))
                {
                    listItemDict.Add(key.ToUpper(), listItems);
                }
            }
        }

        /// <summary>
        /// 为指定列设置下拉列表默认值
        /// </summary>
        /// <param name="key">列的原始名称</param>
        /// <param name="listItems">列的别名列表集合</param>
        public void AddColumnListItem(string key, List<string> listItems)
        {
            if (!string.IsNullOrEmpty(key) && listItems != null)
            {
                if (!listItemDict.ContainsKey(key.ToUpper()))
                {
                    List<CListItem> list = new List<CListItem>();
                    foreach (string item in listItems)
                    {
                        list.Add(new CListItem(item, item));
                    }
                    listItemDict.Add(key.ToUpper(), list);
                }
            }
        }

        /// <summary>
        /// 为指定列设置下拉列表默认值
        /// </summary>
        /// <param name="key">列的原始名称</param>
        /// <param name="listItems">列的别名列表集合，字符串格式，使用"|"或者","分开每个列，如“ID,Name”</param>
        public void AddColumnListItem(string key, string listItems)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(listItems))
            {
                if (!listItemDict.ContainsKey(key.ToUpper()))
                {
                    List<CListItem> list = new List<CListItem>();
                    string[] items = listItems.Split(new char[] { '|', ',' });
                    for (int i = 0; i < items.Length; i++)
                    {
                        string str = items[i];
                        if (!string.IsNullOrEmpty(str))
                        {
                            str = str.Trim();
                            if (!string.IsNullOrEmpty(str))
                            {
                                list.Add(new CListItem(str, str));
                            }
                        }
                    }
                    listItemDict.Add(key.ToUpper(), list);
                }
            }
        }

        /// <summary>
        /// 返回对应字段的显示顺序，如果没有，返回-1
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private int GetDisplayColumnIndex(string columnName)
        {
            int result = -1;
            if (columnDict.ContainsKey(columnName.ToUpper()))
            {
                result = columnDict[columnName.ToUpper()];
            }

            return result;
        }

        #endregion

        public FrmAdvanceSearch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 处理数据查询后的事件触发
        /// </summary>
        public virtual void ProcessDataSearch(object sender, EventArgs e)
        {
            if (dtAdvance != null)
            {
                SearchCondition searchCondition = new SearchCondition();
                foreach (DataRow row in dtAdvance.Rows)
                {
                    string fieldName = row["字段"].ToString();
                    string fieldDisplay = row["字段名称"].ToString();
                    FieldType fieldType = (FieldType)row["字段类型"];
                    string fieldValue = row["查询条件值"].ToString();
                    string valueDisplay = row["查询条件显示"].ToString();

                    if (fieldType == FieldType.Text)
                    {
                        searchCondition.AddCondition(fieldName, fieldValue, SqlOperator.Like);
                    }
                    else if (fieldType == FieldType.DropdownList)
                    {
                        searchCondition.AddCondition(fieldName, fieldValue, SqlOperator.Equal);
                    }
                    else if (fieldType == FieldType.DateTime)
                    {
                        #region 日期类型转换
                        if (!string.IsNullOrEmpty(fieldValue))
                        {
                            string[] itemArray = fieldValue.Split('~');
                            if (itemArray != null)
                            {
                                DateTime value;
                                bool result = false;

                                if (itemArray.Length > 0)
                                {
                                    result = DateTime.TryParse(itemArray[0].Trim(), out value);
                                    if (result)
                                    {
                                        searchCondition.AddCondition(fieldName, value, SqlOperator.MoreThanOrEqual);
                                    }
                                }
                                if (itemArray.Length > 1)
                                {
                                    result = DateTime.TryParse(itemArray[1].Trim(), out value);
                                    if (result)
                                    {
                                        searchCondition.AddCondition(fieldName, value.AddDays(1), SqlOperator.LessThan);
                                    }
                                }
                            }
                        } 
                        #endregion
                    }
                    else if (fieldType == FieldType.Numeric)
                    {
                        #region 数值类型转换
                        if (!string.IsNullOrEmpty(fieldValue))
                        {
                            string[] itemArray = fieldValue.Split('~');
                            if (itemArray != null)
                            {
                                decimal value = 0M;
                                bool result = false;

                                if (itemArray.Length > 0)
                                {
                                    result = decimal.TryParse(itemArray[0].Trim(), out value);
                                    if (result)
                                    {
                                        searchCondition.AddCondition(fieldName, value, SqlOperator.MoreThanOrEqual);
                                    }
                                }
                                if (itemArray.Length > 1)
                                {
                                    result = decimal.TryParse(itemArray[1].Trim(), out value);
                                    if (result)
                                    {
                                        searchCondition.AddCondition(fieldName, value, SqlOperator.LessThanOrEqual);
                                    }
                                }
                            }
                        } 
                        #endregion
                    }
                }

                if (ConditionChanged != null)
                {
                    ConditionChanged(searchCondition);
                }
            }
        }

        /// <summary>
        /// 数据清除后的操作
        /// </summary>
        public virtual void ProcessDataClear(object sender, EventArgs e)
        {
            if(DataClear != null)
            {
                ProcessDataSearch(null, null);

                DataClear(null,null);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ProcessDataSearch(null, null);
        }

        private void FrmAdvanceSearch_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            //第一次创建对象内容，后面只需要更新界面即可
            if (dtAdvance == null)
            {
                #region 首次创建
                dtAdvance = new DataTable();
                dtAdvance.Columns.Add("字段");
                dtAdvance.Columns.Add("字段名称");
                dtAdvance.Columns.Add("字段类型", typeof(FieldType));
                dtAdvance.Columns.Add("查询条件值");
                dtAdvance.Columns.Add("查询条件显示");

                FieldType customedType = FieldType.Text;
                foreach (DataRow dr in dtFieldTypeTable.Rows)
                {
                    #region 转换字段显示名称
                    string originalName = dr["ColumnName"].ToString();
                    string columnName = originalName;
                    if (!string.IsNullOrEmpty(this.DisplayColumns) && !columnDict.ContainsKey(columnName.ToUpper()))
                    {
                        continue;//人为去掉，跳过
                    }

                    if (columnNameAlias.ContainsKey(columnName.ToUpper()))
                    {
                        columnName = columnNameAlias[columnName.ToUpper()];
                    }
                    #endregion

                    #region 转换数据内容
                    string dataType = dr["DataType"].ToString();
                    switch (dataType)
                    {
                        case "system.byte[]"://跳过一些不需查询的字段类型                            
                            continue;

                        case "system.string":
                        case "system.guid":
                        case "system.char":
                        case "system.boolean":
                            customedType = FieldType.Text;
                            break;

                        case "system.int16":
                        case "system.int32":
                        case "system.int64":
                        case "system.uint16":
                        case "system.uint32":
                        case "system.uint64":
                        case "system.single":
                        case "system.decimal":
                        case "system.double":
                        case "system.float":
                        case "system.byte":
                            customedType = FieldType.Numeric;
                            break;
                        case "system.datetime":
                            customedType = FieldType.DateTime; //需要大写
                            break;
                    }
                    #endregion

                    //特殊转换
                    if (listItemDict.ContainsKey(originalName))
                    {
                        customedType = FieldType.DropdownList;
                    }

                    DataRow row = dtAdvance.NewRow();
                    row["字段"] = originalName;
                    row["字段类型"] = customedType;
                    row["字段名称"] = columnName;
                    dtAdvance.Rows.Add(row);
                } 
                #endregion
            }
            this.gridControl1.DataSource = dtAdvance;
        }

        /// <summary>
        /// 更新查询字段值，并重新绑定界面
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="fieldValue">字段值</param>
        private void UpdateFieldValue(string fieldName, string fieldValue, string valueDisplay)
        {
            if (dtAdvance != null && !string.IsNullOrEmpty(fieldName))
            {
                foreach (DataRow row in dtAdvance.Rows)
                {
                    string name = row["字段"].ToString();
                    if (fieldName.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        row["查询条件值"] = fieldValue;
                        row["查询条件值"] = fieldValue;
                        row["查询条件显示"] = valueDisplay;
                    }
                }
                BindData();
            }
        }

        void dlg_DataClear(string fieldName)
        {
            UpdateFieldValue(fieldName, "", "");

            //更新父窗体的数据显示
            ProcessDataSearch(null, null);
        }

        private void ctxMenuSetData_Click(object sender, EventArgs e)
        {
            gridControl1_MouseDoubleClick(null, null);
        }

        private void ctxMenuClearData_Click(object sender, EventArgs e)
        {
            int[] rowSelected = this.gridView1.GetSelectedRows();
            if (rowSelected.Length == 0) return;

            string fieldName = this.gridView1.GetFocusedRowCellValue("字段").ToString();
            if(!string.IsNullOrEmpty(fieldName))
            {
                dlg_DataClear(fieldName);
            }
        }

        private void gridControl1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.gridView1.Columns.Count > 0)
            {
                this.gridView1.Columns["字段"].Visible = false;
                this.gridView1.Columns["字段类型"].Visible = false;
                this.gridView1.Columns["查询条件值"].Visible = false;

                this.gridView1.Columns["字段名称"].Width = 100;
                this.gridView1.Columns["查询条件显示"].Width = 200;
            }
        }

        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int[] rowSelected = this.gridView1.GetSelectedRows();
            if (rowSelected.Length == 0) return;

            string fieldName = this.gridView1.GetFocusedRowCellValue("字段").ToString();
            string fieldDisplay = this.gridView1.GetFocusedRowCellValue("字段名称").ToString();
            FieldType fieldType = (FieldType)this.gridView1.GetFocusedRowCellValue("字段类型");
            string fieldValue = this.gridView1.GetFocusedRowCellValue("查询条件值").ToString();
            #region 根据类型转换不同的窗体
            FrmQueryBase dlg = null;
            if (fieldType == FieldType.Text)
            {
                dlg = new FrmQueryTextEdit();
            }
            else if (fieldType == FieldType.Numeric)
            {
                dlg = new FrmQueryNumericEdit();
            }
            else if (fieldType == FieldType.DateTime)
            {
                dlg = new FrmQueryDateEdit();
            }
            else if (fieldType == FieldType.DropdownList)
            {
                dlg = new FrmQueryDropdown();
            }
            #endregion

            dlg.FieldName = fieldName;
            dlg.FieldDisplayName = fieldDisplay;
            dlg.FieldDefaultValue = fieldValue;
            if (listItemDict.ContainsKey(fieldName.ToUpper()))
            {
                dlg.DropDownItems = listItemDict[fieldName.ToUpper()];
            }

            dlg.DataClear += new FrmQueryBase.DataClearEventHandler(dlg_DataClear);
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //更新查询界面显示
                UpdateFieldValue(fieldName, dlg.ReturnValue, dlg.ReturnDisplay);

                //更新父窗体的数据显示
                ProcessDataSearch(null, null);
            }
        }

        private void gridControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gridControl1_MouseDoubleClick(null, null);
            }
        }
    }
}
