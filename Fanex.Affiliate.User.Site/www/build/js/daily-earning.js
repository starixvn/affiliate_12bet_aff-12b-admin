"use strict";

var DailyEarning = (function ($) {
    var _me = {
        initialize: function initialize() {
            DailyEarning.initEvent();
        },

        initEvent: function initEvent() {
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

        renderDailyEarning: function renderDailyEarning(date, sportType) {
            $.ajax({
                type: "GET",
                data: { monthDate: date, sportType: sportType },
                dataType: "html",
                cache: false,
                url: Site.resolveRelativeUrl("UserPanel/GetDailyEarning"),
                success: function success(response) {
                    $("#dailyEarningContent").html(response);
                }
            });
        },

        renderDailyEarningDetail: function renderDailyEarningDetail() {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                url: Site.resolveRelativeUrl("UserPanel/DailyEarningDetail"),
                success: function success(response) {
                    $("#mainPanel").html(response);
                }
            });
        }
    };

    return _me;
})(jQuery);

DailyEarning.initialize();

