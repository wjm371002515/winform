using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using JCodes.Framework.Common;
using JCodes.Framework.Data.BLL;
using JCodes.Framework.Data.Model;

namespace JCodes.Framework.WebUI.admin.ashx
{
    /// <summary>
    /// 订单管理 
    /// </summary>
    public class bg_orders : IHttpHandler
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
                    case "getorders": //根据用户的权限获取用户点击的菜单有权限的按钮
                        string pageName = context.Request.Params["pagename"];
                        string menuCode = context.Request.Params["menucode"]; //菜单标识码
                        DataTable dt = new Data.BLL.Button().GetButtonByMenuCodeAndUserId(menuCode, user.Id);
                        context.Response.Write(ToolbarHelper.GetToolBar(dt, pageName));
                        break;
                    #endregion

                    #region 查询订单
                    case "search":
                        string strWhere = null;
                        if (string.IsNullOrEmpty(context.Request.Params["sqlWhere"]))
                        {
                            strWhere = "1=1";
                        }
                        else
                        {
                            strWhere = "1=1" + context.Request.Params["sqlWhere"];
                        }
                        string sort = context.Request.Params["sort"]; //排序列
                        string order = context.Request.Params["order"]; //排序方式 asc或者desc
                        int pageindex = int.Parse(context.Request.Params["page"]);
                        int pagesize = int.Parse(context.Request.Params["rows"]);

                        int totalCount; //输出参数
                        string strJson = new Data.BLL.Order().GetPager("tbOrder", "*", sort + " " + order, pagesize,
                            pageindex, strWhere, out totalCount);
                        context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");
                        userOperateLog.OperateInfo = "查询订单";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "查询条件：" + strWhere + " 排序：" + sort + " " + order + " 页码/每页大小：" +
                                                     pageindex + " " + pagesize;
                        Data.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    #endregion

                    #region 添加订单
                    case "addorder":
                        // 这里的if 去判断一下用户是否具有权限
                        if (user != null && new Authority().IfAuthority("orders", "add", user.Id))
                        {
                            Data.Model.Order addorder = new Data.Model.Order();
                            addorder.type = Convert.ToChar(context.Request.Params["ui_order_type_add"]);
                            addorder.shopname = context.Request.Params["ui_order_shopname_add"];
                            addorder.aliwangwang = context.Request.Params["ui_order_aliwangwang_add"];
                            addorder.in_company = context.Request.Params["ui_order_in_company_add"];
                            addorder.in_orderid = context.Request.Params["ui_order_in_orderid_add"];
                            addorder.id = context.Request.Params["ui_order_id_add"];
                            addorder.in_color = context.Request.Params["ui_order_in_color_add"];
                            addorder.in_size = context.Request.Params["ui_order_in_size_add"];
                            addorder.in_num = string.IsNullOrEmpty(context.Request.Params["ui_order_in_num_add"])
                                ? 0
                                : Convert.ToInt32(context.Request.Params["ui_order_in_num_add"]);
                            addorder.out_company = context.Request.Params["ui_order_out_company_add"];
                            addorder.out_orderid = context.Request.Params["ui_order_out_orderid_add"];
                            addorder.out_id = context.Request.Params["ui_order_out_id_add"];
                            addorder.out_color = context.Request.Params["ui_order_out_color_add"];
                            addorder.out_size = context.Request.Params["ui_order_out_size_add"];
                            addorder.out_num = string.IsNullOrEmpty(context.Request.Params["ui_order_out_num_add"])
                                ? 0
                                : Convert.ToInt32(context.Request.Params["ui_order_out_num_add"]);
                            addorder.remark = context.Request.Params["ui_order_remark_add"];
                            addorder.create_time = DateTime.Now;
                            addorder.update_time = DateTime.Now;
                            addorder.username = user.UserId;

                            // 如果用户没有填数据 我们这里系统默认的给其赋值
                            if (string.IsNullOrEmpty(addorder.shopname))
                            {
                                addorder.shopname = "未填";
                            }
                            if (string.IsNullOrEmpty(addorder.aliwangwang))
                            {
                                addorder.aliwangwang = "未填";
                            }
                            if (string.IsNullOrEmpty(addorder.in_company))
                            {
                                addorder.in_company = "未填";
                            }
                            if (string.IsNullOrEmpty(addorder.id))
                            {
                                addorder.id = "未填";
                            }
                            if (string.IsNullOrEmpty(addorder.in_color))
                            {
                                addorder.in_color = "未填";
                            }
                            if (string.IsNullOrEmpty(addorder.in_size))
                            {
                                addorder.in_size = "未填";
                            }
                            if (string.IsNullOrEmpty(addorder.out_company))
                            {
                                addorder.out_company = "未填";
                            }
                            if (string.IsNullOrEmpty(addorder.out_orderid))
                            {
                                addorder.out_orderid = "未填";
                            }
                            if (string.IsNullOrEmpty(addorder.out_id))
                            {
                                addorder.out_id = "未填";
                            }
                            if (string.IsNullOrEmpty(addorder.out_color))
                            {
                                addorder.out_color = "未填";
                            }
                            if (string.IsNullOrEmpty(addorder.out_size))
                            {
                                addorder.out_size = "未填";
                            }
                            if (string.IsNullOrEmpty(addorder.remark))
                            {
                                addorder.remark = "未填";
                            }

                            string orderresult = new Data.BLL.Order().AddOrder(addorder);
                            if (!string.IsNullOrEmpty(orderresult))
                            {
                                userOperateLog.OperateInfo = "添加订单";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "添加订单，快递单号：" + orderresult + ",管理员:" + user.UserId;
                                context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");  
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "添加订单";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "添加失败";
                                context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                            }
                            Data.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        }
                        break;
                    #endregion

                    #region 删除订单
                    case "delorder":
                        if (user != null && new Authority().IfAuthority("orders", "delete", user.Id))
                        {
                            string orderid = context.Request.Params["in_orderid"];
                            if (new Data.BLL.Order().DelOrder(orderid))
                            {
                                userOperateLog.OperateInfo = "删除订单";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "删除成功，快递单号：" + orderid;
                                context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "删除订单";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "删除失败，快递单号：" + orderid;
                                context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "删除订单";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        Data.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    #endregion

                    #region 编辑订单
                    case "editorder":
                        if (user != null && new Authority().IfAuthority("orders", "edit", user.Id))
                        {
                            Data.Model.Order editorder = new Data.Model.Order();
                            editorder.type = Convert.ToChar(context.Request.Params["ui_order_type_edit"]);
                            editorder.shopname = context.Request.Params["ui_order_shopname_edit"];
                            editorder.aliwangwang = context.Request.Params["ui_order_aliwangwang_edit"];
                            editorder.in_company = context.Request.Params["ui_order_in_company_edit"];
                            editorder.in_orderid = context.Request.Params["ui_order_in_orderid_edit"];
                            editorder.id = context.Request.Params["ui_order_id_edit"];
                            editorder.in_color = context.Request.Params["ui_order_in_color_edit"];
                            editorder.in_size = context.Request.Params["ui_order_in_size_edit"];
                            editorder.in_num = string.IsNullOrEmpty(context.Request.Params["ui_order_in_num_edit"])
                                ? 0
                                : Convert.ToInt32(context.Request.Params["ui_order_in_num_edit"]);
                            editorder.out_company = context.Request.Params["ui_order_out_company_edit"];
                            editorder.out_orderid = context.Request.Params["ui_order_out_orderid_edit"];
                            editorder.out_id = context.Request.Params["ui_order_out_id_edit"];
                            editorder.out_color = context.Request.Params["ui_order_out_color_edit"];
                            editorder.out_size = context.Request.Params["ui_order_out_size_edit"];
                            editorder.out_num = string.IsNullOrEmpty(context.Request.Params["ui_order_out_num_edit"])
                                ? 0
                                : Convert.ToInt32(context.Request.Params["ui_order_out_num_edit"]);
                            editorder.remark = context.Request.Params["ui_order_remark_edit"];
                            editorder.create_time = DateTime.Now;
                            editorder.update_time = DateTime.Now;
                            editorder.username = user.UserId;

                            // 如果用户没有填数据 我们这里系统默认的给其赋值
                            if (string.IsNullOrEmpty(editorder.shopname))
                            {
                                editorder.shopname = "未填";
                            }
                            if (string.IsNullOrEmpty(editorder.aliwangwang))
                            {
                                editorder.aliwangwang = "未填";
                            }
                            if (string.IsNullOrEmpty(editorder.in_company))
                            {
                                editorder.in_company = "未填";
                            }
                            if (string.IsNullOrEmpty(editorder.id))
                            {
                                editorder.id = "未填";
                            }
                            if (string.IsNullOrEmpty(editorder.in_color))
                            {
                                editorder.in_color = "未填";
                            }
                            if (string.IsNullOrEmpty(editorder.in_size))
                            {
                                editorder.in_size = "未填";
                            }
                            if (string.IsNullOrEmpty(editorder.out_company))
                            {
                                editorder.out_company = "未填";
                            }
                            if (string.IsNullOrEmpty(editorder.out_orderid))
                            {
                                editorder.out_orderid = "未填";
                            }
                            if (string.IsNullOrEmpty(editorder.out_id))
                            {
                                editorder.out_id = "未填";
                            }
                            if (string.IsNullOrEmpty(editorder.out_color))
                            {
                                editorder.out_color = "未填";
                            }
                            if (string.IsNullOrEmpty(editorder.out_size))
                            {
                                editorder.out_size = "未填";
                            }
                            if (string.IsNullOrEmpty(editorder.remark))
                            {
                                editorder.remark = "未填";
                            }

                            bool result = new Data.BLL.Order().EditOrder(editorder);
                            if (result)
                            {
                                userOperateLog.OperateInfo = "修改订单";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "修改成功，快递单号：" + editorder.in_orderid;
                                context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "修改订单";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "修改失败，快递单号：" + editorder.in_orderid;
                                context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                            }
                            Data.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "修改订单";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        break;
                    #endregion

                    #region 批量操作 最多支持20条数据
                    case "batch":
                        if (user != null && new Authority().IfAuthority("orders", "edit", user.Id))
                        {
                            Data.Model.Order addorder = new Data.Model.Order();
                            List<string> orderList = new List<string>();

                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add1"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add1"]);
                            }

                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add2"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add2"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add3"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add3"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add4"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add4"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add5"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add5"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add6"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add6"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add7"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add7"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add8"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add8"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add9"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add9"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add10"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add10"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add11"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add11"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add12"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add12"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add13"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add13"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add14"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add14"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add15"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add15"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add16"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add16"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add17"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add17"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add18"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add18"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add19"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add19"]);
                            }
                            if (!string.IsNullOrEmpty(context.Request.Params["ui_order_in_orderid_add20"]))
                            {
                                orderList.Add(context.Request.Params["ui_order_in_orderid_add20"]);
                            }
                            int count = 0;
                            for (int i = 0; i < orderList.Count; i ++)
                            {
                                addorder.create_time = DateTime.Now;
                                addorder.update_time = DateTime.Now;
                                addorder.username = user.UserId;

                                // 如果用户没有填数据 我们这里系统默认的给其赋值
                                if (string.IsNullOrEmpty(addorder.shopname))
                                {
                                    addorder.shopname = "未填";
                                }
                                if (string.IsNullOrEmpty(addorder.aliwangwang))
                                {
                                    addorder.aliwangwang = "未填";
                                }
                                if (string.IsNullOrEmpty(addorder.in_company))
                                {
                                    addorder.in_company = "未填";
                                }
                                if (string.IsNullOrEmpty(addorder.id))
                                {
                                    addorder.id = "未填";
                                }
                                if (string.IsNullOrEmpty(addorder.in_color))
                                {
                                    addorder.in_color = "未填";
                                }
                                if (string.IsNullOrEmpty(addorder.in_size))
                                {
                                    addorder.in_size = "未填";
                                }
                                if (string.IsNullOrEmpty(addorder.out_company))
                                {
                                    addorder.out_company = "未填";
                                }
                                if (string.IsNullOrEmpty(addorder.out_orderid))
                                {
                                    addorder.out_orderid = "未填";
                                }
                                if (string.IsNullOrEmpty(addorder.out_id))
                                {
                                    addorder.out_id = "未填";
                                }
                                if (string.IsNullOrEmpty(addorder.out_color))
                                {
                                    addorder.out_color = "未填";
                                }
                                if (string.IsNullOrEmpty(addorder.out_size))
                                {
                                    addorder.out_size = "未填";
                                }
                                if (string.IsNullOrEmpty(addorder.remark))
                                {
                                    addorder.remark = "未填";
                                }
                                addorder.in_orderid = orderList[i];
                                addorder.type = '0';    // 默认为0 退货

                                string orderresult = new Data.BLL.Order().AddOrder(addorder);
                                if (!string.IsNullOrEmpty(orderresult))
                                {
                                    count ++;
                                }
                            }

                            if (count != 0)
                            {
                                userOperateLog.OperateInfo = "添加订单";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "添加订单，成功单数：" + count + ",管理员:" + user.UserId;
                                context.Response.Write("{\"msg\":\"添加成功！总共添加"+count+"\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "添加订单";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "添加失败";
                                context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                            }
                            Data.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
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

                        context.Response.Write("{\"success\":true,\"msg\":\""+sb.ToString()+"\"}");
                        break;
                    #endregion

                    default:
                        context.Response.Write("{\"result\":\"参数错误！\",\"success\":false}");
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"msg\":\"" + JsonHelper.StringFilter(ex.Message) + "\",\"success\":false}");
                userOperateLog.OperateInfo = "订单功能异常";
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