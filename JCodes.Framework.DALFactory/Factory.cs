using System;
using System.Collections.Generic;
using System.Text;
using JCodes.Framework.Data.Model;
using JCodes.Framework.Data.IDAL;

namespace JCodes.Framework.Data.DALFactory
{
    /// <summary>
    /// 工厂类：创建访问数据库的实例对象
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// 根据传入的类名获取实例对象
        /// </summary>
        private static object GetInstance(string name)
        {
            //ILog log = LogManager.GetLogger(typeof(Factory));  //初始化日志记录器

            string configName = System.Configuration.ConfigurationManager.AppSettings["DataAccess"];
            if (string.IsNullOrEmpty(configName))
            {
                //log.Fatal("没有从配置文件中获取命名空间名称！");   //Fatal致命错误，优先级最高
                throw new InvalidOperationException();    //抛错，代码不会向下执行了
            }

            string className = string.Format("{0}.{1}", configName, name); 

            //加载程序集
            System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(configName);
            //创建指定类型的对象实例
            return assembly.CreateInstance(className);
        }

        /// <summary>
        /// 利用反射获取访问登录信息的数据访问对象（结合配置文件app.config）
        /// </summary>
        public static IAuthority GetAuthorityDAL()
        {
            IAuthority authority = GetInstance("Authority") as IAuthority;
            return authority;
        }

        public static IBug GetBugDAL()
        {
            IBug bug = GetInstance("Bug") as IBug;
            return bug;
        }

        public static IButton GetButtonDAL()
        {
            IButton button = GetInstance("Button") as IButton;
            return button;
        }
        public static IDepartment GetDepartmentDAL()
        {
            IDepartment department = GetInstance("Department") as IDepartment;
            return department;
        }

        public static ILoginLog GetLoginInfoDAL()
        {
            ILoginLog loginInfo = GetInstance("LoginLog") as ILoginLog;
            return loginInfo;
        }

        public static IMenu GetMenuDAL()
        {
            IMenu menu = GetInstance("Menu") as IMenu;
            return menu;
        }

        public static IRole GetRoleDAL()
        {
            IRole role = GetInstance("Role") as IRole;
            return role;
        }

        public static IRoleMenuButton GetRoleMenuButtonDAL()
        {
            IRoleMenuButton roleMenuButton = GetInstance("RoleMenuButton") as IRoleMenuButton;
            return roleMenuButton;
        }

        public static IUser GetUserDAL()
        {
            IUser user = GetInstance("User") as IUser;
            return user;
        }

        public static IDAL.IUserDepartment GetUserDepartmentDAL()
        {
            IDAL.IUserDepartment userDepartment = GetInstance("UserDepartment") as IDAL.IUserDepartment;
            return userDepartment;
        }

        public static IDAL.IUserOperateLog GetUserOperateLogDAL()
        {
            IDAL.IUserOperateLog userOperateLog = GetInstance("UserOperateLog") as IDAL.IUserOperateLog;
            return userOperateLog;
        }

        public static IDAL.IUserRole GetUserRoleDAL()
        {
            IDAL.IUserRole userRole = GetInstance("UserRole") as IDAL.IUserRole;
            return userRole;
        }

        public static IDAL.IOrder GetOrderDAL()
        {
            IDAL.IOrder order = GetInstance("Order") as IDAL.IOrder;
            return order;
        }

        public static IDAL.IDaily GetDailyDAL()
        {
            IDAL.IDaily daily = GetInstance("Daily") as IDaily;
            return daily;
            ;
        }
    }
}
