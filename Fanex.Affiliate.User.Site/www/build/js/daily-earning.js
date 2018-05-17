"use strict";

var DailyEarning = (function ($) {
    return {
        initialize: function initialize() {
            DailyEarning.initEvent();
        },

        initEvent: function initEvent() {
            $("#dailyEarningContent").on("click", ".dateItem", function () {
                DailyEarning.renderDailyEarning();
            });
        },

        renderDailyEarning: function renderDailyEarning() {
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
})(jQuery);

DailyEarning.initialize();

