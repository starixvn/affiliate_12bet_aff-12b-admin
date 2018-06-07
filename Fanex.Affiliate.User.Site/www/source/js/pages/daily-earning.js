var DailyEarning = (function ($) {
    var SPORTTYPE_SPORTSBOOK = 1;

    var _me = {
        initialize: function () {
            DailyEarning.initEvent();
        },

        initEvent: function () {
            $("#dailyEarningContent").on("click", ".dateItem", function () {
                var params = {
                    sportType: $("#SportType").length ? $("#SportType").val() : SPORTTYPE_SPORTSBOOK,
                    monthDate: $(this).data("date")
                };

                Site.redirect(Site.resolveRelativeUrl("UserPanel/#DailyEarningDetail?" + $.param(params)));
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

        renderDailyEarning: function (params) {
            $.ajax({
                type: "GET",
                data: params,
                dataType: "html",
                cache: false,
                url: Site.resolveRelativeUrl("UserPanel/DailyEarning"),
                success: function (response) {
                    $("#dailyEarningContent").html(response);
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        }
    };

    return _me;
})(jQuery);

DailyEarning.initialize();