//本文件定义一些在各个视图里面经常用到的一些Javascript脚本函数

//datagrid宽度高度自动调整的函数
$.fn.extend({
    resizeDataGrid: function (heightMargin, widthMargin, minHeight, minWidth) {
        var height = $(document.body).height() - heightMargin;
        var width = $(document.body).width() - widthMargin;
        height = height < minHeight ? minHeight : height;
        width = width < minWidth ? minWidth : width;
        $(this).datagrid('resize', {
            height: height,
            width: width
        });
    }
});
//对象居中的函数，调用例子：$("#loading").center();
$.fn.center = function () {
    this.css("position", "absolute");
    this.css("top", Math.max(0, (($(window).height() - this.outerHeight()) / 2) +
                                        $(window).scrollTop()) + "px");
    this.css("left", Math.max(0, (($(window).width() - this.outerWidth()) / 2) +
                                        $(window).scrollLeft()) + "px");
    return this;
}

//在页面中生成GUID的值
function newGuid() {
    var guid = "";
    for (var i = 1; i <= 32; i++) {
        var n = Math.floor(Math.random() * 16.0).toString(16);
        guid += n;
        if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
            guid += "-";
    }
    return guid;
}

//打包下载所有附件
function DownloadAttach(guid) {
    window.open('/FileUpload/DownloadAttach?guid=' + guid);
}

//在新窗口中查看附件
function ShowAttach(id, ext) {
    var showWindow = true;//标识是否使用窗口查看。office文档+图片文档窗口查看，其他的直接下载
    var viewUrl = '/FileUpload/ViewAttach';
    var returnUrl;
    //var hostname = window.location.host;
    //var hostname = 'http://www.iqidi.com'

    var postData = { id: id };
    $.ajaxSettings.async = false;
    $.get("/FileUpload/GetAttachViewUrl", postData, function (url) {
        if (ext == 'xls' || ext == 'xlsx' || ext == 'doc' || ext == 'docx' || ext == 'ppt' || ext == 'pptx') {
            viewUrl = url;
        }
        else if (ext == "png" || ext == "jpg" || ext == "jpeg" || ext == "gif" || ext == "bmp" || ext == "tif") {
            viewUrl = "/" + url;
        }
        else {
            viewUrl = "/" + url;
            showWindow = false;
        }

        returnUrl = url;
    });

    if (showWindow) {
        $.showWindow({
            title: '查看附件',
            useiframe: true,
            width: 900,
            height: 700,
            content: 'url:' + viewUrl,
            data: { url: returnUrl, ext: ext },
            buttons: [{
                text: '取消',
                iconCls: 'icon-cancel',
                handler: function (win) {
                    win.close();
                }
            }],
            onLoad: function (win, content) {
                //window打开时调用，初始化form内容
                if (content) {
                    //content.doInit(win);
                }
            }
        });
    }
    else {
        //附件直接下载，不用打开窗体
        window.open(viewUrl);
    }
}

//绑定附件列表
function ShowUpFiles(guid, show_div) {
    $.ajax({
        type: 'GET',
        url: '/FileUpload/GetAttachmentHtml?guid=' + guid,
        //async: false, //同步
        //dataType: 'json',
        success: function (json) {
            $("#" + show_div + "").html(json);
        },
        error: function (xhr, status, error) {
            $.messager.alert("提示", "操作失败" + xhr.responseText); //xhr.responseText
        }
    });
}

//绑定附件列表（查看状态）
function ViewUpFiles(guid, show_div) {
    $.ajax({
        type: 'GET',
        url: '/FileUpload/GetViewAttachmentHtml?guid=' + guid,
        success: function (json) {
            $("#" + show_div + "").html(json);
        },
        error: function (xhr, status, error) {
            $.messager.alert("提示", "操作失败" + xhr.responseText); //xhr.responseText
        }
    });
}

//删除指定的附件后，对附件组进行更新
// id 删除附件id, attachguid 附件组ID, show_div 显示附件的Div
function DeleteAndRefreshAttach(id, attachguid, show_div) {
    $.messager.confirm("删除确认", "您确定要删除该附件吗？", function (action) {
        if (action) {
            $.ajax({
                type: 'POST',
                url: '/FileUpload/Delete?id=' + id,
                async: false,
                success: function (msg) {
                    ShowUpFiles(attachguid, show_div);//更新列表
                },
                error: function (xhr, status, error) {
                    $.messager.alert("提示", "操作失败"); //xhr.responseText
                }
            });
        }
    });
}


//绑定字典内容到指定的控件
function BindDictItem(control, dictTypeName) {
    $('#' + control).combobox({
        url: '/DictData/GetDictJson?dictTypeName=' + encodeURI(dictTypeName),
        valueField: 'Value',
        textField: 'Text'
    });
}

//绑定回车键操作到指定的控件
function BindReturnEvent(control) {
    $('#' + control).bind("enterKey", function (e) {
        ConfirmScanData();
    });
    $('#' + control).keyup(function (e) {
        if (e.keyCode == 13) {
            $(this).trigger("enterKey");
        }
    });
}

//获取日期获取日期+时间的字符串
function GetCurrentDate(hasTime) {
    var curr_time = new Date();
    var strDate = curr_time.getFullYear() + "-";
    strDate += curr_time.getMonth() + 1 + "-";
    strDate += curr_time.getDate();

    if (hasTime) {
        strDate += " " + curr_time.getHours() + ":";
        strDate += curr_time.getMinutes() + ":";
        strDate += curr_time.getSeconds();
    }
    return strDate;
}

//EasyUI树控件的相关操作
function expandAll(treeName) {
    var node = $('#' + treeName).tree('getSelected');
    if (node) {
        $('#' + treeName).tree('expandAll', node.target);
    }
    else {
        $('#' + treeName).tree('expandAll');
    }
}
function collapseAll(treeName) {
    var node = $('#' + treeName).tree('getSelected');
    if (node) {
        $('#' + treeName).tree('collapseAll', node.target);
    }
    else {
        $('#' + treeName).tree('collapseAll');
    }
}
function unCheckTree(tree) {
    var nodes = $('#' + tree).tree('getChecked');
    if (nodes) {
        for (var i = 0; i < nodes.length; i++) {
            $('#' + tree).tree('uncheck', nodes[i].target);
        }
    }
}
function checkAllTree(tree, checked) {
    var children = $('#' + tree).tree('getChildren');
    for (var i = 0; i < children.length; i++) {
        if (checked) {
            $('#' + tree).tree('check', children[i].target);
        } else {
            $('#' + tree).tree('uncheck', children[i].target);
        }
    }
}



//显示错误或提示信息（需要引用jNotify相关文件）
function showError(tips, TimeShown, autoHide) {
    jError(
      tips,
      {
          autoHide: autoHide || true, // added in v2.0
          TimeShown: TimeShown || 1500,
          HorizontalPosition: 'center',
          VerticalPosition: 'top',
          ShowOverlay: true,
          ColorOverlay: '#000',
          onCompleted: function () { // added in v2.0
              //alert('jNofity is completed !');
          }
      }
    );
}
function showTips(tips, TimeShown, autoHide) {
        jSuccess(
          tips,
          {
              autoHide: autoHide || true, // added in v2.0
              TimeShown: TimeShown || 1500,
              HorizontalPosition: 'center',
              VerticalPosition: 'top',
              ShowOverlay: true,
              ColorOverlay: '#000',
              onCompleted: function () { // added in v2.0
                  //alert('jNofity is completed !');
              }
          }
        );
    }