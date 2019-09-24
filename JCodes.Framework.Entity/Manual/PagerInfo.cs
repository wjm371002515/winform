using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace JCodes.Framework.Entity
{
    public delegate void PageInfoChanged(PagerInfo info);

    public partial class PagerInfo
    {
        public event PageInfoChanged OnPageInfoChanged;
    }
}
