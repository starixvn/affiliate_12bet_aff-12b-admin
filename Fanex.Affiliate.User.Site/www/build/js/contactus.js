"use strict";

var ContactUs = (function ($) {
    var _me = {
        initialize: function initialize() {
            console.log("COntactUs");
            _me.initEvent();
        },

        initEvent: function initEvent() {
            $("#contactForm").on("submit", function () {
                return _me.submit();
            });

            $("#btnReset").on("click", function () {
                _me.reset();
            });
        },

        reset: function reset() {
            var requiredInputList = $("input,textarea", $("#contactForm"));

            for (var i = 0; i < requiredInputList.length; i++) {
                var element = requiredInputList[i];
                $(element).val("");
            }
        },

        submit: function submit() {
            return _me.checkRequiredInput() && _me.checkValidEmail();
        },

        checkRequiredInput: function checkRequiredInput() {
            var requiredInputList = $("input[type=text],textarea", $("#contactForm"));
            var isValid = true;

            for (var i = 0; i < requiredInputList.length; i++) {
                var element = requiredInputList[i];
                if (element.name !== "Affiliate") {

                    var $element = $(element);
                    var hasValue = !!$element.val();

                    var $container = $element.closest(".group");
                    _me.toggleErrorMessage($container, hasValue);

                    if (isValid && !hasValue) {
                        isValid = false;
                    }
                }
            }

            return isValid;
        },

        checkValidEmail: function checkValidEmail() {
            var pattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

            var $email = $("#Email");
            var isValid = pattern.test($email.val());

            var $container = $email.closest(".group");
            _me.toggleErrorMessage($container, isValid);

            return isValid;
        },

        toggleErrorMessage: function toggleErrorMessage($container, isValid) {
            $container.toggleClass("has-error", !isValid);
            $container.find(".help-block").toggleClass("hide", isValid);
        }
    };

    return _me;
})(jQuery);

ContactUs.initialize();

