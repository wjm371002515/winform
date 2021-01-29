using Aspose.Cells;
using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using JCodes.Framework.Common.Extension;
using System.Text.RegularExpressions;

namespace JCodes.Framework.WebDemo.Controllers
{
    public partial class InterfaceController : Controller
    {
        Hashtable lstUnissuedBondInfo = Hashtable.Synchronized( new Hashtable());
        Hashtable lstDicDBUnissuedBondInfo = new Hashtable();
        Hashtable lstDicDBUnissuedBondUpdateDataInfo = new Hashtable();
        Dictionary<Int32, String> lstStringDBUnissuedBondUpdateDataInfo = new Dictionary<Int32, String>();
        Dictionary<string, string> DicProjectStatus = new Dictionary<string, string>();
        List<UnissuedBondInfo> lstDBUnissuedBondInfo = null;
        List<UnissuedBondUpdateDataInfo> lstDBUnissuedBondUpdateDataInfo = null;

        List<Task> tasks = new List<Task>();

        bool isAllComplete = false;

        AppConfig appconfig = new AppConfig();

        //List<UnissuedBondInfo> lstUnissuedBondInfo = new List<UnissuedBondInfo>();

        /// <summary>
        /// 异步获取小区投票信息
        /// </summary>
        /// <returns></returns>
        public string test_zheshang() 
        {
            return "浙商证券接口函数";
        }

        // 获取全部的统计数据
        public JsonResult GetAll()
        {
            /*
            过会（或通过发改委、交易所审核）的项目名称	
            核准总额（亿）	
            批文剩余额度（亿）	
            浙商证券预估份额（亿元）	
            批文剩余时间（天）	
            区域	
            项目类型	
            项目负责人	
            投行部门	
            批文日期	
            批文过期时间	
            今天日期	
            申报时间	
            发行进度	
            余额包销/代销
            来源
             */
            List<ReviewBondInfo> lst = new List<ReviewBondInfo>();
            lst.Clear();

            // 获取深圳
            GetSZ();

            // 获取上海
            //lst = GetSH(lst);

            // 获取发改委
            GetFGW();

            return Json(new { total = lst.Count, rows = lst }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllExamine(Int32 limit = 50, Int32 offset = 0, String name = "", String status = "", String starttime = "", String endtime = "", String _ = "")
        {
            Int32 starttick = System.Environment.TickCount;
            lstUnissuedBondInfo.Clear();

            #region 先取数据库
            lstDBUnissuedBondInfo = BLLFactory<UnissuedBond>.Instance.GetAll();
            lstDBUnissuedBondUpdateDataInfo = BLLFactory<UnissuedBondUpdateData>.Instance.GetAll();
            lstStringDBUnissuedBondUpdateDataInfo.Clear();


            foreach (var oneDBUnissuedBondInfo in lstDBUnissuedBondInfo)
            {
                if (!lstDicDBUnissuedBondInfo.ContainsKey(oneDBUnissuedBondInfo.Id))
                    lstDicDBUnissuedBondInfo.Add(oneDBUnissuedBondInfo.Id, oneDBUnissuedBondInfo);
            }
            foreach (var oneDBUnissuedBondUpdateDataInfo in lstDBUnissuedBondUpdateDataInfo)
            {
                lstDicDBUnissuedBondUpdateDataInfo.Add(oneDBUnissuedBondUpdateDataInfo.Id + "_" + oneDBUnissuedBondUpdateDataInfo.DisplayName, oneDBUnissuedBondUpdateDataInfo);

                string ProjectStatusName = string.Empty;
                if (DicProjectStatus.ContainsKey(oneDBUnissuedBondUpdateDataInfo.DisplayName))
                    ProjectStatusName = DicProjectStatus[oneDBUnissuedBondUpdateDataInfo.DisplayName];
                else
                    ProjectStatusName = oneDBUnissuedBondUpdateDataInfo.DisplayName;
                if (lstStringDBUnissuedBondUpdateDataInfo.ContainsKey(oneDBUnissuedBondUpdateDataInfo.Id))
                {
                    lstStringDBUnissuedBondUpdateDataInfo[oneDBUnissuedBondUpdateDataInfo.Id] += string.Format("{0} {1}<br />", oneDBUnissuedBondUpdateDataInfo.Date, ProjectStatusName);
                }
                else
                    lstStringDBUnissuedBondUpdateDataInfo[oneDBUnissuedBondUpdateDataInfo.Id] = string.Format("{0} {1}<br />", oneDBUnissuedBondUpdateDataInfo.Date, ProjectStatusName);
            }
            #endregion

            // 初始化默认值
            if (string.IsNullOrEmpty(starttime)) {
                starttime = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(endtime))
            {
                endtime = DateTime.Now.ToString("yyyy-MM-dd");
            }

            DicProjectStatus.Clear();
            DicProjectStatus.Add("1", "已受理");
            DicProjectStatus.Add("2", "已反馈");
            DicProjectStatus.Add("4", "通过");
            DicProjectStatus.Add("5", "未通过");
            DicProjectStatus.Add("8", "终止");
            DicProjectStatus.Add("10", "已回复交易所意见");
            DicProjectStatus.Add("11", "提交注册");
            DicProjectStatus.Add("12", "注册生效");

            // 获取深圳
            //GetSZExamine(limit, offset, name, status, starttime, endtime, _);

            // 获取上海
            //GetSHExamine(limit, offset, name, status, starttime, endtime, _);

            // 获取企业债
            GetQYZ(limit, offset, name, status, starttime, endtime, _);

            //异步等待所有任务执行完毕
            Task.WaitAll(tasks.ToArray());

            Int32 endtick = System.Environment.TickCount;
            return Json(new { responseTime = endtick - starttick, total = lstUnissuedBondInfo.Count, rows = lstUnissuedBondInfo.Values }, JsonRequestBehavior.AllowGet);
        }

        private void GetSZ() {
            // 深证
            // http://bond.szse.cn/disclosure/progressinfo/index.html
            // http://bond.szse.cn/api/report/ShowReport/data?SHOWTYPE=JSON&CATALOGID=xmjdxx&TABKEY=tab1&txtCxs=%E6%B5%99%E5%95%86
        }

        private void GetFGW() {
            // 发改委
            // https://www.ndrc.gov.cn/xxgk/zcfb/qt/
        }

        private void GetSH()
        {
            // 上海
            // http://bond.sse.com.cn/bridge/information/index_unde.shtml?unde_id=73844297-2&&unde_name=%E6%B5%99%E5%95%86%E8%AF%81%E5%88%B8%E8%82%A1%E4%BB%BD%E6%9C%89%E9%99%90%E5%85%AC%E5%8F%B8
            string url = "http://query.sse.com.cn/commonQuery.do?isPagination=true&sqlId=COMMON_SSE_ZCZZQXMLB&pageHelp.pageSize=10&area=&trade=&status=&bond_type=&begin=&under_name=%E6%B5%99%E5%95%86%E8%AF%81%E5%88%B8&issuer_name=&_=1596677637180";
            //           "http://query.sse.com.cn/commonQuery.do?
            // isPagination=true&sqlId=COMMON_SSE_ZCZZQXMLB&pageHelp.pageSize=10&area=&trade=&status=&begin=&under_name=%E6%B5%99%E5%95%86%E8%AF%81%E5%88%B8&issuer_name=&bond_type=&pageHelp.pageNo=6&pageHelp.beginPage=6&pageHelp.endPage=10&_=1596677637195"
            // 1596677637181 毫秒时间错
            /*
             GET http://query.sse.com.cn/commonQuery.do?jsonCallBack=jsonpCallback40788&isPagination=true&sqlId=COMMON_SSE_ZCZZQXMLB&pageHelp.pageSize=10&area=&trade=&status=&bond_type=&begin=&under_name=%E6%B5%99%E5%95%86%E8%AF%81%E5%88%B8&issuer_name=&_=1596677637181 HTTP/1.1
            Host: query.sse.com.cn
            Connection: keep-alive
            User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36
            Accept: * / *
            Referer: http://bond.sse.com.cn/bridge/information/
            Accept-Encoding: gzip, deflate
            Accept-Language: zh-CN,zh;q=0.9
            Cookie: yfx_c_g_u_id_10000042=_ck20080608493613557945373274207; yfx_f_l_v_t_10000042=f_t_1596674976318__r_t_1596674976318__v_t_1596674976318__r_c_0; JSESSIONID=C4F0D9AC0DA93FB396CEFF1327B436A1
             */
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.Host = "query.sse.com.cn";
            //request.Connection = "keep-alive";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36";
            request.Accept = "*/*";
            request.Referer = "http://bond.sse.com.cn/bridge/information/";
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9"); 


            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                // 请求成功的状态码：200
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string html = reader.ReadToEnd();
                            JObject jo = (JObject)JsonConvert.DeserializeObject(html);

                            Int32 totalNum = jo["result"].Count();
                            /*
                              "LIST1": "义乌市城市投资建设集团有限公司",
                              "AUDIT_NAME": "义乌市城市投资建设集团有限公司2020年非公开发行公司债券",
                              "WEN_HAO": "-",
                              "BOND_TYPE": "1",
                              "PLAN_ISSUE_AMOUNT": "19",
                              "LIST11": "913307827324296707",
                              "LIST2": "浙商证券股份有限公司",
                              "LIST22": "73844297-2",
                              "NUM": "2",
                              "AUDIT_SUB_STATUS": "-",
                              "ACCEPT_DATE": "2020-07-01",
                              "SHORT_NAME": "浙商证券",
                              "REG_APRV_WEN_HAO": "-",
                              "AREA": "浙江金华",
                              "CSRC_CODE": "i",
                              "BOND_NUM": "13507",
                              "AUDIT_STATUS": "2",
                              "SEC_NAME": " ",
                              "PUBLISH_DATE": "2020-08-04"
                             */
                            for (Int32 i = 0; i < totalNum; i++)
                            {
                                ReviewBondInfo reviewBondInfo = new ReviewBondInfo();
                                reviewBondInfo.Id = Convert.ToInt32(jo["result"][i]["BOND_NUM"].ToString());
                                reviewBondInfo.ProjectName = jo["result"][i]["AUDIT_NAME"].ToString();
                                reviewBondInfo.IssueMoney = jo["result"][i]["PLAN_ISSUE_AMOUNT"].ToString();
                                reviewBondInfo.LeftMoney = string.Empty;
                                reviewBondInfo.EstimateAmount = string.Empty;
                                reviewBondInfo.Area = jo["result"][i]["AREA"].ToString();
                                //reviewBondInfo.ProjectType = bondType[Convert.ToInt32(jo["result"][i]["BOND_TYPE"].ToString())];
                                reviewBondInfo.ProjectLeader = string.Empty;
                                reviewBondInfo.DeptName = string.Empty;
                                reviewBondInfo.ApprovalDate = string.Empty;
                                reviewBondInfo.ApprovalPassDate = string.Empty;
                                reviewBondInfo.TodayDate = DateTime.Now.ToString("yyyy-MM-dd");
                                reviewBondInfo.DeclareDate = string.Empty;
                                reviewBondInfo.PublishType = string.Empty;
                                reviewBondInfo.ConsignmentType = string.Empty;
                                reviewBondInfo.From = "上海债券网站";
                                reviewBondInfo.Remark = string.Empty;

                                //lst.Add(reviewBondInfo);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("服务器返回错误：{0}", response.StatusCode);
                }
            }

            /*
             请求明细
            GET http://query.sse.com.cn/commonQuery.do?jsonCallBack=jsonpCallback40443090&isPagination=false&sqlId=COMMON_SSE_ZCZZQXMXXXX&audit_id=13507&_=1596684281181 HTTP/1.1
            Host: query.sse.com.cn
            Connection: keep-alive
            User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36
            Accept: * / *
            Referer: http://bond.sse.com.cn/bridge/information/index_detail.shtml?bound_id=13507
            Accept-Encoding: gzip, deflate
            Accept-Language: zh-CN,zh;q=0.9
            Cookie: yfx_c_g_u_id_10000042=_ck20080608493613557945373274207; yfx_f_l_v_t_10000042=f_t_1596674976318__r_t_1596674976318__v_t_1596674976318__r_c_0; JSESSIONID=C4F0D9AC0DA93FB396CEFF1327B436A1


             */
        }

        // 获取上海
        private void GetSHExamine(Int32 limit = 50, Int32 offset = 0, String name = "", String status = "", String starttime = "", String endtime = "", String _ = "")
        {
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "开始调用 GetSHExamine", typeof(InterfaceController));
            // 上海
            // http://bond.sse.com.cn/bridge/information/index_unde.shtml?unde_id=73844297-2&&unde_name=%E6%B5%99%E5%95%86%E8%AF%81%E5%88%B8%E8%82%A1%E4%BB%BD%E6%9C%89%E9%99%90%E5%85%AC%E5%8F%B8
            string url = "http://query.sse.com.cn/commonQuery.do?isPagination=true&sqlId=COMMON_SSE_ZCZZQXMLB&pageHelp.pageSize=10&area=&trade=&status="+status+"&bond_type=&begin=&under_name="+name+"&issuer_name=&_="+_;
           
            // Page 50-100条记录
            // http://query.sse.com.cn/commonQuery.do?isPagination=true&sqlId=COMMON_SSE_ZCZZQXMXXXX_CXSLSFXQK&unde_id=73844297-2&pageHelp.pageNo=6&pageHelp.beginPage=6&pageHelp.endPage=10&_=1597422169843
            //           "http://query.sse.com.cn/commonQuery.do?
            // 如果页数少于50页
            // isPagination=true&sqlId=COMMON_SSE_ZCZZQXMLB&pageHelp.pageSize=10&area=&trade=&status=&begin=&under_name=%E6%B5%99%E5%95%86%E8%AF%81%E5%88%B8&issuer_name=&bond_type=&pageHelp.pageNo=6&pageHelp.beginPage=6&pageHelp.endPage=10&_=1596677637195"
            // 1596677637181 毫秒时间错
            /*
             GET http://query.sse.com.cn/commonQuery.do?jsonCallBack=jsonpCallback40788&isPagination=true&sqlId=COMMON_SSE_ZCZZQXMLB&pageHelp.pageSize=10&area=&trade=&status=&bond_type=&begin=&under_name=%E6%B5%99%E5%95%86%E8%AF%81%E5%88%B8&issuer_name=&_=1596677637181 HTTP/1.1
            Host: query.sse.com.cn
            Connection: keep-alive
            User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36
            Accept: * / *
            Referer: http://bond.sse.com.cn/bridge/information/
            Accept-Encoding: gzip, deflate
            Accept-Language: zh-CN,zh;q=0.9
            Cookie: yfx_c_g_u_id_10000042=_ck20080608493613557945373274207; yfx_f_l_v_t_10000042=f_t_1596674976318__r_t_1596674976318__v_t_1596674976318__r_c_0; JSESSIONID=C4F0D9AC0DA93FB396CEFF1327B436A1
             */
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "开始访问上交所债券网站:" + url, typeof(InterfaceController));
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.Host = "query.sse.com.cn";
            //request.Connection = "keep-alive";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36";
            request.Accept = "*/*";
            request.Referer = "http://bond.sse.com.cn/bridge/information/";
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                // 请求成功的状态码：200
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string html = reader.ReadToEnd();
                            JObject jo = (JObject)JsonConvert.DeserializeObject(html);

                            Int32 totalNum = jo["result"].Count();
                            /*
                              "LIST1": "义乌市城市投资建设集团有限公司",
                              "AUDIT_NAME": "义乌市城市投资建设集团有限公司2020年非公开发行公司债券",
                              "WEN_HAO": "-",
                              "BOND_TYPE": "1",
                              "PLAN_ISSUE_AMOUNT": "19",
                              "LIST11": "913307827324296707",
                              "LIST2": "浙商证券股份有限公司",
                              "LIST22": "73844297-2",
                              "NUM": "2",
                              "AUDIT_SUB_STATUS": "-",
                              "ACCEPT_DATE": "2020-07-01",
                              "SHORT_NAME": "浙商证券",
                              "REG_APRV_WEN_HAO": "-",
                              "AREA": "浙江金华",
                              "CSRC_CODE": "i",
                              "BOND_NUM": "13507",
                              "AUDIT_STATUS": "2",
                              "SEC_NAME": " ",
                              "PUBLISH_DATE": "2020-08-04"
                             */
                            for (Int32 i = 0; i < totalNum; i++)
                            {
                                lock (lstUnissuedBondInfo.SyncRoot) {
                                    // 判断时间更新 PUBLISH_DATE,如果不在时间范围内则跳出
                                    DateTime startdt = Convert.ToDateTime(starttime);
                                    DateTime enddt = Convert.ToDateTime(endtime);
                                    DateTime publishdt = Convert.ToDateTime(jo["result"][i]["PUBLISH_DATE"].ToString());
                                    if (startdt > publishdt || enddt < publishdt)
                                        break;

                                    UnissuedBondInfo unissuedBondInfo = new UnissuedBondInfo();
                                    unissuedBondInfo.Id = Convert.ToInt32(jo["result"][i]["BOND_NUM"].ToString());
                                    unissuedBondInfo.ProjectName = jo["result"][i]["AUDIT_NAME"].ToString();
                                    unissuedBondInfo.RaisedAmount = jo["result"][i]["PLAN_ISSUE_AMOUNT"].ToString();
                                    unissuedBondInfo.ProjectType = jo["result"][i]["BOND_TYPE"].ToString();
                                    unissuedBondInfo.From = "上海债券网站";
                                    unissuedBondInfo.Remark = string.Format("更新时间为:{0}", jo["result"][i]["PUBLISH_DATE"].ToString());

                                    // 如果没有则数据库新增记录
                                    if (!lstDicDBUnissuedBondInfo.ContainsKey(unissuedBondInfo.Id)) {
                                        BLLFactory<UnissuedBond>.Instance.Insert(unissuedBondInfo);
                                    }

                                    lstUnissuedBondInfo.Add(unissuedBondInfo.Id, unissuedBondInfo);
                                }
                            }
                        }
                    }
                }
                else
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("服务器返回错误：{0}, 错误内容: {1}", response.StatusCode, response.StatusDescription), typeof(InterfaceController));
                }
            }
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "结束访问上交所债券网站:" + url, typeof(InterfaceController));
            /*
             请求明细
            GET http://query.sse.com.cn/commonQuery.do?jsonCallBack=jsonpCallback40443090&isPagination=false&sqlId=COMMON_SSE_ZCZZQXMXXXX&audit_id=13507&_=1596684281181 HTTP/1.1
            Host: query.sse.com.cn
            Connection: keep-alive
            User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36
            Accept: * / *
            Referer: http://bond.sse.com.cn/bridge/information/index_detail.shtml?bound_id=13507
            Accept-Encoding: gzip, deflate
            Accept-Language: zh-CN,zh;q=0.9
            Cookie: yfx_c_g_u_id_10000042=_ck20080608493613557945373274207; yfx_f_l_v_t_10000042=f_t_1596674976318__r_t_1596674976318__v_t_1596674976318__r_c_0; JSESSIONID=C4F0D9AC0DA93FB396CEFF1327B436A1
             */
            // 返回数据
            /*
                {"actionErrors":[],"actionMessages":[],"errorMessages":[],"errors":{},"fieldErrors":{},"isPagination":"false","jsonCallBack":"jsonpCallback41918952","locale":"zh_CN","pageHelp":{"beginPage":1,"cacheSize":5,"data":null,"endDate":null,"endPage":null,"objectResult":null,"pageCount":null,"pageNo":1,"pageSize":10,"searchDate":null,"sort":null,"startDate":null,"total":0},"result":[{"LIST1":"义乌市城市投资建设集团有限公司","AUDIT_NAME":"义乌市城市投资建设集团有限公司2020年非公开发行公司债券","WEN_HAO":"上证函【2020】1721号","BOND_TYPE":"1","PLAN_ISSUE_AMOUNT":"19","LIST11":"913307827324296707","LIST2":"浙商证券股份有限公司","LIST22":"73844297-2","AUDIT_SUB_STATUS":"-","ACCEPT_DATE":"2020-07-01","REG_APRV_WEN_HAO":"-","BOND_NUM":"13507","AUDIT_STATUS":"4","PUBLISH_DATE":"2020-08-12"}],"sqlId":"COMMON_SSE_ZCZZQXMXXXX","texts":null,"type":"","validateCode":""}
             */

            // 多线程
            int maxDegreeOfParallelism = Convert.ToInt32(appconfig.AppConfigGet("maxDegreeOfParallelism"));
            
            TaskFactory taskFactory = new TaskFactory();
            try
            {
                foreach (Int32 key in lstUnissuedBondInfo.Keys)  
                //for (Int32 i = 0; i < lstUnissuedBondInfo.Count; i++)
                {
                    tasks.Add(taskFactory.StartNew(() =>
                    {
                        UnissuedBondInfo webdetail = (UnissuedBondInfo)lstUnissuedBondInfo[key];
                        /*
                         GET http://query.sse.com.cn/commonQuery.do?jsonCallBack=jsonpCallback33767066&isPagination=false&sqlId=COMMON_SSE_ZCZZQXMXXXX&audit_id=13507&_=1597284994551 HTTP/1.1
        Host: query.sse.com.cn
        Connection: keep-alive
        User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36
        Accept: * / *
        Referer: http://bond.sse.com.cn/bridge/information/index_detail.shtml?bound_id=13507
        Accept-Encoding: gzip, deflate
        Accept-Language: zh-CN,zh;q=0.9
        Cookie: yfx_c_g_u_id_10000042=_ck20080608493613557945373274207; yfx_f_l_v_t_10000042=f_t_1596674976318__r_t_1596674976318__v_t_1596674976318__r_c_0; JSESSIONID=0038C3C9FC05F764810C5426BA9382EC
                         */
                        string urlwebdetail = "http://query.sse.com.cn/commonQuery.do?isPagination=false&sqlId=COMMON_SSE_ZCZZQXMXXXX&audit_id=" + webdetail.Id + "&_=" + _;
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "开始访问上交所债券明细网站:" + urlwebdetail, typeof(InterfaceController));
                        HttpWebRequest requestdetail = (HttpWebRequest)HttpWebRequest.Create(urlwebdetail);
                        requestdetail.Method = "GET";
                        //request.Connection = "keep-alive";
                        requestdetail.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36";
                        requestdetail.Accept = "*/*";
                        requestdetail.Referer = "http://bond.sse.com.cn/bridge/information/index_detail.shtml?bound_id=" + webdetail.Id;
                        requestdetail.Headers.Add("Accept-Encoding", "gzip, deflate");
                        requestdetail.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");

                        using (HttpWebResponse response = (HttpWebResponse)requestdetail.GetResponse())
                        {
                            // 请求成功的状态码：200
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                using (Stream stream = response.GetResponseStream())
                                {
                                    using (StreamReader reader = new StreamReader(stream))
                                    {
                                        string html = reader.ReadToEnd();
                                        JObject jo = (JObject)JsonConvert.DeserializeObject(html);

                                        Int32 totalNum = jo["result"].Count();
                                        /*
                                         {  "LIST1": "杭州萧山三阳房地产投资管理有限公司",  
                                            "AUDIT_NAME": "杭州萧山三阳房地产投资管理有限公司2020年非公开发行公司债券",  
                                            "WEN_HAO": "-",  
                                            "BOND_TYPE": "1",  
                                            "PLAN_ISSUE_AMOUNT": "20",  
                                            "LIST11": "74717928-0",  
                                            "LIST2": "国开证券股份有限公司,浙商证券股份有限公司",  
                                            "LIST22": "75770354-1,73844297-2",  
                                            "NUM": "1",  
                                            "AUDIT_SUB_STATUS": "-",  
                                            "ACCEPT_DATE": "2020-07-06",  
                                            "SHORT_NAME": "国开证券,浙商证券",  
                                            "REG_APRV_WEN_HAO": "-",  
                                            "AREA": "浙江杭州",  
                                            "CSRC_CODE": "a",  
                                            "BOND_NUM": "13557",  
                                            "AUDIT_STATUS": "2",  
                                            "SEC_NAME": " ",  
                                            "PUBLISH_DATE": "2020-08-12"}
                                         */

                                        for (Int32 j = 0; j < totalNum; j++)
                                        {
                                            lock (lstUnissuedBondInfo.SyncRoot) {
                                                // 申报日期就是取受理日期
                                                webdetail.DeclareTime = jo["result"][j]["ACCEPT_DATE"].ToString();
                                                webdetail.ProjectStatus = jo["result"][j]["AUDIT_STATUS"].ToString();
                                                webdetail.Managers = jo["result"][j]["LIST2"].ToString();
                                                webdetail.DocNum = jo["result"][j]["WEN_HAO"].ToString();
                                                // 备用字段填写更新日期
                                                webdetail.Data1 = jo["result"][j]["PUBLISH_DATE"].ToString();

                                                webdetail.DeptId = (lstDicDBUnissuedBondInfo[webdetail.Id] as UnissuedBondInfo).DeptId;
                                                webdetail.DeptName = (lstDicDBUnissuedBondInfo[webdetail.Id] as UnissuedBondInfo).DeptName;
                                                webdetail.ProjectLeader = (lstDicDBUnissuedBondInfo[webdetail.Id] as UnissuedBondInfo).ProjectLeader;
                                                try {
                                                    string ProjectStatusName = string.Empty;
                                                    if (DicProjectStatus.ContainsKey(webdetail.ProjectStatus))
                                                        ProjectStatusName = DicProjectStatus[webdetail.ProjectStatus];
                                                    else
                                                        ProjectStatusName = webdetail.ProjectStatus;
                                                    string DBProjectStatusName = string.Empty;
                                                    if (lstStringDBUnissuedBondUpdateDataInfo.ContainsKey(webdetail.Id))
                                                        DBProjectStatusName = lstStringDBUnissuedBondUpdateDataInfo[webdetail.Id];

                                                    // 特殊判断假如在缓存中存在了 则不添加，如果不存在则新增
                                                    if (lstDicDBUnissuedBondUpdateDataInfo.Contains(webdetail.Id + "_" + webdetail.ProjectStatus))
                                                    {
                                                        webdetail.ProjectStatusDetail = DBProjectStatusName;
                                                    }
                                                    else
                                                    {
                                                        webdetail.ProjectStatusDetail = DBProjectStatusName +
                                                           string.Format("<span style='color: red'>{0} {1}</span>", webdetail.Data1.Replace("-", "").ToInt32(), ProjectStatusName);
                                                    }

                                                    if (!lstDicDBUnissuedBondUpdateDataInfo.ContainsKey(webdetail.Id + "_" + webdetail.ProjectStatus))
                                                    {
                                                        UnissuedBondUpdateDataInfo unissuedBondUpdateDataInfo = new UnissuedBondUpdateDataInfo();
                                                        unissuedBondUpdateDataInfo.Id = webdetail.Id;
                                                        unissuedBondUpdateDataInfo.DisplayName = webdetail.ProjectStatus;
                                                        unissuedBondUpdateDataInfo.Date = webdetail.Data1.Replace("-", "").ToInt32();
                                                        BLLFactory<UnissuedBondUpdateData>.Instance.Insert(unissuedBondUpdateDataInfo);

                                                        lstDicDBUnissuedBondUpdateDataInfo.Add(webdetail.Id + "_" + webdetail.ProjectStatus, unissuedBondUpdateDataInfo);
                                                    }
                                                }
                                                catch (Exception ex) {
                                                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("新增内容出错：{0}", ex.Message), typeof(InterfaceController));
                                                }

                                                
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("服务器返回错误：{0}, 错误内容: {1}", response.StatusCode, response.StatusDescription), typeof(InterfaceController));
                            }
                        }
                    }));

                    if (tasks.Count >= maxDegreeOfParallelism)
                    {
                        Task.WaitAny(tasks.ToArray());
                        tasks = tasks.Where(t => t.Status == TaskStatus.Running).ToList();
                    }
                }
            }
            catch (Exception ex) {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("多线程出错啦：{0}", ex.Message), typeof(InterfaceController));
            }

            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "结束调用 GetSHExamine", typeof(InterfaceController));
        }

        // 获取上海
        private void GetSZExamine(Int32 limit = 50, Int32 offset = 0, String name = "", String status = "", String starttime = "", String endtime = "", String _ = "")
        {
            return;
//            // 上海
//            // http://bond.sse.com.cn/bridge/information/index_unde.shtml?unde_id=73844297-2&&unde_name=%E6%B5%99%E5%95%86%E8%AF%81%E5%88%B8%E8%82%A1%E4%BB%BD%E6%9C%89%E9%99%90%E5%85%AC%E5%8F%B8
//            string url = "http://query.sse.com.cn/commonQuery.do?isPagination=true&sqlId=COMMON_SSE_ZCZZQXMLB&pageHelp.pageSize=10&area=&trade=&status=&bond_type=&begin=&under_name=" + name + "&issuer_name=&_=" + _;
//            //           "http://query.sse.com.cn/commonQuery.do?
//            // isPagination=true&sqlId=COMMON_SSE_ZCZZQXMLB&pageHelp.pageSize=10&area=&trade=&status=&begin=&under_name=%E6%B5%99%E5%95%86%E8%AF%81%E5%88%B8&issuer_name=&bond_type=&pageHelp.pageNo=6&pageHelp.beginPage=6&pageHelp.endPage=10&_=1596677637195"
//            // 1596677637181 毫秒时间错
//            /*
//             GET http://query.sse.com.cn/commonQuery.do?jsonCallBack=jsonpCallback40788&isPagination=true&sqlId=COMMON_SSE_ZCZZQXMLB&pageHelp.pageSize=10&area=&trade=&status=&bond_type=&begin=&under_name=%E6%B5%99%E5%95%86%E8%AF%81%E5%88%B8&issuer_name=&_=1596677637181 HTTP/1.1
//            Host: query.sse.com.cn
//            Connection: keep-alive
//            User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36
//            Accept: * / *
//            Referer: http://bond.sse.com.cn/bridge/information/
//            Accept-Encoding: gzip, deflate
//            Accept-Language: zh-CN,zh;q=0.9
//            Cookie: yfx_c_g_u_id_10000042=_ck20080608493613557945373274207; yfx_f_l_v_t_10000042=f_t_1596674976318__r_t_1596674976318__v_t_1596674976318__r_c_0; JSESSIONID=C4F0D9AC0DA93FB396CEFF1327B436A1
//             */
//            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "开始访问上交所债券网站:" + url, typeof(InterfaceController));
//            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
//            request.Method = "GET";
//            request.Host = "query.sse.com.cn";
//            //request.Connection = "keep-alive";
//            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36";
//            request.Accept = "*/*";
//            request.Referer = "http://bond.sse.com.cn/bridge/information/";
//            request.Headers.Add("Accept-Encoding", "gzip, deflate");
//            request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");

//            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
//            {
//                // 请求成功的状态码：200
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    using (Stream stream = response.GetResponseStream())
//                    {
//                        using (StreamReader reader = new StreamReader(stream))
//                        {
//                            string html = reader.ReadToEnd();
//                            JObject jo = (JObject)JsonConvert.DeserializeObject(html);

//                            Int32 totalNum = jo["result"].Count();
//                            /*
//                              "LIST1": "义乌市城市投资建设集团有限公司",
//                              "AUDIT_NAME": "义乌市城市投资建设集团有限公司2020年非公开发行公司债券",
//                              "WEN_HAO": "-",
//                              "BOND_TYPE": "1",
//                              "PLAN_ISSUE_AMOUNT": "19",
//                              "LIST11": "913307827324296707",
//                              "LIST2": "浙商证券股份有限公司",
//                              "LIST22": "73844297-2",
//                              "NUM": "2",
//                              "AUDIT_SUB_STATUS": "-",
//                              "ACCEPT_DATE": "2020-07-01",
//                              "SHORT_NAME": "浙商证券",
//                              "REG_APRV_WEN_HAO": "-",
//                              "AREA": "浙江金华",
//                              "CSRC_CODE": "i",
//                              "BOND_NUM": "13507",
//                              "AUDIT_STATUS": "2",
//                              "SEC_NAME": " ",
//                              "PUBLISH_DATE": "2020-08-04"
//                             */
//                            for (Int32 i = 0; i < totalNum; i++)
//                            {
//                                UnissuedBondInfo unissuedBondInfo = new UnissuedBondInfo();
//                                unissuedBondInfo.Id = Convert.ToInt32(jo["result"][i]["BOND_NUM"].ToString());
//                                unissuedBondInfo.ProjectName = jo["result"][i]["AUDIT_NAME"].ToString();
//                                unissuedBondInfo.RaisedAmount = jo["result"][i]["PLAN_ISSUE_AMOUNT"].ToString();
//                                unissuedBondInfo.ProjectType = jo["result"][i]["BOND_TYPE"].ToString();
//                                unissuedBondInfo.ProjectLeader = string.Empty;
//                                unissuedBondInfo.ProjectProgress = string.Empty;
//                                unissuedBondInfo.From = "上海债券网站";
//                                unissuedBondInfo.Remark = string.Empty;
//                                //lstUnissuedBondInfo.Add(unissuedBondInfo);
//                            }
//                        }
//                    }
//                }
//                else
//                {
//                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("服务器返回错误：{0}, 错误内容: {1}", response.StatusCode, response.StatusDescription), typeof(InterfaceController));
//                }
//            }
//            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "结束访问上交所债券网站:" + url, typeof(InterfaceController));
//            /*
//             请求明细
//            GET http://query.sse.com.cn/commonQuery.do?jsonCallBack=jsonpCallback40443090&isPagination=false&sqlId=COMMON_SSE_ZCZZQXMXXXX&audit_id=13507&_=1596684281181 HTTP/1.1
//            Host: query.sse.com.cn
//            Connection: keep-alive
//            User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36
//            Accept: * / *
//            Referer: http://bond.sse.com.cn/bridge/information/index_detail.shtml?bound_id=13507
//            Accept-Encoding: gzip, deflate
//            Accept-Language: zh-CN,zh;q=0.9
//            Cookie: yfx_c_g_u_id_10000042=_ck20080608493613557945373274207; yfx_f_l_v_t_10000042=f_t_1596674976318__r_t_1596674976318__v_t_1596674976318__r_c_0; JSESSIONID=C4F0D9AC0DA93FB396CEFF1327B436A1
//             */
//            // 返回数据
//            /*
//                {"actionErrors":[],"actionMessages":[],"errorMessages":[],"errors":{},"fieldErrors":{},"isPagination":"false","jsonCallBack":"jsonpCallback41918952","locale":"zh_CN","pageHelp":{"beginPage":1,"cacheSize":5,"data":null,"endDate":null,"endPage":null,"objectResult":null,"pageCount":null,"pageNo":1,"pageSize":10,"searchDate":null,"sort":null,"startDate":null,"total":0},"result":[{"LIST1":"义乌市城市投资建设集团有限公司","AUDIT_NAME":"义乌市城市投资建设集团有限公司2020年非公开发行公司债券","WEN_HAO":"上证函【2020】1721号","BOND_TYPE":"1","PLAN_ISSUE_AMOUNT":"19","LIST11":"913307827324296707","LIST2":"浙商证券股份有限公司","LIST22":"73844297-2","AUDIT_SUB_STATUS":"-","ACCEPT_DATE":"2020-07-01","REG_APRV_WEN_HAO":"-","BOND_NUM":"13507","AUDIT_STATUS":"4","PUBLISH_DATE":"2020-08-12"}],"sqlId":"COMMON_SSE_ZCZZQXMXXXX","texts":null,"type":"","validateCode":""}
//             */
//            foreach (var webdetail in lstUnissuedBondInfo)
//            {
//                /*
//                 GET http://query.sse.com.cn/commonQuery.do?jsonCallBack=jsonpCallback33767066&isPagination=false&sqlId=COMMON_SSE_ZCZZQXMXXXX&audit_id=13507&_=1597284994551 HTTP/1.1
//Host: query.sse.com.cn
//Connection: keep-alive
//User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36
//Accept: * / *
//Referer: http://bond.sse.com.cn/bridge/information/index_detail.shtml?bound_id=13507
//Accept-Encoding: gzip, deflate
//Accept-Language: zh-CN,zh;q=0.9
//Cookie: yfx_c_g_u_id_10000042=_ck20080608493613557945373274207; yfx_f_l_v_t_10000042=f_t_1596674976318__r_t_1596674976318__v_t_1596674976318__r_c_0; JSESSIONID=0038C3C9FC05F764810C5426BA9382EC
//                 */
//                string urlwebdetail = "http://query.sse.com.cn/commonQuery.do?isPagination=false&sqlId=COMMON_SSE_ZCZZQXMXXXX&audit_id=" + webdetail.Id + "&_=" + _;
//                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "开始访问上交所债券明细网站:" + urlwebdetail, typeof(InterfaceController));
//                HttpWebRequest requestdetail = (HttpWebRequest)HttpWebRequest.Create(urlwebdetail);
//                requestdetail.Method = "GET";
//                //request.Connection = "keep-alive";
//                requestdetail.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36";
//                requestdetail.Accept = "*/*";
//                requestdetail.Referer = "http://bond.sse.com.cn/bridge/information/index_detail.shtml?bound_id=" + webdetail.Id;
//                requestdetail.Headers.Add("Accept-Encoding", "gzip, deflate");
//                requestdetail.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");

//                using (HttpWebResponse response = (HttpWebResponse)requestdetail.GetResponse())
//                {
//                    // 请求成功的状态码：200
//                    if (response.StatusCode == HttpStatusCode.OK)
//                    {
//                        using (Stream stream = response.GetResponseStream())
//                        {
//                            using (StreamReader reader = new StreamReader(stream))
//                            {
//                                string html = reader.ReadToEnd();
//                                JObject jo = (JObject)JsonConvert.DeserializeObject(html);

//                                Int32 totalNum = jo["result"].Count();
//                                /*
//                                 {  "LIST1": "杭州萧山三阳房地产投资管理有限公司",  
//                                    "AUDIT_NAME": "杭州萧山三阳房地产投资管理有限公司2020年非公开发行公司债券",  
//                                    "WEN_HAO": "-",  
//                                    "BOND_TYPE": "1",  
//                                    "PLAN_ISSUE_AMOUNT": "20",  
//                                    "LIST11": "74717928-0",  
//                                    "LIST2": "国开证券股份有限公司,浙商证券股份有限公司",  
//                                    "LIST22": "75770354-1,73844297-2",  
//                                    "NUM": "1",  
//                                    "AUDIT_SUB_STATUS": "-",  
//                                    "ACCEPT_DATE": "2020-07-06",  
//                                    "SHORT_NAME": "国开证券,浙商证券",  
//                                    "REG_APRV_WEN_HAO": "-",  
//                                    "AREA": "浙江杭州",  
//                                    "CSRC_CODE": "a",  
//                                    "BOND_NUM": "13557",  
//                                    "AUDIT_STATUS": "2",  
//                                    "SEC_NAME": " ",  
//                                    "PUBLISH_DATE": "2020-08-12"}
//                                 */

//                                for (Int32 i = 0; i < totalNum; i++)
//                                {
//                                    webdetail.DeclareTIme = jo["result"][i]["ACCEPT_DATE"].ToString();
//                                    webdetail.ProjectStatus = jo["result"][i]["AUDIT_STATUS"].ToString();
//                                    webdetail.Managers = jo["result"][i]["LIST2"].ToString();
//                                    webdetail.DocNum = jo["result"][i]["WEN_HAO"].ToString();
//                                    webdetail.DeptId = "数据库";
//                                    webdetail.DeptName = "数据库";
//                                    webdetail.ProjectLeader = "数据库";
//                                    webdetail.ProjectStatusDetail = "数据库";
//                                }
//                            }
//                        }
//                    }
//                    else
//                    {
//                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("服务器返回错误：{0}, 错误内容: {1}", response.StatusCode, response.StatusDescription), typeof(InterfaceController));
//                    }
//                }
//                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "结束访问上交所债券明细网站:" + urlwebdetail, typeof(InterfaceController));
//            }
        }

        private void GetQYZ(Int32 limit = 50, Int32 offset = 0, String name = "", String status = "", String starttime = "", String endtime = "", String _ = "")
        {

            List<string> pageLst = new List<string>();
            List<string> HrefLst = new List<string>();
            string regOptionsStr = @"<option\svalue=\""(?<Page>\d+)\""\s\s>";
            string regDataStr = @"<a\shref=\""/Info/(?<Href>\d+)\""\stitle=\""(?<Title>[\w|\（|\）|\(|\)]+)\""\starget=\""_blank\"">";
            string regDetailWebStr = @"债券名称(?<Name>[\w|\（|\）|\(|\)]+)申报规模(?<Scale>(\d+(.\d+)?\w+))主承销商(?<Leader>[\w|\（|\）|\(|\)|、|：|:|；|;|，|,|\s]+)办理状态(?<DealStatus>[\w|\（|\）|\(|\)]+)更新时间(?<UpdateDate>(\d+年\d+月\d+日))";

            // https://www.chinabond.com.cn/jsp/include/CB_CN/zqxx/zqxxInfo.jsp
            // 第一页 信息
            /*
            xmmc: 
            blzt: 
            zcdcx: 
            _tp_zqxx: 2
             */
            // https://www.chinabond.com.cn/Info/155162429
            string url = "https://www.chinabond.com.cn/jsp/include/CB_CN/zqxx/zqxxInfo.jsp";
            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "开始访问中国债券信息网网站:" + url, typeof(InterfaceController));
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.Host = "www.chinabond.com.cn";
            //request.Connection = "keep-alive";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36";
            request.Accept = "*/*";
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");

            // TODO 403 错误
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                // 请求成功的状态码：200
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (response.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
                    {
                        using (System.IO.Stream stream = response.GetResponseStream())
                        {
                            using (var zipStream =
                                new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress))
                            {
                                Encoding enc = GetEncoding(url);
                                using (StreamReader reader = new System.IO.StreamReader(zipStream, enc))
                                {
                                    string html = reader.ReadToEnd();
                                    #region 获取首页的内容
                                    Regex regData = new Regex(regDataStr);
                                    MatchCollection matchesData = regData.Matches(html);

                                    // 取得匹配项列表
                                    foreach (Match match in matchesData) {
                                        if (!HrefLst.Contains(match.Groups["Href"].Value))
                                            HrefLst.Add(match.Groups["Href"].Value);
                                    }
                                        
                                    #endregion

                                    #region 获取页码信息
                                    Regex regOptions = new Regex(regOptionsStr);
                                    MatchCollection matchesOptions = regOptions.Matches(html);
                                    // 取得匹配项列表
                                    foreach (Match match in matchesOptions)
                                        pageLst.Add(match.Groups["Page"].Value);
                                    #endregion
                                }
                            }
                        }
                    }
                    else
                    {
                        using (System.IO.Stream stream = response.GetResponseStream())
                        {
                            Encoding enc = GetEncoding(url);
                            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream, enc))
                            {
                                string html = reader.ReadToEnd();
                                #region 获取首页的内容
                                Regex regData = new Regex(regDataStr);
                                MatchCollection matchesData = regData.Matches(html);

                                // 取得匹配项列表
                                foreach (Match match in matchesData)
                                {
                                    if (!HrefLst.Contains(match.Groups["Href"].Value))
                                        HrefLst.Add(match.Groups["Href"].Value);
                                } 
                                #endregion

                                #region 获取页码信息
                                Regex regOptions = new Regex(regOptionsStr);
                                MatchCollection matchesOptions = regOptions.Matches(html);
                                // 取得匹配项列表
                                foreach (Match match in matchesOptions)
                                    pageLst.Add(match.Groups["Page"].Value);
                                #endregion
                            }
                        }
                    }
                }
                else
                {
                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("服务器返回错误：{0}, 错误内容: {1}", response.StatusCode, response.StatusDescription), typeof(InterfaceController));
                }
            }

            // 获取全部内容
            foreach (var urlpage in pageLst) {
                string urlhome = "https://www.chinabond.com.cn/jsp/include/CB_CN/zqxx/zqxxInfo.jsp";
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "开始访问中国债券信息网网站:" + urlhome, typeof(InterfaceController));
                HttpWebRequest requestPage = (HttpWebRequest)HttpWebRequest.Create(url);
                requestPage.Host = "www.chinabond.com.cn";
                //request.Connection = "keep-alive";
                requestPage.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36";
                requestPage.Accept = "*/*";
                requestPage.Headers.Add("Accept-Encoding", "gzip, deflate");
                requestPage.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
                string content = "xmmc=&blzt=&zcdcx=&_tp_zqxx=" + urlpage;
                byte[] bs = Encoding.UTF8.GetBytes(content);
                requestPage.Method = "POST";
                requestPage.ContentType = "application/x-www-form-urlencoded";
                requestPage.ContentLength = bs.Length;
                //提交请求数据
                Stream reqStream = requestPage.GetRequestStream();
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Close();

                using (HttpWebResponse response = (HttpWebResponse)requestPage.GetResponse())
                {
                    // 请求成功的状态码：200
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        if (response.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
                        {
                            using (System.IO.Stream stream = response.GetResponseStream())
                            {
                                using (var zipStream =
                                    new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress))
                                {
                                    Encoding enc = GetEncoding(url);
                                    using (StreamReader reader = new System.IO.StreamReader(zipStream, enc))
                                    {
                                        string html = reader.ReadToEnd();
                                        #region 获取首页的内容
                                        Regex regData = new Regex(regDataStr);
                                        MatchCollection matchesData = regData.Matches(html);

                                        // 取得匹配项列表
                                        foreach (Match match in matchesData)
                                            HrefLst.Add(match.Groups["Href"].Value);
                                        #endregion
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (System.IO.Stream stream = response.GetResponseStream())
                            {
                                Encoding enc = GetEncoding(url);
                                using (System.IO.StreamReader reader = new System.IO.StreamReader(stream, enc))
                                {
                                    string html = reader.ReadToEnd();
                                    #region 获取首页的内容
                                    Regex regData = new Regex(regDataStr);
                                    MatchCollection matchesData = regData.Matches(html);

                                    // 取得匹配项列表
                                    foreach (Match match in matchesData)
                                        HrefLst.Add(match.Groups["Href"].Value);
                                    #endregion
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("服务器返回错误：{0}", response.StatusCode);
                    }
                }
            }

            #region 多线程取数据
           int maxDegreeOfParallelism = Convert.ToInt32(appconfig.AppConfigGet("maxDegreeOfParallelism"));
            TaskFactory taskFactory = new TaskFactory();
            try
            {
                foreach (string key in HrefLst)
                {
                    tasks.Add(taskFactory.StartNew(() =>
                    {
                        // 先判断数据库是否存在，不再进行大范围发送请求 DOS
                        UnissuedBondInfo unissuedBondinfo = BLLFactory<UnissuedBond>.Instance.FindById(key);
                        // 如果不存在就需要更新，如果存在则判断是否存在关键字，Key 跟企业名字一一对应
                        if (unissuedBondinfo != null)
                        {
                            // 如果包含了，则新增此数据
                            if (unissuedBondinfo.Managers.Contains(name))
                            {
                                lock (lstUnissuedBondInfo.SyncRoot)
                                {
                                    lstUnissuedBondInfo.Add(unissuedBondinfo.Id, unissuedBondinfo);
                                }
                            }
                        }
                        else {
                            string urlwebdetail = "https://www.chinabond.com.cn/Info/" + key;
                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_DEBUG, "开始访问中国债券信息网明细网站:" + urlwebdetail, typeof(InterfaceController));
                            HttpWebRequest requestdetail = (HttpWebRequest)HttpWebRequest.Create(urlwebdetail);
                            requestdetail.Method = "GET";
                            //request.Connection = "keep-alive";
                            requestdetail.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36";
                            requestdetail.Accept = "*/*";
                            requestdetail.Headers.Add("Accept-Encoding", "gzip, deflate");
                            requestdetail.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");

                            using (HttpWebResponse response = (HttpWebResponse)requestdetail.GetResponse())
                            {
                                // 请求成功的状态码：200
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    if (response.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
                                    {
                                        using (System.IO.Stream stream = response.GetResponseStream())
                                        {
                                            using (var zipStream =
                                                new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress))
                                            {
                                                Encoding enc = GetEncoding(url);
                                                using (StreamReader reader = new System.IO.StreamReader(zipStream, enc))
                                                {
                                                    string html = reader.ReadToEnd();

                                                    string removeTagHtml = ReplaceHtmlTag(html);
                                                    #region 获取首页的内容
                                                    Regex regData = new Regex(regDetailWebStr);
                                                    MatchCollection matchesData = regData.Matches(removeTagHtml);

                                                    if (matchesData.Count != 1)
                                                    {
                                                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("共计{0}匹配内容， 内容为：{1}", matchesData.Count, removeTagHtml), typeof(InterfaceController));
                                                    }
                                                    else
                                                    {
                                                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("债券名称{0} 申报规模{1} 主承销商{2} 办理状态{3} 更新时间{4}", matchesData[0].Groups["Name"].Value, matchesData[0].Groups["Scale"].Value, matchesData[0].Groups["Leader"].Value, matchesData[0].Groups["DealStatus"].Value, matchesData[0].Groups["UpdateDate"].Value), typeof(InterfaceController));

                                                        // 包含名字才需要收集信息
                                                        /*if (matchesData[0].Groups["Leader"].Value.Contains(name))
                                                        {*/
                                                        #region 取到数据处理
                                                        lock (lstUnissuedBondInfo.SyncRoot)
                                                        {
                                                            // 判断时间更新 PUBLISH_DATE,如果不在时间范围内则跳出
                                                            DateTime startdt = Convert.ToDateTime(starttime);
                                                            DateTime enddt = Convert.ToDateTime(endtime);
                                                            /*DateTime publishdt = Convert.ToDateTime(jo["result"][i]["PUBLISH_DATE"].ToString());
                                                            if (startdt > publishdt || enddt < publishdt)
                                                                break;*/

                                                            UnissuedBondInfo unissuedBondInfo = new UnissuedBondInfo();
                                                            unissuedBondInfo.Id = Convert.ToInt32(key);
                                                            unissuedBondInfo.ProjectName = matchesData[0].Groups["Name"].Value;
                                                            unissuedBondInfo.RaisedAmount = matchesData[0].Groups["Scale"].Value;
                                                            unissuedBondInfo.ProjectType = "未知";

                                                            unissuedBondInfo.DeclareTime = "";
                                                            unissuedBondInfo.ProjectStatus = matchesData[0].Groups["DealStatus"].Value;
                                                            unissuedBondInfo.Managers = matchesData[0].Groups["Leader"].Value;
                                                            unissuedBondInfo.DocNum = "";
                                                            if (lstDicDBUnissuedBondInfo.ContainsKey(unissuedBondInfo.Id))
                                                            {
                                                                unissuedBondInfo.DeptId = (lstDicDBUnissuedBondInfo[unissuedBondInfo.Id] as UnissuedBondInfo).DeptId;
                                                                unissuedBondInfo.DeptName = (lstDicDBUnissuedBondInfo[unissuedBondInfo.Id] as UnissuedBondInfo).DeptName;
                                                                unissuedBondInfo.ProjectLeader = (lstDicDBUnissuedBondInfo[unissuedBondInfo.Id] as UnissuedBondInfo).ProjectLeader;
                                                            }
                                                            unissuedBondInfo.Data1 = matchesData[0].Groups["UpdateDate"].Value;

                                                            try
                                                            {
                                                                string DBProjectStatusName = string.Empty;
                                                                if (lstStringDBUnissuedBondUpdateDataInfo.ContainsKey(unissuedBondInfo.Id))
                                                                    DBProjectStatusName = lstStringDBUnissuedBondUpdateDataInfo[unissuedBondInfo.Id];

                                                                // 特殊判断假如在缓存中存在了 则不添加，如果不存在则新增
                                                                if (lstDicDBUnissuedBondUpdateDataInfo.Contains(unissuedBondInfo.Id + "_" + unissuedBondInfo.ProjectStatus))
                                                                {
                                                                    unissuedBondInfo.ProjectStatusDetail = DBProjectStatusName;
                                                                }
                                                                else
                                                                {
                                                                    unissuedBondInfo.ProjectStatusDetail = DBProjectStatusName +
                                                                       string.Format("<span style='color: red'>{0} {1}</span>", unissuedBondInfo.Data1.Replace("-", "").ToInt32(), unissuedBondInfo.ProjectStatus);
                                                                }

                                                                if (!lstDicDBUnissuedBondUpdateDataInfo.ContainsKey(unissuedBondInfo.Id + "_" + unissuedBondInfo.ProjectStatus))
                                                                {
                                                                    UnissuedBondUpdateDataInfo unissuedBondUpdateDataInfo = new UnissuedBondUpdateDataInfo();
                                                                    unissuedBondUpdateDataInfo.Id = unissuedBondInfo.Id;
                                                                    unissuedBondUpdateDataInfo.DisplayName = unissuedBondInfo.ProjectStatus;
                                                                    unissuedBondUpdateDataInfo.Date = unissuedBondInfo.Data1.Replace("-", "").ToInt32();
                                                                    BLLFactory<UnissuedBondUpdateData>.Instance.Insert(unissuedBondUpdateDataInfo);

                                                                    lstDicDBUnissuedBondUpdateDataInfo.Add(unissuedBondInfo.Id + "_" + unissuedBondInfo.ProjectStatus, unissuedBondUpdateDataInfo);
                                                                }
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("新增内容出错：{0}", ex.Message), typeof(InterfaceController));
                                                            }

                                                            unissuedBondInfo.From = "中国债券信息网";

                                                            unissuedBondInfo.Remark = string.Format("更新时间为:{0}", matchesData[0].Groups["UpdateDate"].Value);

                                                            // 如果没有则数据库新增记录
                                                            if (!lstDicDBUnissuedBondInfo.ContainsKey(unissuedBondInfo.Id))
                                                            {
                                                                BLLFactory<UnissuedBond>.Instance.Insert(unissuedBondInfo);
                                                            }

                                                            if (unissuedBondInfo.Managers.Contains(name))
                                                            {
                                                                lstUnissuedBondInfo.Add(unissuedBondInfo.Id, unissuedBondInfo);
                                                            }

                                                        }

                                                        #endregion
                                                        /*}*/
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        using (System.IO.Stream stream = response.GetResponseStream())
                                        {
                                            Encoding enc = GetEncoding(url);
                                            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream, enc))
                                            {
                                                string html = reader.ReadToEnd();

                                                string removeTagHtml = ReplaceHtmlTag(html);
                                                #region 获取首页的内容
                                                Regex regData = new Regex(regDetailWebStr);
                                                MatchCollection matchesData = regData.Matches(removeTagHtml);

                                                if (matchesData.Count != 1)
                                                {
                                                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("共计{0}匹配内容， 内容为：{1}", matchesData.Count, removeTagHtml), typeof(InterfaceController));
                                                }
                                                else
                                                {
                                                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("债券名称{0} 申报规模{1} 主承销商{2} 办理状态{3} 更新时间{4}", matchesData[0].Groups["Name"].Value, matchesData[0].Groups["Scale"].Value, matchesData[0].Groups["Leader"].Value, matchesData[0].Groups["DealStatus"].Value, matchesData[0].Groups["UpdateDate"].Value), typeof(InterfaceController));

                                                    // 包含名字才需要收集信息
                                                    /*if (matchesData[0].Groups["Leader"].Value.Contains(name))
                                                    {*/
                                                    #region 取到数据处理
                                                    lock (lstUnissuedBondInfo.SyncRoot)
                                                    {
                                                        // 判断时间更新 PUBLISH_DATE,如果不在时间范围内则跳出
                                                        DateTime startdt = Convert.ToDateTime(starttime);
                                                        DateTime enddt = Convert.ToDateTime(endtime);
                                                        /*DateTime publishdt = Convert.ToDateTime(jo["result"][i]["PUBLISH_DATE"].ToString());
                                                        if (startdt > publishdt || enddt < publishdt)
                                                            break;*/

                                                        UnissuedBondInfo unissuedBondInfo = new UnissuedBondInfo();
                                                        unissuedBondInfo.Id = Convert.ToInt32(key);
                                                        unissuedBondInfo.ProjectName = matchesData[0].Groups["Name"].Value;
                                                        unissuedBondInfo.RaisedAmount = matchesData[0].Groups["Scale"].Value;
                                                        unissuedBondInfo.ProjectType = "未知";

                                                        unissuedBondInfo.DeclareTime = "";
                                                        unissuedBondInfo.ProjectStatus = matchesData[0].Groups["DealStatus"].Value;
                                                        unissuedBondInfo.Managers = matchesData[0].Groups["Leader"].Value;
                                                        unissuedBondInfo.DocNum = "";
                                                        if (lstDicDBUnissuedBondInfo.ContainsKey(unissuedBondInfo.Id))
                                                        {
                                                            unissuedBondInfo.DeptId = (lstDicDBUnissuedBondInfo[unissuedBondInfo.Id] as UnissuedBondInfo).DeptId;
                                                            unissuedBondInfo.DeptName = (lstDicDBUnissuedBondInfo[unissuedBondInfo.Id] as UnissuedBondInfo).DeptName;
                                                            unissuedBondInfo.ProjectLeader = (lstDicDBUnissuedBondInfo[unissuedBondInfo.Id] as UnissuedBondInfo).ProjectLeader;
                                                        }
                                                        unissuedBondInfo.Data1 = matchesData[0].Groups["UpdateDate"].Value;

                                                        try
                                                        {
                                                            string DBProjectStatusName = string.Empty;
                                                            if (lstStringDBUnissuedBondUpdateDataInfo.ContainsKey(unissuedBondInfo.Id))
                                                                DBProjectStatusName = lstStringDBUnissuedBondUpdateDataInfo[unissuedBondInfo.Id];

                                                            // 特殊判断假如在缓存中存在了 则不添加，如果不存在则新增
                                                            if (lstDicDBUnissuedBondUpdateDataInfo.Contains(unissuedBondInfo.Id + "_" + unissuedBondInfo.ProjectStatus))
                                                            {
                                                                unissuedBondInfo.ProjectStatusDetail = DBProjectStatusName;
                                                            }
                                                            else
                                                            {
                                                                unissuedBondInfo.ProjectStatusDetail = DBProjectStatusName +
                                                                   string.Format("<span style='color: red'>{0} {1}</span>", unissuedBondInfo.Data1.Replace("-", "").ToInt32(), unissuedBondInfo.ProjectStatus);
                                                            }

                                                            if (!lstDicDBUnissuedBondUpdateDataInfo.ContainsKey(unissuedBondInfo.Id + "_" + unissuedBondInfo.ProjectStatus))
                                                            {
                                                                UnissuedBondUpdateDataInfo unissuedBondUpdateDataInfo = new UnissuedBondUpdateDataInfo();
                                                                unissuedBondUpdateDataInfo.Id = unissuedBondInfo.Id;
                                                                unissuedBondUpdateDataInfo.DisplayName = unissuedBondInfo.ProjectStatus;
                                                                unissuedBondUpdateDataInfo.Date = unissuedBondInfo.Data1.Replace("-", "").ToInt32();
                                                                BLLFactory<UnissuedBondUpdateData>.Instance.Insert(unissuedBondUpdateDataInfo);

                                                                lstDicDBUnissuedBondUpdateDataInfo.Add(unissuedBondInfo.Id + "_" + unissuedBondInfo.ProjectStatus, unissuedBondUpdateDataInfo);
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("新增内容出错：{0}", ex.Message), typeof(InterfaceController));
                                                        }

                                                        unissuedBondInfo.From = "中国债券信息网";

                                                        unissuedBondInfo.Remark = string.Format("更新时间为:{0}", matchesData[0].Groups["UpdateDate"].Value);

                                                        // 如果没有则数据库新增记录
                                                        if (!lstDicDBUnissuedBondInfo.ContainsKey(unissuedBondInfo.Id))
                                                        {
                                                            BLLFactory<UnissuedBond>.Instance.Insert(unissuedBondInfo);
                                                        }

                                                        if (unissuedBondinfo.Managers.Contains(name))
                                                        {
                                                            lstUnissuedBondInfo.Add(unissuedBondInfo.Id, unissuedBondInfo);
                                                        }
                                                    }

                                                    #endregion
                                                    /*}*/
                                                }
                                                #endregion
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("服务器返回错误：{0}, 错误内容: {1}", response.StatusCode, response.StatusDescription), typeof(InterfaceController));
                                }
                            }
                        }
                    }));

                    if (tasks.Count >= maxDegreeOfParallelism)
                    {
                        Task.WaitAny(tasks.ToArray());
                        tasks = tasks.Where(t => t.Status == TaskStatus.Running).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_ERR, string.Format("多线程出错啦：{0}", ex.Message), typeof(InterfaceController));
            }

            #endregion

        }

        public static string ReplaceHtmlTag(string html, int length = 0)
        {
            string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");

            if (length > 0 && strText.Length > length)
                return strText.Substring(0, length);

            return strText;
        }

        public Encoding GetEncoding(string strurl)
        {
            string urlToCrawl = strurl;
            //generate http request
            if (urlToCrawl != null && urlToCrawl != "")
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlToCrawl);
                //use GET method to get url's html
                req.Method = "GET";
                req.Accept = "*/*";
                req.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                req.ContentType = "text/xml";
                //use request to get response
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Encoding enc;
                try
                {
                    if (resp.CharacterSet != "ISO-8859-1")
                        enc = Encoding.GetEncoding(resp.CharacterSet);
                    else
                        enc = Encoding.UTF8;
                }
                catch
                {
                    // *** Invalid encoding passed
                    enc = Encoding.UTF8;
                }
                string sHTML = string.Empty;
                using (StreamReader read = new StreamReader(resp.GetResponseStream(), enc))
                {
                    sHTML = read.ReadToEnd();
                    Match charSetMatch = Regex.Match(sHTML, "charset=(?<code>[a-zA-Z0-9\\-]+)", RegexOptions.IgnoreCase);
                    string sChartSet = charSetMatch.Groups["code"].Value;
                    //if it's not utf-8,we should redecode the html.
                    if (!string.IsNullOrEmpty(sChartSet) && !sChartSet.Equals("utf-8", StringComparison.OrdinalIgnoreCase))
                    {
                        enc = Encoding.GetEncoding(sChartSet);
                    }
                }
                return enc;
            }
            return Encoding.Default;
        }
    }
}
