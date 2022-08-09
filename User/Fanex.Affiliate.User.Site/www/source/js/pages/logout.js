var Logout = (function ($) {
    var _me = {
        initialize: function () {
            _me.initEvent();
        },
        initEvent: function () {
            $('#btnLogout').on('click', function (event) {
                _me.handleShowFeedback();
            })
        },

        handleShowFeedback: function () {
            window.location.href = Site.resolveRelativeUrl("Auth/SignOut");

            //$.ajax({
            //    url: Site.resolveRelativeUrl("UserPanel/Feedback/Index?isClickLogout=true"),
            //    type: "GET",
            //    dataType: "html",
            //    cache: false,
            //    success: function (result) {
            //        $("#loginAfter").append(`<div id="affFeedback">${result}</div>`);
            //    },
            //    error: function (jqXHR, textStatus, errorThrown) {
            //        window.location.href = Site.resolveRelativeUrl("Auth/SignOut");
            //    }
            //});
        },
    };

    return _me;
})(jQuery);

Logout.initialize();