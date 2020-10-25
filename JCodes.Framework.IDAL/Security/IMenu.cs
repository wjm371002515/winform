using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.IDAL
{
    /// <summary>
    /// 功能菜单
    /// </summary>
	public interface IMenu : IBaseDAL<MenuInfo>
    {       
        /// <summary>
        /// 获取树形结构的菜单列表
        /// </summary>
        List<MenuNodeInfo> GetTree(string systemtypeId, IsVisable isVisable = IsVisable.是, IsDelete isDelete = IsDelete.否);

        /// <summary>
        /// 获取所有的菜单列表
        /// </summary>
        List<MenuInfo> GetAllMenu(string systemtypeId, IsVisable isVisable = IsVisable.是, IsDelete isDelete = IsDelete.否);

        /// <summary>
        /// 获取第一级的菜单列表
        /// </summary>
        List<MenuInfo> GetTopMenu(string systemtypeId, IsVisable isVisable = IsVisable.是, IsDelete isDelete = IsDelete.否);

        /// <summary>
        /// 获取指定菜单下面的树形列表
        /// </summary>
        /// <param name="id">指定菜单ID</param>
        List<MenuNodeInfo> GetTreeById(string mainMenuId, IsVisable isVisable = IsVisable.是, IsDelete isDelete = IsDelete.否);

        /// <summary>
        /// 根据指定的父ID获取其下面一级（仅限一级）的菜单列表
        /// </summary>
        /// <param name="Pgid">菜单父Id</param>
        List<MenuInfo> GetMenuById(string Pgid, IsVisable isVisable = IsVisable.是, IsDelete isDelete = IsDelete.否);

    }
}