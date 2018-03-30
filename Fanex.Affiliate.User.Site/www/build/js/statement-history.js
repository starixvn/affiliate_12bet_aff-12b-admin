"use strict";

var StatementHistory = (function ($) {
    return {
        initialize: function initialize() {
            StatementHistory.initEvent();
        },

        initEvent: function initEvent() {
            $("#statementContent").on("click", ".amountItem", function () {
                StatementHistory.renderDailyEarning();
            });
        },

        renderDailyEarning: function renderDailyEarning() {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                url: "/UserPanel/DailyEarning",
                success: function success(response) {
                    $("#mainPanel").html(response);
                }
            });
        }
    };
})(jQuery);

StatementHistory.initialize();

