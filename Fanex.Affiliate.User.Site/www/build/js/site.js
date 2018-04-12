"use strict";

var Site = (function ($) {
    var _rootUrl = null;

    var _me = {
        initialize: function initialize() {
            _rootUrl = $("#RootUrl").val();
        },
        resolveRelativeUrl: function resolveRelativeUrl(url) {
            return _rootUrl + url;
        }
    };

    return _me;
})(jQuery);

Site.initialize();

