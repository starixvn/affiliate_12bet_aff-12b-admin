var StatementHistory = (function ($) {
    return {
        initialize: function () {
            StatementHistory.initEvent();
        },

        initEvent: function () {
            $("#statementContent").on("click", ".amountItem", function () {
                StatementHistory.renderDailyEarning();
            });
        },

        renderDailyEarning: function () {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                url: Site.resolveRelativeUrl("UserPanel/DailyEarning"),
                success: function (response) {
                    $("#mainPanel").html(response);
                }
            });
        }
    };
})(jQuery);

StatementHistory.initialize();