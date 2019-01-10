var VerificationAccount = (function () {
    function encrypt(plainText, key) {
        var encrypt = new JSEncrypt();
        encrypt.setPublicKey(key);

        return encrypt.encrypt(plainText);
    }

    function base64DecodeUnicode(str) {
        return decodeURIComponent(Array.prototype.map.call(atob(str), function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
        }).join(''))
    }

    function getUtcUnixTimeStamp() {
        var now = new Date();
        return Date.UTC(now.getUTCFullYear(), now.getUTCMonth(), now.getUTCDate(),
              now.getUTCHours(), now.getUTCMinutes(), now.getUTCSeconds(), now.getUTCMilliseconds());
    }
    var _me = {
        initialize: function () {
            _me.initEvent();
            _me.sendCode();
        },
        initEvent: function () {
            $("#btn-resend").on("click", function (e) {
                e.preventDefault();
                _me.resendCode();
            });
            $("#btn-login").on("click", function (e) {
                e.preventDefault();
                _me.submitForm();
            });
            $("#captchaVerificationAccount").on('click', function (e) {
                e.preventDefault();
                _me.getCaptcha();
            })
        },
        getCaptcha: function () {
            var captchaUrl = Site.resolveRelativeUrl("VerificationAccount/CaptchaImage?_v=" + new Date().getTime());
            $("#captchaVerificationAccount").attr("src", captchaUrl);
        },
        sendCode: function () {
            $.ajax({
                type: "POST",
                url: Site.resolveRelativeUrl("VerificationAccount/SendCode"),
                dataType: "json",
                cache: false,
                contentType: "application/json; charset=UTF-8",
                success: function (response) {
                    if(response.IsOk)
                    {
                        $('.btn-resend').prop("disabled", true);
                        _me.countDown(60);
                    }
                    else
                    {
                        alert($("<div/>").html(response.Message).text());
                    }
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        },
        countDown: function (second) {
            if (second == 0) {
                clearTimeout(_me.countDown);
                $('.btn-resend').html('RESEND CODE');
                $('.btn-resend').prop("disabled", false);
            } else {
                $('.btn-resend').html('RESEND CODE(' + second + ')');
                setTimeout(_me.countDown, 1000, --second);
            }
        },
        submitForm: function () {
            var username = $('#Usename').val();
            var password = $('#Password').val();
            var code = $('#Code').val();
            var captcha = $('#Captcha').val();
            var validationResult = _me.validate(username, password, code, captcha);

            if (validationResult) {
                var token = $('input[name="__RequestVerificationToken"]', $("#userLogin")).val();
                var publicKey = base64DecodeUnicode($("#PublicKey").val());

                var queryString = 'u=' + encodeURIComponent(username.trim()) + '&p=' + encodeURIComponent(password) + '&t=' + getUtcUnixTimeStamp();
                var encryptedQueryString = encrypt(queryString, publicKey);
                $('#Password').val(encryptedQueryString);

                var param = {
                    __RequestVerificationToken: token,
                    LoginInfo: encryptedQueryString,
                    Code: code,
                    Captcha: captcha,
                }
                $.ajax({
                    type: "POST",
                    url: Site.resolveRelativeUrl("VerificationAccount/VerifyAccount"),
                    data: param,
                    dataType: "json",
                    cache: false,
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    success: function (response) {
                        if (response.IsOk) {
                            Site.redirect(Site.resolveRelativeUrl("UserPanel/Main/#AccountOverview"));
                        }
                        else {
                            alert($("<div/>").html(response.Message).text());
                            $('#Password').val("");
                            $('#Captcha').val("abc");
                            _me.getCaptcha();
                        }
                    },
                    beforeSend: function () {
                        Site.showLoader();
                    },
                    complete: function () {
                        Site.hideLoader();
                    }
                });
            }
        },
        validate: function (username, password, code, captcha) {
            var empty = "";
            var result = true;
            
            if (username === empty) {
                $("#Username").focus();
                $("#UsernameInvalid").removeClass('class');
                result = false;
            }
            else {
                $("#UsernameInvalid").addClass('help-block hidden');
            }

            if (password === empty) {
                $("#Password").focus();
                $("#PasswordInvalid").removeClass("hidden");
                result = false;
            }
            else {
                $("#PasswordInvalid").addClass('help-block hidden');
            }

            if (code === empty) {
                $("#Code").focus();
                $("#CodeInvalid").removeClass("hidden");
                result = false;
            }
            else {
                $("#CodeInvalid").addClass('help-block hidden');
            }

            if (captcha === empty) {
                $("#Captcha").focus();
                $("#CaptchaInvalid").removeClass("hidden");
                result = false;
            }
            else {
                $("#CaptchaInvalid").addClass('help-block hidden');
            }

            return result;
        },
    };
    return _me;
})();

VerificationAccount.initialize();