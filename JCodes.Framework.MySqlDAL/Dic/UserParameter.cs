using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.IO;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JCodes.Framework.Common.Framework.BaseDAL;
using JCodes.Framework.Common.Databases;

namespace JCodes.Framework.MySqlDAL
{
    /// <summary>
    /// 用户参数配置
    /// </summary>
    public class UserParameter : BaseDALMySql<UserParameterInfo>, IUserParameter
    {
        #region 对象实例及构造函数

        public static UserParameter Instance
        {
            get
            {
                return new UserParameter();
            }
        }
        public UserParameter()
            : base("TB_UserParameter", "ID")
        {
        }

        #endregion

        /// <summary>
        /// 将DataReader的属性值转化为实体类的属性值，返回实体类
        /// </summary>
        /// <param name="dr">有效的DataReader对象</param>
        /// <returns>实体类对象</returns>
        protected override UserParameterInfo DataReaderToEntity(IDataReader dataReader)
        {
            UserParameterInfo info = new UserParameterInfo();
            SmartDataReader reader = new SmartDataReader(dataReader);

            info.ID = reader.GetString("ID");
            info.Name = reader.GetString("Name");
            info.Content = reader.GetString("Content");
            info.Creator = reader.GetString("Creator");
            info.CreateTime = reader.GetDateTime("CreateTime");

            return info;
        }

        /// <summary>
        /// 将实体对象的属性值转化为Hashtable对应的键值
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(UserParameterInfo obj)
        {
            UserParameterInfo info = obj as UserParameterInfo;
            Hashtable hash = new Hashtable();

            hash.Add("ID", info.ID);
            hash.Add("Name", info.Name);
            hash.Add("Content", info.Content);
            hash.Add("Creator", info.Creator);
            hash.Add("CreateTime", info.CreateTime);

            return hash;
        }

        /// <summary>
        /// 获取字段中文别名（用于界面显示）的字典集合
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetColumnNameAlias()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            #region 添加别名解析
            //dict.Add("ID", "编号");
            dict.Add("ID", "");
            dict.Add("Name", "类型名称");
            dict.Add("Content", "参数文本内容");
            dict.Add("Creator", "创建人");
            dict.Add("CreateTime", "创建时间");
            #endregion

            return dict;
        }

        /// <summary>
        /// 保存配置（插入或更新）到数据库
        /// </summary>
        /// <param name="info">信息对象</param>
        /// <returns></returns>
        public bool SaveParamater(UserParameterInfo info)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(info.Name))
            {
                //类名称和用户组合一个唯一名称 
                string settingsFilename = info.Name;
                if (!string.IsNullOrEmpty(info.Creator))
                {
                    settingsFilename = Path.Combine(info.Creator, info.Name);
                }
                
                Database db = CreateDatabase();

                //使用统一的数据库检查存储操作
                string sql = string.Format("Update {0} Set Content = '{1}' where Name='{2}' ", tableName, info.Content, settingsFilename);
                DbCommand command = db.GetSqlStringCommand(sql);
                result = db.ExecuteNonQuery(command) > 0;
                if (!result)
                {
                    info.Name = settingsFilename;//修改保存的名称

                    result = Insert(info);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据类名称和用户标识获取参数配置内容
        /// </summary>
        /// <param name="name">类名称</param>
        /// <param name="creator">用户标识</param>
        /// <returns></returns>
        public string LoadParameter(string name, string creator = null)
        {
            string result = null;
            if (!string.IsNullOrEmpty(name))
            {
                //类名称和用户组合一个唯一名称 
                string settingsFilename = name;
                if (!string.IsNullOrEmpty(creator))
                {
                    settingsFilename = Path.Combine(creator, name);
                }

                Database db = CreateDatabase();
                string sql = string.Format("Select Content from TB_UserParameter Where Name='{0}' ", settingsFilename);
                DbCommand command = db.GetSqlStringCommand(sql);
                object objResult = db.ExecuteScalar(command);
                if (objResult != null)
                {
                    result = objResult.ToString();
                }
            }
            return result;
        }
    }
}