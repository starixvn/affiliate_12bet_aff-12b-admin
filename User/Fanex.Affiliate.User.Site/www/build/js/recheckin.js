"use strict";

var ReCheckIn = (function ($) {
    var _timeInterval = 30000;
    var _handler;
    var _url = Site.resolveRelativeLangUrl("Auth/ReCheckin");

    var _me = {
        initialize: function initialize() {
            _handler = setInterval(_me.reCheckin, _timeInterval);
        },

        reCheckin: function reCheckin() {
            $.ajax({
                url: _url,
                async: true,
                type: "GET",
                dataType: "json",
                cache: false,
                success: function success(result) {
                    if (result.IsKickOut) {
                        _me.kickout(result.Message);
                    }
                    if (result.IsShowFeedback) {
                        _me.handleShowFeedback();
                    }
                }
            });
        },

        kickout: function kickout(message) {
            clearTimeout(_handler);
            alert($("<div/>").html(message).text());

            setTimeout(function () {
                window.location.href = Site.resolveRelativeLangUrl("");
            }, 500);
        },

        handleShowFeedback: function handleShowFeedback() {
            $.ajax({
                url: Site.resolveRelativeLangUrl("UserPanel/Feedback/Index"),
                type: "GET",
                dataType: "html",
                cache: false,
                success: function success(result) {
                    $("#loginAfter").append("<div id=\"affFeedback\">" + result + "</div>");
                },
                complete: function complete(jqXHR, textStatus) {
                    if (!$("#affFeedbackContainer").length) {
                        $("#affFeedback").remove();
                    }
                }
            });
        }
    };

    return _me;
})(jQuery);

ReCheckIn.initialize();

