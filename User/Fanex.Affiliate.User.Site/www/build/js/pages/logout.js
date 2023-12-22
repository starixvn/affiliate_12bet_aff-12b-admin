'use strict';

var Logout = (function ($) {
    var _me = {
        initialize: function initialize() {
            _me.initEvent();
        },
        initEvent: function initEvent() {
            $('#btnLogout').on('click', function (event) {
                _me.handleShowFeedback();
            });

            $('#btnLogoutMobile').on('click', function (event) {
                _me.handleShowFeedback();
            });
        },

        handleShowFeedback: function handleShowFeedback() {
            $.ajax({
                url: Site.resolveRelativeUrl("UserPanel/Feedback/Index?isClickLogout=true"),
                type: "GET",
                dataType: "html",
                cache: false,
                success: function success(result) {
                    console.log(result);
                    $("#groupAuth").append('<div id="affFeedback">' + result + '</div>');
                },
                complete: function complete(jqXHR, textStatus) {
                    if (!$("#affFeedbackContainer").length) {
                        $("#affFeedback").remove();
                        window.location.href = Site.resolveRelativeUrl("Auth/SignOut");
                    }
                }
            });
        }
    };

    return _me;
})(jQuery);

Logout.initialize();

