using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace JCodes.Framework.CommonControl.Framework
{
    public abstract class JsonSettingsStoreBase : ISettingsStorage
    {
        public Dictionary<string, string> Load(string key)
        {
            string filename = key + ".settings";
            string str2 = this.ReadTextFile(filename);
            if (!string.IsNullOrEmpty(str2))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
                return (Dictionary<string, string>)serializer.ReadObject(new MemoryStream(Encoding.Default.GetBytes(str2)));
            }
            return new Dictionary<string, string>();
        }
        public void Save(string key, Dictionary<string, string> settings)
        {
            string filename = key + ".settings";
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
            MemoryStream stream = new MemoryStream();
            XmlDictionaryWriter writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.Unicode);
            serializer.WriteObject(stream, settings);
            writer.Flush();
            string fileContents = Encoding.Default.GetString(stream.ToArray());
            this.WriteTextFile(filename, fileContents);
        }
        protected abstract string ReadTextFile(string filename);
        protected abstract void WriteTextFile(string filename, string fileContents);
    }
}
