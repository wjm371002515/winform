using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;
using System.Data;
using JCodes.Framework.Common.Encrypt;
using JCodes.Framework.jCodesenum.BaseEnum;

namespace JCodes.Framework.Common.Files
{
     /// <summary>
    /// XML序列号、反序列化、节点等操作类辅助类
    /// </summary>
    public class XmlHelper
    {
        #region 变量
        /// <summary>
        /// XML文件路径
        /// </summary>
        protected string strXmlFile;
        /// <summary>
        /// XmlDocument对象
        /// </summary>
        protected XmlDocument objXmlDoc = new XmlDocument();
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="XmlFile">XML文件路径</param>
        public XmlHelper(string XmlFile)
        {
            try
            {
                objXmlDoc.Load(XmlFile);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(XmlHelper));
                throw ex;
            }
            strXmlFile = XmlFile;
        }

        #region 静态方法

        /// <summary>
        /// 二进制序列化
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static bool Serialize(string path, object obj)
        {
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    IFormatter format = new BinaryFormatter();

                    format.Serialize(stream, obj);
                    stream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(XmlHelper));
                return false;
            }
        }

        /// <summary>
        /// XML序列化
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="obj">对象实例</param>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static bool XmlSerialize(string path, object obj, Type type)
        {
            try
            {
                if (!File.Exists(path))
                {
                    FileInfo fi = new FileInfo(path);
                    if (!fi.Directory.Exists)
                    {
                        Directory.CreateDirectory(fi.Directory.FullName);
                    }
                }

                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    XmlSerializer format = new XmlSerializer(type);

                    format.Serialize(stream, obj, ns);
                    stream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(XmlHelper));
                return false;
            }
        }

        /// <summary>
        /// 二进制反序列化
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static object Deserialize(string path)
        {
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    IFormatter formatter = new BinaryFormatter();
                    stream.Seek(0, SeekOrigin.Begin);
                    object obj = formatter.Deserialize(stream);
                    stream.Close();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(XmlHelper));
                return null;
            }
        }

        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static object XmlDeserialize(string path, Type type)
        {
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlSerializer formatter = new XmlSerializer(type);
                    stream.Seek(0, SeekOrigin.Begin);
                    object obj = formatter.Deserialize(stream);
                    stream.Close();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(XmlHelper));
                return null;
            }
        }

        #endregion

        #region 公用方法

        /// <summary>
        /// 获取XmlDoc 对象
        /// </summary>
        /// <returns></returns>
        public XmlDocument GetXmlDoc()
        {
            return objXmlDoc;
        }

        /// <summary>
        /// 获取指定节点下面的XML子节点
        /// </summary>
        /// <param name="XmlPathNode">XML节点</param>
        /// <returns></returns>
        public XmlNodeList Read(string XmlPathNode)
        {
            try
            {
                XmlNode xn = objXmlDoc.SelectSingleNode(XmlPathNode);
                return xn.ChildNodes;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(XmlHelper));
                throw ex;
            }
        }

        /// <summary>
        /// 获取指定节点下面的XML内容
        /// </summary>
        /// <param name="XmlPathNode">XML节点</param>
        /// <returns></returns>
        public string ReadInnerXML(string XmlPathNode)
        {
            try
            {
                return objXmlDoc.SelectSingleNode(XmlPathNode).InnerXml;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(XmlHelper));
                throw ex;
            }
        }

        /// <summary>
        /// 读取节点属性内容
        /// </summary>
        /// <param name="XmlPathNode">XML节点</param>
        /// <param name="Attrib">节点属性</param>
        /// <returns></returns>
        public string Read(string XmlPathNode, string Attrib)
        {
            string value = "";
            try
            {
                XmlNode xn = objXmlDoc.SelectSingleNode(XmlPathNode);
                value = (Attrib.Equals("") ? xn.InnerText : xn.Attributes[Attrib].Value);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(XmlHelper));
                throw ex;
            }
            return value;
        }

        /// <summary>
        /// 获取元素节点对象
        /// </summary>
        /// <param name="XmlPathNode">XML节点</param>
        /// <param name="elementName">元素节点名称</param>
        /// <returns></returns>
        public XmlElement GetElement(string XmlPathNode, string elementName)
        {
            XmlElement result = null;

            XmlNode nls = objXmlDoc.SelectSingleNode(XmlPathNode);
            foreach (XmlNode xn1 in nls)//遍历
            {
                XmlElement xe2 = (XmlElement)xn1;//转换类型
                if (xe2.Name == elementName)//如果找到
                {
                    result = xe2;
                    break;//找到退出来就可以了
                }
            }

            return result;
        }

        /// <summary>
        /// 获取元素节点的值
        /// </summary>
        /// <param name="XmlPathNode">XML节点</param>
        /// <param name="elementName">元素节点名称</param>
        /// <returns></returns>
        public string GetElementData(string XmlPathNode, string elementName)
        {
            string result = null;

            XmlNode nls = objXmlDoc.SelectSingleNode(XmlPathNode);
            foreach (XmlNode xn1 in nls)//遍历
            {
                XmlElement xe2 = (XmlElement)xn1;//转换类型
                if (xe2.Name == elementName)//如果找到
                {
                    result = xe2.InnerText;
                    break;//找到退出来就可以了
                }
            }

            return result;
        }

        /// <summary>
        /// 获取节点下的DataSet
        /// </summary>
        /// <param name="XmlPathNode">XML节点</param>
        /// <returns></returns>
        public DataSet GetData(string XmlPathNode)
        {
            DataSet ds = new DataSet();
            StringReader read = new StringReader(objXmlDoc.SelectSingleNode(XmlPathNode).OuterXml);
            ds.ReadXml(read);
            return ds;
        }

        /// <summary>
        /// 替换某节点的内容
        /// </summary>
        /// <param name="XmlPathNode">XML节点</param>
        /// <param name="Content">节点内容</param>
        public void Replace(string XmlPathNode, string Content)
        {
            objXmlDoc.SelectSingleNode(XmlPathNode).InnerXml = Content;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="Node">节点</param>
        public void Delete(string Node)
        {
            string mainNode = Node.Substring(0, Node.LastIndexOf("/"));
            objXmlDoc.SelectSingleNode(mainNode).RemoveChild(objXmlDoc.SelectSingleNode(Node));
        }

        /// <summary>
        /// 获取指定节点下面的XML子节点
        /// </summary>
        /// <param name="XmlPathNode">XML节点</param>
        /// <returns></returns>
        public void DeleteByPathNode(string XmlPathNode)
        {
            try
            {
                XmlNode xn = objXmlDoc.SelectSingleNode(XmlPathNode);
                xn.ParentNode.RemoveChild(xn);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(XmlHelper));
                throw ex;
            }
        }

        /// <summary>
        /// 插入一节点和此节点的一子节点
        /// </summary>
        /// <param name="MainNode"></param>
        /// <param name="ChildNode"></param>
        /// <param name="Element"></param>
        /// <param name="Content"></param>
        public void InsertNode(string MainNode, string ChildNode, string Element, string Content)
        {
            XmlNode objRootNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objChildNode = objXmlDoc.CreateElement(ChildNode);
            objRootNode.AppendChild(objChildNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.InnerText = Content;
            objChildNode.AppendChild(objElement);
        }

        /// <summary>
        /// 插入一个节点带一个属性 
        /// </summary>
        /// <param name="MainNode">指定的XML节点</param>
        /// <param name="Element">元素名称</param>
        /// <param name="Attrib">属性名称</param>
        /// <param name="AttribContent">属性值</param>
        /// <param name="Content">内容</param>
        public void InsertElement(string MainNode, string Element, string Attrib, string AttribContent, string Content)
        {
            XmlNode objNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.SetAttribute(Attrib, AttribContent);
            objElement.InnerXml = Content;
            objNode.AppendChild(objElement);
        }

        public void InsertElement(string elementName, string Attrib, string AttribContent, string innerXML, XmlNode refChild)
        {
            XmlNode root = objXmlDoc.DocumentElement;
            //Add the node to the document.
            XmlNode nodeA = objXmlDoc.CreateElement(elementName);
            ((XmlElement)nodeA).SetAttribute(Attrib, AttribContent);
            nodeA.InnerXml = innerXML;
            root.InsertBefore(nodeA, refChild);
        }

        /// <summary>
        /// 插入XML元素
        /// </summary>
        /// <param name="MainNode">指定的XML节点</param>
        /// <param name="Element">元素名称</param>
        /// <param name="Content">内容</param>
        public void InsertElement(string MainNode, string Element, string Content)
        {
            XmlNode objNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.InnerXml = Content;
            objNode.AppendChild(objElement);
        }

        /// <summary>
        /// 保存XML文档
        /// </summary>
        public void Save(bool isSave = true)
        {
            try
            {
                objXmlDoc.Save(strXmlFile);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(XmlHelper));
                throw ex;
            }
            if (isSave)
                objXmlDoc = null;
        }

        /// <summary>
        /// XML序列化并对文件进行加密
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="obj">对象实例</param>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public bool XmlSerializeEncrypt(string path, object obj, Type type)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            try
            {
                if (!File.Exists(path))
                {
                    FileInfo fi = new FileInfo(path);
                    if (!fi.Directory.Exists)
                    {
                        Directory.CreateDirectory(fi.Directory.FullName);
                    }
                }

                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    string content = "";
                    using (MemoryStream ms = new MemoryStream())
                    {
                        XmlSerializer format = new XmlSerializer(type);
                        format.Serialize(ms, obj, ns);
                        ms.Seek(0, 0);
                        content = Encoding.ASCII.GetString(ms.ToArray());
                    }

                    string encrypt = EncodeHelper.EncryptString(content);
                    byte[] bytes = UTF8Encoding.Default.GetBytes(encrypt);
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }


                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(XmlHelper));
                return false;
            }
        }

        /// <summary>
        /// XML反序列化并解密
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public object XmlDeserializeDecrypt(string path, Type type)
        {
            try
            {
                string encrypt = File.ReadAllText(path, Encoding.UTF8);
                string content = EncodeHelper.DecryptString(encrypt, true);

                byte[] bytes = UTF8Encoding.Default.GetBytes(content);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    XmlSerializer formatter = new XmlSerializer(type);

                    stream.Seek(0, SeekOrigin.Begin);
                    object obj = formatter.Deserialize(stream);
                    stream.Close();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(XmlHelper));
                return null;
            }
        }

        #endregion
    }
}
