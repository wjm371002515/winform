using System;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using JCodes.Framework.CommonControl.Framework;
using JCodes.Framework.Common;
using JCodes.Framework.Entity;
using JCodes.Framework.BLL;
using JCodes.Framework.AddIn.UI.Basic;
using JCodes.Framework.jCodesenum.BaseEnum;
using JCodes.Framework.CommonControl;
using JCodes.Framework.Common.Framework;
using JCodes.Framework.CommonControl.Other;
using JCodes.Framework.CommonControl.PlugInInterface;
using System.Threading;
using DevExpress.Utils;
using System.Diagnostics;

namespace JCodes.Framework.AddIn.Other
{
    /// <summary>
    /// 动态创建RibbonPage和其下面的按钮项目辅助类
    /// </summary>
    public class RibbonPageHelper
    {
        private RibbonControl control;
        public MainForm mainForm;

        public RibbonPageHelper(MainForm mainForm, ref RibbonControl control)
        {
            this.mainForm = mainForm;
            this.control = control;
        }

        public void AddPages()
        {
            // 约定菜单共有3级，第一级为大的类别，第二级为小模块分组，第三级为具体的菜单 
            // Portal.gc.SystemType = WareMis
            List<MenuNodeInfo> menuList = BLLFactory<Menus>.Instance.GetTree(Portal.gc.SystemType);
            if (menuList.Count == 0) return;

            int i = 0;
            foreach(MenuNodeInfo firstInfo in menuList)
            {
                //如果没有菜单的权限，则跳过
                if (!Portal.gc.HasFunction(firstInfo.FunctionId)) continue;

                //添加页面（一级菜单）
                RibbonPage page = new RibbonPage();
                page.Text = firstInfo.Name;
                page.Name = firstInfo.ID;
                this.control.Pages.Insert(i++, page);
                
                if(firstInfo.Children.Count == 0) continue;
                foreach(MenuNodeInfo secondInfo in firstInfo.Children)
                {
                    //如果没有菜单的权限，则跳过
                    if (!Portal.gc.HasFunction(secondInfo.FunctionId)) continue;

                    //添加RibbonPageGroup（二级菜单）
                    RibbonPageGroup group = new RibbonPageGroup();
                    group.Text = secondInfo.Name;
                    group.Name = secondInfo.ID;
                    //group.Glyph = LoadIcon(secondInfo.Icon);
                    //group.ImageIndex = 5;
                    page.Groups.Add(group);                

                    if(secondInfo.Children.Count == 0) continue;
                    foreach (MenuNodeInfo thirdInfo in secondInfo.Children)
                    {
                        //如果没有菜单的权限，则跳过
                        if (!Portal.gc.HasFunction(thirdInfo.FunctionId)) continue;

                        // 判断 WinformType 如果是 RgbiSkins 则表示皮肤
                        if (thirdInfo.WinformType == Const.RgbiSkins)
                        {
                            RibbonGalleryBarItem rgbi = new RibbonGalleryBarItem();
                            var galleryItemGroup1 = new GalleryItemGroup();
                            rgbi.Name = thirdInfo.ID;
                            rgbi.Caption = thirdInfo.Name;
                            rgbi.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            galleryItemGroup1});
                            group.ItemLinks.Add(rgbi);
                            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(rgbi, true);
                        }
                        else {
                            //添加功能按钮（三级菜单）
                            BarButtonItem button = new BarButtonItem();
                            button.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                            button.LargeGlyph = LoadIcon(thirdInfo.Icon);
                            button.Glyph = LoadIcon(thirdInfo.Icon);

                            button.Name = thirdInfo.ID;
                            button.Caption = thirdInfo.Name;
                            button.Tag = thirdInfo.WinformType;
                            button.ItemClick += (sender, e) =>
                            {
                                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, "吴建明测试1"+DateTime.Now.ToShortTimeString(), typeof(RibbonPageHelper));

                                if (button.Tag != null && !string.IsNullOrEmpty(button.Tag.ToString()))
                                {
                                    Portal.gc._waitBeforeLogin = new WaitDialogForm("正则加载 "+button.Caption + " 窗体中...", "加载窗体");
                                    LoadPlugInForm(button.Tag.ToString());
                                    Portal.gc._waitBeforeLogin.Invoke((EventHandler)delegate {
                                        if (Portal.gc._waitBeforeLogin != null)
                                        { 
                                            Portal.gc._waitBeforeLogin.Close(); Portal.gc._waitBeforeLogin = null;
                                        }
                                    });  
                                }
                                else
                                {
                                    MessageDxUtil.ShowTips(button.Caption);
                                }
                            };
                            if (thirdInfo.WinformType.Contains(Const.BeginGroup))
                            {
                                group.ItemLinks.Add(button, true);
                            }
                            else
                            {
                                group.ItemLinks.Add(button);
                            }
                            
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 加载图标，如果加载不成功，那么使用默认图标
        /// </summary>
        /// <param name="iconPath"></param>
        /// <returns></returns>
        private Image LoadIcon(string iconPath)
        {
            // 20170512 wjm 临时修改
            Image result = Properties.Resources.favicon.ToBitmap();
            try
            {
                if (!string.IsNullOrEmpty(iconPath))
                {
                    string path = Path.Combine(Application.StartupPath, iconPath);
                    if (File.Exists(path))
                    {
                        result = Image.FromFile(path);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(RibbonPageHelper));
                MessageDxUtil.ShowError(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 加载插件窗体
        /// </summary>
        private void LoadPlugInForm(string typeName)
        {
            try
            {
                string[] itemArray = typeName.Split(new char[]{',',';'});

                string type = itemArray[0].Trim();
                string filePath = itemArray[1].Trim();//必须是相对路径

                // 判断是否是打开连接
                // 如果是 则做页面的调整操作
                if (Const.BtnLink == type)
                {
                    Process.Start(filePath);
                    return;
                }

                //判断是否配置了显示模式，默认窗体为Show非模式显示
                string showDialog = (itemArray.Length > 2) ? itemArray[2].ToLower() : "";
                bool isShowDialog = (showDialog == "1") || (showDialog == "dialog");

                string dllFullPath = Path.Combine(Application.StartupPath, filePath);
                Assembly tempAssembly = System.Reflection.Assembly.LoadFrom(dllFullPath);
                if (tempAssembly != null)
                {
                    Type objType = tempAssembly.GetType(type);
                    if (objType != null)
                    {
                        LoadMdiForm(this.mainForm, objType, isShowDialog);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogLevel.LOG_LEVEL_CRIT, ex, typeof(RibbonPageHelper));
                MessageDxUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 唯一加载某个类型的窗体，如果存在则显示，否则创建。
        /// </summary>
        /// <param name="mainDialog">主窗体对象</param>
        /// <param name="formType">待显示的窗体类型</param>
        /// <returns></returns>
        public static Form LoadMdiForm(Form mainDialog, Type formType, bool isShowDialog)
        {
            Form tableForm = null;
            bool bFound = false;
            if (!isShowDialog) //如果是模态窗口，跳过
            {
                foreach (Form form in mainDialog.MdiChildren)
                {
                    if (form.GetType() == formType)
                    {
                        bFound = true;
                        tableForm = form;
                        break;
                    }
                }
            }

            //没有在多文档中找到或者是模态窗口，需要初始化属性
            if (!bFound || isShowDialog)
            {
                tableForm = (Form)Activator.CreateInstance(formType);

                //如果窗体集成了IFunction接口(第一次创建需要设置)
                IFunction function = tableForm as IFunction;
                if (function != null)
                {
                    //初始化权限控制信息
                    function.InitFunction(Portal.gc.LoginUserInfo, Portal.gc.FunctionDict);

                    //记录程序的相关信息
                    function.AppInfo = new AppInfo(Portal.gc.AppUnit, Portal.gc.AppName, Portal.gc.AppWholeName, Portal.gc.SystemType);
                }

            }

            if (isShowDialog)
            {
                tableForm.ShowDialog();
            }
            else
            {
                tableForm.MdiParent = mainDialog;
                tableForm.Show();
            }
            tableForm.BringToFront();
            tableForm.Activate();

            return tableForm;
        }
    }
}
