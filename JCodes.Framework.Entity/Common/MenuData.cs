using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    /// <summary>
    /// 定义菜单Json的相关数据，方便控制器生成Json数据进行传递
    /// </summary>
    [DataContract]
    [Serializable]
    public class MenuData
    {
        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public string menuid { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [DataMember]
        public string menuname { get; set; }
                  
        /// <summary>
        /// 图标样式
        /// </summary>
        [DataMember]
        public string icon { get; set; }
      
        /// <summary>
        /// url地址
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string url  { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MenuData> menus { get; set; }
        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MenuData() 
        {
            this.menus = new List<MenuData>();
            this.icon = "icon-view";
        }

        /// <summary>
        /// 常用构造函数
        /// </summary>
        public MenuData(string menuid, string menuname, string icon = "icon-view", string url=null)
            : this()
        {
            this.menuid = menuid;
            this.menuname = menuname;
            this.icon = icon;
            this.url = url;
        }

        /// <summary>
        /// 常用构造函数
        /// </summary>
        public MenuData(int menuid, string menuname, string icon = "icon-view", string url = null)
            : this()
        {
            this.menuid = menuid.ToString();
            this.menuname = menuname;
            this.icon = icon;
            this.url = url;
        }
    }
}
