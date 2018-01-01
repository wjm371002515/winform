using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{

    /// <summary>
    /// JsTree的数据模型
    /// </summary>
    [Serializable]
    public class JsTreeData
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public JsTreeData()
        {
            this.state = new JsTreeState();
            this.children = new List<JsTreeData>();
        }

        /// <summary>
        /// 参数化构造函数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="text">节点显示名称</param>
        /// <param name="icon">显示图标</param>
        /// <param name="parent">父节点ID，默认为#</param>
        public JsTreeData(object id, string text, string icon = null)
            : this()
        {
            this.id = id.ToString();
            this.text = text;
            this.icon = icon;
        }

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public string id { get; set; }

        /// <summary>
        /// 节点显示名称
        /// </summary>
        [DataMember]
        public string text { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string icon { get; set; }

        /// <summary>
        /// 状态对象
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public JsTreeState state { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<JsTreeData> children { get; set; }
    }


    /// <summary>
    /// JsTree的数据模型(datatable类型)
    /// </summary>
    [Serializable]
    public class JsTreeTable
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public JsTreeTable()
        {
            this.parent = "#"; //#表示为根节点
            this.state = new JsTreeState();
        }

        /// <summary>
        /// 参数化构造函数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="text">节点显示名称</param>
        /// <param name="icon">显示图标</param>
        /// <param name="parent">父节点ID，默认为#</param>
        public JsTreeTable(string id, string text, string icon = null, string parent = "#")
        {
            this.id = id;
            this.text = text;
            this.icon = icon;

            this.parent = parent;
            this.state = new JsTreeState();
        }

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public string id { get; set; }

        /// <summary>
        /// parent
        /// </summary>
        [DataMember]
        public string parent { get; set; }

        /// <summary>
        /// 节点显示名称
        /// </summary>
        [DataMember]
        public string text { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [DataMember]
        public string icon { get; set; }

        /// <summary>
        /// 状态对象
        /// </summary>
        [DataMember]
        public JsTreeState state { get; set; }
    }

    /// <summary>
    ///  JsTree的状态对象
    /// </summary>
    public class JsTreeState
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public JsTreeState()
        {
            this.opened = true;
            this.selected = false;
            this.disabled = false;
        }

        /// <summary>
        /// 参数化构造函数
        /// </summary>
        /// <param name="opened">是否打开</param>
        /// <param name="selected">是否选中</param>
        /// <param name="disable">是否禁用</param>
        public JsTreeState(bool opened = true, bool selected = false, bool disable = false)
        {
            this.opened = opened;
            this.selected = selected;
            this.disabled = disabled;
        }

        /// <summary>
        /// 是否打开
        /// </summary>
        [DataMember]
        [DefaultValue(true)]
        public bool opened { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        [DataMember]
        [DefaultValue(false)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool selected { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        [DataMember]
        [DefaultValue(false)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool disabled { get; set; }
    }
}