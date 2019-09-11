using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.Common;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 部门机构信息
    /// </summary>
    public class OU : BaseBLL<OUInfo>
    {
        private IOU ouDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        public OU() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            baseDal.OnOperationLog += new OperationLogEventHandler(OperationLog.OnOperationLog);//如果需要记录操作日志，则实现这个事件
            
            this.ouDal = baseDal as IOU;
        }

        /// <summary>
        /// 重载只是显示未被删除的记录
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public override List<OUInfo> GetAll(DbTransaction trans = null)
        {
            string condition = string.Format(" IsDelete = 0");
            return base.Find(condition, trans);
        }

        /// <summary>
        /// 获取顶部的集团信息
        /// </summary>
        /// <returns></returns>
        public List<OUInfo> GetTopGroup()
        {
            string condition = string.Format("PID=-1 ");
            return Find(condition);
        }
               
        /// <summary>
        /// 根据当前用户身份，获取对应的顶级机构管理节点。
        /// 如果是超级管理员，返回集团节点；如果是公司管理员，返回其公司节点
        /// </summary>
        /// <returns></returns>
        public List<OUInfo> GetMyTopGroup(int userId)
        {
            List<OUInfo> list = new List<OUInfo>();

            UserInfo userInfo = BLLFactory<User>.Instance.FindByID(userId);
            if (userInfo != null)
            {
                OUInfo groupInfo = null;
                if (BLLFactory<User>.Instance.UserInRoleById(userId, RoleInfo.SuperAdminName))
                {
                    //超级管理员取集团节点
                    list.AddRange(GetTopGroup());
                }
                else
                {
                    groupInfo = this.FindByID(userInfo.CompanyId);//公司管理员取公司节点
                    list.Add(groupInfo);
                }
            }
            return list;
        }
        
        /// <summary>
        /// 获取部门分类为公司的列表【Category='公司'】
        /// </summary>
        /// <returns></returns>
        public List<OUInfo> GetAllCompany(int groupId)
        {
            string condition = string.Format("OuType='公司' AND PID={0} ", groupId);
            return Find(condition);
        }

         /// <summary>
        /// 获取集团和公司的列表
        /// </summary>
        /// <returns></returns>
        public List<OUInfo> GetGroupCompany()
        {
            string condition = string.Format("OuType='公司' or Category='集团' ");
            return Find(condition);
        }

        /// <summary>
        /// 获取集团和公司的树形结构列表
        /// </summary>
        /// <returns></returns>
        public List<OUNodeInfo> GetGroupCompanyTree()
        {
            List<OUNodeInfo> list = new List<OUNodeInfo>();

            //顶级OU的PID=-1 为集团OU节点
            List<OUInfo> ouList = GetTopGroup();
            foreach (OUInfo groupOU in ouList)
            {
                if (groupOU != null)
                {
                    OUNodeInfo groupNodeInfo = new OUNodeInfo(groupOU);

                    List<OUInfo> companyList = GetAllCompany(groupOU.Id);
                    foreach (OUInfo info in companyList)
                    {
                        groupNodeInfo.Children.Add(new OUNodeInfo(info));
                    }
                    list.Add(groupNodeInfo);
                }
            }
            return list;
        }
        
        /// <summary>
        /// 为机构制定新的人员列表
        /// </summary>
        /// <param name="ouID">机构ID</param>
        /// <param name="newUserList">人员列表</param>
        /// <returns></returns>
        public bool EditOuUsers(int ouID, List<int> newUserList)
        {
            return ouDal.EditOuUsers(ouID, newUserList);
        }

        /// <summary>
        /// 为机构添加相关用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="ouID">机构ID</param>
        public void AddUser(int userID, int ouID)
        {
            if (this.OUInRole(ouID, RoleInfo.SuperAdminName))
            {
                BLLFactory<User>.Instance.CancelExpire(userID);
            }

            this.ouDal.AddUser(userID, ouID);
        }

        /// <summary>
        /// 根据角色ID获取对应的机构列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        public List<OUInfo> GetOUsByRole(int roleID)
        {
            return this.ouDal.GetOUsByRole(roleID);
        }

        /// <summary>
        /// 获取指定用户的机构列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public List<OUInfo> GetOUsByUser(int userID)
        {
            return this.ouDal.GetOUsByUser(userID);
        }

        /// <summary>
        /// 判断机构是否在指定的角色中
        /// </summary>
        /// <param name="ouID">机构ID</param>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public bool OUInRole(int ouID, string roleName)
        {
            bool result = false;
            List<RoleInfo> rolesByOU = BLLFactory<Role>.Instance.GetRolesByOU(ouID);
            foreach (RoleInfo info in rolesByOU)
            {
                if (info.Name == roleName)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 在机构中移除指定的用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="ouID">机构ID</param>
        public void RemoveUser(int userID, int ouID)
        {
            if (this.OUInRole(ouID, RoleInfo.SuperAdminName))
            {
                List<SimpleUserInfo> adminSimpleUsers = BLLFactory<Role>.Instance.GetAdminSimpleUsers();
                if (adminSimpleUsers.Count == 1)
                {
                    SimpleUserInfo info = (SimpleUserInfo)adminSimpleUsers[0];
                    if (userID == info.Id)
                    {
                        throw new Exception("管理员角色至少需要包含一个用户！");
                    }
                }
            }
            ouDal.RemoveUser(userID, ouID);
        }
                        
        /// <summary>
        /// 根据指定机构节点ID，获取其下面所有机构列表
        /// </summary>
        /// <param name="parentId">指定机构节点ID</param>
        /// <returns></returns>
        public List<OUInfo> GetAllOUsByParent(int parentId)
        {
            return ouDal.GetAllOUsByParent(parentId);
        }

        /// <summary>
        /// 获取树形结构的机构列表
        /// </summary>
        public List<OUNodeInfo> GetTree()
        {
            return ouDal.GetTree();
        }

        /// <summary>
        /// 获取指定机构下面的树形列表
        /// </summary>
        /// <param name="mainOUID">指定机构ID</param>
        public List<OUNodeInfo> GetTreeByID(int mainOUID)
        {
            return ouDal.GetTreeByID(mainOUID);
        }

        /// <summary>
        /// 获取机构的名称
        /// </summary>
        /// <param name="id">机构ID</param>
        /// <returns></returns>
        public string GetName(int id, DbTransaction trans = null)
        {
            return ouDal.GetName(id, trans);
        }

        /// <summary>
        /// 根据机构名称获取对应的对象
        /// </summary>
        /// <param name="name">机构名称</param>
        /// <returns></returns>
        public OUInfo FindByName(string name)
        {
            string condition = string.Format("Name ='{0}' ", name);
            return FindSingle(condition);
        }
                        
        /// <summary>
        /// 设置删除标志
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <param name="deleted">是否删除</param>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public bool SetDeletedFlag(object id, bool deleted = true, DbTransaction trans = null)
        {
            return ouDal.SetDeletedFlag(id, deleted, trans);
        }

        /// <summary>
        /// 根据机构的ID递归获取其公司的ID
        /// </summary>
        /// <param name="ouId"></param>
        /// <returns></returns>
        private OUInfo GetCompanyInfo(int ouId)
        {
            OUInfo info = BLLFactory<OU>.Instance.FindByID(ouId);
            if (info.OuType == 1)
            {
                return info;
            }
            else
            {
                return GetCompanyInfo(info.Pid);
            }
        }
    }
}