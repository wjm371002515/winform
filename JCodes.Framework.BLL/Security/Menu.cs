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
	public class Menus : BaseBLL<MenuInfo>
    {
        private IMenus menuDal;

        public Menus() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            baseDal.OnOperationLog += new OperationLogEventHandler(OperationLog.OnOperationLog);//如果需要记录操作日志，则实现这个事件

            this.menuDal = baseDal as IMenus;
        }
                
        /// <summary>
        /// 获取树形结构的菜单列表
        /// </summary>
        public List<MenuNodeInfo> GetTree(string systemType)
        {
            return menuDal.GetTree(systemType);
        }

        /// <summary>
        /// 获取所有的菜单列表
        /// </summary>
        public List<MenuInfo> GetAllTree(string systemType)
        {
            return menuDal.GetAllMenu(systemType);
        }

        /// <summary>
        /// 获取第一级的菜单列表
        /// </summary>
        public List<MenuInfo> GetTopMenu(string systemType)
        {
            return menuDal.GetTopMenu(systemType);
        }

        /// <summary>
        /// 获取指定菜单下面的树形列表
        /// </summary>
        /// <param name="id">指定菜单ID</param>
        public List<MenuNodeInfo> GetTreeByID(string mainMenuID)
        {
            return menuDal.GetTreeByID(mainMenuID);
        }

        /// <summary>
        /// 根据指定的父ID获取其下面一级（仅限一级）的菜单列表
        /// </summary>
        /// <param name="pid">菜单父ID</param>
        public List<MenuInfo> GetMenuByID(string pid)
        {
            return menuDal.GetMenuByID(pid);
        }

    }
}
