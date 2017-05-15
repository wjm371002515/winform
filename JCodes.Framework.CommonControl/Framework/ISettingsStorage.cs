using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JCodes.Framework.CommonControl.Framework
{
    public interface ISettingsStorage
    {
        Dictionary<string, string> Load(string key);
        void Save(string key, Dictionary<string, string> settings);
    }
}
