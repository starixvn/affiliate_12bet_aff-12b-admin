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
            $("#statementContent").on("click", ".amountItem", function () {
                StatementHistory.renderDailyEarning();
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
                }
            });
        },

        renderDailyEarning: function () {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                url: Site.resolveRelativeUrl("UserPanel/DailyEarning"),
                success: function (response) {
                    $("#mainPanel").html(response);
                }
            });
        }
    };

    return _me;
})(jQuery);

StatementHistory.initialize();