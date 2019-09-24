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
    public class InterfaceController : Controller
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

        /// <summary>
        /// 异步获取小区投票信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAjaxDongnanhai() 
        {
            List<IntelVoteInfo> intelVoteInfolst = new List<IntelVoteInfo>();
            for (Int32 i = 0; i < 17; i++)
            {
                Int32 firstNum = 666;
                Int32 secondNum = 262;
                Int32 louzhuang = 1;
                string urldetail = string.Format("https://ynzx.zgwyzxw.cn/my/AlreadyVote/{0}/{1}/钱塘东南家园{2}幢/statistics", firstNum + i, secondNum + i , louzhuang + i);
                string url = "https://ynzx.zgwyzxw.cn/index.php/home/Votestatistics/getAllVote";
                string xiaoquName = string.Format("钱塘东南家园{0}幢", louzhuang + i);

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                string content = "areas_id=" + (firstNum + i) + "&vote_id=" + (secondNum + i);
                byte[] bs = Encoding.UTF8.GetBytes(content);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bs.Length;
                //提交请求数据
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Close();
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
                                IntelVoteInfo intelvoteInfo = new IntelVoteInfo();
                                intelvoteInfo.xiaoquName = xiaoquName;
                                intelvoteInfo.intelVote = jo["data"]["intelVote"].Count() + jo["data"]["recoveryVote"].Count() + jo["data"]["phoneVote"].Count(); // 投票数
                                intelvoteInfo.houseNum = jo["data"]["houseNum"].ToString();// 总户数
                                intelvoteInfo.percentage = jo["data"]["percentage"].ToString();// 百分比
                                intelvoteInfo.detailurl = urldetail;

                                intelVoteInfolst.Add(intelvoteInfo);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("服务器返回错误：{0}", response.StatusCode);
                    }
                }
            }
            return Json(new { total = intelVoteInfolst.Count, rows = intelVoteInfolst }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 异步获取小区投票信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAjaxDongnanhai2()
        {
            List<IntelVoteInfo> intelVoteInfolst = new List<IntelVoteInfo>();

            // 新增统计全部功能https://ynzx.zgwyzxw.cn/my/AlreadyVote/650/299/%E9%92%B1%E5%A1%98%E4%B8%9C%E5%8D%97%E5%AE%B6%E5%9B%AD/statistics
            string totaldetail = "https://ynzx.zgwyzxw.cn/my/AlreadyVote/650/299/%E9%92%B1%E5%A1%98%E4%B8%9C%E5%8D%97%E5%AE%B6%E5%9B%AD/statistics";
            string totalurl = "https://ynzx.zgwyzxw.cn/index.php/home/Votestatistics/getAllVote";
            string totalxiaoquName = "钱塘东南家园";
            HttpWebRequest totalrequest = (HttpWebRequest)HttpWebRequest.Create(totalurl);
            string totalcontent = "areas_id=650&vote_id=299";
            byte[] totalbs = Encoding.UTF8.GetBytes(totalcontent);
            totalrequest.Method = "POST";
            totalrequest.ContentType = "application/x-www-form-urlencoded";
            totalrequest.ContentLength = totalbs.Length;
            //提交请求数据
            Stream totalreqStream = totalrequest.GetRequestStream();
            totalreqStream.Write(totalbs, 0, totalbs.Length);
            totalreqStream.Close();
            Int32[] detailzhuangCount = new Int32[18];
            // 20190816 wujianming 新增商铺信息
            IntelVoteInfo shangpuintelVoteInfo = new IntelVoteInfo();

            using (HttpWebResponse response = (HttpWebResponse)totalrequest.GetResponse())
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
                            IntelVoteInfo intelvoteInfo = new IntelVoteInfo();
                            intelvoteInfo.xiaoquName = totalxiaoquName;
                            intelvoteInfo.intelVote = jo["data"]["intelVote"].Count() + jo["data"]["recoveryVote"].Count() + jo["data"]["phoneVote"].Count(); // 投票数
                            intelvoteInfo.houseNum = jo["data"]["houseNum"].ToString();// 总户数
                            intelvoteInfo.percentage = jo["data"]["percentage"].ToString();// 百分比
                            intelvoteInfo.detailurl = totaldetail;
                            //Int32 tmpzhuang = 1;

                            for (Int32 i = 0; i < jo["data"]["intelVote"].Count(); i++)
                            {
                                if (jo["data"]["intelVote"][i]["yuan"].ToString().Contains( "钱塘东南家园"))
                                {
                                    Int32 tmpzhuang = Convert.ToInt32(jo["data"]["intelVote"][i]["zhuang"].ToString());
                                    /*if (Convert.ToInt32(jo["data"]["intelVote"][i]["zhuang"].ToString()) != tmpzhuang)
                                    {
                                        //detailzhuangCount[tmpzhuang - 1] = detailzhuangCount[tmpzhuang - 1] + 1;
                                        tmpzhuang = Convert.ToInt32(jo["data"]["intelVote"][i]["zhuang"].ToString());
                                    }*/
                                    detailzhuangCount[tmpzhuang - 1] = detailzhuangCount[tmpzhuang - 1] + 1;
                                }
                                if (jo["data"]["intelVote"][i]["yuan"].ToString() == "商铺")
                                {
                                    detailzhuangCount[17] = detailzhuangCount[17] + 1;
                                }
                            }

                            for (Int32 i = 0; i < jo["data"]["recoveryVote"].Count(); i++)
                            {
                                if (jo["data"]["recoveryVote"][i]["yuan"].ToString().Contains("钱塘东南家园"))
                                {
                                    Int32 tmpzhuang = Convert.ToInt32(jo["data"]["recoveryVote"][i]["zhuang"].ToString());
                                   
                                    detailzhuangCount[tmpzhuang - 1] = detailzhuangCount[tmpzhuang - 1] + 1;
                                }
                                if (jo["data"]["recoveryVote"][i]["yuan"].ToString() == "商铺")
                                {
                                    detailzhuangCount[17] = detailzhuangCount[17] + 1;
                                }
                            }

                            for (Int32 i = 0; i < jo["data"]["phoneVote"].Count(); i++)
                            {
                                if (jo["data"]["phoneVote"][i]["yuan"].ToString().Contains("钱塘东南家园"))
                                {
                                    Int32 tmpzhuang = Convert.ToInt32(jo["data"]["phoneVote"][i]["zhuang"].ToString());

                                    detailzhuangCount[tmpzhuang - 1] = detailzhuangCount[tmpzhuang - 1] + 1;
                                }
                                if (jo["data"]["phoneVote"][i]["yuan"].ToString() == "商铺")
                                {
                                    detailzhuangCount[17] = detailzhuangCount[17] + 1;
                                }
                            }

                            intelVoteInfolst.Add(intelvoteInfo);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("服务器返回错误：{0}", response.StatusCode);
                }
            }

            for (Int32 i = 0; i < 17; i++)
            {
                Int32 firstNum = 666;
                Int32 secondNum = 262;
                Int32 louzhuang = 1;
                string urldetail = string.Format("/Home/Index_v3_detail?louzhuang={0}", louzhuang + i);
                string url = "https://ynzx.zgwyzxw.cn/index.php/home/Votestatistics/getAllVote";
                string xiaoquName = string.Format("钱塘东南家园{0}幢", louzhuang + i);

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                string content = "areas_id=" + (firstNum + i) + "&vote_id=" + (secondNum + i);
                byte[] bs = Encoding.UTF8.GetBytes(content);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bs.Length;
                //提交请求数据
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Close();
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
                                IntelVoteInfo intelvoteInfo = new IntelVoteInfo();
                                intelvoteInfo.xiaoquName = xiaoquName;
                                intelvoteInfo.intelVote = detailzhuangCount[i];//jo["data"]["intelVote"].Count(); // 投票数
                                intelvoteInfo.houseNum = jo["data"]["houseNum"].ToString();// 总户数
                                intelvoteInfo.percentage = (100.0*detailzhuangCount[i] / Convert.ToInt32(jo["data"]["houseNum"].ToString())).ToString("0.00");// 百分比
                                intelvoteInfo.detailurl = urldetail;
                                intelvoteInfo.Data1 = string.Format("/Home/Index_Statistics?louzhuang={0}", louzhuang + i);

                                intelVoteInfolst.Add(intelvoteInfo);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("服务器返回错误：{0}", response.StatusCode);
                    }
                }
            }

            // 添加商铺信息
            shangpuintelVoteInfo.xiaoquName = "钱塘东南家园商铺";
            shangpuintelVoteInfo.intelVote = detailzhuangCount[17];//jo["data"]["intelVote"].Count(); // 投票数
            shangpuintelVoteInfo.houseNum = "145";// 总户数
            shangpuintelVoteInfo.percentage = (100.0 * detailzhuangCount[17] / 145).ToString("0.00");;// 百分比
            shangpuintelVoteInfo.detailurl = string.Format("/Home/Index_v3_detail?louzhuang={0}", 18); ;

            intelVoteInfolst.Add(shangpuintelVoteInfo);

            intelVoteInfolst = (from e in intelVoteInfolst orderby e.percentage descending select e).ToList<IntelVoteInfo>();

            return Json(new { total = intelVoteInfolst.Count, rows = intelVoteInfolst }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 详细信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAjaxDongnanhai2_detail(Int32 louzhuang)
        {
            List<IntelVoteDetailInfo> intelVoteDetailInfolst = new List<IntelVoteDetailInfo>();
            // 新增统计全部功能https://ynzx.zgwyzxw.cn/my/AlreadyVote/650/299/%E9%92%B1%E5%A1%98%E4%B8%9C%E5%8D%97%E5%AE%B6%E5%9B%AD/statistics
            //string totaldetail = "https://ynzx.zgwyzxw.cn/my/AlreadyVote/650/299/%E9%92%B1%E5%A1%98%E4%B8%9C%E5%8D%97%E5%AE%B6%E5%9B%AD/statistics";
            string totalurl = "https://ynzx.zgwyzxw.cn/index.php/home/Votestatistics/getAllVote";
            //string totalxiaoquName = "钱塘东南家园";
            HttpWebRequest totalrequest = (HttpWebRequest)HttpWebRequest.Create(totalurl);
            string totalcontent = "areas_id=650&vote_id=299";
            byte[] totalbs = Encoding.UTF8.GetBytes(totalcontent);
            totalrequest.Method = "POST";
            totalrequest.ContentType = "application/x-www-form-urlencoded";
            totalrequest.ContentLength = totalbs.Length;
            //提交请求数据
            Stream totalreqStream = totalrequest.GetRequestStream();
            totalreqStream.Write(totalbs, 0, totalbs.Length);
            totalreqStream.Close();
            Int32[] detailzhuangCount = new Int32[17];
            using (HttpWebResponse response = (HttpWebResponse)totalrequest.GetResponse())
            {
                // 请求成功的状态码：200
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string html = reader.ReadToEnd();
                            /*
                            intelvoteInfo.xiaoquName = totalxiaoquName;
                            intelvoteInfo.intelVote = jo["data"]["intelVote"].Count(); // 投票数
                            intelvoteInfo.houseNum = jo["data"]["houseNum"].ToString();// 总户数
                            intelvoteInfo.percentage = jo["data"]["percentage"].ToString();// 百分比
                            intelvoteInfo.detailurl = totalxiaoquName;
                            //Int32 tmpzhuang = 1;
                             * 
                             * */
                            JObject jo = (JObject)JsonConvert.DeserializeObject(html);
                            for (Int32 i = 0; i < jo["data"]["intelVote"].Count(); i++)
                            {
                                if (Convert.ToInt32(jo["data"]["intelVote"][i]["zhuang"].ToString()) == louzhuang && jo["data"]["intelVote"][i]["yuan"].ToString().Contains( "钱塘东南家园"))
                                {
                                    IntelVoteDetailInfo intelvotedetailInfo = new IntelVoteDetailInfo();
                                    // 钱塘东南家园1幢1单元0101室
                                    // fanghao: "1902", util: "2", zhuang: "17", yuan: "钱塘东南家园"
                                    StringBuilder sb = new StringBuilder();
                                    //sb.Append(jo["data"]["intelVote"][i]["yuan"].ToString());
                                    sb.Append("钱塘东南家园");
                                    sb.Append(louzhuang);
                                    sb.Append("幢");
                                    sb.Append(jo["data"]["intelVote"][i]["util"].ToString());
                                    sb.Append("单元");
                                    sb.Append(jo["data"]["intelVote"][i]["fanghao"].ToString());
                                    sb.Append("室");
                                    intelvotedetailInfo.houseNum = sb.ToString();
                                    intelvotedetailInfo.toupiao = "已投";
                                    intelVoteDetailInfolst.Add(intelvotedetailInfo);
                                }

                                if (18 == louzhuang && jo["data"]["intelVote"][i]["yuan"].ToString() == "商铺")
                                {
                                    IntelVoteDetailInfo intelvotedetailInfo = new IntelVoteDetailInfo();
                                    // 钱塘东南家园1幢1单元0101室
                                    // fanghao: "1902", util: "2", zhuang: "17", yuan: "钱塘东南家园"
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append(jo["data"]["intelVote"][i]["yuan"].ToString());
                                    sb.Append(jo["data"]["intelVote"][i]["zhuang"].ToString());
                                    sb.Append("幢");
                                    sb.Append(jo["data"]["intelVote"][i]["util"].ToString());
                                    sb.Append("单元");
                                    sb.Append(jo["data"]["intelVote"][i]["fanghao"].ToString());
                                    sb.Append("室");
                                    intelvotedetailInfo.houseNum = sb.ToString();
                                    intelvotedetailInfo.toupiao = "已投";
                                    intelVoteDetailInfolst.Add(intelvotedetailInfo);
                                } 
                            }

                            for (Int32 i = 0; i < jo["data"]["recoveryVote"].Count(); i++)
                            {
                                if (Convert.ToInt32(jo["data"]["recoveryVote"][i]["zhuang"].ToString()) == louzhuang && jo["data"]["recoveryVote"][i]["yuan"].ToString().Contains("钱塘东南家园"))
                                {
                                    IntelVoteDetailInfo intelvotedetailInfo = new IntelVoteDetailInfo();
                                    // 钱塘东南家园1幢1单元0101室
                                    // fanghao: "1902", util: "2", zhuang: "17", yuan: "钱塘东南家园"
                                    StringBuilder sb = new StringBuilder();
                                    //sb.Append(jo["data"]["intelVote"][i]["yuan"].ToString());
                                    sb.Append("钱塘东南家园");
                                    sb.Append(louzhuang);
                                    sb.Append("幢");
                                    sb.Append(jo["data"]["recoveryVote"][i]["util"].ToString());
                                    sb.Append("单元");
                                    sb.Append(jo["data"]["recoveryVote"][i]["fanghao"].ToString());
                                    sb.Append("室");
                                    intelvotedetailInfo.houseNum = sb.ToString();
                                    intelvotedetailInfo.toupiao = "现场已投";
                                    intelVoteDetailInfolst.Add(intelvotedetailInfo);
                                }

                                if (18 == louzhuang && jo["data"]["recoveryVote"][i]["yuan"].ToString() == "商铺")
                                {
                                    IntelVoteDetailInfo intelvotedetailInfo = new IntelVoteDetailInfo();
                                    // 钱塘东南家园1幢1单元0101室
                                    // fanghao: "1902", util: "2", zhuang: "17", yuan: "钱塘东南家园"
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append(jo["data"]["recoveryVote"][i]["yuan"].ToString());
                                    sb.Append(jo["data"]["recoveryVote"][i]["zhuang"].ToString());
                                    sb.Append("幢");
                                    sb.Append(jo["data"]["recoveryVote"][i]["util"].ToString());
                                    sb.Append("单元");
                                    sb.Append(jo["data"]["recoveryVote"][i]["fanghao"].ToString());
                                    sb.Append("室");
                                    intelvotedetailInfo.houseNum = sb.ToString();
                                    intelvotedetailInfo.toupiao = "现场已投";
                                    intelVoteDetailInfolst.Add(intelvotedetailInfo);
                                }
                            }

                            for (Int32 i = 0; i < jo["data"]["phoneVote"].Count(); i++)
                            {
                                if (Convert.ToInt32(jo["data"]["phoneVote"][i]["zhuang"].ToString()) == louzhuang && jo["data"]["phoneVote"][i]["yuan"].ToString().Contains("钱塘东南家园"))
                                {
                                    IntelVoteDetailInfo intelvotedetailInfo = new IntelVoteDetailInfo();
                                    // 钱塘东南家园1幢1单元0101室
                                    // fanghao: "1902", util: "2", zhuang: "17", yuan: "钱塘东南家园"
                                    StringBuilder sb = new StringBuilder();
                                    //sb.Append(jo["data"]["intelVote"][i]["yuan"].ToString());
                                    sb.Append("钱塘东南家园");
                                    sb.Append(louzhuang);
                                    sb.Append("幢");
                                    sb.Append(jo["data"]["phoneVote"][i]["util"].ToString());
                                    sb.Append("单元");
                                    sb.Append(jo["data"]["phoneVote"][i]["fanghao"].ToString());
                                    sb.Append("室");
                                    intelvotedetailInfo.houseNum = sb.ToString();
                                    intelvotedetailInfo.toupiao = "电话已投";
                                    intelVoteDetailInfolst.Add(intelvotedetailInfo);
                                }

                                if (18 == louzhuang && jo["data"]["phoneVote"][i]["yuan"].ToString() == "商铺")
                                {
                                    IntelVoteDetailInfo intelvotedetailInfo = new IntelVoteDetailInfo();
                                    // 钱塘东南家园1幢1单元0101室
                                    // fanghao: "1902", util: "2", zhuang: "17", yuan: "钱塘东南家园"
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append(jo["data"]["phoneVote"][i]["yuan"].ToString());
                                    sb.Append(jo["data"]["phoneVote"][i]["zhuang"].ToString());
                                    sb.Append("幢");
                                    sb.Append(jo["data"]["phoneVote"][i]["util"].ToString());
                                    sb.Append("单元");
                                    sb.Append(jo["data"]["phoneVote"][i]["fanghao"].ToString());
                                    sb.Append("室");
                                    intelvotedetailInfo.houseNum = sb.ToString();
                                    intelvotedetailInfo.toupiao = "电话已投";
                                    intelVoteDetailInfolst.Add(intelvotedetailInfo);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("服务器返回错误：{0}", response.StatusCode);
                }
            }

            intelVoteDetailInfolst = (from e in intelVoteDetailInfolst orderby e.houseNum select e).ToList<IntelVoteDetailInfo>();

            return Json(new { total = intelVoteDetailInfolst.Count, rows = intelVoteDetailInfolst }, JsonRequestBehavior.AllowGet);
        }

            /// <summary>
        /// 详细信息标题
        /// </summary>
        /// <returns></returns>
        public string GetAjaxDongnanhai2_Statistics_title(Int32 louzhuang)
        {
            DongnanhaiVotes dongnanhaiVotes = new DongnanhaiVotes();
            Int32 maxCengHushu = dongnanhaiVotes.MaxCengHuShu(louzhuang.ToString());
            string[] parm = new string[] { "单元楼层", "房号", "投票情况" };
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");
            for (int i = 0; i < maxCengHushu; i++)
            {
                for (int j = 0; j < parm.Length; j++)
                {
                    sb.Append("\"");
                    sb.Append(parm[j]);
                    sb.Append("\",");
                }
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(" ]");
            return sb.ToString();
        }

         /// <summary>
        /// 详细信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAjaxDongnanhai2_Statistics(Int32 louzhuang)
        {
            /*
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0101',  '1',  '8',  '钱塘东南家园', '1', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0102',  '1',  '8',  '钱塘东南家园', '1', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0103',  '1',  '8',  '钱塘东南家园', '1', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0104',  '1',  '8',  '钱塘东南家园', '1', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0201',  '1',  '8',  '钱塘东南家园', '2', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0202',  '1',  '8',  '钱塘东南家园', '2', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0203',  '1',  '8',  '钱塘东南家园', '2', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0204',  '1',  '8',  '钱塘东南家园', '2', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0301',  '1',  '8',  '钱塘东南家园', '3', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0302',  '1',  '8',  '钱塘东南家园', '3', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0303',  '1',  '8',  '钱塘东南家园', '3', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0304',  '1',  '8',  '钱塘东南家园', '3', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0401',  '1',  '8',  '钱塘东南家园', '4', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0402',  '1',  '8',  '钱塘东南家园', '4', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0403',  '1',  '8',  '钱塘东南家园', '4', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0404',  '1',  '8',  '钱塘东南家园', '4', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0501',  '1',  '8',  '钱塘东南家园', '5', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0502',  '1',  '8',  '钱塘东南家园', '5', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0503',  '1',  '8',  '钱塘东南家园', '5', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0504',  '1',  '8',  '钱塘东南家园', '5', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0601',  '1',  '8',  '钱塘东南家园', '6', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0602',  '1',  '8',  '钱塘东南家园', '6', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0603',  '1',  '8',  '钱塘东南家园', '6', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0604',  '1',  '8',  '钱塘东南家园', '6', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0701',  '1',  '8',  '钱塘东南家园', '7', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0702',  '1',  '8',  '钱塘东南家园', '7', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0703',  '1',  '8',  '钱塘东南家园', '7', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0704',  '1',  '8',  '钱塘东南家园', '7', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0801',  '1',  '8',  '钱塘东南家园', '8', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0802',  '1',  '8',  '钱塘东南家园', '8', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0803',  '1',  '8',  '钱塘东南家园', '8', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0804',  '1',  '8',  '钱塘东南家园', '8', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0901',  '1',  '8',  '钱塘东南家园', '9', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0902',  '1',  '8',  '钱塘东南家园', '9', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0903',  '1',  '8',  '钱塘东南家园', '9', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('0904',  '1',  '8',  '钱塘东南家园', '9', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1001',  '1',  '8',  '钱塘东南家园', '10', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1002',  '1',  '8',  '钱塘东南家园', '10', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1003',  '1',  '8',  '钱塘东南家园', '10', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1004',  '1',  '8',  '钱塘东南家园', '10', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1101',  '1',  '8',  '钱塘东南家园', '11', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1102',  '1',  '8',  '钱塘东南家园', '11', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1103',  '1',  '8',  '钱塘东南家园', '11', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1104',  '1',  '8',  '钱塘东南家园', '11', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1201',  '1',  '8',  '钱塘东南家园', '12', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1202',  '1',  '8',  '钱塘东南家园', '12', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1203',  '1',  '8',  '钱塘东南家园', '12', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1204',  '1',  '8',  '钱塘东南家园', '12', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1301',  '1',  '8',  '钱塘东南家园', '13', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1302',  '1',  '8',  '钱塘东南家园', '13', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1303',  '1',  '8',  '钱塘东南家园', '13', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1304',  '1',  '8',  '钱塘东南家园', '13', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1401',  '1',  '8',  '钱塘东南家园', '14', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1402',  '1',  '8',  '钱塘东南家园', '14', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1403',  '1',  '8',  '钱塘东南家园', '14', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1404',  '1',  '8',  '钱塘东南家园', '14', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1501',  '1',  '8',  '钱塘东南家园', '15', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1502',  '1',  '8',  '钱塘东南家园', '15', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1503',  '1',  '8',  '钱塘东南家园', '15', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1504',  '1',  '8',  '钱塘东南家园', '15', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1601',  '1',  '8',  '钱塘东南家园', '16', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1602',  '1',  '8',  '钱塘东南家园', '16', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1603',  '1',  '8',  '钱塘东南家园', '16', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1604',  '1',  '8',  '钱塘东南家园', '16', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1701',  '1',  '8',  '钱塘东南家园', '17', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1702',  '1',  '8',  '钱塘东南家园', '17', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1703',  '1',  '8',  '钱塘东南家园', '17', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1704',  '1',  '8',  '钱塘东南家园', '17', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1801',  '1',  '8',  '钱塘东南家园', '18', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1802',  '1',  '8',  '钱塘东南家园', '18', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1803',  '1',  '8',  '钱塘东南家园', '18', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1804',  '1',  '8',  '钱塘东南家园', '18', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1901',  '1',  '8',  '钱塘东南家园', '19', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1902',  '1',  '8',  '钱塘东南家园', '19', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1903',  '1',  '8',  '钱塘东南家园', '19', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('1904',  '1',  '8',  '钱塘东南家园', '19', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2001',  '1',  '8',  '钱塘东南家园', '20', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2002',  '1',  '8',  '钱塘东南家园', '20', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2003',  '1',  '8',  '钱塘东南家园', '20', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2004',  '1',  '8',  '钱塘东南家园', '20', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2101',  '1',  '8',  '钱塘东南家园', '21', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2102',  '1',  '8',  '钱塘东南家园', '21', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2103',  '1',  '8',  '钱塘东南家园', '21', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2104',  '1',  '8',  '钱塘东南家园', '21', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2201',  '1',  '8',  '钱塘东南家园', '22', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2202',  '1',  '8',  '钱塘东南家园', '22', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2203',  '1',  '8',  '钱塘东南家园', '22', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2204',  '1',  '8',  '钱塘东南家园', '22', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2301',  '1',  '8',  '钱塘东南家园', '23', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2302',  '1',  '8',  '钱塘东南家园', '23', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2303',  '1',  '8',  '钱塘东南家园', '23', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2304',  '1',  '8',  '钱塘东南家园', '23', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2401',  '1',  '8',  '钱塘东南家园', '24', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2402',  '1',  '8',  '钱塘东南家园', '24', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2403',  '1',  '8',  '钱塘东南家园', '24', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2404',  '1',  '8',  '钱塘东南家园', '24', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2501',  '1',  '8',  '钱塘东南家园', '25', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2502',  '1',  '8',  '钱塘东南家园', '25', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2503',  '1',  '8',  '钱塘东南家园', '25', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2504',  '1',  '8',  '钱塘东南家园', '25', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2601',  '1',  '8',  '钱塘东南家园', '26', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2602',  '1',  '8',  '钱塘东南家园', '26', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2603',  '1',  '8',  '钱塘东南家园', '26', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2604',  '1',  '8',  '钱塘东南家园', '26', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2701',  '1',  '8',  '钱塘东南家园', '27', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2702',  '1',  '8',  '钱塘东南家园', '27', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2703',  '1',  '8',  '钱塘东南家园', '27', '0');
            INSERT INTO  `sq_codeany`.`onethink_dongnanhaiVotes` (`fanghao` ,`util` ,`zhuang` ,`yuan` , `ceng` , `flag`) VALUES ('2704',  '1',  '8',  '钱塘东南家园', '27', '0');
            */
            DongnanhaiVotes dongnanhaiVotes = new DongnanhaiVotes();
            List<DongnanhaiVotesInfo> lst =  dongnanhaiVotes.GetVotesBylouzhuang(louzhuang.ToString());

            // start 网上查询数据
             List<IntelVoteDetailInfo> intelVoteDetailInfolst = new List<IntelVoteDetailInfo>();
            // 新增统计全部功能https://ynzx.zgwyzxw.cn/my/AlreadyVote/650/299/%E9%92%B1%E5%A1%98%E4%B8%9C%E5%8D%97%E5%AE%B6%E5%9B%AD/statistics
            //string totaldetail = "https://ynzx.zgwyzxw.cn/my/AlreadyVote/650/299/%E9%92%B1%E5%A1%98%E4%B8%9C%E5%8D%97%E5%AE%B6%E5%9B%AD/statistics";
            string totalurl = "https://ynzx.zgwyzxw.cn/index.php/home/Votestatistics/getAllVote";
            //string totalxiaoquName = "钱塘东南家园";
            HttpWebRequest totalrequest = (HttpWebRequest)HttpWebRequest.Create(totalurl);
            string totalcontent = "areas_id=650&vote_id=299";
            byte[] totalbs = Encoding.UTF8.GetBytes(totalcontent);
            totalrequest.Method = "POST";
            totalrequest.ContentType = "application/x-www-form-urlencoded";
            totalrequest.ContentLength = totalbs.Length;
            //提交请求数据
            Stream totalreqStream = totalrequest.GetRequestStream();
            totalreqStream.Write(totalbs, 0, totalbs.Length);
            totalreqStream.Close();
            Int32[] detailzhuangCount = new Int32[17];
            using (HttpWebResponse response = (HttpWebResponse)totalrequest.GetResponse())
            {
                // 请求成功的状态码：200
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string html = reader.ReadToEnd();
                            /*
                            intelvoteInfo.xiaoquName = totalxiaoquName;
                            intelvoteInfo.intelVote = jo["data"]["intelVote"].Count(); // 投票数
                            intelvoteInfo.houseNum = jo["data"]["houseNum"].ToString();// 总户数
                            intelvoteInfo.percentage = jo["data"]["percentage"].ToString();// 百分比
                            intelvoteInfo.detailurl = totalxiaoquName;
                            //Int32 tmpzhuang = 1;
                             * 
                             * */
                            JObject jo = (JObject)JsonConvert.DeserializeObject(html);
                            for (Int32 i = 0; i < jo["data"]["intelVote"].Count(); i++)
                            {
                                if (Convert.ToInt32(jo["data"]["intelVote"][i]["zhuang"].ToString()) == louzhuang && jo["data"]["intelVote"][i]["yuan"].ToString().Contains("钱塘东南家园"))
                                {
                                    IntelVoteDetailInfo intelvotedetailInfo = new IntelVoteDetailInfo();
                                    // 钱塘东南家园1幢1单元0101室
                                    // fanghao: "1902", util: "2", zhuang: "17", yuan: "钱塘东南家园"
                                    StringBuilder sb = new StringBuilder();
                                    //sb.Append(jo["data"]["intelVote"][i]["yuan"].ToString());
                                    sb.Append("钱塘东南家园");
                                    sb.Append(louzhuang);
                                    sb.Append("幢");
                                    sb.Append(jo["data"]["intelVote"][i]["util"].ToString());
                                    sb.Append("单元");
                                    sb.Append(jo["data"]["intelVote"][i]["fanghao"].ToString());
                                    sb.Append("室");
                                    intelvotedetailInfo.houseNum = sb.ToString();
                                    intelvotedetailInfo.toupiao = "已投";
                                    intelvotedetailInfo.Data1 = jo["data"]["intelVote"][i]["util"].ToString();
                                    intelvotedetailInfo.Data2 = jo["data"]["intelVote"][i]["fanghao"].ToString();
                                    intelvotedetailInfo.Data3 = jo["data"]["intelVote"][i]["zhuang"].ToString();
                                    intelVoteDetailInfolst.Add(intelvotedetailInfo);
                                    // SELECT * from onethink_dongnanhaiVotes where fanghao='1004' and util='1' and zhuang='8' and yuan='钱塘东南家园'
                                    dongnanhaiVotes.UpdateFlag(intelvotedetailInfo.Data2, intelvotedetailInfo.Data1, intelvotedetailInfo.Data3, "钱塘东南家园", 1);
                                }
                            }

                            for (Int32 i = 0; i < jo["data"]["recoveryVote"].Count(); i++)
                            {
                                if (Convert.ToInt32(jo["data"]["recoveryVote"][i]["zhuang"].ToString()) == louzhuang && jo["data"]["recoveryVote"][i]["yuan"].ToString().Contains("钱塘东南家园"))
                                {
                                    IntelVoteDetailInfo intelvotedetailInfo = new IntelVoteDetailInfo();
                                    // 钱塘东南家园1幢1单元0101室
                                    // fanghao: "1902", util: "2", zhuang: "17", yuan: "钱塘东南家园"
                                    StringBuilder sb = new StringBuilder();
                                    //sb.Append(jo["data"]["intelVote"][i]["yuan"].ToString());
                                    sb.Append("钱塘东南家园");
                                    sb.Append(louzhuang);
                                    sb.Append("幢");
                                    sb.Append(jo["data"]["recoveryVote"][i]["util"].ToString());
                                    sb.Append("单元");
                                    sb.Append(jo["data"]["recoveryVote"][i]["fanghao"].ToString());
                                    sb.Append("室");
                                    intelvotedetailInfo.houseNum = sb.ToString();
                                    intelvotedetailInfo.toupiao = "现场已投";
                                    intelvotedetailInfo.Data1 = jo["data"]["recoveryVote"][i]["util"].ToString();
                                    intelvotedetailInfo.Data2 = jo["data"]["recoveryVote"][i]["fanghao"].ToString();
                                    intelvotedetailInfo.Data3 = jo["data"]["recoveryVote"][i]["zhuang"].ToString();
                                    intelVoteDetailInfolst.Add(intelvotedetailInfo);

                                    dongnanhaiVotes.UpdateFlag(intelvotedetailInfo.Data2, intelvotedetailInfo.Data1, intelvotedetailInfo.Data3, "钱塘东南家园", 2);
                                }
                            }

                            for (Int32 i = 0; i < jo["data"]["phoneVote"].Count(); i++)
                            {
                                if (Convert.ToInt32(jo["data"]["phoneVote"][i]["zhuang"].ToString()) == louzhuang && jo["data"]["phoneVote"][i]["yuan"].ToString().Contains("钱塘东南家园"))
                                {
                                    IntelVoteDetailInfo intelvotedetailInfo = new IntelVoteDetailInfo();
                                    // 钱塘东南家园1幢1单元0101室
                                    // fanghao: "1902", util: "2", zhuang: "17", yuan: "钱塘东南家园"
                                    StringBuilder sb = new StringBuilder();
                                    //sb.Append(jo["data"]["intelVote"][i]["yuan"].ToString());
                                    sb.Append("钱塘东南家园");
                                    sb.Append(louzhuang);
                                    sb.Append("幢");
                                    sb.Append(jo["data"]["phoneVote"][i]["util"].ToString());
                                    sb.Append("单元");
                                    sb.Append(jo["data"]["phoneVote"][i]["fanghao"].ToString());
                                    sb.Append("室");
                                    intelvotedetailInfo.houseNum = sb.ToString();
                                    intelvotedetailInfo.toupiao = "电话已投";
                                    intelvotedetailInfo.Data1 = jo["data"]["phoneVote"][i]["util"].ToString();
                                    intelvotedetailInfo.Data2 = jo["data"]["phoneVote"][i]["fanghao"].ToString();
                                    intelvotedetailInfo.Data3 = jo["data"]["phoneVote"][i]["zhuang"].ToString();
                                    intelVoteDetailInfolst.Add(intelvotedetailInfo);

                                    dongnanhaiVotes.UpdateFlag(intelvotedetailInfo.Data2, intelvotedetailInfo.Data1, intelvotedetailInfo.Data3, "钱塘东南家园", 3);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("服务器返回错误：{0}", response.StatusCode);
                }
            }


            // 查询数据
            foreach (DongnanhaiVotesInfo oneDongnanhaiVotesInfo in lst)
            {
                foreach (IntelVoteDetailInfo oneIntelVoteDetailInfo in intelVoteDetailInfolst)
                {
                    if (string.Equals(oneDongnanhaiVotesInfo.Zhuang, oneIntelVoteDetailInfo.Data3) && string.Equals(oneDongnanhaiVotesInfo.Util, oneIntelVoteDetailInfo.Data1) &&
                        string.Equals(oneDongnanhaiVotesInfo.Fanghao, oneIntelVoteDetailInfo.Data2))
                    {
                        if (string.Equals("已投", oneIntelVoteDetailInfo.toupiao))
                            oneDongnanhaiVotesInfo.Flag = 1;
                        if (string.Equals("现场已投", oneIntelVoteDetailInfo.toupiao))
                            oneDongnanhaiVotesInfo.Flag = 2;
                        if (string.Equals("电话已投", oneIntelVoteDetailInfo.toupiao))
                            oneDongnanhaiVotesInfo.Flag = 3;
                        break;
                    }
                }
            }

            Int32 maxCengHushu = dongnanhaiVotes.MaxCengHuShu(louzhuang.ToString());

            return Json(new { total = maxCengHushu, rows = lst }, JsonRequestBehavior.AllowGet);
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

            MachinesInfo machine = new JCodes.Framework.BLL.Machines().FindByID(Convert.ToInt32(Id));
            if (machine == null) return Json(new { errCode = -1, errMsg = "不存在的记录" }, JsonRequestBehavior.AllowGet);
            else return Json(new { errCode = 0, errMsg = "", data=machine }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDictData(String Id)
        {
            if (!ValidateUtil.IsNumber(Id)) return Json(new { errCode = -1, errMsg = "入参格式错误" }, JsonRequestBehavior.AllowGet);

            List<DictDataInfo> lst = new JCodes.Framework.BLL.DictData().FindByTypeID(Convert.ToInt32(Id));
            if (lst == null || lst.Count == 0) return Json(new { errCode = -1, errMsg = "不存在的记录" }, JsonRequestBehavior.AllowGet);
            else return Json(new { errCode = 0, errMsg = "", data = lst }, JsonRequestBehavior.AllowGet);
        }
    }
}
