'use strict';

var VerificationAccount = (function () {
    var INTERVAL_SENDCODE_SECOND = 900;

    function encrypt(plainText, key) {
        var encrypt = new JSEncrypt();
        encrypt.setPublicKey(key);

        return encrypt.encrypt(plainText);
    }

    function base64DecodeUnicode(str) {
        return decodeURIComponent(Array.prototype.map.call(atob(str), function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));
    }

    function getUtcUnixTimeStamp() {
        var now = new Date();
        return Date.UTC(now.getUTCFullYear(), now.getUTCMonth(), now.getUTCDate(), now.getUTCHours(), now.getUTCMinutes(), now.getUTCSeconds(), now.getUTCMilliseconds());
    }
    var _me = {
        initialize: function initialize() {
            _me.initEvent();
            _me.sendCode(true);
        },
        initEvent: function initEvent() {
            $("#btnResend").on("click", function (e) {
                _me.sendCode(false);
            });
            $("#btn-login").on("click", function (e) {
                _me.submitForm();
            });
            $("#captchaVerificationAccount").on('click', function (e) {
                e.preventDefault();
                _me.getCaptcha();
            });
            $('#verification-account input').on('keypress', function (e) {
                var code = e.keyCode || e.which;
                if (code === 13) {
                    _me.submitForm();
                }
            });
        },
        getCaptcha: function getCaptcha() {
            var captchaUrl = Site.resolveRelativeUrl("VerificationAccount/CaptchaImage?_v=" + new Date().getTime());
            $("#captchaVerificationAccount").attr("src", captchaUrl);
        },
        sendCode: function sendCode(isReload) {
            var token = $('input[name="__RequestVerificationToken"]', $("#verificationAccountForm")).val();

            $.ajax({
                type: "POST",
                url: Site.resolveRelativeUrl("VerificationAccount/SendCode"),
                data: { __RequestVerificationToken: token },
                dataType: "json",
                cache: false,
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                success: function success(response) {
                    $('#btnResend').prop("disabled", true);
                    _me.countDown(INTERVAL_SENDCODE_SECOND);

                    if (!isReload && !response.IsOk) {
                        alert($("<div/>").html(response.Message).text());
                    }
                },
                beforeSend: function beforeSend() {
                    Site.showLoader();
                },
                complete: function complete() {
                    Site.hideLoader();
                }
            });
        },
        countDown: function countDown(second) {
            if (second === 0) {
                clearTimeout(_me.countDown);
                $('#btnResend').html('RESEND CODE');
                $('#btnResend').prop("disabled", false);
            } else {
                $('#btnResend').html('RESEND CODE(' + second + ')');
                setTimeout(_me.countDown, 1000, --second);
            }
        },
        submitForm: function submitForm() {
            var username = $('#Usename').val();
            var password = $('#Password').val();
            var code = $('#Code').val();
            var captcha = $('#Captcha').val();
            var $form = $("#verificationAccountForm");
            var validator = $form.validate();
            var isValidForm = validator.form();

            if (isValidForm) {
                var token = $('input[name="__RequestVerificationToken"]', $("#userLogin")).val();
                var publicKey = base64DecodeUnicode($("#PublicKey").val());

                var queryString = 'u=' + encodeURIComponent(username.trim()) + '&p=' + encodeURIComponent(password) + '&t=' + getUtcUnixTimeStamp();
                var encryptedQueryString = encrypt(queryString, publicKey);
                $('#Password').val(encryptedQueryString);

                var param = {
                    __RequestVerificationToken: token,
                    LoginInfo: encryptedQueryString,
                    Code: code,
                    Captcha: captcha
                };
                $.ajax({
                    type: "POST",
                    url: Site.resolveRelativeUrl("VerificationAccount/VerifyAccount"),
                    data: param,
                    dataType: "json",
                    cache: false,
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    success: function success(response) {
                        if (response.IsOk) {
                            Site.redirect(Site.resolveRelativeUrl("UserPanel/Main/#AccountOverview"));
                        } else {
                            alert($("<div/>").html(response.Message).text());
                            $('#Password').val("");
                            $('#Captcha').val("");
                            _me.getCaptcha();
                        }
                    },
                    beforeSend: function beforeSend() {
                        Site.showLoader();
                    },
                    complete: function complete() {
                        Site.hideLoader();
                    }
                });
            }
        }
    };
    return _me;
})();

VerificationAccount.initialize();

