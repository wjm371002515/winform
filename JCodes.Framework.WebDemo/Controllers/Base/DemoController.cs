using JCodes.Framework.Common.Format;
using System;
using System.Web.Mvc;

namespace JCodes.Framework.WebDemo.Controllers
{
    public class DemoController : BaseController
    {
        private const Int32 a1 = 100;

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 计算机人员技能摸底算法
        /// V = a1/(1 + r1) + a2/((1+r1)(1+r2)) + ... + an/((1+r1)(1+r2)...(1+rn))
        /// </summary>
        /// <param name="n">变量n的值</param>
        /// <param name="type">3种算法
        ///                    type=0: a1=a2=...=an=100 设定r值(r1...rn本次视为相同值) 设定递增(递减)额度(整数) 设定n值(大于5的整数) 后得出V值
        ///                    type=1: a1为固定值100 设定r值(r1...rn本次视为相同值) 设定递增(递减)额度(整数) 设定n值(大于5的整数) 后得出V值
        ///                    type=2: a1为固定值100 设定r值(r1...rn本次视为相同值) 设定递增(递减)比例 设定n值(大于5的整数) 后得出V值
        /// </param>
        /// <param name="r">r的值</param>
        /// <param name="increase">增长比例</param>
        /// <returns></returns>
        public ActionResult CalcDemo(Int32 n, Int32 type, Int32 r, Int32 increase)
        {
            float result = 0;
            ReplyClass replyClass = new ReplyClass();
            if (!CheckInputValue(n, type, r, increase))
            {
                replyClass.ErrCode = 10001;
                replyClass.ErrMsg = "参数错误";
                return Json(replyClass, JsonRequestBehavior.AllowGet);
            }

            // type=0: a1=a2=...=an=100 设定r值(r1...rn本次视为相同值) 设定递增(递减)额度(整数) 设定n值(大于5的整数) 后得出V值
            // type=1: a1为固定值100 设定r值(r1...rn本次视为相同值) 设定递增(递减)额度(整数) 设定n值(大于5的整数) 后得出V值 
            if (0 == type || 1 == type)
            {
                if (0 == type) increase = 0;
                float tmpa1 = a1;
                float tmpr = 1;
                for (Int32 i = 0; i < n; i++)
                {
                    tmpa1 = a1 + increase * i;
                    tmpr = tmpr * (1 + (float)r / 100);
                    result += tmpa1 / tmpr;
                }
                replyClass.ErrCode = 0;
                replyClass.ErrMsg = result.ToString("f2");
            }
            else if (type == 2)
            {
                // type=2: a1为固定值100 设定r值(r1...rn本次视为相同值) 设定递增(递减)比例 设定n值(大于5的整数) 后得出V值
                float tmpa1 = a1;
                float tmpr = 1;
                for (Int32 i = 0; i < n; i++)
                {
                    if (i > 0)
                        tmpa1 = tmpa1 * (1 + (float)increase/100);
                    tmpr = tmpr * (1 + (float)r / 100);
                    result += tmpa1 / tmpr;
                }
                replyClass.ErrCode = 0;
                replyClass.ErrMsg = result.ToString("f2");
            }
            else
            {
                replyClass.ErrCode = 10001;
                replyClass.ErrMsg = "参数错误";
            }
            
            return Json(replyClass, JsonRequestBehavior.AllowGet);

        }

        private bool CheckInputValue(Int32 n, Int32 type, Int32 r, Int32 increase)
        {
            if (n <= 5)
            {
                return false;
            }

            return true;
        }
    }

    public class ReplyClass
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public Int32 ErrCode;
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public String ErrMsg ;
    }
}
