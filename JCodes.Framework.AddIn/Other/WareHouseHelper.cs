using JCodes.Framework.AddIn.UI.WareHouse;
using JCodes.Framework.BLL;
using JCodes.Framework.Common;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JCodes.Framework.AddIn.Other
{
    public class WareHouseHelper
    {
        public static List<CListItem> GetWareHouse(int userId, string userName)
        {
            List<CListItem> itemList = new List<CListItem>();
            List<WareHouseInfo> wareList = new List<WareHouseInfo>();

            List<RoleInfo> roleList = BLLFactory<Role>.Instance.GetRolesByUser(userId);
            bool found = false;
            foreach (RoleInfo roleInfo in roleList)
            {
                if (roleInfo.Name == "组长" || roleInfo.Name == "超级管理员" || roleInfo.Name == "系统管理员" || roleInfo.Name == "普通用户")
                {
                    found = true;
                }
            }

            if (found)
            {
                //如果是组长，获取所有可以管理的库房
                wareList = BLLFactory<WareHouses>.Instance.GetAll();
                foreach (WareHouseInfo wareInfo in wareList)
                {
                    itemList.Add(new CListItem(wareInfo.Name, wareInfo.Name));
                }
            }
            else
            {
                //非组长只能管理负责的
                wareList = BLLFactory<WareHouses>.Instance.GetMangedList(userName);
                foreach (WareHouseInfo wareInfo in wareList)
                {
                    itemList.Add(new CListItem(wareInfo.Name, wareInfo.Name));
                }
            }

            return itemList;
        }

        #region 弹出提示消息窗口
        /// <summary>
        /// 弹出提示消息窗口
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="content"></param>
        public static void Notify(string caption, string content)
        {
            Notify(caption, content, 400, 200, 5000);
        }

        /// <summary>
        /// 弹出提示消息窗口
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="content"></param>
        public static void Notify(string caption, string content, int width, int height, int waitTime)
        {
            NotifyWindow notifyWindow = new NotifyWindow(caption, content);
            notifyWindow.TitleClicked += new System.EventHandler(notifyWindowClick);
            notifyWindow.TextClicked += new EventHandler(notifyWindowClick);
            notifyWindow.SetDimensions(width, height);
            notifyWindow.WaitTime = waitTime;
            notifyWindow.Notify();

            //保存到系统消息表
            //SystemMessageInfo messageInfo = new SystemMessageInfo();
            //messageInfo.ID = Guid.NewGuid().ToString();
            //messageInfo.Title = caption;
            //messageInfo.Content = content;
            //try
            //{
            //    BLLFactory<SystemMessage>.Instance.Insert(messageInfo);
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(WareHouseHelper));
            //    MessageDxUtil.ShowError(ex.Message);
            //}
        }

        private static void notifyWindowClick(object sender, EventArgs e)
        {
            //SystemMessageInfo info = BLLFactory<SystemMessage>.Instance.FindLast();
            //if (info != null)
            //{
            //    //FrmEditMessage dlg = new FrmEditMessage();
            //    //dlg.ID = info.ID;
            //    //dlg.ShowDialog();
            //}
        } 
        #endregion
    }
}
