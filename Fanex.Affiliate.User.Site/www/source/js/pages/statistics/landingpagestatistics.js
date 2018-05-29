var LandingPageStatistics = (function ($) {
    var _pageSize = 20;
    var _pageIndex = 0;
    var _selectedPage;
    var _selectedLanguage;

    function initPager() {
        $("#statisticsDetailPager").pager({
            totalItems: parseInt($("#TotalItem").val()),
            pageSize: _pageSize,
            pageIndex: _pageIndex,
            onChange: function (pageIndex, pageSize) {
                _pageIndex = pageIndex;
                _pageSize = pageSize;

                var params = _me.getDetailParam();
                _me.getDetailView(params);
            }
        });
    }

    var _me = {
        initialize: function () {
            _me.initEvent();
        },

        initEvent: function () {
            $('#btnStatisticsSubmit').on('click', function () {
                $('#statisticsDetailTable').html('');
                $('#statisticsDetailPager').html('').hide();
                console.log('click');

                var params = {
                    fromDate: $('#FromDate').val(),
                    toDate: $('#ToDate').val(),
                    deviceType: $('#DeviceType').val(),
                    sportCode: $('#SportCode').val(),
                    languageCode: $('#Language').val()
                };
                _me.getLandingPageView(params);
            });

            $('#statisticsTable').on('click', '.statisticsItem', function () {
                _selectedPage = $(this).data('page');
                _selectedLanguage = $(this).data('language');
                _pageIndex = 0;
                var params = _me.getDetailParam();

                $('#statisticsDetailPager').show();
                _me.getDetailView(params, initPager);
            });
        },

        getLandingPageView: function (params) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/GetLandingPageStatistics"),
                success: function (response) {
                    $("#statisticsTable").html(response);
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        },

        getDetailView: function (params, onSuccess) {
            if (!params.sportCode || !params.languageCode) {
                return;
            }

            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/GetStatisticsDetailBySite"),
                success: function (response) {
                    $("#statisticsDetailTable").html(response);
                    onSuccess && onSuccess();
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        },

        getDetailParam: function () {
            return {
                sportCode: _selectedPage,
                languageCode: _selectedLanguage,
                fromDate: $('#FromDate').val(),
                toDate: $('#ToDate').val(),
                deviceType: $('#DeviceType').val(),
                pageSize: _pageSize,
                pageIndex: _pageIndex + 1
            };
        }
    };

    return _me;
})(jQuery)

LandingPageStatistics.initialize();