var ChangePassword = (function ($) {
    var _me = {
        initialize: function () {
            _me.initEvent();
        },

        initEvent: function () {
            $("#btnCancel").on("click", function () {
                Site.redirect(Site.resolveRelativeUrl("UserPanel/#MyAccount"));
            });

            $("#btnSubmit").on("click", function () {
                _me.submit();
            });

            $("#updatePasswordForm input[type='password']").on("keypress", function (e) {
                if (e.keyCode === 13) {
                    _me.submit();
                }
            });
        },

        submit: function () {
            var $newPassword = $("#NewPassword");
            if (!_me.isValidPasswordRule($newPassword.val())) {
                _me.showErrorMessageForRule();
                return false;
            }

            var $oldPassword = $("#OldPassword");
            var $confirmedNewPassword = $("#ConfirmNewPassword");

            var token = $('input[name="__RequestVerificationToken"]', $("#updatePasswordForm")).val();
            var encryptKey = $("#EncriptionKey").val();
            var encryptIV = $("#EncriptionIv").val();

            var encryptedOldPassword = _me.encryptPassword($oldPassword.val(), encryptKey, encryptIV);
            var encryptedNewPassword = _me.encryptPassword($newPassword.val(), encryptKey, encryptIV);
            var confirmedNewPassword = _me.encryptPassword($confirmedNewPassword.val(), encryptKey, encryptIV);

            $oldPassword.val(encryptedOldPassword);
            $newPassword.val(encryptedNewPassword);
            $confirmedNewPassword.val(confirmedNewPassword);

            $("#updatePasswordForm").submit();
        },

        isValidPasswordRule: function (password) {
            var pattern = /^(?=.*\d+.*\d+)(?=.*[a-zA-Z].*[a-zA-Z]).{6,}$/;

            return pattern.test(password);
        },

        showErrorMessageForRule: function () {
            var $container = $("#NewPassword").closest(".form-group");
            _me.toggleErrorMessage($container, false);
        },

        toggleErrorMessage: function ($container, isValid) {
            $container.toggleClass("has-error", !isValid);
            $container.find(".help-block").toggleClass("hide", isValid);
        },

        encryptPassword: function (password, encryptKey, encryptIV) {
            if (!password) {
                return "";
            }

            var key = CryptoJS.enc.Utf8.parse(encryptKey);
            var iv = CryptoJS.enc.Utf8.parse(encryptIV);

            return CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(password), key, {
                iv: iv,
                mode: CryptoJS.mode.CBC,
                padding: CryptoJS.pad.Pkcs7
            }).toString();
        },
    };

    return _me;
})(jQuery);

ChangePassword.initialize();