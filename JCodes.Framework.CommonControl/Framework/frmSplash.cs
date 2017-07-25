using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Office;

namespace JCodes.Framework.CommonControl.Framework
{
    public partial class frmSplash : Form, ISplashForm
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        #region ISplashForm

        void ISplashForm.SetStatusInfo(string NewStatusInfo)
        {
            lbStatusInfo.Text = NewStatusInfo;
        }

        #endregion

        private void frmSplash_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                AppConfig config = Cache.Instance["AppConfig"] as AppConfig;
                if (config == null)
                {
                    config = new AppConfig();
                    Cache.Instance["AppConfig"] = config;
                }	
                string picturePath = config.AppConfigGet("SplashScreen");
                if (!string.IsNullOrEmpty(picturePath))
                {
                    string realPath = Path.Combine(Application.StartupPath, picturePath);
                    if (File.Exists(realPath))
                    {
                        this.BackgroundImage = Image.FromFile(realPath);
                        Size newSize = this.BackgroundImage.Size;
                        if(this.BackgroundImage.Size.Width > 800)
                        {
                            newSize = new Size(this.BackgroundImage.Size.Width / 2, this.BackgroundImage.Size.Height / 2);
                        }
                        this.Size = newSize;
                        this.Invalidate();                        
                    }
                }
            }
        }
    }
}