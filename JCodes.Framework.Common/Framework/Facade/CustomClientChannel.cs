using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.Reflection;

namespace JCodes.Framework.Common.Framework.Facade
{
    /// <summary>
    /// 自定义的客户端信道(允许从自定义的配置文件中加载)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomClientChannel<T> : ChannelFactory<T>
    {
        private string _configurationPath;
        private string _endpointConfigurationName;
        private TimeSpan? _timeout = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configurationPath">自定义配置文件路径</param>
        public CustomClientChannel(string configurationPath) : base(typeof(T))
        {
            this._configurationPath = configurationPath;
            base.InitializeEndpoint((string)null, null);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configurationPath">自定义配置文件路径</param>
        /// <param name="timeout">超时时间</param>
        public CustomClientChannel(string configurationPath, TimeSpan timeout) : base(typeof(T))
        {
            this._timeout = timeout;
            this._configurationPath = configurationPath;
            base.InitializeEndpoint((string)null, null);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="configurationPath">自定义配置文件路径</param>
        public CustomClientChannel(Binding binding, string configurationPath) : this(binding, (EndpointAddress)null, configurationPath)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceEndpoint"></param>
        /// <param name="configurationPath">自定义配置文件路径</param>
        public CustomClientChannel(ServiceEndpoint serviceEndpoint, string configurationPath) : base(typeof(T))
        {
            this._configurationPath = configurationPath;
            base.InitializeEndpoint(serviceEndpoint);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="endpointConfigurationName"></param>
        /// <param name="configurationPath">自定义配置文件路径</param>
        public CustomClientChannel(string endpointConfigurationName, string configurationPath) : this(endpointConfigurationName, null, configurationPath)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="endpointAddress"></param>
        /// <param name="configurationPath">自定义配置文件路径</param>
        public CustomClientChannel(Binding binding, EndpointAddress endpointAddress, string configurationPath) : base(typeof(T))
        {
            this._configurationPath = configurationPath;
            base.InitializeEndpoint(binding, endpointAddress);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="remoteAddress"></param>
        /// <param name="configurationPath">自定义配置文件路径</param>
        public CustomClientChannel(Binding binding, string remoteAddress, string configurationPath)  : this(binding, new EndpointAddress(remoteAddress), configurationPath)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="endpointConfigurationName"></param>
        /// <param name="endpointAddress"></param>
        /// <param name="configurationPath">自定义配置文件路径</param>
        public CustomClientChannel(string endpointConfigurationName, EndpointAddress endpointAddress, string configurationPath) : base(typeof(T))
        {
            this._configurationPath = configurationPath;
            this._endpointConfigurationName = endpointConfigurationName;
            base.InitializeEndpoint(endpointConfigurationName, endpointAddress);
        }

        /// <summary>
        /// 从指定的配置文件中加载服务终结点
        /// </summary>
        /// <returns></returns>
        protected override ServiceEndpoint CreateDescription()
        {
            ServiceEndpoint serviceEndpoint = base.CreateDescription();

            if (_endpointConfigurationName != null)
                serviceEndpoint.Name = _endpointConfigurationName;

            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = this._configurationPath;

            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            ServiceModelSectionGroup group = ServiceModelSectionGroup.GetSectionGroup(config);

            ChannelEndpointElement selectedEndpoint = null;
            foreach (ChannelEndpointElement endpoint in group.Client.Endpoints)
            {
                if (endpoint.Contract == serviceEndpoint.Contract.ConfigurationName &&
                    (this._endpointConfigurationName == null || this._endpointConfigurationName == endpoint.Name))
                {
                    selectedEndpoint = endpoint;
                    break;
                }
            }

            if (selectedEndpoint != null)
            {
                if (serviceEndpoint.Binding == null)
                {
                    serviceEndpoint.Binding = CreateBinding(selectedEndpoint.Binding, group);
                }

                if (serviceEndpoint.Address == null)
                {
                    serviceEndpoint.Address = new EndpointAddress(selectedEndpoint.Address, GetIdentity(selectedEndpoint.Identity), selectedEndpoint.Headers.Headers);
                }

                if (serviceEndpoint.Behaviors.Count == 0 && selectedEndpoint.BehaviorConfiguration != null)
                {
                    AddBehaviors(selectedEndpoint.BehaviorConfiguration, serviceEndpoint, group);
                }

                serviceEndpoint.Name = selectedEndpoint.Contract;
            }

            return serviceEndpoint;
        }

        /// <summary>
        /// 为所选择的终结点配置绑定
        /// </summary>
        /// <param name="bindingName"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        private Binding CreateBinding(string bindingName, ServiceModelSectionGroup group)
        {
            BindingCollectionElement bindingElementCollection = group.Bindings[bindingName];
            if (bindingElementCollection.ConfiguredBindings.Count > 0)
            {
                IBindingConfigurationElement be = bindingElementCollection.ConfiguredBindings[0];

                Binding binding = GetBinding(be);
                if (be != null)
                {
                    be.ApplyConfiguration(binding);
                }
                if (_timeout != null)
                {
                    binding.CloseTimeout = (TimeSpan)_timeout;
                    binding.OpenTimeout = (TimeSpan)_timeout;
                    binding.ReceiveTimeout = (TimeSpan)_timeout;
                    binding.SendTimeout = (TimeSpan)_timeout;
                }
                return binding;
            }
            return null;
        }

        /// <summary>
        /// 一些创建匹配绑定的方法
        /// </summary>
        /// <param name="configurationElement"></param>
        /// <returns></returns>
        private Binding GetBinding(IBindingConfigurationElement configurationElement)
        {
            if (configurationElement is CustomBindingElement)
                return new CustomBinding();
            else if (configurationElement is BasicHttpBindingElement)
                return new BasicHttpBinding();
            else if (configurationElement is NetMsmqBindingElement)
                return new NetMsmqBinding();
            else if (configurationElement is NetNamedPipeBindingElement)
                return new NetNamedPipeBinding();
            else if (configurationElement is NetPeerTcpBindingElement)
                return new NetPeerTcpBinding();
            else if (configurationElement is NetTcpBindingElement)
                return new NetTcpBinding();
            else if (configurationElement is WSDualHttpBindingElement)
                return new WSDualHttpBinding();
            else if (configurationElement is WSHttpBindingElement)
                return new WSHttpBinding();
            else if (configurationElement is WSFederationHttpBindingElement)
                return new WSFederationHttpBinding();

            return null;
        }

        /// <summary>
        /// 添加configured behavior 到所选择的终结点
        /// </summary>
        /// <param name="behaviorConfiguration"></param>
        /// <param name="serviceEndpoint"></param>
        /// <param name="group"></param>
        private void AddBehaviors(string behaviorConfiguration, ServiceEndpoint serviceEndpoint, ServiceModelSectionGroup group)
        {
            if (!String.IsNullOrEmpty(behaviorConfiguration))
            {
                EndpointBehaviorElement behaviorElement = group.Behaviors.EndpointBehaviors[behaviorConfiguration];
                for (int i = 0; i < behaviorElement.Count; i++)
                {
                    BehaviorExtensionElement behaviorExtension = behaviorElement[i];
                    object extension = behaviorExtension.GetType().InvokeMember("CreateBehavior",
                        BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance,
                        null, behaviorExtension, null);
                    if (extension != null)
                    {
                        serviceEndpoint.Behaviors.Add((IEndpointBehavior)extension);
                    }
                }
            }
        }

        /// <summary>
        /// 从配置文件重获取终结点的identity 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private EndpointIdentity GetIdentity(IdentityElement element)
        {
            EndpointIdentity identity = null;
            PropertyInformationCollection properties = element.ElementInformation.Properties;
            if (properties["userPrincipalName"].ValueOrigin != PropertyValueOrigin.Default)
            {
                return EndpointIdentity.CreateUpnIdentity(element.UserPrincipalName.Value);
            }
            if (properties["servicePrincipalName"].ValueOrigin != PropertyValueOrigin.Default)
            {
                return EndpointIdentity.CreateSpnIdentity(element.ServicePrincipalName.Value);
            }
            if (properties["dns"].ValueOrigin != PropertyValueOrigin.Default)
            {
                return EndpointIdentity.CreateDnsIdentity(element.Dns.Value);
            }
            if (properties["rsa"].ValueOrigin != PropertyValueOrigin.Default)
            {
                return EndpointIdentity.CreateRsaIdentity(element.Rsa.Value);
            }
            if (properties["certificate"].ValueOrigin != PropertyValueOrigin.Default)
            {
                X509Certificate2Collection supportingCertificates = new X509Certificate2Collection();
                supportingCertificates.Import(Convert.FromBase64String(element.Certificate.EncodedValue));
                if (supportingCertificates.Count == 0)
                {
                    throw new InvalidOperationException("UnableToLoadCertificateIdentity");
                }
                X509Certificate2 primaryCertificate = supportingCertificates[0];
                supportingCertificates.RemoveAt(0);
                return EndpointIdentity.CreateX509CertificateIdentity(primaryCertificate, supportingCertificates);
            }

            return identity;
        }

        /// <summary>
        /// 应用配置内容
        /// </summary>
        /// <param name="configurationName">配置节点名称</param>
        protected override void ApplyConfiguration(string configurationName)
        {
            //base.ApplyConfiguration(configurationName);
        }
    }

}
