"use strict";

var ExistingPromotion = (function ($) {
    var _me = {
        initialize: function initialize() {
            _me.initEvent();
        },
        initEvent: function initEvent() {
            $(".promotion-login-link").on("click", function () {
                event.preventDefault();
                $("#loginDialog").modal({ backdrop: "static" });
            });
        }
    };

    return _me;
})(jQuery);

ExistingPromotion.initialize();

