"use strict";

var DailyEarning = (function ($) {
    var SPORTTYPE_SPORTSBOOK = 1;

    var _me = {
        initialize: function initialize() {
            DailyEarning.initEvent();
        },

        initEvent: function initEvent() {
            $("#dailyEarningContent").on("click", ".dateItem", function () {
                var params = {
                    productId: $("#ProductId").length ? $("#ProductId").val() : SPORTTYPE_SPORTSBOOK,
                    monthDate: $(this).data("date")
                };

                Site.redirect(Site.resolveRelativeUrl("UserPanel/Main/#DailyEarningDetail?" + $.param(params)));
            });

            $("#ProductId").on("change", function () {
                var params = {
                    monthDate: $("#MonthDate").val(),
                    productId: $(this).val(),
                    IsOnlyContent: true
                };

                _me.renderDailyEarning(params);
            });
        },

        renderDailyEarning: function renderDailyEarning(params) {
            $.ajax({
                type: "POST",
                data: JSON.stringify(params),
                dataType: "html",
                contentType: "application/json; charset=UTF-8",
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
        }
    };

    return _me;
})(jQuery);

DailyEarning.initialize();

