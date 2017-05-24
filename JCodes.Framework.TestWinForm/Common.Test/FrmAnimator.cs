using JCodes.Framework.Common;
using JCodes.Framework.Common.Winform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestCommons
{
    public partial class FrmAnimator : Form
    {
        private FullscreenHelper fullScreenHelper;

        public FrmAnimator()
        {
            InitializeComponent();

            fullScreenHelper = new FullscreenHelper(this);
            fullScreenHelper.Fullscreen = true;
        }

        private void FrmAnimator_DoubleClick(object sender, EventArgs e)
        {
            fullScreenHelper.Toggle();
        }
    }
}
