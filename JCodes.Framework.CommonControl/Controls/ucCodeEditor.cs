using System.IO;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;

namespace JCodes.Framework.CommonControl.Controls
{
    public partial class ucCodeEditor : UserControl {
        public ucCodeEditor() {
            InitializeComponent();
        }
        public void LoadCode(Stream stream) {
            richEditControl1.LoadDocument(stream, DocumentFormat.Rtf);
            richEditControl1.Document.Sections[0].Page.Width = 10000;
        }
    }
}
