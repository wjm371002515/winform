namespace JCodes.Framework.AddIn.Basic
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup2 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.applicationMenu1 = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.btnExit = new DevExpress.XtraBars.BarButtonItem();
            this.btnRelogin = new DevExpress.XtraBars.BarButtonItem();
            this.btnModPwd = new DevExpress.XtraBars.BarButtonItem();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.rgbiSkins = new DevExpress.XtraBars.RibbonGalleryBarItem();
            this.progressBar = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemProgressBar3 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.menuLogo = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.lblCommandStatus = new DevExpress.XtraBars.BarStaticItem();
            this.lblCalendar = new DevExpress.XtraBars.BarStaticItem();
            this.lblCurrentUser = new DevExpress.XtraBars.BarStaticItem();
            this.popMenuCloseCurrent = new DevExpress.XtraBars.BarButtonItem();
            this.popMenuCloseAll = new DevExpress.XtraBars.BarButtonItem();
            this.popMenuCloseOther = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.repositoryItemProgressBar2 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.notifyMenu_Show = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyMenu_About = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyMenu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ApplicationButtonDropDownControl = this.applicationMenu1;
            this.ribbonControl.ApplicationButtonText = null;
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Images = this.imageCollection1;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.rgbiSkins,
            this.btnExit,
            this.progressBar,
            this.menuLogo,
            this.lblCommandStatus,
            this.lblCalendar,
            this.lblCurrentUser,
            this.btnRelogin,
            this.popMenuCloseCurrent,
            this.popMenuCloseAll,
            this.popMenuCloseOther,
            this.barButtonItem2,
            this.barButtonItem1,
            this.btnModPwd});
            this.ribbonControl.LargeImages = this.imageCollection1;
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ribbonControl.MaxItemId = 68;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.PageHeaderItemLinks.Add(this.menuLogo, true);
            this.ribbonControl.PageHeaderItemLinks.Add(this.barButtonItem2);
            this.ribbonControl.PageHeaderItemLinks.Add(this.barButtonItem1);
            this.ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1,
            this.repositoryItemProgressBar2,
            this.repositoryItemProgressBar3,
            this.repositoryItemPictureEdit1});
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.ribbonControl.ShowToolbarCustomizeItem = false;
            this.ribbonControl.Size = new System.Drawing.Size(1311, 63);
            this.ribbonControl.StatusBar = this.ribbonStatusBar1;
            this.ribbonControl.Toolbar.ShowCustomizeItem = false;
            // 
            // applicationMenu1
            // 
            this.applicationMenu1.ItemLinks.Add(this.btnExit);
            this.applicationMenu1.ItemLinks.Add(this.btnRelogin);
            this.applicationMenu1.ItemLinks.Add(this.btnModPwd);
            this.applicationMenu1.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.LargeImagesText;
            this.applicationMenu1.MinWidth = 240;
            this.applicationMenu1.Name = "applicationMenu1";
            this.applicationMenu1.Ribbon = this.ribbonControl;
            // 
            // btnExit
            // 
            this.btnExit.Caption = "退出系统(&Q)";
            this.btnExit.Id = 22;
            this.btnExit.ImageIndex = 23;
            this.btnExit.LargeImageIndex = 23;
            this.btnExit.Name = "btnExit";
            this.btnExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExit_ItemClick);
            // 
            // btnRelogin
            // 
            this.btnRelogin.Caption = "重新登录(&R)";
            this.btnRelogin.Id = 57;
            this.btnRelogin.ImageIndex = 10;
            this.btnRelogin.LargeImageIndex = 10;
            this.btnRelogin.Name = "btnRelogin";
            this.btnRelogin.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRelogin_ItemClick);
            // 
            // btnModPwd
            // 
            this.btnModPwd.Caption = "修改密码";
            this.btnModPwd.Id = 67;
            this.btnModPwd.ImageIndex = 11;
            this.btnModPwd.Name = "btnModPwd";
            this.btnModPwd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnModPwd_ItemClick);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "add.ico");
            this.imageCollection1.Images.SetKeyName(1, "remove.ico");
            this.imageCollection1.Images.SetKeyName(2, "search.ico");
            this.imageCollection1.Images.SetKeyName(3, "Search2.ICO");
            this.imageCollection1.Images.SetKeyName(4, "cubes_class libraries4.ico");
            this.imageCollection1.Images.SetKeyName(5, "new_file.ico");
            this.imageCollection1.Images.SetKeyName(6, "Excel.ICO");
            this.imageCollection1.Images.SetKeyName(7, "Setting_3.ico");
            this.imageCollection1.Images.SetKeyName(8, "Books.ico");
            this.imageCollection1.Images.SetKeyName(9, "User.ICO");
            this.imageCollection1.Images.SetKeyName(10, "Security.ico");
            this.imageCollection1.Images.SetKeyName(11, "Security2.ICO");
            this.imageCollection1.Images.SetKeyName(12, "order.ico");
            this.imageCollection1.Images.SetKeyName(13, "Diagram.ico");
            this.imageCollection1.Images.SetKeyName(14, "XP-calendar.png");
            this.imageCollection1.Images.SetKeyName(15, "Stock2.ICO");
            this.imageCollection1.Images.SetKeyName(16, "Chat.ico");
            this.imageCollection1.Images.SetKeyName(17, "Box_Blue.png");
            this.imageCollection1.Images.SetKeyName(18, "info16.png");
            this.imageCollection1.Images.SetKeyName(19, "Help_Circle_Blue.png");
            this.imageCollection1.Images.SetKeyName(20, "Info_Box_Blue.png");
            this.imageCollection1.Images.SetKeyName(21, "Customer.ICO");
            this.imageCollection1.Images.SetKeyName(22, "house.ico");
            this.imageCollection1.Images.SetKeyName(23, "Quit.ico");
            this.imageCollection1.Images.SetKeyName(24, "Contact.ico");
            this.imageCollection1.Images.SetKeyName(25, "user004.ico");
            this.imageCollection1.Images.SetKeyName(26, "Addressbook.ico");
            this.imageCollection1.Images.SetKeyName(27, "Refresh.ico");
            // 
            // rgbiSkins
            // 
            this.rgbiSkins.Caption = "ribbonGalleryBarItem1";
            // 
            // 
            // 
            galleryItemGroup2.Caption = "Group1";
            this.rgbiSkins.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            galleryItemGroup2});
            this.rgbiSkins.Id = 21;
            this.rgbiSkins.Name = "rgbiSkins";
            // 
            // progressBar
            // 
            this.progressBar.Edit = this.repositoryItemProgressBar3;
            this.progressBar.EditValue = 50;
            this.progressBar.Id = 30;
            this.progressBar.Name = "progressBar";
            this.progressBar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.progressBar.Width = 200;
            // 
            // repositoryItemProgressBar3
            // 
            this.repositoryItemProgressBar3.Name = "repositoryItemProgressBar3";
            // 
            // menuLogo
            // 
            this.menuLogo.CanOpenEdit = false;
            this.menuLogo.Edit = this.repositoryItemPictureEdit1;
            this.menuLogo.Hint = "访问技术网站";
            this.menuLogo.Id = 51;
            this.menuLogo.ImageIndex = 18;
            this.menuLogo.Name = "menuLogo";
            toolTipItem3.Text = "访问技术支持网站";
            superToolTip3.Items.Add(toolTipItem3);
            this.menuLogo.SuperTip = superToolTip3;
            this.menuLogo.Width = 0;
            this.menuLogo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuLogo_ItemClick);
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.InitialImage = null;
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.NullText = " ";
            this.repositoryItemPictureEdit1.ReadOnly = true;
            this.repositoryItemPictureEdit1.ShowMenu = false;
            // 
            // lblCommandStatus
            // 
            this.lblCommandStatus.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.lblCommandStatus.Id = 52;
            this.lblCommandStatus.Name = "lblCommandStatus";
            this.lblCommandStatus.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // lblCalendar
            // 
            this.lblCalendar.Caption = "2010-11-12 0:00:00";
            this.lblCalendar.Id = 53;
            this.lblCalendar.Name = "lblCalendar";
            this.lblCalendar.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.Description = "查看/编辑当前用户信息";
            this.lblCurrentUser.Id = 55;
            this.lblCurrentUser.Name = "lblCurrentUser";
            toolTipTitleItem2.Text = "提示信息";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "单击该处，可以进行查看/编辑当前用户信息操作。";
            superToolTip1.Items.Add(toolTipTitleItem2);
            superToolTip1.Items.Add(toolTipItem1);
            this.lblCurrentUser.SuperTip = superToolTip1;
            this.lblCurrentUser.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // popMenuCloseCurrent
            // 
            this.popMenuCloseCurrent.Caption = "关闭当前窗口(&C)";
            this.popMenuCloseCurrent.Id = 60;
            this.popMenuCloseCurrent.Name = "popMenuCloseCurrent";
            this.popMenuCloseCurrent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.popMenuCloseCurrent_ItemClick);
            // 
            // popMenuCloseAll
            // 
            this.popMenuCloseAll.Caption = "关闭全部(&A)";
            this.popMenuCloseAll.Id = 61;
            this.popMenuCloseAll.Name = "popMenuCloseAll";
            this.popMenuCloseAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.popMenuCloseAll_ItemClick);
            // 
            // popMenuCloseOther
            // 
            this.popMenuCloseOther.Caption = "关闭其他窗口(&O)";
            this.popMenuCloseOther.Id = 62;
            this.popMenuCloseOther.Name = "popMenuCloseOther";
            this.popMenuCloseOther.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.popMenuCloseOther_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "刷新内存";
            this.barButtonItem2.Description = "刷新内存";
            this.barButtonItem2.Hint = "刷新内存";
            this.barButtonItem2.Id = 64;
            this.barButtonItem2.ImageIndex = 27;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "问题反馈";
            this.barButtonItem1.Description = "问题反馈";
            this.barButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.Glyph")));
            this.barButtonItem1.Hint = "问题反馈";
            this.barButtonItem1.Id = 63;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            // 
            // repositoryItemProgressBar2
            // 
            this.repositoryItemProgressBar2.Name = "repositoryItemProgressBar2";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.lblCommandStatus);
            this.ribbonStatusBar1.ItemLinks.Add(this.lblCurrentUser);
            this.ribbonStatusBar1.ItemLinks.Add(this.lblCalendar);
            this.ribbonStatusBar1.ItemLinks.Add(this.progressBar);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 1049);
            this.ribbonStatusBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1311, 37);
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeader;
            this.xtraTabbedMdiManager1.MdiParent = this;
            this.xtraTabbedMdiManager1.PageImagePosition = DevExpress.XtraTab.TabPageImagePosition.Far;
            this.xtraTabbedMdiManager1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.xtraTabbedMdiManager1_MouseDown);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notifyMenu_Show,
            this.notifyMenu_About,
            this.notifyMenu_Exit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(232, 94);
            // 
            // notifyMenu_Show
            // 
            this.notifyMenu_Show.Name = "notifyMenu_Show";
            this.notifyMenu_Show.Size = new System.Drawing.Size(231, 30);
            this.notifyMenu_Show.Text = "显示/隐藏窗体(&S)";
            this.notifyMenu_Show.Click += new System.EventHandler(this.notifyMenu_Show_Click);
            // 
            // notifyMenu_About
            // 
            this.notifyMenu_About.Name = "notifyMenu_About";
            this.notifyMenu_About.Size = new System.Drawing.Size(231, 30);
            this.notifyMenu_About.Text = "关于(&A)";
            this.notifyMenu_About.Click += new System.EventHandler(this.notifyMenu_About_Click);
            // 
            // notifyMenu_Exit
            // 
            this.notifyMenu_Exit.Name = "notifyMenu_Exit";
            this.notifyMenu_Exit.Size = new System.Drawing.Size(231, 30);
            this.notifyMenu_Exit.Text = "退出(&X)";
            this.notifyMenu_Exit.Click += new System.EventHandler(this.notifyMenu_Exit_Click);
            // 
            // popupMenu1
            // 
            this.popupMenu1.ItemLinks.Add(this.popMenuCloseCurrent);
            this.popupMenu1.ItemLinks.Add(this.popMenuCloseAll);
            this.popupMenu1.ItemLinks.Add(this.popMenuCloseOther);
            this.popupMenu1.Name = "popupMenu1";
            this.popupMenu1.Ribbon = this.ribbonControl;
            // 
            // MainForm
            // 
            this.AllowMdiBar = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 1086);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Ribbon = this.ribbonControl;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar1;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MaximizedBoundsChanged += new System.EventHandler(this.MainForm_MaximizedBoundsChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Move += new System.EventHandler(this.MainForm_Move);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.RibbonGalleryBarItem rgbiSkins;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private DevExpress.XtraBars.BarButtonItem btnExit;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar3;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar2;
        private DevExpress.XtraBars.BarEditItem menuLogo;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraBars.BarStaticItem lblCommandStatus;
        private DevExpress.XtraBars.BarStaticItem lblCalendar;
        private DevExpress.XtraBars.BarStaticItem lblCurrentUser;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem btnRelogin;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem popMenuCloseCurrent;
        private DevExpress.XtraBars.BarButtonItem popMenuCloseAll;
        private DevExpress.XtraBars.BarButtonItem popMenuCloseOther;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu applicationMenu1;
        private DevExpress.XtraBars.BarButtonItem btnModPwd;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem notifyMenu_Show;
        private System.Windows.Forms.ToolStripMenuItem notifyMenu_About;
        private System.Windows.Forms.ToolStripMenuItem notifyMenu_Exit;
        public DevExpress.XtraBars.BarEditItem progressBar;
    }
}

