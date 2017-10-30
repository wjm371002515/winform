using DevExpress.XtraRichEdit.API.Native;
using JCodes.Framework.Common.Format;
using JCodes.Framework.CommonControl.BaseUI;
using System;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using DevExpress.Office.Utils;
using DevExpress.XtraRichEdit.Internal;
using DevExpress.XtraRichEdit.Services;
using DevExpress.XtraRichEdit.Import;
using DevExpress.XtraRichEdit.Export;
using DevExpress.XtraRichEdit;
using DevExpress.Utils;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.Office.Internal;
using JCodes.Framework.Common.Files;
using DevExpress.CodeParser;
using System.IO;

namespace JCodes.Framework.AddIn.Test
{
    public partial class FrmdelegateSetUI : BaseDock
    {
        public FrmdelegateSetUI()
        {
            InitializeComponent();

            richText.AddService(typeof(ISyntaxHighlightService), new SyntaxHighlightService(richText));
            IRichEditCommandFactoryService commandFactory = richText.GetService<IRichEditCommandFactoryService>();
            CustomRichEditCommandFactoryService newCommandFactory = new CustomRichEditCommandFactoryService(commandFactory);
            richText.RemoveService(typeof(IRichEditCommandFactoryService));
            richText.AddService(typeof(IRichEditCommandFactoryService), newCommandFactory);

            IDocumentImportManagerService importManager = richText.GetService<IDocumentImportManagerService>();
            importManager.UnregisterAllImporters();
            importManager.RegisterImporter(new PlainTextDocumentImporter());
            importManager.RegisterImporter(new SourcesCodeDocumentImporter());

            IDocumentExportManagerService exportManager = richText.GetService<IDocumentExportManagerService>();
            exportManager.UnregisterAllExporters();
            exportManager.RegisterExporter(new PlainTextDocumentExporter());
            exportManager.RegisterExporter(new SourcesCodeDocumentExporter());

            richText.LoadDocument(@"C:\Users\Public\Documents\DevExpress Demos 14.1\Components\WinForms\CS\RichEditMainDemo\Modules\SyntaxHighlight.cs", DocumentFormat.PlainText);
        }

        void richEditControl_InitializeDocument(object sender, EventArgs e)
        {
            Document document = richText.Document;
            document.BeginUpdate();
            try
            {
                document.DefaultCharacterProperties.FontName = "Courier New";
                document.DefaultCharacterProperties.FontSize = 10;
                document.Sections[0].Page.Width = Units.InchesToDocumentsF(100);
                document.Sections[0].LineNumbering.CountBy = 1;
                document.Sections[0].LineNumbering.RestartType = LineNumberingRestart.Continuous;

                SizeF tabSize = richText.MeasureSingleLineString("    ", document.DefaultCharacterProperties);
                TabInfoCollection tabs = document.Paragraphs[0].BeginUpdateTabs(true);
                try
                {
                    for (int i = 1; i <= 30; i++)
                    {
                        DevExpress.XtraRichEdit.API.Native.TabInfo tab = new DevExpress.XtraRichEdit.API.Native.TabInfo();
                        tab.Position = i * tabSize.Width;
                        tabs.Add(tab);
                    }
                }
                finally
                {
                    document.Paragraphs[0].EndUpdateTabs(tabs);
                }
            }
            finally
            {
                document.EndUpdate();
            }
        }

        //第二步：定义线程的主体方法
        /// <summary>
        /// 线程的主体方法
        /// </summary>
        private void ThreadProcSafe()
        {
            //...执行线程任务

            //在线程中更新UI（通过控件的.Invoke方法）
            System.Timers.Timer t = new System.Timers.Timer(1000);//实例化Timer类，设置间隔时间为1000毫秒；
            t.Elapsed += new System.Timers.ElapsedEventHandler((obj, e) => {
                Console.WriteLine(DateTimeHelper.GetServerDateTime());
                this.SetText(DateTimeHelper.GetServerDateTime2() + "This text was set safely.\r\n"); 
            });
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；

            //...执行线程其他任务
        }

        // 第一步：定义委托类型
        // 将text更新的界面控件的委托类型
        delegate void SetTextCallback(string text);
        private void btnStart_Click(object sender, EventArgs e)
        {
            var demoThread =
                new Thread(new ThreadStart(this.ThreadProcSafe));
            demoThread.Start();
        }

        //第三步：定义更新UI控件的方法
        /// <summary>
        /// 更新文本框内容的方法
        /// </summary>
        /// <param name="text"></param>
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.richText.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
            {
                while (!this.richText.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (this.richText.Disposing || this.richText.IsDisposed)
                        return;
                }
                SetTextCallback d = new SetTextCallback(SetText);
                this.richText.Invoke(d, new object[] { text });
            }
            else
            {
                this.richText.Text += text;
            }
        }
    }

    #region SyntaxHighlightService
    public class SyntaxHighlightService : ISyntaxHighlightService
    {
        #region Fields
        readonly RichEditControl editor;
        readonly SyntaxHighlightInfo syntaxHighlightInfo;
        #endregion


        public SyntaxHighlightService(RichEditControl editor)
        {
            this.editor = editor;
            this.syntaxHighlightInfo = new SyntaxHighlightInfo();
        }


        #region ISyntaxHighlightService Members
        public void ForceExecute()
        {
            Execute();
        }
        public void Execute()
        {
            TokenCollection tokens = Parse(editor.Text);
            HighlightSyntax(tokens);
        }
        #endregion
        TokenCollection Parse(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;
            ITokenCategoryHelper tokenizer = CreateTokenizer();
            if (tokenizer == null)
                return new TokenCollection();
            return tokenizer.GetTokens(code);
        }

        ITokenCategoryHelper CreateTokenizer()
        {
            string fileName = editor.Options.DocumentSaveOptions.CurrentFileName;
            if (String.IsNullOrEmpty(fileName))
                return null;
            ITokenCategoryHelper result = TokenCategoryHelperFactory.CreateHelperForFileExtensions(Path.GetExtension(fileName));
            if (result != null)
                return result;
            else
                return null;
        }

        void HighlightSyntax(TokenCollection tokens)
        {
            if (tokens == null || tokens.Count == 0)
                return;
            Document document = editor.Document;
            CharacterProperties cp = document.BeginUpdateCharacters(0, 1);

            List<SyntaxHighlightToken> syntaxTokens = new List<SyntaxHighlightToken>(tokens.Count);
            foreach (Token token in tokens)
            {
                HighlightCategorizedToken((CategorizedToken)token, syntaxTokens);
            }
            document.ApplySyntaxHighlight(syntaxTokens);
            document.EndUpdateCharacters(cp);
        }
        void HighlightCategorizedToken(CategorizedToken token, List<SyntaxHighlightToken> syntaxTokens)
        {
            Color backColor = editor.ActiveView.BackColor;
            SyntaxHighlightProperties highlightProperties = syntaxHighlightInfo.CalculateTokenCategoryHighlight(token.Category);
            SyntaxHighlightToken syntaxToken = SetTokenColor(token, highlightProperties, backColor);
            if (syntaxToken != null)
                syntaxTokens.Add(syntaxToken);
        }
        SyntaxHighlightToken SetTokenColor(Token token, SyntaxHighlightProperties foreColor, Color backColor)
        {
            if (editor.Document.Paragraphs.Count < token.Range.Start.Line)
                return null;
            int paragraphStart = DocumentHelper.GetParagraphStart(editor.Document.Paragraphs[token.Range.Start.Line - 1]);
            int tokenStart = paragraphStart + token.Range.Start.Offset - 1;
            if (token.Range.End.Line != token.Range.Start.Line)
                paragraphStart = DocumentHelper.GetParagraphStart(editor.Document.Paragraphs[token.Range.End.Line - 1]);

            int tokenEnd = paragraphStart + token.Range.End.Offset - 1;
            System.Diagnostics.Debug.Assert(tokenEnd > tokenStart);
            return new SyntaxHighlightToken(tokenStart, tokenEnd - tokenStart, foreColor);
        }
    }
    #endregion

    #region SyntaxHighlightInfo
    public class SyntaxHighlightInfo
    {
        readonly Dictionary<TokenCategory, SyntaxHighlightProperties> properties;

        public SyntaxHighlightInfo()
        {
            this.properties = new Dictionary<TokenCategory, SyntaxHighlightProperties>();
            Reset();
        }
        public void Reset()
        {
            properties.Clear();
            Add(TokenCategory.Text, DXColor.Black);
            Add(TokenCategory.Keyword, DXColor.Blue);
            Add(TokenCategory.String, DXColor.Brown);
            Add(TokenCategory.Comment, DXColor.Green);
            Add(TokenCategory.Identifier, DXColor.Black);
            Add(TokenCategory.PreprocessorKeyword, DXColor.Blue);
            Add(TokenCategory.Number, DXColor.Red);
            Add(TokenCategory.Operator, DXColor.Black);
            Add(TokenCategory.Unknown, DXColor.Black);
            Add(TokenCategory.XmlComment, DXColor.Gray);

            Add(TokenCategory.CssComment, DXColor.Green);
            Add(TokenCategory.CssKeyword, DXColor.Brown);
            Add(TokenCategory.CssPropertyName, DXColor.Red);
            Add(TokenCategory.CssPropertyValue, DXColor.Blue);
            Add(TokenCategory.CssSelector, DXColor.Blue);
            Add(TokenCategory.CssStringValue, DXColor.Blue);

            Add(TokenCategory.HtmlAttributeName, DXColor.Red);
            Add(TokenCategory.HtmlAttributeValue, DXColor.Blue);
            Add(TokenCategory.HtmlComment, DXColor.Green);
            Add(TokenCategory.HtmlElementName, DXColor.Brown);
            Add(TokenCategory.HtmlEntity, DXColor.Gray);
            Add(TokenCategory.HtmlOperator, DXColor.Black);
            Add(TokenCategory.HtmlServerSideScript, DXColor.Black);
            Add(TokenCategory.HtmlString, DXColor.Blue);
            Add(TokenCategory.HtmlTagDelimiter, DXColor.Blue);
        }
        void Add(TokenCategory category, Color foreColor)
        {
            SyntaxHighlightProperties item = new SyntaxHighlightProperties();
            item.ForeColor = foreColor;
            properties.Add(category, item);
        }

        public SyntaxHighlightProperties CalculateTokenCategoryHighlight(TokenCategory category)
        {
            SyntaxHighlightProperties result = null;
            if (properties.TryGetValue(category, out result))
                return result;
            else
                return properties[TokenCategory.Text];
        }
    }
    #endregion

    #region CustomRichEditCommandFactoryService
    public class CustomRichEditCommandFactoryService : IRichEditCommandFactoryService
    {
        readonly IRichEditCommandFactoryService service;

        public CustomRichEditCommandFactoryService(IRichEditCommandFactoryService service)
        {
            Guard.ArgumentNotNull(service, "service");
            this.service = service;
        }

        #region IRichEditCommandFactoryService Members
        RichEditCommand IRichEditCommandFactoryService.CreateCommand(RichEditCommandId id)
        {
            if (id.Equals(RichEditCommandId.InsertColumnBreak) || id.Equals(RichEditCommandId.InsertLineBreak) || id.Equals(RichEditCommandId.InsertPageBreak))
                return service.CreateCommand(RichEditCommandId.InsertParagraph);
            return service.CreateCommand(id);
        }
        #endregion
    }
    #endregion

    public static class SourceCodeDocumentFormat
    {
        public static readonly DocumentFormat Id = new DocumentFormat(1325);
    }
    public class SourcesCodeDocumentImporter : PlainTextDocumentImporter
    {
        internal static readonly FileDialogFilter filter = new FileDialogFilter("Source Files", new string[] { "cs", "vb", "html", "htm", "js", "xml", "css" });
        public override FileDialogFilter Filter { get { return filter; } }
        public override DocumentFormat Format { get { return SourceCodeDocumentFormat.Id; } }
    }
    public class SourcesCodeDocumentExporter : PlainTextDocumentExporter
    {
        public override FileDialogFilter Filter { get { return SourcesCodeDocumentImporter.filter; } }
        public override DocumentFormat Format { get { return SourceCodeDocumentFormat.Id; } }
    }

}
