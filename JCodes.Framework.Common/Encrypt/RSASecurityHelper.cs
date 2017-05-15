using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 非对称加密、解密、验证辅助类
    /// </summary>
    public class RSASecurityHelper
    {
        /// <summary>
        /// 非对称加密生成的私钥和公钥 
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="publicKey">公钥</param>
        public static void GenerateRSAKey(out string privateKey, out string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            privateKey = rsa.ToXmlString(true);
            publicKey = rsa.ToXmlString(false);
        }
        
        #region 非对称数据加密（公钥加密）

        /// <summary>
        /// 非对称加密字符串数据，返回加密后的数据
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="originalString">待加密的字符串</param>
        /// <returns>加密后的数据</returns>
        public static string RSAEncrypt(string publicKey, string originalString)
        {
            byte[] PlainTextBArray;
            byte[] CypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            PlainTextBArray = (new UnicodeEncoding()).GetBytes(originalString);
            CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
            Result = Convert.ToBase64String(CypherTextBArray);
            return Result;
        }

        /// <summary>
        /// 非对称加密字节数组，返回加密后的数据
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="originalBytes">待加密的字节数组</param>
        /// <returns>返回加密后的数据</returns>
        public static string RSAEncrypt(string publicKey, byte[] originalBytes)
        {
            byte[] CypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            CypherTextBArray = rsa.Encrypt(originalBytes, false);
            Result = Convert.ToBase64String(CypherTextBArray);
            return Result;
        } 

        #endregion

        #region 非对称解密（私钥解密）

        /// <summary>
        /// 非对称解密字符串，返回解密后的数据
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="encryptedString">待解密数据</param>
        /// <returns>返回解密后的数据</returns>
        public static string RSADecrypt(string privateKey, string encryptedString)
        {
            byte[] PlainTextBArray;
            byte[] DypherTextBArray;
            string Result;
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            PlainTextBArray = Convert.FromBase64String(encryptedString);
            DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
            Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
            return Result;
        }

        /// <summary>
        /// 非对称解密字节数组，返回解密后的数据
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="encryptedBytes">待解密数据</param>
        /// <returns></returns>
        public static string RSADecrypt(string privateKey, byte[] encryptedBytes)
        {
            byte[] DypherTextBArray;
            string Result;
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            DypherTextBArray = rsa.Decrypt(encryptedBytes, false);
            Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
            return Result;
        } 
        #endregion

        #region 非对称加密签名、验证

        /// <summary>
        /// 使用非对称加密签名数据
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="originalString">待加密的字符串</param>
        /// <returns>加密后的数据</returns>
        public static string RSAEncrypSignature(string privateKey, string originalString)
        {
            string signature;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey); //私钥
                // 加密对象 
                RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsa);
                f.SetHashAlgorithm("SHA1");
                byte[] source = ASCIIEncoding.ASCII.GetBytes(originalString);
                SHA1Managed sha = new SHA1Managed();
                byte[] result = sha.ComputeHash(source);
                byte[] b = f.CreateSignature(result);
                signature = Convert.ToBase64String(b);
            }
            return signature;
        }

        /// <summary>
        /// 对私钥加密签名的字符串，使用公钥对其进行验证
        /// </summary>
        /// <param name="originalString">未加密的文本，如机器码</param>
        /// <param name="encrytedString">加密后的文本，如注册序列号</param>
        /// <returns>如果验证成功返回True，否则为False</returns>
        public static bool Validate(string originalString, string encrytedString)
        {
            return Validate(originalString, encrytedString, UIConstants.PublicKey);
        }

        /// <summary>
        /// 对私钥加密的字符串，使用公钥对其进行验证
        /// </summary>
        /// <param name="originalString">未加密的文本，如机器码</param>
        /// <param name="encrytedString">加密后的文本，如注册序列号</param>
        /// <param name="publicKey">非对称加密的公钥</param>
        /// <returns>如果验证成功返回True，否则为False</returns>
        public static bool Validate(string originalString, string encrytedString, string publicKey)
        {
            bool bPassed = false;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    rsa.FromXmlString(publicKey); //公钥
                    RSAPKCS1SignatureDeformatter formatter = new RSAPKCS1SignatureDeformatter(rsa);
                    formatter.SetHashAlgorithm("SHA1");

                    byte[] key = Convert.FromBase64String(encrytedString); //验证
                    SHA1Managed sha = new SHA1Managed();
                    byte[] name = sha.ComputeHash(ASCIIEncoding.ASCII.GetBytes(originalString));
                    if (formatter.VerifySignature(name, key))
                    {
                        bPassed = true;
                    }
                }
                catch
                {
                }
            }
            return bPassed;
        }

        #endregion

        #region Hash 加密

        /// <summary> Hash 加密 </summary>
        /// <param name="str2Hash"></param>
        /// <returns></returns>
        public static int HashEncrypt(string str2Hash)
        {
            str2Hash += Const.strHash;       // 增加一个常量字符串

            int len = str2Hash.Length;
            int result = (str2Hash[len - 1] - 31) * 95 + Const.salt;
            for (int i = 0; i < len - 1; i++)
            {
                result = (result * 95) + (str2Hash[i] - 32);
            }

            return result;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">待加密字串</param>
        /// <returns>加密后的字串</returns>
        public static string ComputeMD5(string str)
        {
            byte[] hashValue = ComputeMD5Data(str);
            return BitConverter.ToString(hashValue).Replace("-", "");
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">待加密字串</param>
        /// <returns>加密后的字串</returns>
        public static byte[] ComputeMD5Data(string input)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(input);
            return MD5.Create().ComputeHash(buffer);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="data">待加密数据</param>
        /// <returns>加密后的字串</returns>
        public static byte[] ComputeMD5Data(byte[] data)
        {
            return MD5.Create().ComputeHash(data);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="stream">待加密流</param>
        /// <returns>加密后的字串</returns>
        public static byte[] ComputeMD5Data(Stream stream)
        {
            return MD5.Create().ComputeHash(stream);
        }

        /// <summary>
        /// 返回注册码信息 (用公钥生成)
        /// </summary>
        /// <returns></returns>
        public static string GetRegistrationCode()
        {
            string machineCode = HardwareInfoHelper.GetCPUId();
            string expireDate = Convert.ToDateTime(Data.getSysDate()).AddYears(1).ToString("yyyy-MM-dd");
            string publicKey = Const.publicKey;
            return RSASecurityHelper.RSAEncrypt(publicKey, machineCode + Const.VerticalLine + expireDate);
        }

  
        /// <summary>
        /// 验证注册码信息
        /// </summary>
        /// <param name="encryptedString"> 注册码信息 </param>
        /// <returns>返回0 表示正确，-1表示注册码错误，-2表示机器码错误， -3表示注册已过期， -4表示其他内部错误</returns>
        public static Int32 CheckRegistrationCode(string encryptedString)
        {
            string originalString = null;
            try
            {
                originalString = RSASecurityHelper.RSADecrypt(Const.privateKey, encryptedString);
            }
            catch (Exception ex)
            {
                return -1;
            }

            string[] d2 = originalString.Split('|');
            if (d2.Length != 2)
                return -4;

            string machineCode = d2[0];
            string expireDate = d2[1];
            if (!string.Equals(machineCode, HardwareInfoHelper.GetCPUId(), StringComparison.OrdinalIgnoreCase))
                return -2;
            
            DateTime nowdt = Convert.ToDateTime( Data.getSysDate() );
            DateTime expiredt = Convert.ToDateTime( expireDate);
            if (nowdt > expiredt)
                return -3;

            return 0;
        }
        #endregion
    }
}