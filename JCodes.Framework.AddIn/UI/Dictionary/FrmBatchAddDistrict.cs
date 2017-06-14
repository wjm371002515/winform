using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.Common;
using JCodes.Framework.CommonControl;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.BLL;
using JCodes.Framework.CommonControl.BaseUI;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.Other;

namespace JCodes.Framework.AddIn.UI.Dictionary
{
    public partial class FrmBatchAddDistrict : BaseDock
    {
        public string ID = string.Empty;
        public string LoginID = "";//登陆用户ID 

        public FrmBatchAddDistrict()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 使用事务参数，插入数据，最后统一提交事务处理
        /// </summary>
        /// <param name="dictData">字典数据</param>
        /// <param name="seq">排序</param>
        /// <param name="trans">事务对象</param>
        private void InsertDictData(string dictData, DbTransaction trans)
        {
            if (!string.IsNullOrWhiteSpace(dictData))
            {
                DistrictInfo info = new DistrictInfo();
                info.DistrictName = dictData;
                info.CityID = Convert.ToInt32(this.txtCity.Tag.ToString());

                bool succeed = BLLFactory<District>.Instance.Insert(info, trans);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] arrayItems = this.txtDistrictData.Lines;
            if (arrayItems != null && arrayItems.Length > 0)
            {
                DbTransaction trans = BLLFactory<DictData>.Instance.CreateTransaction();
                if (trans != null)
                {
                    try
                    {
                        #region MyRegion
                        foreach (string strItem in arrayItems)
                        {
                            if (this.radSplit.Checked)
                            {
                                if (!string.IsNullOrWhiteSpace(strItem))
                                {
                                    string[] dataItems = strItem.Split(new char[] { ',', '，', ';', '；', '/', '、' });
                                    foreach (string dictData in dataItems)
                                    {
                                        #region 保存数据

                                        InsertDictData(dictData, trans);
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                #region 保存数据
                                if (!string.IsNullOrWhiteSpace(strItem))
                                {
                                    InsertDictData(strItem, trans);
                                }
                                #endregion
                            }
                        }
                        #endregion

                        trans.Commit();
                        ProcessDataSaved(this.btnOK, new EventArgs());
                        MessageDxUtil.ShowTips("保存成功");
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(FrmBatchAddDistrict));
                        MessageDxUtil.ShowError(ex.Message);
                    }
                }
            }
        }
    }
}
