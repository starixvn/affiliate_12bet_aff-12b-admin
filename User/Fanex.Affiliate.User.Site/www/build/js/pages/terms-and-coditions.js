"use strict";

var TermAndCondition = (function ($) {
    var _me = {
        initialize: function initialize() {
            _me.initEvent();
        },
        initEvent: function initEvent() {
            $('.block-content:not(:first-of-type) .anwser').css("display", "none");
            $('.block-content:first-of-type .button-toggle').addClass("open");
            $('.block-content:first-of-type').addClass("active");

            $('.button-toggle').click(function () {
                $('.open').not(this).removeClass('open').parent().next().slideUp(300);
                $(this).toggleClass('open').parent().next().slideToggle(300);

                $('.block-content').removeClass("active");
                if ($(this).hasClass('open')) {
                    $(this).parents('.block-content').addClass("active");
                    $(this).parents('.block-content').addClass("active").next().addClass('spacing');
                }
            });
        }
    };

    return _me;
})(jQuery);

TermAndCondition.initialize();

