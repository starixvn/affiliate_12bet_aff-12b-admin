"use strict";

var Home = (function ($) {
    var _me = {
        initialize: function initialize() {
            _me.initEvent();
            _me.registerLoginDialog();
            _me.renderHotNews();
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
                $("#loginDialogcaptchaImg").attr("src", "");
                Login.generateCaptcha();
            });
        },

        generateCaptcha: function generateCaptcha() {
            var captchaUrl = Site.resolveRelativeUrl("Home/CaptchaImage?_v=" + new Date().getTime());
            $("#loginDialogcaptchaImg").attr("src", captchaUrl);
        },

        renderHotNews: function renderHotNews() {
            $.ajax({
                type: "GET",
                url: Site.resolveRelativeUrl("Home/LatestHotNews"),
                dataType: "html",
                cache: false,
                success: function success(response) {
                    $("#marquee").html(response);
                }
            });
        }
    };

    return _me;
})(jQuery);

Home.initialize();

