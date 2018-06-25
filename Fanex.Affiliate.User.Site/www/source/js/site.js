var Site = (function ($) {
    var _rootUrl = null;

    var _me = {
        initialize: function () {
            _rootUrl = $("#RootUrl").val();
        },
        resolveRelativeUrl: function (url) {
            return _rootUrl + url;
        },
        escapeHtml: function (inputString) {
            var div = document.createElement('div');
            div.appendChild(document.createTextNode(inputString));
            return div.innerHTML;
        },
        showLoader: function () {
            $("#blocker").show();
        },
        hideLoader: function () {
            $("#blocker").hide();
        },
        redirect: function(url) {
            window.location.href = url;
        },
        reload: function () {
            window.location.reload();
        },
        refreshCaptcha: function () {
            var captchaUrl = Site.resolveRelativeUrl("Home/CaptchaImage?_v=" + new Date().getTime());
            $(".validation-value img").attr("src", captchaUrl);
        }

    };

    return _me;
})(jQuery);

Site.initialize();