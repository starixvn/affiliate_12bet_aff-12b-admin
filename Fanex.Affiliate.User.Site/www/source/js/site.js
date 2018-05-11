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
        }
    };

    return _me;
})(jQuery);

Site.initialize();