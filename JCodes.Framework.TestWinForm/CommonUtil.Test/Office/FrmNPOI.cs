using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Web;
using System.Threading;
using JCodes.Framework.Common;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl.Other;

namespace TestControlUtil
{
    public partial class FrmNPOI : Form
    {
        public FrmNPOI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();

            ISheet sheet = workbook.CreateSheet("My Sheet");

            sheet.CreateRow(0).CreateCell(0).SetCellValue("0");
            sheet.CreateRow(1).CreateCell(0).SetCellValue("1");
            sheet.CreateRow(2).CreateCell(0).SetCellValue("2");
            sheet.CreateRow(3).CreateCell(0).SetCellValue("3");
            sheet.CreateRow(4).CreateCell(0).SetCellValue("4");
            sheet.CreateRow(5).CreateCell(0).SetCellValue("5");

            string saveFile = FileDialogHelper.SaveExcel("test.xls");
            if (!string.IsNullOrEmpty(saveFile))
            {
                SaveToFile(workbook, saveFile);

                if (MessageDxUtil.ShowYesNoAndTips("保存成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(saveFile);
                }
            }

            workbook = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("My Sheet");

            ICellStyle style1 = workbook.CreateCellStyle();
            style1.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.BLUE.index2;
            style1.FillPattern = FillPatternType.SOLID_FOREGROUND;
            ICellStyle style2 = workbook.CreateCellStyle();
            style2.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.YELLOW.index2;
            style2.FillPattern = FillPatternType.SOLID_FOREGROUND;

            ICell cell = sheet.CreateRow(0).CreateCell(0);
            cell.CellStyle = style1;
            cell.SetCellValue(0);

            cell = sheet.CreateRow(1).CreateCell(0);
            cell.CellStyle = style2;
            cell.SetCellValue(1);

            cell = sheet.CreateRow(2).CreateCell(0);
            cell.CellStyle = style1;
            cell.SetCellValue(2);

            cell = sheet.CreateRow(3).CreateCell(0);
            cell.CellStyle = style2;
            cell.SetCellValue(3);

            cell = sheet.CreateRow(4).CreateCell(0);
            cell.CellStyle = style1;
            cell.SetCellValue(4);

            string saveFile = FileDialogHelper.SaveExcel("test.xls");
            if (!string.IsNullOrEmpty(saveFile))
            {
                SaveToFile(workbook, saveFile);

                if (MessageDxUtil.ShowYesNoAndTips("保存成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(saveFile);
                }
            }

            workbook.CloneSheet(0);
        }

        private void SaveToFile(HSSFWorkbook workbook, string fileName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
            }
        }

        private void SaveToWeb(HSSFWorkbook workbook, string fileName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                HttpContext curContext = HttpContext.Current;
                curContext.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=EmptyWorkbook.xls"));
                curContext.Response.BinaryWrite(ms.ToArray());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = DataTableHelper.CreateTable("姓名,年龄|int,Email邮箱,描述");
            for (int i = 0; i < 50; i++)
            {
                DataRow row = dt.NewRow();
                row[0] = RandomChinese.GetRandomChinese(3);
                row[1] = new Random(DateTimeHelper.GetServerDateTime2().Millisecond).Next(20, 50);
                row[2] = "xxx@163.com";
                row[3] = RandomChinese.GetRandomChinese2(50);
                dt.Rows.Add(row);

                Thread.Sleep(10);
            }
            string saveFile = FileDialogHelper.SaveExcel("test.xls");
            if (!string.IsNullOrEmpty(saveFile))
            {

                NPOIHelper.Export(dt, "测试内容", saveFile);
                if (MessageDxUtil.ShowYesNoAndTips("保存成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(saveFile);
                }
            }
        }

    }
}
