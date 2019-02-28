var Site = (function ($) {
    var _rootUrl = null;
    var _language = null;

    var _me = {
        initialize: function () {
            _rootUrl = $("#RootUrl").val();
            _language = $("#Language").val();
        },
        resolveRelativeUrl: function (url) {
            return _rootUrl + url;
        },
        getCurrentLanguage: function () {
            return _language;
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
        redirect: function (url) {
            window.location.href = url;
        },
        reload: function () {
            window.location.reload(true);
        },
        displayResult: function (isSuccess, message, selector) {
            selector = selector || "#messageResult";
            var $result = $(selector);
            $result.toggleClass("alert-success", isSuccess);
            $result.toggleClass("alert-danger", !isSuccess);
            $result.html(message);
            $result.show();
        }
    };

    return _me;
})(jQuery);

Site.initialize();