var Login = (function ($) {
    var _me = {
        initialize: function () {
            _me.generateCaptcha();
            _me.initEvent();
        },

        initEvent: function () {
            $("#btnLogin").on("click", function () {
                _me.requestSignin();
            });

            $("#userLogin input").on("keypress", function (e) {
                if (e.keyCode === 13) {
                    _me.requestSignin();
                }
            });
        },

        requestSignin: function (onDialog) {
            var prefix = onDialog ? "dialog" : "";

            var $password = $("#" + prefix + "txtPassword");
            var username = $("#" + prefix + "txtUsername").val();
            var password = $password.val();
            var validation = $("#" + prefix + "txtValidation").val();

            var validationResult = _me.validate(username, password, validation, prefix);
            if (validationResult.isValid) {
                var token = $('input[name="__RequestVerificationToken"]', $("#userLogin")).val();
                var encryptKey = $("#KeyString").val();
                var encryptIV = $("#IVString").val();

                var encryptedPassword = _me.encryptPassword(password, encryptKey, encryptIV);

                $password.val(encryptedPassword);

                var postData = {
                    __RequestVerificationToken: token,
                    UserName: username,
                    Password: encryptedPassword,
                    ValidationKey: validation
                };

                $.ajax({
                    type: "POST",
                    url: Site.resolveRelativeUrl("Auth/SignIn"),
                    data: postData,
                    dataType: "json",
                    cache: false,
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    success: function (response) {
                        if (response.IsOk) {
                            location.href = Site.resolveRelativeUrl("UserPanel/#AccountOverview");
                        }
                        else {
                            alert(response.Message);
                            location.reload();
                        }
                    }
                });
            }
            else {
                alert(validationResult.message);
            }
        },

        validate: function (username, password, validationCode, prefix) {
            var empty = "";
            var result = {
                isValid: false,
                message: empty
            };

            if (username === empty) {
                $("#" + prefix + "txtUsername").focus();
                result.message = validationMessage.enterUserName;
                return result;
            }

            if (password === empty) {
                $("#" + prefix + "txtPassword").focus();
                result.message = validationMessage.enterPassword;
                return result;
            }

            if (validationCode === empty) {
                $("#" + prefix + "txtValidation").focus();
                result.message = validationMessage.enterValidationCode;
                return result;
            }

            result.isValid = true;
            return result;
        },

        encryptPassword: function (password, encryptKey, encryptIV) {
            var key = CryptoJS.enc.Utf8.parse(encryptKey);
            var iv = CryptoJS.enc.Utf8.parse(encryptIV);

            return CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(password), key, {
                iv: iv,
                mode: CryptoJS.mode.CBC,
                padding: CryptoJS.pad.Pkcs7
            }).toString();
        },

        generateCaptcha: function () {
            var captchaUrl = Site.resolveRelativeUrl("Home/CaptchaImage?_v=" + new Date().getTime());
            $("#captchaImg").attr("src", captchaUrl);
        }
    };

    return _me;
})(jQuery);

Login.initialize();