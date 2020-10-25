using Aspose.Cells;
using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Format;
using JCodes.Framework.Entity;
using JCodes.Framework.jCodesenum;
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
            string totaldetail = "https://ynzx.zgwyzxw.cn/my/AlreadyVote/650/663/%E9%92%B1%E5%A1%98%E4%B8%9C%E5%8D%97%E5%AE%B6%E5%9B%AD/statistics";
            string totalurl = "https://ynzx.zgwyzxw.cn/index.php/home/Votestatistics/getAllVote";
            string totalxiaoquName = "钱塘东南家园";
            HttpWebRequest totalrequest = (HttpWebRequest)HttpWebRequest.Create(totalurl);
            string totalcontent = "areas_id=650&vote_id=663";
            byte[] totalbs = Encoding.UTF8.GetBytes(totalcontent);
            totalrequest.Method = "POST";
            totalrequest.ContentType = "application/x-www-form-urlencoded";
            totalrequest.ContentLength = totalbs.Length;
            //提交请求数据
            Stream totalreqStream = totalrequest.GetRequestStream();
            totalreqStream.Write(totalbs, 0, totalbs.Length);
            totalreqStream.Close();
            Int32[] detailzhuangCount = new Int32[18];
            Int32[] zhuangHouseNum = new int[] { 104, 100, 108, 106, 106, 108, 104, 104, 107, 108, 82, 82, 105, 159, 216, 216, 157 };

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

                            DongnanhaiVotes dongnanhaiVotes = new DongnanhaiVotes();

                            JObject jo = (JObject)JsonConvert.DeserializeObject(html);
                            IntelVoteInfo intelvoteInfo = new IntelVoteInfo();
                            intelvoteInfo.xiaoquName = totalxiaoquName;
                            intelvoteInfo.intelVote = jo["data"]["intelVote"].Count() + dongnanhaiVotes.GetXianchangAndPhoneShu("flag in (2, 3)"); //jo["data"]["recoveryVote"].Count() + jo["data"]["phoneVote"].Count(); // 投票数
                            //intelvoteInfo.intelVote = jo["data"]["intelVote"].Count() + lst.Count; // 投票数
                            intelvoteInfo.houseNum = "住宅2072户<br />商铺140户";//jo["data"]["houseNum"].ToString();// 总户数
                            intelvoteInfo.percentage = (100.00 * (jo["data"]["intelVote"].Count() + /*jo["data"]["recoveryVote"].Count() + jo["data"]["phoneVote"].Count()*/ dongnanhaiVotes.GetXianchangAndPhoneShu("flag in (2, 3)")) / (2072 + 140)).ToString("0.000");//jo["data"]["percentage"].ToString("");// 百分比
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

            DongnanhaiVotes dongnanhaiVotes2 = new DongnanhaiVotes();
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
                                intelvoteInfo.intelVote = detailzhuangCount[i] + dongnanhaiVotes2.GetXianchangAndPhoneShu(string.Format("flag in (2, 3) and zhuang = '{0}'", (i + 1)));//jo["data"]["intelVote"].Count(); // 投票数
                                intelvoteInfo.houseNum = zhuangHouseNum[i].ToString();//jo["data"]["houseNum"].ToString();// 总户数
                                intelvoteInfo.percentage = (100.0 * (detailzhuangCount[i] + dongnanhaiVotes2.GetXianchangAndPhoneShu(string.Format("flag in (2, 3) and zhuang = '{0}'", (i + 1)))) / zhuangHouseNum[i]).ToString("0.00");//Convert.ToInt32(jo["data"]["houseNum"].ToString())).ToString("0.00");// 百分比
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
            shangpuintelVoteInfo.houseNum = "140";// 总户数
            shangpuintelVoteInfo.percentage = (100.0 * detailzhuangCount[17] / 140).ToString("0.00");;// 百分比
            shangpuintelVoteInfo.detailurl = string.Format("/Home/Index_v3_detail?louzhuang={0}", 18); ;

            intelVoteInfolst.Add(shangpuintelVoteInfo);

            //intelVoteInfolst = (from e in intelVoteInfolst orderby e.percentage descending select e).ToList<IntelVoteInfo>();

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
            string totalcontent = "areas_id=650&vote_id=663";
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
            string totalcontent = "areas_id=650&vote_id=663";
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
                                    //dongnanhaiVotes.UpdateFlag(intelvotedetailInfo.Data2, intelvotedetailInfo.Data1, intelvotedetailInfo.Data3, "钱塘东南家园", 1);
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

                                    //dongnanhaiVotes.UpdateFlag(intelvotedetailInfo.Data2, intelvotedetailInfo.Data1, intelvotedetailInfo.Data3, "钱塘东南家园", 2);
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

                                    //dongnanhaiVotes.UpdateFlag(intelvotedetailInfo.Data2, intelvotedetailInfo.Data1, intelvotedetailInfo.Data3, "钱塘东南家园", 3);
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

        public void LoadXlsData() {
            /*Workbook workbookSrc = null;

            string file = @"F:\日常生活\业委会\20190907_物业交接资料\业主信息.xlsx";
            // 特殊处理 如果遇到打不开的文件则此文件被破坏需要跳过
            try
            {
                // 读取xls数据 判断是否是合法的xls表格
                workbookSrc = new Workbook(file);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, string.Format("{0}文件被破坏打不开", file), typeof(FrmXlsDataDeal));
            }

            #region 加载第一页
            Worksheet sheetSrc = workbookSrc.Worksheets[0];
            Cells cells = sheetSrc.Cells;

            Int32 productNameIndex = string.IsNullOrEmpty(config.AppConfigGet("SrcProductNameIndex")) ? 0 : ConvertHelper.ToInt32(config.AppConfigGet("SrcProductNameIndex"), 0);
            string productName = config.AppConfigGet("SrcProductName");
            Int32 tmpproductNameIndex = 0;
            for (Int32 i = 0; i < 10; i++)
            {
                if (string.Equals(cells[tmpproductNameIndex + i, productNameIndex].DisplayStringValue, productName))
                {
                    tmpproductNameIndex = tmpproductNameIndex + i;
                    break;
                }
            }

            Int32 productUnitIndex = string.IsNullOrEmpty(config.AppConfigGet("SrcProductUnitIndex")) ? 0 : ConvertHelper.ToInt32(config.AppConfigGet("SrcProductUnitIndex"), 0);
            string productUnit = config.AppConfigGet("SrcProductUnit");
            Int32 tmpproductUnitIndex = 0;
            for (Int32 i = 0; i < 10; i++)
            {
                if (string.Equals(cells[tmpproductUnitIndex + i, productUnitIndex].DisplayStringValue, productUnit))
                {
                    tmpproductUnitIndex = tmpproductUnitIndex + i;
                    break;
                }
            }

            if (tmpproductUnitIndex == tmpproductNameIndex)
            {
                normalRow = tmpproductNameIndex;
            }
            else
            {
                MessageDxUtil.ShowError("参考XLS文件 格式有错误请检查");
                return;
            }

            cacheProducts.Clear();
            normalRow++;
            // 两个都不为空则为有效数据，如果一个为空或者2个都为空则到结尾了
            while (!string.IsNullOrEmpty(cells[normalRow, productNameIndex].DisplayStringValue) && !string.IsNullOrEmpty(cells[normalRow, productUnitIndex].DisplayStringValue))
            {
                cacheProducts.Add(cells[normalRow, productNameIndex].DisplayStringValue, cells[normalRow, productUnitIndex].DisplayStringValue);

                AddLog(LogLevel.LOG_LEVEL_INFO, string.Format("缓存加载 参考项识别内容: {0}配置为{1} {2}配置为{3}", productName, cells[normalRow, productNameIndex].DisplayStringValue, productUnit, cells[normalRow, productUnitIndex].DisplayStringValue));

                normalRow++;
            }
            #endregion*/
        }
    }
}
