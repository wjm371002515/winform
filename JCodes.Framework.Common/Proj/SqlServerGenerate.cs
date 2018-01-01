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
                        sbFields.Append(string.Format("\r\n{0} {1} ,", tablefieldInfo.FieldName, dicts[tablefieldInfo.FieldType]));
                    else
                        sbFields.Append(string.Format("\r\n{0} {1} ,", tablefieldInfo.FieldName, "不存在的数据类型请检查"));
                }
            }

            List<TableIndexsInfo> indexsLst = objIndexs as List<TableIndexsInfo>;
            // 新增修改记录
            if (indexsLst != null)
            {
                foreach (TableIndexsInfo tableindexInfo in indexsLst)
                {
                    //sbUniques.Append(string.Format(modrecordModel, modrecord.ModVersion.ToString().PadRight(15, ' '), modrecord.ModDate.ToString("yyyy-MM-dd HH:mm:ss").PadRight(19, ' '), modrecord.ModOrderId.PadRight(11, ' '), getStrLength(5, modrecord.Proposer), getStrLength(5, modrecord.Programmer), getStrLength(20, modrecord.ModContent), getStrLength(20, modrecord.ModReason), getStrLength(20, modrecord.Remark)));
                    // 约束只会是3者之一 改成下拉框
                    //tablein
            //         CONSTRAINT uc_PersonID UNIQUE (Id_P,LastName),  -- 创建唯一索引
            //CONSTRAINT pk_PersonID PRIMARY KEY (Id_P)       -- 创建主键
                }
            }

            sbResult.Append(sbFields.ToString().TrimEnd(','));

            sbResult.Append(");\r\n");

            if (existHistable)
            {
                sbResult.Append("历史表数据添加");
            }


            return sbResult.ToString();
        }
    }
}
