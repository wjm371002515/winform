using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace JCodes.Framework.IDAL
{
    public interface IFunctions : IBaseDAL<FunctionInfo>
	{
        List<FunctionInfo> GetFunctions(string roleIDs, string typeID);
        List<FunctionNodeInfo> GetFunctionNodes(string roleIDs, string typeID);

        List<FunctionInfo> GetFunctionsByRole(int roleID);

        List<FunctionNodeInfo> GetTree(string systemType);
        List<FunctionNodeInfo> GetTreeByID(string mainID);
        List<FunctionNodeInfo> GetTreeWithRole(string systemType, List<Int32> roleList);

        bool DeleteWithSubNode(string mainID, Int32 userId);

        List<FunctionInfo> GetFunctionByPID(string PID);
	}
}