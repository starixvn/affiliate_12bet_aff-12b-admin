'use strict';

var Layout = (function ($) {
    var DEFAULT_SPEED = 5;

    function createNewUrlByLanguage(url, language) {
        var hasQueryString = url.indexOf('?') !== -1;
        var updatedQueryParam = 'lang=' + language;
        var key = 'lang';

        if (hasQueryString) {
            var regex = new RegExp('([?&])' + key + '=.*?(&|$)', 'i');
            return regex.test(url) ? url.replace(regex, '$1' + key + '=' + language + '$2') : url + '&' + updatedQueryParam;
        } else {
            return url + '?' + updatedQueryParam;
        }
    }

    var _me = {
        initialize: function initialize() {
            _me.registerAjaxHandlerError();
            _me.initEvent();
            _me.renderHotNews();
        },
        initEvent: function initEvent() {
            $("#selectedLanguage").on("click", function (e) {
                if ($(this).hasClass('active')) {
                    $(this).blur();
                } else {
                    $(this).focus();
                    $(this).addClass("active");
                }
            });

            $("#selectedLanguage").on("blur", function (e) {
                $(this).removeClass("active");
            });

            $("#languageList").on("click", ".language-item", function () {
                var $selectedItem = $(this);
                _me.changeLanguage($selectedItem.data('option'));
            });

            $('li.parent').on('mouseover', function () {
                var $menuItem = $(this),
                    $submenuWrapper = $('> .submenu-container', $menuItem);

                // grab the menu item's position relative to its positioned parent
                var menuItemPos = $menuItem.position();
                var withMenu = $menuItem.outerWidth();
                var heightMenu = $menuItem.outerHeight();

                // place the submenu in the correct position relevant to the menu item
                $submenuWrapper.css({
                    top: menuItemPos.top + heightMenu,
                    left: menuItemPos.left + withMenu - 200
                });
            });

            $(window).scroll(function () {
                if ($(this).scrollTop() > 200) {
                    $('#moveToTop').fadeIn(200);
                } else {
                    $('#moveToTop').fadeOut(200);
                }
            });
        },
        registerAjaxHandlerError: function registerAjaxHandlerError() {
            $(document).ajaxError(function (event, jqXHR) {
                var status = jqXHR.status;

                switch (status) {
                    case 401:
                    case 403:
                        window.location.href = Site.resolveRelativeUrl("");
                        break;
                    case 404:
                        window.location.href = Site.resolveRelativeUrl("Notify/PageNotFound");
                        break;
                    case 500:
                        alert($("<div/>").html(window.Lang.ServerError).text());
                        break;
                    default:
                        break;
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
        },

        changeLanguage: function changeLanguage(selectedLanguage) {
            var hash = window.location.hash;
            var href = window.location.href;
            var actualUrl = !!hash ? href.substring(0, href.indexOf(hash)) : href;
            var newActualUrl = createNewUrlByLanguage(actualUrl, selectedLanguage);

            var url = newActualUrl + hash;

            Site.redirect(url);
        }
    };

    return _me;
})(jQuery);

Layout.initialize();

