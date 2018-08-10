var DailyEarning = (function ($) {
    var SPORTTYPE_SPORTSBOOK = 1;

    var _me = {
        initialize: function () {
            DailyEarning.initEvent();
        },

        initEvent: function () {
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

        renderDailyEarning: function (params) {
            $.ajax({
                type: "POST",
                data: JSON.stringify(params),
                dataType: "html",
                contentType: "application/json; charset=UTF-8",
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