using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Timers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JCodes.Framework.WebDemo
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
        }

        
   　    
        protected void Application_End(object sender, EventArgs e) 
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_INFO, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":Application_End!", typeof(MvcApplication));

   　       //下面的代码是关键，可解决IIS应用程序池自动回收的问题
   　       Thread.Sleep(1000);
   　       //这里设置你的web地址，可以随便指向你的任意一个aspx页面甚至不存在的页面，目的是要激发Application_Start
            /*string url = "https://jcodes.cn/Home/c8903ed406ee4adf9470638f8eaf6fdc"; 
   　       HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);  
      
   　       HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();  
      
   　       Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流*/
        }   
    }
}