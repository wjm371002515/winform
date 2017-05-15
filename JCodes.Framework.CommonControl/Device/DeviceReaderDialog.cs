using JCodes.Framework.jCodesenum.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCodes.Framework.CommonControl
{
    /// <summary>
    /// 读卡器、USB条码扫描器、串口条码扫描器数据读取及显示窗体
    /// </summary>
    public partial class DeviceReaderDialog : BaseForm
    {
        private CardReader _cardReader;
        private USBScanner _usbScanner;
        private COMScanner _comScanner;
        public DeviceReaderDialog(DeviceType type = DeviceType.Card)
        {
            InitializeComponent();
            //TODO: 硬件 接口没有做好之前先能手填
            this.Readonly = false;

            if (type == DeviceType.Card)
            {
                this._cardReader = new CardReader(this);
                this._cardReader.CardRead += new CardReadEventHandler(_cardReader_CardRead);
            }
            else if (type == DeviceType.UsbScanner)
            {
                this._usbScanner = new USBScanner(this);
                this._usbScanner.ScannerRead += new ScannerReadEventHandler(Scanner_ScannerRead);
            }
            else if (type == DeviceType.ComScanner)
            {
                this._comScanner = new COMScanner("COM3");
                this._comScanner.ScannerRead += new ScannerReadEventHandler(Scanner_ScannerRead);
            }
        }

        void Scanner_ScannerRead(string scanCode)
        {
            this.txtCode.Text = scanCode;
            DialogResult = DialogResult.OK;
        }

        void _cardReader_CardRead(string cardCode)
        {
            this.txtCode.Text = cardCode;
            DialogResult = DialogResult.OK;
        }

        public string Code
        {
            get { return txtCode.Text; }
        }

        public bool Readonly
        {
            get { return txtCode.Properties.ReadOnly; }
            set
            {
                txtCode.Properties.ReadOnly = value;
                this.btnOK.Enabled = !value;
                this.btnOK.Visible = !value;
            }
        }

        private void DeviceReaderDialog_Load(object sender, EventArgs e)
        {
            if (!this.Readonly)
            {
                this.KeyDown += new KeyEventHandler(DeviceReaderDialog_KeyDown);
            }
        }

        void DeviceReaderDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
