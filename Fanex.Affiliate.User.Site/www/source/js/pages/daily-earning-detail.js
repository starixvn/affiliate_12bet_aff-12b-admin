var DailyEarningDetail = (function ($) {
    function formatDateUnit(value) {
        return value < 10 ? '0' + value : value;
    }

    function initDatePicker() {
        new Pikaday({
            field: document.getElementById("MonthDate"),
            trigger: document.getElementById("datePicker"),
            yearRange: [2000, new Date().getFullYear()],
            toString: function (date, format) {
                var day = date.getDate();
                var month = date.getMonth() + 1;
                var year = date.getFullYear();
                return formatDateUnit(month) + "/" + formatDateUnit(day) + "/" + year;
            }
        });
    }

    var _me = {
        initialize: function () {
            initDatePicker();
            _me.initEvent();
        },

        initEvent: function () {
            $("#btnEarningDetailSubmit").on("click", function () {
                var params = {
                    sportType: $("#SportType").val(),
                    monthDate: $("#MonthDate").val(),
                    isInitial: false
                }

                _me.renderDailyEarningDetail(params);
            });

            $("#dailyEarningDetailContent").on("click", ".earningDetailItem", function () {
                var params = $(this).data("param");

                if (params) {
                    Site.showLoader();
                    Site.redirect(Site.resolveRelativeUrl("BetList/Index?" + $.param(params)));
                }
            });
        },

        renderDailyEarningDetail: function (params) {
            $.ajax({
                type: "GET",
                dataType: "html",
                data: params,
                cache: false,
                url: Site.resolveRelativeUrl("UserPanel/DailyEarningDetail"),
                success: function (response) {
                    $("#dailyEarningDetailContent").html(response);
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
})(jQuery)

DailyEarningDetail.initialize();