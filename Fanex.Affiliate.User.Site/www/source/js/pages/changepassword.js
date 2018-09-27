var ChangePassword = (function ($) {
    var _me = {
        initialize: function () {
            _me.initEvent();
        },

        initEvent: function () {
            $("#btnCancel").on("click", function () {
                Site.redirect(Site.resolveRelativeUrl("UserPanel/Main/#MyAccount"));
            });

            $("#updatePasswordForm").on("submit", function (e) {
                e.preventDefault();
                _me.submit();
                return false;
            });
        },

        submit: function () {
            var $form = $("#updatePasswordForm");
            var validator = $form.validate();
            var isValidForm = validator.form();

            if (isValidForm) {
                var $newPassword = $("#NewPassword");
                var $oldPassword = $("#OldPassword");
                var $confirmedNewPassword = $("#ConfirmNewPassword");

                var encryptKey = $("#EncriptionKey").val();
                var encryptIV = $("#EncriptionIv").val();

                var encryptedOldPassword = _me.encryptPassword($oldPassword.val(), encryptKey, encryptIV);
                var encryptedNewPassword = _me.encryptPassword($newPassword.val(), encryptKey, encryptIV);
                var confirmedNewPassword = _me.encryptPassword($confirmedNewPassword.val(), encryptKey, encryptIV);

                $oldPassword.val(encryptedOldPassword);
                $newPassword.val(encryptedNewPassword);
                $confirmedNewPassword.val(confirmedNewPassword);

                var postData = $form.serializeObject();

                $.ajax({
                    type: "POST",
                    url: Site.resolveRelativeUrl("Password/Update"),
                    data: postData,
                    dataType: "json",
                    cache: false,
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    success: function (response) {
                        $form[0].reset();
                        validator.resetForm();
                        Site.displayResult(response.IsOk, response.Message);
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
        }
    };

    return _me;
})(jQuery);

ChangePassword.initialize();