var DailyEarningDetail = (function ($) {
    function formatDateUnit(value) {
        return value < 10 ? '0' + value : value;
    }

    function initDatePicker() {
        UserPanel.datepicker.push(new Pikaday({
            field: document.getElementById("MonthDate"),
            trigger: document.getElementById("datePicker"),
            yearRange: [2000, new Date().getFullYear()],
            toString: function (date, format) {
                var day = date.getDate();
                var month = date.getMonth() + 1;
                var year = date.getFullYear();
                return formatDateUnit(month) + "/" + formatDateUnit(day) + "/" + year;
            }
        }));
    }

    var _me = {
        initialize: function () {
            initDatePicker();
            _me.initEvent();
        },

        initEvent: function () {
            $("#btnEarningDetailSubmit").on("click", function () {
                var params = {
                    ProductId: $("#ProductId").val(),
                    monthDate: $("#MonthDate").val(),
                    IsOnlyContent: true
                }

                _me.renderDailyEarningDetail(params);
            });

            $("#dailyEarningDetailContent").on("click", ".earningDetailItem", function () {
                var params = $(this).data("param");

                if (params) {
                    Site.showLoader();
                    Site.redirect(Site.resolveRelativeUrl("UserPanel/BetList/Index?" + $.param(params)));
                }
            });

            $("#dailyEarningDetailContent").on("click", ".transactionItem", function () {
                var params = {
                    username: $(this).data("username"),
                    date: $(this).data("date"),
                    isdeposit: $(this).data("isdeposit")
                }

                if (params.username) {
                    var url = Site.resolveRelativeUrl("UserPanel/MemberTransaction/Index");
                    var title = (params.isdeposit ? dialogTitle.titleDeposit : dialogTitle.titleWithdrawal) + " " + params.date;
                    _me.showTransactionInfo(url, params, title);
                }
            });
        },

        renderDailyEarningDetail: function (params) {
            $.ajax({
                type: "POST",
                dataType: "html",
                data: JSON.stringify(params),
                cache: false,
                contentType: "application/json; charset=UTF-8",

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
        },

        showTransactionInfo: function (url, params, title) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                url: url,
                data: params,
                success: function (response) {
                    $("#transactionDisalogTitle").html(title || "");
                    $("#transactionDialogContent").html(response);
                    $("#transactionDialog").modal("show");
                },
                beforeSend: function () {
                    Site.showLoader();
                    $("#transactionDialogContent").html("");
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