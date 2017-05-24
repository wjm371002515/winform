using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace JCodes.Framework.Common.Images
{
    /// <summary>
    /// 操作程序集文件中的图片、光标、图标、位图等资源的辅助类
    /// </summary>
    public class ResourceHelper
    {
        /// <summary>
        /// 从程序集中获取对应的光标对象
        /// </summary>
        /// <param name="assemblyType">程序集类型</param>
        /// <param name="cursorName">光标名称</param>
        /// <returns></returns>
        public static Cursor LoadCursor(Type assemblyType, string cursorName)
        {
            //获取包含位图资源的程序集对象
            Assembly myAssembly = Assembly.GetAssembly(assemblyType);

            //获取包含图片资源Stream对象
            Stream iconStream = myAssembly.GetManifestResourceStream(cursorName);

            //从流对象中加载光标对象
            return new Cursor(iconStream);
        }

        /// <summary>
        /// 从程序集中获取对应的图标对象
        /// </summary>
        /// <param name="assemblyType">程序集类型</param>
        /// <param name="iconName">图标名称</param>
        /// <returns></returns>
        public static Icon LoadIcon(Type assemblyType, string iconName)
        {
            //获取包含位图资源的程序集对象
            Assembly myAssembly = Assembly.GetAssembly(assemblyType);

            //获取包含图片资源Stream对象
            Stream iconStream = myAssembly.GetManifestResourceStream(iconName);

            //从流对象中加载图标对象
            return new Icon(iconStream);
        }

        /// <summary>
        /// 从程序集中获取对应的图标对象，指定大小。
        /// </summary>
        /// <param name="assemblyType">程序集类型</param>
        /// <param name="iconName">图标名称</param>
        /// <param name="iconSize">图标大小</param>
        /// <returns></returns>
        public static Icon LoadIcon(Type assemblyType, string iconName, Size iconSize)
        {
            // Load the entire Icon requested (may include several different Icon sizes)
            Icon rawIcon = LoadIcon(assemblyType, iconName);

            // Create and return a new Icon that only contains the requested size
            return new Icon(rawIcon, iconSize);
        }

        /// <summary>
        /// 从程序集中获取对应的位图对象
        /// </summary>
        /// <param name="assemblyType">程序集类型</param>
        /// <param name="imageName">位图名称</param>
        /// <returns></returns>
        public static Bitmap LoadBitmap(Type assemblyType, string imageName)
        {
            return LoadBitmap(assemblyType, imageName, false, new Point(0, 0));
        }

        /// <summary>
        /// 从程序集中获取对应的位图对象
        /// </summary>
        /// <param name="assemblyType">程序集类型</param>
        /// <param name="imageName">位图名称</param>
        /// <param name="transparentPixel">位图中指定像素的透明颜色</param>
        /// <returns></returns>
        public static Bitmap LoadBitmap(Type assemblyType, string imageName, Point transparentPixel)
        {
            return LoadBitmap(assemblyType, imageName, true, transparentPixel);
        }

        /// <summary>
        /// 从程序集中获取对应的位图对象
        /// </summary>
        /// <param name="assemblyType">程序集类型</param>
        /// <param name="imageName">位图名称</param>
        /// <param name="imageSize">位图大小</param>
        /// <returns></returns>
        public static ImageList LoadBitmapStrip(Type assemblyType, string imageName, Size imageSize)
        {
            return LoadBitmapStrip(assemblyType, imageName, imageSize, false, new Point(0, 0));
        }

        /// <summary>
        /// 从程序集中获取对应的位图对象集合
        /// </summary>
        /// <param name="assemblyType">程序集类型</param>
        /// <param name="imageName">位图名称</param>
        /// <param name="imageSize">位图大小</param>
        /// <param name="transparentPixel">位图中指定像素的透明颜色</param>
        /// <returns></returns>
        public static ImageList LoadBitmapStrip(Type assemblyType, string imageName,
            Size imageSize, Point transparentPixel)
        {
            return LoadBitmapStrip(assemblyType, imageName, imageSize, true, transparentPixel);
        }

        /// <summary>
        /// 从程序集中获取对应的位图对象
        /// </summary>
        /// <param name="assemblyType">程序集类型</param>
        /// <param name="imageName">位图名称</param>
        /// <param name="makeTransparent">是否设置透明色</param>
        /// <param name="transparentPixel">位图中指定像素的透明颜色</param>
        /// <returns></returns>
        protected static Bitmap LoadBitmap(Type assemblyType, string imageName,
            bool makeTransparent, Point transparentPixel)
        {
            // Get the assembly that contains the bitmap resource
            Assembly myAssembly = Assembly.GetAssembly(assemblyType);

            // Get the resource stream containing the images
            Stream imageStream = myAssembly.GetManifestResourceStream(imageName);

            // Load the bitmap from stream
            Bitmap image = new Bitmap(imageStream);
            if (makeTransparent)
            {
                Color backColor = image.GetPixel(transparentPixel.X, transparentPixel.Y);

                // Make backColor transparent for Bitmap
                image.MakeTransparent(backColor);
            }

            return image;
        }

        /// <summary>
        /// 从程序集中获取对应的位图对象集合
        /// </summary>
        /// <param name="assemblyType">程序集类型</param>
        /// <param name="imageName">位图名称</param>
        /// <param name="imageSize">位图大小</param>
        /// <param name="makeTransparent">是否设置透明色</param>
        /// <param name="transparentPixel">位图中指定像素的透明颜色</param>
        /// <returns></returns>
        protected static ImageList LoadBitmapStrip(Type assemblyType, string imageName,
            Size imageSize, bool makeTransparent, Point transparentPixel)
        {
            // Create storage for bitmap strip
            ImageList images = new ImageList();

            // Define the size of images we supply
            images.ImageSize = imageSize;

            // Get the assembly that contains the bitmap resource
            Assembly myAssembly = Assembly.GetAssembly(assemblyType);

            // Get the resource stream containing the images
            Stream imageStream = myAssembly.GetManifestResourceStream(imageName);

            // Load the bitmap strip from resource
            Bitmap pics = new Bitmap(imageStream);

            if (makeTransparent)
            {
                Color backColor = pics.GetPixel(transparentPixel.X, transparentPixel.Y);

                // Make backColor transparent for Bitmap
                pics.MakeTransparent(backColor);
            }

            // Load them all !
            images.Images.AddStrip(pics);

            return images;
        }
    }
}
