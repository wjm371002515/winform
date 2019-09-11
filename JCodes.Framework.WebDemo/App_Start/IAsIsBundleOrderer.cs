using System;
namespace JCodes.Framework.WebDemo.App_Start
{
    interface IAsIsBundleOrderer
    {
        global::System.Collections.Generic.IEnumerable<global::System.IO.FileInfo> OrderFiles(global::System.Web.Optimization.BundleContext context, global::System.Collections.Generic.IEnumerable<global::System.IO.FileInfo> files);
    }
}
