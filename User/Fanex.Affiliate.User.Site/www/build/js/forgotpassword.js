"use strict";

var ForgotPassword = (function ($) {
    var _me = {
        initialize: function initialize() {
            _me.initEvent();
        },

        initEvent: function initEvent() {
            $("#forgotPasswordForm").on("submit", function (e) {
                _me.submit($(this));
                return false;
            });

            $("#btnReset").on("click", function () {
                $("#Email").val("");
                $("#forgotPasswordForm").validate().resetForm();
            });
        },

        submit: function submit($form) {
            var validator = $form.validate();
            var isValidForm = validator.form();
            if (isValidForm) {
                var postData = $form.serializeObject();
                $.ajax({
                    type: "POST",
                    url: Site.resolveRelativeLangUrl("Password/Forgot"),
                    data: postData,
                    dataType: "json",
                    cache: false,
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    success: function success(response) {
                        _me.handleResponseResult(response);
                    },
                    beforeSend: function beforeSend() {
                        Site.showLoader();
                    },
                    complete: function complete() {
                        Site.hideLoader();
                    }
                });
            }
        },

        handleResponseResult: function handleResponseResult(response) {
            $("#messageSuccess").toggle(response.IsOk);
            $("#messageError").toggle(!response.IsOk).html(response.Message || resourceLanguage.generalError);

            if (response.IsOk) {
                $("#mainContent").remove();
            }
        }
    };

    return _me;
})(jQuery);

ForgotPassword.initialize();

