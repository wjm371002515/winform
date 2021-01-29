// wujianming 20201222 新增框架报错信息捕获，反馈给后台
window.onerror = function (errorMessage, scriptURI, lineNo, columNo, error) {
    console.log('errorMessage:' + errorMessage);
    console.log('scriptURI:' + scriptURI);
    console.log('lineNo:' + lineNo);//异常行号
    console.log('columNo:' + columNo);//异常列号
    console.log('error:' + error);//异常堆栈信息

    var httphtml;
    if (window.XMLHttpRequest) {
        // code for IE7+, Firefox, Chrome, Opera, Safari  
        httphtml = new XMLHttpRequest();
    } else {
        // code for IE6, IE5  
        httphtml = new ActiveXObject("Microsoft.XMLHTTP");
    }

    if (!httphtml) {
        alert("httphtml异常");
        returnfalse;
    }
    //将账户名和密码进行编码传递，放置数据乱码

    //方法一，通过问号传递
    var postData = {
        DataType: errorMessage,
        InputParam: scriptURI,
        Data1: lineNo,
        Data2: columNo,
        TimeStamp: Date.parse(new Date()) / 1000
    };
    //异步 - True 或 False？
    httphtml.open("POST", "/Interface/AjaxPostJsErr", false);
    httphtml.onreadystatechange = function () {
        /*
        存有 XMLHttpRequest 的状态。从 0 到 4 发生变化。
        0: 请求未初始化
        1: 服务器连接已建立
        2: 请求已接收
        3: 请求处理中
        4: 请求已完成，且响应已就绪
        */
        if (httphtml.readyState == 4) {
            /*
            200: "OK"
            404: 未找到页面
            */
            if (httphtml.status == 200) {
                /*
                //将ajax请求处理后返回的值显示出来
                var value = httphtml.responseText;
                //函数eval对json格式字符串进行反序列化操作。
                var obj = eval("(" + value + ")");
                if (obj.state == "1") {
                    alert(obj.msg);
                }
                else {
                    alert(obj.msg);
                }*/

            } else {
                //alert("ajax请求错误！");
            }
        }

    }
    //有两种办法处理：第一种方式是   用来将对象序列化为JSON字符串(JSON.stringify())，

    //第二种方式是直接用双引号包裹起来，比如data: "{'accountName':'foovalue', 'password':'barvalue'}"。
    var data = JSON.stringify(postData);
    httphtml.setRequestHeader("Data-Type","json");
    httphtml.setRequestHeader("Content-Type","application/json");
    //将请求发送到服务器。参数string仅用于POST请求；
    httphtml.send(data);
}

//throw new Error('这是一个错误');

/*3.用正则表达式实现html转码*/
function htmlEncodeByRegExp(str) {
    var s = "";
    if (str.length == 0) return "";
    s = str.replace(/&/g, "&amp;");
    s = s.replace(/</g, "&lt;");
    s = s.replace(/>/g, "&gt;");
    s = s.replace(/ /g, "&nbsp;");
    s = s.replace(/\'/g, "&#39;");
    s = s.replace(/\"/g, "&quot;");
    return s;
}

/*4.用正则表达式实现html解码*/
function htmlDecodeByRegExp(str) {
    var s = "";
    if (str.length == 0) return "";
    s = str.replace(/&amp;/g, "&");
    s = s.replace(/&lt;/g, "<");
    s = s.replace(/&gt;/g, ">");
    s = s.replace(/&nbsp;/g, " ");
    s = s.replace(/&#39;/g, "\'");
    s = s.replace(/&quot;/g, "\"");
    return s;
}

function IsEmpty(obj) {
    if (typeof obj == "undefined" || obj == null || obj == "" || obj == "NaN") {
        return true;
    } else {
        return false;
    }
}

PrintConsoleInfo();

function PrintConsoleInfo() {
    console.clear();
    console.log("jCodes 网站");
    console.log("%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C", "color:#fff6f7", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff");
    console.log("%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d", "color:#ff0024", "color:#fff1f3", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff");
    console.log("%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s", "color:#ffffff", "color:#ff0024", "color:#ff0327", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#fffcfc", "color:#ff0024", "color:#ffeef0", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff");
    console.log("%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ff0024", "color:#ff0024", "color:#ff0024", "color:#ff0024", "color:#ff0024", "color:#ff0024", "color:#fe0024", "color:#fd0024", "color:#fc0025", "color:#fb0025", "color:#fa0025", "color:#f91335", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff");
    console.log("%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d", "color:#ffffff", "color:#ff0024", "color:#ff0024", "color:#fe0024", "color:#fe8c9c", "color:#ffffff", "color:#fb0025", "color:#fa0025", "color:#f90025", "color:#f80025", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#009a89", "color:#009788", "color:#3bafa4", "color:#5ba09d", "color:#7da4a6", "color:#a6b8bc", "color:#e3e9ea", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff");
    console.log("%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s", "color:#ffffff", "color:#fc637a", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#fcabb8", "color:#f50026", "color:#f40026", "color:#f30026", "color:#f20027", "color:#f10027", "color:#fbbac5", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#019586", "color:#019587", "color:#019687", "color:#009a89", "color:#009989", "color:#009687", "color:#009687", "color:#009788", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff");
    console.log("%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ee0028", "color:#ed0028", "color:#eb0028", "color:#ea0028", "color:#ea052d", "color:#ffffff", "color:#ffffff", "color:#fff9ee", "color:#fbb437", "color:#fbb236", "color:#fbb23c", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#009888", "color:#f7f8f8", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff");
    console.log("%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#e80029", "color:#e70029", "color:#e50029", "color:#e5002a", "color:#fef5f7", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#fbac35", "color:#faaa34", "color:#faa734", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#d9dee0", "color:#009a89", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff");
    console.log("%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#e1002a", "color:#df002b", "color:#df002b", "color:#de002b", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#faa333", "color:#faa132", "color:#fa9e31", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#393d49", "color:#393d49", "color:#393d49", "color:#373e49", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#009687", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#fa5b2e", "color:#ff5721", "color:#ff5821", "color:#f9dddc");
    console.log("%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#da002c", "color:#d9002c", "color:#d8002c", "color:#d7002d", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#fa9a30", "color:#fa9830", "color:#fee4cb", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ede8ea", "color:#ffffff", "color:#ffffff", "color:#353f49", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#009687", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ff5721", "color:#ffffff", "color:#fae2e2", "color:#ffffff");
    console.log("%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#d3002d", "color:#d2002e", "color:#d1002e", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#f9932e", "color:#f9902e", "color:#f98d2d", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#393d49", "color:#ffffff", "color:#ffffff", "color:#373e49", "color:#373e49", "color:#ffffff", "color:#ffffff", "color:#5dbd7a", "color:#5dbc79", "color:#ffffff", "color:#ffffff", "color:#009787", "color:#009687", "color:#92aeb1", "color:#009687", "color:#ffffff", "color:#00aff2", "color:#15a6e5", "color:#00aef1", "color:#ffffff", "color:#ffffff", "color:#ff5721", "color:#ff5821", "color:#ff5721", "color:#f95c30");
    console.log("%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#cf002f", "color:#ce002f", "color:#cc002f", "color:#eb9eb0", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#f98d2d", "color:#f98b2c", "color:#f9882c", "color:#ffffff", "color:#ffffff", "color:#f0e9ec", "color:#755d6c", "color:#ffffff", "color:#34404a", "color:#635161", "color:#c6b0ba", "color:#ffffff", "color:#5fb878", "color:#f5f2f1", "color:#5fb878", "color:#5dbb79", "color:#499995", "color:#9cc5c3", "color:#aabcc0", "color:#ffffff", "color:#009687", "color:#ffffff", "color:#01aaed", "color:#ffffff", "color:#01aaed", "color:#ffffff", "color:#ffffff", "color:#fe5822", "color:#ffffff", "color:#ff5920", "color:#f66443");
    console.log("%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#c80030", "color:#c70030", "color:#c60030", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#f9872b", "color:#f9842b", "color:#f9822a", "color:#ffffff", "color:#ffffff", "color:#353f4a", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#393d49", "color:#ffffff", "color:#ffffff", "color:#5fb878", "color:#ffffff", "color:#74ab7c", "color:#5dbc7a", "color:#009888", "color:#dcdee1", "color:#ffffff", "color:#ffffff", "color:#009687", "color:#c6e0f1", "color:#01aaed", "color:#47b3e6", "color:#00acef", "color:#ffffff", "color:#ffffff", "color:#f85e37", "color:#ffffff", "color:#ff5721", "color:#f17c69");
    console.log("%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#cc1e49", "color:#ffffff", "color:#ffffff", "color:#c20031", "color:#c10031", "color:#c00032", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#f9802a", "color:#f97d29", "color:#f87b28", "color:#ffffff", "color:#ffffff", "color:#393d49", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#5fb878", "color:#ffffff", "color:#b3baaa", "color:#5fb878", "color:#009888", "color:#9fc4c4", "color:#ffffff", "color:#f4f9f9", "color:#009687", "color:#d9e3ef", "color:#01aaed", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ff5721", "color:#ffffff", "color:#ffffff", "color:#ff5721", "color:#fbdbd6");
    console.log("%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#fef9fa", "color:#ffffff", "color:#ffffff", "color:#bd0032", "color:#bb0033", "color:#ba0033", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#f87a28", "color:#f87727", "color:#f87527", "color:#ffffff", "color:#ffffff", "color:#393d49", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#5fb878", "color:#ffffff", "color:#b3baaa", "color:#5fb878", "color:#009989", "color:#0d9185", "color:#ffffff", "color:#009988", "color:#009687", "color:#c6e0f0", "color:#01aaed", "color:#ffffff", "color:#00aff2", "color:#ffffff", "color:#ff5721", "color:#ff5721", "color:#f18778", "color:#fc5422", "color:#ffffff");
    console.log("%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#fefdfd", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#b60034", "color:#b50034", "color:#b40034", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#f87326", "color:#f87026", "color:#f86e25", "color:#ffffff", "color:#ffffff", "color:#383d49", "color:#3b3e4a", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#b6bcad", "color:#e1dfda", "color:#7caf83", "color:#6ea977", "color:#3a9590", "color:#009687", "color:#009a89", "color:#ffffff", "color:#009687", "color:#ffffff", "color:#51a5d7", "color:#aebdd8", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ff5821", "color:#fdecea", "color:#ffffff", "color:#ffffff");
    console.log("%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#b8073a", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#b20035", "color:#b10035", "color:#af0035", "color:#ffffff", "color:#ffffff", "color:#fee8dc", "color:#f86d25", "color:#f86b24", "color:#f97c3f", "color:#ffffff", "color:#ffffff", "color:#715d6c", "color:#393d49", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#c7d7c7", "color:#ecf0ec", "color:#ffffff", "color:#ffffff", "color:#2e918b", "color:#ffffff", "color:#ecf4f3", "color:#aabcc0", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff");
    console.log("%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#b00035", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ac0036", "color:#ab0036", "color:#aa0036", "color:#ffffff", "color:#ffffff", "color:#f86a24", "color:#f86723", "color:#f86523", "color:#fffdfc", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#383e49", "color:#393d49", "color:#d1bfc7", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#efeaed", "color:#b9a0ac", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff");
    console.log("%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#aa0036", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#a60037", "color:#a50038", "color:#ba3d68", "color:#ffffff", "color:#ffffff", "color:#fcc4ab", "color:#f76123", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#3c3c49", "color:#393d49", "color:#393d49", "color:#393d49", "color:#393d49", "color:#393d49", "color:#373e49", "color:#fefefe", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff");
    console.log("%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#a40038", "color:#ab1548", "color:#fcf8fa", "color:#a10038", "color:#a10039", "color:#9f0039", "color:#ffffff", "color:#ffffff", "color:#fdd2c1", "color:#ffffff", "color:#ffffff", "color:#f75820", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff");
    console.log("%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#9f0039", "color:#9f0039", "color:#9f0039", "color:#9f0039", "color:#9f0039", "color:#9f0039", "color:#ffffff", "color:#ffffff", "color:#fcc7b3", "color:#fff8f6", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff");
    console.log("%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#b7416b", "color:#9f0039", "color:#9f0039", "color:#9f0039", "color:#9f0039", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff");
    console.log("%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d%c e%c s%c j%c C%c o%c d", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#a8184c", "color:#9f0039", "color:#9f0039", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#fef4f0", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff", "color:#ffffff");

    console.log("用代码改变世界,哪怕一点点也好。");
    console.log("Change the world with code, even a little bit.");
    console.log("");
    console.log("%c承接计算机专业各类毕业设计", "color: red")
    console.log("%c有偿软件服务和技术服务", "color: red");
    console.log("%c微信: wjm371002515, 也可把需求整理发送至邮箱 codeany@163.com", "color: red");
}