var ReCheckIn = (function ($) {
    var _handler;
    var _me = {
        initialize: function () {
            _handler = setInterval(_me.reCheckin, 30000);
        },

        reCheckin: function () {
            var url = $("#recheckinUrl").val() + new Date().getTime();
            $.ajax({
                url: url,
                async: true,
                type: "GET",
                dataType: "json",
                cache: false,
                success: function (result) {
                    if (result.IsKickedOut) {
                        _me.kickout(result.Message);
                    }
                }
            });
        },

        kickout: function (message) {
            clearInterval(_handler);
            window.alert(message);
            setTimeout(function () {
                parent.location.reload();
            }, 500);
        }
    };
    return _me;
})(jQuery);
ReCheckIn.initialize();