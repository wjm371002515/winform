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
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.AddIn.Report.WareHouse
{
    public partial class FrmLineHistoryReport : DevExpress.XtraEditors.XtraUserControl
    {        
        /// <summary>
        /// 报表标题
        /// </summary>
        public string ReportTitle = "报表";

        public FrmLineHistoryReport()
        {
            InitializeComponent();

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

        List<string> wareHousetList = new List<string>();
        List<string> yearList = new List<string>();
        private void FrmLineHistoryReport_Load(object sender, EventArgs e)
        {
            //库房列表
            wareHousetList = BLLFactory<PurchaseHeader>.Instance.GetFieldList("WareHouse");

            this.txtWareHouse.Properties.Items.Clear();
            this.txtWareHouse.Properties.Items.AddRange(wareHousetList.ToArray());
            this.txtWareHouse.Properties.Items.Insert(0, "");

            //年份列表
            yearList = BLLFactory<PurchaseHeader>.Instance.GetYearList("CreateDate");
            this.txtYear.Properties.Items.Clear();
            this.txtYear.Properties.Items.AddRange(yearList.ToArray());

            BindData();
        }

        private void BindData()
        {
            if (this.txtYear.Text.Length > 0 && ValidateUtil.IsNumber(this.txtYear.Text))
            {
                DataTable dt = CreateData();
                this.gridControl1.DataSource = dt;
                this.gridView1.PopulateColumns();

                //设置报表标题
                this.Text = ReportTitle;
                this.lblReportTitle.Text = ReportTitle;
                 
                CreateChart(dt);
            }
        }

        /// <summary>
        /// 准备数据内容
        /// </summary>
        /// <returns></returns>
        private DataTable CreateData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("类型"));

            List<int> totalInList = new List<int>();
            List<int> totalOutList = new List<int>();            
            for (int i = 1; i <= 12; i++)
            {
                dt.Columns.Add(new DataColumn(string.Format("{0}-{1}月", this.txtYear.Text, i), typeof(decimal)));

                DateTime dtStart = Convert.ToDateTime(string.Format("{0}-{1}-1", this.txtYear.Text, i));
                DateTime dtEnd = dtStart.AddMonths(1);

                SearchCondition condition = new SearchCondition();
                condition.AddCondition("h.WareHouse", this.txtWareHouse.Text, SqlOperator.Like);//增加h为指定特定的表
                condition.AddCondition("CreateDate", dtStart, SqlOperator.MoreThanOrEqual);
                condition.AddCondition("CreateDate", dtEnd, SqlOperator.LessThan);
                //入库数量
                int totalIn = BLLFactory<PurchaseHeader>.Instance.GetPurchaseQuantity(condition.BuildConditionSql().Replace("Where", ""), true);
                totalInList.Add(totalIn);

                condition = new SearchCondition();
                condition.AddCondition("h.WareHouse", this.txtWareHouse.Text, SqlOperator.Like);//增加h为指定特定的表
                condition.AddCondition("CreateDate", dtStart, SqlOperator.MoreThanOrEqual);
                condition.AddCondition("CreateDate", dtEnd, SqlOperator.LessThan);
                //出库数量
                int totalOut = BLLFactory<PurchaseHeader>.Instance.GetPurchaseQuantity(condition.BuildConditionSql().Replace("Where", ""), false);
                totalOutList.Add(totalOut);
            }

            DataRow dr = dt.NewRow();
            dr[0] = "入库数量";
            int j=1;
            foreach(int count in totalInList)
            {
                dr[j++] = count;
            }
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "出库数量";
            j = 1;
            foreach (int count in totalOutList)
            {
                dr[j++] = count;
            }
            dt.Rows.Add(dr);

            return dt;
        }

        //设置几个颜色，用于统一图例线条颜色和Y坐标颜色
        List<Color> colorList = new List<Color>() { Color.Red, Color.Blue, Color.Orange, Color.DarkGreen, Color.Black, Color.Pink };

        /// <summary>
        /// 根据数据创建一个图形展现
        /// </summary>
        /// <param name="caption">图形标题</param>
        /// <param name="viewType">图形类型</param>
        /// <param name="dt">数据DataTable</param>
        /// <param name="rowIndex">图形数据的行序号</param>
        /// <returns></returns>
        private Series CreateSeries(string caption, DevExpress.XtraCharts.ViewType viewType, DataTable dt, int rowIndex)
        {
            Series series = new Series(caption, viewType);
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                string argument = dt.Columns[i].ColumnName;//参数名称
                decimal value = (decimal)dt.Rows[rowIndex][i];//参数值
                series.Points.Add(new SeriesPoint(argument, value));
            }

            //必须设置ArgumentScaleType的类型，否则显示会转换为日期格式，导致不是希望的格式显示
            //也就是说，显示字符串的参数，必须设置类型为ScaleType.Qualitative
            series.ArgumentScaleType = ScaleType.Qualitative;
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;//显示标注标签

            return series;
        }

        private void CreateChart(DataTable dt)
        {
            //this.chartControl1.Series.Clear();
            //chartControl1.Titles.Clear();//清理标题
            //if (chartControl1.Diagram != null)
            //{
            //    XYDiagram xyDiagram = ((XYDiagram)chartControl1.Diagram);
            //    if (xyDiagram != null)
            //    {
            //        xyDiagram.SecondaryAxesY.Clear();
            //    }
            //}
            this.panelControl1.Controls.Clear();
            this.chartControl1 = new ChartControl();
            this.chartControl1.Dock = DockStyle.Fill;
            this.panelControl1.Controls.Add(this.chartControl1);

            #region Series
            List<Series> list = new List<Series>();
            //创建几个图形的对象
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Series series1 = CreateSeries(dt.Rows[i][0].ToString(), DevExpress.XtraCharts.ViewType.Line, dt, i);
                list.Add(series1);
            }
            #endregion

            chartControl1.Series.AddRange(list.ToArray());
            chartControl1.Legend.Visible = false;
            chartControl1.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;

            for (int i = 0; i < list.Count; i++)
            {
                list[i].View.Color = colorList[i];

                CreateAxisY(list[i]);
            }
        }

        /// <summary>
        /// 创建图表的第二坐标系
        /// </summary>
        /// <param name="series">Series对象</param>
        /// <returns></returns>
        private SecondaryAxisY CreateAxisY(Series series)
        {            
            SecondaryAxisY myAxis = new SecondaryAxisY(series.Name);
            ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Add(myAxis);
            ((LineSeriesView)series.View).AxisY = myAxis;
            myAxis.Title.Text = series.Name;
            myAxis.Title.Alignment = StringAlignment.Far; //顶部对齐
            myAxis.Title.Visible = true; //显示标题m
            myAxis.Title.Font = new System.Drawing.Font("宋体", 9.0f);

            Color color = series.View.Color;//设置坐标的颜色和图表线条颜色一致

            myAxis.Title.TextColor = color;
            myAxis.Label.TextColor = color;
            myAxis.Color = color;

            return myAxis;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //this.chartControl1.ShowPrintPreview(DevExpress.XtraCharts.Printing.PrintSizeMode.Zoom);
            DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink();
            DevExpress.XtraPrinting.PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();

            compositeLink.PrintingSystem = ps;
            compositeLink.Landscape = true;
            compositeLink.PaperKind = System.Drawing.Printing.PaperKind.A4;

            DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(ps);
            ps.PageSettings.Landscape = true;
            link.Component = this.chartControl1;
            compositeLink.Links.Add(link);

            link.CreateDocument();  //建立文档
            ps.PreviewFormEx.Show();//进行预览
        }

        private void FrmLineHistoryReport_SizeChanged(object sender, EventArgs e)
        {
            this.lblReportTitle.Location = new Point(this.Size.Width / 2, this.lblReportTitle.Location.Y);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            BindData();
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
                DataTable dt = this.gridControl1.DataSource as DataTable;
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
                    int colSpan = 3;
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
                    content = string.Format("统计报表详细列表如下：");
                    cell.PutValue(content);

                    #endregion

                    #region 生成报表头部表格
                    Style headStyle = CreateStyle(workbook, true);
                    Style normalStyle = CreateStyle(workbook, false);
                    int startRow = 2;
                    int startCol = 0;

                    int index = 0;
                    foreach (DataColumn col in dt.Columns)
                    {
                        range = worksheet.Cells.CreateRange(startRow, index, 2, 1);
                        range.Merge();
                        range.SetStyle(headStyle);
                        cell = range[0, 0];
                        cell.PutValue(col.ColumnName);
                        cell.SetStyle(headStyle);
                        index++;
                    }

                    #endregion

                    //写入数据到Excel
                    startRow = startRow + 2;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        startCol = 0;
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
                    cell.PutValue("以曲线图展示如下：");

                    //插入图片到Excel里面
                    using (MemoryStream stream = new MemoryStream())
                    {
                        stream.Position = 0;
                        ChartControl chart = (ChartControl)chartControl1.Clone();
                        chart.Size = new Size(800, 400);

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
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmLineHistoryReport));
                MessageDxUtil.ShowError(ex.Message);
                return;
            }
        }
    }
}
