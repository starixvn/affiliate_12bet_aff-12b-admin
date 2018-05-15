"use strict";

var ReCheckIn = (function ($) {
    var _handler;
    var _url = Site.resolveRelativeUrl("Auth/ReCheckin");

    var _me = {
        initialize: function initialize() {
            _handler = setInterval(_me.reCheckin, 30000);
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
                        clearInterval(_handler);
                        window.alert(result.Message);

                        setTimeout(function () {
                            window.location.href = Site.resolveRelativeUrl("");
                        }, 500);
                    }
                }
            });
        }
    };

    return _me;
})(jQuery);

ReCheckIn.initialize();

