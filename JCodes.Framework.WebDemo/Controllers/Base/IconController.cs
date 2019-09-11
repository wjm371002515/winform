using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Office;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using JCodes.Framework.WebUI.Common;

namespace JCodes.Framework.WebDemo.Controllers
{
    /// <summary>
    /// 图表管理控制器
    /// </summary>
    public class IconController : BaseController
    {
        /// <summary>
        /// 根据文件目录，和查找条件，查询里面的文件集合
        /// </summary>
        /// <param name="dirPath">文件目录</param>
        /// <param name="searchPatterns">查询匹配条件，如"*.gif", "*.jpg", "*.png"等</param>
        /// <returns></returns>
        private string[] GetImages(string dirPath, params string[] searchPatterns)
        {
            if (searchPatterns.Length <= 0)
            {
                return null;
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(dirPath);
                FileInfo[][] fis = new FileInfo[searchPatterns.Length][];
                int count = 0;
                for (int i = 0; i < searchPatterns.Length; i++)
                {
                    FileInfo[] fileInfos = di.GetFiles(searchPatterns[i]);
                    fis[i] = fileInfos;
                    count += fileInfos.Length;
                }

                string[] files = new string[count];
                int n = 0;
                for (int i = 0; i <= fis.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < fis[i].Length; j++)
                    {
                        string temp = fis[i][j].FullName;
                        files[n] = temp;
                        n++;
                    }
                }
                return files;
            }
        }

        /// <summary>
        /// 获取指定目录下的图片文件名列表
        /// </summary>
        /// <param name="realPath">实际路径</param>
        /// <returns></returns>
        private List<CListItem> GetImageToList(string realPath, int size)
        {
            List<CListItem> list = new List<CListItem>();
            string[] fileArray = GetImages(realPath, "*.gif", "*.jpg", "*.png");
            if (fileArray != null)
            {
                foreach (string file in fileArray)
                {
                    string fileName = Path.GetFileName(file);
                    string displayText = Path.GetFileNameWithoutExtension(file);
                    //文件名需要去除（）和[]等符号
                    displayText = CRegex.Replace(displayText, @"[)\];,\t\r ]|[\n]", "", 0);
                    displayText = CRegex.Replace(displayText, @"[(\[]", "-", 0);
                    //displayText = displayText.Replace("(", "-").Replace(")", "-").Replace("[", "-").Replace("]", "-");

                    //避免冲突，样式名称加上尺寸
                    //16*16的名称：icon-005   32*32的名称：icon-32-005
                    if (size == 16)
                    {
                        displayText = string.Format("icon-{0}", displayText);
                    }
                    else
                    {
                        displayText = string.Format("icon-{0}-{1}", size, displayText);
                    }

                    list.Add(new CListItem(fileName, displayText));
                }
            }
            return list;
        }

        /// <summary>
        /// 生成图标文件
        /// </summary>
        /// <param name="iconSize">图表尺寸，可选16,32等</param>
        /// <returns></returns>
        public ActionResult GenerateIconCSS(int iconSize = 16)
        {
            CommonResult result = new CommonResult();

            string realPath = Server.MapPath("~/Content/icons-customed/" + iconSize);
            if (Directory.Exists(realPath))
            {
                List<CListItem> list = GetImageToList(realPath, iconSize);

                try
                {
                    //使用相对路径进行构造处理
                    string template = string.Format("Content/icons-customed/{0}/icon.css.vm", iconSize);
                    NVelocityHelper helper = new NVelocityHelper(template);
                    helper.AddKeyValue("FileNameList", list);

                    helper.FileNameOfOutput = "icon";
                    helper.FileExtension = ".css";
                    helper.DirectoryOfOutput = realPath;//指定实际路径

                    string outputFilePath = helper.ExecuteFile();
                    if (!string.IsNullOrEmpty(outputFilePath))
                    {
                        result.Success = true;

                        //写入数据库
                        bool write = BLLFactory<Icon>.Instance.BatchAddIcon(list, iconSize);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(IconController));
                    result.ErrorMessage = ex.Message;
                }
            }
            else
            {
                result.ErrorMessage = "没有找到图片文件";
            }

            return ToJsonContent(result);
        }


        //public override ActionResult FindWithPager()
        //{
        //    //检查用户是否有权限，否则抛出MyDenyAccessException异常
        //    base.CheckAuthorized(AuthorizeKey.ListKey);

        //    string where = GetPagerCondition();
        //    PagerInfo pagerInfo = GetPagerInfo();
        //    List<IconInfo> list = baseBLL.FindWithPager(where, pagerInfo);

        //    //如果需要修改字段显示，则参考下面代码处理
        //    //foreach(IconInfo info in list)
        //    //{
        //    //    info.PID = BLLFactory<Icon>.Instance.GetFieldValue(info.PID, "Name");
        //    //    if (!string.IsNullOrEmpty(info.Creator))
        //    //    {
        //    //        info.Creator = BLLFactory<User>.Instance.GetFullNameByID(info.Creator.ToInt32());
        //    //    }
        //    //}

        //    //Json格式的要求{total:22,rows:{}}
        //    //构造成Json的格式传递
        //    var result = new { total = pagerInfo.RecordCount, rows = list };
        //    return ToJsonContentDate(result);
        //}

        /// <summary>
        /// 根据条件获取基于PagedList的对象集合，并返回给分页视图使用
        /// </summary>
        /// <param name="id">分页页码</param>
        /// <param name="iconsize">图标尺寸</param>
        /// <returns></returns>
        private PagedList<IconInfo> GetPageList(int? id, int? iconsize = 16)
        {
            int size = iconsize ?? 16;
            int pageIndex = id ?? 1;
            int pageSize = 200;

            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.CurrenetPageIndex = pageIndex;
            pagerInfo.PageSize = pageSize;

            string where = string.Format("iconsize = {0}", size);
            List<IconInfo> list = BLLFactory<Icon>.Instance.FindWithPager(where, pagerInfo);
            PagedList<IconInfo> pageList = pageList = new PagedList<IconInfo>(list, pageIndex, pageSize, pagerInfo.RecordCount);
            return pageList;
        }

        /// <summary>
        /// 根据条件获取分页数据集合，并绑定到视图里面
        /// </summary>
        /// <param name="id">分页页码</param>
        /// <param name="iconsize">图标尺寸</param>
        /// <returns></returns>
        public ActionResult AjaxSearchPost(int? id = 1, int? iconsize = 16)
        {
            PagedList<IconInfo> pageList = GetPageList(id, iconsize);
            return View("AjaxSearchPost", pageList);
        }

        /// <summary>
        /// 根据条件获取分页数据集合，并绑定到视图里面
        /// </summary>
        /// <param name="id">分页页码</param>
        /// <param name="iconsize">图标尺寸</param>
        /// <returns></returns>
        public ActionResult Select(int? id = 1, int? iconsize = 16)
        {
            PagedList<IconInfo> pageList = GetPageList(id, iconsize);
            return View("select", pageList);
        }

        /// <summary>
        /// 根据条件获取分页数据集合，并绑定到视图里面
        /// </summary>
        /// <param name="id">分页页码</param>
        /// <param name="iconsize">图标尺寸</param>
        /// <returns></returns>
        public ActionResult Index(int? id = 1, int? iconsize = 16)
        {
            PagedList<IconInfo> pageList = GetPageList(id, iconsize);
            return View(pageList);
        }

    }
}
