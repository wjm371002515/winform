using JCodes.Framework.BLL;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JCodes.Framework.WebDemo.Controllers
{
    public partial class InterfaceController : Controller
    {
        //
        // GET: /Interface/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAjaxMachines(string NWIP, string WWIP, string JQRT, string GLY, string ZG, Int32 limit, Int32 offset)
        {
            PagerInfo pagerinfo = new PagerInfo();
            pagerinfo.PageSize = limit;
            pagerinfo.CurrenetPageIndex = (offset + limit) / limit;

            List<MachinesInfo> data = new JCodes.Framework.BLL.Machines().GetMachines(NWIP, WWIP, JQRT, GLY, ZG, pagerinfo);

            var total = new JCodes.Framework.BLL.Machines().GetMachinesCount(NWIP, WWIP, JQRT, GLY, ZG);
            var rows = data.ToList();
            return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string GetAjaxIntranet(string key)
        {
            if (string.IsNullOrEmpty(key)) return JsonConvert.SerializeObject(new { value = String.Empty });

            DataTable dt = new JCodes.Framework.BLL.Machines().GetIntranet(key);

            return JsonConvert.SerializeObject(new { value = dt});
        }

        [HttpGet]
        public string GetAjaxWWIP(string key)
        {
            if (string.IsNullOrEmpty(key)) return JsonConvert.SerializeObject(new { value = String.Empty });

            DataTable dt = new JCodes.Framework.BLL.Machines().GetWWIP(key);

            return JsonConvert.SerializeObject(new { value = dt });
        }

        [HttpGet]
        public string GetAjaxJQRT(string key)
        {
            if (string.IsNullOrEmpty(key)) return JsonConvert.SerializeObject(new { value = String.Empty });

            DataTable dt = new JCodes.Framework.BLL.Machines().GetJQRT(key);

            return JsonConvert.SerializeObject(new { value = dt });
        }

        [HttpGet]
        public string GetAjaxGLY(string key)
        {
            if (string.IsNullOrEmpty(key)) return JsonConvert.SerializeObject(new { value = String.Empty });

            DataTable dt = new JCodes.Framework.BLL.Machines().GetGLY(key);

            return JsonConvert.SerializeObject(new { value = dt });
        }

        [HttpGet]
        public string GetAjaxZG(string key)
        {
            if (string.IsNullOrEmpty(key)) return JsonConvert.SerializeObject(new { value = String.Empty });

            DataTable dt = new JCodes.Framework.BLL.Machines().GetZG(key);

            return JsonConvert.SerializeObject(new { value = dt });
        }

        public JsonResult AddMachine(MachinesInfo machine)
        {
            if (machine.GBRQ == null) machine.GBRQ = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.GBRQ).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.GBRQ).Length > 32)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 GBRQ" }, JsonRequestBehavior.AllowGet);
            if (machine.GLY == null) machine.GLY = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.GLY).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.GLY).Length > 32)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 GLY" }, JsonRequestBehavior.AllowGet);
            if (machine.GWFWDK == null) machine.GWFWDK = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.GWFWDK).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.GWFWDK).Length > 256)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 GWFWDK" }, JsonRequestBehavior.AllowGet);
            if (machine.GWIP == null) machine.GWIP = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.GWIP).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.GWIP).Length > 64)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 GWIP" }, JsonRequestBehavior.AllowGet);
            if (machine.JGWZ == null) machine.JGWZ = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.JGWZ).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.JGWZ).Length > 128)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 JGWZ" }, JsonRequestBehavior.AllowGet);
            if (machine.JQRT == null) machine.JQRT = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.JQRT).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.JQRT).Length > 256)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 JQRT" }, JsonRequestBehavior.AllowGet);
            if (machine.JQXH == null) machine.JQXH = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.JQXH).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.JQXH).Length > 128)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 JQXH" }, JsonRequestBehavior.AllowGet);
            if (machine.NWFWDK == null) machine.NWFWDK = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.NWFWDK).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.NWFWDK).Length > 256)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 NWFWDK" }, JsonRequestBehavior.AllowGet);
            if (machine.NWIP == null) machine.NWIP = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.NWIP).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.NWIP).Length > 64)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 NWIP" }, JsonRequestBehavior.AllowGet);
            if (machine.TABTYPE == null) machine.TABTYPE = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.TABTYPE).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.TABTYPE).Length > 6)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 TABTYPE" }, JsonRequestBehavior.AllowGet);
            if (machine.WJLY == null) machine.WJLY = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.WJLY).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.WJLY).Length > 256)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 WJLY" }, JsonRequestBehavior.AllowGet);
            if (machine.WWFWDK == null) machine.WWFWDK = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.WWFWDK).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.WWFWDK).Length > 256)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 WWFWDK" }, JsonRequestBehavior.AllowGet);
            if (machine.WWIP == null) machine.WWIP = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.WWIP).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.WWIP).Length > 64)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 WWIP" }, JsonRequestBehavior.AllowGet);
            if (machine.XTBB == null) machine.XTBB = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.XTBB).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.XTBB).Length > 128)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 XTBB" }, JsonRequestBehavior.AllowGet);
            if (machine.YJXLH == null) machine.YJXLH = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.YJXLH).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.YJXLH).Length > 128)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 YJXLH" }, JsonRequestBehavior.AllowGet);
            if (machine.YYBB == null) machine.YYBB = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.YYBB).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.YYBB).Length > 256)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 YYBB" }, JsonRequestBehavior.AllowGet);
            if (machine.ZG == null) machine.ZG = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.ZG).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.ZG).Length > 32)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 ZG" }, JsonRequestBehavior.AllowGet);

            Int32 r = new JCodes.Framework.BLL.Machines().AddMachine(machine);
            if (r > 0)
                return Json(new { errCode = 0, errMsg = "" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { errCode = -1, errMsg = "添加设备失败" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModMachine(MachinesInfo machine)
        {
            if (machine.GBRQ == null) machine.GBRQ = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.GBRQ).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.GBRQ).Length > 32)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 GBRQ" }, JsonRequestBehavior.AllowGet);
            if (machine.GLY == null) machine.GLY = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.GLY).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.GLY).Length > 32)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 GLY" }, JsonRequestBehavior.AllowGet);
            if (machine.GWFWDK == null) machine.GWFWDK = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.GWFWDK).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.GWFWDK).Length > 256)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 GWFWDK" }, JsonRequestBehavior.AllowGet);
            if (machine.GWIP == null) machine.GWIP = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.GWIP).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.GWIP).Length > 64)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 GWIP" }, JsonRequestBehavior.AllowGet);
            if (machine.JGWZ == null) machine.JGWZ = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.JGWZ).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.JGWZ).Length > 128)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 JGWZ" }, JsonRequestBehavior.AllowGet);
            if (machine.JQRT == null) machine.JQRT = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.JQRT).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.JQRT).Length > 256)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 JQRT" }, JsonRequestBehavior.AllowGet);
            if (machine.JQXH == null) machine.JQXH = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.JQXH).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.JQXH).Length > 128)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 JQXH" }, JsonRequestBehavior.AllowGet);
            if (machine.NWFWDK == null) machine.NWFWDK = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.NWFWDK).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.NWFWDK).Length > 256)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 NWFWDK" }, JsonRequestBehavior.AllowGet);
            if (machine.NWIP == null) machine.NWIP = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.NWIP).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.NWIP).Length > 64)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 NWIP" }, JsonRequestBehavior.AllowGet);
            if (machine.TABTYPE == null) machine.TABTYPE = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.TABTYPE).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.TABTYPE).Length > 6)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 TABTYPE" }, JsonRequestBehavior.AllowGet);
            if (machine.WJLY == null) machine.WJLY = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.WJLY).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.WJLY).Length > 256)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 WJLY" }, JsonRequestBehavior.AllowGet);
            if (machine.WWFWDK == null) machine.WWFWDK = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.WWFWDK).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.WWFWDK).Length > 256)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 WWFWDK" }, JsonRequestBehavior.AllowGet);
            if (machine.WWIP == null) machine.WWIP = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.WWIP).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.WWIP).Length > 64)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 WWIP" }, JsonRequestBehavior.AllowGet);
            if (machine.XTBB == null) machine.XTBB = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.XTBB).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.XTBB).Length > 128)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 XTBB" }, JsonRequestBehavior.AllowGet);
            if (machine.YJXLH == null) machine.YJXLH = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.YJXLH).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.YJXLH).Length > 128)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 YJXLH" }, JsonRequestBehavior.AllowGet);
            if (machine.YYBB == null) machine.YYBB = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.YYBB).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.YYBB).Length > 256)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 YYBB" }, JsonRequestBehavior.AllowGet);
            if (machine.ZG == null) machine.ZG = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.ZG).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.ZG).Length > 32)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 ZG" }, JsonRequestBehavior.AllowGet);
            if (machine.MODIFYMARK == null) machine.MODIFYMARK = string.Empty;
            if (System.Text.Encoding.Default.GetBytes(machine.MODIFYMARK).Length < 0 || System.Text.Encoding.Default.GetBytes(machine.MODIFYMARK).Length > 256)
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 MODIFYMARK" }, JsonRequestBehavior.AllowGet);
            if (machine.ID <= 0 || !ValidateUtil.IsNumber(machine.ID.ToString()))
                return Json(new { errCode = -1, errMsg = "字段长度校验失败 ID" }, JsonRequestBehavior.AllowGet);
            
            
            Int32 r = new JCodes.Framework.BLL.Machines().ModMachine(machine);
            if (r > 0)
                return Json(new { errCode = 0, errMsg = "" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { errCode = -1, errMsg = "修改设备失败" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOneMachine(String Id)
        {
            if (!ValidateUtil.IsNumber(Id)) return Json(new { errCode = -1, errMsg = "入参格式错误" }, JsonRequestBehavior.AllowGet);

            MachinesInfo machine = new JCodes.Framework.BLL.Machines().FindById(Convert.ToInt32(Id));
            if (machine == null) return Json(new { errCode = -1, errMsg = "不存在的记录" }, JsonRequestBehavior.AllowGet);
            else return Json(new { errCode = 0, errMsg = "", data=machine }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDictData(String Id)
        {
            if (!ValidateUtil.IsNumber(Id)) return Json(new { errCode = -1, errMsg = "入参格式错误" }, JsonRequestBehavior.AllowGet);

            List<DictDataInfo> lst = new JCodes.Framework.BLL.DictData().FindByTypeId(Convert.ToInt32(Id));
            if (lst == null || lst.Count == 0) return Json(new { errCode = -1, errMsg = "不存在的记录" }, JsonRequestBehavior.AllowGet);
            else return Json(new { errCode = 0, errMsg = "", data = lst }, JsonRequestBehavior.AllowGet);
        }
    }
}
