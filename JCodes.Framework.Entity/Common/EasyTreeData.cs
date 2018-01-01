using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 定义EasyUI树的相关数据，方便控制器生成Json数据进行传递
    /// </summary>
    [DataContract]
    [Serializable]
    public class EasyTreeData
    {
        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public string id { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [DataMember]
        public string text { get; set; }
        
        /// <summary>
        /// 是否展开
        /// </summary>
        [DataMember]
        public string state  { get; set; }

        /// <summary>
        /// 图标样式
        /// </summary>
        [DataMember]
        public string iconCls { get; set; }

        [JsonProperty(PropertyName = "checked", NullValueHandling = NullValueHandling.Ignore)]
        [DataMember(Name="checked")]
        public bool? Checked { get; set; }


        /// <summary>
        /// 子节点集合
        /// </summary>
        [DataMember]
        public List<EasyTreeData> children { get; set; }
        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public EasyTreeData() 
        {
            this.children = new List<EasyTreeData>();
            this.state = "open";
        }

        /// <summary>
        /// 常用构造函数
        /// </summary>
        public EasyTreeData(string id, string text, string iconCls = "", string state = "open")
            : this()
        {
            this.id = id;
            this.text = text;
            this.state = state;
            this.iconCls = iconCls;
        }

        /// <summary>
        /// 常用构造函数
        /// </summary>
        public EasyTreeData(int id, string text, string iconCls = "", string state = "open")
            : this()
        {
            this.id = id.ToString();
            this.text = text;
            this.state = state;
            this.iconCls = iconCls;
        }
    } 

}
