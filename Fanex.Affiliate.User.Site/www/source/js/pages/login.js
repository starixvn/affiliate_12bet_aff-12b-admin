var Login = (function ($) {
    var _me = {
        initialize: function () {
            _me.registerLoginDialog();
            _me.initEvent();
            _me.initLoginDialogEvent();
        },
        registerLoginDialog: function () {
            $("#loginDialog").on("show.bs.modal", function (e) {
                _me.generateDialogCaptcha();
            })

            $("#loginDialog").on("hide.bs.modal", function (e) {
                $("#loginDialogcaptchaImg").attr("src", "");
                _me.generateCaptcha();
            })
        },
        initLoginDialogEvent: function () {
            $('#mobileLogin').on('click', function () {
                $("#loginDialog").modal({ backdrop: "static" });
            });

            $(".mediaLoginLink").on("click", function () {
                $('#isCLickedBannerLink').val("1");
                $("#loginDialog").modal({ backdrop: "static" });
            });

            $("#btnDialogLogin").on("click", function () {
                _me.requestSignin(true);
            });

            $("#dialogUserInput input").on("keypress", function (e) {
                if (e.keyCode === 13) {
                    _me.requestSignin(true);
                }
            });

            $("#loginDialogcaptchaImg").on("click", function () {
                _me.generateDialogCaptcha();
            });
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

            $("#captchaImg").on("click", function () {
                _me.generateCaptcha();
            });
        },

        requestSignin: function (onDialog) {
            var prefix = onDialog ? "dialog" : "";

            var $username = $("#" + prefix + "txtUsername");
            var $password = $("#" + prefix + "txtPassword");
            var $validation = $("#" + prefix + "txtValidation");

            var username = $username.val();
            var password = $password.val();
            var validation = $validation.val();

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
                    ValidationKey: validation,
                    EncriptionKey: encryptKey,
                    EncriptionIv: encryptIV
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
                            var isRedirectToMediaPage = $('#isCLickedBannerLink').val() === "1";
                            if (onDialog && isRedirectToMediaPage) {
                                PageTracking.addTrackingForMediaPage();

                                setTimeout(function () {
                                    Site.redirect(Site.resolveRelativeUrl("UserPanel/Main/#MediaGallery"));
                                }, 10);
                            }
                            else {
                                Site.redirect(Site.resolveRelativeUrl("UserPanel/Main/#AccountOverview"));
                            }
                        }
                        else {
                            alert(response.Message);
                            if (onDialog) {
                                $username.val("");
                                $password.val("");
                                $validation.val("");
                            }
                            else {
                                Site.reload();
                            }
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
        generateDialogCaptcha: function () {
            var captchaUrl = Site.resolveRelativeUrl("Auth/CaptchaImage?_v=" + new Date().getTime());
            $("#loginDialogcaptchaImg").attr("src", captchaUrl);
        },
        generateCaptcha: function () {
            var captchaUrl = Site.resolveRelativeUrl("Auth/CaptchaImage?_v=" + new Date().getTime());
            $("#captchaImg").attr("src", captchaUrl);
        }
    };

    return _me;
})(jQuery);

Login.initialize();