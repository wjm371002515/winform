using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 用户参数配置
    /// </summary>
	public class UserParameter : BaseBLL<UserParameterInfo>
    {
        public UserParameter() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 保存配置（插入或更新）到数据库
        /// </summary>
        /// <param name="info">信息对象</param>
        /// <returns></returns>
        public bool SaveParamater(UserParameterInfo info)
        {
            IUserParameter dal = baseDal as IUserParameter;
            return dal.SaveParamater(info);
        }

        /// <summary>
        /// 根据类名称和用户标识获取参数配置内容
        /// </summary>
        /// <param name="name">类名称</param>
        /// <param name="creator">用户标识</param>
        /// <returns></returns>
        public string LoadParameter(string name, string creator = null)
        {
            IUserParameter dal = baseDal as IUserParameter;
            return dal.LoadParameter(name, creator);
        }
    }
}
