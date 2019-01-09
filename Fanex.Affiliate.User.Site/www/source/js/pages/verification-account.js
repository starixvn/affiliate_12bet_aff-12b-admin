var VerificationAccount = (function () {
    var _me = {
        initialize: function () {
            _me.initEvent();
            _me.sendCode();
        },
        initEvent: function () {
            $("#verification-account #btn-resend").on("click", function (e) {
                e.preventDefault();
                _me.resendCode();
            });
            $("#verification-account #btn-login").on("click", function (e) {
                e.preventDefault();
                _me.submitForm();
            });
        },
        sendCode: function () {
            $.ajax({
                type: "POST",
                url: Site.resolveRelativeUrl("VerificationAccount/SendCode?Username=" + userName),
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
                $('.btn-resend').removeAttr("disabled");
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
                var param = {
                    Username: username,
                    Password: password,
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
                $("#UsernameInvalid").removeAttr('class');
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