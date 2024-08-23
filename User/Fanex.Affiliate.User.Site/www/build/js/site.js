"use strict";

var Site = (function ($) {
    var _rootUrl = null;
    var _rootLangUrl = null;
    var _language = null;
    var optionAll = "<option value=''>" + window.Lang.All + "</option>";

    var _me = {
        initialize: function initialize() {
            _rootUrl = $("#RootUrl").val();
            _rootLangUrl = $("#RootLangUrl").val();
            _language = $("#Language").val();
        },
        resolveRelativeUrl: function resolveRelativeUrl(url) {
            return _rootUrl + url;
        },
        resolveRelativeLangUrl: function resolveRelativeLangUrl(url) {
            return _rootLangUrl + url;
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

            isSuccess && window.setTimeout(function () {
                return $result.hide();
            }, 3000);
        },
        buildOptionList: function buildOptionList(dataList, value, text, hasAllOption) {
            var optionList = dataList.map(function (item) {
                return "<option value='" + item[value] + "'>" + item[text] + "</option>";
            });

            if (hasAllOption) {
                optionList.unshift(optionAll);
            }

            return optionList.join("");
        }
    };

    return _me;
})(jQuery);

Site.initialize();

