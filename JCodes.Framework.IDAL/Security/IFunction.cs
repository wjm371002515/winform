using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace JCodes.Framework.IDAL
{
    public interface IFunction : IBaseDAL<FunctionInfo>
	{
        List<FunctionInfo> GetFunctions(string roleIds, string systemtypeId);
        List<FunctionNodeInfo> GetFunctionNodes(string roleIds, string typeId);

        List<FunctionInfo> GetFunctionsByRoleId(Int32 roleId);

        List<FunctionNodeInfo> GetTree(string systemtypeId);
        List<FunctionNodeInfo> GetTreeById(string mainId);
        List<FunctionNodeInfo> GetTreeWithRole(string systemtypeId, List<Int32> roleList);

        bool DeleteWithSubNode(string mainId, Int32 userId);

        List<FunctionInfo> GetFunctionByPgid(string pgid);
	}
}