'use strict';

var Faq = (function ($) {
    var _me = {
        initialize: function initialize() {
            _me.initEvent();
        },
        initEvent: function initEvent() {
            $('.block-content').addClass("active");
            $('.block-content .button-toggle').addClass("open");
            $('.button-toggle').click(function () {
                $(this).toggleClass('open').parent().next().slideToggle(300);
                $(this).parents('.block-content').toggleClass("active");

                //$('.block-content').removeClass("active");
                //if ($(this).hasClass('open')) {
                //    $(this).parents('.block-content').addClass("active");
                //}
            });
        }
    };

    return _me;
})(jQuery);

Faq.initialize();

