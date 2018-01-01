function Clearnav() {
    var pp = $('#nav').accordion('panels');

    $.each(pp, function (i, n) {
        if (n) {
            var t = n.panel('options').title;
            $('#nav').accordion('remove', t);
        }
    });

    pp = $('#nav').accordion('getSelected');
    if (pp) {
        var title = pp.panel('options').title;
        $('#nav').accordion('remove', title);
    }
}

function addNav(data) {
    $.each(data, function (i, sm) {
        var menulist = "";
        menulist += '<ul>';
        $.each(sm.menus, function (j, o) {
            //如果是脚本，连接的方式不同
            if (o.url && o.url.indexOf("javascript") >= 0) {
                menulist += '<li><div><a ref="' + o.menuid + '" href="javascript:void(0)" onclick="'
                        + o.url + '" ><span class="icon ' + o.icon
                        + '" >&nbsp;</span><span class="nav">' + o.menuname
                        + '</span></a></div></li> ';
            }
            else {
                menulist += '<li><div><a ref="' + o.menuid + '" href="#" rel="'
                        + o.url + '" ><span class="icon ' + o.icon
                        + '" >&nbsp;</span><span class="nav">' + o.menuname
                        + '</span></a></div></li> ';
            }
        });
        menulist += '</ul>';

        $('#nav').accordion('add', {
            title: sm.menuname,
            content: menulist,
            iconCls: 'icon ' + sm.icon
        });

    });

    $('.easyui-accordion li a').click(function () {
        var title = $(this).children('.nav').text();

        var url = $(this).attr("rel");
        var menuid = $(this).attr("ref");
        var icon = getIcon2(data, menuid);

        var exist = $('#tabs').tabs('exists', title); //返回false或true
        if (!exist && url != undefined) {
            addTab(title, url, icon);
        }
        else {
            $('#tabs').tabs('select', title);
        }

        $('.easyui-accordion li div').removeClass("selected");
        $(this).parent().addClass("selected");
    }).hover(function () {
        $(this).parent().addClass("hover");
    }, function () {
        $(this).parent().removeClass("hover");
    });

    //选中第一个
    var pp = $('#nav').accordion('panels');
    var t = pp[0].panel('options').title;
    $('#nav').accordion('select', t);
}

/**
* 菜单项鼠标Hover
*/
function hoverMenuItem() {
    $(".easyui-accordion").find('a').hover(function () {
        $(this).parent().addClass("hover");
    }, function () {
        $(this).parent().removeClass("hover");
    });
}

$(function () {
    var data = _menus["default"];
    InitLeftMenu(data);
    tabClose();
    tabCloseEven();

    $('#tabs').tabs('add', {
        title: '首页',
        content: createFrame('http://wuhuacong.cnblogs.com')
    }).tabs({
        onSelect: function (title) {
            var currTab = $('#tabs').tabs('getTab', title);
            var iframe = $(currTab.panel('options').content);

            var exist = $('#tabs').tabs('exists', title); //返回false或true
            if (exist)
                return;

            var src = iframe.attr('src');
            if (src) {
                $('#tabs').tabs('update', { tab: currTab, options: { content: createFrame(src)} });
            }

        }
    });

})

//初始化左侧
function InitLeftMenu(data) {
    //hoverMenuItem();
	$("#nav").accordion({animate:false});

    $.each(data, function(i, n) {
		var menulist ='';
		menulist +='<ul>';
		$.each(n.menus, function (j, o) {
            //如果是脚本，连接的方式不同
		    if (o.url && o.url.indexOf("javascript") >= 0) {
		        menulist += '<li><div><a ref="' + o.menuid + '" href="javascript:void(0)" onclick="' + o.url + '" ><span class="icon ' + o.icon + '" >&nbsp;</span><span class="nav">' + o.menuname + '</span></a></div></li> ';
		    }
		    else {
		        menulist += '<li><div><a ref="' + o.menuid + '" href="#" rel="' + o.url + '" ><span class="icon ' + o.icon + '" >&nbsp;</span><span class="nav">' + o.menuname + '</span></a></div></li> ';
		    }
        })
		menulist += '</ul>';
		//alert(menulist);
		
		$('#nav').accordion('add', {
            title: n.menuname,
            content: menulist,
            iconCls: n.icon
        });

    });

	$('.easyui-accordion li a').click(function(){
		var title = $(this).children('.nav').text();

		var url = $(this).attr("rel");
		var menuid = $(this).attr("ref");
		var icon = getIcon2(data, menuid);
		
		var exist = $('#tabs').tabs('exists', title);//返回false或true
		if (url) {
		    if (!exist) {
		        addTab(title, url, icon);
		    }
		    else {
		        $('#tabs').tabs('select', title);
		    }
		}
		
		$('.easyui-accordion li div').removeClass("selected");
		$(this).parent().addClass("selected");
	}).hover(function(){
		$(this).parent().addClass("hover");
	},function(){
		$(this).parent().removeClass("hover");
	});

	//选中第一个
	var panels = $('#nav').accordion('panels');
	var t = panels[0].panel('options').title;
    $('#nav').accordion('select', t);
}

//获取左侧导航的图标
function getIcon2(data, menuid) {
    var icon = 'icon ';
    $.each(data, function (k, item) {        
        $.each(item.menus, function (i, o) {
            if (o.menuid == menuid) {
                icon += o.icon;
            }
        })
    })
    return icon;
}

//获取左侧导航的图标
function getIcon(menuid){
	var icon = 'icon ';
	$.each(_menus.menus, function(i, n) {
		 $.each(n.menus, function(j, o) {
		 	if(o.menuid==menuid){
				icon += o.icon;				
			}
		 })
	})
	
	return icon;
}

function addTab(subtitle,url,icon){
	if(!$('#tabs').tabs('exists',subtitle)){
		$('#tabs').tabs('add',{
			title:subtitle,
			content:createFrame(url),
			closable:true,
			icon:icon
		});
	}else{
		$('#tabs').tabs('select',subtitle);
		$('#mm-tabupdate').click();
	}
	tabClose();
}

function createFrame(url)
{
    var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:99%;overflow:hidden"></iframe>';
	return s;
}

function tabClose()
{
	/*双击关闭TAB选项卡*/
	$(".tabs-inner").dblclick(function(){
		var subtitle = $(this).children(".tabs-closable").text();
		$('#tabs').tabs('close',subtitle);
	})
	/*为选项卡绑定右键*/
	$(".tabs-inner").bind('contextmenu',function(e){
		$('#mm').menu('show', {
			left: e.pageX,
			top: e.pageY
		});

		var subtitle =$(this).children(".tabs-closable").text();

		$('#mm').data("currtab",subtitle);
		$('#tabs').tabs('select',subtitle);
		return false;
	});
}
//绑定右键菜单事件
function tabCloseEven()
{
	//刷新
	$('#mm-tabupdate').click(function(){
		var currTab = $('#tabs').tabs('getSelected');
		var url = $(currTab.panel('options').content).attr('src');
		$('#tabs').tabs('update',{
			tab:currTab,
			options:{
				content:createFrame(url)
			}
		})
	})
	//关闭当前
	$('#mm-tabclose').click(function(){
		var t = $('#mm').data("currtab");
		if (t !== "首页") {
		    $('#tabs').tabs('close', t);//currtab_title
		}
	})
	//全部关闭
	$('#mm-tabcloseall').click(function(){
		$('.tabs-inner span').each(function(i,n){
			var t = $(n).text();
			if(t !== "首页")
				$('#tabs').tabs('close',t);
		});
	});
	//关闭除当前之外的TAB
	$('#mm-tabcloseother').click(function(){
		$('#mm-tabcloseright').click();
		$('#mm-tabcloseleft').click();
	});
	//关闭当前右侧的TAB
	$('#mm-tabcloseright').click(function(){
		var nextall = $('.tabs-selected').nextAll();
		if(nextall.length==0){
			//alert('后边没有啦~~');
			return false;
		}
		nextall.each(function(i,n){
			var t=$('a:eq(0) span',$(n)).text();
			if(t !== "首页")
			   $('#tabs').tabs('close',t);
		});
		return false;
	});
	//关闭当前左侧的TAB
	$('#mm-tabcloseleft').click(function(){
		var prevall = $('.tabs-selected').prevAll();
		if(prevall.length==0){
			//alert('到头了，前边没有啦~~');
			return false;
		}
		prevall.each(function(i,n){
			var t=$('a:eq(0) span',$(n)).text();
			if(t !== "首页")
			    $('#tabs').tabs('close',t);
		});
		return false;
	});

	//退出
	$("#mm-exit").click(function(){
		$('#mm').menu('hide');
	})
}

//弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
	$.messager.alert(title, msgString, msgType);
}
