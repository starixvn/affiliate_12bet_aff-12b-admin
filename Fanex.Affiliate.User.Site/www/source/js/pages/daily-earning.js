var DailyEarning = (function ($) {
    var _me = {
        initialize: function () {
            DailyEarning.initEvent();
        },

        initEvent: function () {
            $("#dailyEarningContent").on("click", ".dateItem", function () {
                var param = {
                    sportType: $("#SportType").val(),
                    monthDate: $(this).data("date")
                }

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
                }

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
        },

        renderDailyEarningDetail: function (param) {
            $.ajax({
                type: "GET",
                dataType: "html",
                data: param,
                cache: false,
                url: Site.resolveRelativeUrl("UserPanel/DailyEarningDetail"),
                success: function (response) {
                    $("#mainPanel").html(response);
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