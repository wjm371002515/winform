﻿<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=ConfigurationManager.AppSettings["title"] %></title>
    <link rel="shortcut icon" type="image/ico" href="images/favicon.ico" />
    <link href="themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="css/table.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="js/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="js/common.js" type="text/javascript"></script>
    <style type="text/css">
        .code
        {
            background-color: White;
            font-family: Arial;
            font-style: italic;
            color: Red;
            border: 0;
            padding: 2px 3px;
            letter-spacing: 3px;
            font-weight: bolder;
            cursor: pointer;
            width: 60px;
        }
        .unchanged
        {
            border: 0;
        }
    </style>
    <!--[if lte IE 8]>
    <script type="text/javascript">
            // 如果IE8或者一下的浏览器去兼容json对象
            if(typeof JSON == 'undefined') {
                $('head').append($("<script type='text/javascript' src='js/json2.js'>"));
            }
	</script>
	<![endif]-->
    <script type="text/javascript">
        var city;     //访问者所在城市
        $(function () {
            $("#loginDialog").dialog({
                title: "系统登录",
                closable: false,
                modal: true,
                width: 400,
                height: 300,
                buttons: [{
                    id: "loginBtn",
                    text: "登 录",
                    handler: function () {
                        if ($("#loginFrm").form('validate')) {
                            $("#loginFrm").submit();
                        }
                    }
                }]
            });

            var url = location.href;
            //登录
            $("#loginFrm").form({
                url: 'ashx/bg_user_login.ashx',
                onSubmit: function (param) {
                    param.action = "login";
                    param.city = city;    //访问者所在城市
                    return checkInput();   //验证码是否正确
                },
                success: function (data) {
                    var result = eval('(' + data + ')');  //字符串转json
                    if (result.success) {
                        if (url.indexOf("ReturnUrl=") != -1) {
                            window.location.href = decodeURIComponent(url.substring(url.indexOf("ReturnUrl=") + 10));
                        }
                        else {
                            window.location.href = "index.aspx";
                        }
                    } else {
                        $.show_warning("提示", result.msg);
                        $('#loginBtn').linkbutton('enable');
                        createCode();   //刷新验证码
                        $("#loginVerificationCode").val("");
                    }
                }
            })

            //回车提交表单
            $("#loginFrm").find('input').on('keyup', function (event) {
                if (event.keyCode == '13') {
                    //登录按钮如果处于禁用状态，那么回车也不提交表单
                    if (!($('#loginBtn').linkbutton("options").disabled) && $("#loginFrm").form('validate')) {
                        $("#loginFrm").submit();
                    }
                }
            })
            createCode();  //创建验证码
        })

        function getiflogin() {
            $.ajax({
                url: "ashx/bg_user_login.ashx",
                type: "post",
                dataType: "text/plain", // 这里测试json 对应的兼容性问题
                data: "action=iflogin&city=" + city,
                success: function (result) {
                    if (result.success) {
                        window.location.href = "index.aspx";
                    }
                },
                beforeSend: function () {
                    $('#loginBtn').linkbutton('disable');   //点击登陆后先禁用登录按钮，防止连击
                },
                complete: function () {
                    $('#loginBtn').linkbutton('enable');
                }
            });
        }

        //创建验证码
        var code;
        function createCode() {
            code = "";
            var codeLength = 4;     //验证码长度   
            var checkCode = document.getElementById("checkCode");
            var selectChar = new Array(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z');
            for (var i = 0; i < codeLength; i++) {
                var charIndex = Math.floor(Math.random() * 36);
                code += selectChar[charIndex];
            }
            if (checkCode) {
                checkCode.className = "code";
                checkCode.value = code;
                //$("#loginVerificationCode").val(code);   //自动填写验证码
            }
        }

        //检查验证码
        function checkInput() {
            if ($.trim($("#loginVerificationCode").val()) == "") {
                $("#msg").text("验证码不能为空");
                $("#msg").stop(true, true).animate({ opacity: "show" }, 300, function () {//.stop使动画达到末尾，否则连按回车一直显示隐藏
                    $("#msg").animate({ opacity: "hide" }, 2000);
                });
                $("#loginVerificationCode").val("").focus();
                return false;
            }
            else if (document.getElementById("loginVerificationCode").value.toUpperCase() != code) {
                $("#msg").text("验证码错误@_@！");
                $("#msg").stop(true, true).animate({ opacity: "show" }, 300, function () {
                    $("#msg").animate({ opacity: "hide" }, 2000);
                });
                $("#loginVerificationCode").val("").focus();
                createCode();       //刷新验证码
                return false;
            } else {
                $('#loginBtn').linkbutton('disable');
                return true;
            }
        }
    </script>
</head>
<body>
    <noscript>
        <div style="position: absolute; z-index: 100000; height: 2046px; top: 0px; left: 0px;
            width: 100%; background: white; text-align: center;">
            <h1>
                必须打开浏览器的javascript支持！！！</h1>
        </div>
    </noscript>
    <div id="loginDialog" style="padding: 15px; text-align: center">
        <form id="loginFrm" method="post" runat="server">
        <table class="tableForm" style="width: 100%">
            <tr>
                <th>
                    用户名：
                </th>
                <td>
                    <input type="text" name="loginName" class="easyui-validatebox" required="true" />
                </td>
            </tr>
            <tr>
                <th>
                    密&nbsp;码：
                </th>
                <td>
                    <input type="password" name="loginPwd" class="easyui-validatebox" required="true" />
                </td>
            </tr>
            <tr>
                <th>
                    验证码：
                </th>
                <td>
                    <input type="text" id="loginVerificationCode" autocomplete="off" />
                    <input type="text" onclick="createCode();$('#loginVerificationCode').val('').focus();"
                        readonly="readonly" id="checkCode" class="unchanged" title="点击刷新验证码" />
                </td>
            </tr>
            <tr>
                <th>
                    记住我：
                </th>
                <td>
                    <select name="remember">
                        <option value="notremember">不记住</option>
                        <option value="oneday">一天</option>
                        <option value="sevenday" selected="selected">七天</option>
                        <option value="onemouth">一个月</option>
                        <option value="oneyear">一年</option>
                    </select>
                    <span id="msg" style="color: Red"></span>
                </td>
            </tr>
        </table>
        </form>
        <span style="float:right;margin-top:2px;"><a href="http://jcodes.cn" style="color:#666666; text-decoration:none;">个人网站&nbsp;</a>最终版权归吴建明所有&nbsp;QQ：371002515</span>
    </div>
</body>
</html>