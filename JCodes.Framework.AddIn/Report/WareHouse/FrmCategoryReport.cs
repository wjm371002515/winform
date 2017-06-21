using System;
using System.IO;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.Utils;
using Aspose.Cells;
using Range = Aspose.Cells.Range;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.AddIn.Other;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Files;

namespace JCodes.Framework.AddIn.Report.WareHouse
{
    public partial class FrmCategoryReport : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 报表标题
        /// </summary>
        public string ReportTitle = "报表";
        public string FieldName = "";

        public FrmCategoryReport()
        {
            InitializeComponent();

            this.gridControl1.DataSourceChanged += new EventHandler(gridControl1_DataSourceChanged);

            //关联回车键进行查询
            foreach (Control control in this.layoutControl1.Controls)
            {
                control.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            }
        }

        /// <summary>
        /// 提供给控件回车执行查询的操作
        /// </summary>
        private void SearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, null);
            }
        }

        void gridControl1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.gridView1.Columns.Count > 0)
            {
                this.gridView1.Columns[0].Caption = "统计项目";
                this.gridView1.Columns[1].Caption = "统计值";
            }
        }

        /// <summary>
        /// 根据查询条件构造查询语句
        /// </summary> 
        private string GetConditionSql()
        {
            SearchCondition condition = new SearchCondition();

            condition.AddCondition("WareHouse", txtWareHouse.Text, SqlOperator.Like);
            condition.AddCondition("Dept", txtDept.Text, SqlOperator.Like);

            string where = condition.BuildConditionSql().Replace("Where", "");
            return where;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 创建图表的Series对象，可以指定相应的类型
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="viewType">图表类型，如DevExpress.XtraCharts.ViewType.Pie3D，DevExpress.XtraCharts.ViewType.Bar</param>
        /// <param name="format">显示格式，如百分比NumericFormat.Percent，NumericFormat.General</param>
        /// <returns></returns>
        private Series CreateSeries(DataTable dt, DevExpress.XtraCharts.ViewType viewType, NumericFormat format)
        {
            Series series = new Series("Serices1 ", viewType);
            series.DataSource = dt;
            series.ArgumentScaleType = ScaleType.Qualitative;
            series.ArgumentDataMember = "argument";
            series.ValueScaleType = ScaleType.Numerical;
            series.ValueDataMembers.AddRange(new string[] { "datavalue" });
            series.PointOptions.PointView = PointView.ArgumentAndValues;
            series.PointOptions.ValueNumericOptions.Format = format;
            if (format == NumericFormat.Number)
            {
                //series.PointOptions.ValueNumericOptions.Precision = 0;
            }

            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签

            return series;
        }

        private void InitDictItem()
        {
            this.txtWareHouse.Properties.Items.Clear();
            this.txtWareHouse.Properties.Items.AddRange(BLLFactory<WareHouses>.Instance.GetAllWareHouse().ToArray());

            //this.txtDept.BindDictItems("部门");
        }

        private DataTable dt = null;
        private void BindData()
        {
            if (string.IsNullOrEmpty(FieldName)) return;

            //设置报表标题
            this.Text = ReportTitle;
            this.lblReportTitle.Text = ReportTitle;

            this.chartPie.Series.Clear();
            this.chartBar.Series.Clear();

            string where = GetConditionSql();
            dt = BLLFactory<ItemDetail>.Instance.GetReportData(FieldName, where);

            this.gridControl1.DataSource = dt;

            if (dt != null && dt.Rows.Count > 0)
            {
                this.chartPie.DataSource = dt;

                Series pieSeries = CreateSeries(dt, DevExpress.XtraCharts.ViewType.Pie3D, NumericFormat.Percent);
                chartPie.Series.Add(pieSeries);
                chartPie.Legend.Visible = true;

                PieSeriesLabel label = pieSeries.Label as PieSeriesLabel;
                ((PiePointOptions)label.PointOptions).PercentOptions.PercentageAccuracy = 4;
                ((PiePointOptions)label.PointOptions).PercentOptions.ValueAsPercent = true;

                label.Position = PieSeriesLabelPosition.TwoColumns; //设置饼图上lable的显示方式，此方式将独立出一个列显示lable
                (pieSeries.View as DevExpress.XtraCharts.Pie3DSeriesView).ExplodeMode = PieExplodeMode.All; //突出显示饼块
                (pieSeries.View as DevExpress.XtraCharts.Pie3DSeriesView).ExplodedDistancePercentage = 5;
                //(pieSeries.View as DevExpress.XtraCharts.PieSeriesView).RuntimeExploding = true; //设置了他，你就可以把你喜欢的饼块拖出来。。。

                this.chartBar.DataSource = dt;
                chartBar.Series.Add(CreateSeries(dt, DevExpress.XtraCharts.ViewType.Bar, NumericFormat.General));
                chartBar.Legend.Visible = false;
                chartBar.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
            }

            this.xtraTabControl1.SelectedTabPageIndex = 0;
        }

        private void FrmCategoryReport_Load(object sender, EventArgs e)
        {
            InitDictItem();
            BindData();
        }

        private void FrmCategoryReport_SizeChanged(object sender, EventArgs e)
        {
            this.lblReportTitle.Location = new Point(this.Size.Width / 2, this.lblReportTitle.Location.Y);
        }

        private Style CreateStyle(Workbook workbook, bool isHeader)
        {
            int styleIndex = workbook.Styles.Add();
            Style style = workbook.Styles[styleIndex];
            style.Pattern = BackgroundType.Solid;
            if (isHeader)
            {
                style.ForegroundColor = Color.Yellow;
                style.Font.IsBold = true;
                style.Font.Size = 10;
            }
            else
            {
                style.Font.Size = 9;
            }

            style.HorizontalAlignment = TextAlignmentType.Center;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin; //应用边界线 左边界线 
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin; //应用边界线 右边界线 
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin; //应用边界线 上边界线 
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin; //应用边界线 下边界线 
            return style;
        }

        private Style CreateTitleStyle(Workbook workbook)
        {
            int styleIndex = workbook.Styles.Add();
            Style style = workbook.Styles[styleIndex];
            style.Pattern = BackgroundType.Solid;
            style.Font.IsBold = true;
            style.Font.Size = 12;
            style.HorizontalAlignment = TextAlignmentType.Center;
            return style;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageDxUtil.ShowTips("没有数据需要导出！");
                    return;
                }

                string saveDocFile = FileDialogHelper.SaveExcel(string.Format("{0}.xls", ReportTitle), "C:\\");
                if (!string.IsNullOrEmpty(saveDocFile))
                {
                    Workbook workbook = new Workbook();
                    Worksheet worksheet = workbook.Worksheets[0];
                    worksheet.PageSetup.Orientation = PageOrientationType.Landscape;//横向打印
                    worksheet.PageSetup.Zoom = 100;//以100%的缩放模式打开
                    worksheet.PageSetup.PaperSize = PaperSizeType.PaperA4;

                    #region 表头及说明信息
                    Range range; Cell cell; string content;
                    int colSpan = 10;
                    range = worksheet.Cells.CreateRange(0, 0, 1, colSpan);
                    range.Merge();
                    range.RowHeight = 20;
                    range.SetStyle(CreateTitleStyle(workbook));
                    cell = range[0, 0];
                    cell.PutValue(ReportTitle);

                    range = worksheet.Cells.CreateRange(1, 0, 1, colSpan);
                    range.Merge();
                    range.RowHeight = 15;
                    cell = range[0, 0];
                    content = string.Format("所选查询条件内，总计有{0}个统计项，详细列表如下：", dt.Rows.Count);
                    cell.PutValue(content);

                    #endregion

                    #region 生成报表头部表格
                    Style headStyle = CreateStyle(workbook, true);
                    Style normalStyle = CreateStyle(workbook, false);
                    int startRow = 2;
                    int startCol = 0;
                    range = worksheet.Cells.CreateRange(startRow, 0, 2, 1);
                    range.Merge();
                    range.SetStyle(headStyle);
                    cell = range[0, 0];
                    cell.PutValue("序号");
                    cell.SetStyle(headStyle);

                    range = worksheet.Cells.CreateRange(startRow, 1, 2, 1);
                    range.Merge();
                    range.SetStyle(headStyle);
                    range.ColumnWidth = 40;
                    cell = range[0, 0];
                    cell.PutValue("统计项目");
                    cell.SetStyle(headStyle);

                    range = worksheet.Cells.CreateRange(startRow, 2, 2, 1);
                    range.Merge();
                    range.SetStyle(headStyle);
                    range.ColumnWidth = 40;
                    cell = range[0, 0];
                    cell.PutValue("统计值");
                    cell.SetStyle(headStyle);

                    #endregion

                    //写入数据到Excel
                    startRow = startRow + 2;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //添加序号
                        cell = worksheet.Cells[startRow, 0];
                        cell.PutValue(i + 1);
                        cell.SetStyle(normalStyle);

                        startCol = 1;
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            DataRow dr = dt.Rows[i];
                            cell = worksheet.Cells[startRow, startCol];
                            cell.PutValue(dr[j]);
                            cell.SetStyle(normalStyle);

                            startCol++;
                        }
                        startRow++;
                    }

                    //写入图注
                    startRow += 1;//跳过1行
                    range = worksheet.Cells.CreateRange(startRow++, 0, 1, colSpan);
                    range.Merge();
                    range.RowHeight = 15;
                    cell = range[0, 0];
                    cell.PutValue("以饼图展示如下：");

                    //插入图片到Excel里面
                    using (MemoryStream stream = new MemoryStream())
                    {
                        stream.Position = 0;
                        ChartControl chart = (ChartControl)chartPie.Clone();
                        chart.Size = new Size(600, 400);

                        chart.ExportToImage(stream, ImageFormat.Jpeg);
                        worksheet.Pictures.Add(startRow, 0, stream);
                    }

                    //写入图注
                    startRow += 25;//跳过20行
                    range = worksheet.Cells.CreateRange(startRow++, 0, 1, colSpan);
                    range.Merge();
                    range.RowHeight = 15;
                    cell = range[0, 0];
                    cell.PutValue("以柱状图展示如下：");

                    //插入图片到Excel里面
                    using (MemoryStream stream = new MemoryStream())
                    {
                        stream.Position = 0;
                        ChartControl chart = (ChartControl)chartBar.Clone();
                        chart.Size = new Size(800, 300);

                        chart.ExportToImage(stream, ImageFormat.Jpeg);
                        worksheet.Pictures.Add(startRow, 0, stream);
                    }

                    workbook.Save(saveDocFile);
                    if (MessageDxUtil.ShowYesNoAndTips("保存成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDocFile);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmCategoryReport));
                MessageDxUtil.ShowError(ex.Message);
                return;
            }
        }

        private Image ExportChartImage(ChartControl chart)
        {
            MemoryStream stream = new MemoryStream();
            chartPie.ExportToImage(stream, ImageFormat.Jpeg);
            Image image = Image.FromStream(stream);
            stream.Close();

            return image;
        }

        private void chkHidPieLable_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chartPie.Series[0] != null)
            {
                this.chartPie.Series[0].LabelsVisibility = chkHidPieLable.Checked ? DefaultBoolean.False : DefaultBoolean.True;
            }
        }

        private void chkHideLegend_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chartPie.Series[0] != null)
            {
                this.chartPie.Series[0].ShowInLegend = !chkHideLegend.Checked;
            }
        }
    }
}
