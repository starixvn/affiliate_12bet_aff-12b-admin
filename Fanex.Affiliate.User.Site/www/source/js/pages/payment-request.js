var PaymentRequest = (function ($) {
    function isValidKey(charCode) {
        return !(charCode > 31 && (charCode < 48 || charCode > 57)) ||
            charCode === 46;
    }

    function buildErrorMessage(message) {
        return '<p class="alert alert-danger" role="alert">' + message + '</p>';
    }

    var _me = {
        initialize: function () {
            _me.initEvent();
        },

        initEvent: function () {
            $("#Amount").on("keypress", function (event) {
                var charCode = (event.which) ? event.which : event.keyCode;
                return isValidKey(charCode);
            });

            $("#Amount").on("input propertychange paste", function () {
                var reg = /^0+/gi;
                if (this.value.match(reg)) {
                    this.value = this.value.replace(reg, '');
                }
            });

            $("#requestPayoutForm").on("submit", function () {
                Site.showLoader();

                var pattern = /^\d+(\.\d{1,2})?$/;
                var balance = parseFloat($("#BalBroughtForward").val());
                var minAmount = parseFloat($("#MinAmount").val());

                var amountString = $("#Amount").val();
                var isValidNumber = pattern.test(amountString);
                var amount = parseFloat($("#Amount").val());

                var canSubmit = isValidNumber && amount >= minAmount && amount <= balance;

                if (!canSubmit) {
                    if (isNaN(amount) || !isValidNumber) {
                        $("#errorMessage").html(buildErrorMessage(resourceLang.invalidAmount));
                    }
                    else {
                        $("#errorMessage").html(buildErrorMessage(resourceLang.invalidRange));
                    }

                    Site.hideLoader();
                }

                return canSubmit;
            });

            $("#cancelPaymentForm").on("submit", function () {
                Site.showLoader();
            });
        }
    };

    return _me;
})(jQuery)

PaymentRequest.initialize();