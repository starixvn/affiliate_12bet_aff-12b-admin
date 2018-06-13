'use strict';

var PaymentRequest = (function ($) {
    function isValidKey(charCode) {
        return !(charCode > 31 && (charCode < 48 || charCode > 57)) || charCode === 46;
    }

    function buildErrorMessage(message) {
        return '<p class="alert alert-danger" role="alert">' + message + '</p>';
    }

    var _me = {
        initialize: function initialize() {
            _me.initEvent();
        },

        initEvent: function initEvent() {
            $("#Amount").on("keypress", function (event) {
                var charCode = event.which ? event.which : event.keyCode;
                console.log(charCode);
                return isValidKey(charCode);
            });

            $("#Amount").on("input propertychange paste", function () {
                var reg = /^0+/gi;
                if (this.value.match(reg)) {
                    this.value = this.value.replace(reg, '');
                }
            });

            $("#btnRequestPayout").on("click", function () {
                var balance = parseFloat($("#BalBroughtForward").val());
                var minAmount = parseFloat($("#MinAmount").val());
                var amount = parseFloat($("#Amount").val());

                if (amount >= minAmount && amount <= balance) {
                    $("#requestPayoutForm").submit();
                } else if (isNaN(amount)) {
                    $("#errorMessage").html(buildErrorMessage(resourceLang.invalidAmount));
                } else {
                    $("#errorMessage").html(buildErrorMessage(resourceLang.invalidRange));
                }
            });

            $("#cancelPaymentRequest").on("click", function () {
                $("#cancelPaymentForm").submit();
            });
        }
    };

    return _me;
})(jQuery);

PaymentRequest.initialize();

