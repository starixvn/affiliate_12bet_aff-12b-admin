var ExistingPromotion = (function ($) {
    var _me = {
        initialize: function () {
            _me.initEvent();
        },
        initEvent: function () {
            $(".promotion-login-link").on("click", function (event) {
                event.preventDefault();
                $("#signUpToAffiliateProgram").html(promotionResourceLanguage.signUpToAffiliateProgram);

                $("#loginDialog").modal({ backdrop: "static" });
            })
        }
    };

    return _me;
})(jQuery);

ExistingPromotion.initialize();