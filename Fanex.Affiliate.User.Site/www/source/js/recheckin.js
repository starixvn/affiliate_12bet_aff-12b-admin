var ReCheckIn = (function ($) {
    var _handler;
    var _url = Site.resolveRelativeUrl("Auth/ReCheckin");

    var _me = {
        initialize: function () {
            _handler = setInterval(_me.reCheckin, 30000);
        },

        reCheckin: function () {
            $.ajax({
                url: _url,
                async: true,
                type: "GET",
                dataType: "json",
                cache: false,
                success: function (result) {
                    if (result.IsKickOut) {
                        _me.kickout(result.Message);
                    }
                }
            });
        },

        kickout: function (message) {
            clearInterval(_handler);
            window.alert(message);

            setTimeout(function () {
                window.location.href = Site.resolveRelativeUrl("");
            }, 500);
        }
    }

    return _me;
})(jQuery);

ReCheckIn.initialize();