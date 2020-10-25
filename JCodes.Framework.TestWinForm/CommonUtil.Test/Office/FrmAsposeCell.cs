using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aspose.Cells;
using System.Threading;
using System.IO;
using System.Diagnostics;
using JCodes.Framework.Common;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.Other.Images;
using JCodes.Framework.Common.Databases;

namespace TestControlUtil
{
    public partial class FrmAsposeCell : Form
    {
        public FrmAsposeCell()
        {
            InitializeComponent();
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

        private DataTable GetData(List<string> DeptNameList)
        {
            //准备数据绑定
            string columnString = "序号|int,疾病名称,";
            foreach (string deptName in DeptNameList)
            {
                columnString += string.Format("{0}人次,{0}百分比,", deptName);
            }
            columnString += "合计人次,合计百分比";
            DataTable dt = DataTableHelper.CreateTable(columnString);
            for (int i = 0; i < 5; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = (i + 1).ToString();
                dr[1] = "测试名称" + (i + 1).ToString();

                int j = 0;
                foreach (string deptName in DeptNameList)
                {
                    dr[2 + j] = Convert.ToInt32(new Random().NextDouble() * 100);
                    dr[3 + j] = new Random().NextDouble().ToString("P2");
                    j += 2;
                    Thread.Sleep(20);
                }
                dr[dt.Columns.Count - 2] = Convert.ToInt32(new Random().NextDouble() * 100);
                dr[dt.Columns.Count - 1] = new Random().NextDouble().ToString("P2");

                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> DeptNameList = new List<string>() { "一师", "一团", "二团" };
            DataTable dt = GetData(DeptNameList);

            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets[0];
            worksheet.PageSetup.Orientation = PageOrientationType.Landscape;//横向打印
            worksheet.PageSetup.Zoom = 100;//以100%的缩放模式打开
            worksheet.PageSetup.PaperSize = PaperSizeType.PaperA4;

            Range range; Cell cell;
            int colSpan = 4 + DeptNameList.Count * 2;
            range = worksheet.Cells.CreateRange(0, 0, 1, colSpan);
            range.Merge();
            range.RowHeight = 20;
            range.SetStyle(CreateTitleStyle(workbook));
            cell = range[0, 0];
            cell.PutValue("患病情况统计");

            range = worksheet.Cells.CreateRange(1, 0, 1, colSpan);
            range.Merge();
            range.RowHeight = 15;
            cell = range[0, 0];
            cell.PutValue("所选部别范围内，总计有1000名人员，查询统计结果如下：");

            range = worksheet.Cells.CreateRange(2, 0, 1, colSpan);
            range.Merge();
            range.RowHeight = 15;
            cell = range[0, 0];
            cell.PutValue("自2007-1-1开始到现在，统计共有500人有患病史，累计900人次，患病情况如下表：");

            #region 生成报表头部表格
            Style headStyle = CreateStyle(workbook, true);
            Style normalStyle = CreateStyle(workbook, false);
            int startRow = 4;
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
            cell.PutValue("疾病名称");
            cell.SetStyle(headStyle);

            int startCol = 2;
            foreach (string deptName in DeptNameList)
            {
                range = worksheet.Cells.CreateRange(startRow, startCol, 1, 2);
                range.Merge();
                range.SetStyle(headStyle);
                cell = range[0, 0];
                cell.PutValue(deptName);

                cell = worksheet.Cells[startRow + 1, startCol];
                cell.PutValue("人次");
                cell.SetStyle(headStyle);
                cell = worksheet.Cells[startRow + 1, startCol + 1];
                cell.PutValue("百分比");
                cell.SetStyle(headStyle);

                startCol += 2;
            }

            range = worksheet.Cells.CreateRange(startRow, startCol, 1, 2);
            range.Merge();
            range.SetStyle(headStyle);
            cell = range[0, 0];
            cell.PutValue("合计");

            cell = worksheet.Cells[startRow + 1, startCol];
            cell.PutValue("人次");
            cell.SetStyle(headStyle);
            cell = worksheet.Cells[startRow + 1, startCol + 1];
            cell.PutValue("百分比");
            cell.SetStyle(headStyle); 
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
            cell.PutValue("以柱状图展示如下：");

            //插入图片到Excel里面
            byte[] bytes = ImageHelper.ImageToBytes(this.pictureBox1.Image);
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                worksheet.Pictures.Add(startRow, 0, stream);
            }

            //Save the excel file.
            string saveFile = FileDialogHelper.SaveExcel("rangecells.xls", "C:\\");
            if (!string.IsNullOrEmpty(saveFile))
            {
                workbook.Save(saveFile);
                if (MessageDxUtil.ShowYesNoAndTips("保存成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(saveFile);
                }
            }
        }

        private void btnBindExcel_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> dictSource = new Dictionary<string, object>();
            dictSource.Add("ACCUSER_SEX", "男");
            dictSource.Add("ACCUSER_TEL", "18620292076");

            string templateFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Templates/Advice.xls");
            string savedFile = AsposeExcelTools.ExportWithDataSource(templateFile, "testAdvice.xls", dictSource);
            if (!string.IsNullOrEmpty(savedFile))
            {
                if (MessageDxUtil.ShowYesNoAndTips("导出成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                {
                    Process.Start(savedFile);
                }
            }
        }

        private void btnReplaceContent_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dictSource = new Dictionary<string, string>();
            dictSource.Add("TIS_HANDLE_NO", "T0001");
            dictSource.Add("ACCUSE_INDUSTRY", "出租车");
            dictSource.Add("ACCUSER_NAME", "张三");

            string templateFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Templates/Advice.xls");
            string savedFile = AsposeExcelTools.ExportWithReplace(templateFile, "testAdvice.xls", dictSource);
            if (!string.IsNullOrEmpty(savedFile))
            {
                if (MessageDxUtil.ShowYesNoAndTips("导出成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                {
                    Process.Start(savedFile);
                }
            }
        }

        private void btnGeneralToXLS_Click(object sender, EventArgs e)
        {
            Workbook workbook = new Workbook();
            workbook.Open(@"C:\Users\Jimmy\Desktop\投行整理\存续期.xls");

            workbook.Save("D:\\b.html", SaveFormat.Html);
        }
    }
}
