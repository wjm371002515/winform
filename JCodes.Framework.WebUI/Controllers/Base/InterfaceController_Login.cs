using JCodes.Framework.Common;
using JCodes.Framework.Common.Encrypt;
using JCodes.Framework.Common.Files;
using System.Web.Mvc;

namespace JCodes.Framework.WebDemo.Controllers
{
    /// <summary>
    /// 允许跨域
    /// </summary>
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            base.OnActionExecuting(filterContext);
        }
    }

    public partial class InterfaceController : Controller
    {
        [HttpGet]
        [AllowCrossSiteJson]
        public JsonResult GetPublicKey(string appid)
        {
            LogHelper.WriteLog(jCodesenum.LogLevel.LOG_LEVEL_DEBUG, "输入参数 [appid: " + appid + "]", typeof(InterfaceController));
            // TODO 查询数据库 appid
            AppConfig config = new AppConfig();

            string publicKey = config.AppConfigGet("WX_PublicKey").Replace("&lt;", "<").Replace("&gt;", ">");
            var rsa = new RSA(publicKey);
            // 生成公钥
            publicKey = rsa.ToPEM_PKCS1(true);

            LogHelper.WriteLog(jCodesenum.LogLevel.LOG_LEVEL_DEBUG, "输出参数 [errCode: 0, errMsg: " + publicKey + "]:", typeof(InterfaceController));
            return Json(new { errCode = 0, errMsg = publicKey }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Login(string login_info, string app_id)
        {
            LogHelper.WriteLog(jCodesenum.LogLevel.LOG_LEVEL_DEBUG, "输入参数 [login_info: " + login_info + ", app_id: " + app_id + "]", typeof(InterfaceController));
            // TODO 查询数据库 appid
            AppConfig config = new AppConfig();

            string privateKey = config.AppConfigGet("WX_PrivateKey").Replace("&lt;", "<").Replace("&gt;", ">");
            var rsa = new RSA(privateKey);
            string str_login_info = rsa.DecodeOrNull(login_info);

            LogHelper.WriteLog(jCodesenum.LogLevel.LOG_LEVEL_DEBUG, "输出参数 [str_login_info: " + str_login_info + "]:", typeof(InterfaceController));

            return Json(new { errCode = 0, errMsg = str_login_info }, JsonRequestBehavior.AllowGet);
        }
    }
}
