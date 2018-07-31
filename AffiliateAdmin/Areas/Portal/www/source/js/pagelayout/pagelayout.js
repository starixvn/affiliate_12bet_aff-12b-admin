var PageLayout = (function ($) {
    var _me = {
        initialize: function () {
            _me.initEvent();
        },

        initEvent: function () {
            $('.nav-link').on('click', function () {
                var $this = $(this);
                var $container = $this.parent().parent();
                $container.find('.nav-link').removeClass('active');
                $this.addClass('active');
            });
        }
    };

    return _me;
})(jQuery)

PageLayout.initialize();