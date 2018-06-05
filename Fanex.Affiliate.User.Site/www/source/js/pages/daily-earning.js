var DailyEarning = (function ($) {
    var _me = {
        initialize: function () {
            DailyEarning.initEvent();
        },

        initEvent: function () {
            $("#dailyEarningContent").on("click", ".dateItem", function () {
                DailyEarning.renderDailyEarningDetail();
            });

            $("#statementHistoryLink").on("click", function () {
                Site.reload();
            });

            $("#SportType").on("change", function () {
                var monthDate = $("#MonthDate").val();
                var selectedSportType = $(this).val();

                _me.renderDailyEarning(monthDate, selectedSportType);
            });
        },

        renderDailyEarning: function (date, sportType) {
            $.ajax({
                type: "GET",
                data: { monthDate: date, sportType: sportType },
                dataType: "html",
                cache: false,
                url: Site.resolveRelativeUrl("UserPanel/GetDailyEarning"),
                success: function (response) {
                    $("#dailyEarningContent").html(response);
                }
            });
        },

        renderDailyEarningDetail: function () {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                url: Site.resolveRelativeUrl("UserPanel/DailyEarningDetail"),
                success: function (response) {
                    $("#mainPanel").html(response);
                }
            });
        }
    };

    return _me;
})(jQuery);

DailyEarning.initialize();