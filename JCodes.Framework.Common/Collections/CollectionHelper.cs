using JCodes.Framework.Common.Office;
using System;
using System.Collections.Generic;
using System.Text;

namespace JCodes.Framework.Common.Collections
{
    /// <summary>
    /// 把实体类树形结构的集合的名称进行缩进转化的辅助类
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    public class CollectionHelper<T> where T : class
    {
        /// <summary>
        /// 把实体类树形结构的集合，根据层次关系对名称进行空格缩进，方便显示（如下拉列表）
        /// </summary>
        /// <param name="pID">父节点</param>
        /// <param name="level">层次等级，从0开始</param>
        /// <param name="list">实体类列表</param>
        /// <param name="pidName">父节点名称</param>
        /// <param name="idName">ID名称</param>
        /// <param name="name">树节点名称</param>
        /// <returns></returns>
        public static List<T> Fill(int pID, int level, List<T> list, string pidName, string idName, string name)
        {
            List<T> returnList = new List<T>();
            foreach (T obj in list)
            {
                int typePID = (int)ReflectionUtil.GetProperty(obj, pidName);
                int typeID = (int)ReflectionUtil.GetProperty(obj, idName);
                string typeName = ReflectionUtil.GetProperty(obj, name) as string;

                if (pID == typePID)
                {
                    string newName = new string('　', level * 2) + typeName;
                    ReflectionUtil.SetProperty(obj, name, newName);
                    returnList.Add(obj);

                    returnList.AddRange(Fill(typeID, level + 1, list, pidName, idName, name));
                }
            }
            return returnList;
        }

        /// <summary>
        /// 把实体类树形结构的集合，根据层次关系对名称进行空格缩进，方便显示（如下拉列表）
        /// </summary>
        /// <param name="pID">父节点</param>
        /// <param name="level">层次等级，从0开始</param>
        /// <param name="list">实体类列表</param>
        /// <param name="pidName">父节点名称</param>
        /// <param name="idName">ID名称</param>
        /// <param name="name">树节点名称</param>
        /// <returns></returns>
        public static List<T> Fill(string pID, int level, List<T> list, string pidName, string idName, string name)
        {
            List<T> returnList = new List<T>();
            foreach (T obj in list)
            {
                string typePID = (string)ReflectionUtil.GetProperty(obj, pidName);
                string typeID = (string)ReflectionUtil.GetProperty(obj, idName);
                string typeName = ReflectionUtil.GetProperty(obj, name) as string;

                if (pID == typePID)
                {
                    string newName = new string('　', level * 2) + typeName;
                    ReflectionUtil.SetProperty(obj, name, newName);
                    returnList.Add(obj);

                    returnList.AddRange(Fill(typeID, level + 1, list, pidName, idName, name));
                }
            }
            return returnList;
        }
    }
}
