var AccountOverview = (function ($) {
    function formatDateUnit(value) {
        return value < 10 ? '0' + value : value;
    }

    function initMonthPicker() {
        $("#MonthDate").monthPicker({
            triggerSelector: "#monthPickerTrigger",
            onSelect: function (month, year) {
                var date = formatDateUnit(month) + "/01/" + year;
                $("#MonthDate").data("original-date", date);
            }
        });
    }

    var _me = {
        initialize: function () {
            initMonthPicker();
            _me.initEvent();
        },

        initEvent: function () {
            $("#statisticsSubmit").on("click", function () {
                var params = {
                    monthDate: $("#MonthDate").data("original-date")
                }

                Site.redirect(Site.resolveRelativeUrl("UserPanel/#AccountOverView?") + $.param(params));
            });
        }
    };

    return _me;
})(jQuery);

AccountOverview.initialize();