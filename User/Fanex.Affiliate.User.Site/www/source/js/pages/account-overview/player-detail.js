var PlayersDetails = (function ($) {
    var _pageSize = 20;
    var _pageIndex = 0;
    var _sortingColumn = 'UserName';
    var _isSortingAsc = false;
    const SortByIdList = {
        "Username": 1,
        "RegisteredDate": 2,
        "Deposit": 3,
        "Withdrawal": 4,
        "Turnover": 5,
        "Winloss": 6,
        "Bonus": 7,
        "BankingFee": 8
    };

    function initPager() {
        $("#player-detail-pager").pager({
            totalItems: parseInt($("#TotalItem").val()),
            pageSize: _pageSize,
            pageIndex: _pageIndex,
            languageCode: Site.getCurrentLanguage(),
            onChange: function (pageIndex, pageSize) {
                _pageIndex = pageIndex;
                _pageSize = pageSize;

                var params = _me.getPlayersDetailsParam();
                _me.renderPlayersDetails(params);
            }
        });
    }

    var _me = {
        initialize: function () {
            initPager();
            _me.initEvent();
            _me.activeSortIcon($('#defaultSorting'));
        },

        initEvent: function () {
            $('#player-detail-table').on('click', '.th', function () {
                var $this = $(this);
                _me.processSorting($this);
            });
        },

        getPlayersDetailsParam: function (sortById, sortTypeId) {
            return {
                MonthDate: $("#MonthDate").data("original-date"),
                PageSize: _pageSize,
                PageIndex: _pageIndex + 1,
                SortById: sortById || 1,
                SortTypeId: sortTypeId || 1
            };
        },

        renderPlayersDetails: function (params) {
            $.ajax({
                type: "POST",
                dataType: "html",
                cache: false,
                data: JSON.stringify(params),
                contentType: "application/json; charset=UTF-8",
                url: Site.resolveRelativeUrl("UserPanel/Summary/PlayersDetailsContent"),
                success: function (response) {
                    $("#player-detail-content").html(response);
                    $("#player-detail-content").addClass($("#HasScroll").val())
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        },

        processSorting: function ($element) {
            var columnName = $element.closest('.th').data('name');

            var params = _me.getPlayersDetailsParam(SortByIdList[columnName], _isSortingAsc ? 1 : 2);
            _me.renderPlayersDetails(params);

            if (columnName === _sortingColumn) {
                _isSortingAsc = !_isSortingAsc;
            } else {
                _isSortingAsc = true;
                _sortingColumn = columnName;
            }

            var sortingSelector = _isSortingAsc ? '.asc' : '.desc';
            var $sortIcon = $element.find(sortingSelector);
            _me.activeSortIcon($sortIcon);
        },
        activeSortIcon: function ($element) {
            $('.th').find('.caret.current').removeClass('current');
            $element.addClass('current');
        }
    };

    return _me;
})(jQuery);

PlayersDetails.initialize();