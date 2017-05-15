using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// AD(Active Directory 动态目录）操作辅助类
    /// </summary>
    public class ADHelper : IDisposable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Query">查询条件</param>
        /// <param name="UserName">登录用户名</param>
        /// <param name="Password">登录密码</param>
        /// <param name="Path">LDAP服务路径</param>
        public ADHelper(string Query, string UserName, string Password, string Path)
        {
            Entry = new DirectoryEntry(Path, UserName, Password, AuthenticationTypes.Secure);
            this.Path = Path;
            this.UserName = UserName;
            this.Password = Password;
            this.Query = Query;
            Searcher = new DirectorySearcher(Entry);
            Searcher.Filter = Query;
            Searcher.PageSize = 1000;
        }

        #region Public Functions

        /// <summary>
        /// 检查是否用户被授权了
        /// </summary>
        /// <returns>如果被授权返回True，否则False</returns>
        public virtual bool Authenticate()
        {
            try
            {
                if (!Entry.Guid.ToString().ToLower().Trim().Equals(""))
                    return true;
            }
            catch { }
            return false;
        }

        /// <summary>
        /// 关闭目录
        /// </summary>
        public virtual void Close()
        {
            Entry.Close();
        }
        
        /// <summary>
        /// 返回一个组的成员列表
        /// </summary>
        /// <param name="GroupName">组名称</param>
        /// <returns>成员列表</returns>
        public virtual List<Entry> FindActiveGroupMembers(string GroupName)
        {
            try
            {
                List<Entry> Entries = this.FindGroups("cn=" + GroupName);
                return (Entries.Count < 1) ? new List<Entry>() : this.FindActiveUsersAndGroups("memberOf=" + Entries[0].DistinguishedName);
            }
            catch
            {
                return new List<Entry>();
            }
        }

        /// <summary>
        /// 查询所有活动组
        /// </summary>
        /// <param name="Filter">Filter used to modify the query</param>
        /// <param name="args">Additional arguments (used in string formatting</param>
        /// <returns>A list of all active groups' entries</returns>
        public virtual List<Entry> FindActiveGroups(string Filter, params object[] args)
        {
            Filter = string.Format(Filter, args);
            Filter = string.Format("(&((userAccountControl:1.2.840.113556.1.4.803:=512)(!(userAccountControl:1.2.840.113556.1.4.803:=2))(!(cn=*$)))({0}))", Filter);
            return FindGroups(Filter);
        }

        /// <summary>
        /// 查询所有活动用户
        /// </summary>
        /// <param name="Filter">Filter used to modify the query</param>
        /// <param name="args">Additional arguments (used in string formatting</param>
        /// <returns>A list of all active users' entries</returns>
        public virtual List<Entry> FindActiveUsers(string Filter, params object[] args)
        {
            Filter = string.Format(Filter, args);
            Filter = string.Format("(&((userAccountControl:1.2.840.113556.1.4.803:=512)(!(userAccountControl:1.2.840.113556.1.4.803:=2))(!(cn=*$)))({0}))", Filter);
            return FindUsers(Filter);
        }

        /// <summary>
        /// 查询所有活动用户和组
        /// </summary>
        /// <param name="Filter">Filter used to modify the query</param>
        /// <param name="args">Additional arguments (used in string formatting</param>
        /// <returns>A list of all active groups' entries</returns>
        public virtual List<Entry> FindActiveUsersAndGroups(string Filter, params object[] args)
        {
            Filter = string.Format(Filter, args);
            Filter = string.Format("(&((userAccountControl:1.2.840.113556.1.4.803:=512)(!(userAccountControl:1.2.840.113556.1.4.803:=2))(!(cn=*$)))({0}))", Filter);
            return FindUsersAndGroups(Filter);
        }

        /// <summary>
        /// 查找所有符合查询条件的节点对象
        /// </summary>
        /// <returns>A list of all entries that match the query</returns>
        public virtual List<Entry> FindAll()
        {
            List<Entry> ReturnedResults = new List<Entry>();
            using (SearchResultCollection Results = Searcher.FindAll())
            {
                foreach (SearchResult Result in Results)
                    ReturnedResults.Add(new Entry(Result.GetDirectoryEntry()));
            }
            return ReturnedResults;
        }

        /// <summary>
        /// 查找所有计算机
        /// </summary>
        /// <param name="Filter">Filter used to modify the query</param>
        /// <param name="args">Additional arguments (used in string formatting</param>
        /// <returns>A list of all computers meeting the specified Filter</returns>
        public virtual List<Entry> FindComputers(string Filter, params object[] args)
        {
            Filter = string.Format(Filter, args);
            Filter = string.Format("(&(objectClass=computer)({0}))", Filter);
            Searcher.Filter = Filter;
            return FindAll();
        }

        /// <summary>
        /// 查询所有组
        /// </summary>
        /// <param name="Filter">Filter used to modify the query</param>
        /// <param name="args">Additional arguments (used in string formatting</param>
        /// <returns>A list of all groups meeting the specified Filter</returns>
        public virtual List<Entry> FindGroups(string Filter, params object[] args)
        {
            Filter = string.Format(Filter, args);
            Filter = string.Format("(&(objectClass=Group)(objectCategory=Group)({0}))", Filter);
            Searcher.Filter = Filter;
            return FindAll();
        }

        /// <summary>
        /// 查询所有符合条件的单个实体
        /// </summary>
        /// <returns>A single entry matching the query</returns>
        public virtual Entry FindOne()
        {
            return new Entry(Searcher.FindOne().GetDirectoryEntry());
        }

        /// <summary>
        /// 查询所有用户和组
        /// </summary>
        /// <param name="Filter">Filter used to modify the query</param>
        /// <param name="args">Additional arguments (used in string formatting</param>
        /// <returns>A list of all users and groups meeting the specified Filter</returns>
        public virtual List<Entry> FindUsersAndGroups(string Filter, params object[] args)
        {
            Filter = string.Format(Filter, args);
            Filter = string.Format("(&(|(&(objectClass=Group)(objectCategory=Group))(&(objectClass=User)(objectCategory=Person)))({0}))", Filter);
            Searcher.Filter = Filter;
            return FindAll();
        }

        /// <summary>
        /// 通过用户名称查询用户实体
        /// </summary>
        /// <param name="UserName">User name to search by</param>
        /// <returns>The user's entry</returns>
        public virtual Entry FindUserByUserName(string UserName)
        {
            if (string.IsNullOrEmpty(UserName))
                throw new ArgumentNullException("UserName");

            List<Entry> list = FindUsers("samAccountName=" + UserName);
            if (list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <param name="Filter">Filter used to modify the query</param>
        /// <param name="args">Additional arguments (used in string formatting</param>
        /// <returns>A list of all users meeting the specified Filter</returns>
        public virtual List<Entry> FindUsers(string Filter, params object[] args)
        {
            Filter = string.Format(Filter, args);
            Filter = string.Format("(&(objectClass=User)(objectCategory=Person)({0}))", Filter);
            Searcher.Filter = Filter;
            return FindAll();
        }

        #endregion

        #region Properties
        /// <summary>
        /// AD服务路径
        /// </summary>
        public virtual string Path
        {
            get { return _Path; }
            set
            {
                _Path = value;
                if (Entry != null)
                {
                    Entry.Close();
                    Entry.Dispose();
                    Entry = null;
                }
                if (Searcher != null)
                {
                    Searcher.Dispose();
                    Searcher = null;
                }
                Entry = new DirectoryEntry(_Path, _UserName, _Password, AuthenticationTypes.Secure);
                Searcher = new DirectorySearcher(Entry);
                Searcher.Filter = Query;
                Searcher.PageSize = 1000;
            }
        }

        /// <summary>
        /// 用户登录名称
        /// </summary>
        public virtual string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                if (Entry != null)
                {
                    Entry.Close();
                    Entry.Dispose();
                    Entry = null;
                }
                if (Searcher != null)
                {
                    Searcher.Dispose();
                    Searcher = null;
                }
                Entry = new DirectoryEntry(_Path, _UserName, _Password, AuthenticationTypes.Secure);
                Searcher = new DirectorySearcher(Entry);
                Searcher.Filter = Query;
                Searcher.PageSize = 1000;
            }
        }

        /// <summary>
        /// 用户登录密码
        /// </summary>
        public virtual string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                if (Entry != null)
                {
                    Entry.Close();
                    Entry.Dispose();
                    Entry = null;
                }
                if (Searcher != null)
                {
                    Searcher.Dispose();
                    Searcher = null;
                }
                Entry = new DirectoryEntry(_Path, _UserName, _Password, AuthenticationTypes.Secure);
                Searcher = new DirectorySearcher(Entry);
                Searcher.Filter = Query;
                Searcher.PageSize = 1000;
            }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public virtual string Query
        {
            get { return _Query; }
            set
            {
                _Query = value;
                Searcher.Filter = _Query;
            }
        }

        /// <summary>
        /// 排序条件
        /// </summary>
        public virtual string SortBy
        {
            get { return _SortBy; }
            set
            {
                _SortBy = value;
                Searcher.Sort.PropertyName = _SortBy;
                Searcher.Sort.Direction = SortDirection.Ascending;
            }
        }

        #endregion

        #region Private Variables

        private string _Path = "";
        private string _UserName = "";
        private string _Password = "";
        private DirectoryEntry Entry = null;
        private string _Query = "";
        private DirectorySearcher Searcher = null;
        private string _SortBy = "";

        #endregion

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (Entry != null)
            {
                Entry.Close();
                Entry.Dispose();
                Entry = null;
            }
            if (Searcher != null)
            {
                Searcher.Dispose();
                Searcher = null;
            }
        }
    }

    /// <summary>
    /// 目录节点（实体）对象
    /// </summary>
    public class Entry : IDisposable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DirectoryEntry">目录节点（实体）对象</param>
        public Entry(DirectoryEntry DirectoryEntry)
        {
            this.DirectoryEntry = DirectoryEntry;
        }

        #region Properties
        /// <summary>
        /// Actual base directory entry
        /// </summary>
        public virtual DirectoryEntry DirectoryEntry { get; set; }

        /// <summary>
        /// Email属性
        /// </summary>
        public virtual string Email
        {
            get { return (string)GetValue("mail"); }
            set { SetValue("mail", value); }
        }

        /// <summary>
        /// 节点唯一性名称
        /// </summary>
        public virtual string DistinguishedName
        {
            get { return (string)GetValue("distinguishedname"); }
            set { SetValue("distinguishedname", value); }
        }

        /// <summary>
        /// 节点country code属性
        /// </summary>
        public virtual string CountryCode
        {
            get { return (string)GetValue("countrycode"); }
            set { SetValue("countrycode", value); }
        }

        /// <summary>
        /// 节点company 属性
        /// </summary>
        public virtual string Company
        {
            get { return (string)GetValue("company"); }
            set { SetValue("company", value); }
        }

        /// <summary>
        /// 节点MemberOf 属性
        /// </summary>
        public virtual List<string> MemberOf
        {
            get
            {
                List<string> Values = new List<string>();
                PropertyValueCollection Collection = DirectoryEntry.Properties["memberof"];
                foreach (object Item in Collection)
                {
                    Values.Add((string)Item);
                }
                return Values;
            }
        }

        /// <summary>
        /// 节点的显示名称DisplayName
        /// </summary>
        public virtual string DisplayName
        {
            get { return (string)GetValue("displayname"); }
            set { SetValue("displayname", value); }
        }

        /// <summary>
        /// 节点initials 属性
        /// </summary>
        public virtual string Initials
        {
            get { return (string)GetValue("initials"); }
            set { SetValue("initials", value); }
        }

        /// <summary>
        /// 节点title属性
        /// </summary>
        public virtual string Title
        {
            get { return (string)GetValue("title"); }
            set { SetValue("title", value); }
        }

        /// <summary>
        /// 节点属性samaccountname
        /// </summary>
        public virtual string SamAccountName
        {
            get { return (string)GetValue("samaccountname"); }
            set { SetValue("samaccountname", value); }
        }

        /// <summary>
        /// 节点属性givenname 
        /// </summary>
        public virtual string GivenName
        {
            get { return (string)GetValue("givenname"); }
            set { SetValue("givenname", value); }
        }

        /// <summary>
        /// 节点属性cn
        /// </summary>
        public virtual string CN
        {
            get { return (string)GetValue("cn"); }
            set { SetValue("cn", value); }
        }

        /// <summary>
        /// 节点属性name 
        /// </summary>
        public virtual string Name
        {
            get { return (string)GetValue("name"); }
            set { SetValue("name", value); }
        }

        /// <summary>
        /// 节点属性office
        /// </summary>
        public virtual string Office
        {
            get { return (string)GetValue("physicaldeliveryofficename"); }
            set { SetValue("physicaldeliveryofficename", value); }
        }

        /// <summary>
        /// 节点电话号码属性telephone number
        /// </summary>
        public virtual string TelephoneNumber
        {
            get { return (string)GetValue("telephonenumber"); }
            set { SetValue("telephonenumber", value); }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// 保存修改
        /// </summary>
        public virtual void Save()
        {
            if (DirectoryEntry == null)
                throw new NullReferenceException("DirectoryEntry shouldn't be null");
            DirectoryEntry.CommitChanges();
        }

        /// <summary>
        /// 获取实体的指定属性值
        /// </summary>
        /// <param name="Property">Property you want the information about</param>
        /// <returns>an object containing the property's information</returns>
        public virtual object GetValue(string Property)
        {
            PropertyValueCollection Collection = DirectoryEntry.Properties[Property];
            return Collection != null ? Collection.Value : null;
        }

        /// <summary>
        /// 获取实体的属性值
        /// </summary>
        /// <param name="Property">Property you want the information about</param>
        /// <param name="Index">Index of the property to return</param>
        /// <returns>an object containing the property's information</returns>
        public virtual object GetValue(string Property, int Index)
        {
            PropertyValueCollection Collection = DirectoryEntry.Properties[Property];
            return Collection != null ? Collection[Index] : null;
        }

        /// <summary>
        /// 保存实体节点的属性值
        /// </summary>
        /// <param name="Property">Property of the entry to set</param>
        /// <param name="Value">Value to set the property to</param>
        public virtual void SetValue(string Property, object Value)
        {
            PropertyValueCollection Collection = DirectoryEntry.Properties[Property];
            if (Collection != null)
                Collection.Value = Value;
        }

        /// <summary>
        /// 保存实体节点的属性值
        /// </summary>
        /// <param name="Property">Property of the entry to set</param>
        /// <param name="Index">Index of the property to set</param>
        /// <param name="Value">Value to set the property to</param>
        public virtual void SetValue(string Property, int Index, object Value)
        {
            PropertyValueCollection Collection = DirectoryEntry.Properties[Property];
            if (Collection != null)
                Collection[Index] = Value;
        }

        #endregion

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (DirectoryEntry != null)
            {
                DirectoryEntry.Dispose();
                DirectoryEntry = null;
            }
        }
    }

}
