using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using System.Collections;
using System.Collections.Generic;

namespace JCodes.Framework.IDAL
{
    public interface IFunction : IBaseDAL<FunctionInfo>
	{
        List<FunctionInfo> GetFunctions(string roleIDs, string typeID);
        List<FunctionNodeInfo> GetFunctionNodes(string roleIDs, string typeID);

        List<FunctionInfo> GetFunctionsByRole(int roleID);

        List<FunctionNodeInfo> GetTree(string systemType);
        List<FunctionNodeInfo> GetTreeByID(string mainID);
        List<FunctionNodeInfo> GetTreeWithRole(string systemType, List<int> roleList);

        bool DeleteWithSubNode(string mainID);
	}
}