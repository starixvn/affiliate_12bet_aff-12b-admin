"use strict";

var DailyEarning = (function ($) {
    var _me = {
        initialize: function initialize() {
            DailyEarning.initEvent();
        },

        initEvent: function initEvent() {
            $("#dailyEarningContent").on("click", ".dateItem", function () {
                var param = {
                    sportType: $("#SportType").val(),
                    monthDate: $(this).data("date")
                };

                DailyEarning.renderDailyEarningDetail(param);
            });

            $("#statementHistoryLink").on("click", function () {
                Site.reload();
            });

            $("#SportType").on("change", function () {
                var params = {
                    monthDate: $("#MonthDate").val(),
                    sportType: $(this).val(),
                    isInitial: false
                };

                _me.renderDailyEarning(params);
            });
        },

        renderDailyEarning: function renderDailyEarning(params) {
            $.ajax({
                type: "GET",
                data: params,
                dataType: "html",
                cache: false,
                url: Site.resolveRelativeUrl("UserPanel/DailyEarning"),
                success: function success(response) {
                    $("#dailyEarningContent").html(response);
                },
                beforeSend: function beforeSend() {
                    Site.showLoader();
                },
                complete: function complete() {
                    Site.hideLoader();
                }
            });
        },

        renderDailyEarningDetail: function renderDailyEarningDetail(param) {
            $.ajax({
                type: "GET",
                dataType: "html",
                data: param,
                cache: false,
                url: Site.resolveRelativeUrl("UserPanel/DailyEarningDetail"),
                success: function success(response) {
                    $("#mainPanel").html(response);
                },
                beforeSend: function beforeSend() {
                    Site.showLoader();
                },
                complete: function complete() {
                    Site.hideLoader();
                }
            });
        }
    };

    return _me;
})(jQuery);

DailyEarning.initialize();

