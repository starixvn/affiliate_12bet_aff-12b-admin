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
        },
        showLoader: function showLoader() {
            $("#blocker").show();
        },
        hideLoader: function hideLoader() {
            $("#blocker").hide();
        },
        redirect: function redirect(url) {
            window.location.href = url;
        },
        reload: function reload() {
            window.location.reload();
        }

    };

    return _me;
})(jQuery);

Site.initialize();

