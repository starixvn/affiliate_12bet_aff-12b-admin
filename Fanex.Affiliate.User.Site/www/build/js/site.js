"use strict";

var Site = (function ($) {
    var _rootUrl = null;

    var _me = {
        initialize: function initialize() {
            _rootUrl = $("#RootUrl").val();
        },
        resolveRelativeUrl: function resolveRelativeUrl(url) {
            return _rootUrl + url;
        },
        escapeHtml: function escapeHtml(inputString) {
            var div = document.createElement('div');
            div.appendChild(document.createTextNode(inputString));
            return div.innerHTML;
        }
    };

    return _me;
})(jQuery);

Site.initialize();

