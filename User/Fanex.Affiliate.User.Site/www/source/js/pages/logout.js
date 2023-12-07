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
            $.ajax({
                url: Site.resolveRelativeUrl("UserPanel/Feedback/Index?isClickLogout=true"),
                type: "GET",
                dataType: "html",
                cache: false,
                success: function (result) {
                    $("#btnLogout").append(`<div id="affFeedback">${result}</div>`);
                },
                complete: function (jqXHR, textStatus) {
                    if (!($("#affFeedbackContainer").length)) {
                        $("#affFeedback").remove();
                        window.location.href = Site.resolveRelativeUrl("Auth/SignOut");
                    }
                }
            });
        },
    };

    return _me;
})(jQuery);

Logout.initialize();