using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JCodes.Framework.Entity
{
    public class TableFieldsInfo 
    {
        private string fieldName;

        [DisplayName("字段名")]
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        private string chineseName;

        [DisplayName("中文名")]
        public string ChineseName
        {
            get { return chineseName; }
            set { chineseName = value; }
        }
            
        private string fieldType;

        [DisplayName("字段类型")]
        public string FieldType
        {
            get { return fieldType; }
            set { fieldType = value; }
        }

        private string fieldInfo;

        [DisplayName("字段说明")]
        public string FieldInfo
        {
            get { return fieldInfo; }
            set { fieldInfo = value; }
        }

        private string isNull;

        [DisplayName("允许空")]
        public string IsNull
        {
            get { return isNull; }
            set { isNull = value; }
        }

        private string remark;

        [DisplayName("修改内容")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}
