using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JCodes.Framework.AddIn.Test
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();

            Init_Data();
        }

        private void Init_Data()
        {
            List<TestGridViewEntity> lst = new List<TestGridViewEntity>();
            lst.Add(new TestGridViewEntity(){ Column1 ="1", Column2 = "Jimmy", Column3=120033, Column4=20180101, Column5 = DateTime.Now });
            gridControl1.DataSource = lst;
        }
    }

    public class TestGridViewEntity
    {
        public string Column1
        {
            get;
            set;
        }
        public string Column2
        {
            get;
            set;
        }

        public Int32 Column3
        {
            get;
            set;
        }

        public Int32 Column4
        {
            get;
            set;
        }

        public DateTime Column5
        {
            get;
            set;
        }

    }
}
