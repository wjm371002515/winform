using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Common.Proj
{
    public class OracleGenerate : ISqlGenerate
    {
        private const string modrecordModel       = "-- V{0} {1} {2} {3} {4} {5} {6} {7}";
        private const string modrecordheaderModel = "-- 修改版本         修改日期            修改单      申请人     修改人     修改内容                                 修改原因                                 备注\r\n";
        private const string test                 = "-- V1.234.567.8900  2017-01-01 13:55:00 M1234567890 测试人加二 测试人加二 这里可以存在多个字的这里可以存在多个字的 这里可以存在多个字的这里可以存在多个字的 这里可以存在多个字的这里可以存在多个字的";


        public string printHeaderInfo(string sqlfile, string version, string author, string lastModDate, string notes, string generateDate, object objLst)
        {
            throw new NotImplementedException();
        }

        public string printInitInfo(string tableName)
        {
            throw new NotImplementedException();
        }

        public string getStrLength(int num, string str)
        {
            throw new NotImplementedException();
        }


        public string initTableInfo(string tableEnglishName, string tableChineseName, Boolean existHistable, object objFields, object objIndexs, object dictFieldType)
        {
            throw new NotImplementedException();
        }
    }
}
