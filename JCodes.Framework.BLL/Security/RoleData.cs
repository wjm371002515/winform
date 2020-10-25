using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Common.Extension;
using JCodes.Framework.IDAL;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 角色的数据权限
    /// </summary>
	public class RoleData : BaseBLL<RoleDataInfo>
    {
        private IRoleData dal = null;

        public RoleData() : base()
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

            dal = baseDal as IRoleData;
        }

        /// <summary>
        /// 获取用户所属角色对应的管理公司列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public List<int> GetBelongCompanysByUser(Int32 userId)
        {
            List<RoleDataInfo> roleDataList = FindByUser(userId);
            List<Int32> companyList = new List<Int32>();

            foreach (RoleDataInfo roleDataInfo in roleDataList)
            {
                if (!string.IsNullOrEmpty(roleDataInfo.CompanyLst))
                {
                    List<Int32> tmpList = roleDataInfo.CompanyLst.ToDelimitedList<Int32>(",");
                    foreach (Int32 id in tmpList)
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
        public List<Int32> GetBelongDeptsByUser(Int32 userId)
        {
            List<RoleDataInfo> roleDataList = FindByUser(userId);
            List<Int32> deptList = new List<Int32>();

            foreach (RoleDataInfo roleDataInfo in roleDataList)
            {
                if (!string.IsNullOrEmpty(roleDataInfo.DeptLst))
                {
                    List<Int32> tmpList = roleDataInfo.DeptLst.ToDelimitedList<Int32>(",");
                    foreach (Int32 id in tmpList)
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
        public List<RoleDataInfo> FindByUser(Int32 userId)
        {
            //获取用户包含的角色
            List<RoleInfo> rolesByUser = BLLFactory<Role>.Instance.GetRolesByUser(userId);
            List<Int32> roleList = new List<Int32>();
            foreach (RoleInfo info in rolesByUser)
            {
                roleList.Add(info.Id);
            }

            List<RoleDataInfo> list = new List<RoleDataInfo>();

            // 获取用户信息
            UserInfo userInfo = BLLFactory<User>.Instance.FindById(userId);

            // 根据角色获取对应的数据权限集合
            foreach (Int32 roleId in roleList)
            {
                RoleDataInfo info = FindByRoleId(roleId);
                if (info != null)
                {
                    #region 替换所在部门和所在公司的值
                    if (!string.IsNullOrEmpty(info.CompanyLst))
                    {
                        //不重复出现的公司列表
                        List<Int32> notDuplicatedCompanyList = new List<Int32>();

                        List<Int32> companyList = info.CompanyLst.ToDelimitedList<Int32>(",");
                        for (Int32 i = 0; i < companyList.Count; i++)
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
                        info.CompanyLst = string.Join(",", notDuplicatedCompanyList);
                    }
                    if (!string.IsNullOrEmpty(info.DeptLst))
                    {
                        //不重复出现的部门列表
                        List<Int32> notDuplicatedDeptList = new List<Int32>();

                        List<Int32> deptList = info.DeptLst.ToDelimitedList<Int32>(",");
                        for (Int32 i = 0; i < deptList.Count; i++)
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

                        info.DeptLst = string.Join(",", deptList);
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
        public RoleDataInfo FindByRoleId(Int32 roleId)
        {
            string condition = string.Format("RoleId = {0}", roleId);
            return baseDal.FindSingle(condition);
        }

        /// <summary>
        /// 保存角色的数据权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="belongCompanys">包含公司</param>
        /// <param name="belongDepts">包含部门</param>
        /// <returns></returns>
        public bool UpdateRoleData(Int32 roleId, string belongCompanys, string belongDepts)
        {
            bool result = false;
            RoleDataInfo info = FindByRoleId(roleId);
            if (info != null)
            {
                info.CompanyLst = belongCompanys;
                info.DeptLst = belongDepts;

                result = baseDal.Update(info, info.Id);
            }
            else
            {
                info = new RoleDataInfo();
                info.RoleId = (short)roleId;
                info.CompanyLst = belongCompanys;
                info.DeptLst = belongDepts;
                info.Id = baseDal.GetMaxId() + 1;

                result = baseDal.Insert(info);
            }
            return result;
        }

        /// <summary>
        /// 获取数据库的配置，角色数据权限(不对所在公司，所在部门转义）
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public Dictionary<Int32, Int32> GetRoleDataDict(Int32 roleId)
        {
            Dictionary<Int32, Int32> dict = new Dictionary<Int32, Int32>();
            //获取用户的角色权限
            RoleDataInfo roleDataInfo = FindByRoleId(roleId);
            if (roleDataInfo != null)
            {
                //包含公司
                if (!string.IsNullOrEmpty(roleDataInfo.CompanyLst))
                {
                    List<Int32> companyList = roleDataInfo.CompanyLst.ToDelimitedList<Int32>(",");
                    foreach (Int32 id in companyList)
                    {
                        if (!dict.ContainsKey(id))
                        {
                            dict.Add(id, id);
                        }
                    }
                }
                //包含部门
                if (!string.IsNullOrEmpty(roleDataInfo.DeptLst))
                {
                    List<Int32> deptList = roleDataInfo.DeptLst.ToDelimitedList<Int32>(",");
                    foreach (int id in deptList)
                    {
                        if (!dict.ContainsKey(id))
                        {
                            dict.Add(id, id);
                        }
                    }
                }
            }
            return dict;
        }
    }
}
