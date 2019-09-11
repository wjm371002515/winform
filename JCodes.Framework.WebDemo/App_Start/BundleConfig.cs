using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Optimization;

namespace JCodes.Framework.WebDemo
{
    /// <summary>
    /// BundleConfig用来将js和css进行合并与压缩，（多个文件可以打包成一个文件），并且可以区分调试和非调试。
    /// 在调试时不进行压缩，以原始方式显示出来，以方便查找问题。
    /// </summary>
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //StyleBundle css_metronic = new StyleBundle("~/Content/themes/css");
            //css_metronic.Include("~/Content/themes/css/bootstrap.min.css?v=3.3.5");


            //ScriptBundle js_metronic = new ScriptBundle("~/Scripts");
            //js_metronic.Orderer = new AsIsBundleOrderer();//按添加先后次序排列，否则容易出现：Uncaught ReferenceError: jQuery is not defined
            //js_metronic.Include("~/Content/metronic/assets/global/plugins/bootstrap/js/bootstrap.js");

            //bundles.Add(css_metronic);
            //bundles.Add(js_metronic);

            //当进行css和JS压缩处理，不过会导致图片路径有问题，停用！！
            BundleTable.EnableOptimizations = false;
        }
    }

    /// <summary>
    /// 自定义Bundles排序
    /// </summary>
    internal class AsIsBundleOrderer : IBundleOrderer
    {
        public virtual IEnumerable<FileInfo> OrderFiles(BundleContext context, IEnumerable<FileInfo> files)
        {
            return files;
        }
    }
    internal static class BundleExtensions
    {
        public static Bundle ForceOrdered(this Bundle sb)
        {
            sb.Orderer = new AsIsBundleOrderer();
            return sb;
        }
    }
}