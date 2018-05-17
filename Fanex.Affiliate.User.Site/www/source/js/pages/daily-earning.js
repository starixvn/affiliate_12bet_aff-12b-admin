var DailyEarning = (function ($) {
    return {
        initialize: function () {
            DailyEarning.initEvent();
        },

        initEvent: function () {
            $("#dailyEarningContent").on("click", ".dateItem", function () {
                DailyEarning.renderDailyEarning();
            });
        },

        renderDailyEarning: function () {
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
})(jQuery);

DailyEarning.initialize();