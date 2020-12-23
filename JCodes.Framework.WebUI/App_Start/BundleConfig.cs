using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Optimization;

namespace JCodes.Framework.WebUI
{
    /// <summary>
    /// BundleConfig用来将js和css进行合并与压缩，（多个文件可以打包成一个文件），并且可以区分调试和非调试。
    /// 在调试时不进行压缩，以原始方式显示出来，以方便查找问题。
    /// </summary>
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // 20171229 wjm 删除min前缀，不然会引起资源加载不正常
            //为了减少太多的Bundles命名，定义的CSS的Bundle为："~/Content/css"、"~/Content/jquerytools"
            //定义的Script的Bundles为："~/bundles/jquery"、"~/bundles/jquerytools"
            StyleBundle css_metronic = new StyleBundle("~/metronic/css");
            css_metronic.Include("~/Content/metronic/assets/global/plugins/font-awesome/css/font-awesome.css",          // font-awesome.min.css
               "~/Content/metronic/assets/global/plugins/simple-line-icons/simple-line-icons.css",                      // simple-line-icons.min.css
               "~/Content/metronic/assets/global/plugins/bootstrap/css/bootstrap.css",                                  // bootstrap.min.css
               "~/Content/metronic/assets/global/plugins/uniform/css/uniform.default.css",
               "~/Content/metronic/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.css",                    // bootstrap-switch.min.css

               "~/Content/metronic/assets/global/plugins/bootstrap-select/bootstrap-select.css",                        // bootstrap-select.min.css
               "~/Content/metronic/assets/global/plugins/select2/select2.css",      
               "~/Content/metronic/assets/global/plugins/jquery-multi-select/css/multi-select.css",

               "~/Content/metronic/assets/global/plugins/icheck/skins/all.css",
               "~/Content/MyPlugins/sweetalert/dist/sweetalert.css",

               "~/Content/metronic/assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css",
                "~/Content/metronic/assets/global/plugins/bootstrap-modal/css/bootstrap-modal-bs3patch.css",
                "~/Content/metronic/assets/global/plugins/bootstrap-modal/css/bootstrap-modal.css",

               "~/Content/metronic/assets/global/plugins/jstree/dist/themes/default/style.css",                         // style.min.css

               "~/Content/metronic/assets/global/css/components-rounded.css",
               "~/Content/metronic/assets/global/css/plugins.css",
               "~/Content/metronic/assets/admin/layout2/css/layout.css",
               "~/Content/metronic/assets/admin/layout2/css/themes/blue.css",
               "~/Content/metronic/assets/admin/layout2/css/custom.css",

               //增加自定义图标样式
                "~/Content/icons-customed/16/icon.css",
                "~/Content/icons-customed/24/icon.css",
                "~/Content/icons-customed/32/icon.css"
               );


            ScriptBundle js_metronic = new ScriptBundle("~/metronic/js");
            js_metronic.Orderer = new AsIsBundleOrderer();//按添加先后次序排列，否则容易出现：Uncaught ReferenceError: jQuery is not defined
            js_metronic.Include(
                "~/Content/metronic/assets/global/plugins/bootstrap/js/bootstrap.js",                               // bootstrap.min.js
                "~/Content/metronic/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.js",    // bootstrap-hover-dropdown.min.js
                "~/Content/metronic/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.js",                  // jquery.slimscroll.min.js
                "~/Content/metronic/assets/global/plugins/jquery.blockui-min.js",                                   // jquery.blockui.min.js
                "~/Content/metronic/assets/global/plugins/jquery.cokie-min.js",                                     // jquery.cokie.min.js
                "~/Content/metronic/assets/global/plugins/uniform/jquery.uniform.js",                               // jquery.uniform.min.js
                "~/Content/metronic/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.js",                 // bootstrap-switch.min.js

                "~/Content/metronic/assets/global/plugins/fuelux/js/spinner.js",                                    // spinner.min.js
                "~/Content/metronic/assets/global/plugins/bootstrap-touchspin/bootstrap.touchspin.js",

                "~/Content/metronic/assets/global/plugins/bootstrap-select/bootstrap-select.js",                    // bootstrap-select.min.js
                "~/Content/metronic/assets/global/plugins/select2/select2.js",                                      // select2.min.js
                "~/Content/metronic/assets/global/plugins/select2/select2_locale_zh-CN.js",
                "~/Content/metronic/assets/global/plugins/jquery-multi-select/js/jquery.multi-select.js",

                "~/Content/metronic/assets/global/plugins/datatables/media/js/jquery.dataTables.js",                // jquery.dataTables.min.js
                "~/Content/metronic/assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js",

                "~/Content/metronic/assets/global/plugins/jquery-validation/js/jquery.validate.js",                 // jquery.validate.min.js
                "~/Content/metronic/assets/global/plugins/jquery-validation/js/additional-methods.js",              // additional-methods.min.js
                "~/Content/metronic/assets/global/plugins/jquery-validation/js/localization/messages_zh.js",        // messages_zh.min.js

                "~/Content/metronic/assets/global/plugins/bootbox/bootbox-min.js",                                  // bootbox.min.js
                "~/Content/metronic/assets/global/plugins/bootstrap-modal/js/bootstrap-modalmanager.js",
                "~/Content/metronic/assets/global/plugins/bootstrap-modal/js/bootstrap-modal.js",

                "~/Content/metronic/assets/global/plugins/jstree/dist/jstree.js",                                   // jstree.min.js
                "~/Content/metronic/assets/global/plugins/icheck/icheck.js",                                        // icheck.min.js
                "~/Content/MyPlugins/sweetalert/dist/sweetalert-dev.js",                                            // sweetalert.min.js

                "~/Content/metronic/assets/global/scripts/metronic.js",
                "~/Content/metronic/assets/admin/layout2/scripts/layout.js",
                "~/Content/metronic/assets/admin/layout2/scripts/demo.js",

                "~/Content/metronic/assets/admin/pages/scripts/table-managed.js",
                "~/Content/metronic/assets/admin/pages/scripts/ui-extended-modals.js"
                );


            //引用分页控件控件paginator
            js_metronic.Include("~/Content/MyPlugins/bootstrap-paginator/bootstrap-paginator.js");

            //引用消息提示控件toastr
            css_metronic.Include("~/Content/metronic/assets/global/plugins/bootstrap-toastr/toastr.css");           // toastr.min.css
            js_metronic.Include("~/Content/metronic/assets/global/plugins/bootstrap-toastr/toastr.js");             // toastr.min.js

            //引用消息提示控件jNotify
            css_metronic.Include("~/Content/JQueryTools/jNotify/jquery/jNotify.jquery.css");
            js_metronic.Include("~/Content/JQueryTools/jNotify/jquery/jNotify.jquery.js");

            //Tag标签的控件应用
            css_metronic.Include("~/Content/JQueryTools/Tags-Input/jquery.tagsinput.css");
            js_metronic.Include("~/Content/JQueryTools/Tags-Input/jquery.tagsinput.js");

            //添加对uploadify控件的支持
            css_metronic.Include("~/Content/JQueryTools/uploadify/uploadify.css");
            js_metronic.Include("~/Content/JQueryTools/uploadify/jquery.uploadify.js");

            //添加LODOP控件支持
            js_metronic.Include("~/Content/JQueryTools/LODOP/CheckActivX.js");

            //添加对bootstrap-summernote的支持
            css_metronic.Include("~/Content/metronic/assets/global/plugins/bootstrap-summernote/summernote.css");
            js_metronic.Include("~/Content/metronic/assets/global/plugins/bootstrap-summernote/summernote.js");     // summernote.min.js
            js_metronic.Include("~/Content/metronic/assets/global/plugins/bootstrap-summernote/lang/summernote-zh-CN.js");

            //添加对bootstrap-fileinput控件的支持
            css_metronic.Include("~/Content/MyPlugins/bootstrap-fileinput/css/fileinput.css");                      // fileinput.min.css
            js_metronic.Include("~/Content/MyPlugins/bootstrap-fileinput/js/fileinput.js");                         // fileinput.min.js
            js_metronic.Include("~/Content/MyPlugins/bootstrap-fileinput/js/fileinput_locale_zh.js");

            //添加对fancybox控件的支持
            css_metronic.Include("~/Content/metronic/assets/global/plugins/fancybox/source/jquery.fancybox.css");
            css_metronic.Include("~/Content/metronic/assets/admin/pages/css/portfolio.css");
            js_metronic.Include("~/Content/metronic/assets/global/plugins/jquery-mixitup/jquery.mixitup-min.js");   // jquery.mixitup.min.js
            js_metronic.Include("~/Content/metronic/assets/global/plugins/fancybox/source/jquery.fancybox.pack.js");


            //执行增加的样式
            //css_metronic.Include("~/Content/icons-customed/16/icon.css",
            //    "~/Content/icons-customed/24/icon.css",
            //    "~/Content/icons-customed/32/icon.css",
            //    "~/Content/themes/Default/style.css",
            //    "~/Content/themes/Default/default.css");   
         
            bundles.Add(css_metronic);
            bundles.Add(js_metronic);

            //当进行css和JS压缩处理，不过会导致图片路径有问题，停用！！
            BundleTable.EnableOptimizations = false;
        }
    }

    /// <summary>
    /// 自定义Bundles排序
    /// </summary>
    internal class AsIsBundleOrderer : IBundleOrderer
    {
        public virtual IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
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