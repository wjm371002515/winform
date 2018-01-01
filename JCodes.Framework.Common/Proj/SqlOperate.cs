using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JCodes.Framework.Common.Proj
{
    public class SqlOperate
    {
        public static string printHeaderInfo(string DBString, string sqlfile, string version, string author, string lastModDate, string notes, string generateDate, List<ModRecordInfo> modLst)
        {
            // 动态加载汇编  
            string path = "JCodes.Framework.Common.dll";

            Assembly assembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + path);

            //获取类型，参数（名称空间+类）   
            string classFullName = string.Format("JCodes.Framework.Common.Proj.{0}Generate", DBString);
            Type type = assembly.GetType(classFullName);

            //创建该对象的实例，object类型，参数（名称空间+类）   
            object instance = assembly.CreateInstance(classFullName);

            //设置Show_Str方法中的参数类型，Type[]类型；如有多个参数可以追加多个   
            Type[] params_type = new Type[7];
            params_type[0] = Type.GetType("System.String");
            params_type[1] = Type.GetType("System.String");
            params_type[2] = Type.GetType("System.String");
            params_type[3] = Type.GetType("System.String");
            params_type[4] = Type.GetType("System.String");
            params_type[5] = Type.GetType("System.String");
            params_type[6] = Type.GetType("System.Object");

            //设置Show_Str方法中的参数值；如有多个参数可以追加多个   
            Object[] params_obj = new Object[7];
            params_obj[0] = sqlfile;
            params_obj[1] = version;
            params_obj[2] = author;
            params_obj[3] = lastModDate;
            params_obj[4] = notes;
            params_obj[5] = generateDate;
            params_obj[6] = modLst;

            //执行Show_Str方法   
            object str = type.GetMethod("printHeaderInfo", params_type).Invoke(instance, params_obj);

            LogHelper.WriteLog(jCodesenum.BaseEnum.LogLevel.LOG_LEVEL_DEBUG, str.ToString(), typeof(SqlOperate));
         
            return str.ToString();
        }

        public static string initTableInfo(string DBString, string tableEnglishName, string tableChineseName, Boolean existHistable, object objFields, object objIndexs, object dictFieldType, bool isRichText)
        {
            // 动态加载汇编  
            string path = "JCodes.Framework.Common.dll";

            Assembly assembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + path);

            //获取类型，参数（名称空间+类）   
            string classFullName = string.Format("JCodes.Framework.Common.Proj.{0}Generate", DBString);
            Type type = assembly.GetType(classFullName);

            //创建该对象的实例，object类型，参数（名称空间+类）   
            object instance = assembly.CreateInstance(classFullName);

            //设置Show_Str方法中的参数类型，Type[]类型；如有多个参数可以追加多个   
            Type[] params_type = new Type[6];
            params_type[0] = Type.GetType("System.String");
            params_type[1] = Type.GetType("System.String");
            params_type[2] = Type.GetType("System.Boolean");
            params_type[3] = Type.GetType("System.Object");
            params_type[4] = Type.GetType("System.Object");
            params_type[5] = Type.GetType("System.Object");

            //设置Show_Str方法中的参数值；如有多个参数可以追加多个   
            Object[] params_obj = new Object[6];
            params_obj[0] = tableEnglishName;
            params_obj[1] = tableChineseName;
            params_obj[2] = existHistable;
            params_obj[3] = objFields;
            params_obj[4] = objIndexs;
            params_obj[5] = dictFieldType;

            //执行Show_Str方法   
            object str = type.GetMethod("initTableInfo", params_type).Invoke(instance, params_obj);

            LogHelper.WriteLog(jCodesenum.BaseEnum.LogLevel.LOG_LEVEL_DEBUG, str.ToString(), typeof(SqlOperate));

            return str.ToString();
        }

        /// <summary>
        /// 根据正则表达式转换为RichTextBox内容
        /// </summary>
        private string ToRichText(string str) {
            return str;
        }

    }
}
