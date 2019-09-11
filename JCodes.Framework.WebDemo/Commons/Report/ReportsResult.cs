using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCodes.Framework.WebDemo.Common
{
    public class ReportsResult : ActionResult
    {
        public byte[] Data { get; set; }
        public string MineType { get; set; }

        public ReportsResult(byte[] data, string mineType)
        {
            this.Data = data;
            this.MineType = mineType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (Data == null)
            {
                new EmptyResult().ExecuteResult(context);
                return;
            }
            context.HttpContext.Response.ContentType = MineType;

            using (MemoryStream ms = new MemoryStream(Data))
            {
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms))
                {
                    context.HttpContext.Response.Output.Write(sr.ReadToEnd());
                }
            }
        }
    }
}