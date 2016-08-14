using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using JCodes.Framework.Common;
using JCodes.Framework.Data.Model;
using JCodes.Framework.Data.BLL;

namespace JCodes.Framework.WebUI.admin.ashx
{
    /// <summary>
    /// bg_dailys 的摘要说明
    /// </summary>
    public class bg_dailys : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request.Params["action"];
            Data.Model.UserOperateLog userOperateLog = null;   //操作日志对象
            try
            {
                Data.Model.User user = UserHelper.GetUser(context);   //获取cookie里的用户对象
                userOperateLog = new Data.Model.UserOperateLog();
                userOperateLog.UserIp = context.Request.UserHostAddress;
                userOperateLog.UserName = user.UserId;

                switch (action)
                {
                    #region 获取订单
                    case "getdailys": //根据用户的权限获取用户点击的菜单有权限的按钮
                        string pageName = context.Request.Params["pagename"];
                        string menuCode = context.Request.Params["menucode"]; //菜单标识码
                        DataTable dt = new Data.BLL.Button().GetButtonByMenuCodeAndUserId(menuCode, user.Id);
                        context.Response.Write(ToolbarHelper.GetToolBar(dt, pageName));
                        break;
                    #endregion

                    #region 查询
                    case "search":
                        string strWhere = "1=1";
                        string sort = context.Request.Params["sort"]; //排序列
                        string order = context.Request.Params["order"]; //排序方式 asc或者desc


                        string ui_daily_createdate = context.Request.Params["ui_daily_createdate"];
                        string ui_daily_shopname = context.Request.Params["ui_daily_shopname"];
                        string ui_daily_aliwangwang = context.Request.Params["ui_daily_aliwangwang"];
                        string ui_daily_key = context.Request.Params["ui_daily_key"];
                        string ui_daily_isSolve = context.Request.Params["ui_daily_isSolve"];


                        if (!string.IsNullOrEmpty(ui_daily_createdate) && !SqlInjection.GetString(ui_daily_createdate))   //防止sql注入
                            strWhere += string.Format(" and createdate = '{0}'", ui_daily_createdate.Trim());
                        if (!string.IsNullOrEmpty(ui_daily_shopname) && !SqlInjection.GetString(ui_daily_shopname))
                            strWhere += string.Format(" and shopname = '{0}'", ui_daily_shopname.Trim());
                        if (!string.IsNullOrEmpty(ui_daily_aliwangwang) && !SqlInjection.GetString(ui_daily_aliwangwang))
                            strWhere += string.Format(" and aliwangwang = '{0}'", ui_daily_aliwangwang.Trim());
                        if (!string.IsNullOrEmpty(ui_daily_key) && !SqlInjection.GetString(ui_daily_key))
                            strWhere += string.Format(" and ((shopname like '{0}') or (aliwangwang like '{1}') or (remark like '{2}'))", ui_daily_key.Trim(), ui_daily_key.Trim(), ui_daily_key.Trim());
                        if (!string.IsNullOrEmpty(ui_daily_isSolve) && !SqlInjection.GetString(ui_daily_isSolve))
                            strWhere += string.Format(" and isSolve = '{0}'", ui_daily_isSolve.Trim());

                        int pageindex = int.Parse(context.Request.Params["page"]);
                        int pagesize = int.Parse(context.Request.Params["rows"]);

                        int totalCount; //输出参数
                        string strJson = new Data.BLL.Order().GetPager("tbDaily", "*", sort + " " + order, pagesize,
                            pageindex, strWhere, out totalCount);
                        context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");
                        userOperateLog.OperateInfo = "查询事务";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "查询事务" + " 排序：" + sort + " " + order + " 页码/每页大小：" +
                                                     pageindex + " " + pagesize;
                        Data.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    #endregion

                    #region 添加事务
                    case "adddaily":
                        // 这里的if 去判断一下用户是否具有权限
                        if (user != null && new Authority().IfAuthority("daily", "add", user.Id))
                        {
                            Data.Model.Daily adddaily = new Data.Model.Daily();
                            adddaily.createdate = DateTime.Now.ToString("yyyyMMdd");
                            adddaily.shopname = context.Request.Params["ui_daily_shopname_add"];
                            adddaily.aliwangwang = context.Request.Params["ui_daily_aliwangwang_add"];
                            adddaily.dosolve = context.Request.Params["ui_daily_dosolve_add"];
                            adddaily.username = user.UserId;
                            adddaily.isSolve = 0;
                            adddaily.solvename = "";
                            adddaily.remark = context.Request.Params["ui_daily_remark_add"];
                            
                           

                            // 如果用户没有填数据 我们这里系统默认的给其赋值
                            if (string.IsNullOrEmpty(adddaily.shopname))
                            {
                                adddaily.shopname = "未填";
                            }
                            if (string.IsNullOrEmpty(adddaily.aliwangwang))
                            {
                                adddaily.aliwangwang = "未填";
                            }
                            if (string.IsNullOrEmpty(adddaily.dosolve))
                            {
                                adddaily.dosolve = "未填";
                            }
                            if (string.IsNullOrEmpty(adddaily.remark))
                            {
                                adddaily.remark = "未填";
                            }
                      

                            string dailyid = new Data.BLL.Daily().AddDaily(adddaily);
                            if (!string.IsNullOrEmpty(dailyid))
                            {
                                userOperateLog.OperateInfo = "添加事务";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "添加事务，添加ID为：" + dailyid + ",管理员:" + user.UserId;
                                context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "添加事务";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "添加失败";
                                context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                            }
                            Data.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        }
                        break;
                    #endregion

                    #region 删除订单
                    case "deldaily":
                        if (user != null && new Authority().IfAuthority("daily", "delete", user.Id))
                        {
                            int id = Convert.ToInt32(context.Request.Params["id"]);
                            if (new Data.BLL.Daily().DelDaily(id))
                            {
                                userOperateLog.OperateInfo = "删除业务";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "删除成功, 业务编号：" + id;
                                context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "删除业务";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "删除失败，业务编号：" + id;
                                context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "删除业务";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        Data.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    #endregion

                    #region 编辑订单
                    case "editdaily":
                        if (user != null && new Authority().IfAuthority("daily", "edit", user.Id))
                        {
                            Data.Model.Daily editdaily = new Data.Model.Daily();

                            editdaily.Id = Convert.ToInt32(context.Request.Params["id"]);
                            editdaily.shopname = context.Request.Params["ui_daily_shopname_edit"];
                            editdaily.aliwangwang = context.Request.Params["ui_daily_aliwangwang_edit"];
                            editdaily.dosolve = context.Request.Params["ui_daily_dosolve_edit"];
                            editdaily.username = user.UserId;
                            editdaily.isSolve = string.IsNullOrEmpty(context.Request.Params["ui_daily_isSolve_edit"])?0:Convert.ToInt32(context.Request.Params["ui_daily_isSolve_edit"]);
                            editdaily.solvename = context.Request.Params["ui_daily_solvename_edit"];
                            editdaily.remark = context.Request.Params["ui_daily_remark_edit"];

                            // 如果用户没有填数据 我们这里系统默认的给其赋值
                            if (string.IsNullOrEmpty(editdaily.shopname))
                            {
                                editdaily.shopname = "未填";
                            }
                            if (string.IsNullOrEmpty(editdaily.aliwangwang))
                            {
                                editdaily.aliwangwang = "未填";
                            }
                            if (string.IsNullOrEmpty(editdaily.dosolve))
                            {
                                editdaily.dosolve = "未填";
                            }
                            if (string.IsNullOrEmpty(editdaily.remark))
                            {
                                editdaily.remark = "未填";
                            }
                            if (string.IsNullOrEmpty(editdaily.solvename))
                            {
                                editdaily.solvename = "未填";
                            }

                            bool result = new Data.BLL.Daily().EditDaily(editdaily);
                            if (result)
                            {
                                userOperateLog.OperateInfo = "修改业务";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "修改成功，修改业务标号：" + editdaily.Id;
                                context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "修改业务";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "修改失败，修改业务标号：" + editdaily.Id;
                                context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                            }
                            Data.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "修改业务";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        break;
                    #endregion

                    #region 根据过滤条件进行查询
                    case "setsearch":
                        StringBuilder sb = new StringBuilder();
                        if (!string.IsNullOrEmpty(context.Request.Params["ui_order_type_search"]))
                        {
                            sb.Append(" and type='" + context.Request.Params["ui_order_type_search"] + "'");
                        }
                        if (!string.IsNullOrEmpty(context.Request.Params["ui_order_aliwangwang_search"]))
                        {
                            sb.Append(" and aliwangwang='" + context.Request.Params["ui_order_aliwangwang_search"] + "'");
                        }
                        if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_company_search"]))
                        {
                            sb.Append(" and in_company='" + context.Request.Params["ui_order_in_company_search"] + "'");
                        }
                        if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_search"]))
                        {
                            sb.Append(" and in_orderid='" + context.Request.Params["ui_order_in_orderid_search"] + "'");
                        }
                        if (!string.IsNullOrEmpty(context.Request.Params["ui_order_id_search"]))
                        {
                            sb.Append(" and id='" + context.Request.Params["ui_order_id_search"] + "'");
                        }

                        context.Response.Write("{\"success\":true,\"msg\":\"" + sb.ToString() + "\"}");
                        break;
                    #endregion

                    default:
                        context.Response.Write("{\"result\":\"参数" + action + "错误2！" + "\",\"success\":false}");
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"msg\":\"" + JsonHelper.StringFilter(ex.Message) + "\",\"success\":false}");
                userOperateLog.OperateInfo = "事务功能异常";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = JsonHelper.StringFilter(ex.Message);
                Data.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}