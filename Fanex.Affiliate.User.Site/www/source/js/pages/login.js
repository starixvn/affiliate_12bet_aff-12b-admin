var Login = (function ($) {
    var _me = {
        initialize: function () {
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

        requestSignin: function(){
            var username = $("#txtUsername").val();
            var password = $("#txtPassword").val();
            var validation = $("#txtValidation").val();

            var validationResult = _me.validate(username, password, validation);
            if (validationResult.isValid) {
                var token = $('input[name="__RequestVerificationToken"]', $("#userLogin")).val();

                var postData = {
                    __RequestVerificationToken: token,
                    UserName: username,
                    Password: password,
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
                            location.href = "/UserPanel/#AccountOverview";
                        }
                        else {
                            alert(response.Message);
                        }
                    }
                });
            }
            else {
                alert(validationResult.message);
            }
        },

        validate: function (username, password, validationCode) {
            var empty = "";
            var result = {
                isValid: false,
                message: empty
            }

            if (username === empty) {
                $("#txtUsername").focus();
                result.message = validationMessage.enterUserName;
                return false;
            }

            if (password === empty) {
                $("#txtPassword").focus();
                result.message = validationMessage.enterPassword;
                return false;
            }

            if (validationCode === empty) {
                $("#txtValidation").focus();
                result.message = validationMessage.enterValidationCode;
                return false;
            }

            result.isValid = true;
            return result;
        }
    };

    return _me;
})(jQuery);

Login.initialize();