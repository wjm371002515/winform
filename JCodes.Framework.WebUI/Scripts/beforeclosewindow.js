$("a").each(function () {
    console.log($(this).attr("title"));
});

window.onerror = function (errorMessage, scriptURI, lineNo, columNo, error) {
    console.log('errorMessage:' + errorMessage);
    console.log('scriptURI:' + scriptURI);
    console.log('lineNo:' + lineNo);//异常行号
    console.log('columNo:' + columNo);//异常列号
    console.log('error:' + error);//异常堆栈信息
}

throw new Error('这是一个错误');
