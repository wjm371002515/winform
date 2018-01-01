using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JCodes.Framework.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            InitData();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            // 取值 var dict = JCodes.Framework.Common.Office.Cache.Instance["Dict"] as Dictionary<Int32, String>;
            JCodes.Framework.Common.Office.Cache.Instance["Dict"] = new JCodes.Framework.Common.ErrorInfo().GetErrorInfo();
        }
    }
}