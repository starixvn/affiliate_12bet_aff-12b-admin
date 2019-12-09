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
            _me.renderSummaryView(null, "Summary");
            _me.renderOverviewView(null, "Overview");
        },

        initEvent: function () {
            $("#statisticsSubmit").on("click", function () {
                //var params = {
                //    monthDate: $("#MonthDate").data("original-date")
                //}

                var actionNameSummary = $('.menu-summary.active').data('name');
                var actionNameOverview = $('.menu-overview.active').data('name');

                _me.renderSummaryView(null, actionNameSummary);
                _me.renderOverviewView(null, actionNameOverview);

                //Site.redirect(Site.resolveRelativeUrl("UserPanel/Main/#AccountOverView?") + $.param(params));
            });

            $('#summary-section').on('click', '.menu-summary', function () {
                var name = $(this).data('name');
                _me.renderSummaryView(null, name);
            });

            $('#overview-section').on('click', '.menu-overview', function () {
                var name = $(this).data('name');
                _me.renderOverviewView(null, name);
            });
        },

        activeMenu: function (sectionName, actionName) {
            var activeClassName = "active";
            $(".menu-" + sectionName).removeClass(activeClassName);
            $("#menu" + actionName).addClass(activeClassName);
        },

        renderSummaryView: function (params, actionName) {
            _me.activeMenu("summary", actionName);

            $.ajax({
                type: "POST",
                dataType: "html",
                cache: false,
                data: JSON.stringify(params),
                url: Site.resolveRelativeUrl("UserPanel/AccountOverView/" + actionName),
                contentType: "application/json; charset=UTF-8",
                success: function (response) {
                    $("#summary-section-content").html(response);
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        },

        renderOverviewView: function (params, actionName) {
            _me.activeMenu("overview", actionName);

            $.ajax({
                type: "POST",
                dataType: "html",
                cache: false,
                data: JSON.stringify(params),
                url: Site.resolveRelativeUrl("UserPanel/AccountOverView/" + actionName),
                contentType: "application/json; charset=UTF-8",
                success: function (response) {
                    $("#overview-section-content").html(response);
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