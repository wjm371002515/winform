/**
 * resizable - jQuery EasyUI
 * 
 * Copyright (c) 2009-2013 www.jeasyui.com. All rights reserved.
 *
 * Licensed under the GPL or commercial licenses
 * To use it on other terms please contact us: jeasyui@gmail.com
 * http://www.gnu.org/licenses/gpl.txt
 * http://www.jeasyui.com/license_commercial.php
 */
(function($){
	var isResizing = false;
	var isIE = (function() {   
		var undef, v = 3, div = document.createElement('div'), all = div.getElementsByTagName('i');   
		while (div.innerHTML = '<!--[if gt IE ' + (++v) + ']><i></i><![endif]-->', all[0]);   
		return v > 4 ? v : undef;   
	}());
	$.fn.resizable = function(options, param){
		if (typeof options == 'string'){
			return $.fn.resizable.methods[options](this, param);
		}
		
		function resize(e){
			var resizeData = e.data;
			var options = $.data(resizeData.target, 'resizable').options;
			if (resizeData.dir.indexOf('e') != -1) {
				var width = resizeData.startWidth + e.pageX - resizeData.startX;
				width = Math.min(
							Math.max(width, options.minWidth),
							options.maxWidth
						);
				resizeData.width = width;
			}
			if (resizeData.dir.indexOf('s') != -1) {
				var height = resizeData.startHeight + e.pageY - resizeData.startY;
				height = Math.min(
						Math.max(height, options.minHeight),
						options.maxHeight
				);
				resizeData.height = height;
			}
			if (resizeData.dir.indexOf('w') != -1) {
				resizeData.width = resizeData.startWidth - e.pageX + resizeData.startX;
				if (resizeData.width >= options.minWidth && resizeData.width <= options.maxWidth) {
					resizeData.left = resizeData.startLeft + e.pageX - resizeData.startX;
				}
			}
			if (resizeData.dir.indexOf('n') != -1) {
				resizeData.height = resizeData.startHeight - e.pageY + resizeData.startY;
				if (resizeData.height >= options.minHeight && resizeData.height <= options.maxHeight) {
					resizeData.top = resizeData.startTop + e.pageY - resizeData.startY;
				}
			}
		}
		
		function applySize(e){
			var resizeData = e.data;
			var target = resizeData.target;
			$(target).css({
				left: resizeData.left,
				top: resizeData.top
			});
			$(target)._outerWidth(resizeData.width)._outerHeight(resizeData.height);
		}
		
		function doDown(e){
			isResizing = true;
			if(isIE===8){
				if(e.data.target.setCapture){
					e.data.target.setCapture();
				}else if(window.captureEvents){
					window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP);
				}
			}
			$.data(e.data.target, 'resizable').options.onStartResize.call(e.data.target, e);
			return false;
		}
		
		function doMove(e){
			resize(e);
			if ($.data(e.data.target, 'resizable').options.onResize.call(e.data.target, e) != false){
				applySize(e)
			}
			return false;
		}
		
		function doUp(e){
			isResizing = false;
			resize(e, true);
			applySize(e);
			if(isIE===8){
				if(e.data.target.releaseCapture){
					e.data.target.releaseCapture();
				}else if(window.captureEvents){
					window.captureEvents(Event.MOUSEMOVE|Event.MOUSEUP);
				}
			}
			$.data(e.data.target, 'resizable').options.onStopResize.call(e.data.target, e);
			$(document).unbind('.resizable');
			$('body').css('cursor','');
//			$('body').css('cursor','auto');
			return false;
		}
		
		return this.each(function(){
			var opts = null;
			var state = $.data(this, 'resizable');
			if (state) {
				$(this).unbind('.resizable');
				opts = $.extend(state.options, options || {});
			} else {
				opts = $.extend({}, $.fn.resizable.defaults, $.fn.resizable.parseOptions(this), options || {});
				$.data(this, 'resizable', {
					options:opts
				});
			}
			
			if (opts.disabled == true) {
				return;
			}
			
			// bind mouse event using namespace resizable
			$(this).bind('mousemove.resizable', {target:this}, function(e){
				if (isResizing) return;
				var dir = getDirection(e);
				if (dir == '') {
					$(e.data.target).css('cursor', '');
				} else {
					$(e.data.target).css('cursor', dir + '-resize');
				}
			}).bind('mouseleave.resizable', {target:this}, function(e){
				$(e.data.target).css('cursor', '');
			}).bind('mousedown.resizable', {target:this}, function(e){
				var dir = getDirection(e);
				if (dir == '') return;
				
				function getCssValue(css) {
					var val = parseInt($(e.data.target).css(css));
					if (isNaN(val)) {
						return 0;
					} else {
						return val;
					}
				}
				
				var data = {
					target: e.data.target,
					dir: dir,
					startLeft: getCssValue('left'),
					startTop: getCssValue('top'),
					left: getCssValue('left'),
					top: getCssValue('top'),
					startX: e.pageX,
					startY: e.pageY,
					startWidth: $(e.data.target).outerWidth(),
					startHeight: $(e.data.target).outerHeight(),
					width: $(e.data.target).outerWidth(),
					height: $(e.data.target).outerHeight(),
					deltaWidth: $(e.data.target).outerWidth() - $(e.data.target).width(),
					deltaHeight: $(e.data.target).outerHeight() - $(e.data.target).height()
				};
				$(document).bind('mousedown.resizable', data, doDown);
				$(document).bind('mousemove.resizable', data, doMove);
				$(document).bind('mouseup.resizable', data, doUp);
				$('body').css('cursor', dir+'-resize');
			});
			
			// get the resize direction
			function getDirection(e) {
				var tt = $(e.data.target);
				var dir = '';
				var offset = tt.offset();
				var width = tt.outerWidth();
				var height = tt.outerHeight();
				var edge = opts.edge;
				if (e.pageY > offset.top && e.pageY < offset.top + edge) {
					dir += 'n';
				} else if (e.pageY < offset.top + height && e.pageY > offset.top + height - edge) {
					dir += 's';
				}
				if (e.pageX > offset.left && e.pageX < offset.left + edge) {
					dir += 'w';
				} else if (e.pageX < offset.left + width && e.pageX > offset.left + width - edge) {
					dir += 'e';
				}
				
				var handles = opts.handles.split(',');
				for(var i=0; i<handles.length; i++) {
					var handle = handles[i].replace(/(^\s*)|(\s*$)/g, '');
					if (handle == 'all' || handle == dir) {
						return dir;
					}
				}
				return '';
			}
			
			
		});
	};
	
	$.fn.resizable.methods = {
		options: function(jq){
			return $.data(jq[0], 'resizable').options;
		},
		enable: function(jq){
			return jq.each(function(){
				$(this).resizable({disabled:false});
			});
		},
		disable: function(jq){
			return jq.each(function(){
				$(this).resizable({disabled:true});
			});
		}
	};
	
	$.fn.resizable.parseOptions = function(target){
		var t = $(target);
		return $.extend({},
				$.parser.parseOptions(target, [
					'handles',{minWidth:'number',minHeight:'number',maxWidth:'number',maxHeight:'number',edge:'number'}
				]), {
			disabled: (t.attr('disabled') ? true : undefined)
		})
	};
	
	$.fn.resizable.defaults = {
		disabled:false,
		handles:'n, e, s, w, ne, se, sw, nw, all',
		minWidth: 10,
		minHeight: 10,
		maxWidth: 10000,//$(document).width(),
		maxHeight: 10000,//$(document).height(),
		edge:5,
		onStartResize: function(e){},
		onResize: function(e){},
		onStopResize: function(e){}
	};
	
})(jQuery);



//以下是针对EasyUI的特性进行处理
var ie = (function () {
    var undef, v = 3, div = document.createElement('div'), all = div.getElementsByTagName('i');
    while (div.innerHTML = '<!--[if gt IE ' + (++v) + ']><i></i><![endif]-->', all[0]);
    return v > 4 ? v : undef;
}());
/**
    * add by cgh
    * 针对panel window dialog三个组件调整大小时会超出父级元素的修正
    * 如果父级元素的overflow属性为hidden，则修复上下左右个方向
    * 如果父级元素的overflow属性为非hidden，则只修复上左两个方向
 */
var easyuiPanelOnResize = function (width, height) {
    if (!$.data(this, 'window') && !$.data(this, 'dialog')) return;

    if (ie === 8) {
        var data = $.data(this, "window") || $.data(this, "dialog");
        if (data.pmask) {
            var masks = data.window.nextAll('.window-proxy-mask');
            if (masks.length > 1) {
                $(masks[1]).remove();
                masks[1] = null;
            }
        }
    }
    if ($(this).panel('options').maximized == true) {
        $(this).panel('options').fit = false;
    }
    $(this).panel('options').reSizing = true;
    if (!$(this).panel('options').reSizeNum) {
        $(this).panel('options').reSizeNum = 1;
    } else {
        $(this).panel('options').reSizeNum++;
    }
    var parentObj = $(this).panel('panel').parent();
    var left = $(this).panel('panel').position().left;
    var top = $(this).panel('panel').position().top;

    if ($(this).panel('panel').offset().left < 0) {
        $(this).panel('move', {
            left: 0
        });
    }
    if ($(this).panel('panel').offset().top < 0) {
        $(this).panel('move', {
            top: 0
        });
    }

    if (left < 0) {
        $(this).panel('move', {
            left: 0
        }).panel('resize', {
            width: width + left
        });
    }
    if (top < 0) {
        $(this).panel('move', {
            top: 0
        }).panel('resize', {
            height: height + top
        });
    }
    if (parentObj.css("overflow") == "hidden") {
        var inline = $.data(this, "window").options.inline;
        if (inline == false) {
            parentObj = $(window);
        }

        if ((width + left > parentObj.width()) && $(this).panel('options').reSizeNum > 1) {
            $(this).panel('resize', {
                width: parentObj.width() - left
            });
        }

        if ((height + top > parentObj.height()) && $(this).panel('options').reSizeNum > 1) {
            $(this).panel('resize', {
                height: parentObj.height() - top
            });
        }
    }
    $(this).panel('options').reSizing = false;
};
/**
 * add by cgh
 * 针对panel window dialog三个组件拖动时会超出父级元素的修正
 * 如果父级元素的overflow属性为hidden，则修复上下左右个方向
 * 如果父级元素的overflow属性为非hidden，则只修复上左两个方向
*/
var easyuiPanelOnMove = function (left, top) {
    if ($(this).panel('options').reSizing)
        return;
    var parentObj = $(this).panel('panel').parent();
    var width = $(this).panel('options').width;
    var height = $(this).panel('options').height;
    var right = left + width;
    var buttom = top + height;
    var parentWidth = parentObj.width();
    var parentHeight = parentObj.height();

    if (left < 0) {
        $(this).panel('move', {
            left: 0
        });
    }
    if (top < 0) {
        $(this).panel('move', {
            top: 0
        });
    }

    if (parentObj.css("overflow") == "hidden") {
        var inline = $.data(this, "window").options.inline;
        if (inline == false) {
            parentObj = $(window);
        }
        if (left > parentObj.width() - width) {
            $(this).panel('move', {
                "left": parentObj.width() - width
            });
        }
        if (top > parentObj.height() - height) {
            $(this).panel('move', {
                "top": parentObj.height() - height
            });
        }
    }
};

$.fn.panel.defaults.onResize = easyuiPanelOnResize;
$.fn.window.defaults.onResize = easyuiPanelOnResize;
$.fn.dialog.defaults.onResize = easyuiPanelOnResize;
$.fn.window.defaults.onMove = easyuiPanelOnMove;
$.fn.panel.defaults.onMove = easyuiPanelOnMove;
$.fn.dialog.defaults.onMove = easyuiPanelOnMove;
