using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 定义zTree的相关数据，方便控制器生成Json数据进行传递
    /// </summary>
    [DataContract]
    [Serializable]
    public class TreeData
    {
        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public string id { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        [DataMember]
        public string pid { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [DataMember]
        public string name { get; set; }

        /// <summary>
        /// 节点图标
        /// </summary>
        [DataMember]
        public string icon { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        [DataMember]
        public List<TreeData> children { get; set; }
        
        /// <summary>
        /// 是否展开
        /// </summary>
        [DataMember]
        public bool open  { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TreeData() 
        {
            this.children = new List<TreeData>();
            this.open = true;
        }

        /// <summary>
        /// 常用构造函数
        /// </summary>
        public TreeData(string id, string pid, string name, string icon = "") : this()
        {
            this.id = id;
            this.pid = pid;
            this.name = name;
            this.icon = icon;
        }

        /// <summary>
        /// 常用构造函数
        /// </summary>
        public TreeData(int id, int pid, string name, string icon = "") : this()
        {
            this.id = id.ToString();
            this.pid = pid.ToString();
            this.name = name;
            this.icon = icon;
        }
    }
}
