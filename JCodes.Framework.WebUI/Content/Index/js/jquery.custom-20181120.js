
(function($){ //create closure so we can safely use $ as alias for jQuery

    $(document).ready(function(){

        "use strict";

        //if(navigator.userAgent.toLowerCase().indexOf('firefox') > -1){
            $(".featured-slide a.thumbnail-link").attr("target", "_self");
        //}

        /*-----------------------------------------------------------------------------------*/
        /*  Superfish Menu
        /*-----------------------------------------------------------------------------------*/
        // initialise plugin
        if ($(window).width() >= 960) {
            var example = $('.sf-menu').superfish({
                //add options here if required
                delay:       100,
                speed:       'fast',
                autoArrows:  false  
            }); 
        }

        /*-----------------------------------------------------------------------------------*/
        /*  bxSlider
        /*-----------------------------------------------------------------------------------*/
        $('#featured-slider .bxslider').bxSlider({
            auto: true,
            preloadImages: 'all',
            pause: '4000',
            autoHover: true,
            adaptiveHeight: true,
            mode: 'fade',
            onSliderLoad: function(){ 
                $("#featured-slider .bxslider").css("display", "block");
                $('#featured-slider .entry-header').fadeIn("100");
                $(".ribbon").fadeIn('1000'); 
            }
        });                           

        $('.gallery-slider').show().bxSlider({
            auto: true,
            preloadImages: 'all',
            pause: '4000',
            autoHover: true,
            adaptiveHeight: true,
            //mode: 'fade',
            onSliderLoad: function(){ 
                $(".single #primary .gallery-slider").css("display", "block");
                $(".single #primary .bx-wrapper").css("visibility", "visible");    
                $(".sidebar .widget_media_gallery .gallery-slider").css("display", "block");
                $(".sidebar .widget_media_gallery .bx-wrapper").css("visibility", "visible");                                 
            }
        });  

        $(window).load(function() {
            // executes when complete page is fully loaded, including all frames, objects and images
            $(".custom-share").fadeIn('1000'); 
            $("#featured-grid .entry-header").fadeIn('1000'); 
            $(".bottom-right").fadeIn('1000'); 
            $(".widget_posts_thumbnail .entry-wrap").fadeIn('1000');  
            $(".breadcrumbs.is_zhuanti h1").fadeIn('1000');  
                     
        });
               

        /*-----------------------------------------------------------------------------------*/
        /*  Back to Top
        /*-----------------------------------------------------------------------------------*/
        // hide #back-top first
        //$("#back-top").hide();

        $(function () {
            // fade in #back-top
            $(window).scroll(function () {
                if ($(this).scrollTop() > 100) {
                    $('.bottom-right #back-top.bottom-icon').css('visibility','visible');
                } else {
                    $('.bottom-right #back-top.bottom-icon').css('visibility','hidden');
                }
            });

            // scroll body to 0px on click
            $('#back-top').click(function () {
                $('body,html').animate({
                    scrollTop: 0
                }, 400);
                return false;
            });
        });     
        
        /*-----------------------------------------------------------------------------------*/
        /*  Misc.
        /*-----------------------------------------------------------------------------------*/       
         $('.widget_ad .widget-title').fadeIn("100"); 

        /*-----------------------------------------------------------------------------------*/
        /*  Mobile Menu & Search
        /*-----------------------------------------------------------------------------------*/

        /* Mobile Menu */
        $('.slicknav_btn').click(function(){

            $('.header-search').slideUp('fast', function() {});
            $('.search-icon > .fa-search').removeClass('active');
            $('.search-icon > .fa-close').removeClass('active');

        });

        /* Mobile Search */
        $('.search-icon > .fa-search').click(function(){

            $('.header-search').slideDown('fast', function() {});
            $('.search-icon > .fa-search').toggleClass('active');
            $('.search-icon > .fa-close').toggleClass('active');

            $('.slicknav_btn').removeClass('slicknav_open');
            $('.slicknav_nav').addClass('slicknav_hidden');
            $('.slicknav_nav').css('display','none');

        });

        $('.search-icon > .fa-close').click(function(){

            $('.header-search').slideUp('fast', function() {});
            $('.search-icon > .fa-search').toggleClass('active');
            $('.search-icon > .fa-close').toggleClass('active');

            $('.slicknav_btn').removeClass('slicknav_open');
            $('.slicknav_nav').addClass('slicknav_hidden');  
            $('.slicknav_nav').css('display','none');

        });          


    });


})(jQuery);