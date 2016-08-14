using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JCodes.Framework.Common;
using JCodes.Framework.Data.Model;
using JCodes.Framework.Data.BLL;

namespace JCodes.Framework.WebUI.admin.ashx
{
    /// <summary>
    /// 后台导航树
    /// </summary>
    public class bg_menu : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
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
                    case "getUserMenu":  //获取特定用户能看到的菜单（左侧树）
                        context.Response.Write(new Data.BLL.Menu().GetUserMenu(user.Id));
                        break;
                    case "getAllMenu":   //根据角色id获取此角色有的权限（设置角色时自动勾选已经有的按钮权限）
                        int roleid = Convert.ToInt32(context.Request.Params["roleid"]);  //角色id
                        context.Response.Write(new Data.BLL.Menu().GetAllMenu(roleid));
                        break;
                    case "getMyAuthority":  //前台根据用户名查“我的权限”
                        context.Response.Write(new Data.BLL.Menu().GetMyAuthority(user.Id));
                        userOperateLog.OperateInfo = "查询我的信息";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "查询我的信息";
                        Data.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "search":
                        string strWhere = "1=1";
                        string sort = context.Request.Params["sort"] == null ? "Id" : context.Request.Params["sort"];  //排序列
                        string order = context.Request.Params["order"] == null ? "asc" : context.Request.Params["order"];  //排序方式 asc或者desc
                        int pageindex = int.Parse(context.Request.Params["page"]);
                        int pagesize = int.Parse(context.Request.Params["rows"]);

                        int totalCount;   //输出参数
                        string strJson = "";    //输出结果
                        if (order.IndexOf(',') != -1)   //如果有","就是多列排序（不能拿列判断，列名中间可能有","符号）
                        {
                            //多列排序：
                            //sort：ParentId,Sort,AddDate
                            //order：asc,desc,asc
                            string sortMulti = "";  //拼接排序条件，例：ParentId desc,Sort asc
                            string[] sortArray = sort.Split(',');   //列名中间有","符号，这里也要出错。正常不会有
                            string[] orderArray = order.Split(',');
                            for (int i = 0; i < sortArray.Length; i++)
                            {
                                sortMulti += sortArray[i] + " " + orderArray[i] + ",";
                            }
                            strJson = new Data.BLL.Menu().GetPager("tbMenu", "Id,Name,ParentId,Code,LinkAddress,Icon,Sort,AddDate", sortMulti.Trim(','), pagesize, pageindex, strWhere, out totalCount);
                            userOperateLog.Description = "查询条件：" + strWhere + " 排序：" + sortMulti.Trim(',') + " 页码/每页大小：" + pageindex + " " + pagesize;
                        }
                        else
                        {
                            strJson = new Data.BLL.Menu().GetPager("tbMenu", "Id,Name,ParentId,Code,LinkAddress,Icon,Sort,AddDate", sort + " " + order, pagesize, pageindex, strWhere, out totalCount);
                            userOperateLog.Description = "查询条件：" + strWhere + " 排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
                        }

                        context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");
                        userOperateLog.OperateInfo = "查询菜单";
                        userOperateLog.IfSuccess = true;
                        Data.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    default:
                        context.Response.Write("{\"result\":\"参数错误！\",\"success\":false}");
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"msg\":\"" + JsonHelper.StringFilter(ex.Message) + "\",\"success\":false}");
                userOperateLog.OperateInfo = "菜单功能异常";
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