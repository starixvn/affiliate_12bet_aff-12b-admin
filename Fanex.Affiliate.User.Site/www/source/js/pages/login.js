var Login = (function ($) {
    var _me = {
        initialize: function () {
            _me.initEvent();
        },

        initEvent: function () {
            $("#btnLogin").on("click", function () {
                var username = $("#txtUsername").val();
                var password = $("#txtPassword").val();
                var validation = $("#txtValidation").val();

                if (_me.isValid(username, password, validation)) {
                    var token = $('input[name="__RequestVerificationToken"]', $("#userLogin")).val();

                    var postData = {
                        __RequestVerificationToken: token,
                        UserName: username,
                        EncryptedPass: password,
                        ValidationKey: validation
                    };

                    $.ajax({
                        type: "POST",
                        url: "/Home/Login",
                        data: postData,
                        dataType: "json",
                        cache: false,
                        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                        success: function (response) {
                            if (response.IsOk) {
                                location.href = "/UserPanel/#AccountOverview";
                            }
                        }
                    });
                }
                else {
                    alert("Please enter all fields");
                }
            });
        },

        isValid: function (username, password, validationCode) {
            var empty = "";

            if (username === empty) {
                $("#txtUsername").focus();
                return false;
            }

            if (password === empty) {
                $("#txtPassword").focus();
                return false;
            }

            if (validationCode === empty) {
                $("#txtValidation").focus();
                return false;
            }

            return true;
        }
    };

    return _me;
})(jQuery);

Login.initialize();