var Summary = (function ($) {
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
            var params = {
                monthDate: $("#MonthDate").data("original-date")
            };

            initMonthPicker();
            _me.initEvent();
            _me.renderDashBoardView(params);
            _me.renderDetailView(params, "Overview");
        },

        initEvent: function () {
            $("#statisticsSubmit").on("click", function () {
                var params = {
                    monthDate: $("#MonthDate").data("original-date")
                }

                var actionMenu = $('.details-section-menu-item.active').data('name');

                _me.renderDashBoardView(params);
                _me.renderDetailView(params, actionMenu);
            });

            $('#details-section-menu').on('click', '.details-section-menu-item', function () {
                var name = $(this).data('name');
                _me.renderDetailView({ monthDate: $("#MonthDate").data("original-date") }, name);
            });
        },

        renderDashBoardView: function (params) {
            $.ajax({
                type: "POST",
                dataType: "html",
                cache: false,
                data: JSON.stringify(params),
                url: Site.resolveRelativeUrl("UserPanel/AccountOverView/DashBoard"),
                contentType: "application/json; charset=UTF-8",
                success: function (response) {
                    $("#dashboard-section").html(response);
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        },

        activeMenu: function (actionName) {
            var activeClassName = "active";
            $(".details-section-menu-item").removeClass(activeClassName);
            $("#menu" + actionName).addClass(activeClassName);
        },

        renderDetailView: function (params, actionName) {
            _me.activeMenu(actionName);

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

Summary.initialize();