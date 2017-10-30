using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using org.in2bits.MyXls;
using System.Threading;
using JCodes.Framework.Common;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Databases;
using JCodes.Framework.Common.Office;
using JCodes.Framework.CommonControl.Other;

namespace TestControlUtil
{
    public partial class FrmMyXls : Form
    {
        public FrmMyXls()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            XlsDocument xls = new XlsDocument();

            //添加文件属性
            xls.SummaryInformation.Author = "伍华聪"; //作者
            xls.SummaryInformation.Subject = "测试Myxls的Excel文件生成";
            xls.DocumentSummaryInformation.Company = "http://www.iqidi.com 广州爱启迪技术有限公司";

            Worksheet sheet = xls.Workbook.Worksheets.Add("sheet1");//状态栏标题名称
            Cells cells = sheet.Cells;

            XF xf = xls.NewXF();
            xf.HorizontalAlignment = HorizontalAlignments.Centered;
            xf.VerticalAlignment = VerticalAlignments.Centered;
            xf.Pattern = 1;
            xf.PatternColor = Colors.Default30;
            xf.UseBorder = true;
            xf.TopLineStyle = 1;
            xf.TopLineColor = Colors.Black;
            xf.BottomLineStyle = 1;
            xf.BottomLineColor = Colors.Black;
            xf.LeftLineStyle = 1;
            xf.LeftLineColor = Colors.Black;
            xf.RightLineStyle = 1;
            xf.RightLineColor = Colors.Black;
            xf.Font.Bold = true;
            xf.Font.Height = 11 * 20;
            xf.Font.ColorIndex = 1;

            cells.Add(1, 1, "姓名", xf);
            cells.Add(1, 2, "年龄", xf);
            cells.Add(1, 3, "Email邮箱", xf);
            cells.Add(1, 4, "描述", xf);

            for (int i = 0; i < 500; i++)
            {
                Cell nameCell = cells.Add(i + 2, 1, RandomChinese.GetRandomChinese2(3));
                nameCell.Font.FontFamily = FontFamilies.Roman; //字体
                nameCell.Font.Bold = true;  //字体为粗体    

                cells.Add(i + 2, 2, new Random().Next(20, 50));
                cells.Add(i + 2, 3, "xxx@163.com");
                cells.Add(i + 2, 4, RandomChinese.GetRandomChinese2(50));
                Thread.Sleep(10);
            }

            string saveFile = FileDialogHelper.SaveExcel("wuhuacong.xls", "C:\\");
            if (!string.IsNullOrEmpty(saveFile))
            {
                xls.FileName = saveFile;
                xls.Save(true);

                if (MessageDxUtil.ShowYesNoAndTips("保存成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(saveFile);
                }
            }
        }

        private void btnToolGen_Click(object sender, EventArgs e)
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

            string saveFile = FileDialogHelper.SaveExcel("wuhuacong.xls", "C:\\");
            if (!string.IsNullOrEmpty(saveFile))
            {
                MyXlsHelper.Export(dt, saveFile);

                if (MessageDxUtil.ShowYesNoAndTips("保存成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(saveFile);
                }
            }
        }
    }
}
