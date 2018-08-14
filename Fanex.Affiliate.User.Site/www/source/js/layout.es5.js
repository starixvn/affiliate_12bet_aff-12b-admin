"use strict";

var Layout = (function ($) {
    var DEFAULT_SPEED = 5;

    function initSlider() {
        var $mainBanner = $('#mainBanner');
        $mainBanner.carousel();
    }

    var _me = {
        initialize: function initialize() {
            initSlider();
            _me.registerAjaxHandlerError();
            _me.initEvent();
            _me.renderHotNews();
        },
        initEvent: function initEvent() {
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
                    success: function success(response) {
                        location.reload(true);
                    }
                });
            });
        },
        registerAjaxHandlerError: function registerAjaxHandlerError() {
            $(document).ajaxError(function (event, jqXHR) {
                var status = jqXHR.status;

                if (status !== 200) {
                    if (status === 401 || status == 403) {
                        window.location.href = Site.resolveRelativeUrl("");
                    } else if (status === 404) {
                        window.location.href = Site.resolveRelativeUrl("Notify/PageNotFound");
                    } else {
                        alert("An error occurred while processing your request");
                    }
                }
            });
        },
        renderHotNews: function renderHotNews() {
            $.ajax({
                type: "GET",
                url: Site.resolveRelativeUrl("News/LatestHotNews"),
                dataType: "html",
                cache: false,
                success: function success(response) {
                    var $marquee = $("#marquee");
                    $marquee.html(response);
                    // Time = Distance/Speed
                    var containerWith = $("#marqueeContainer").width();
                    var timeTaken = containerWith / DEFAULT_SPEED;

                    $marquee.css("animation-duration", timeTaken + "s");
                }
            });
        }
    };

    return _me;
})(jQuery);

Layout.initialize();

