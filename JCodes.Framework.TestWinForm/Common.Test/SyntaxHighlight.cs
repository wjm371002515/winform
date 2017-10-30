using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using DevExpress.CodeParser;
using DevExpress.Utils;
using DevExpress.Office.Internal;
using DevExpress.Office.Utils;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Services;
using DevExpress.XtraRichEdit.Internal;
using DevExpress.XtraRichEdit.Import;
using DevExpress.XtraRichEdit.Export;
using DevExpress.XtraRichEdit.Utils;
using DevExpress.XtraRichEdit.Commands;

namespace TestCommons {
    #region SyntaxHighlightModule
    public partial class SyntaxHighlightModule : DevExpress.XtraEditors.XtraForm 
    {

        public SyntaxHighlightModule() {
            InitializeComponent();
        }

        void richEditControl_InitializeDocument(object sender, EventArgs e) {
            Document document = (sender as RichEditControl).Document;
            document.BeginUpdate();
            document.DefaultCharacterProperties.FontName = "Courier New";
            document.DefaultCharacterProperties.FontSize = 10;
            document.Sections[0].Page.Width = Units.InchesToDocumentsF(50);
            document.Sections[0].LineNumbering.CountBy = 1;
            document.Sections[0].LineNumbering.RestartType = LineNumberingRestart.Continuous;
            document.EndUpdate();
        }
    }
    #endregion

    #region SyntaxHighlightService
    public class SyntaxHighlightService : ISyntaxHighlightService {
        #region Fields
        readonly RichEditControl editor;
        readonly SyntaxHighlightInfo syntaxHighlightInfo;
        #endregion


        public SyntaxHighlightService(RichEditControl editor) {
            this.editor = editor;
            this.syntaxHighlightInfo = new SyntaxHighlightInfo();
        }


        #region ISyntaxHighlightService Members
        public void ForceExecute() {
            Execute();
        }
        public void Execute() {
            TokenCollection tokens = Parse(editor.Text);
            HighlightSyntax(tokens);
        }
        #endregion
        TokenCollection Parse(string code) {
            if (string.IsNullOrEmpty(code))
                return null;
            ITokenCategoryHelper tokenizer = CreateTokenizer();
            if (tokenizer == null)
                return new TokenCollection();
            return tokenizer.GetTokens(code);
        }

        ITokenCategoryHelper CreateTokenizer() {
            string fileName = editor.Options.DocumentSaveOptions.CurrentFileName;
            if (String.IsNullOrEmpty(fileName))
                return null;
            ITokenCategoryHelper result = TokenCategoryHelperFactory.CreateHelperForFileExtensions(Path.GetExtension(fileName));
            if (result != null)
                return result;
            else
                return null;
        }

        void HighlightSyntax(TokenCollection tokens) {
            if (tokens == null || tokens.Count == 0)
                return;
            Document document = editor.Document;
            CharacterProperties cp = document.BeginUpdateCharacters(0, 1);

            List<SyntaxHighlightToken> syntaxTokens = new List<SyntaxHighlightToken>(tokens.Count);
            foreach (Token token in tokens) {
                HighlightCategorizedToken((CategorizedToken)token, syntaxTokens);
            }
            document.ApplySyntaxHighlight(syntaxTokens);
            document.EndUpdateCharacters(cp);
        }
        void HighlightCategorizedToken(CategorizedToken token, List<SyntaxHighlightToken> syntaxTokens) {
            Color backColor = editor.ActiveView.BackColor;
            SyntaxHighlightProperties highlightProperties = syntaxHighlightInfo.CalculateTokenCategoryHighlight(token.Category);
            SyntaxHighlightToken syntaxToken = SetTokenColor(token, highlightProperties, backColor);
            if (syntaxToken != null)
                syntaxTokens.Add(syntaxToken);
        }
        SyntaxHighlightToken SetTokenColor(Token token, SyntaxHighlightProperties foreColor, Color backColor) {
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
    public class SyntaxHighlightInfo {
        readonly Dictionary<TokenCategory, SyntaxHighlightProperties> properties;

        public SyntaxHighlightInfo() {
            this.properties = new Dictionary<TokenCategory, SyntaxHighlightProperties>();
            Reset();
        }
        public void Reset() {
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
        void Add(TokenCategory category, Color foreColor) {
            SyntaxHighlightProperties item = new SyntaxHighlightProperties();
            item.ForeColor = foreColor;
            properties.Add(category, item);
        }

        public SyntaxHighlightProperties CalculateTokenCategoryHighlight(TokenCategory category) {
            SyntaxHighlightProperties result = null;
            if (properties.TryGetValue(category, out result))
                return result;
            else
                return properties[TokenCategory.Text];
        }
    }
    #endregion

    #region CustomRichEditCommandFactoryService
    public class CustomRichEditCommandFactoryService : IRichEditCommandFactoryService {
        readonly IRichEditCommandFactoryService service;

        public CustomRichEditCommandFactoryService(IRichEditCommandFactoryService service) {
            Guard.ArgumentNotNull(service, "service");
            this.service = service;
        }

        #region IRichEditCommandFactoryService Members
        RichEditCommand IRichEditCommandFactoryService.CreateCommand(RichEditCommandId id) {
            if (id.Equals(RichEditCommandId.InsertColumnBreak) || id.Equals(RichEditCommandId.InsertLineBreak) || id.Equals(RichEditCommandId.InsertPageBreak))
                return service.CreateCommand(RichEditCommandId.InsertParagraph);
            return service.CreateCommand(id);
        }
        #endregion
    }
    #endregion

    public static class SourceCodeDocumentFormat {
        public static readonly DocumentFormat Id = new DocumentFormat(1325);
    }
    public class SourcesCodeDocumentImporter : PlainTextDocumentImporter {
        internal static readonly FileDialogFilter filter = new FileDialogFilter("Source Files", new string[] { "cs", "vb", "html", "htm", "js", "xml", "css" });
        public override FileDialogFilter Filter { get { return filter; } }
        public override DocumentFormat Format { get { return SourceCodeDocumentFormat.Id; } }
    }
    public class SourcesCodeDocumentExporter : PlainTextDocumentExporter {
        public override FileDialogFilter Filter { get { return SourcesCodeDocumentImporter.filter; } }
        public override DocumentFormat Format { get { return SourceCodeDocumentFormat.Id; } }
    }

}
