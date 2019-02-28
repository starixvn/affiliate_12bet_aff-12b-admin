"use strict";

var Site = (function ($) {
    var _rootUrl = null;
    var _language = null;

    var _me = {
        initialize: function initialize() {
            _rootUrl = $("#RootUrl").val();
            _language = $("#Language").val();
        },
        resolveRelativeUrl: function resolveRelativeUrl(url) {
            return _rootUrl + url;
        },
        getCurrentLanguage: function getCurrentLanguage() {
            return _language;
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
            window.location.reload(true);
        },
        displayResult: function displayResult(isSuccess, message, selector) {
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

