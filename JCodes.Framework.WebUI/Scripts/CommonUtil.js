//本文件定义一些在各个视图里面经常用到的一些Javascript脚本函数

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

//绑定回车键操作到指定的控件
function BindReturnEvent(ctrlName) {
    var control = $('#' + ctrlName);
    control.bind("enterKey", function (e) {
        ConfirmScanData();
    });
    control.keyup(function (e) {
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

//绑定字典内容到指定的Select控件
function BindSelect(ctrlName, url) {
    var control = $('#' + ctrlName);
    //设置Select2的处理
    control.select2({
        allowClear: true,
        formatResult: formatResult,
        formatSelection: formatSelection,
        escapeMarkup: function (m) {
            return m;
        }
    });

    //绑定Ajax的内容
    $.getJSON(url, function (data) {
        control.empty();//清空下拉框
        $.each(data, function (i, item) {
            control.append("<option value='" + item.Value + "'>&nbsp;" + item.Text + "</option>");
        });
    });
}

//格式化Select的选择结果
function formatSelection(state) {
    if (!state.id) return state.text; // optgroup
    return "<i class=\"fa fa-file-text-o icon-state-success\"></i>&nbsp;" + $.trim(state.text);
}
//格式化Select的列表
function formatResult(state) {
    if (!state.id) return state.text; // optgroup    
    return "<i class=\"fa fa-file-text-o icon-state-success\"></i>" + state.text;
}

//绑定字典内容到指定的控件
function BindDictItem(ctrlName, dictTypeName) {
    var url = '/DictData/GetDictJson?dictTypeName=' + encodeURI(dictTypeName);
    BindSelect(ctrlName, url);
}


//全选/取消全选
function selectAll(obj) {
    if ($(obj).is(':checked')) {
        $("[name='checkbox']").each(function () {
            $(this).prop("checked", true);
            $(this).parent().addClass("checked");
        });
    } else {
        $("[name='checkbox']").each(function () {
            $(this).prop("checked", false);
            $(this).parent().removeClass("checked");
        });
    }
}

//删除操作的确认
function showDelete(delFunction, tips) {
    var newtips = arguments[1] || "您确认删除选定的记录吗？"
    var result = false;
    swal({
        title: "操作提示",
        text: newtips,
        type: "warning", showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        cancelButtonText: "取消",
        confirmButtonText: "是的，执行删除！",
        closeOnConfirm: true
    }, function () {
        delFunction();
    });
    return result;
}

//绑定窗体的验证处理
function formValidate(ctrlName, submitHandler) {
    var control = $('#' + ctrlName);
    //判断表单的信息是否通过验证
    control.validate({
        meta: "validate",
        errorElement: 'span',
        errorClass: 'help-block help-block-error',
        focusInvalid: false,
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        success: function (label) {
            label.closest('.form-group').removeClass('has-error');
            label.remove();
        },
        errorPlacement: function (error, element) {
            element.parent('div').append(error);
        },
        submitHandler: submitHandler
    });
}

//使用toastr控件
function showToast(tips, toastType) {
    toastr.options = {
      "closeButton": false, //是否显示关闭按钮
      "debug": false,       //是否使用debug模式
      "newestOnTop": false,
      "progressBar": false,
      "positionClass": "toast-top-right",//弹出窗的位置
      "preventDuplicates": false,
      "onclick": null,
      "showDuration": "300",    //显示的动画时间
      "hideDuration": "1000",   //消失的动画时间
      "timeOut": "5000",        //展现时间
      "extendedTimeOut": "1000",//加长展示时间
      "showEasing": "swing",    //显示时的动画缓冲方式
      "hideEasing": "linear",   //消失时的动画缓冲方式
      "showMethod": "fadeIn",   //显示时的动画方式
      "hideMethod": "fadeOut"   //消失时的动画方式
    };
    var toastType = arguments[1] || "success"; //success，warning, error
    toastr[toastType](tips);
}

//显示错误或提示信息（需要引用jNotify相关文件）
function showError(tips, TimeShown, autoHide) {
    jError(
      tips,
      {
          autoHide: autoHide || true,   // 是否自动隐藏提示条
          TimeShown: TimeShown || 1500, // 显示时间：毫秒
          HorizontalPosition: 'center', // 水平位置:left, center, right
          VerticalPosition: 'top',      // 垂直位置：top, center, bottom
          ShowOverlay: true,            // 是否显示遮罩层
          ColorOverlay: '#000',         // 设置遮罩层的颜色
          onCompleted: function () {    // 完成的处理代码
              //alert('jNofity is completed !');
          }
      }
    );
}

//显示提示信息
function showTips(tips, TimeShown, autoHide) {
    jSuccess(
      tips,
      {
          autoHide: autoHide || true,   // 是否自动隐藏提示条
          TimeShown: TimeShown || 1500, // 显示时间：毫秒
          HorizontalPosition: 'center', // 水平位置:left, center, right
          VerticalPosition: 'top',      // 垂直位置：top, center, bottom
          ShowOverlay: true,            // 是否显示遮罩层
          ColorOverlay: '#000',         // 设置遮罩层的颜色
          onCompleted: function () {    // 完成的处理代码
              //alert('jNofity is completed !');
          }
      }
    );
}

//删除相关查看、编辑、删除的功能操作HTML代码
function getActionHtml(id) {
    var tr = "";
    tr += "<td>";
    tr += "<a href='javascript:;' onclick=\"EditViewById('" + id + "', view='view')\" title='查看'><span class='glyphicon glyphicon-search'></span></a>&nbsp;";
    tr += "<a href='javascript:;' onclick=\"EditViewById('" + id + "')\" title='编辑'><span class='glyphicon glyphicon-pencil'></span></a>&nbsp;";
    tr += "<a href='javascript:;' onclick=\"DeleteByIds('" + id + "')\" title='删除'><span class='glyphicon glyphicon-remove'></span></a>";
    tr += "</td>";

    return tr;
}

//删除相关查看、编辑、删除的功能操作HTML代码
function getViewActionHtml(id) {
    var tr = "";
    tr += "<td>";
    tr += "<a href='javascript:;' onclick=\"EditViewById('" + id + "', view='view')\" title='查看'><span class='glyphicon glyphicon-search'></span></a>&nbsp;";
    tr += "</td>";

    return tr;
}

//以指定的Json数据，初始化JStree控件
//treeName为树div名称，url为数据源地址，checkbox为是否显示复选框，loadedfunction为加载完毕的回调函数
function bindJsTree(treeName, url, checkbox, loadedfunction) {
    var control = $('#' + treeName)
    control.data('jstree', false);//清空数据，必须

    var isCheck = arguments[2] || false; //设置checkbox默认值为false
    if(isCheck) {
        //复选框树的初始化
        $.getJSON(url, function (data) {
            control.jstree({
                'plugins' : [ "checkbox" ], //出现选择框
                'checkbox': { cascade: "", three_state: false }, //不级联
                'core': {
                    'data': data,
                    "themes": {
                        "responsive": false
                    }
                }
            }).bind('loaded.jstree', loadedfunction);
        });
    }
    else {
        //普通树列表的初始化
        $.getJSON(url, function (data) {
            control.jstree({
                'core': {
                    'data': data,
                    "themes": {
                        "responsive": false
                    }
                }
            }).bind('loaded.jstree', loadedfunction);
        });        
    }
}

//执行导出操作，输出文件
function executeExport(url, condition) {
    $.ajax({
        type: "POST",
        url: url,
        data: condition,
        success: function (filePath) {
            var downUrl = '/FileUpload/DownloadFile?file=' + filePath;
            window.location = downUrl;
        }
    });
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
    var type= "";

    $.ajaxSettings.async = false;
    $.get("/FileUpload/GetAttachViewUrl", postData, function (url) {
        if (ext == 'xls' || ext == 'xlsx' || ext == 'doc' || ext == 'docx' || ext == 'ppt' || ext == 'pptx') {
            viewUrl = url;
        }
        else if (ext == "png" || ext == "jpg" || ext == "jpeg" || ext == "gif" || ext == "bmp" || ext == "tif") {
            viewUrl = "/" + url;
            type="image";
        }
        else {
            viewUrl = "/" + url;
            showWindow = false;
        }

        returnUrl = url;
    });

    if (showWindow) {
        if(type =="image") {
            var imgContent = '<img src="'+ viewUrl + '" />';
            $("#divViewFile").html(imgContent);
            $("#file").modal("show");
        } else {
            $.ajax({
                type: 'GET',
                url: viewUrl,
                //async: false, //同步
                //dataType: 'json',
                success: function (json) {
                    $("#divViewFile").html(json);
                    $("#file").modal("show");
                },
                error: function (xhr, status, error) {
                    showError("操作失败" + xhr.responseText); //xhr.responseText
                }
            }); 
        }     
    }
    else {
        //附件直接下载，不用打开窗体
        window.open(viewUrl);
    }
}

//个人助理-计算器
function calculator() {
    var viewUrl = '/Content/calculate.htm';
    $.ajax({
        type: 'GET',
        url: viewUrl,
        //async: false, //同步
        //dataType: 'json',
        success: function (json) {
            $("#divViewFile").html(json);
            $("#lblFileTitle").text("计算器");
            $("#file").modal("show");
        },
        error: function (xhr, status, error) {
            showError("操作失败" + xhr.responseText); //xhr.responseText
        }
    });  
}

//个人助理-公元农历
function calendar() {    
    var viewUrl = '/Content/almanac.htm';
    $.ajax({
        type: 'GET',
        url: viewUrl,
        //async: false, //同步
        //dataType: 'json',
        success: function (json) {
            $("#divViewFile").html(json);
            $("#lblFileTitle").text("公元农历");
            $("#file").modal("show");
        },
        error: function (xhr, status, error) {
            showError("操作失败" + xhr.responseText); //xhr.responseText
        }
    });
}

//绑定附件列表
function ShowUpFiles(guid, show_div) {
    $.ajax({
        type: 'GET',
        url: '/FileUpload/GetAttachmentBootstrap?guid=' + guid,
        //async: false, //同步
        //dataType: 'json',
        success: function (json) {
            $("#" + show_div + "").html(json);
        },
        error: function (xhr, status, error) {
            showError("操作失败" + xhr.responseText); //xhr.responseText
        }
    });
}

//绑定附件列表（查看状态）
function ViewUpFiles(guid, show_div) {
    $.ajax({
        type: 'GET',
        url: '/FileUpload/GetViewAttachmentBootstrap?guid=' + guid,
        success: function (json) {
            $("#" + show_div + "").html(json);
        },
        error: function (xhr, status, error) {
            showError("操作失败" + xhr.responseText); //xhr.responseText
        }
    });
}

//删除指定的附件后，对附件组进行更新
// id 删除附件id, attachguid 附件组ID, show_div 显示附件的Div
function DeleteAndRefreshAttach(id, attachguid, show_div) {
    bootbox.confirm("您确定要删除该附件吗？", function (result) {
       if (result) {
            $.ajax({
                type: 'POST',
                url: '/FileUpload/Delete?id=' + id,
                async: false,
                success: function (msg) {
                    ShowUpFiles(attachguid, show_div);//更新列表
                },
                error: function (xhr, status, error) {
                    showError("操作失败"); //xhr.responseText
                }
            });
       }
    });
}

//初始化fileinput控件（第一次初始化）
function initFileInput(ctrlName, uploadUrl) {    
    var control = $('#' + ctrlName); 

    control.fileinput({
        language: 'zh', //设置语言
        uploadUrl: uploadUrl, //上传的地址
        allowedFileExtensions : ['jpg', 'png','gif'],//接收的文件后缀
        showUpload: false, //是否显示上传按钮
        showCaption: false,//是否显示标题
        browseClass: "btn btn-primary", //按钮样式             
        previewFileIcon: "<i class='glyphicon glyphicon-king'></i>", 
        // overwriteInitial: false, //是否覆盖原图
        // dropZoneEnabled:false, //是否显示拖动区域               
        // uploadExtraData: {id: id}, //附加内容，修改需要使用refresh重新设置
        // initialPreview: [  //预览图片的设置
        //     "<img src='" + imageurl + "' class='file-preview-image' alt='肖像图片' title='肖像图片'>",
        // ],
    });
}



//EasyUI树控件的相关操作
function expandAll(treeName) {
    var control = $('#' + treeName)
    var node = control.tree('getSelected');
    if (node) {
        control.tree('expandAll', node.target);
    }
    else {
        control.tree('expandAll');
    }
}
function collapseAll(treeName) {
    var control = $('#' + treeName)
    var node = control.tree('getSelected');
    if (node) {
        control.tree('collapseAll', node.target);
    }
    else {
        control.tree('collapseAll');
    }
}
function unCheckTree(treeName) {
    var control = $('#' + treeName)
    var nodes = control.tree('getChecked');
    if (nodes) {
        for (var i = 0; i < nodes.length; i++) {
            control.tree('uncheck', nodes[i].target);
        }
    }
}
function checkAllTree(treeName, checked) {
    var control = $('#' + treeName)
    var children = control.tree('getChildren');
    for (var i = 0; i < children.length; i++) {
        if (checked) {
            control.tree('check', children[i].target);
        } else {
            control.tree('uncheck', children[i].target);
        }
    }
}

//字符串转日期格式，strDate要转为日期格式的字符串 
function getDateStr(strDate) {
    var st = strDate;
    var a = st.split(" ");
    var b = a[0].split("-");
    var c = a[1].split(":");

    return b[0] + "-" + b[1] + "-" + b[2];
}

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