using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Reflection;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.BLL;
using JCodes.Framework.Common.Files;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Caching;
using JCodes.Framework.WebUI.Common;

namespace JCodes.Framework.WebDemo.Controllers
{
    /// <summary>
    /// 所有需要进行登录控制的控制器基类
    /// </summary>
    public class BaseController : Controller 
    {
        #region 属性变量

        /// <summary>
        /// 当前登录的用户属性
        /// </summary>
        public UserInfo CurrentUser = new UserInfo();

        /// <summary>
        /// 定义常用功能的控制ID，方便基类控制器对用户权限的控制
        /// </summary>
        protected AuthorizeKeyInfo authorizeKeyInfo = new AuthorizeKeyInfo(); 

        #endregion

        #region 权限控制内容

        /// <summary>
        /// 获取用户的能使用的功能集合
        /// </summary>
        protected virtual Dictionary<string, string> Functions
        {
            get
            {
                Dictionary<string, string> functionDict = Session["Functions"] as Dictionary<string, string>;
                if (functionDict == null)
                {
                    functionDict = new Dictionary<string, string>();
                }
                return functionDict;
            }
        }

        /// <summary>
        /// 判断当前用户是否拥有某功能点的权限
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public virtual bool HasFunction(string functionId)
        {
            return Permission.HasFunction(functionId);
        }

        /// <summary>
        /// 判断是否为系统管理员
        /// </summary>
        /// <returns>true:系统管理员,false:不是系统管理员</returns>
        public virtual bool IsAdmin()
        {
            return Permission.IsAdmin();
        }

        /// <summary>
        /// 用于检查方法执行前的权限，如果未授权，返回MyDenyAccessException异常
        /// </summary>
        /// <param name="functionId"></param>
        protected virtual void CheckAuthorized(string functionId)
        {
            if(!HasFunction(functionId))
            {
                string errorMessage = "您未被授权使用该功能，请重新登录测试或联系管理员进行处理。";
                throw new MyDenyAccessException(errorMessage);
            }
        }

        /// <summary>
        /// 对AuthorizeKey对象里面的操作权限进行赋值，用于页面判断
        /// </summary>
        protected virtual void ConvertAuthorizedInfo()
        {
            //判断用户权限
            authorizeKeyInfo.CanInsert = HasFunction(authorizeKeyInfo.InsertKey);
            authorizeKeyInfo.CanUpdate = HasFunction(authorizeKeyInfo.UpdateKey);
            authorizeKeyInfo.CanDelete = HasFunction(authorizeKeyInfo.DeleteKey);
            authorizeKeyInfo.CanView = HasFunction(authorizeKeyInfo.ViewKey);
            authorizeKeyInfo.CanList = HasFunction(authorizeKeyInfo.ListKey);
            authorizeKeyInfo.CanExport = HasFunction(authorizeKeyInfo.ExportKey);
        }

        #endregion

        #region 异常处理及记录
        /// <summary>
        /// 重写基类在Action执行之前的处理
        /// </summary>
        /// <param name="filterContext">重写方法的参数</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //得到用户登录的信息
            /*CurrentUser = Session["UserInfo"] as UserInfo;
            if (CurrentUser == null)
            {
                Response.Redirect("/Login/Index");//如果用户为空跳转到登录界面
            }
            else
            {
                //设置授权属性，然后赋值给ViewBag保存
                ConvertAuthorizedInfo();
                ViewBag.AuthorizeKey = AuthorizeKey;

                //登录信息统一设置
                ViewBag.FullName = CurrentUser.FullName;
                ViewBag.Name = CurrentUser.Name;

                ViewBag.MenuString = GetMenuString();
                //ViewBag.MenuString = GetMenuStringCache(); //使用缓存，隔一段时间更新
            }*/
        }

        /// <summary>
        /// 覆盖基类控制器的异常处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is MyDenyAccessException)
            {
                base.OnException(filterContext);

                //自定义非授权的异常处理，可记录用户操作

                // 当自定义显示错误 mode = On，显示友好错误页面
                if (filterContext.HttpContext.IsCustomErrorEnabled)
                {
                    filterContext.ExceptionHandled = true;
                    this.View("Error").ExecuteResult(this.ControllerContext);
                    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
            }
            else
            {
                base.OnException(filterContext);
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, filterContext.Exception, typeof(BaseController));
                
                // 当自定义显示错误 mode = On，显示友好错误页面
                if (filterContext.HttpContext.IsCustomErrorEnabled)
                {
                    filterContext.ExceptionHandled = true;
                    this.View("Error").ExecuteResult(this.ControllerContext);
                    //Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
        } 
        #endregion

        #region 菜单管理

        public string GetMenuStringCache()
        {
            string itemValue = MemoryCacheHelper.GetCacheItem<string>("GetMenuStringCache", delegate()
                {
                    return GetMenuString();
                },
                null, DateTime.Now.AddMinutes(5) //5分钟以后过期，重新获取
            );
            return itemValue;
        }

        public string GetMenuString()
        {
            #region 菜单格式代码
            /*
            <li id="2">
                <a href="javascript:;">
                    <i class="icon-basket"></i>
                    <span class="title">行业动态</span>
                    <span class="selected"></span>
                    <span class="arrow open"></span>
                </a>
                <ul class="sub-menu">
                    <li class="heading" style="font-size:14px;color:yellow">
                        <i class="icon-home"></i>
                        行业动态
                    </li>
                    <li>
                        <a href="second?ai=2">
                            <i class="icon-home"></i>
                            <span class="badge badge-danger">4</span>
                            政策法规
                        </a>
                    </li>
             */
            
            #endregion

            #region 定义的格式模板
            // javascript:;
            // {0}?tid={1}
            var firstTemplate = @"
            <li id='{3}'>
                <a href='{0}'>
                    <i class='{1}'></i>
                    <span class='title'>{2}</span>
                    <span class='selected'></span>
                    <span class='arrow open'></span>
                </a>";

            var secondTemplate = @"
                <li class='heading' style='font-size:14px;color:yellow'>
                    <i class='{0}'></i>
                    {1}
                </li>";

            var thirdTemplate = @"
                <li id='{3}'>
                    <a href='{0}'>
                        <i class='{1}'></i>
                        {2}
                    </a>
                </li>";
            var firstTemplateEnd = "</li>";
            var secondTemplateStart = "<ul class='sub-menu'>";
            var secondTemplateEnd = "</ul>"; 
            #endregion

            string tmpUrl = "";
            string url = "";
            string icon = "icon-home";
            StringBuilder sb = new StringBuilder();
            List<MenuInfo> list = BLLFactory<Menus>.Instance.GetTopMenu(Const.SystemTypeID);
            foreach (MenuInfo info in list)
            {
                if (!HasFunction(info.AuthGid))
                {
                    continue;
                }

                //一级
                icon = info.WebIcon;
                url = (!string.IsNullOrEmpty(info.Url) && info.Url.Trim() != "#") ? string.Format("{0}{1}tid={2}", info.Url, GetUrlJoiner(info.Url), info.Gid) : "javascript:;";
                sb = sb.AppendFormat(firstTemplate, url, icon, info.Name, info.Gid);

                List<MenuNodeInfo> nodeList = BLLFactory<Menus>.Instance.GetTreeByID(info.Gid);
                if (nodeList.Count > 0)
                {
                    sb = sb.Append(secondTemplateStart);//二级菜单如果有的话，增加一个标题内容
                }
                foreach (MenuNodeInfo nodeInfo in nodeList)
                {
                    if (!HasFunction(nodeInfo.AuthGid))
                    {
                        continue;
                    }

                    //二级
                    icon = nodeInfo.WebIcon;
                    tmpUrl = string.Format("{0}{1}tid={2}", nodeInfo.Url, GetUrlJoiner(nodeInfo.Url), info.Gid);
                    url = (!string.IsNullOrEmpty(nodeInfo.Url) && nodeInfo.Url.Trim() != "#") ? tmpUrl : "javascript:;";
                    sb = sb.AppendFormat(secondTemplate, icon, nodeInfo.Name);
                                        
                    foreach (MenuNodeInfo subNodeInfo in nodeInfo.Children)
                    {
                        if (!HasFunction(subNodeInfo.AuthGid))
                        {
                            continue;
                        }

                        //三级
                        icon = subNodeInfo.WebIcon;
                        //tid 为顶级分类id，sid 为第三级菜单id
                        tmpUrl = string.Format("{0}{1}tid={2}&sid={3}", subNodeInfo.Url, GetUrlJoiner(subNodeInfo.Url), info.Gid, subNodeInfo.Gid);
                        url = (!string.IsNullOrEmpty(subNodeInfo.Url) && subNodeInfo.Url.Trim() != "#") ? tmpUrl : "javascript:;";
                        sb = sb.AppendFormat(thirdTemplate, url, icon, subNodeInfo.Name, subNodeInfo.Gid);
                    }
                }
                if (nodeList.Count > 0)
                {
                    sb = sb.Append(secondTemplateEnd);//二级菜单如果有的话，增加一个标题内容结束
                }
                sb = sb.Append(firstTemplateEnd);
            }
            return sb.ToString();
        } 
        
        #endregion

        #region 辅助函数

        /// <summary>
        /// 获取URL的连接字符串，如果有?参数那么连接符为&，否则为?
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetUrlJoiner(string url)
        {
            return url.Contains("?") ? "&" : "?";
        }

        /// <summary>
        /// 生成GUID的服务器方法
        /// </summary>
        /// <returns></returns>
        public ActionResult NewGuid()
        {
            string guid = System.Guid.NewGuid().ToString();
            return Content(guid);
        }

        /// <summary>
        /// 把object对象转换为ContentResult
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected ContentResult ToJsonContent(object obj)
        {
            string result = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return Content(result);
        }

        /// <summary>
        /// 返回处理过的时间的Json字符串
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public ContentResult ToJsonContentDate(object date)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return Content(JsonConvert.SerializeObject(date, Formatting.Indented, timeConverter));
        }

        public ContentResult Content(bool result)
        {
            return Content(result.ToString().ToLower());//小写方便脚本处理
        }

        public ContentResult Content(int result)
        {
            return Content(result.ToString());
        }

        /// <summary>
        /// 把对象为json字符串
        /// </summary>
        /// <param name="obj">待序列号对象</param>
        /// <returns></returns>
        protected string ToJson(object obj)
        {
            //使用Json.NET的序列号类，能够更加高效、完美
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        protected void ClearCache()
        {
            List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                MemoryCache.Default.Remove(cacheKey);
            }
        }

        /// <summary>
        /// 调用AsposeCell控件，生成Excel文件 wujianming
        /// </summary>
        /// <param name="datatable">生成的表格数据</param>
        /// <param name="relatedPath">服务器相对路径</param>
        /// <returns></returns>
        protected virtual bool GenerateExcel(DataTable datatable, string relatedPath)
        {
            #region 把DataTable转换为Excel并输出
            /*Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
            //为单元格添加样式    
            Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];
            //设置居中
            style.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;
            //设置背景颜色
            style.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 0);
            style.Pattern = Aspose.Cells.BackgroundType.Solid;
            style.Font.IsBold = true;

            int rowIndex = 0;
            for (int i = 0; i < datatable.Columns.Count; i++)
            {
                DataColumn col = datatable.Columns[i];
                string columnName = col.Caption ?? col.ColumnName;
                workbook.Worksheets[0].Cells[rowIndex, i].PutValue(columnName);
                workbook.Worksheets[0].Cells[rowIndex, i].SetStyle(style);
            }
            rowIndex++;

            foreach (DataRow row in datatable.Rows)
            {
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    workbook.Worksheets[0].Cells[rowIndex, i].PutValue(row[i].ToString());
                }
                rowIndex++;
            }

            for (int k = 0; k < datatable.Columns.Count; k++)
            {
                workbook.Worksheets[0].AutoFitColumn(k, 0, 150);
            }
            workbook.Worksheets[0].FreezePanes(1, 0, 1, datatable.Columns.Count);

            //根据用户创建目录，确保生成的文件不会产生冲突
            string realPath = Server.MapPath(relatedPath);
            string parentPath = Directory.GetParent(realPath).FullName;
            DirectoryUtil.AssertDirExist(parentPath);

            workbook.Save(realPath, Aspose.Cells.SaveFormat.Excel97To2003);
            */
            #endregion

            return true;
        }


        /// <summary>
        /// 读取文件的字节
        /// </summary>
        /// <param name="fileData">附件信息</param>
        /// <returns></returns>
        protected virtual byte[] ReadFileBytes(HttpPostedFileBase fileData)
        {
            byte[] data;
            using (Stream inputStream = fileData.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }
            return data;
        }
        #endregion
    }
}
