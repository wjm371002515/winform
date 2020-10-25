using JCodes.Framework.CommonControl.BaseUI;
using PdfSharp.Drawing;
using System.IO;
using System;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace JCodes.Framework.TestWinForm.PdfDemo
{
    public partial class PdfTestFrm : BaseDock
    {
        public PdfTestFrm()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, System.EventArgs e)
        {
            string filename1 = "C:\\Users\\Jimmy\\Desktop\\hypg2.pdf";
            string filename2 = "C:\\Users\\Jimmy\\Desktop\\hypg.pdf";
            string newfilename = "C:\\Users\\Jimmy\\Desktop\\合并_hypg.pdf";


            PdfReader reader = new PdfReader(filename2);
            Document document = new Document(reader.GetPageSizeWithRotation(1));
            int n = reader.NumberOfPages;
            FileStream baos = new FileStream(newfilename, FileMode.Create, FileAccess.Write);
            PdfCopy copy = new PdfCopy(document, baos);
            copy.ViewerPreferences = PdfWriter.HideToolbar | PdfWriter.HideMenubar;
            //往pdf中写入内容   
            document.Open();
            for (int i = 1; i <= n; i++)
            {
                PdfImportedPage page = copy.GetImportedPage(reader, i);
                copy.AddPage(page);
            }
            document.Close();
            reader.Close();


            /*List<PdfReader> readerList = new List<PdfReader>();//记录合并PDF集合 
            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(newfilename, FileMode.Create));
            document.Open();

            PdfContentByte cb = writer.DirectContent;
            PdfImportedPage newPage;
            for (int i = 0; i < fileList.Length; i++)
            {
                if (!string.IsNullOrEmpty(fileList[i]))
                {
                    PdfReader reader = new PdfReader(fileList[i]);
                    int iPageNum = reader.NumberOfPages;
                    for (int j = 1; j <= iPageNum; j++)
                    {
                        document.NewPage();
                        newPage = writer.GetImportedPage(reader, j);
                        cb.AddTemplate(newPage, 0, 0);
                    }
                    readerList.Add(reader);
                }
            }
            document.Close();*/
        }
    }
}
