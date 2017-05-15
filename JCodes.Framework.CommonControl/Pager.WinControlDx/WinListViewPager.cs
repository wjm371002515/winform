using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Diagnostics;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;

namespace JCodes.Framework.CommonControl
{
    public partial class WinListViewPager : DevExpress.XtraEditors.XtraUserControl
    {
        private DataTable dataSource = new DataTable();
        private PagerInfo pagerInfo = null;
        //private Export2Excel export2XLS = new Export2Excel();
        private SaveFileDialog saveFileDialog = new SaveFileDialog();
        private bool isExportAllPage = false;

        public DataTable TableToExport = new DataTable();
        public ProgressBar ProgressBar;
        public event EventHandler OnStartExport;
        public event EventHandler OnEndExport;
        public event EventHandler OnPageChanged;
        public event EventHandler OnDeleteSelected;
        public event EventHandler OnRefresh;

        public WinListViewPager()
        {
            InitializeComponent();

            this.pager.PageChanged += new PageChangedEventHandler(pager_PageChanged);
        }

        private void pager_PageChanged(object sender, EventArgs e)
        {
            if (OnPageChanged != null)
            {
                OnPageChanged(this, new EventArgs());
            }
        }
        
        public DataTable DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                this.listView1.Columns.Clear();
                foreach (DataColumn col in value.Columns)
                {
                    this.listView1.Columns.Add(col.ColumnName, 120);
                }

                ListViewItem item;
                this.listView1.Items.Clear();
                foreach (DataRow row in value.Rows)
                {
                    item = new ListViewItem();
                    item.SubItems.Clear();

                    item.SubItems[0].Text = row[0].ToString();
                    for (int i = 1; i < value.Columns.Count; i++)
                    {
                        item.SubItems.Add(row[i].ToString());
                    }
                    this.listView1.Items.Add(item);
                }

                this.pager.InitPageInfo(PagerInfo.RecordCount);
            }
        }

        public PagerInfo PagerInfo
        {
            get
            {
                if (pagerInfo == null)
                {
                    pagerInfo = new PagerInfo();
                    pagerInfo.RecordCount = this.pager.RecordCount;
                    pagerInfo.CurrenetPageIndex = this.pager.CurrentPageIndex;
                    pagerInfo.PageSize = this.pager.PageSize;
                }
                else
                {
                    pagerInfo.CurrenetPageIndex = this.pager.CurrentPageIndex;
                }

                return pagerInfo;
            }
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
                        MessageBox.Show("文件格式不正确");
                    }
                }
                else
                {
                    MessageBox.Show("需要指定一个保存的目录");
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

        //do the new excel document work using the background worker
        private void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dtExport = this.dataSource;
            if (TableToExport != null && isExportAllPage)
            {
                dtExport = TableToExport;
            }

            string outError = "";
            AsposeExcelTools.DataTableToExcel2(dtExport, (String)e.Argument, out outError);
        }

        //show a message to the user when the background worker has finished
        //and re-enable the export buttons
        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (OnEndExport != null)
            {
                OnEndExport(this, new EventArgs());
            }

            if (MessageBox.Show("导出操作完成, 您想打开该Excel文件么?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Process.Start(saveFileDialog.FileName);
            }
        }
                
        #endregion

        private void menu_Delete_Click(object sender, EventArgs e)
        {
            if (OnDeleteSelected != null)
            {
                OnDeleteSelected(this.listView1, new EventArgs());
            }
        }

        private void menu_Refresh_Click(object sender, EventArgs e)
        {
            if (this.OnRefresh != null)
            {
                OnRefresh(this.listView1, new EventArgs());
            }
        }

        private void menu_Print_Click(object sender, EventArgs e)
        {

        }

        private void WinListViewPager_Load(object sender, EventArgs e)
        {
            if (this.ContextMenuStrip == null)
            {
                this.ContextMenuStrip = new ContextMenuStrip();
            }

            for (int i = 0; i < this.contextMenuStrip1.Items.Count; i++)
            {
                ToolStripItem item = this.contextMenuStrip1.Items[i];
                this.ContextMenuStrip.Items.Add(item);
            }
        }
    }
}
