using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSplashScreen;
using DevExpress.Utils;
using System.Diagnostics;
using DevExpress.XtraEditors;

namespace JCodes.Framework.WinFormUI
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        #region 属性
        WaitDialogForm wait = null;
        #endregion

        public frmMain() {
            wait = new WaitDialogForm("Please Wati, Logining...", "Login");
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            CreateResourceStream();
            //SolutionExplorer.InitTreeView(treeView1);
            wait.Close(); 
        }
        Assembly currentAssemblyCore;
        Assembly CurrentAssembly {
            get {
                if(currentAssemblyCore == null)
                    currentAssemblyCore = Assembly.GetExecutingAssembly();
                return currentAssemblyCore;
            }
        }
        private void CreateResourceStream() {
            fileStreams.Add(CurrentAssembly.GetManifestResourceStream("DockingDemo.Resources.ProgramText.rtf"));
            fileStreams.Add(CurrentAssembly.GetManifestResourceStream("DockingDemo.Resources.ProgramText2.rtf"));
            fileStreams.Add(CurrentAssembly.GetManifestResourceStream("DockingDemo.Resources.ProgramText3.rtf"));
        }
        private void frmMain_Load(object sender, System.EventArgs e) {
            SkinHelper.InitSkinPopupMenu(iPaintStyle);
            BeginInvoke(new MethodInvoker(InitDemo));
        }
        List<Stream> fileStreams = new List<Stream>();
        int i = 0;
        int projectIndex = 0;
        Cursor currentCursor;
        void InitDemo() {
            AddControls(this, comboBox1);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            textBox1.ContextMenu = textBox2.ContextMenu = new ContextMenu();
            AddNewForm("File.cs");
            //DevExpress.Demos.ClassViewer.AddClassInfo(treeView1, this.GetType(), new object[] { this, new SolutionExplorer() });
           
        }
        void AddNewForm(string s) {
            //RichEditControl control = new RichEditControl();
            //control.MenuManager = barManager1;
            //control.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Draft;
            //control.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //control.LayoutUnit = DevExpress.XtraRichEdit.DocumentLayoutUnit.Pixel;
            //control.Name = s;
            //control.Text = s;
            //control.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            //control.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            //control.SelectionChanged += new EventHandler(tb_SelectionChanged);
            //tabbedView1.BeginUpdate();
            //BaseDocument document = tabbedView1.Controller.AddDocument(control);
            //document.Image = imageList4.Images[10];
            //document.Form.Text = s;
            //document.Form.Icon = CreateIcon(10);
            //document.Footer = Directory.GetCurrentDirectory();
            //control.LoadDocument(fileStreams[i%3], DocumentFormat.Rtf);
            //control.Document.Sections[0].Page.Width = 10000;
            //tabbedView1.EndUpdate();
            //fileStreams[i].Seek(0, 0);
            //i++;
            //if(i == 3) i = 0;
        }
        System.Drawing.Icon CreateIcon(int index) {
            return CreateIcon(imageList4.Images[index]);
        }
        System.Drawing.Icon CreateIcon(Image image) {
            if(image == null) return null;
            using(System.Drawing.Bitmap newIcon = new System.Drawing.Bitmap(16, 16, System.Drawing.Imaging.PixelFormat.Format32bppArgb)) {
                using(System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(newIcon)) {
                    g.DrawImageUnscaled(image, 0, 0, 16, 16);
                    return System.Drawing.Icon.FromHandle(newIcon.GetHicon());
                }
            }
        }
        //RichEditControl ActiveRTB {
        //    get {
        //        if(this.ActiveMdiChild != null && this.ActiveMdiChild.ContainsFocus) {
        //            if(this.ActiveMdiChild.Controls.Count == 0) return null;
        //            return this.ActiveMdiChild.Controls[0] as RichEditControl;
        //        }
        //        return null;
        //    }
        //}
        void InitEdit()
        {
            //RichEditControl editControl = ActiveRTB;
            //if (editControl != null)
            //{
            //    iCut.Enabled = iCopy.Enabled = editControl.Document.Selection != null;
            //    iPaste.Enabled = string.IsNullOrEmpty(Clipboard.GetText(TextDataFormat.Text)) ? false : true;
            //    iUndo.Enabled = editControl.CanUndo;
            //    iRedo.Enabled = editControl.CanRedo;
            //}
            //else
            //{
            //    iCut.Enabled = iCopy.Enabled = iPaste.Enabled = iUndo.Enabled = iRedo.Enabled = false;
            //}
        }
        void tb_SelectionChanged(object sender, EventArgs e) {
            InitEdit();
        }
        void AddControls(Control container, DevExpress.XtraEditors.ComboBoxEdit cb) {
            foreach(object obj in container.Controls) {
                cb.Properties.Items.Add(obj);
                if(obj is Control) AddControls(obj as Control, cb);
            }
        }
        void repositoryItemComboBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter && eFind.EditValue != null)
                repositoryItemComboBox1.Items.Add(eFind.EditValue.ToString());
        }
        void comboBox2_SelectedIndexChanged(object sender, System.EventArgs e) {
            if(comboBox2.SelectedIndex == 0)
                textBox2.Text = " ------ Build started: Project: DockingDemo, Configuration: Debug .NET ------\r\n\r\n Preparing resources...\r\n Updating references...\r\n Performing main compilation...\r\n\r\n Build complete -- 0 errors, 0 warnings\r\n Building satellite assemblies...\r\n\r\n\r\n ---------------------- Done ----------------------\r\n\r\n     Build: 1 succeeded, 0 failed, 0 skipped";
            else textBox2.Text = " 'DefaultDomain': Loaded 'd:\\winnt\\microsoft.net\\framework\\v1.0.3705\\mscorlib.dll', No symbols loaded.\r\n 'DockingDemo': Loaded 'C:\\BarDemos\\CS\\DockingDemo\\bin\\Debug\\DockingDemo.exe', Symbols loaded.";
        }
        void textBox2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
            e.Handled = true;
        }
      
        void ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            projectIndex++;
            AddNewForm(string.Format("File{0}.cs", projectIndex));
        }
       
        void frmMain_MdiChildActivate(object sender, System.EventArgs e) {
            InitEdit();
        }
        void iCut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //RichEditControl rtb = ActiveRTB;
            //if(rtb != null) rtb.Cut();
        }
        void iCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //RichEditControl rtb = ActiveRTB;
            //if(rtb != null) rtb.Copy();
        }
        void iPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //RichEditControl rtb = ActiveRTB;
            //if(rtb != null) rtb.Paste();
        }
        void iSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //RichEditControl rtb = ActiveRTB;
            //if(rtb != null) {
            //    rtb.SelectAll();
            //    rtb.Focus();
            //}
        }
        void iUndo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //RichEditControl rtb = ActiveRTB;
            //if(rtb != null) rtb.Undo();
            //InitEdit();
        }
        void iRedo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //RichEditControl rtb = ActiveRTB;
            //if(rtb != null) rtb.Redo();
            //InitEdit();
        }
        //AB15908
        protected DockPanel GetTopDockPanelCore(DockPanel panel) {
            if(panel.ParentPanel != null) return GetTopDockPanel(panel.ParentPanel);
            else return panel;
        }
        protected DockPanel GetTopDockPanel(DockPanel panel) {
            DockPanel floatPanelCandidate = GetTopDockPanelCore(panel);
            if(floatPanelCandidate.Dock == DockingStyle.Float) return floatPanelCandidate;
            else return panel;
        }
        //AB15908
        void iSolutionExplorer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            dockPanel1.Show();
        }
        void iProperties_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            dockPanel2.Show();
        }
        void iTaskList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            dockPanel3.Show();
        }
        void iFindResults_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            dockPanel4.Show();
        }
        void iOutput_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            dockPanel5.Show();
        }
        void iToolbox_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            dockPanel6.Show();
        }
        void solutionExplorer1_PropertiesItemClick(object sender, System.EventArgs e) {
            dockPanel2.Show();
        }
        void solutionExplorer1_TreeViewItemClick(object sender, System.EventArgs e) {
            //DevExpress.XtraTreeList.TreeList treeView = sender as DevExpress.XtraTreeList.TreeList;
            //string fileName = treeView.FocusedNode.GetDisplayText(0);
            //fileName = fileName.Replace(".cs", string.Empty);
            //Stream stream = CurrentAssembly.GetManifestResourceStream(string.Format("DockingDemo.Resources.{0}.rtf", fileName));
            //foreach(BaseDocument document in tabbedView1.Documents) {
            //    if(document.Caption == (fileName + ".cs")) {
            //        tabbedView1.Controller.Activate(document);
            //        return;
            //    }
            //}
            //if(stream != null) {
            //    RichEditControl control = new RichEditControl();
            //    control.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Draft;
            //    control.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //    control.LayoutUnit = DevExpress.XtraRichEdit.DocumentLayoutUnit.Pixel;
            //    control.Text = fileName + ".cs";
            //    control.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            //    control.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            //    control.SelectionChanged += new EventHandler(tb_SelectionChanged);
            //    tabbedView1.BeginUpdate();
            //    BaseDocument document = tabbedView1.Controller.AddDocument(control);
            //    document.Form.Icon = CreateIcon(treeView.FocusedNode.StateImageIndex);
            //    document.Form.Text = fileName + ".cs";
            //    document.Image = imageList4.Images[treeView.FocusedNode.StateImageIndex];
            //    document.Footer = Directory.GetCurrentDirectory();
            //    control.LoadDocument(stream, DocumentFormat.Rtf);
            //    control.Document.Sections[0].Page.Width = 10000;
            //    tabbedView1.EndUpdate();
            //}
        }
        void iSaveLayout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "XML files (*.xml)|*.xml";
            dlg.Title = "Save Layout";
            if(dlg.ShowDialog() == DialogResult.OK) {
                Refresh(true);
                barManager1.SaveToXml(dlg.FileName);
                Refresh(false);
            }
        }
        void iLoadLayout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XML files (*.xml)|*.xml|All files|*.*";
            dlg.Title = "Restore Layout";
            if(dlg.ShowDialog() == DialogResult.OK) {
                Refresh(true);
                try {
                    barManager1.RestoreFromXml(dlg.FileName);
                    SkinHelper.InitSkinPopupMenu(iPaintStyle);
                }
                catch { }
                Refresh(false);
            }
        }
        void Refresh(bool isWait) {
            if(isWait) {
                currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
            }
            else
                Cursor.Current = currentCursor;
            this.Refresh();
        }
        void iExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.Close();
        }
        void tabbedView1_DocumentAdded(object sender, DocumentEventArgs e) {
            e.Document.Form.Text = e.Document.Caption;
            e.Document.Form.Icon = CreateIcon(e.Document.Image);
        }
        void tabbedView1_LostFocus(object sender, EventArgs e) {
            InitEdit();
        }

        /// <summary>
        /// 网络爬虫
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiCrawler_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddTabForm("Test", null, @".\AddIn\JCodes.Framework.AddIn.dll", @"JCodes.Framework.AddIn.MenuMgr");
        }

        /// <summary>
        /// 动态加载窗体
        /// </summary>
        /// <param name="name">窗体的名字</param>
        /// <param name="icon">窗体的icon</param>
        /// <param name="dllName">对应的dll文件</param>
        /// <param name="spaceName">对应的类名</param>
        void AddTabForm(string name, string icon, string dllName, string spaceName)
        {
            // 获取文档列表
            var documents = tabbedView1.Controller.View.Documents;
            for (int i = 0; i < documents.Count; i++)
            {
                // 如果要加载的AddIn 已经被加载过了，则激活老的
                if (string.Equals(documents[i].Caption, name))
                {
                    tabbedView1.Controller.Activate(documents[i]);
                    return;
                }
            }

            if (!File.Exists(dllName))
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, @"[AddTabForm][name=" + name + "][icon=" + icon + "]" + "[dllName=" + dllName + "]" + "[spaceName=" + spaceName + "] 未找到对应的dll文件", typeof(frmMain));
                return;
            }

            // 动态加载窗体
            Assembly assembly = Assembly.LoadFrom(dllName);
            XtraForm form = (XtraForm)assembly.CreateInstance(spaceName);

            // 如果这个form 不存在则不在继续
            if (form == null)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, @"[AddTabForm][name="+name+"][icon="+icon+"]"+"[dllName="+dllName+"]"+"[spaceName="+spaceName+"] 未找到对应的类对象", typeof(frmMain));
                return;
            }

            // 这两个要一起，添加控件的时候，以Text 取生成对应的
            form.Text = name;
            form.Name = name;

            // 判断是否传入了图标，如果传入了则用图标
            if (!string.IsNullOrEmpty(icon))
            {
                form.Icon = new Icon(icon);
            }

            // 加载窗体到tabbedView 界面中
            tabbedView1.BeginUpdate();
            tabbedView1.Controller.AddDocument(form);
            tabbedView1.EndUpdate();
        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddTabForm("Test2", null, @".\AddIn\JCodes.Framework.AddIn2.dll", @"JCodes.Framework.AddIn.XtraForm1");
        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AddTabForm("Test3", null, @".\AddIn\JCodes.Framework.AddIn.dll", @"JCodes.Framework.AddIn.XtraForm2");
        }
    }
}
