using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl.Pager.Others;
using JCodes.Framework.CommonControl.Other;

namespace JCodes.Framework.CommonControl.Pager
{ 
    /// <summary>
    /// 不带分页导航的列表展示控件
    /// </summary>
    public partial class WinGridView : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 是否显示CheckBox列
        /// </summary>
        public bool ShowCheckBox { get; set; }

        private object dataSource;//数据源
        private string displayColumns = "";//显示的列
        private string printTitle = "";//报表标题
        private Dictionary<string, int> columnDict = new Dictionary<string, int>();//字段列顺序

        private SaveFileDialog saveFileDialog = new SaveFileDialog();
        private bool isExportAllPage = false;//是否导出所有页
        private Dictionary<string, string> columnNameAlias = new Dictionary<string, string>();//字段别名字典集合
        private ContextMenuStrip appendedMenu;//右键菜单
        private bool m_ShowAddMenu = true;//是否显示新建菜单
        private bool m_ShowEditMenu = true;//是否显示编辑菜单
        private bool m_ShowDeleteMenu = true;//是否显示删除菜单

        /// <summary>
        /// 新建菜单的显示内容
        /// </summary>
        public string AddMenuText = "新建(&N)";
        /// <summary>
        /// 编辑菜单的显示内容
        /// </summary>
        public string EditMenuText = "编辑选定项(&E)";
        /// <summary>
        /// 删除菜单的显示内容
        /// </summary>
        public string DeleteMenuText = "删除选定项(&D)";
        /// <summary>
        /// 刷新菜单的显示内容
        /// </summary>
        public string RefreshMenuText = "刷新列表(&R)";

        /// <summary>
        /// 导出全部的数据源
        /// </summary>
        public object AllToExport;

        /// <summary>
        /// 是否显示行号
        /// </summary>
        public bool ShowLineNumber = true;
        /// <summary>
        /// 获取或设置奇数行的背景色
        /// </summary>
        public Color EventRowBackColor = Color.LightCyan;

        /// <summary>
        /// 是否使用最佳宽度
        /// </summary>
        public bool BestFitColumnWith = true;

        /// <summary>
        /// 冻结列的固定样式，默认为左边
        /// </summary>
        public FixedStyle Fixed = FixedStyle.Left;

        /// <summary>
        /// 冻结列的字段，多个字段逗号分开
        /// </summary>
        public string FixedColumns { get;set;}
        #region 权限功能控制
        /// <summary>
        /// 设置是否显示导出菜单
        /// </summary>
        [Category("分页"), Description("是否显示导出菜单。"), Browsable(true)]
        public bool ShowExportButton
        {
            get
            {
                return this.menu_Export.Enabled;
            }
            set
            {
                this.menu_Export.Enabled = value;
            }
        }
        /// <summary>
        /// 是否显示新建菜单
        /// </summary>
        [Category("分页"), Description("是否显示新建菜单。"), Browsable(true)]
        public bool ShowAddMenu
        {
            get { return m_ShowAddMenu; }
            set { m_ShowAddMenu = value; }
        }
        /// <summary>
        /// 是否显示编辑菜单
        /// </summary>
        [Category("分页"), Description("是否显示编辑菜单。"), Browsable(true)]
        public bool ShowEditMenu
        {
            get { return m_ShowEditMenu; }
            set { m_ShowEditMenu = value; }
        }

        /// <summary>
        /// 是否显示删除菜单
        /// </summary>
        [Category("分页"), Description("是否显示删除菜单。"), Browsable(true)]
        public bool ShowDeleteMenu
        {
            get { return m_ShowDeleteMenu; }
            set { m_ShowDeleteMenu = value; }
        }  
        #endregion

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

        #region 事件处理

        /// <summary>
        /// 导出Excel前执行的操作
        /// </summary>
        public event EventHandler OnStartExport;
        /// <summary>
        /// 导出Excel后执行的操作
        /// </summary>
        public event EventHandler OnEndExport;
        /// <summary>
        /// 双击控件实现的操作，实现后出现右键菜单“编辑选定项”
        /// </summary>
        public event EventHandler OnEditSelected;
        /// <summary>
        /// 实现事件后出现“删除选定项”菜单项
        /// </summary>
        public event EventHandler OnDeleteSelected;
        /// <summary>
        /// 实现事件后出现“更新”菜单项
        /// </summary>
        public event EventHandler OnRefresh;
        /// <summary>
        /// 实现事件后，出现“新建”菜单项
        /// </summary>
        public event EventHandler OnAddNew;
        /// <summary>
        /// 实现对单击GirdView控件的响应
        /// </summary>
        public event EventHandler OnGridViewMouseClick;                
        /// <summary>
        /// 实现对双击GirdView控件的响应
        /// </summary>
        public event EventHandler OnGridViewMouseDoubleClick;
        /// <summary>
        /// 实现对复选框选择变化的响应
        /// </summary>
        public event SelectionChangedEventHandler OnCheckBoxSelectionChanged;

        #endregion

        /// <summary>
        /// 追加的菜单项目
        /// </summary>
        public ContextMenuStrip AppendedMenu
        {
            get
            { 
                return appendedMenu;
            }
            set
            {
                if (value != null)
                {
                    appendedMenu = value;

                    if (appendedMenu.Items.Count > 0)
                        this.toolStripSeparator1.Visible = true;

                    for (int i = 0; appendedMenu.Items.Count > 0; i++)
                    {
                        this.contextMenuStrip1.Items.Insert(i, appendedMenu.Items[0]);
                    }
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public WinGridView()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            this.menu_Add.Visible = (this.OnAddNew != null && this.ShowAddMenu);
            this.menu_Delete.Visible = (this.OnDeleteSelected != null && this.ShowDeleteMenu);
            this.menu_Edit.Visible = (this.OnEditSelected != null && this.ShowEditMenu);
            this.menu_Refresh.Visible = (this.OnRefresh != null);
            this.toolStripSeparator2.Visible = (menu_Add.Visible || menu_Delete.Visible || menu_Edit.Visible || menu_Refresh.Visible);

            this.menu_Add.Text = AddMenuText;
            this.menu_Edit.Text = EditMenuText;
            this.menu_Delete.Text = DeleteMenuText;
            this.menu_Refresh.Text = RefreshMenuText;
        }

        /// <summary>
        /// 封装的GridView1
        /// </summary>
        public GridView GridView1
        {
            get
            {
                return this.gridView1;
            }
        }

        /// <summary>
        /// 获取或设置数据源
        /// </summary>
        public object DataSource
        {
            get { return dataSource; }
            set
            {
                if (this.gridView1.Columns != null)
                {
                    this.gridView1.Columns.Clear();
                }

                dataSource = value;                                
                this.gridControl1.DataSource = dataSource;
            }
        }

        private Dictionary<string, string> GetColumnNameTypes(DataTable dt)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (DataColumn col in dt.Columns)
            {
                if (!dict.ContainsKey(col.ColumnName))
                {
                    dict.Add(col.ColumnName, col.DataType.FullName);
                }
            }
            return dict;
        }

        /// <summary>
        /// 显示的列内容，需要指定以防止GridView乱序
        /// 使用"|"或者","分开每个列，如“ID|Name”
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
        /// 返回对应字段的显示顺序，如果没有，返回-1
        /// </summary>
        /// <param name="columnName">列名称</param>
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
        
        /// <summary>
        /// 添加列名的别名
        /// </summary>
        /// <param name="key">列的原始名称</param>
        /// <param name="alias">列的别名</param>
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
        /// 打印报表的抬头（标题）
        /// </summary>
        public string PrintTitle
        {
            get { return printTitle; }
            set { printTitle = value; }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            isExportAllPage = true;
            ExportToExcel();
        }

        private void btnExportCurrent_Click(object sender, EventArgs e)
        {
            isExportAllPage = false;
            ExportToExcel();
        }        

        #region 导出Excel操作

        private void ExportToExcel()
        {
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.xls)|*.xls";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!saveFileDialog.FileName.Equals(String.Empty))
                {
                    FileInfo f = new FileInfo(saveFileDialog.FileName);
                    if (f.Extension.ToLower().Equals(".xls"))
                    {
                        StartExport(saveFileDialog.FileName);
                    }
                    else
                    {
                        MessageDxUtil.ShowTips("文件格式不正确");
                    }
                }
                else
                {
                    MessageDxUtil.ShowTips("需要指定一个保存的目录");
                }
            }
        }

        /// <summary>
        /// starts the export to new excel document
        /// </summary>
        /// <param name="filepath">the file to export to</param>
        private void StartExport(String filepath)
        {
            if (OnStartExport != null)
            {
                OnStartExport(this, new EventArgs());
            }

            //create a new background worker, to do the exporting
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += new DoWorkEventHandler(bg_DoWork);
            bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
            bg.RunWorkerAsync(filepath);
        }

        /// <summary>
        /// 使用背景线程导出Excel文档
        /// </summary>
        private void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable table = new DataTable();
            if (AllToExport != null && isExportAllPage)
            {
                if (AllToExport is DataView)
                {
                    DataView dv = (DataView)AllToExport;//默认导出显示内容
                    table = dv.ToTable();
                }
                else if (AllToExport is DataTable)
                {
                    table = AllToExport as DataTable;
                }
                else
                {
                    table = ReflectionUtil.CreateTable(AllToExport);
                }
                
                //解析标题
                string originalName = string.Empty;
                foreach (DataColumn column in table.Columns)
                {
                    originalName = column.Caption;
                    if (columnNameAlias.ContainsKey(originalName.ToUpper()))
                    {
                        column.Caption = columnNameAlias[originalName.ToUpper()];
                        column.ColumnName = columnNameAlias[originalName.ToUpper()];
                    }
                }
                //for (int i = 0; i < this.gridView1.Columns.Count; i++)
                //{
                //    if (!this.gridView1.Columns[i].Visible)
                //    {
                //        table.Columns.Remove(this.gridView1.Columns[i].FieldName);
                //    }
                //}
            }
            else
            {
                DataColumn column;
                DataRow row;
                for (int i = 0; i < this.gridView1.Columns.Count; i++)
                {
                    if (this.gridView1.Columns[i].Visible)
                    {
                        column = new DataColumn(this.gridView1.Columns[i].FieldName, typeof(string));
                        column.Caption = this.gridView1.Columns[i].Caption;
                        table.Columns.Add(column);
                    }
                }

                object cellValue = "";
                string fieldName = "";
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    row = table.NewRow();
                    for (int j = 0; j < gridView1.Columns.Count; j++)
                    {
                        if (this.gridView1.Columns[j].Visible)
                        {
                            fieldName = gridView1.Columns[j].FieldName;
                            cellValue = gridView1.GetRowCellValue(i, fieldName);
                            row[fieldName] = cellValue ?? "";
                        }
                    }
                    table.Rows.Add(row);
                }
            }

            string outError = "";
            AsposeExcelTools.DataTableToExcel2(table, (String)e.Argument, out outError);
        }

        //show a message to the user when the background worker has finished
        //and re-enable the export buttons
        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (OnEndExport != null)
            {
                OnEndExport(this, new EventArgs());
            }

            if (MessageDxUtil.ShowYesNoAndTips("导出操作完成, 您想打开该Excel文件么?") == DialogResult.Yes)
            {
                Process.Start(saveFileDialog.FileName);
            }
        }
                
        #endregion

        #region 菜单操作
        private void menu_Delete_Click(object sender, EventArgs e)
        {
            if (OnDeleteSelected != null && this.ShowDeleteMenu)
            {
                OnDeleteSelected(this.gridView1, new EventArgs());
            }
        }

        private void menu_Refresh_Click(object sender, EventArgs e)
        {
            if (this.OnRefresh != null)
            {
                OnRefresh(this.gridView1, new EventArgs());
            }
        }

        private void menu_Edit_Click(object sender, EventArgs e)
        {
            if (OnEditSelected != null && this.ShowEditMenu)
            {
                OnEditSelected(this.gridView1, new EventArgs());
            }
        }

        private void menu_Print_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_GridView(this.gridView1, this.printTitle);
        } 

        private void menu_Add_Click(object sender, EventArgs e)
        {
            if (this.OnAddNew != null && this.ShowAddMenu)
            {
                this.OnAddNew(this.gridView1, new EventArgs());
            }
        }

        private void menu_Export_Click(object sender, EventArgs e)
        {
            isExportAllPage = false;
            ExportToExcel();
        }

        private void menu_CopyInfo_Click(object sender, EventArgs e)
        {
            int[] selectedRow = this.gridView1.GetSelectedRows();
            if (selectedRow == null || selectedRow.Length == 0)
                return;

            StringBuilder sbHeader = new StringBuilder();
            StringBuilder sb = new StringBuilder();

            if (selectedRow.Length == 1)
            {
                //单行复制的时候
                foreach (GridColumn gridCol in this.gridView1.Columns)
                {
                    if (gridCol.Visible)
                    {
                        sbHeader.AppendFormat("{0}：{1} \r\n", gridCol.Caption, this.gridView1.GetRowCellDisplayText(selectedRow[0], gridCol.FieldName));
                    }
                }
                sb.AppendLine();
            }
            else
            {
                //多行复制的时候
                foreach (GridColumn gridCol in this.gridView1.Columns)
                {
                    if (gridCol.Visible)
                    {
                        sbHeader.AppendFormat("{0}\t", gridCol.Caption);
                    }
                }

                foreach (int row in selectedRow)
                {
                    foreach (GridColumn gridCol in this.gridView1.Columns)
                    {
                        if (gridCol.Visible)
                        {
                            sb.AppendFormat("{0}\t", this.gridView1.GetRowCellDisplayText(row, gridCol.FieldName));
                        }
                    }
                    sb.AppendLine();
                }
            }

            try {
                Clipboard.Clear();
                Clipboard.SetText(sbHeader.ToString() + "\r\n" + sb.ToString());
                MessageDxUtil.ShowTips(Const.CopyOkMsg);
            }
            catch (Exception ex)
            {
                
            }
        }

        private void menu_SetColumn_Click(object sender, EventArgs e)
        {
            FrmSelectColumnDisplay dlg = new FrmSelectColumnDisplay();
            dlg.DisplayColumNames = this.displayColumns;
            dlg.ColumnNameAlias = columnNameAlias;
            dlg.DataGridView = this.gridView1;
            dlg.ShowDialog();
        }

        #endregion
       
        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            #region 修改别名及可见
            //先判断设置显示的列(如果没有则显示全部）
            string originalName = string.Empty;
            string tempColumns = string.Empty;
            if (string.IsNullOrEmpty(this.DisplayColumns))
            {
                for (int i = 0; i < this.gridView1.Columns.Count; i++)
                {
                    originalName = this.gridView1.Columns[i].FieldName;
                    tempColumns += string.Format("{0},", originalName);
                }
                tempColumns = tempColumns.Trim(',');
                this.DisplayColumns = tempColumns;//全部显示
            }

            //转换为大写列表
            List<string> fixedList = new List<string>();
            if (!string.IsNullOrEmpty(FixedColumns))
            {
                fixedList = FixedColumns.ToUpper().ToDelimitedList<string>(",");
            }

            //字段的排序顺序，先记录（使用排序的字典）
            SortedDictionary<int, string> colIndexList = new SortedDictionary<int, string>();
            foreach (GridColumn col in this.gridView1.Columns)
            {
                //设置列标题
                originalName = col.FieldName;
                if (columnNameAlias.ContainsKey(originalName.ToUpper()))
                {
                    col.Caption = columnNameAlias[originalName.ToUpper()];
                }
                else
                {
                    col.Caption = originalName;//如果没有别名用原始字段名称，如ID
                }

                //设置不显示字段
                if (!columnDict.ContainsKey(originalName.ToUpper()))
                {
                    col.Visible = false;
                }

                //这里先记录每个字段名称，以及它的真实顺序位置
                //col.VisibleIndex = GetDisplayColumnIndex(originalName.ToUpper());
                int VisibleIndex = GetDisplayColumnIndex(originalName.ToUpper());
                if (VisibleIndex == -1)
                {
                    //如果是不显示的，则设置可见顺序为-1
                    col.VisibleIndex = VisibleIndex;
                }
                else
                {
                    //否则记录起来后面一并按顺序设置
                    if (!colIndexList.ContainsKey(VisibleIndex))
                    {
                        colIndexList.Add(VisibleIndex, originalName);
                    }
                }
            }

            //统一设置所有可见的字段顺序
            foreach (int index in colIndexList.Keys)
            {
                originalName = colIndexList[index];
                this.gridView1.Columns[originalName].VisibleIndex = index;
            }

            //设置列固定（大写判断）
            for (int i = 0; i < this.gridView1.VisibleColumns.Count; i++)
            {
                GridColumn col = this.gridView1.VisibleColumns[i];
                originalName = col.FieldName;
                if (fixedList != null && fixedList.Contains(originalName.ToUpper()))
                {
                    col.Fixed = Fixed;
                }
            }
            #endregion

            #region 设置特殊内容显示
            object cellValue = "";
            string fieldName = "";
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                for (int j = 0; j < gridView1.Columns.Count; j++)
                {
                    fieldName = gridView1.Columns[j].FieldName;
                    cellValue = gridView1.GetRowCellValue(i, fieldName);
                    if (cellValue != null && cellValue.GetType() == typeof(DateTime))
                    {
                        if (cellValue != DBNull.Value)
                        {
                            DateTime dtTemp = DateTime.MinValue;
                            DateTime.TryParse(cellValue.ToString(), out dtTemp);
                            TimeSpan ts = dtTemp.Subtract(Convert.ToDateTime("1753/1/1"));
                            if (ts.TotalDays < 1)
                            {
                                gridView1.SetRowCellValue(i, fieldName, DBNull.Value);
                            }
                        }
                    }
                }
            } 
            #endregion

            if (this.ShowLineNumber)
            {
                this.gridView1.IndicatorWidth = 40;
            }

            this.gridView1.OptionsView.ColumnAutoWidth = BestFitColumnWith;
            if (BestFitColumnWith)
            {
                this.gridView1.BestFitColumns();
            }
            if (ShowCheckBox)
            {
                GridCheckMarksSelection selection = new GridCheckMarksSelection(gridView1);
                selection.CheckMarkColumn.VisibleIndex = 0;
                selection.CheckMarkColumn.Width = 60;
                selection.SelectionChanged += new SelectionChangedEventHandler(selection_SelectionChanged);
                this.gridView1.OptionsBehavior.Editable = true;
                this.gridView1.OptionsBehavior.ReadOnly = false;
            }
        }

        void selection_SelectionChanged(object sender, EventArgs e)
        {
            if (this.OnCheckBoxSelectionChanged != null)
            {
                this.OnCheckBoxSelectionChanged(sender, e);
            }
        }

        /// <summary>
        /// 获取勾选上的行索引列表
        /// </summary>
        /// <returns></returns>
        public List<int> GetCheckedRows()
        {
            List<int> list = new List<int>();
            if (this.ShowCheckBox)
            {
                for (int rowIndex = 0; rowIndex < this.gridView1.RowCount; rowIndex++)
                {
                    object objValue = this.gridView1.GetRowCellValue(rowIndex, "CheckMarkSelection");
                    if (objValue != null)
                    {
                        bool check = false;
                        bool.TryParse(objValue.ToString(), out check);
                        if (check)
                        {
                            list.Add(rowIndex);
                        }
                    }
                }
            }
            return list;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (OnGridViewMouseClick != null)
            {
                OnGridViewMouseClick(sender, e);
            }
        }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.OnEditSelected != null && this.ShowEditMenu)
            {
                this.OnEditSelected(this.gridView1, new EventArgs());
            }
            else if (OnGridViewMouseDoubleClick != null)
            {
                OnGridViewMouseDoubleClick(this.gridView1, new EventArgs());
            }
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != gridControl1) return;

            ToolTipControlInfo info = null;
            //Get the view at the current mouse position
            GridView view = gridControl1.GetViewAt(e.ControlMousePosition) as GridView;
            if (view == null) return;

            //Get the view's element information that resides at the current position
            GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
            //Display a hint for row indicator cells
            if (hi.HitTest == GridHitTest.RowIndicator)
            {
                //An object that uniquely identifies a row indicator cell
                object o = hi.HitTest.ToString() + hi.RowHandle.ToString();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("行数据基本信息：");
                foreach (GridColumn gridCol in view.Columns)
                {
                    if (gridCol.Visible)
                    {
                        sb.AppendFormat("    {0}：{1}\r\n", gridCol.Caption, view.GetRowCellDisplayText(hi.RowHandle, gridCol.FieldName));
                    }
                }
                info = new ToolTipControlInfo(o, sb.ToString());
            }

            //Supply tooltip information if applicable, otherwise preserve default tooltip (if any)
            if (info != null)
            {
                e.Info = info;
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (ShowLineNumber)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle >= 0)
                    {
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                }
            }
        }

        private void WinGridView_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.contextMenuStrip1.Opening += new CancelEventHandler(contextMenuStrip1_Opening);
                this.gridControl1.MouseClick += new MouseEventHandler(dataGridView1_MouseClick);
                this.gridControl1.MouseDoubleClick += new MouseEventHandler(dataGridView1_MouseDoubleClick);

                this.gridView1.Appearance.EvenRow.BackColor = EventRowBackColor;
            }
        }

        private void menu_ColumnWidth_Click(object sender, EventArgs e)
        {
            this.BestFitColumnWith = !this.BestFitColumnWith;
            ShowWidthStatus();
            if (OnRefresh != null)
            {
                OnRefresh(sender, e);
            }
        }

        private void ShowWidthStatus()
        {
            if (this.BestFitColumnWith)
            {
                this.menu_ColumnWidth.Text = "设置列固定宽度(&W)";
            }
            else
            {
                this.menu_ColumnWidth.Text = "设置列自动适应宽度(&W)";
            }
        }

    }
}
