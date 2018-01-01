using JCodes.Framework.Common.Format;
using JCodes.Framework.Common.Office;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace JCodes.Framework.Common
{
    public class ErrorInfo
    {
        private Dictionary<Int32, String> lst = new Dictionary<int, string>();

        public ErrorInfo()
        {
            lst.Add(10000, "登陆失败");
        }

        public Dictionary<Int32, String> GetErrorInfo()
        {
            return lst;
        }
    }
}
