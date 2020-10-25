using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using System;
using System.Collections;
using System.Collections.Generic;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 系统功能定义
    /// </summary>
    public class Function : BaseBLL<FunctionInfo>
    {
        private IFunction dal;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Function() : base()
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

            dal = baseDal as IFunction;
        }

        /// <summary>
        /// 根据角色ID字符串（逗号分开)和系统类型ID，获取对应的操作功能列表
        /// </summary>
        /// <param name="roleIds">角色ID</param>
        /// <param name="systemtypeId">系统类型ID</param>
        /// <returns></returns>
        public List<FunctionInfo> GetFunctions(string roleIds, string systemtypeId)
        {
            if (roleIds == string.Empty)
            {
                roleIds = "-1";
            }
            return this.dal.GetFunctions(roleIds, systemtypeId);
        }

        /// <summary>
        /// 根据角色ID字符串（逗号分开)和系统类型ID，获取对应的操作功能列表
        /// </summary>
        /// <param name="roleIDs">角色ID</param>
        /// <param name="typeID">系统类型ID</param>
        /// <returns></returns>
        public List<FunctionNodeInfo> GetFunctionNodes(string roleIds, string systemtypeId)
        {
            if (roleIds == string.Empty)
            {
                roleIds = "-1";
            }
            return this.dal.GetFunctionNodes(roleIds, systemtypeId);
        }

        /// <summary>
        /// 根据角色ID获取对应的操作功能列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        public List<FunctionInfo> GetFunctionsByRoleId(Int32 roleId)
        {
            return this.dal.GetFunctionsByRoleId(roleId);
        }

        /// <summary>
        /// 根据用户ID，获取对应的功能列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="typeID">系统类别ID</param>
        /// <returns></returns>
        public List<FunctionInfo> GetFunctionsByUser(Int32 userId, string systemtypeId)
        {
            List<RoleInfo> rolesByUser = BLLFactory<Role>.Instance.GetRolesByUser(userId);
            string roleIds = ",";
            foreach (RoleInfo info in rolesByUser)
            {
                roleIds = roleIds + info.Id + ",";
            }
            roleIds = roleIds.Trim(',');//移除前后的逗号

            List<FunctionInfo> functions = new List<FunctionInfo>();
            if (!string.IsNullOrEmpty(roleIds))
            {
                functions = this.GetFunctions(roleIds, systemtypeId);
            }
            return functions;
        }

        /// <summary>
        /// 根据用户ID，获取对应的功能列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="systemtypeId">系统类别ID</param>
        /// <returns></returns>
        public List<FunctionNodeInfo> GetFunctionNodesByUser(Int32 userId, string systemtypeId)
        {
            List<RoleInfo> rolesByUser = BLLFactory<Role>.Instance.GetRolesByUser(userId);
            string roleIds = ",";
            foreach (RoleInfo info in rolesByUser)
            {
                roleIds = roleIds + info.Id + ",";
            }
            roleIds = roleIds.Trim(',');//移除前后的逗号

            List<FunctionNodeInfo> functions = new List<FunctionNodeInfo>();
            if (!string.IsNullOrEmpty(roleIds))
            {
                functions = this.GetFunctionNodes(roleIds, systemtypeId);
            }
            return functions;
        }

        /// <summary>
        /// 获取树形结构的功能列表
        /// </summary>
        /// <param name="systemType">系统类型的OID</param>
        public List<FunctionNodeInfo> GetTree(string systemtypeId)
        {
            return dal.GetTree(systemtypeId);
        }

        /// <summary>
        /// 获取指定功能下面的树形列表
        /// </summary>
        /// <param name="id">指定功能ID</param>
        public List<FunctionNodeInfo> GetTreeById(string mainId)
        {
            return dal.GetTreeById(mainId);
        }
                       
        /// <summary>
        /// 根据角色获取树形结构的功能列表
        /// </summary>
        public List<FunctionNodeInfo> GetTreeWithRole(string systemtypeId, List<Int32> roleList)
        {
            return dal.GetTreeWithRole(systemtypeId, roleList);
        }
                      
        /// <summary>
        /// 根据角色获取树形结构的功能列表
        /// </summary>
        public List<FunctionNodeInfo> GetTreeWithUser(string systemtypeId, Int32 userId)
        {
            List<RoleInfo> rolesByUser = BLLFactory<Role>.Instance.GetRolesByUser(userId);
            List<Int32> roleList = new List<Int32>();
            foreach (RoleInfo info in rolesByUser)
            {
                roleList.Add(info.Id);
            }

            return GetTreeWithRole(systemtypeId, roleList);
        }

        /// <summary>
        /// 删除指定节点及其子节点。如果该节点含有子节点，子节点也会一并删除
        /// </summary>
        /// <param name="Id">节点ID</param>
        /// <returns></returns>
        public bool DeleteWithSubNode(string mainId, Int32 userId)
        {
            return dal.DeleteWithSubNode(mainId, userId);
        }

        /// <summary>
        /// 根据指定的父ID获取其下面一级（仅限一级）的功能列表
        /// </summary>
        /// <param name="pgid">菜单父ID</param>
        public List<FunctionInfo> GetFunctionByPgid(string pgid)
        {
            return dal.GetFunctionByPgid(pgid);
        }
    }
}