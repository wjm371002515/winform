using JCodes.Framework.AddIn.Basic;
using JCodes.Framework.Common.Encrypt;
using JCodes.Framework.Common.Files;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Entity;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace JCodes.Framework.TestWinForm
{
    public partial class BizControlForm : BaseDock
    {
        private string TestGuid = "28150621-a65a-412c-bad7-c61e2bee7951"; 

        public BizControlForm()
        {
            InitializeComponent();

            this.attachmentControl1.AttachmentGid = TestGuid;

            UserInfo info = new UserInfo();
            info.CompanyId = 2;
            Portal.gc.UserInfo = info;

        }

        private void btnAttachment_Click(object sender, System.EventArgs e)
        {
            FrmAttachment dlg = new FrmAttachment();
            dlg.UserId = 1;
            dlg.ShowDialog();
        }

        private string _privateKey = string.Empty;
        private string _publicKey = string.Empty;
       
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, System.EventArgs e)
        {
            AppConfig config = new AppConfig();
            /*RSASecurityHelper.GenerateRSAKey(out _privateKey,out _publicKey);
            string abc = textBox4.Text.Trim();
            textBox4.Text = RSASecurityHelper.RSAEncrypt(_publicKey, abc);*/
            string piKey = config.AppConfigGet("WX_PrivateKey").Replace("&lt;", "<").Replace("&gt;", ">");
            string puey = config.AppConfigGet("WX_PublicKey").Replace("&lt;", "<").Replace("&gt;", ">");

            string privateStr = RSASecurityHelper.RSAEncrypt(puey, "吴建明");
            string publicStr = RSASecurityHelper.RSADecrypt(piKey, privateStr);
            Console.WriteLine(publicStr);


            string decryptStr = "xHYQsXsYuWWyzIJkiLBvcBJCS+EwohLnwsn0cRWMLaKsL4On+l7O/owGt7YiGDUp/sUerqO39b1cHgr+1lBiBeONd8YPMjAgYPCrW7ecr67tReyTwRraIFLp/5dJomQxc9EFPsBQ5au3wXLLH5dYZFXF7ZXqOtdkNFtYi1y/pAM=";
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, System.EventArgs e)
        {
            string abc = textBox4.Text.Trim();
            textBox4.Text = RSASecurityHelper.RSADecrypt(_privateKey, abc);
        }

        private RSACryptoServiceProvider CreateRsaProviderFromPrivateKey(string privateKey)
        {
            var privateKeyBits = System.Convert.FromBase64String(privateKey);

            var RSA = new RSACryptoServiceProvider();
            var RSAparams = new RSAParameters();

            using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                byte bt = 0;
                ushort twobytes = 0;
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");

                bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");

                RSAparams.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.D = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.P = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Q = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DP = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DQ = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }

            RSA.ImportParameters(RSAparams);
            return RSA;
        }

        private int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();
            else
                if (bt == 0x82)
                {
                    highbyte = binr.ReadByte();
                    lowbyte = binr.ReadByte();
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                    count = BitConverter.ToInt32(modint, 0);
                }
                else
                {
                    count = bt;
                }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }

  
    }
}
