// wujianming 20201222 新增框架报错信息捕获，反馈给后台
window.onerror = function (errorMessage, scriptURI, lineNo, columNo, error) {
    console.log('errorMessage:' + errorMessage);
    console.log('scriptURI:' + scriptURI);
    console.log('lineNo:' + lineNo);//异常行号
    console.log('columNo:' + columNo);//异常列号
    console.log('error:' + error);//异常堆栈信息

    // TODO 发送异常到后台

}