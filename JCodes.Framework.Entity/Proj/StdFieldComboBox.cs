using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Entity
{
    public class StdFieldComboBox
    {
        /// <summary>
        /// 字段名
        /// </summary>
        private string name;

        [DisplayName("字段名")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string chineseName;

        [DisplayName("字段名称")]
        public string ChineseName
        {
            get { return chineseName; }
            set { chineseName = value; }
        }

        private string datatype;

        [DisplayName("字段类型")]
        public string DataType
        {
            get { return datatype; }
            set { datatype = value; }
        }

        private Int32 dictno;

        [DisplayName("字典条目")]
        public Int32 DictNo
        {
            get { return dictno; }
            set { dictno = value; }
        }

        private string dictnamelst;

        [DisplayName("字典条目说明")]
        public string DictNameLst
        {
            get { return dictnamelst; }
            set { dictnamelst = value; }
        }
    }
}
