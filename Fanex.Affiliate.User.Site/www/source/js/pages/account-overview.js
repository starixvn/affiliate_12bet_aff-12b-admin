var AccountOverview = (function ($) {
    function initMonthPicker() {
        $("#MonthDate").monthPicker({
            triggerSelector: "#monthPickerTrigger",
            onSelect: function (month, year) {
                console.log(month, year);
            }
        });
    }

    var _me = {
        initialize: function () {
            initMonthPicker();
        },

        initEvent: function () {
        }
    };

    return _me;
})(jQuery);

AccountOverview.initialize();