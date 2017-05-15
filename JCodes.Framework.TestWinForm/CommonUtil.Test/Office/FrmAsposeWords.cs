using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using Aspose.Words;
using Aspose.Words.Tables;
using System.IO;
using System.Diagnostics;
using JCodes.Framework.Common;

namespace TestControlUtil
{
    public partial class FrmAsposeWords : Form
    {
        public FrmAsposeWords()
        {
            InitializeComponent();
        }

        private void btnGenerateDoc_Click(object sender, EventArgs e)
        {
            string fileName = string.Format("数据库设计文档.doc");
            string saveDocFile = FileDialogHelper.SaveWord(fileName, "C:\\");
            if (!string.IsNullOrEmpty(saveDocFile))
            {
                try
                {
                    #region 准备工作
                    Aspose.Words.Document doc = new Aspose.Words.Document();
                    doc.BuiltInDocumentProperties.Title = "数据库设计说明书";
                    doc.BuiltInDocumentProperties.Keywords = "数据库设计说明书";
                    doc.BuiltInDocumentProperties.CreatedTime = DateTime.Now;
                    doc.BuiltInDocumentProperties.LastSavedTime = DateTime.Now;
                    
                    Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);
                    builder.PageSetup.PaperSize = PaperSize.A4;
                    builder.PageSetup.Orientation = Aspose.Words.Orientation.Portrait;
                    builder.InsertParagraph();

                    Dictionary<string, string> tableList = new Dictionary<string,string>();
                    tableList.Add("TB_DictType", "数据字典类型基础表");
                    tableList.Add("TB_DictData", "数据字典项目基础表");
                    tableList.Add("TB_LoginLog", "用户登陆日志表");

                    int i = 1;
                    foreach (string key in tableList.Keys)
                    {
                        string description = string.Format("{0}（{1}）", key, tableList[key]);
                        GenerateTableData(doc, builder, i++, description);
                    }

                    #endregion

                    doc.Save(saveDocFile, SaveFormat.Doc);
                    if (MessageUtil.ShowYesNoAndTips("保存成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDocFile);
                    }
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowError(ex.Message);
                    return;
                }
            }
        }

        private void GenerateTableData(Aspose.Words.Document doc, Aspose.Words.DocumentBuilder builder, int index, string description)
        {
            builder.Font.Size = 10;
            builder.Font.Bold = true;
            builder.Write(string.Format("{0}）{1}", index, description));

            Table table = builder.StartTable();
            builder.RowFormat.HeadingFormat = true;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;

            double totalWidth = 660;

            builder.InsertCell();// 添加一个单元格    
            builder.CellFormat.Borders.LineStyle = LineStyle.Single;
            builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
            builder.CellFormat.Width = totalWidth;
            builder.Font.Size = 9;
            builder.Font.Bold = true;
            builder.CellFormat.Shading.BackgroundPatternColor = Color.LightGray;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;//水平居左对齐
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;//垂直居中对齐
            builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
            builder.Write("字段列表");
            builder.EndRow();

            List<string> columFields = new List<string>() { "编号", "字段列名", "字段描述", "数据类型", "可空", "默认值", "约束类型" };
            for (int i = 0; i < columFields.Count; i++)
            {
                builder.InsertCell();// 添加一个单元格                 
                if (i == 0)
                {
                    builder.CellFormat.Width = 40;
                }
                else if (i == 1 || i == 2)
                {
                    builder.CellFormat.Width = 150;
                }
                else
                {
                    builder.CellFormat.Width = 80;
                }

                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//修改为水平居中对齐
                builder.Write(columFields[i]);
            }
            builder.EndRow();

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    builder.InsertCell();// 添加一个单元格  
                    builder.Font.Bold = false;
                    builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//修改内容为白色背景
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;//修改为水平居左对齐

                    if (j == 0)
                    {
                        builder.CellFormat.Width = 40;
                    }
                    else if (j == 1 || j == 2)
                    {
                        builder.CellFormat.Width = 150;
                    }
                    else
                    {
                        builder.CellFormat.Width = 80;
                    }

                    if (j == 0)
                    {
                        builder.Write((i + 1).ToString());
                    }
                    else
                    {
                        builder.Write("测试" + j.ToString());
                    }
                }
                builder.EndRow();
            }

            builder.EndTable();
            builder.InsertParagraph();
        }

        private void btnBindDoc_Click(object sender, EventArgs e)
        {
            //绑定数据源方式进行，需要在文档里面添加标签引用
            Dictionary<string, string> dictSource = new Dictionary<string, string>();
            dictSource.Add("ACCUSER_SEX", "男");
            dictSource.Add("ACCUSER_TEL", "18620292076");

            string templateFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Templates/Advice.doc");
            string savedFile = AsposeWordTools.ExportWithBookMark(templateFile, "testAdvice.doc", dictSource);
            if (!string.IsNullOrEmpty(savedFile))
            {
                if (MessageUtil.ShowYesNoAndTips("导出成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                {
                    Process.Start(savedFile);
                }
            }
        }

        private void btnReplaceContent_Click(object sender, EventArgs e)
        {
            //替换文档里面的内容，实现模板化替换
            Dictionary<string, string> dictSource = new Dictionary<string, string>();
            dictSource.Add("TIS_HANDLE_NO", "T0001");
            dictSource.Add("ACCUSE_INDUSTRY", "出租车");
            dictSource.Add("ACCUSER_NAME", "张三");

            string templateFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Templates/Advice.doc");
            string savedFile = AsposeWordTools.ExportWithReplace(templateFile, "testAdvice.doc", dictSource);
            if (!string.IsNullOrEmpty(savedFile))
            {
                if (MessageUtil.ShowYesNoAndTips("导出成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                {
                    Process.Start(savedFile);
                }
            }
        }

        private void FrmAsposeWords_Load(object sender, EventArgs e)
        {

        }
    }
}
