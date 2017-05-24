using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using JCodes.Framework.Common.Files;

namespace JCodes.Framework.Common.Office
{
    /// <summary>
    /// 本辅助类主要用来简化对Aspose.Word控件的使用，提供导出Word文档的操作
    /// </summary>
    public class AsposeWordTools
    {
        /// <summary>
        /// 根据列表的书签引用，导出Word文件。
        /// </summary>
        /// <param name="templateFile">模板文件</param>
        /// <param name="saveFileName">要保存的文件名称,如a.doc</param>
        /// <param name="dictBookMark">书签键值列表</param>
        public static string ExportWithBookMark(string templateFile, string saveFileName, Dictionary<string, string> dictBookMark)
        {
            if (!File.Exists(templateFile))
            {
                throw new ArgumentException(templateFile, string.Format("{0} 文件不存在", Path.GetFileName(templateFile)));
            }

            string saveFile = FileDialogHelper.SaveWord(saveFileName, "C:\\");
            if (!string.IsNullOrEmpty(saveFile))
            {
                Aspose.Words.Document doc = new Aspose.Words.Document(templateFile);
                foreach (string name in dictBookMark.Keys)
                {
                    Aspose.Words.Bookmark bookmark = doc.Range.Bookmarks[name];
                    if (bookmark != null)
                    {
                        bookmark.Text = dictBookMark[name];
                    }
                }

                doc.Save(saveFile);
            }
            return saveFile;
        }

        /// <summary>
        /// 根据列表的书签引用，在Web环境中导出Word文件。
        /// </summary>
        /// <param name="templateFile">模板文件（相对目录）</param>
        /// <param name="saveFileName">要保存的文件名称,如a.doc</param>
        /// <param name="dictBookMark">书签键值列表</param>
        public static void WebExportWithBookMark(string templateFile, string saveFileName, Dictionary<string, string> dictBookMark)
        {
            HttpContext curContext = HttpContext.Current;

            string physicPath = curContext.Server.MapPath(templateFile);
            if (!File.Exists(physicPath))
            {
                throw new ArgumentException(templateFile, string.Format("{0} 文件不存在，", templateFile));
            }

            Aspose.Words.Document doc = new Aspose.Words.Document(physicPath);
            foreach (string name in dictBookMark.Keys)
            {
                Aspose.Words.Bookmark bookmark = doc.Range.Bookmarks[name];
                if (bookmark != null)
                {
                    bookmark.Text = dictBookMark[name];
                }
            }

            doc.Save(curContext.Response, saveFileName, Aspose.Words.ContentDisposition.Attachment,
                Aspose.Words.Saving.SaveOptions.CreateSaveOptions(Aspose.Words.SaveFormat.Doc));
        }
           
        /// <summary>
        /// 根据键值对列表的替换模板内容，导出Word文件。
        /// </summary>
        /// <param name="templateFile">模板文件</param>
        /// <param name="saveFileName">要保存的文件名称,如a.doc</param>
        /// <param name="dictReplace">待替换内容和替换值的键值对</param>
        public static string ExportWithReplace(string templateFile, string saveFileName, Dictionary<string, string> dictReplace)
        {
            if (!File.Exists(templateFile))
            {
                throw new ArgumentException(templateFile, string.Format("文件不存在，", Path.GetFileName(templateFile)));
            }

            string saveFile = FileDialogHelper.SaveWord(saveFileName, "C:\\");
            if (!string.IsNullOrEmpty(saveFile))
            {
                Aspose.Words.Document doc = new Aspose.Words.Document(templateFile);
                foreach (string name in dictReplace.Keys)
                {
                    doc.Range.Replace(name, dictReplace[name], true, true);
                }

                doc.Save(saveFile);
            }
            return saveFile;
        }
         
        /// <summary>
        /// 根据键值对列表的替换模板内容，在Web环境中导出Word文件。
        /// </summary>
        /// <param name="templateFile">模板文件</param>
        /// <param name="saveFileName">要保存的文件名称,如a.doc</param>
        /// <param name="dictReplace">待替换内容和替换值的键值对</param>
        public static void WebExportWithReplace(string templateFile, string saveFileName, Dictionary<string, string> dictReplace)
        {
            HttpContext curContext = HttpContext.Current;

            string physicPath = curContext.Server.MapPath(templateFile);
            if (!File.Exists(physicPath))
            {
                throw new ArgumentException(templateFile, string.Format("{0} 文件不存在，", templateFile));
            }

            Aspose.Words.Document doc = new Aspose.Words.Document(physicPath);
            foreach (string name in dictReplace.Keys)
            {
                doc.Range.Replace(name, dictReplace[name], true, true);
            }

            doc.Save(curContext.Response, saveFileName, Aspose.Words.ContentDisposition.Attachment,
                Aspose.Words.Saving.SaveOptions.CreateSaveOptions(Aspose.Words.SaveFormat.Doc));
        }

    }
}
