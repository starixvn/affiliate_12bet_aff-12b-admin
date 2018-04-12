var Site = (function ($) {
    var _rootUrl = null;

    var _me = {
        initialize: function () {
            _rootUrl = $("#RootUrl").val();
        },
        resolveRelativeUrl: function (url) {
            return _rootUrl + url;
        }
    };

    return _me;
})(jQuery);

Site.initialize();