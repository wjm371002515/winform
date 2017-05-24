using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;

namespace JCodes.Framework.IDAL
{
    /// <summary>
    /// 功能菜单
    /// </summary>
	public interface IMenus : IBaseDAL<MenuInfo>
    {       
        /// <summary>
        /// 获取树形结构的菜单列表
        /// </summary>
        List<MenuNodeInfo> GetTree(string systemType ="");

        /// <summary>
        /// 获取所有的菜单列表
        /// </summary>
        List<MenuInfo> GetAllMenu(string systemType = "");

        /// <summary>
        /// 获取第一级的菜单列表
        /// </summary>
        List<MenuInfo> GetTopMenu(string systemType = "");

        /// <summary>
        /// 获取指定菜单下面的树形列表
        /// </summary>
        /// <param name="id">指定菜单ID</param>
        List<MenuNodeInfo> GetTreeByID(string id);

        /// <summary>
        /// 根据指定的父ID获取其下面一级（仅限一级）的菜单列表
        /// </summary>
        /// <param name="pid">菜单父ID</param>
        List<MenuInfo> GetMenuByID(string pid);

    }
}