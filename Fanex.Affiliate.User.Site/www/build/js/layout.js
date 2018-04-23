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

            $("#languageList").on("click", ".language-item", function () {
                var $selectedItem = $(this);
                var selectedValue = $selectedItem.data("option");

                $.ajax({
                    type: "POST",
                    url: Site.resolveRelativeUrl("Home/ChangeLanguage"),
                    data: { languageCode: selectedValue },
                    dataType: "json",
                    cache: false,
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    success: function (response) {
                        location.reload(true);
                    }
                });
            });

            $("#moveToTop").on("click", function () {
                $(window).scrollTop(0);
            });
        }
    }

    _me.initialize();
})(jQuery);