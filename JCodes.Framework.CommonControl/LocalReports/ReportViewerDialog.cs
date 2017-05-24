using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using JCodes.Framework.CommonControl.BaseUI;

namespace JCodes.Framework.CommonControl.LocalReports
{
    /// <summary>
    /// 通用的RDLC报表的查看器
    /// </summary>
    public partial class ReportViewerDialog : BaseForm
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ReportViewerDialog()
        {
            InitializeComponent();

            this.IsPrintLayout = true;//默认使用打印布局
        }

        private void ReportViewerDialog_Load(object sender, EventArgs e)
        {
            //是否为打印布局
            if (IsPrintLayout)
            {
                this.rpViewer.SetDisplayMode(DisplayMode.PrintLayout);
                this.rpViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.rpViewer.ZoomPercent = 100;
            }

            LocalReport report = this.rpViewer.LocalReport;
            if (string.IsNullOrEmpty(ReportFilePath))
            {
                ReportHelper.LoadReport(report, ReportName, _dataSourceDict, _parameters);
            }
            else
            {
                ReportHelper.LoadReportWithPath(report, ReportFilePath, _dataSourceDict, _parameters);
            }
            this.rpViewer.RefreshReport();
        }

        #region 私有字段
        private Dictionary<string, object> _dataSourceDict;//报表和数据源映射关系
        private NameValueCollection _parameters;//报表参数
        #endregion

        #region 公有属性

        /// <summary>
        /// 报表名称（默认在Resource/Report目录下）
        /// </summary>
        public string ReportName { get; set; }

        /// <summary>
        /// 指定报表的相对路径（包括rdlc后缀名的相对路径），如果指定该属性，则ReportName不用指定
        /// </summary>
        public string ReportFilePath { get; set; }

        /// <summary>
        /// 报表和数据源映射关系：报表对应表名称和绑定数据源的表名映射关系。
        /// </summary>
        public Dictionary<string, object> DataSourceDict
        {
            get
            {
                if (_dataSourceDict == null)
                {
                    _dataSourceDict = new Dictionary<string, object>();
                }
                return _dataSourceDict;
            }
        }

        /// <summary>
        /// 报表参数的名称和值的集合
        /// </summary>
        public NameValueCollection Parameters
        {
            get
            {
                if (_parameters == null)
                {
                    _parameters = new NameValueCollection();
                }
                return _parameters;
            }
        }

        /// <summary>
        /// 是否设置为打印布局
        /// </summary>
        public bool IsPrintLayout { get; set; }

        #endregion
    }
}
