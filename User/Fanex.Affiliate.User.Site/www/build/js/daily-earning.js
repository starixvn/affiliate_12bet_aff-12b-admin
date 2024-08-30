"use strict";

var DailyEarning = (function ($) {
    var _me = {
        initialize: function initialize() {
            DailyEarning.initEvent();
        },

        initEvent: function initEvent() {
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
                url: Site.resolveRelativeLangUrl("UserPanel/DailyEarning"),
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

