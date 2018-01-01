using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Common.Proj
{
    interface ISqlGenerate
    {
        string printHeaderInfo(string sqlfile, string version, string author, string lastModDate, string notes, string generateDate, object objLst);

        string printInitInfo(string tableName);

        string initTableInfo(string tableEnglishName, string tableChineseName, Boolean existHistable, object objFields, object objIndexs, object dictFieldType);

        string getStrLength(Int32 num, string str);
    }
}
