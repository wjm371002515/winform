using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.Common;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.IDAL;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.jCodesenum;

namespace JCodes.Framework.BLL
{
    /// <summary>
    /// 部门机构信息
    /// </summary>
    public class OU : BaseBLL<OUInfo>
    {
        private IOU dal = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public OU() : base()
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
            
            dal = baseDal as IOU;
        }

        /// <summary>
        /// 重载只是显示未被删除的记录
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <returns></returns>
        public override List<OUInfo> GetAll(DbTransaction trans = null)
        {
            string condition = string.Format(" IsDelete = {0}", (short)IsDelete.否);
            return base.Find(condition, trans);
        }

        /// <summary>
        /// 获取顶部的集团信息
        /// </summary>
        /// <returns></returns>
        public List<OUInfo> GetTopGroup()
        {
            string condition = string.Format("Pid = -1 ");
            return Find(condition);
        }
               
        /// <summary>
        /// 根据当前用户身份，获取对应的顶级机构管理节点。
        /// 是公司管理员，返回其公司节点
        /// </summary>
        /// <returns></returns>
        public List<OUInfo> GetMyTopGroup(Int32 userId)
        {
            List<OUInfo> list = new List<OUInfo>();

            UserInfo userInfo = BLLFactory<User>.Instance.FindById(userId);
            if (userInfo != null)
            {
                OUInfo groupInfo = this.FindById(userInfo.CompanyId);//公司管理员取公司节点
                list.Add(groupInfo);
            }
            return list;
        }

        /// <summary>
        /// 根据当前用户身份，获取对应的顶级机构管理节点。
        /// 超级管理员，返回集团节点；
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        public List<OUInfo> GetSuperAdminTopGroup(Int32 userId) {
            List<OUInfo> list = new List<OUInfo>();

            UserInfo userInfo = BLLFactory<User>.Instance.FindById(userId);
            if (userInfo != null)
            {
                //超级管理员取集团节点
                list.AddRange(GetTopGroup());
            }
            return list;
        }
        
        /// <summary>
        /// 获取部门分类为公司的列表【Category='公司'】
        /// </summary>
        /// <returns></returns>
        public List<OUInfo> GetAllCompany(Int32 groupId)
        {
            string condition = string.Format("OuType={1} AND Pid={0} ", groupId, (short)OuType.公司);
            return Find(condition);
        }

         /// <summary>
        /// 获取集团和公司的列表
        /// </summary>
        /// <returns></returns>
        public List<OUInfo> GetGroupCompany()
        {
            string condition = string.Format("OuType={0} or OuType={1} ", (short)OuType.公司, (short)OuType.集团);
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
        /// <param name="ouId">机构Id</param>
        /// <param name="newUserList">人员列表</param>
        /// <returns></returns>
        public bool EditOuUsers(Int32 ouId, List<Int32> newUserList)
        {
            return dal.EditOuUsers(ouId, newUserList);
        }

        /// <summary>
        /// 为机构添加相关用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="ouId">机构ID</param>
        public void AddUser(Int32 userId, Int32 ouId)
        {
            dal.AddUser(ouId, ouId);
        }

        /// <summary>
        /// 根据角色ID获取对应的机构列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public List<OUInfo> GetOUsByRoleId(Int32 roleId)
        {
            return dal.GetOUsByRoleId(roleId);
        }

        /// <summary>
        /// 获取指定用户的机构列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public List<OUInfo> GetOUsByUserId(Int32 userId)
        {
            return dal.GetOUsByUser(userId);
        }

        /// <summary>
        /// 在机构中移除指定的用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="ouId">机构ID</param>
        public void RemoveUser(Int32 userId, Int32 ouId)
        {
            dal.RemoveUser(userId, ouId);
        }
                        
        /// <summary>
        /// 根据指定机构节点ID，获取其下面所有机构列表
        /// </summary>
        /// <param name="parentId">指定机构节点ID</param>
        /// <returns></returns>
        public List<OUInfo> GetAllOUsByParent(Int32 parentId)
        {
            return dal.GetAllOUsByParent(parentId);
        }

        /// <summary>
        /// 获取树形结构的机构列表
        /// </summary>
        public List<OUNodeInfo> GetTree()
        {
            return dal.GetTree();
        }

        /// <summary>
        /// 获取指定机构下面的树形列表
        /// </summary>
        /// <param name="mainOUID">指定机构ID</param>
        public List<OUNodeInfo> GetTreeById(Int32 mainOUId)
        {
            return dal.GetTreeById(mainOUId);
        }

        /// <summary>
        /// 获取机构的名称
        /// </summary>
        /// <param name="id">机构ID</param>
        /// <returns></returns>
        public string GetName(Int32 id, DbTransaction trans = null)
        {
            return dal.GetName(id, trans);
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
        public bool SetDeletedFlag(Int32 Id, DbTransaction trans = null)
        {
            return dal.SetDeletedFlag(Id, trans);
        }

        /// <summary>
        /// 根据机构的ID递归获取其公司的ID
        /// </summary>
        /// <param name="ouId"></param>
        /// <returns></returns>
        private OUInfo GetCompanyInfo(Int32 ouId)
        {
            OUInfo info = BLLFactory<OU>.Instance.FindById(ouId);
            if (info.OuType == (short)OuType.公司)
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