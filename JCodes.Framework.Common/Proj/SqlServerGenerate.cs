using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace JCodes.Framework.Common.Proj
{
    public class SqlServerGenerate : ISqlGenerate
    {
        private const string modrecordModel       = "-- V{0} {1} {2} {3} {4} {5} {6} {7}";
        private const string modrecordheaderModel = "-- 修改版本         修改日期            修改单      申请人     修改人     修改内容                                 修改原因                                 备注\r\n";
        private const string test                 = "-- V1.234.567.8900  2017-01-01 13:55:00 M1234567890 测试人加二 测试人加二 这里可以存在多个字的这里可以存在多个字的 这里可以存在多个字的这里可以存在多个字的 这里可以存在多个字的这里可以存在多个字的";

        /* 创建表的SQL 唯一索引 
         CREATE TABLE Persons(
            Id_P int NOT NULL,
            LastName varchar(255) NOT NULL,
            FirstName varchar(255),
            Address varchar(255),
            City varchar(255),
            CONSTRAINT uc_PersonID UNIQUE (Id_P,LastName),  -- 创建唯一索引
            CONSTRAINT pk_PersonID PRIMARY KEY (Id_P)       -- 创建主键
            );
         create index ix_Persons on Persons (FirstName);    -- 创建索引
         */

        /// <summary>
        /// 生成SQL表头
        /// </summary>
        /// <param name="sqlfile"></param>
        /// <param name="version"></param>
        /// <param name="author"></param>
        /// <param name="lastModDate"></param>
        /// <param name="notes"></param>
        /// <param name="generateDate"></param>
        /// <param name="modLst"></param>
        /// <returns></returns>
        public string printHeaderInfo(string sqlfile, string version, string author, string lastModDate, string notes, string generateDate, object objLst)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("-- -------------------------------------------------------------\r\n");
            sb.Append(string.Format("-- SQLfile     : {0}\r\n", sqlfile));
            sb.Append(string.Format("-- Version     : {0}\r\n", version));
            sb.Append(string.Format("-- Author      : {0}\r\n", author));
            sb.Append(string.Format("-- LastModDate : {0}\r\n", lastModDate));
            sb.Append(string.Format("-- Notes       : {0}\r\n", notes));
            sb.Append(string.Format("-- GenerateDate: {0}\r\n", generateDate));
            sb.Append("-- -------------------------------------------------------------\r\n\r\n\r\n");
            sb.Append(modrecordheaderModel);

            var modLst = objLst as List<ModRecordInfo>;
         
            // 新增修改记录
            if (modLst != null)
            {
                foreach (var modrecord in modLst)
                {
                    sb.Append(string.Format(modrecordModel, modrecord.ModVersion.ToString().PadRight(15, ' '), modrecord.ModDate.ToString("yyyy-MM-dd HH:mm:ss").PadRight(19, ' '), modrecord.ModOrderId.PadRight(11, ' '), getStrLength(5, modrecord.Proposer), getStrLength(5, modrecord.Programmer), getStrLength(20, modrecord.ModContent), getStrLength(20, modrecord.ModReason), getStrLength(20, modrecord.Remark)));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 打印初始化信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string printInitInfo(string tableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("PRINT Init {0} Data\r\n", tableName));
            sb.Append(string.Format("truncate table {0}", tableName));
            return sb.ToString();
        }


        public string getStrLength(Int32 num, string str)
        {
            return str.PadRight(num * 2 - System.Text.Encoding.Default.GetBytes(str.ToCharArray()).Length + str.Length, ' ');
        }


        public string initTableInfo(string tableEnglishName,string tableChineseName, Boolean existHistable, object objFields, object objIndexs, object dictFieldType)
        {
            StringBuilder sbResult = new StringBuilder();
            sbResult.Append("-- -------------------------------------------------------------\r\n");
            sbResult.Append("-- 全量脚本\r\n");
            sbResult.Append(string.Format("-- 创建表 {0}({1})的当前表      : {0}\r\n", tableEnglishName, tableChineseName));
            sbResult.Append("-- -------------------------------------------------------------\r\n\r\n");
            sbResult.Append(modrecordheaderModel);

            sbResult.Append(string.Format("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'{0}') AND type in (N'U'))\r\n", tableEnglishName));
            sbResult.Append(string.Format("\tDROP TABLE {0};\r\n\r\n", tableEnglishName));

            sbResult.Append(string.Format("CREATE TABLE {0}(", tableEnglishName));

            StringBuilder sbFields = new StringBuilder();
            String IndexStr = string.Empty;
            Dictionary<string, string> dicts = dictFieldType as Dictionary<string, string>;

            // 添加create table字样

            List<TableFieldsInfo> fieldsLst = objFields as List<TableFieldsInfo>;
            // 新增修改记录
            if (fieldsLst != null)
            {
                foreach (TableFieldsInfo tablefieldInfo in fieldsLst)
                {
                    if (dicts.ContainsKey(tablefieldInfo.FieldType))
                        sbFields.Append(string.Format("\r\n\t[{0}] {1} {2},", tablefieldInfo.FieldName, dicts[tablefieldInfo.FieldType], tablefieldInfo.IsNull ? string.Empty : "NOT NULL"));
                    else
                        sbFields.Append(string.Format("\r\n\t[{0}] {1} {2},", tablefieldInfo.FieldName, "不存在的数据类型请检查", tablefieldInfo.IsNull ? string.Empty : "NOT NULL"));
                }
            }

            List<TableIndexsInfo> indexsLst = objIndexs as List<TableIndexsInfo>;
            // 新增修改记录
            if (indexsLst != null)
            {
                foreach (TableIndexsInfo tableindexInfo in indexsLst)
                {
                    if (string.Equals(tableindexInfo.ConstraintType, "主键"))
                    {
                        sbFields.Append(string.Format("\r\n\tCONSTRAINT {0} PRIMARY KEY ({1}) ,", tableindexInfo.IndexName, tableindexInfo.IndexFieldLst));
                    }
                    else if (string.Equals(tableindexInfo.ConstraintType, "唯一索引"))
                    {
                        sbFields.Append(string.Format("\r\n\tCONSTRAINT {0} UNIQUE ({1}) ,", tableindexInfo.IndexName, tableindexInfo.IndexFieldLst));
                    }
                    else if (string.Equals(tableindexInfo.ConstraintType, "索引"))
                    {
                        //    -- 创建索引
                        IndexStr += string.Format("\r\nCREATE INDEX {0} ON {1} ({2});", tableindexInfo.IndexName, tableEnglishName, tableindexInfo.IndexFieldLst);
                    }
                }
            }

            sbResult.Append(sbFields.ToString().TrimEnd(','));

            sbResult.Append("\r\n);\r\nGO\r\n");

            // 创建唯一索引
            sbResult.Append(IndexStr);

            if (existHistable)
            {
                sbResult.Append("历史表数据添加");
            }


            return sbResult.ToString();
        }

        public string initTableDataInfo(string tableEnglishName, string tableChineseName, object objTable)
        {
            StringBuilder sbResult = new StringBuilder();
            sbResult.Append("-- -------------------------------------------------------------\r\n");
            sbResult.Append("-- 基础数脚本\r\n");
            sbResult.Append(string.Format("-- 初始化 {0}({1})表基础数据      : {0}\r\n", tableEnglishName, tableChineseName));
            sbResult.Append("-- -------------------------------------------------------------\r\n\r\n");
            sbResult.Append(modrecordheaderModel);

            StringBuilder sbTableData = new StringBuilder();

            sbResult.Append(string.Format("TRUNCATE TABLE {0}\r\n\r\n", tableEnglishName));

            System.Data.DataTable dt = objTable as System.Data.DataTable;
            // 新增修改记录
            if (dt != null)
            {
                // 先取出字段
                StringBuilder sbFields = new StringBuilder();
                for (Int32 i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 0)
                        sbFields.Append(string.Format("insert into {0}([{1}]", tableEnglishName, dt.Columns[i]));
                    else if (i == dt.Columns.Count - 1)
                        sbFields.Append(string.Format(", [{0}])", dt.Columns[i]));
                    else
                        sbFields.Append(string.Format(", [{0}]", dt.Columns[i]));
                }

                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    System.Data.DataRow dr = dt.Rows[i];
                    StringBuilder sbRowData = new StringBuilder();
                    for (Int32 j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j == 0)
                            sbRowData.Append(string.Format(" values('{0}'", dr[j]));
                        else if (j == dt.Columns.Count - 1)
                            sbRowData.Append(string.Format(", '{0}'); \r\n", dr[j]));
                        else
                            sbRowData.Append(string.Format(", '{0}'", dr[j]));
                    }
                    sbResult.Append(sbFields.ToString()).Append(sbRowData.ToString());
                }
            }

            return sbResult.ToString();
        }

        public string initDictTypeInfo(string tableEnglishName, string tableChineseName, object objTable)
        {
            StringBuilder sbResult = new StringBuilder();
            sbResult.Append("-- -------------------------------------------------------------\r\n");
            sbResult.Append("-- 基础数脚本\r\n");
            sbResult.Append(string.Format("-- 初始化 {0}({1})表基础数据      : {0}\r\n", tableEnglishName, tableChineseName));
            sbResult.Append("-- -------------------------------------------------------------\r\n\r\n");
            sbResult.Append(modrecordheaderModel);

            sbResult.Append(string.Format("TRUNCATE TABLE {0}\r\n\r\n", tableEnglishName));

            List<DictInfo> dictLst = objTable as List<DictInfo>;
            // 新增修改记录
            if (dictLst != null)
            {
                foreach (var dict in dictLst)
                {
                    sbResult.Append(string.Format("INSERT [{0}] ([ID], [PID], [Name], [Remark], [Seq], [EditorId], [LastUpdateTime]) VALUES ({1}, {2}, N'{3}', N'{4}', N'{5}', '1', GETDATE())\r\n", tableEnglishName, dict.Id, dict.Pid, dict.Name, dict.Remark, dict.Id));
                }
            }

            return sbResult.ToString();
        }

        public string initDictDataInfo(string tableEnglishName, string tableChineseName, object objTable)
        {
            StringBuilder sbResult = new StringBuilder();
            sbResult.Append("-- -------------------------------------------------------------\r\n");
            sbResult.Append("-- 基础数脚本\r\n");
            sbResult.Append(string.Format("-- 初始化 {0}({1})表基础数据      : {0}\r\n", tableEnglishName, tableChineseName));
            sbResult.Append("-- -------------------------------------------------------------\r\n\r\n");
            sbResult.Append(modrecordheaderModel);

            sbResult.Append(string.Format("TRUNCATE TABLE {0}\r\n\r\n", tableEnglishName));

            List<DictDataInfo> dicdetailtLst = objTable as List<DictDataInfo>;
            // 新增修改记录
            if (dicdetailtLst != null)
            {
                foreach (var dict in dicdetailtLst)
                {
                    // 特殊处理，对于插入的文本中已经包含了单引号则需要用转义字符来处理
                    string remark = dict.Remark;
                    if (remark.Contains(Const.SingleQuotation))
                        remark = remark.Replace(Const.SingleQuotation, Const.DuobleSingleQuotation);
                    sbResult.Append(string.Format("INSERT [{0}] ([Gid], [DictTypeId], [Value], [Name], [Remark], [Seq], [EditorId], [LastUpdateTime]) VALUES (NEWID(), {1}, {2}, N'{3}', N'{4}', N'{5}', '1', GETDATE())\r\n", tableEnglishName, dict.DicttypeID, dict.Value, dict.Name, remark, dict.Seq));
                }
            }

            return sbResult.ToString();
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="tableEnglishName"></param>
        /// <param name="tableChineseName"></param>
        /// <param name="objTable"></param>
        /// <returns></returns>
        public string initMenuInfo(string tableEnglishName, string tableChineseName, object objTable)
        {
            StringBuilder sbResult = new StringBuilder();
            sbResult.Append("-- -------------------------------------------------------------\r\n");
            sbResult.Append("-- 基础数脚本\r\n");
            sbResult.Append(string.Format("-- 初始化 {0}({1})表基础数据      : {0}\r\n", tableEnglishName, tableChineseName));
            sbResult.Append("-- -------------------------------------------------------------\r\n\r\n");
            sbResult.Append(modrecordheaderModel);

            sbResult.Append(string.Format("TRUNCATE TABLE {0}\r\n\r\n", tableEnglishName));

            List<MenuInfo> menuInfoLst = objTable as List<MenuInfo>;
            // 新增修改记录
            if (menuInfoLst != null)
            {
                foreach (var menuInfo in menuInfoLst)
                {
                    // 特殊处理，对于插入的文本中已经包含了单引号则需要用转义字符来处理
                    sbResult.Append(string.Format("INSERT [{0}] ([Gid], [Pgid], [Name], [Icon], [Seq], [AuthGid], [IsVisable], [WinformClass], [Url], [WebIcon], [SystemtypeId], [CreatorId], [CreatorTime], [EditorId], [LastUpdateTime], [IsDelete]) VALUES (N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', {7}, N'{8}', N'{9}', N'{10}',N'{11}', {12}, GETDATE(), {13}, GETDATE(),{14});\r\n", tableEnglishName, menuInfo.Gid, menuInfo.Pgid, menuInfo.Name, menuInfo.Icon, menuInfo.Seq, menuInfo.AuthGid, menuInfo.IsVisable, menuInfo.WinformClass, menuInfo.Url, menuInfo.WebIcon, menuInfo.SystemtypeId, menuInfo.CreatorId,  menuInfo.EditorId,  menuInfo.IsDelete ));
                }
            }

            return sbResult.ToString();
        }
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="tableEnglishName"></param>
        /// <param name="tableChineseName"></param>
        /// <param name="objTable"></param>
        /// <returns></returns>
        public string initFunctionInfo(string tableEnglishName, string tableChineseName, object objTable)
        {
            StringBuilder sbResult = new StringBuilder();
            sbResult.Append("-- -------------------------------------------------------------\r\n");
            sbResult.Append("-- 基础数脚本\r\n");
            sbResult.Append(string.Format("-- 初始化 {0}({1})表基础数据      : {0}\r\n", tableEnglishName, tableChineseName));
            sbResult.Append("-- -------------------------------------------------------------\r\n\r\n");
            sbResult.Append(modrecordheaderModel);

            sbResult.Append(string.Format("TRUNCATE TABLE {0}\r\n\r\n", tableEnglishName));

            List<FunctionInfo> functionInfolst = objTable as List<FunctionInfo>;
            // 新增修改记录
            if (functionInfolst != null)
            {
                foreach (var functionInfo in functionInfolst)
                {
                    sbResult.Append(string.Format("INSERT [{0}] ([Gid], [Pgid], [Name], [AuthGid], [SystemtypeId], [Seq]) VALUES (N'{1}',  N'{2}', N'{3}', N'{4}', N'{5}', N'{6}');\r\n", tableEnglishName, functionInfo.ID, functionInfo.PID, functionInfo.Name, functionInfo.FunctionId, functionInfo.SystemType_ID, functionInfo.Seq));
                }
            }

            return sbResult.ToString();
        }
    }
}
