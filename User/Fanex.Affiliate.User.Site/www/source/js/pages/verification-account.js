var VerificationAccount = (function () {
    var _refreshCaptchaTimeoutId = null;
    var _countdownResendCodeTimeoutId = null;
    var INTERVAL_SENDCODE_SECOND = 60;
    var REFRESH_CAPTCHA_INTERVAL = 600000;

    function registerRefreshCaptcha() {
        function refreshCaptcha() {
            _me.generateCaptcha();
            clearTimeout(_refreshCaptchaTimeoutId);
            _refreshCaptchaTimeoutId = setTimeout(refreshCaptcha, REFRESH_CAPTCHA_INTERVAL);
        }

        clearTimeout(_refreshCaptchaTimeoutId);
        _refreshCaptchaTimeoutId = setTimeout(refreshCaptcha, REFRESH_CAPTCHA_INTERVAL);
    }

    function updateAction(action) {
        $('#ActionLink').val(action.Link);
        $('#ActionToken').val(action.TimeToken);
    }

    var _me = {
        initialize: function () {
            _me.initEvent();
            _me.sendCode(true);
            registerRefreshCaptcha();
        },
        initEvent: function () {
            $("#btnResend").on("click", function (e) {
                _me.sendCode(false);
            });
            $("#btnLoginVerification").on("click", function (e) {
                _me.submitForm();
            });
            $("#captchaVerificationAccount").on('click', function (e) {
                e.preventDefault();
                _me.generateCaptcha();
                registerRefreshCaptcha();
            });
            $('#verification-account input').on('keypress', function (e) {
                var code = e.keyCode || e.which;
                if (code === 13) {
                    _me.submitForm();
                }
            });
        },
        generateCaptcha: function () {
            var captchaUrl = Site.resolveRelativeUrl("VerificationAccount/CaptchaImage?_v=" + new Date().getTime());
            $("#captchaVerificationAccount").attr("src", captchaUrl);
        },
        sendCode: function (isReload) {
            var token = $('input[name="__RequestVerificationToken"]', $("#verificationAccountForm")).val();

            $.ajax({
                type: "POST",
                url: Site.resolveRelativeUrl("VerificationAccount/SendCode"),
                data: { __RequestVerificationToken: token },
                dataType: "json",
                cache: false,
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                success: function (response) {
                    $('#btnResend').prop("disabled", true);
                    _me.countDown(INTERVAL_SENDCODE_SECOND);
                    $('#VerificationToken').val(response.VerificationToken);
                    updateAction(response.Action);

                    if (!isReload && !response.IsOk) {
                        alert(response.Message);
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
            clearTimeout(_countdownResendCodeTimeoutId);
            if (second === 0) {
                $('#btnResend').html('RESEND CODE');
                $('#btnResend').prop('disabled', false);
            } else {
                $('#btnResend').html('RESEND CODE(' + second + ')');
                _countdownResendCodeTimeoutId = setTimeout(_me.countDown, 1000, --second);
            }
        },
        submitForm: function () {
            var username = $('#Usename').val();
            var password = $('#Password').val();
            var code = $('#Code').val();
            var captcha = $('#Captcha').val();
            var $form = $("#verificationAccountForm");
            var validator = $form.validate();
            var isValidForm = validator.form();

            if (isValidForm) {
                var csrfToken = $('input[name="__RequestVerificationToken"]', $("#userLogin")).val();
                var publicKey = AuthHelper.base64DecodeUnicode($("#PublicKey").val());
                var timeStamp = AuthHelper.getUtcUnixTimeStamp();

                var queryString = 'u=' + encodeURIComponent(username.trim()) + '&p=' + encodeURIComponent(password) + '&t=' + timeStamp;
                var encryptedQueryString = AuthHelper.encrypt(queryString, publicKey);
                $('#Password').val(encryptedQueryString);

                var verificationToken = $('#VerificationToken').val();
                var authMessage = encryptedQueryString + verificationToken + captcha + code;
                var hashMessage = CryptoJS.HmacSHA256(authMessage, verificationToken).toString();

                var url = Site.resolveRelativeUrl("Execute") + '/' + encodeURIComponent($('#ActionLink').val());
                var param = {
                    __RequestVerificationToken: csrfToken,
                    LoginInfo: encryptedQueryString,
                    Code: code,
                    Captcha: captcha,
                    HashMessage: hashMessage,
                    t: $('#ActionToken').val()
                }
                $.ajax({
                    type: "POST",
                    url: url,
                    data: param,
                    dataType: "json",
                    cache: false,
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    success: function (response) {
                        if (response.IsOk) {
                            Site.redirect(Site.resolveRelativeUrl("UserPanel/Main/#AccountOverview"));
                        }
                        else {
                            var message = response.IsExpired ? resource.verificationFailed : response.Message;
                            alert(message);
                            $('#Password').val("");
                            $('#Captcha').val("");
                            _me.generateCaptcha();
                            registerRefreshCaptcha();
                        }

                        updateAction(response.Action);
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
    };
    return _me;
})();

VerificationAccount.initialize();