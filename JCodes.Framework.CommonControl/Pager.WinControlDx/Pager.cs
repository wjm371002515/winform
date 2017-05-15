using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using JCodes.Framework.Entity;

namespace JCodes.Framework.CommonControl
{
    public delegate void PageChangedEventHandler(object sender, EventArgs e);
    public delegate void ExportCurrentEventHandler(object sender, EventArgs e);
    public delegate void ExportAllEventHandler(object sender, EventArgs e);

    /// <summary>
    /// 分页工具条用户控件，仅提供分页信息显示及改变页码操作
    /// </summary>
    public class Pager : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 页面切换的时候触发的时间
        /// </summary>
        public event PageChangedEventHandler PageChanged;
        public event ExportCurrentEventHandler ExportCurrent;
        public event ExportAllEventHandler ExportAll;

        private int m_PageSize;
        private int m_PageCount;
        private int m_RecordCount;
        private int m_CurrentPageIndex;
        private bool m_ShowExportButton = true;//是否显示导出按钮

        private LabelControl lblPageInfo;
        private TextEdit txtCurrentPage;
        private SimpleButton btnFirst;
        private SimpleButton btnPrevious;
        private SimpleButton btnNext;
        private SimpleButton btnLast;
        private SimpleButton btnExport;
        private SimpleButton btnExportCurrent;

        private PagerInfo pagerInfo = null;

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagerInfo PagerInfo
        {
            get
            {
                if (pagerInfo == null)
                {
                    pagerInfo = new PagerInfo();
                    pagerInfo.RecordCount = this.RecordCount;
                    pagerInfo.CurrenetPageIndex = this.CurrentPageIndex;
                    pagerInfo.PageSize = this.PageSize;

                    pagerInfo.OnPageInfoChanged += new PageInfoChanged(pagerInfo_OnPageInfoChanged);
                }
                else
                {
                    pagerInfo.CurrenetPageIndex = this.CurrentPageIndex;
                }

                return pagerInfo;
            }
        }

        void pagerInfo_OnPageInfoChanged(PagerInfo info)
        {
            this.RecordCount = info.RecordCount;
            this.CurrentPageIndex = info.CurrenetPageIndex;
            this.PageSize = info.PageSize;

            this.InitPageInfo();
        }

        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;
        
        /// <summary> 
        /// 默认构造函数，设置分页初始信息
        /// </summary>
        public Pager()
        {
            InitializeComponent();

            this.m_PageSize = 50;
            this.m_RecordCount = 0;
            this.m_CurrentPageIndex = 1; //默认为第一页
            this.InitPageInfo();
        }

        /// <summary> 
        /// 带参数的构造函数
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// </summary>
        public Pager(int recordCount, int pageSize)
        {
            InitializeComponent();

            this.m_PageSize = pageSize;
            this.m_RecordCount = recordCount;
            this.m_CurrentPageIndex = 1; //默认为第一页
            this.InitPageInfo();
        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
       
        #region 组件设计器生成的代码
        /// <summary> 
        /// 设计器支持所需的方法 - 不要使用代码编辑器 
        /// 修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPageInfo = new DevExpress.XtraEditors.LabelControl();
            this.txtCurrentPage = new DevExpress.XtraEditors.TextEdit();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrevious = new DevExpress.XtraEditors.SimpleButton();
            this.btnLast = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportCurrent = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentPage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Location = new System.Drawing.Point(28, 10);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(213, 14);
            this.lblPageInfo.TabIndex = 0;
            this.lblPageInfo.Text = "共 {0} 条记录，每页 {1} 条，共 {2} 页";
            // 
            // txtCurrentPage
            // 
            this.txtCurrentPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentPage.EditValue = "1";
            this.txtCurrentPage.Location = new System.Drawing.Point(323, 6);
            this.txtCurrentPage.Name = "txtCurrentPage";
            this.txtCurrentPage.Size = new System.Drawing.Size(25, 20);
            this.txtCurrentPage.TabIndex = 5;
            this.txtCurrentPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurrentPage_KeyDown);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(351, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(30, 20);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = ">";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFirst.Location = new System.Drawing.Point(259, 6);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(30, 20);
            this.btnFirst.TabIndex = 7;
            this.btnFirst.Text = "|<";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.Location = new System.Drawing.Point(291, 6);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(30, 20);
            this.btnPrevious.TabIndex = 8;
            this.btnPrevious.Text = "<";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnLast
            // 
            this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLast.Location = new System.Drawing.Point(383, 6);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(30, 20);
            this.btnLast.TabIndex = 9;
            this.btnLast.Text = ">|";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(507, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(74, 23);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "导出全部页";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnExportCurrent
            // 
            this.btnExportCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportCurrent.Location = new System.Drawing.Point(427, 4);
            this.btnExportCurrent.Name = "btnExportCurrent";
            this.btnExportCurrent.Size = new System.Drawing.Size(74, 23);
            this.btnExportCurrent.TabIndex = 10;
            this.btnExportCurrent.Text = "导出当前页";
            this.btnExportCurrent.Click += new System.EventHandler(this.btnExportCurrent_Click);
            // 
            // Pager
            // 
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnExportCurrent);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.txtCurrentPage);
            this.Controls.Add(this.lblPageInfo);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "Pager";
            this.Size = new System.Drawing.Size(606, 32);
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentPage.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// 页面变化处理
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPageChanged(EventArgs e)
        {
            if (PageChanged != null)
            {
                PageChanged(this, e);
            }
        }

        /// <summary>
        /// 是否显示导出按钮
        /// </summary>
        [Description("是否显示导出按钮。"), DefaultValue(true), Category("分页")]
        public bool ShowExportButton
        {
            get { return m_ShowExportButton; }
            set
            {
                m_ShowExportButton = value;
                this.btnExport.Visible = value;
                this.btnExportCurrent.Visible = value;
            }
        }

        /// <summary>
        /// 设置或获取一页中显示的记录数目
        /// </summary>
        [Description("设置或获取一页中显示的记录数目"), DefaultValue(50), Category("分页")]
        public int PageSize
        {
            set
            {
                this.m_PageSize = value;
            }
            get
            {
                return this.m_PageSize;
            }
        }
        
        /// <summary>
        /// 获取记录总页数
        /// </summary>
        [Description("获取记录总页数"), DefaultValue(0), Category("分页")]
        public int PageCount
        {
            get
            {
                return this.m_PageCount;
            }
        }

        /// <summary>
        /// 设置或获取记录总数
        /// </summary>
        [Description("设置或获取记录总数"), Category("分页")]
        public int RecordCount
        {
            set
            {
                this.m_RecordCount = value;
            }
            get
            {
                return this.m_RecordCount;
            }
        }
        
        /// <summary>
        /// 当前的页面索引, 开始为1
        /// </summary>
        [Description("当前的页面索引, 开始为1"), DefaultValue(0), Category("分页")]
        [Browsable(false)]
        public int CurrentPageIndex
        {
            set
            {
                this.m_CurrentPageIndex = value;
            }
            get
            {
                return this.m_CurrentPageIndex;
            }
        }

        /// <summary>
        /// 初始化分页信息
        /// </summary>
        /// <param name="info"></param>
        public void InitPageInfo(PagerInfo info)
        {
            this.m_RecordCount = info.RecordCount;
            this.m_PageSize = info.PageSize;
            this.InitPageInfo();
        }
        
        /// <summary> 
        /// 初始化分页信息
        /// <param name="pageSize">每页记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// </summary>
        public void InitPageInfo(int recordCount, int pageSize)
        {
            this.m_RecordCount = recordCount;
            this.m_PageSize = pageSize;
            this.InitPageInfo();
        }
        
        /// <summary> 
        /// 初始化分页信息
        /// <param name="recordCount">总记录数</param>
        /// </summary>
        public void InitPageInfo(int recordCount)
        {
            this.m_RecordCount = recordCount;
            this.InitPageInfo();
        }
        
        /// <summary> 
        /// 初始化分页信息
        /// </summary>
        public void InitPageInfo()
        {
            if (this.m_PageSize < 1)
                this.m_PageSize = 10; //如果每页记录数不正确，即更改为10
            if (this.m_RecordCount < 0)
                this.m_RecordCount = 0; //如果记录总数不正确，即更改为0

            //取得总页数
            if (this.m_RecordCount % this.m_PageSize == 0)
            {
                this.m_PageCount = this.m_RecordCount / this.m_PageSize;
            }
            else
            {
                this.m_PageCount = this.m_RecordCount / this.m_PageSize + 1;
            }

            //设置当前页
            if (this.m_CurrentPageIndex > this.m_PageCount)
            {
                this.m_CurrentPageIndex = this.m_PageCount;
            }
            if (this.m_CurrentPageIndex < 1)
            {
                this.m_CurrentPageIndex = 1;
            }

            //设置按钮的可用性
            bool enable = (this.CurrentPageIndex > 1);
            this.btnPrevious.Enabled = enable;

            enable = (this.CurrentPageIndex < this.PageCount);
            this.btnNext.Enabled = enable;

            this.txtCurrentPage.Text = this.m_CurrentPageIndex.ToString();
            this.lblPageInfo.Text = string.Format("共 {0} 条记录，每页 {1} 条，共 {2} 页", this.m_RecordCount, this.m_PageSize, this.m_PageCount);

             this.btnExport.Enabled = this.ShowExportButton;
             this.btnExportCurrent.Enabled = this.ShowExportButton;
        }

        /// <summary>
        /// 刷新页面数据
        /// </summary>
        /// <param name="page">页码</param>
        public void RefreshData(int page)
        {
            this.m_CurrentPageIndex = page;
            EventArgs e = new EventArgs();
            OnPageChanged(e);
        }
        
        private void btnFirst_Click(object sender, System.EventArgs e)
        {
            this.RefreshData(1);
        }
        
        private void btnPrevious_Click(object sender, System.EventArgs e)
        {
            if (this.m_CurrentPageIndex > 1)
            {
                this.RefreshData(this.m_CurrentPageIndex - 1);
            }
            else
            {
                this.RefreshData(1);
            }
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            if (this.m_CurrentPageIndex < this.m_PageCount)
            {
                this.RefreshData(this.m_CurrentPageIndex + 1);
            }
            else if (this.m_PageCount < 1)
            {
                this.RefreshData(1);
            }
            else
            {
                this.RefreshData(this.m_PageCount);
            }
        }
        
        private void btnLast_Click(object sender, System.EventArgs e)
        {
            if (this.m_PageCount > 0)
            {
                this.RefreshData(this.m_PageCount);
            }
            else
            {
                this.RefreshData(1);
            }
        }
        
        private void txtCurrentPage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int num;
                try
                {
                    num = Convert.ToInt16(this.txtCurrentPage.Text);
                }
                catch (Exception ex)
                {
                    num = 1;
                }

                if (num > this.m_PageCount)
                    num = this.m_PageCount;
                if (num < 1)
                    num = 1;

                this.RefreshData(num);
            }
        }

        private void btnExportCurrent_Click(object sender, EventArgs e)
        {
            if (ExportCurrent != null)
            {
                ExportCurrent(sender, e);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (ExportAll != null)
            {
                ExportAll(sender, e);
            }
        }
    }
}

