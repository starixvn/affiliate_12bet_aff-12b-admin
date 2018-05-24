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
        }

    };

    return _me;
})(jQuery);

Site.initialize();