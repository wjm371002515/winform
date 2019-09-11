using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Extension;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 角色的数据权限
    /// </summary>
	public class RoleData : BaseBLL<RoleDataInfo>
    {
        public RoleData() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);

            baseDal.OnOperationLog += new OperationLogEventHandler(OperationLog.OnOperationLog);//如果需要记录操作日志，则实现这个事件
        }

        /// <summary>
        /// 获取用户所属角色对应的管理公司列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public List<int> GetBelongCompanysByUser(int userId)
        {
            List<RoleDataInfo> roleDataList = FindByUser(userId);
            List<int> companyList = new List<int>();

            foreach (RoleDataInfo roleDataInfo in roleDataList)
            {
                if (!string.IsNullOrEmpty(roleDataInfo.BelongCompanys))
                {
                    List<int> tmpList = roleDataInfo.BelongCompanys.ToDelimitedList<int>(",");
                    foreach (int id in tmpList)
                    {
                        if (!companyList.Contains(id))
                        {
                            companyList.Add(id);
                        }
                    }
                }
            }
            return companyList;
        }

        /// <summary>
        /// 获取用户所属角色对应的管理公司列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public List<int> GetBelongDeptsByUser(int userId)
        {
            List<RoleDataInfo> roleDataList = FindByUser(userId);
            List<int> deptList = new List<int>();

            foreach (RoleDataInfo roleDataInfo in roleDataList)
            {
                if (!string.IsNullOrEmpty(roleDataInfo.BelongDepts))
                {
                    List<int> tmpList = roleDataInfo.BelongDepts.ToDelimitedList<int>(",");
                    foreach (int id in tmpList)
                    {
                        if (!deptList.Contains(id))
                        {
                            deptList.Add(id);
                        }
                    }
                }
            }
            return deptList;
        }


        /// <summary>
        /// 获取用户所属角色对应的数据权限集合
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public List<RoleDataInfo> FindByUser(int userId)
        {
            //获取用户包含的角色
            List<RoleInfo> rolesByUser = BLLFactory<Role>.Instance.GetRolesByUser(userId);
            List<int> roleList = new List<int>();
            foreach (RoleInfo info in rolesByUser)
            {
                roleList.Add(info.Id);
            }

            //获取用户信息
            UserInfo userInfo = BLLFactory<User>.Instance.FindByID(userId);

            //根据角色获取对应的数据权限集合
            List<RoleDataInfo> list = new List<RoleDataInfo>();
            foreach (int roleId in roleList)
            {
                RoleDataInfo info = FindByRoleId(roleId);
                if (info != null)
                {
                    #region 替换所在部门和所在公司的值
                    if (!string.IsNullOrEmpty(info.BelongCompanys))
                    {
                        //不重复出现的公司列表
                        List<int> notDuplicatedCompanyList = new List<int>();

                        List<int> companyList = info.BelongCompanys.ToDelimitedList<int>(",");
                        for (int i = 0; i < companyList.Count; i++)
                        {
                            // 20170610 wujm 这里不需要对其做转换反而会造成权限不对
                            /*if (companyList[i] == -1) // -1代表用户所在公司
                            {
                                companyList[i] = userInfo.Company_ID.ToInt32();
                            }*/

                            if (!notDuplicatedCompanyList.Contains(companyList[i]))
                            {
                                notDuplicatedCompanyList.Add(companyList[i]);
                            }
                        }
                        info.BelongCompanys = string.Join(",", notDuplicatedCompanyList);
                    }
                    if (!string.IsNullOrEmpty(info.BelongDepts))
                    {
                        //不重复出现的部门列表
                        List<int> notDuplicatedDeptList = new List<int>();

                        List<int> deptList = info.BelongDepts.ToDelimitedList<int>(",");
                        for (int i = 0; i < deptList.Count; i++)
                        {
                            // 20170610 wujm 这里不需要对其做转换反而会造成权限不对
                            /*if (deptList[i] == -11) // -11代表用户所在部门
                            {
                                deptList[i] = userInfo.Dept_ID.ToInt32();
                            }*/

                            if (!notDuplicatedDeptList.Contains(deptList[i]))
                            {
                                notDuplicatedDeptList.Add(deptList[i]);
                            }
                        }

                        info.BelongDepts = string.Join(",", deptList);
                    } 
                    #endregion

                    list.Add(info);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据角色ID获取对应的记录对象
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public RoleDataInfo FindByRoleId(int roleId)
        {
            string condition = string.Format("Role_ID = {0}", roleId);
            return baseDal.FindSingle(condition);
        }

        /// <summary>
        /// 保存角色的数据权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="belongCompanys">包含公司</param>
        /// <param name="belongDepts">包含部门</param>
        /// <returns></returns>
        public bool UpdateRoleData(int roleId, string belongCompanys, string belongDepts)
        {
            bool result = false;
            RoleDataInfo info = FindByRoleId(roleId);
            if (info != null)
            {
                info.BelongCompanys = belongCompanys;
                info.BelongDepts = belongDepts;

                result = baseDal.Update(info, info.ID);
            }
            else
            {
                info = new RoleDataInfo();
                info.Role_ID = roleId;
                info.BelongCompanys = belongCompanys;
                info.BelongDepts = belongDepts;

                result = baseDal.Insert(info);
            }
            return result;
        }

        /// <summary>
        /// 获取数据库的配置，角色数据权限(不对所在公司，所在部门转义）
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        public Dictionary<int, int> GetRoleDataDict(int roleID)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            //获取用户的角色权限
            RoleDataInfo roleDataInfo = FindByRoleId(roleID);
            if (roleDataInfo != null)
            {
                //包含公司
                if (!string.IsNullOrEmpty(roleDataInfo.BelongCompanys))
                {
                    List<int> companyList = roleDataInfo.BelongCompanys.ToDelimitedList<int>(",");
                    foreach (int id in companyList)
                    {
                        if (!dict.ContainsKey(id))
                        {
                            dict.Add(id, id);
                        }
                    }
                }
                //包含部门
                if (!string.IsNullOrEmpty(roleDataInfo.BelongDepts))
                {
                    List<int> deptList = roleDataInfo.BelongDepts.ToDelimitedList<int>(",");
                    foreach (int id in deptList)
                    {
                        if (!dict.ContainsKey(id))
                        {
                            dict.Add(id, id);
                        }
                    }
                }
                //排除部门

            }
            return dict;
        }
    }
}
