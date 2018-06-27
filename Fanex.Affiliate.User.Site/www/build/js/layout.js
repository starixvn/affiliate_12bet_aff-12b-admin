var Layout = (function ($) {
    var DEFAULT_SPEED = 20;

    function initSlider() {
        var $mainBanner = $('#mainBanner');
        $mainBanner.carousel();
    }

    var _me = {
        initialize: function () {
            initSlider();
            _me.registerAjaxHandlerError();
            _me.initEvent();
            _me.renderHotNews();
        },
        initEvent: function () {
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
        },
        registerAjaxHandlerError: function () {
            $(document).ajaxError(function (event, jqXHR) {
                var status = jqXHR.status;

                if (status !== 200) {
                    if (status === 401) {
                        window.location.href = Site.resolveRelativeUrl("");
                    }
                    else if (status === 404) {
                        window.location.href = Site.resolveRelativeUrl("Notify/PageNotFound");
                    }
                    else {
                        alert("An error occurred while processing your request");
                    }
                }
            });
        },
        renderHotNews: function () {
            $.ajax({
                type: "GET",
                url: Site.resolveRelativeUrl("News/LatestHotNews"),
                dataType: "html",
                cache: false,
                success: function (response) {
                    var $marquee = $("#marquee");
                    $marquee.html(response);
                    // Time = Distance/Speed
                    var containerWith = $("#marqueeContainer").width();
                    var timeTaken = containerWith / DEFAULT_SPEED;

                    $marquee.css("animation-duration", timeTaken + "s");
                }
            });
        }
    }

    return _me;
})(jQuery);

Layout.initialize();