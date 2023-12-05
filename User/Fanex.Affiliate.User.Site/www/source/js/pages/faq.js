var Faq = (function ($) {
    var _me = {
        initialize: function () {
            _me.initEvent();
        },
        initEvent: function () {
            $('.block-content').addClass("active");
            $('.block-content .button-toggle').addClass("open");
            $('.button-toggle').click(function () {
                $(this).toggleClass('open').parent().next().slideToggle(300);
                $(this).parents('.block-content').toggleClass("active");
            });
        }
    };

    return _me;
})(jQuery);

Faq.initialize();