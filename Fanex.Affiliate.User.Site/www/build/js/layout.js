(function ($) {
    function initSlider() {
        var $mainBanner = $('#mainBanner');
        $mainBanner.carousel();
    }

    var _me = {
        initialize: function () {
            initSlider();

            _me.initEvent();
        },
        initEvent: function () {
            $("#selectedLanguage").on("click", function () {
                $("#languageList").toggle();
            });
        }
    }

    _me.initialize();
})(jQuery);