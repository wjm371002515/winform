using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 功能菜单
    /// </summary>
	public class Menu : BaseBLL<MenuInfo>
    {
        private IMenu dal;

        public Menu() : base()
        {
            if (isMultiDatabase)
            {
                base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, dicmultiDatabase[this.GetType().Name].ToString());
            }
            else
            {
                base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            }

            baseDal.OnOperationLog += new OperationLogEventHandler(OperationLog.OnOperationLog);//如果需要记录操作日志，则实现这个事件

            dal = baseDal as IMenu;
        }
                
        /// <summary>
        /// 获取树形结构的菜单列表
        /// </summary>
        public List<MenuNodeInfo> GetTree(string systemtypeId)
        {
            return dal.GetTree(systemtypeId);
        }

        /// <summary>
        /// 获取所有的菜单列表
        /// </summary>
        public List<MenuInfo> GetAllTree(string systemtypeId)
        {
            return dal.GetAllMenu(systemtypeId);
        }

        /// <summary>
        /// 获取第一级的菜单列表
        /// </summary>
        public List<MenuInfo> GetTopMenu(string systemtypeId)
        {
            return dal.GetTopMenu(systemtypeId);
        }

        /// <summary>
        /// 获取指定菜单下面的树形列表
        /// </summary>
        /// <param name="id">指定菜单ID</param>
        public List<MenuNodeInfo> GetTreeById(string mainMenuId)
        {
            return dal.GetTreeById(mainMenuId);
        }

        /// <summary>
        /// 根据指定的父ID获取其下面一级（仅限一级）的菜单列表
        /// </summary>
        /// <param name="pgid">菜单父ID</param>
        public List<MenuInfo> GetMenuById(string pgid)
        {
            return dal.GetMenuById(pgid);
        }

    }
}
