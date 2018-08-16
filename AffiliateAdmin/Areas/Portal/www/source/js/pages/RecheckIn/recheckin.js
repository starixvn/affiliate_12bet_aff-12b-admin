var ReCheckIn = (function ($) {
    var _handler;
    var _me = {
        initialize: function () {
            _handler = setTimeout(_me.reCheckin, 3000);
        },

        reCheckin: function () {
            var url = $("#recheckinUrl").val() + "?time=" + new Date().getTime();
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
                    _handler = setTimeout(_me.reCheckin, 8000);
                },
                statusCode: {
                    500: function (xhr) {
                        if (window.console) console.log(xhr.responseText);
                    },
                    403: function (xhr) {
                        alert("Your session was expired!")
                        parent.location.reload();
                    },
                    401: function (xhr) {
                        alert("Your session was expired!")
                        parent.location.reload();
                    }


                }

            });
        },

        kickout: function (message) {
            clearTimeout(_handler);
            window.alert(message);
            setTimeout(function () {
                parent.location.reload();
            }, 500);
        }
    };
    return _me;
})(jQuery);
ReCheckIn.initialize();