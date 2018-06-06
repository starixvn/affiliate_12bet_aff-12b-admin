var StatementHistory = (function ($) {
    var _pageSize = 20;
    var _pageIndex = 0;

    function initPager() {
        $("#statementPager").pager({
            totalItems: parseInt($("#TotalItem").val()),
            pageSize: _pageSize,
            pageIndex: _pageIndex,
            onChange: function (pageIndex, pageSize) {
                _pageIndex = pageIndex;
                _pageSize = pageSize;

                var params = _me.getStatementHistoryParam();
                _me.renderStatementHistory(params);
            }
        });
    }

    var _me = {
        initialize: function () {
            console.log("statement history");
            initPager();
            StatementHistory.initEvent();
        },

        initEvent: function () {
            $("#statementDataList").on("click", ".amountItem", function () {
                var params = {
                    monthDate: $(this).data("date")
                };

                Site.redirect(Site.resolveRelativeUrl("UserPanel/#DailyEarning?" + $.param(params)));
            });
        },

        getStatementHistoryParam: function () {
            return {
                pageSize: _pageSize,
                pageIndex: _pageIndex + 1
            };
        },

        renderStatementHistory: function (params) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/GetStatementHistoryData"),
                success: function (response) {
                    $("#statementDataList").html(response);
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

StatementHistory.initialize();