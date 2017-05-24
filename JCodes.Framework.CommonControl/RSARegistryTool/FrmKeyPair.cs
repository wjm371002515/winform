using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace JCodes.Framework.CommonControl.RSARegistryTool
{
    public partial class FrmKeyPair : Form
    {
        public FrmKeyPair()
        {
            InitializeComponent();

            GenerateKey();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateKey();
        }

        private void GenerateKey()
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // 公钥 
                string pubkey = rsa.ToXmlString(false);
                this.txtPublicKey.Text = pubkey;
                // 私钥 
                string prikey = rsa.ToXmlString(true);
                this.txtPrivateKey.Text = prikey;
            }
        }
    }
}
