using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Files;

namespace JCodes.Framework.OracleDAL
{
    public class OraclePortal
    {
        public static GlobalTablePre gc = new GlobalTablePre();
    }

    public class GlobalTablePre
    {
        // 权限对应的表前缀
        public string _securityTablePre = new AppConfig().AppConfigGet("SecurityTablePre");

        public string _50GoTablePre = new AppConfig().AppConfigGet("50GoTablePre");

        public string _basicTablePre = new AppConfig().AppConfigGet("BasicTablePre");

        public string _contactTablePre = new AppConfig().AppConfigGet("ContactTablePre");
    }
}
