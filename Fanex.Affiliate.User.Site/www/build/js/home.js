"use strict";

var Home = (function ($) {
    var _me = {
        initialize: function initialize() {
            _me.initEvent();
            _me.registerLoginDialog();
        },

        initEvent: function initEvent() {
            $("#loginLink").on("click", function () {
                $("#loginDialog").modal({ backdrop: "static" });
            });

            $("#btnDialogLogin").on("click", function () {
                Login.requestSignin(true);
            });

            $("#dialogUserInput input").on("keypress", function (e) {
                if (e.keyCode === 13) {
                    Login.requestSignin(true);
                }
            });
        },

        registerLoginDialog: function registerLoginDialog() {
            $("#loginDialog").on("show.bs.modal", function (e) {
                _me.generateCaptcha();
            });

            $("#loginDialog").on("hide.bs.modal", function (e) {
                Login.generateCaptcha();
            });
        },

        generateCaptcha: function generateCaptcha() {
            var captchaUrl = Site.resolveRelativeUrl("Home/CaptchaImage?_v=" + new Date().getTime());
            $("#loginDialogcaptchaImg").attr("src", captchaUrl);
        }
    };

    return _me;
})(jQuery);

Home.initialize();

