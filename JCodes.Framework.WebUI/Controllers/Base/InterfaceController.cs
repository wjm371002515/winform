using JCodes.Framework.BLL;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using System.Web.Mvc;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.WebDemo.Controllers
{
    public partial class InterfaceController : Controller
    {
         /// <summary>
        /// 前端错误信息收集
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void AjaxPostJsErr(InputClass model)
        {
            // 错误信息  model.DataType
            // 错误地址 InputParam: scriptURI,
            // 错误行号 Data1: lineNo,
            // 错误列 Data2: columNo,
            // 时间戳(秒) TimeStamp: Date.parse(new Date()) / 1000
            // TODO
            FrontLogInfo frontLogInfo = new FrontLogInfo();
            frontLogInfo.ErrorMessage = model.DataType;
            frontLogInfo.ErrorLineNo =  model.Data1.ToInt32();
            frontLogInfo.ErrorColumNo = model.Data2.ToInt32();
            frontLogInfo.TimeStamp = model.TimeStamp;
            frontLogInfo.ErrorPath = model.InputParam;
            BLLFactory<FrontLog>.Instance.Insert2(frontLogInfo);
        }
    }
}
