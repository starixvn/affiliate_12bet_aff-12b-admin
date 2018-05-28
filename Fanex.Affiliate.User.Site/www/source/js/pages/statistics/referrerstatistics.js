var ReferrerStatistics = (function ($) {
    var _pageSize = 20;
    var _pageIndex = 0;
    var _selectedDomain;

    function initPager() {
        $("#statisticsDetailPager").pager({
            totalItems: parseInt($("#TotalItem").val()),
            pageSize: _pageSize,
            pageIndex: _pageIndex,
            onChange: function (pageIndex, pageSize) {
                _pageIndex = pageIndex;
                _pageSize = pageSize;

                var params = _me.getDetailParam();
                _me.getTrackingDetailView(params);
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
                    deviceType: $('#DeviceType').val()
                };
                _me.getReferrerView(params);
            });

            $('#statisticsTable').on('click', '.statisticsItem', function () {
                _selectedDomain = $(this).data('domain');
                _pageIndex = 0;
                var params = _me.getDetailParam();

                $('#statisticsDetailPager').show();
                _me.getTrackingDetailView(params, initPager);
            });
        },

        getReferrerView: function (params) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/GetReferrerStatistics"),
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

        getTrackingDetailView: function (params, onSuccess) {
            if (!params.domain) {
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
                domain: _selectedDomain,
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

ReferrerStatistics.initialize();