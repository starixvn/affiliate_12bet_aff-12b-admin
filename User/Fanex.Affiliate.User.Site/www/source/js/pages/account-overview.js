var AccountOverview = (function ($) {
    function formatDateUnit(value) {
        return value < 10 ? '0' + value : value;
    }

    function initMonthPicker() {
        $("#MonthDate").monthPicker({
            range: [new Date(2009, 11, 1), new Date($("#ToDay").val())],
            triggerSelector: "#monthPickerTrigger",
            onSelect: function (month, year) {
                var date = formatDateUnit(month) + "/01/" + year;
                $("#MonthDate").data("original-date", date);
            }
        });
    }

    var _me = {
        initialize: function () {
            initMonthPicker();
            _me.initEvent();
            _me.renderView(null, "Summary");
        },

        initEvent: function () {
            $("#statisticsSubmit").on("click", function () {
                //var params = {
                //    monthDate: $("#MonthDate").data("original-date")
                //}

                var actionMenu = $('.account-overview-menu.active').data('name');

                _me.renderView(null, actionMenu);

                //Site.redirect(Site.resolveRelativeUrl("UserPanel/Main/#AccountOverView?") + $.param(params));
            });

            $('#account-overview-menu').on('click', '.account-overview-menu', function () {
                var name = $(this).data('name');
                _me.renderView(null, name);
            });

            $('#details-section').on('click', '.details-section-menu', function () {
                var name = $(this).data('name');
                _me.renderSummaryView(null, name);
            });
        },

        activeMenu: function (actionName) {
            var activeClassName = "active";
            $(".account-overview-menu").removeClass(activeClassName);
            $("#menu" + actionName).addClass(activeClassName);
        },

        renderView: function (params, actionName) {
            _me.activeMenu(actionName);

            $.ajax({
                type: "POST",
                dataType: "html",
                cache: false,
                data: JSON.stringify(params),
                url: Site.resolveRelativeUrl("UserPanel/AccountOverView/" + actionName),
                contentType: "application/json; charset=UTF-8",
                success: function (response) {
                    $("#account-overview-content").html(response);
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    if (actionName === "Summary") {
                        _me.renderSummaryView(null, "Overview");
                    } else {
                        Site.hideLoader();
                    }
                }
            });
        },

        activeSummaryMenu: function (actionName) {
            var activeClassName = "active";
            $(".details-section-menu").removeClass(activeClassName);
            $("#menu" + actionName).addClass(activeClassName);
        },

        renderSummaryView: function (params, actionName) {
            _me.activeSummaryMenu(actionName);

            $.ajax({
                type: "POST",
                dataType: "html",
                cache: false,
                data: JSON.stringify(params),
                url: Site.resolveRelativeUrl("UserPanel/AccountOverView/" + actionName),
                contentType: "application/json; charset=UTF-8",
                success: function (response) {
                    $("#details-section-content").html(response);
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        },
    };

    return _me;
})(jQuery);

AccountOverview.initialize();