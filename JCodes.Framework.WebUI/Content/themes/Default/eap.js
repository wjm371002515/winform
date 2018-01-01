
//------------------系统中用到的方法---------------
//鼠标在某数据行上点击（选中该行）
function dataline_click(obj)
{
   try
   {
      var datagrid = document.getElementById('datagrid');
      for(var i=0;i<datagrid.rows.length;i++)
      {
         if(datagrid.rows[i].className=='dataline-click')
         {
            if(i%2==0) datagrid.rows[i].className='datalineone';
            else datagrid.rows[i].className='datalinetwo';
         }
      }
      obj.className = "dataline-click";	
   }
   catch(ex)
   {}
}
//鼠标移动到数据行上
function dataline_over(obj)
{
   try
   {
      if(obj.className=="datalineone") obj.className="dataline-over1";
      if(obj.className=="datalinetwo") obj.className="dataline-over2";
   }
   catch(ex)
   {}
}
//鼠标从该数据行上移开
function dataline_out(obj) 
{
   try
   {
	   if(obj.className=="dataline-over1") obj.className="datalineone";
	   if(obj.className=="dataline-over2") obj.className="datalinetwo";
   }
   catch(ex)
   {}
}

//把UTF16转UTF8(JavaScript中获得的中文字符是用UTF16进行编码的，而统一的页面标准格式是UTF-8可不一样哦，所以需要先进行转化UTF-16到UTF8)
function utf16to8(str)
{
   var out, i, len, c;
   out = "";
   len = str.length;
   for(i = 0; i < len; i++) 
   {
      c = str.charCodeAt(i);
      if((c >= 0x0001) && (c <= 0x007F)) 
      {
         out += str.charAt(i);
      }
      else if (c > 0x07FF) 
      {
         out += String.fromCharCode(0xE0 | ((c >> 12) & 0x0F));
         out += String.fromCharCode(0x80 | ((c >>  6) & 0x3F));
         out += String.fromCharCode(0x80 | ((c >>  0) & 0x3F));
       }
      else 
      {
         out += String.fromCharCode(0xC0 | ((c >>  6) & 0x1F));
         out += String.fromCharCode(0x80 | ((c >>  0) & 0x3F));
      }
   }
   return out;
}
//把UTF8转Utf16的
function utf8to16(str)
{
   var out, i, len, c;
   var char2, char3;
   out = "";
   len = str.length;
   i = 0;
   while(i < len) 
   {
      c = str.charCodeAt(i++);
      switch(c >> 4)
      {
         case 0: case 1: case 2: case 3: case 4: case 5: case 6: case 7:
           // 0xxxxxxx
           out += str.charAt(i-1);
           break;
         case 12: case 13:
           // 110x xxxx   10xx xxxx
           char2 = str.charCodeAt(i++);
           out += String.fromCharCode(((c & 0x1F) << 6) | (char2 & 0x3F));
           break;
         case 14:
           // 1110 xxxx  10xx xxxx  10xx xxxx
           char2 = str.charCodeAt(i++);
           char3 = str.charCodeAt(i++);
           out += String.fromCharCode(((c & 0x0F) << 12) |
              ((char2 & 0x3F) << 6) |
              ((char3 & 0x3F) << 0));
           break;
      }
   }
   return out;
}

//对字符串inputstr进行加密，并返回加密后字符串
function encode(inputstr)
{
   var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
   if(inputstr=="")return "AA==";
   inputstr = escape(inputstr);
   var output = "";
   var chr1, chr2, chr3 = "";
   var enc1, enc2, enc3, enc4 = "";
   var i = 0;
   do
   {
      chr1 = inputstr.charCodeAt(i++);
      chr2 = inputstr.charCodeAt(i++);
      chr3 = inputstr.charCodeAt(i++);
      enc1 = chr1 >> 2;
      enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
      enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
      enc4 = chr3 & 63;
      if(isNaN(chr2)){ enc3 = enc4 = 64; }
      else if(isNaN(chr3)){ enc4 = 64; }
      output = output + keyStr.charAt(enc1) + keyStr.charAt(enc2) + keyStr.charAt(enc3) + keyStr.charAt(enc4);
      chr1 = chr2 = chr3 = "";
      enc1 = enc2 = enc3 = enc4 = "";
   } 
   while (i < inputstr.length);
   return output;
}

//对字符串进行inputstr解密，并返回解密后的字符串
function decode(inputstr)
{
   var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
   if(inputstr=="AA==")return "";
   var output = "";
   var chr1, chr2, chr3 = "";
   var enc1, enc2, enc3, enc4 = "";
   var i = 0;
   // remove all characters that are not A-Z, a-z, 0-9, +, /, or =
   var base64test = /[^A-Za-z0-9\+\/\=]/g;
   if(base64test.exec(inputstr))
   {
      /* alert("There were invalid base64 characters in the inputstr text.\n" +
         "Valid base64 characters are A-Z, a-z, 0-9, '+', '/', and '='\n" +
         "Expect errors in decoding.");*/
   }
   inputstr = inputstr.replace(/[^A-Za-z0-9\+\/\=]/g, "");
   do
   {
      enc1 = keyStr.indexOf(inputstr.charAt(i++));
      enc2 = keyStr.indexOf(inputstr.charAt(i++));
      enc3 = keyStr.indexOf(inputstr.charAt(i++));
      enc4 = keyStr.indexOf(inputstr.charAt(i++));
      chr1 = (enc1 << 2) | (enc2 >> 4);
      chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
      chr3 = ((enc3 & 3) << 6) | enc4;
      output = output + String.fromCharCode(chr1);
      if(enc3 != 64){ output = output + String.fromCharCode(chr2); }
      if(enc4 != 64){ output = output + String.fromCharCode(chr3); }
      chr1 = chr2 = chr3 = "";
      enc1 = enc2 = enc3 = enc4 = "";
   } 
   while (i < inputstr.length);
   return unescape(output);
}

//返回ajax的Request对象
function initAjax()
{
   var ajax_request=false; //把false优化为null?
   try
   {
      ajax_request = new ActiveXObject("Msxml2.XMLHTTP");
   }
   catch(ex)
   {
      try
      { 
         ajax_request = new ActiveXObject("Microsoft.XMLHTTP");  
      }
      catch(ex)
      { 
         ajax_request = false; 
      }
   }
   if(!ajax_request && typeof XMLHttpRequest!='undefined') 
   {
      ajax_request = new XMLHttpRequest();
   }
   return ajax_request;
}
//获取返回xml
function getresponsexml(urlstr)
{
   var xmlDoc=null;
   try
   {

      var http_request=false;
      http_request=initAjax();
      http_request.onreadystatechange = function()
      {
         if (http_request.readyState == 4) 
         {
            xmlDoc = http_request.responseXML;
         }
      }
      http_request.open("GET", urlstr, false);
      http_request.send(null);
   }
   catch(ex1)
   {
      xmlDoc=null;
   }
   return xmlDoc;
}
//获取返回的Ajax字符串
//url:调用urlstr获得返回字符串
//谭亮修改
function getresponseText(urlstr){
      var xmlDoc=null;
   try{

      var http_request=false;
      http_request=initAjax();
    http_request.onreadystatechange = function(){
          if (http_request.readyState == 4) {
            xmlDoc = http_request.responseText;
       }
      }
      http_request.open("GET", urlstr, false);
      http_request.send(null);
   }catch(ex1){
         xmlDoc=null;
      }
      return xmlDoc;
}
//获取返回的Ajax字符串
//url:调用urlstr获得返回字符串
function getAjaxText(url)
{
   var ajaxText="";
   try
   {
      var http_request=initAjax();
      http_request.onreadystatechange=function()
      {
         if(http_request.readyState==4){ ajaxText = Trim(http_request.responseText); return ajaxText; }
      }
      http_request.open("GET", url, false);
      http_request.send(null);
      ajaxText = Trim(http_request.responseText);
   }
   catch(ex)
   {
      ajaxText="调用ajax出错。";
   }
   return ajaxText;
}
//显示某操作(action)的进度条，进度提示信息为message。message参数可省略，action参数建议不要省略。
var progress_stop=1,progress_element=null;
function showProgress(message)
{
   try
   {
      var element = event.srcElement;
      if(progress_stop==0) return true;
      progress_stop=0;
      progress_element=element;
      if(element!=null)
      { 
         element.disabled=true; 
         if(element.type=="submit")element.form.submit(); 
      }
      if(message!=null) 
      {
         progress_text.innerHTML=message;
      }
      setTimeout("showProgressWidth(10)",500);
   }
   catch(ex)
   { 
      alert("执行Javascript脚本出错。"); 
   }
   return true;
}
function showProgressWidth(wid)
{
   try
   {
      progress_width.style.width=wid ;
      newWid=wid+10;
      if(newWid>=800) newWid=0;
      progress_line.style.display='';
      if(progress_stop==0) setTimeout("showProgressWidth(newWid)", 100);
      else
      {
         if(progress_element!=null) progress_element.disabled=false;
         progress_line.style.display='none';
      }
   }
   catch(ex)
   { 
      alert("执行Javascript脚本出错。"); 
   }
}

//调用PrintControl.ExecWB(?,?)实现直接打印和打印预览功能。(直接用系统提供的print()方法打印无法隐藏某些区域)
//preview：是否显示预览。null/false:不显示，true:显示
function printPage(preview)
{
   try
   {
      var content=window.document.body.innerHTML;
      var oricontent=content;
      while(content.indexOf("{$printhide}")>=0) content=content.replace("{$printhide}","style='display:none'");
      if(content.indexOf("ID=\"PrintControl\"")<0) content=content+"<OBJECT ID=\"PrintControl\" WIDTH=0 HEIGHT=0 CLASSID=\"CLSID:8856F961-340A-11D0-A96B-00C04FD705A2\"></OBJECT>";
      window.document.body.innerHTML=content;
      //PrintControl.ExecWB(7,1)打印预览，(1,1)打开，(4,1)另存为，(17,1)全选，(10,1)属性，(6,1)打印，(6,6)直接打印，(8,1)页面设置
      if(preview==null||preview==false) PrintControl.ExecWB(6,1);
      else PrintControl.ExecWB(7,1); //OLECMDID_PRINT=7; OLECMDEXECOPT_DONTPROMPTUSER=6/OLECMDEXECOPT_PROMPTUSER=1
      window.document.body.innerHTML=oricontent;
   }
   catch(ex)
   { 
      alert("执行Javascript脚本出错。"); 
   }
}

//把该金额(numeric)转化为大写的中文金额
function capitalNum(numeric)
{
   try
   {
      var n = numeric;
      var strOutput = "";
      var strUnit = '仟佰拾亿仟佰拾万仟佰拾元角分';
      n += "00";
      var intPos = n.indexOf('.');
      if (intPos >= 0) n = n.substring(0, intPos) + n.substr(intPos + 1, 2);
      strUnit = strUnit.substr(strUnit.length - n.length);
      for (var i=0; i < n.length; i++) strOutput += '零壹贰叁肆伍陆柒捌玖'.substr(n.substr(i,1),1) + strUnit.substr(i,1);
      return strOutput;
   }
   catch(ex)
   { 
      return ""; 
      alert("执行Javascript脚本出错。"); 
   }
}

// 把点中的菜单属性设置为menuclick，变为选中状态；并把上次点中的菜单属性设置为menu，恢复为初始状态。
var lastMenu;
var lastModule;
document.onclick = menu_click;
function menu_click()
{

  var srcElement=window.event.srcElement;
  if(srcElement.className=="menu")
  {
     if(lastMenu!=null) lastMenu.className="menu";
     srcElement.className="menuclick";
     lastMenu=srcElement;
   }
  if(srcElement.className=="modulemove")
  {
     if(lastModule!=null) lastModule.className="module";
     srcElement.className="moduleclick";
     lastModule=srcElement;
   }
}

//和树状结构有关的点击操作(点一次就展开，再点一次就折叠)
//obj:<tr>对象
//img:文件夹图片前的[加号]/[减号]标志。
//opt:打开的文件夹标志或关闭的文件夹标志。
//flag:文件夹打开/关闭标志。(-1:全部打开，0:切换状态，1:全部关闭)
//path:用到的图片的路径
function folder_turnit(obj,img,opt,flag,path)
{
   display=obj.style.display;
   if((flag==-1)||(flag==0 && display=="none"))
   { 
      obj.style.display=""; img.src=path+"images/-.gif"; opt.src=path+"images/dir_f2.gif"; 
   }
   if((flag==+1)||(flag==0 && display!="none"))
   { 
      obj.style.display="none";  img.src=path+"images/+.gif"; opt.src=path+"images/dir_f1.gif"; 
   }
}

//把所有名称类似"chk???"的checkbox的值设置为和[chkall]的值相同
//form:要重新设置check控件的form对象
function checkall()
{
   var element = event.srcElement;
   for(var ii=0;ii<element.form.elements.length;ii++)
   {
      var e = element.form.elements[ii];
      if(e!=element && e.type=="checkbox" && e.id.indexOf('chk')==0) e.checked=element.checked;
   }
}

//设置用户/群组权限时，当选中某权限后，自动设置所有上级及下级权限
function selectright()
{
   var element = event.srcElement;
   for(var ii=0;ii<element.form.elements.length;ii++)
   {
      var e = element.form.elements[ii];
      //该权限的下一级权限设置为相同
      if(e.id.indexOf(element.id)==0) e.checked = element.checked;
      //当该权限为true时，该权限的上级权限设置为true
      if(e.type=="checkbox" && element.checked && e.id!='chkall' && element.id.indexOf(e.id)==0) e.checked = element.checked;
   }
}

//确认是否确实要提交
function confirmsubmit()
{
   if(confirm('确实要提交吗？')) 
   {
      return true;
   }
   else
   {
      return false;
   }
}

//确认是否确实要删除(删除单条记录，删除所有记录，删除指定条件记录)
function confirmdelete()
{
   if(confirm('确实要删除记录吗？')) 
   {
      return true;
   }
   else
   {
      return false;
   }
}

//确认是否有选中的记录，并确认是否要执行该操作。
function confirm_selected()
{
   var element = event.srcElement;
   selected=false;
   for(var ii=0;ii<element.form.elements.length;ii++)
   {
      var e = element.form.elements[ii];
      if(e.type=="checkbox" && e.id!='chkall' && e.id.indexOf('chk')==0 && e.checked)  selected=true;
   }
   if(!selected)
   { 
      alert("您还未选择记录，请选择。"); return false; 
   }
   if(confirm('是否确实要对选中的所有记录执行该操作吗？')) 
   {  
      return true;
   }
   return false;
}
//检查备注框输入长度
function textCount(obj,a_limit)
{
   if (obj.value.length > a_limit) 
   {   
      alert("不能超过" + a_limit + "字符！")
      obj.value = obj.value.substring(0,a_limit);
   }
}
//从mystr的第pos个位置区字符串，分隔符为delimiter。 如果mystr,delimiter等于null或空字符串，或pos<1,则返回空字符串。
function fieldGet(mystr,pos,delimiter)
{
   // 若输入字符串为空,或查找域号小于1,则返回空字符串
   if(mystr==null||delimiter==null||mystr==""||delimiter=="" || pos < 1) return "";
   // 循环查找,将指针指向要读取域的起始位置
   index=mystr.indexOf(delimiter);
   if (index < 0 && pos==1) return mystr;
   count = 1;
   pindex=0;
   leng=delimiter.length;
   while(index>=0 && count<pos){
      count = count + 1;
      pindex = index + leng;
      index = mystr.indexOf(delimiter,index + leng);
      }
   //
   if(count < pos) return "";
   if (index >= 0) return mystr.substring( pindex, index);  //去掉了index-pindex
   return mystr.substring( pindex);
}//end method

// 计算mystr的区域数，分隔符为delimiter。如果mystr,delimiter等于null或空字符串,则返回1。最小的返回值为1。
function fieldCount(mystr,delimiter)
{
   // 若输入字符串为空,则返回域数目为1
   if(mystr==null||delimiter==null||mystr=="" || delimiter=="") return 1;
   // 循环查找,将指针指向要读取域的起始位置
   count = 1;
   leng=delimiter.length;
   index=mystr.indexOf(delimiter);
   while(index >= 0)
   {
      count = count + 1;
      index=mystr.indexOf(delimiter,index+leng);
   }
   return count;
}//end method

//根据报表种类不同显示不同的设置参数
function showReport(rptTitle,myElement)
{
   try{
   var element = myElement==null?event.srcElement:myElement;
   var frmMain = myElement==null?event.srcElement.form:myElement.form;
   //如果重新选择了种类，则显示相应的界面
   if(element.name=="report_sort"){
      //根据要执行的是查询/统计/图表，来调整要显示的区域
      if(frmMain.report_sort.value.indexOf("q")==0){
         set_order_cond.style.display='';
         set_group_cond.style.display='none';
         set_total_cond.style.display='none';
         set_chart_cond.style.display='none';
         }
      if(frmMain.report_sort.value.indexOf("g")==0){
         set_order_cond.style.display='none';
         set_group_cond.style.display='';
         set_total_cond.style.display='';
         set_chart_cond.style.display='none';
         }
      if(frmMain.report_sort.value.indexOf("c")==0){
         set_order_cond.style.display='none';
         set_group_cond.style.display='none';
         set_total_cond.style.display='none';
         set_chart_cond.style.display='';
         //调整图表中z轴的显示信息
         var group_field=getGroupField();
         var chart_field=getChartField();
         for(var ii=0;ii<3;ii++){
           chartz=document.getElementsByName("chartz")[ii];
           //chartz=document.getElementById("chartz"+ii);
           if(ii==0) chartz.options.length = 0;
           else  chartz.options.length = 1;
           count=fieldCount(group_field,",");
           for(var jj=1;jj<=count;jj++){
              var text1=fieldGet(group_field,jj,",");
              count2=fieldCount(chart_field,",");
              for(var kk=1;kk<=count2;kk++){
                 var text2=fieldGet(chart_field,kk,",");
                 if(text1.indexOf(text2)>=0){
                    var no = new Option();
                    no.value=fieldGet(text1,1," as ");
                    no.text =text2;
                    chartz.options[chartz.options.length] = no;
                    }
                 }
              }
           }
         }
      //执行结果的显示方式
      if(frmMain.report_sort.value.indexOf("c")==0){ fileext_obj.innerHTML="<select name=fileext>"+fileext_obj_chart.innerHTML; }
      else{  fileext_obj.innerHTML="<select name=fileext>"+fileext_obj_data.innerHTML; }
      //调整报表的标题和预定义报表的标题
      if(rptTitle==null) rptTitle="";
      frmMain.report_title.value=rptTitle+frmMain.report_sort.options[frmMain.report_sort.selectedIndex].text;
      frmMain.report_name.value=frmMain.report_title.value;
      }
   //如果点中了保存统计查询，则显示保存界面
   if(frmMain.save_report.checked) set_save_cond.style.display='';
   else set_save_cond.style.display='none';
   //
   }catch(ex){ alert("执行Javascript脚本出错。"); }
}

//保存统计查询报表中用到的字段条件
function procReport()
{
   try{
   var field_cond="",group_cond="",order_cond="",record_cond="",group_field="",total_field="";
   var field_cond_info="",group_cond_info="",order_cond_info="",record_cond_info="",group_field_info="",total_field_info="";
   var frmMain = event.srcElement.form;
   //如果是显示在页面上，则打开新窗口。如果是保存到文件中，则在当前窗口显示
   if(frmMain.report_sort.value.indexOf("c")==0||frmMain.fileext.value=="") frmMain.target="_blank";
   else frmMain.target="_action";
   //保存查询的相关信息
   if(frmMain.report_sort.value.indexOf("q")==0){
      //显示的字段信息
      var fieldtemp=frmMain.field_query;
      if(fieldtemp.length<=0) fieldtemp=frmMain.field_all;
      for(var ii=0; ii<fieldtemp.length; ii++){
         if(fieldtemp.options[ii].value=="") continue;
         if(field_cond!=""){ field_cond=field_cond+","; field_cond_info=field_cond_info+","; }
         field_cond=field_cond+fieldtemp.options[ii].value+" as '"+fieldtemp.options[ii].text+"'";
         field_cond_info=field_cond_info+fieldtemp.options[ii].text;
         }
      //排序信息
      for(var ii=0;ii<frmMain.elements.length-2;ii++){
         var e = frmMain.elements[ii];
         var e1 = frmMain.elements[ii+1];
         var e2 = frmMain.elements[ii+2];
         if(e.name!="queryorder"||e1.name!="queryorder2"||e.value==null||e1.value==null||e.value==""||e1.value=="") continue;
         if((","+order_cond+",").indexOf(","+e.value+" ")>=0) continue;
         if(order_cond!=""){ order_cond=order_cond+","; order_cond_info=order_cond_info+","; }
         order_cond=order_cond+e.value+" "+e1.value;
         order_cond_info=order_cond_info+e.options[e.selectedIndex].text+" "+e1.options[e1.selectedIndex].text;
         //分组字段
         if(e2.name!="querytotal"||e2.value==null||e2.value=="") continue;
         if(group_field!="") group_field=group_field+",";
         group_field=group_field+e.options[e.selectedIndex].text;
         }
      }
   //保存统计的相关信息
   if(frmMain.report_sort.value.indexOf("g")==0){
      for(var ii=0;ii<frmMain.elements.length;ii++){
         var e = frmMain.elements[ii];
         if(e.name!="groupby"||e.value==null||e.value=="") continue;
         //显示的字段信息
         if((","+field_cond+",").indexOf(","+e.value+" ")<0){
            if(field_cond!=""){ field_cond=field_cond+","; field_cond_info=field_cond_info+","; }
            field_cond=field_cond+e.value+" as '"+e.options[e.selectedIndex].text+"'";
            field_cond_info=field_cond_info+e.options[e.selectedIndex].text;
            }
         //group by字段信息
         if((","+group_cond+",").indexOf(","+e.value+",")<0){
            if(group_cond!=""){ group_cond=group_cond+","; group_cond_info=group_cond_info+","; }
            group_cond=group_cond+e.value;
            group_cond_info=group_cond_info+e.options[e.selectedIndex].text;
            }
         //排序/分组信息
         if(ii+1>=frmMain.elements.length) continue;
         if(ii+2>=frmMain.elements.length) continue;
         var e1 = frmMain.elements[ii+1]; //排序字段
         var e2 = frmMain.elements[ii+2]; //分组字段(设置了分组的字段，会自动做排序)
         //排序信息
         if(e1.name=="grouporder"&&e1.value!=null&&(e1.value!=""||e2.value!="")&&(","+order_cond+",").indexOf(","+e.value+" ")<0){
            if(order_cond!=""){ order_cond=order_cond+","; order_cond_info=order_cond_info+","; }
            order_cond=order_cond+e.value+" "+e1.value;
            order_cond_info=order_cond_info+e.options[e.selectedIndex].text+" "+e1.options[e1.selectedIndex].text;
            }
         //分组信息
         if(e2.name=="grouptotal"&&e2.value!=null&&e2.value!=""&&(","+group_field+",").indexOf(","+e.options[e.selectedIndex].text+",")<0){
            if(group_field!="") group_field=group_field+",";
            group_field=group_field+e.options[e.selectedIndex].text;
            }
         }
      //增加排序信息
      for(var ii=0;ii<frmMain.elements.length;ii++){
         var e = frmMain.elements[ii];
         if(e.name!="grouporder2"||e.value==null||e.value=="") continue;
         if(order_cond!=""){ order_cond=order_cond+","; order_cond_info=order_cond_info+","; }
         order_cond=order_cond+e.value;
         order_cond_info=order_cond_info+e.options[e.selectedIndex].text;
         }
      //统计时显示的字段
      var newfield=getGroupField();
      if(newfield==null) newfield="";
      if(field_cond!="" && newfield!="") field_cond=field_cond+",";
      field_cond=field_cond+newfield;
      }
   //保存图表的相关信息
   if(frmMain.report_sort.value.indexOf("c")==0){
      for(var ii=0;ii<frmMain.elements.length;ii++){
         var e = frmMain.elements[ii];
         if(e.name!="chartx"&&e.name!="charty"&&e.name!="chartz"||e.value==null||e.value=="") continue;
         //显示的字段信息
         if((","+field_cond+",").indexOf(","+e.value+" ")<0){
            if(field_cond!=""){ field_cond=field_cond+","; field_cond_info=field_cond_info+","; }
            field_cond=field_cond+e.value+" as '"+e.options[e.selectedIndex].text+"'";
            field_cond_info=field_cond_info+e.options[e.selectedIndex].text;
            }
         //group by字段信息
         if((e.name=="chartx"||e.name=="charty")&&(","+group_cond+",").indexOf(","+e.value+",")<0){
            if(group_cond!=""){ group_cond=group_cond+","; group_cond_info=group_cond_info+","; }
            group_cond=group_cond+e.value;
            group_cond_info=group_cond_info+e.options[e.selectedIndex].text;
            }
         //排序信息
         if(ii+1>=frmMain.elements.length) continue;
         var e1 = frmMain.elements[ii+1];
         if(e1.name.indexOf("chartorder")==0&&e1.value!=null&&e1.value!=""&&(","+order_cond+",").indexOf(","+e.value+" ")<0){
            if(order_cond!=""){ order_cond=order_cond+","; order_cond_info=order_cond_info+","; }
            order_cond=order_cond+e.value+" "+e1.value;
            order_cond_info=order_cond_info+e.options[e.selectedIndex].text+" "+e1.options[e1.selectedIndex].text;
            }
         }
      }
   //整理数据
   if(field_cond=="") field_cond="*";
   if(group_cond!="") group_cond="group by "+group_cond;
   if(order_cond!="") order_cond="order by "+order_cond;
   record_cond=frmMain.max_record.value;
   total_field=getTotalField();
   if(record_cond!="") record_cond_info="显示前 "+record_cond+" 条记录";
   if(group_field!="") group_field_info="按 "+group_field+" 分组统计";
   if(total_field!="") total_field_info="统计 "+total_field+" 的值";
   if(field_cond_info!="") field_cond_info="显示 "+field_cond_info+" 字段";
   if(group_cond_info!="") group_cond_info="统计 "+group_cond_info+" 字段";
   if(order_cond_info!="") order_cond_info="按 "+order_cond_info+" 方式排序";
   //保存数据
   frmMain.fieldCond.value=field_cond; frmMain.fieldCondInfo.value=field_cond_info;
   frmMain.groupCond.value=group_cond; frmMain.groupCondInfo.value=group_cond_info;
   frmMain.orderCond.value=order_cond; frmMain.orderCondInfo.value=order_cond_info;
   frmMain.recordCond.value=record_cond; frmMain.recordCondInfo.value=record_cond_info;
   frmMain.groupField.value=group_field; frmMain.groupFieldInfo.value=group_field_info;
   frmMain.totalField.value=total_field; frmMain.totalFieldInfo.value=total_field_info;
   }catch(ex){ alert("执行Javascript脚本出错。"); }
}

//把list中选中的记录进行上移
//list：要进行移动的记录所在的列表对象
function move_list_up(list)
{
   var selected=false;
   var tlen=list.options.length;
   for(var ii=0; ii<tlen; ii++){
      if(list.options[ii].selected) selected=true;
      if(ii<=0) continue;
      if(list.options[ii-1].selected||!list.options[ii].selected) continue;
      var newOption = new Option();
      newOption.value = list.options[ii-1].value;
      newOption.text  = list.options[ii-1].text;
      newOption.selected  = list.options[ii-1].selected;
      list.options[ii-1].value =list.options[ii].value;
      list.options[ii-1].text  =list.options[ii].text;
      list.options[ii-1].selected =list.options[ii].selected;
      list.options[ii].value =newOption.value;
      list.options[ii].text  =newOption.text;
      list.options[ii].selected  =newOption.selected;
      }
   if(!selected){ alert("您还未选择要移动的记录，请选择。"); return false; }
}

//把list中选中的记录进行下移
//list：要进行移动的记录所在的列表对象
function move_list_down(list)
{
   var selected=false;
   var tlen=list.options.length;
   for(var ii=tlen-1; ii>=0; ii--){
      if(list.options[ii].selected) selected=true;
      if(ii>=tlen-1) continue;
      if(list.options[ii+1].selected||!list.options[ii].selected) continue;
      var newOption = new Option();
      newOption.value = list.options[ii+1].value;
      newOption.text  = list.options[ii+1].text;
      newOption.selected  = list.options[ii+1].selected;
      list.options[ii+1].value =list.options[ii].value;
      list.options[ii+1].text  =list.options[ii].text;
      list.options[ii+1].selected =list.options[ii].selected;
      list.options[ii].value =newOption.value;
      list.options[ii].text  =newOption.text;
      list.options[ii].selected  =newOption.selected;
      }
   if(!selected){ alert("您还未选择要移动的记录，请选择。"); return false; }
}

//把listFrom中选中记录或所有记录，拷贝到listTo中(如果listFrom中该记录value为空或listTo中已经有该记录(即text相同)，则不移动)
//listFrom：要拷贝的记录所在的列表对象
//listTo  ：记录要拷贝到的目的列表对象
//moveall ：是否拷贝listFrom中的所有记录
function copy_list_item(listFrom,listTo,moveall)
{
   var selected=false;
   var tlen=listTo.options.length;
   for(var ii=0; ii<listFrom.options.length; ii++){
      value=listFrom.options[ii].value;
      //如果不是移动所有并且未选中，或者value为空，则continue;
      if((!moveall && !listFrom.options[ii].selected)||(value=="")) continue;
      selected=true;
      //如果listTo中已经有该记录，则continue;
      var flag=0;
      for(var jj=0; jj<tlen; jj++){
         if(listFrom.options[ii].text==listTo.options[jj].text){ flag=1; break; }
         }
      if(flag==1) continue;
      //移动该记录
      var newOption = new Option();
      newOption.value = listFrom.options[ii].value
      newOption.text = listFrom.options[ii].text
      listTo.options[listTo.options.length] = newOption;
      }
   if(!selected){ alert("您还未选择记录，请选择。"); return false; }
}

//删除list中选中的记录或所有记录
//list  :要删除记录的列表对象
//delall:是否删除列表中的所有记录
function delete_list_item(list,delall,clearflag)
{
  //把要删除的项的标题和值都设置为空
  for(var ii=0; ii<list.options.length; ii++)  {
    if((delall || list.options[ii].selected) &&list.options[ii].value!="") {
       list.options[ii].value = ""
       list.options[ii].text = ""
       }
    }
  //清除列表中已经删除的项
  var ii=0;jj=0;
  for(;(ii<list.options.length)&&(list.options[ii].text!="");ii++);
  if(ii>=list.options.length){ if(!delall) alert("请先选择记录。"); return; }
  for(jj=ii+1;;){
     for(;(jj<list.options.length)&&(list.options[jj].text=="");jj++);
     if(jj>=list.options.length) break;
     list.options[ii].value = list.options[jj].value
     list.options[ii].text = list.options[jj].text
     ii=ii+1;jj=jj+1;
     }
  list.options.length = ii;
  //清空选中标志
  for(var ii=0; ii<list.options.length; ii++){
    if(clearflag) list.options[ii].selected=false;
    }
  }

//把listFrom中所有记录的text和value分别保存到listText和listValue中并选中，这样就可以作为参数传递到服务器了
//listFrom ：要拷贝出text和value的列表对象。
//listText ：保存text数据的列表对象
//listValue：保存value数据的列表对象
function save_list_item(listFrom,listText,listValue)
{
   var len = listFrom.length;
   for(var ii=0; ii<len; ii++)  {
      var newOptionText  = new Option("",listFrom.options[ii].text);
      var newOptionValue = new Option("",listFrom.options[ii].value);
      listText.options[ii] = newOptionText;
      listValue.options[ii] = newOptionValue;
      listText.options[ii].selected=true;
      listValue.options[ii].selected=true;
      }
   listText.options.length = len;
   listValue.options.length= len;
}


//在后台系统的页面中调用选择用户/群组功能(模态窗口)
//URL  ：网页的URL(未用到，该参数以后要取消)
//user1：要选择的第一类用户（必须）
//user2：要选择的第一类用户（必须）
//user3：要选择的第一类用户（必须）
//stype：要选择的用户种类("":选择单个用户,msg:选择2个消息用户,task:选择2个任务用户,mail:选择3个邮件用户,form:选择3个表单用户,proj:选择3个项目用户)
//utype：要选择的是:""所有，user用户，role角色，dept部门，group群组
//alluser：是否显示所有用户(null/""/true：显示所有用户，false/其它:只显示未禁用的用户)



var popup_path="";//弹出页面popup.apsx的完整URL

//选择人员，返回name
function seladdrname(seltype,username)
{ 
   //seltype选择类型 user:人员.dept:部门.role:角色.all:选择人员部门角色 single=true单选 single=false多选
   //page=selidname 返回id,name  page=selname返回name
   var url = "address.aspx?page=selname&single=false&seltype="+seltype+"&seltext=" + username;
   revalue = window.showModalDialog(url,'dialog','dialogHeight:500px;dialogWidth:620px;status:no;scroll:yes;resizable:yes;help:no;center:yes;');
   if(revalue != undefined)
   {
      return revalue;
   }
}
      
//在后台系统的页面中调用选择用户/群组功能(模态窗口)
//URL  ：网页的URL(未用到，该参数以后要取消)
//user1：要选择的第一类用户（必须）
//user2：要选择的第一类用户（必须）
//user3：要选择的第一类用户（必须）
//stype：要选择的用户种类("":选择单个用户,msg:选择2个消息用户,task:选择2个任务用户,mail:选择3个邮件用户,form:选择3个表单用户,proj:选择3个项目用户)
//utype：要选择的是:""所有，user用户，role角色，dept部门，group群组
//alluser：是否显示所有用户(null/""/true：显示所有用户，false/其它:只显示未禁用的用户)
function getSelectUser(URL,user1,user2,user3,stype,utype,alluser)
{
   if(user2==null) user2="";
   if(user3==null) user3="";
   if(stype==null) stype="";
   if(utype==null) utype="";
   if(alluser==null) alluser="";
   if(popup_path!=null&&popup_path!="") URL=popup_path;
   var theurl = URL+"?page=selectuser&stype="+stype+"&utype="+utype+"&user1="+user1+"&user2="+user2+"&user3="+user3+"&alluser="+alluser;
   
   alert(theurl);
   var returnValue = window.showModalDialog(theurl,'dialog','dialogHeight:510px;dialogWidth:700px;edge:raised;help:no;status:no;scroll:yes;');
   return returnValue;
}

//在后台系统的页面中调用选择用户/群组功能(模态窗口)
//URL  ：网页的URL(未用到，该参数以后要取消)
//user1：缺省值
//utype：要选择的是:""所有，user用户，role角色，dept部门，group群组
//alluser：是否显示所有用户(null/""/true：显示所有用户，false/其它:只显示未禁用的用户)
function getSelectOne(URL,user1,utype,alluser)
{
   if(utype==null) utype="";
   if(alluser==null) alluser="";
   if(popup_path!=null&&popup_path!="") URL=popup_path;
   var theurl = URL+"?page=selectone&utype="+utype+"&user1="+user1+"&alluser="+alluser;
   var returnValue = window.showModalDialog(theurl,'dialog','dialogHeight:410px;dialogWidth:600px;edge:raised;help:no;status:no;scroll:yes;');
   return returnValue;
}

//显示用户列表并选择一个用户(模态窗口)
//URL  ：网页的URL(未用到，该参数以后要取消)
//defaultvalue：缺省用户(如果直接退出，则表示选了缺省用户)
function getUserList(URL,defaultvalue)
{
   if(defaultvalue==null) defaultvalue="";
   if(popup_path!=null&&popup_path!="") URL=popup_path;
   var theurl = URL+"?page=userlist&default="+defaultvalue;
   var returnValue = window.showModalDialog(theurl,'dialog','dialogHeight:420px;dialogWidth:420px;edge:raised;help:no;status:no;scroll:yes;');
   return returnValue;
}

//在tbox显示usertext/uservalue中指定种类的用户/群组列表
//usertext ：可选择的用户名/群组名列表(数组类型)
//usertype ：可选择的用户名/群组名类型(数组类型)(0:用户,1:角色,2:部门)
//uservalue：可选择的用户id/群组id列表(数组类型)
//tbox     ：显示指定种类用户/群组的列表对象
//mytype   ：显示用户/群组的方式(0:显示所有，-1:所有群组，-2:所有用户，-3:所有角色，-4:所有部门，其它/,1,2,3,:显示指定的部门用户
function show_user_list(usertext,usertype,uservalue,tbox,mytype)
{
   tbox.options.length = 0;
   var ptype="";
   var no = new Option();
   no.value="";
   no.text ="---------请选择---------";
   tbox.options[tbox.options.length] = no;
   for(var ii=0; ii<usertext.length; ii++)  {
      var idx=mytype.indexOf(","+uservalue[ii]+",");
      if(mytype=="0"||mytype=="-1"&&usertype[ii]!="0"||mytype=="-2"&&usertype[ii]=="0"||mytype=="-3"&&usertype[ii]=="1"||mytype=="-4"&&usertype[ii]=="2"||mytype!="-1"&&mytype!="-2"&&mytype!="-3"&&mytype!="-4"&&usertype[ii]=="0"&&idx>=0){
         if(ptype!=""&&ptype!=usertype[ii]){
            var no = new Option();
            no.value="";
            no.text ="---------请选择---------";
            tbox.options[tbox.options.length] = no;
            }
         ptype=usertype[ii];
         var no = new Option();
         no.value= uservalue[ii];
         no.text = usertext[ii];
         tbox.options[tbox.options.length] = no;
         }
      }
}

//在后台系统的页面中调用日历功能选择日期/日期&时间(模态窗口)
//URL  ：网页的URL(未用到，该参数以后要取消)
//defaultValue：缺省值(如果直接退出，则表示选了缺省值)
//minyear ：年份的最小值
//maxyear ：年份的最大值
//vtype   ：日历显示方式(0:日期，1:日期&时，2:日期&时&分，3:日期&时&分&秒，4:时&分&秒)
function getCalendar(URL,defaultValue,minyear,maxyear,vtype)
{
   if(minyear==null)
   {
      minyear="";
   }
   if(maxyear==null)
   {
      maxyear="";
   }
   if(vtype==null)
   {
      vtype="0";
   }
   if(popup_path!=null&&popup_path!="") 
   {
      URL=popup_path;
   }
   
   var theurl = URL+"?page=calendar&default="+defaultValue+"&minyear="+minyear+"&maxyear="+maxyear+"&vtype="+vtype;
   var returnValue ;
   
   if(vtype==4) 
   {
      returnValue = window.showModalDialog(theurl,'dialog','dialogHeight:150px;dialogWidth:320px;edge:raised;help:no;status:no;scroll:yes;');
   }
   else 
   {
      returnValue = window.showModalDialog(theurl,'dialog','dialogHeight:280px;dialogWidth:320px;edge:raised;help:no;status:no;scroll:yes;');
   }
   
   return returnValue;
}

function getCalendar2(URL,defaultValue,minyear,maxyear,vtype)
{
   if(minyear==null) minyear="";
   if(maxyear==null) maxyear="";
   if(vtype==null) vtype="2";
   if(popup_path!=null&&popup_path!="") URL=popup_path;
   
   var theurl = URL+"?page=calendar&default="+defaultValue+"&minyear="+minyear+"&maxyear="+maxyear+"&vtype="+vtype;
   var returnValue ;
   
   if(vtype==4) returnValue = window.showModalDialog(theurl,'dialog','dialogHeight:150px;dialogWidth:320px;edge:raised;help:no;status:no;scroll:yes;');
   else returnValue = window.showModalDialog(theurl,'dialog','dialogHeight:280px;dialogWidth:320px;edge:raised;help:no;status:no;scroll:yes;');
   
   return returnValue;
}

function getCalendar2(URL,defaultValue,minyear,maxyear,vtype)
{
   if(minyear==null) minyear="";
   if(maxyear==null) maxyear="";
   if(vtype==null) vtype="2";
   if(popup_path!=null&&popup_path!="") URL=popup_path;
   var theurl = URL+"?page=calendar&default="+defaultValue+"&minyear="+minyear+"&maxyear="+maxyear+"&vtype="+vtype;
   var returnValue ;
   if(vtype==4) returnValue = window.showModalDialog(theurl,'dialog','dialogHeight:150px;dialogWidth:320px;edge:raised;help:no;status:no;scroll:yes;');
   else returnValue = window.showModalDialog(theurl,'dialog','dialogHeight:280px;dialogWidth:320px;edge:raised;help:no;status:no;scroll:yes;');
   return returnValue;
}


//------------------实现日历功能的页面(share.do?page=calendar)用到的方法---------------
//保存日历的日期&时间(用到year,month,date,hour,minute,second控件)
//vtype：保存日历时间的方式(0:日期,1:日期&时,2:日期&时分,3:日期&时间,4:时间,5:日期)
function get_calendar(vtype)
{
   if(date.value<10) date.value="0"+date.value;
   if(vtype==0) return year.value+"-"+month.value+"-"+date.value;
   else if(vtype==1) return year.value+"-"+month.value+"-"+date.value+" "+hour.value;
   else if(vtype==2) return year.value+"-"+month.value+"-"+date.value+" "+hour.value+":"+minute.value;
   else if(vtype==3) return year.value+"-"+month.value+"-"+date.value+" "+hour.value+":"+minute.value+":"+second.value;
   else if(vtype==4) return hour.value+":"+minute.value+":"+second.value;
   else return year.value+"-"+month.value+"-"+date.value;
}

//显示日历，包括日期&时间(用到oCalendar,date控件)
//vtype：保存日历时间的方式(0:日期,1:日期&时,2:日期&时分,3:日期&时间,4:时间,5:日期)
function calendar(vtype)
{
   while(oCalendar.rows.length>2){ myNewRow = oCalendar.deleteRow(); }
   
   theDate = new Date(year.value,month.value-1,1); //本月11日（month.value从1开始）
   theDate1 = theDate.getDay(); //本月1日是星期几?
   theDate = new Date(year.value,month.value,1);   //下月1日
   theDate2 = theDate.getDay(); //下月1日是星期几?
   
   var oRow;
   var oCell;
   var count=0;
   
   if(theDate1!=0)
   { 
      //如果本月1日不是星期日(0)
      oRow = oCalendar.insertRow(); count=count+1;
      oRow.align="center";
      oRow.height=20;
      for(ii=0;ii<theDate1;ii++)
      {
         oCell = oRow.insertCell();  oCell.innerHTML="&nbsp;";
      }
   }
   
   var ii=1;
   while(ii<28 || (theDate+1)%7!=theDate2)
   {
      theDate = new Date(year.value,month.value-1,ii); 
      theDate = theDate.getDay();
      
      if(theDate==0)
      {
         oRow = oCalendar.insertRow(); count=count+1;
         oRow.align="center";
         oRow.height=20;
      }
      
      oCell = oRow.insertCell();
      var temp = new Date();
      var cYear = temp.getYear();
      var cMonth = temp.getMonth()+1;
      var cDate  = temp.getDate();
      var text = "&nbsp;&nbsp;"+ii+"&nbsp;&nbsp;";
      
      if(ii==date.value)
      {
         oCell.bgColor="#00FF00";
      }
      else if(ii==cDate && year.value==cYear && month.value==cMonth) 
      {
         oCell.bgColor="yellow";
      }
      
      var color="blue";
      if(theDate==6 || theDate==0) color="red";
      
      if(vtype==0) 
      {
         str = "<font color="+color+" style='{cursor:hand;}' onclick='javascript:date.value="+ii+";returnValue=get_calendar(0);'>"+text+"</font>";
      }
      else
      {
         str = "<font color="+color+" style='{cursor:hand;}' onclick='javascript:date.value="+ii+";calendar("+vtype+"); ";
         if(ii==date.value) str=str+"returnValue=get_calendar("+vtype+");top.close();";
         str=str+"'>"+text+"</font>";
      }
      oCell.innerHTML=str;
      ii++;
   }
   
   for(ii=1;ii<7-theDate;ii++)
   {
      oCell = oRow.insertCell(); oCell.innerHTML="&nbsp;";
   }
   
   if(count<6)
   { 
      //每月对应的日历都显示6行，如果不够就插入空行，以避免有的月份只有5行。
      oRow = oCalendar.insertRow(); count=count+1;
      oRow.align="center";
      oRow.height=20;
      for(ii=0;ii<7;ii++)
      {
         oCell = oRow.insertCell(); oCell.innerHTML="&nbsp;";
      }
   }
}



//在后台系统的页面中调用上传文件(弹出窗口)
//URL  ：网页的URL(未用到，该参数以后要取消)
//form  ：要上传文件的form对象
//field ：保存上传文件名的字段对象
//dir   ：上传文件到哪个目录(admin:后台系统上传文件的目录,app:应用模块上传文件的目录,web:网站前台上传文件的目录,其它:临时目录)
//prefix：上传文件保存后的文件名(一般是把时间转化为数字作为文件名)的前缀。
function getFileUpload(URL,form,field,dir,prefix)
{
   if(dir==null) dir="temp";  //temp,admin,app,web
   if(prefix==null) prefix="upload";
   if(popup_path!=null&&popup_path!="") URL=popup_path;
   var theurl = URL+"?page=fileupload&form="+form+"&field="+field+"&dir="+dir+"&prefix="+prefix;
   window.open(theurl,'_blank','toolbar=0,location=0,directories=0,status=1,menubar=0,scrollbars=no,resizable=no,left=300,top=300,width=450,height=150');
   return;
}

//在后台系统的页面中调用选择文件(弹出窗口)
//URL  ：网页的URL
//form  ：要选择文件的form对象
//field ：保存选择文件名的字段对象
//dir   ：到哪个目录选择文件(admin:后台系统上传文件的目录,app:应用模块上传文件的目录,web:网站前台上传文件的目录,其它:临时目录)
function getFileSelect(URL,form,field,dir)
{
   if(dir==null) dir="temp";  //temp,admin,app,web
   if(popup_path!=null&&popup_path!="") URL=popup_path;
   var theurl = URL+"?page=fileselect&form="+form+"&field="+field+"&dir="+dir;
   window.open(theurl,'_blank','toolbar=0,location=0,directories=0,status=1,menubar=0,scrollbars=yes,resizable=yes,left=200,top=200,width=500,height=300');
   return;
}
//--------------------------树结构用到的方法----------------------------------
// 节点对象(节点ID,父节点ID,name:节点名称,url:节点的URL,title:在线提示,target:打开URL的窗口名称,icon:节点图片,iconOpen:展开节点的图片,open:节点是否展开)
function Node(id, pid, name, url, title, target, icon, iconOpen, open) {
   this.id    = id;  //节点ID    Unique identity number.
   this.pid   = pid; //父节点ID,增家根节点时,该参数为-1 Number refering to the parent node. The value for the root node has to be -1.
   this.name  = name;//节点名称  Text label for the node.
   this.url   = url; //节点的URL Url for the node.
   this.title = title; //在线提示 Title for the node.
   this.target= target;//打开URL的窗口名称 Target for the node.
   this.icon  = icon;  //节点图片 Image file to use as the icon. Uses default if not specified.
   this.iconOpen = iconOpen; //展开节点的图片 Image file to use as the open icon. Uses default if not specified.
   this._io = open || false; //节点是否展开( is the node open).
   this._is = false; //是否选中(is selected ?)
   this._ls = false; //是否是兄弟节点中的最后一个(last sibling ?)
   this._hc = false; //是否有子节点(has child?)
   this._ai = 0;  //在节点树组中的index值
this._p;  //父节点的对象
};

// 树对象
function dTree(objName,path) {
   this.config = {
      target           : null, //在树控件上点了链接后在什么窗口显示 Target for all the nodes.
      folderLinks      : true, //目录上允许有链接。(如果为false,则在增加目录节点时设置的URL无效) Should folders be links.
      useSelection     : false, //识别节点选中状态(用高亮度显示)。 Nodes can be selected(highlighted).
      useCookies       : false, //使用Cookies来记住选中的节点 The tree uses cookies to rember it's state.
      useLines         : true,  //树结构中允许有树形线 Tree is drawn with lines.
      useIcons         : true,  //树结构中允许有图标 Tree is drawn with icons.
      useStatusText    : false, //在状态栏不显示URL只显示节点名称。 Displays node names in the statusbar instead of the url.
      closeSameLevel   : false, //某父节点下只有一个子节点是展开状态(不能同时展开多个子节点) Only one node within a parent can be expanded at the same time. openAll() and closeAll() functions do not work when this is enabled.
      inOrder          : true,  //会先添加父节点。(设置了该参数，会增加速度) If parent nodes are always added before children, setting this to true speeds up the tree.
      folderTitleOpen  : true   //是否允许点父节点的标题切换目录的展开/关闭状态
   }
   if(path==null) path="";
   this.icon = {
      root         : path+'images/dtree/base.gif',
      folder       : path+'images/dtree/folder.gif',
      folderOpen   : path+'images/dtree/folderopen.gif',
      node         : path+'images/dtree/page.gif',
      empty        : path+'images/dtree/empty.gif',
      line         : path+'images/dtree/line.gif',
      join         : path+'images/dtree/join.gif',
      joinBottom   : path+'images/dtree/joinbottom.gif',
      plus         : path+'images/dtree/plus.gif',
      plusBottom   : path+'images/dtree/plusbottom.gif',
      minus        : path+'images/dtree/minus.gif',
      minusBottom  : path+'images/dtree/minusbottom.gif',
      nlPlus       : path+'images/dtree/nolines_plus.gif',
      nlMinus      : path+'images/dtree/nolines_minus.gif'
   };
   this.obj = objName; //树节点中各对象的名称的[后缀]
   this.aNodes = []; //所有节点的集合
   this.aIndent = [];
   this.root = new Node(-1); //顶级节点。在界面上显示出来的根节点都是该节点的子节点。
   this.selectedNode = null; //选中的节点
   this.selectedFound = false;
   this.completed = false;
};

// 增加新的节点 Adds a new node to the node array
//(节点ID,父节点ID,name:节点名称,url:节点的URL,title:在线提示,target:打开URL的窗口名称,icon:节点图片,iconOpen:展开节点的图片,open:节点是否展开)
dTree.prototype.add = function(id, pid, name, url, title, target, icon, iconOpen, open) {
   this.aNodes[this.aNodes.length] = new Node(id, pid, name, url, title, target, icon, iconOpen, open);
};

//增加根节点，该根节点的id为空字符串 Adds a new node to the node array
//(name:节点名称,url:节点的URL,title:在线提示,target:打开URL的窗口名称,icon:节点图片,iconOpen:展开节点的图片,open:节点是否展开)
dTree.prototype.addRoot = function(name, url, title, target, icon, iconOpen, open) {
   this.aNodes[this.aNodes.length] = new Node("", -1, name, url, title, target, icon, iconOpen, open);
};

// 判断某节点id是否已经存在
dTree.prototype.exist = function(id) {
   for(var n=0; n<this.aNodes.length; n++) {
      if (this.aNodes[n].id ==id) return true;
      }
   return false;
};

// 根据节点id返回节点对象
dTree.prototype.getNode = function(id) {
   for(var n=0; n<this.aNodes.length; n++) {
      if (this.aNodes[n].id ==id) return this.aNodes[n];
      }
   return null;
};

// 根据节点id返回该节点在数组中的索引值
dTree.prototype.getIndex = function(id) {
   for(var n=0; n<this.aNodes.length; n++) {
      if (this.aNodes[n].id ==id) return n;
      }
   return -1;
};

// 展开所有的节点 Open all nodes
dTree.prototype.openAll = function() {
   this.oAll(true);
};
//关闭所有的节点 close all nodes
dTree.prototype.closeAll = function() {
   this.oAll(false);
};

//把该树节点转化为html形式显示出来 Outputs the tree to the page
dTree.prototype.toString = function() {
   var str = '<div class="dtree">\n';
   if (document.getElementById) {
      if (this.config.useCookies) this.selectedNode = this.getSelected();
      str += this.addNode(this.root);
   } else str += 'Browser not supported.';
   str += '</div>';
   if (!this.selectedFound) this.selectedNode = null;
   this.completed = true;
   return str;
};

//把pNode节点下的树结构转化为html形式(调用了this.node()方法，在this.node()方法中递归调用了该this.addNode()方法) Creates the tree structure
dTree.prototype.addNode = function(pNode) {
   var str = '';
   var n=0;
   if(this.config.inOrder) n = pNode._ai;
   for(n; n<this.aNodes.length; n++) {
      if (this.aNodes[n].pid == pNode.id) {
         var cn = this.aNodes[n];
         cn._p = pNode;
         cn._ai = n;
         this.setCS(cn);
         if(!cn.target && this.config.target) cn.target = this.config.target;
         if(cn._hc && !cn._io && this.config.useCookies) cn._io = this.isOpen(cn.id);
         if(!this.config.folderLinks && cn._hc) cn.url = null;
         if(this.config.useSelection && cn.id == this.selectedNode && !this.selectedFound) {
            cn._is = true;
            this.selectedNode = n;
            this.selectedFound = true;
            }
         str += this.node(cn, n);
         if (cn._ls) break;
         }
      }
   return str;
};

//根据节点对象(node)和索引值(nodeId)创建该节点的html字符串(调用了this.addNode()方法)  Creates the node icon, url and text
dTree.prototype.node = function(node, nodeId) {
   var str = '<div class="dTreeNode">' + this.indent(node, nodeId);
   if(this.config.useIcons) {
      if(!node.icon) node.icon = (this.root.id == node.pid) ? this.icon.root : ((node._hc) ? this.icon.folder : this.icon.node);
      if(!node.iconOpen) node.iconOpen = (node._hc) ? this.icon.folderOpen : this.icon.node;
      if(this.root.id == node.pid) {
         node.icon = this.icon.root;
         node.iconOpen = this.icon.root;
         }
      str += '<img width=18 height=18 align=absbottom '+(node._hc ?'style="{cursor : hand;}" onclick="javascript: ' + this.obj + '.o(' + nodeId + ');"':'')+' border=0 id="i' + this.obj + nodeId + '" src="' + ((node._io) ? node.iconOpen : node.icon) + '" alt="" />';
      }
   if(node.url) {
      str += '<a id="s' + this.obj + nodeId + '" class="' + ((this.config.useSelection) ? ((node._is ? 'nodeSel' : 'node')) : 'node') + '" href="' + node.url + '"';
      if(node.title) str += ' title="' + node.title + '"';
      if(node.target) str += ' target="' + node.target + '"';
      if(this.config.useStatusText) str += ' onmouseover="window.status=\'' + node.name + '\';return true;" onmouseout="window.status=\'\';return true;" ';
      if(((node._hc && this.config.folderLinks) || !node._hc)) str += ' onclick="javascript: '+(this.config.useSelection ?this.obj + '.s(' + nodeId + ');':'') +(node._hc && node.pid != this.root.id?this.obj + '.o(' + nodeId + ',true);':'')+'"';
      str += '>';
      }
   else if ((!this.config.folderLinks || !node.url) && node._hc && node.pid != this.root.id && this.config.folderTitleOpen) str += '<a href="javascript:' + this.obj + '.o(' + nodeId + ');" class="node">';
   str += node.name;
   if (node.url || ((!this.config.folderLinks || !node.url) && node._hc)) str += '</a>';
   str += '</div>';
   if (node._hc) {
      str += '<div id="d' + this.obj + nodeId + '" class="clip" style="display:' + ((this.root.id == node.pid || node._io) ? 'block' : 'none') + ';">';
      str += this.addNode(node);
      str += '</div>';
      }
   this.aIndent.pop();
   return str;
};

// 增加节点前的+/-号图片，以及树结构的线 Adds the empty and line icons
dTree.prototype.indent = function(node, nodeId) {
   var str = '';
   if(this.root.id != node.pid) {
      for(var n=0; n<this.aIndent.length; n++) str += '<img width=18 height=18 align=absbottom border=0 src="' + ( (this.aIndent[n] == 1 && this.config.useLines) ? this.icon.line : this.icon.empty ) + '" alt="" />';
      (node._ls) ? this.aIndent.push(0) : this.aIndent.push(1);
      //如果有子节点
      if(node._hc) {
         str += '<img width=18 height=18 align=absbottom style="{cursor : hand;}" onclick="javascript: ' + this.obj + '.o(' + nodeId + ');" border=0 id="j' + this.obj + nodeId + '" src="';
         if (!this.config.useLines) str += (node._io) ? this.icon.nlMinus : this.icon.nlPlus;
         else str += ( (node._io) ? ((node._ls && this.config.useLines) ? this.icon.minusBottom : this.icon.minus) : ((node._ls && this.config.useLines) ? this.icon.plusBottom : this.icon.plus ) );
         str += '" alt="" />';
         }
      //如果没有子节点
      else str += '<img width=18 height=18 align=absbottom border=0 src="' + ( (this.config.useLines) ? ((node._ls) ? this.icon.joinBottom : this.icon.join ) : this.icon.empty) + '" alt="" />';
   }
   return str;
};

//检测某节点node是否有子节点，以及该节点是否为兄弟节点的最后一个。 Checks if a node has any children and if it is the last sibling
dTree.prototype.setCS = function(node) {
   var lastId;
   for (var n=0; n<this.aNodes.length; n++) {
      if (this.aNodes[n].pid == node.id) node._hc = true;
      if (this.aNodes[n].pid == node.pid) lastId = this.aNodes[n].id;
   }
   if (lastId==node.id) node._ls = true;
};

//返回选中的节点 Returns the selected node
dTree.prototype.getSelected = function() {
   var sn = this.getCookie('cs' + this.obj);
   return (sn) ? sn : null;
};

//高亮度显示选中的节点 Highlights the selected node
dTree.prototype.s = function(id) {
   if (!this.config.useSelection) return;
   var cn = this.aNodes[id];
   if (cn._hc && !this.config.folderLinks) return;
   if (this.selectedNode != id) {
      if (this.selectedNode || this.selectedNode==0) {
         eOld = document.getElementById("s" + this.obj + this.selectedNode);
         eOld.className = "node";
      }
      eNew = document.getElementById("s" + this.obj + id);
      eNew.className = "nodeSel";
      this.selectedNode = id;
      if (this.config.useCookies) this.setCookie('cs' + this.obj, cn.id);
   }
};

//切换目录的展开/关闭状态 Toggle Open or close
dTree.prototype.o = function(id,status) {
   var cn = this.aNodes[id];
   if(status==null) status=!cn._io;
   this.nodeStatus(status, id, cn._ls);
   cn._io = status;
   if (this.config.closeSameLevel) this.closeLevel(cn);
   if (this.config.useCookies) this.updateCookie();
};

//展开所有节点 Open or close all nodes
dTree.prototype.oAll = function(status) {
   for (var n=0; n<this.aNodes.length; n++) {
      if (this.aNodes[n]._hc && this.aNodes[n].pid != this.root.id) {
         this.nodeStatus(status, n, this.aNodes[n]._ls)
         this.aNodes[n]._io = status;
      }
   }
   if (this.config.useCookies) this.updateCookie();
};

//部分展开节点(nId:节点数组的索引值，bSelect:是否选中，bFirst:false表示nId是ID值) Opens the tree to a specific node
dTree.prototype.openTo = function(nId, bSelect, bFirst) {
   if (!bFirst) {
      for (var n=0; n<this.aNodes.length; n++) {
         if (this.aNodes[n].id == nId) {
            nId=n;
            break;
         }
      }
   }
   if(nId>=this.aNodes.length) return;
   var cn=this.aNodes[nId]; //取出指定的节点
   if (cn.pid==this.root.id || !cn._p) return; //如果是顶级节点，则返回
   cn._io = true;  //设置为展开状态
   cn._is = bSelect; //设置为是否选中状态
   if (this.completed && cn._hc) this.nodeStatus(true, cn._ai, cn._ls);
   if (this.completed && bSelect) this.s(cn._ai);
   else if (bSelect) this._sn=cn._ai;
   this.openTo(cn._p._ai, false, true);
};

//部分关闭某一层次的所有节点 Closes all nodes on the same level as certain node
dTree.prototype.closeLevel = function(node) {
   for (var n=0; n<this.aNodes.length; n++) {
      if (this.aNodes[n].pid == node.pid && this.aNodes[n].id != node.id && this.aNodes[n]._hc) {
         this.nodeStatus(false, n, this.aNodes[n]._ls);
         this.aNodes[n]._io = false;
         this.closeAllChildren(this.aNodes[n]);
      }
   }
}

//关闭某节点下的所有节点 Closes all children of a node
dTree.prototype.closeAllChildren = function(node) {
   for (var n=0; n<this.aNodes.length; n++) {
      if (this.aNodes[n].pid == node.id && this.aNodes[n]._hc) {
         if (this.aNodes[n]._io) this.nodeStatus(false, n, this.aNodes[n]._ls);
         this.aNodes[n]._io = false;
         this.closeAllChildren(this.aNodes[n]);
      }
   }
}

//展开/关闭某一个节点 Change the status of a node(open or closed)
dTree.prototype.nodeStatus = function(status, id, bottom) {
   eDiv   = document.getElementById('d' + this.obj + id);
   eJoin   = document.getElementById('j' + this.obj + id);
   if (this.config.useIcons) {
      eIcon   = document.getElementById('i' + this.obj + id);
      eIcon.src = (status) ? this.aNodes[id].iconOpen : this.aNodes[id].icon;
   }
   eJoin.src = (this.config.useLines)?
   ((status)?((bottom)?this.icon.minusBottom:this.icon.minus):((bottom)?this.icon.plusBottom:this.icon.plus)):
   ((status)?this.icon.nlMinus:this.icon.nlPlus);
   eDiv.style.display = (status) ? 'block': 'none';
};

//清除cookie [Cookie] Clears a cookie
dTree.prototype.clearCookie = function() {
   var now = new Date();
   var yesterday = new Date(now.getTime() - 1000 * 60 * 60 * 24);
   this.setCookie('co'+this.obj, 'cookieValue', yesterday);
   this.setCookie('cs'+this.obj, 'cookieValue', yesterday);
};

//设置cookie中的某个值 [Cookie] Sets value in a cookie
dTree.prototype.setCookie = function(cookieName, cookieValue, expires, path, domain, secure) {
   document.cookie =
      escape(cookieName) + '=' + escape(cookieValue)
      + (expires ? '; expires=' + expires.toGMTString() : '')
      + (path ? '; path=' + path : '')
      + (domain ? '; domain=' + domain : '')
      + (secure ? '; secure' : '');
};

//读取cookie中的某个值 [Cookie] Gets a value from a cookie
dTree.prototype.getCookie = function(cookieName) {
   var cookieValue = '';
   var posName = document.cookie.indexOf(escape(cookieName) + '=');
   if (posName != -1) {
      var posValue = posName + (escape(cookieName) + '=').length;
      var endPos = document.cookie.indexOf(';', posValue);
      if (endPos != -1) cookieValue = unescape(document.cookie.substring(posValue, endPos));
      else cookieValue = unescape(document.cookie.substring(posValue));
   }
   return (cookieValue);
};

//把当前打开的节点保存到cookie中，多个节点id用.号分隔  [Cookie] Returns ids of open nodes as a string
dTree.prototype.updateCookie = function() 
{
   var str = '';
   for (var n=0; n<this.aNodes.length; n++) {
      if (this.aNodes[n]._io && this.aNodes[n].pid != this.root.id) {
         if (str) str += '.';
         str += this.aNodes[n].id;
      }
   }
   this.setCookie('co' + this.obj, str);
};

//从cookie值中判断某节点是否为展开状态 [Cookie] Checks if a node id is in a cookie
dTree.prototype.isOpen = function(id) 
{
   var aOpen = this.getCookie('co' + this.obj).split('.');
   for (var n=0; n<aOpen.length; n++)
      if (aOpen[n] == id) return true;
   return false;
};

//如果浏览器不支持数组的push和pop功能,则用程序实现 If Push and pop is not implemented by the browser
if (!Array.prototype.push) 
{
   Array.prototype.push = function array_push() 
   {
      for(var i=0;i<arguments.length;i++)
         this[this.length]=arguments[i];
      return this.length;
   }
};
if (!Array.prototype.pop) 
{
   Array.prototype.pop = function array_pop() 
   {
      lastElement = this[this.length-1];
      this.length = Math.max(this.length-1,0);
      return lastElement;
   }
};


//------------------实现图表功能用到的方法---------------
var chart_cake_onit=true;
var chart_cake_num=0;
var chart_cake_stop;
//立体饼状图上移
function chart_cake_moveup(iteam,txt,top)
{
   temp=eval(iteam);
   tempat=eval(top);
   temptxt=eval(txt);
   at=parseInt(temp.style.top);
   if(chart_cake_num>=5) temptxt.style.display = "";
   if(at>(tempat-10)&&chart_cake_onit)
   {
      chart_cake_num++;
      temp.style.top=at-2;
      chart_cake_stop=setTimeout("chart_cake_moveup(temp,temptxt,tempat)",10);
   }
}
//立体饼状图下移
function chart_cake_movedown(iteam,txt,top)
{
   temp=eval(iteam);
   temptxt=eval(txt);
   clearTimeout(chart_cake_stop);
   temp.style.top=top;
   chart_cake_num=0;
   temptxt.style.display = "none";
}
//还原立体饼状图的移动参数
function chart_cake_movereset(over)
{
   if(over==1) chart_cake_onit=false;
   else chart_cake_onit=true;
}
//放大缩小图形
function chart_zoom(id,action,zoomXY)
{
   var obj = document.getElementById(id).style;
   var k = 1.11;
   if(action=="-") k = 0.9;
   var width=parseInt(obj.width.replace("px",""))
   var height=parseInt(obj.height.replace("px",""))
   if(zoomXY.match("x")&&(width>=100||k>1)&&(width<=2000||k<1))
   {
      obj.width = width*k;
   }
   if(zoomXY.match("y")&&(height>=100||k>1)&&(height<=2000||k<1))
   {
      obj.height = height*k;
   }
}






/*业务应用开发部分*/

//确认是否删除选中的记录，并确认是否要执行删除。
function confirmdel()
{
   selected = false;
   for(var ii = 0;ii < frmdg.elements.length;ii++)
   {
      var e = frmdg.elements[ii];
      if(e.type=="checkbox" && e.id!='chkall' && e.id.indexOf('chk')==0 && e.checked)  selected=true;
   }
   if(!selected)
   { 
      alert("请选择要删除的记录。"); 
      return false; 
   }
   if(confirm('确实要删除所选记录吗？'))
   {
      frmdg.action.value='delete'; 
      frmdg.reload.value='reload'; 
      frmdg.submit();
      return true;
   }
   return false;
}

function tosearch()
{
   if(document.getElementById("tblsearch").style.display == "none")
   {
      document.getElementById("tblsearch").style.display = "inline";
   }
   else
   {
      document.getElementById("tblsearch").style.display = "none";
   }
}
function tohsearch()
{
   if(document.getElementById("trhsearch").style.display == "none")
   {
      document.getElementById("trhsearch").style.display = "inline";
      document.getElementById("imgsearch").src = "../skin/images/hsearch.gif";
   }
   else
   {
      document.getElementById("trhsearch").style.display = "none";
      document.getElementById("imgsearch").src = "../skin/images/lsearch.gif";
   }
}

/******************************************************
描述：	显示指定大小风格固定的模式对话框
返回：	对话框的返回值
参数：	strURL 对话框文件地址
		intWidth 对话框的宽度
		intHeight 对话框的高度
		aryParam 对话框的传入参数
******************************************************/
function openDialog(strURL, intWidth, intHeight, aryParam)
{
	return window.showModalDialog(strURL,aryParam,'dialogHeight:' + intHeight + 'px;dialogWidth:' + intWidth + 'px;status:no;scroll:yes;resizable:yes;help:no;center:yes;');
}

/******************************************************
描述：	打开指定大小风格固定的新窗口
返回：	对话框的返回值
参数：	strfileName 对话框文件地址
		intWidth 对话框的宽度
		intHeight 对话框的高度
******************************************************/
function openNewWindow(strURL,intWidth, intHeight)
{
	window.open(strURL,'',"toolbar=0,location=0,directories=0,status=1,menubar=0,scrollbars=1,resizable=1,top=0,left=0,width=" + intWidth + ",height=" + intHeight );
}

/******************************************************
描述：	去掉字符串的前后空格
返回：	
参数：	
******************************************************/
function trimStr(sVal)
{
	var iPos;
	//clear starting space
	while(true)
	{
		iPos = sVal.indexOf(' ');
		if(iPos == -1)
		{
			break;
		}
		
		if(iPos > 0) 
		{
			break;
		}
		sVal = sVal.slice(1);
	}
	//clear ending space
	while(true)
	{
		iPos = sVal.lastIndexOf(' ');
		if(iPos == -1) 
		{
			break;
		}
		
		if(iPos < sVal.length-1)
		{
			break;
		}
	
		sVal = sVal.slice(0, iPos);
	}
	return(sVal);
}

/******************************************************
描述：	执行服务器端的程序文件
返回：	成功则返回程序执行结果。失败则返回-1
参数：	strPrgmURL	程序文件路径
		   strMethod	发送方法（POST或GET）
		   strParamString POST方法时的参数字符串
******************************************************/
function ExecServerPrgm(strPrgmURL, strMethod, strParamString)
{
	try
	{
		var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
		
		xmlhttp.Open(strMethod, strPrgmURL, false);
		if (strMethod.toUpperCase() == 'POST')
		{
			xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
			xmlhttp.Send(strParamString);			
		}
		else
		{
			xmlhttp.Send();				
		}
		if (xmlhttp.status == 200)
		{
			return unescape(trimStr(xmlhttp.responseText));
		}
		else
		{
			return -1;
		}
	}
	catch(e)
	{
	  return -1;
	}
}

/******************************************************
描述：	刷新页面
返回：	
参数：	
******************************************************/
function refreshPage()
{
	window.location.href = window.location.href;
}