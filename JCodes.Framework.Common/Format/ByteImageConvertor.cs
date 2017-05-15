using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 提供字节数组和图片之间的转换辅助类
    /// </summary>
    public sealed class ByteImageConvertor
    {
        private ByteImageConvertor()
        {
        }

        /// <summary>
        /// 把Image对象转换为Byte数组
        /// </summary>
        /// <param name="image">图片Image对象</param>
        /// <returns>字节集合</returns>
        public static byte[] ImageToBytes(Image image)
        {
            byte[] bytes = null;
            if (image != null)
            {
                lock (image)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        bytes = ms.GetBuffer();
                    }
                }
            }
            return bytes;
        }

        /// <summary>
        /// 把Image对象转换为Byte数组
        /// </summary>
        /// <param name="image">image对象</param>
        /// <param name="imageFormat">图片格式（后缀名）</param>
        /// <returns></returns>
        public static byte[] ImageToBytes(Image image, ImageFormat imageFormat)
        {
            if (image == null) { return null; }
            byte[] data = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bitmap = new Bitmap(image))
                {
                    bitmap.Save(ms, imageFormat);
                    ms.Position = 0;
                    data = new byte[ms.Length];
                    ms.Read(data, 0, Convert.ToInt32(ms.Length));
                    ms.Flush();
                }
            }
            return data;
        }

        /// <summary>
        /// 转换Byte数组到Image对象
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>Image图片</returns>
        public static Image ImageFromBytes(byte[] bytes)
        {
            Image image = null;
            try
            {
                if (bytes != null)
                {
                    MemoryStream ms = new MemoryStream(bytes, false);
                    using (ms)
                    {
                        image = ImageFromStream(ms);
                    }
                }
            }
            catch
            {
            }
            return image;
        }

        /// <summary>
        /// 转换地址（文件路径或者URL地址）到Image对象
        /// </summary>
        /// <param name="url">图片地址（文件路径或者URL地址）</param>
        /// <returns>Image对象</returns>
        public static Image ImageFromUrl(string url)
        {
            Image image = null;
            try
            {
                if (!String.IsNullOrEmpty(url))
                {
                    Uri uri = new Uri(url);
                    if (uri.IsFile)
                    {
                        FileStream fs = new FileStream(uri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        using (fs)
                        {
                            image = ImageFromStream(fs);
                        }
                    }
                    else
                    {
                        System.Net.WebClient wc = new System.Net.WebClient();   // TODO: consider changing this to WebClientEx
                        using (wc)
                        {
                            byte[] bytes = wc.DownloadData(uri);
                            MemoryStream ms = new MemoryStream(bytes, false);
                            using (ms)
                            {
                                image = ImageFromStream(ms);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return image;
        }

        /// <summary>
        /// 从流转换为Image对象
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static Image ImageFromStream(Stream stream)
        {
            Image image = null;
            try
            {
                stream.Position = 0;
                Image tempImage = System.Drawing.Bitmap.FromStream(stream);
                // dont close stream yet, first create a copy
                using (tempImage)
                {
                    image = new Bitmap(tempImage);
                }
            }
            catch
            {
                // 当文件为.ico图标文件的时候，上面操作无效，继续转换
                try
                {
                    stream.Position = 0;
                    Icon icon = new Icon(stream);
                    if (icon != null) image = icon.ToBitmap();
                }
                catch
                { }
            }

            return image;
        }

        /// <summary>
        /// byte[]数组转换为Bitmap
        /// </summary>
        /// <param name="bytes">byte[]数组</param>
        /// <returns></returns>
        public static Bitmap BitmapFromBytes(byte[] bytes)
        {
            Bitmap bitmap = null;
            try
            {
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    bitmap = new Bitmap((Image)new Bitmap(stream));
                }
            }
            catch { }

            return bitmap;
        }

        /// <summary>
        /// Bitmap对象转换为byte 数组
        /// </summary>
        /// <param name="bitmap">Bitmap对象</param>
        /// <returns></returns>
        public static byte[] BitmapToBytes(Bitmap bitmap)
        {
            byte[] byteImage = null;
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, bitmap.RawFormat);

                    byteImage = new Byte[stream.Length];
                    byteImage = stream.ToArray();
                }
            }
            catch { }

            return byteImage;
        }
    }
}