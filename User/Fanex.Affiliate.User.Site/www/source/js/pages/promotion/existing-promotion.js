var ExistingPromotion = (function ($) {
    var _me = {
        initialize: function () {
            _me.initEvent();
        },
        initEvent: function () {
            $(".promotion-login-link").on("click", function (event) {
                event.preventDefault();
                $("#loginDialog").modal({ backdrop: "static" });
            })
        }
    };

    return _me;
})(jQuery);

ExistingPromotion.initialize();