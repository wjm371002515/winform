using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace JCodes.Framework.Common.Web
{
    /// <summary>
    /// 使用该类，可以实现右下角的弹出界面。
    /// </summary>
    public class PopupMessageHelper
    {
        private StringBuilder m_Script;

        /// <summary>
        /// 构告出弹出界面的前台脚本。
        /// </summary>
        public PopupMessageHelper()
        {
            m_Script = new StringBuilder();
            #region Script
            m_Script.Append("<SCRIPT language=\"JavaScript\">  \n ");
            m_Script.Append("<!--  \n ");
            m_Script.Append("function CLASS_MSN_MESSAGE(id,width,height,caption,message,target,action){  \n ");
            m_Script.Append("this.id     = id;   \n ");
            m_Script.Append("this.caption= caption;  \n ");
            m_Script.Append("this.message= message;  \n ");
            m_Script.Append("this.target = target;  \n ");
            m_Script.Append("this.action = action;  \n ");
            m_Script.Append("this.width    = width?width:200;  \n ");
            m_Script.Append("this.height = height?height:120;  \n ");
            m_Script.Append("this.timeout= 2000;  \n ");
            m_Script.Append("this.speed    = 20; \n ");
            m_Script.Append("this.step    = 1; \n ");
            m_Script.Append("this.right    = screen.width -1;  \n ");
            m_Script.Append("this.bottom = screen.height; \n ");
            m_Script.Append("this.left    = this.right - this.width; \n ");
            m_Script.Append("this.top    = this.bottom - this.height; \n ");
            m_Script.Append("this.timer    = 0; \n ");
            m_Script.Append("this.pause    = false;\n ");
            m_Script.Append("this.close    = false;\n ");
            m_Script.Append("this.autoHide    = true;\n ");
            m_Script.Append("}    \n ");
            m_Script.Append("/*  \n ");
            m_Script.Append("*    隐藏消息方法  \n ");
            m_Script.Append("*/  \n ");
            m_Script.Append("CLASS_MSN_MESSAGE.prototype.hide = function(){  \n ");
            m_Script.Append("if(this.onunload()){  \n ");
            m_Script.Append("var offset  = this.height>this.bottom-this.top?this.height:this.bottom-this.top; \n ");
            m_Script.Append("var me  = this;  \n ");
            m_Script.Append("if(this.timer>0){   \n ");
            m_Script.Append("window.clearInterval(me.timer);  \n ");
            m_Script.Append("}  \n ");
            m_Script.Append("var fun = function(){  \n ");
            m_Script.Append("if(me.pause==false||me.close){\n ");
            m_Script.Append("var x  = me.left; \n ");
            m_Script.Append("var y  = 0; \n ");
            m_Script.Append("var width = me.width; \n ");
            m_Script.Append("var height = 0; \n ");
            m_Script.Append("if(me.offset>0){ \n ");
            m_Script.Append("height = me.offset; \n ");
            m_Script.Append("}      \n ");
            m_Script.Append("y  = me.bottom - height;      \n ");
            m_Script.Append("if(y>=me.bottom){ \n ");
            m_Script.Append("window.clearInterval(me.timer);  \n ");
            m_Script.Append("me.Pop.hide();  \n ");
            m_Script.Append("} else { \n ");
            m_Script.Append("me.offset = me.offset - me.step;  \n ");
            m_Script.Append("} \n ");
            m_Script.Append("me.Pop.show(x,y,width,height);    \n ");
            m_Script.Append("}             \n ");
            m_Script.Append("}  \n ");
            m_Script.Append("this.timer = window.setInterval(fun,this.speed)      \n ");
            m_Script.Append("}  \n ");
            m_Script.Append("}    \n ");
            m_Script.Append("/* \n ");
            m_Script.Append("*    消息卸载事件，可以重写  \n ");
            m_Script.Append("*/  \n ");
            m_Script.Append("CLASS_MSN_MESSAGE.prototype.onunload = function() {  \n ");
            m_Script.Append("return true;  \n ");
            m_Script.Append("}  \n ");
            m_Script.Append("/*  \n ");
            m_Script.Append("*    消息命令事件，要实现自己的连接，请重写它  \n ");
            m_Script.Append("*  \n ");
            m_Script.Append("*/  \n ");
            m_Script.Append("CLASS_MSN_MESSAGE.prototype.oncommand = function(){  \n ");
            m_Script.Append("this.hide();    \n ");
            m_Script.Append("} \n ");
            m_Script.Append("/*  \n ");
            m_Script.Append("*    消息显示方法  \n ");
            m_Script.Append("*/  \n ");
            m_Script.Append("CLASS_MSN_MESSAGE.prototype.show = function(){  \n ");
            m_Script.Append("var oPopup = window.createPopup(); /*IE5.5+ */   \n ");
            m_Script.Append("this.Pop = oPopup;    \n ");
            m_Script.Append("var w = this.width;  \n ");
            m_Script.Append("var h = this.height;    \n ");
            m_Script.Append("var str = \"<DIV style='BORDER-RIGHT: #455690 1px solid; BORDER-TOP: #a6b4cf 1px solid; Z-INDEX: 99999; LEFT: 0px; BORDER-LEFT: #a6b4cf 1px solid; WIDTH: \" + w + \"px; BORDER-BOTTOM: #455690 1px solid; POSITION: absolute; TOP: 0px; HEIGHT: \" + h + \"px; BACKGROUND-COLOR: #c9d3f3'>\"  \n ");
            m_Script.Append("str += \"<TABLE style='BORDER-TOP: #ffffff 1px solid; BORDER-LEFT: #ffffff 1px solid' cellSpacing=0 cellPadding=0 width='100%' bgColor=#cfdef4 border=0>\"  \n ");
            m_Script.Append("str += \"<TR>\"  \n ");
            m_Script.Append("str += \"<TD style='FONT-SIZE: 12px;COLOR: #0f2c8c' width=30 height=24></TD>\"  \n ");
            m_Script.Append("str += \"<TD style='PADDING-LEFT: 4px; FONT-WEIGHT: normal; FONT-SIZE: 12px; COLOR: #1f336b; PADDING-TOP: 4px' vAlign=center width='100%'>\" + this.caption + \"</TD>\"\n ");
            m_Script.Append(" str += \"<TD style='PADDING-RIGHT: 2px; PADDING-TOP: 2px' vAlign=center align=right width=19>\"  \n ");
            m_Script.Append("str += \"<SPAN title=关闭 style='FONT-WEIGHT: bold; FONT-SIZE: 12px; CURSOR: hand; COLOR: red; MARGIN-RIGHT: 4px' id='btSysClose' >×</SPAN></TD>\"  \n ");
            m_Script.Append("str += \"</TR>\"  \n ");
            m_Script.Append("str += \"<TR>\"  \n ");
            m_Script.Append("str += \"<TD style='PADDING-RIGHT: 1px;PADDING-BOTTOM: 1px' colSpan=3 height=\" + (h-28) + \">\"  \n ");
            m_Script.Append("str += \"<DIV style='BORDER-RIGHT: #b9c9ef 1px solid; PADDING-RIGHT: 8px; BORDER-TOP: #728eb8 1px solid; PADDING-LEFT: 8px; FONT-SIZE: 12px; PADDING-BOTTOM: 8px; BORDER-LEFT: #728eb8 1px solid; WIDTH: 100%; COLOR: #1f336b; PADDING-TOP: 8px; BORDER-BOTTOM: #b9c9ef 1px solid; HEIGHT: 100%'>\" + this.message   \n ");
            m_Script.Append("str += \"</DIV>\"  \n ");
            m_Script.Append("str += \"</TD>\"  \n ");
            m_Script.Append("str += \"</TR>\"  \n ");
            m_Script.Append("str += \"</TABLE>\"  \n ");
            m_Script.Append("str += \"</DIV>\"    \n ");
            m_Script.Append("oPopup.document.body.innerHTML = str;  \n ");
            m_Script.Append("this.offset  = 0; \n ");
            m_Script.Append("var me  = this;  \n ");
            m_Script.Append("oPopup.document.body.onmouseover = function(){me.pause=true;}\n ");
            m_Script.Append("oPopup.document.body.onmouseout = function(){me.pause=false;}\n ");
            m_Script.Append("var fun = function(){  \n ");
            m_Script.Append("var x  = me.left; \n ");
            m_Script.Append("var y  = 0; \n ");
            m_Script.Append("var width    = me.width; \n ");
            m_Script.Append("var height    = me.height; \n ");
            m_Script.Append("if(me.offset>me.height){ \n ");
            m_Script.Append("height = me.height; \n ");
            m_Script.Append("} else { \n ");
            m_Script.Append("height = me.offset; \n ");
            m_Script.Append("} \n ");
            m_Script.Append("y  = me.bottom - me.offset; \n ");
            m_Script.Append("if(y<=me.top){ \n ");
            m_Script.Append("me.timeout--; \n ");
            m_Script.Append("if(me.timeout==0){ \n ");
            m_Script.Append("window.clearInterval(me.timer);  \n ");
            m_Script.Append("if(me.autoHide){\n ");
            m_Script.Append("me.hide(); \n ");
            m_Script.Append("}\n ");
            m_Script.Append("} \n ");
            m_Script.Append("} else { \n ");
            m_Script.Append("me.offset = me.offset + me.step; \n ");
            m_Script.Append("} \n ");
            m_Script.Append("me.Pop.show(x,y,width,height);    \n ");
            m_Script.Append("}    \n ");
            m_Script.Append("this.timer = window.setInterval(fun,this.speed)        \n ");
            m_Script.Append("var btClose = oPopup.document.getElementById(\"btSysClose\" );    \n ");
            m_Script.Append("btClose.onclick = function(){  \n ");
            m_Script.Append("me.close = true;\n ");
            m_Script.Append("me.hide();  \n ");
            m_Script.Append("} \n ");
            m_Script.Append("}  \n ");
            m_Script.Append("/* \n ");
            m_Script.Append("** 设置速度方法 \n ");
            m_Script.Append("**/ \n ");
            m_Script.Append("CLASS_MSN_MESSAGE.prototype.speed = function(s){ \n ");
            m_Script.Append("var t = 20; \n ");
            m_Script.Append("try { \n ");
            m_Script.Append("t = praseInt(s); \n ");
            m_Script.Append("} catch(e){} \n ");
            m_Script.Append("this.speed = t; \n ");
            m_Script.Append("} \n ");
            m_Script.Append("/* \n ");
            m_Script.Append("** 设置步长方法 \n ");
            m_Script.Append("**/ \n ");
            m_Script.Append("CLASS_MSN_MESSAGE.prototype.step = function(s){ \n ");
            m_Script.Append("var t = 1; \n ");
            m_Script.Append("try { \n ");
            m_Script.Append("t = praseInt(s); \n ");
            m_Script.Append("} catch(e){} \n ");
            m_Script.Append("this.step = t; \n ");
            m_Script.Append("}   \n ");
            m_Script.Append("CLASS_MSN_MESSAGE.prototype.rect = function(left,right,top,bottom){ \n ");
            m_Script.Append("try { \n ");
            m_Script.Append("this.left        = left    !=null?left:this.right-this.width; \n ");
            m_Script.Append("this.right        = right    !=null?right:this.left +this.width; \n ");
            m_Script.Append("this.bottom        = bottom!=null?(bottom>screen.height?screen.height:bottom):screen.height; \n ");
            m_Script.Append("this.top        = top    !=null?top:this.bottom - this.height; \n ");
            m_Script.Append("} catch(e){} \n ");
            m_Script.Append("} \n ");
            m_Script.Append("-->  \n ");
            m_Script.Append("</SCRIPT> \n ");
            #endregion Script
            HttpContext.Current.Response.Write(m_Script.ToString());
        }

        /// <summary>
        /// 缓慢的显示。
        /// <param name="Title">弹出框的标题</param>
        /// <param name="Content">弹出框显示的内容</param>
        /// <param name="Width">弹出框的宽度</param>
        /// <param name="Height">弹出框的高度</param>
        /// </summary>
        public void Slowly(string Title, string Content, int Width, int Height)
        {
            string ShowMessage = string.Format("<script language='javascript'>var MSG2 =new CLASS_MSN_MESSAGE('DivMessage',{0},{1},'{2}','{3}');MSG2.rect(null,null,null,screen.height); MSG2.show(); </script>", Width, Height, Title, Content);
            HttpContext.Current.Response.Write(ShowMessage);
        }

        /// <summary>
        /// 缓慢的显示。
        /// <param name="Title">弹出框的标题</param>
        /// <param name="Content">弹出框显示的内容</param>
        /// </summary>
        public void Slowly(string Title, string Content)
        {
            Slowly(Title, Content, 0, 0);
        }

        /// <summary>
        /// 快速的显示
        /// <param name="Title">弹出框的标题</param>
        /// <param name="Content">弹出框显示的内容</param>
        /// <param name="Width">弹出框的宽度</param>
        /// <param name="Height">弹出框的高度</param>
        /// </summary>
        public void Quickly(string Title, string Content, int Width, int Height)
        {
            string ShowMessage = string.Format(" <script language='javascript'>var MSG1 = new CLASS_MSN_MESSAGE('DivMessage', {0}, {1}, '{2}', '{3}');MSG1.rect(null, null, null, screen.height - 50);MSG1.speed = 10;MSG1.step = 5; MSG1.show();  </script>", Width, Height, Title, Content);
            HttpContext.Current.Response.Write(ShowMessage);
        }

        /// <summary>
        /// 快速的显示
        /// <param name="Title">弹出框的标题</param>
        /// <param name="Content">弹出框显示的内容</param>
        /// </summary>
        public void Quickly(string Title, string Content)
        {
            Quickly(Title, Content, 0, 0);
        }

        /// <summary>
        /// 调用显示的对象实例
        /// </summary>
        /// <returns></returns>
        public static PopupMessageHelper Instance
        {
            get
            {
                return new PopupMessageHelper();
            }
        }
    }
}
