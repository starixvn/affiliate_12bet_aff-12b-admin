'use strict';

var TrackingStatistics = (function ($) {
    var _me = {
        initialize: function initialize() {
            _me.initEvent();
            document.addEventListener('click', _me.handleDocumentClick);
        },

        initEvent: function initEvent() {
            $('#btnStatisticsSubmit').on('click', function () {
                $("#statisticsDetailTable").html('');

                var params = {
                    trackingCode: $('#TrackingCode').val()
                };
                _me.getMainView(params);
            });

            $('#statisticsTable').on('click', '.statisticsItem', function () {
                var params = {
                    trackingCode: $('#TrackingCode').val(),
                    dateMonth: $(this).data('date')
                };
                _me.getDetailView(params);
            });

            $('#country-multiple-select-label').on('click', function () {
                $('#country-multiple-select-options-container').toggle();
            });

            $('#country-option-all').on('change', function (event) {
                $(".country-option-checkbox").prop("checked", $(this).is(":checked"));
            });

            $('.country-option-checkbox').on('change', function (event) {
                if (!$(this).is(":checked")) {
                    $("#country-option-all").prop("checked", false);
                } else {
                    var isCheckAll = $(".country-option-checkbox:checked").length == $(".country-option-checkbox").length;

                    $("#country-option-all").prop("checked", isCheckAll);
                }
            });

            //$('#country').on('click', function (event) {
            //    console.log(event);

            //    if ($('.multiple-select-options-container').is(":hidden")) {
            //        $('.multiple-select-options-container').show();
            //    } else {
            //        $('.multiple-select-options-container').hide();
            //    }

            //    //_selectedDomain = $(this).data('domain');
            //    //_pageIndex = 0;
            //    //var params = _me.getDetailParam();

            //    //$('#statisticsDetailPager').show();
            //    //_me.getDetailView(params, initPager);
            //});
        },

        handleDocumentClick: function handleDocumentClick(event) {
            console.log(event);
            //if ($(`#country`).) {
            //    $(`#country-multiple-select-options-container`).toggle();
            //}
        },
        getMainView: function getMainView(params) {
            $.ajax({
                type: "POST",
                dataType: "html",
                cache: false,
                data: JSON.stringify(params),
                url: Site.resolveRelativeUrl("UserPanel/TrackingStatistics/Index"),
                contentType: "application/json; charset=UTF-8",
                success: function success(response) {
                    $("#statisticsTable").html(response);
                },
                beforeSend: function beforeSend() {
                    Site.showLoader();
                },
                complete: function complete() {
                    Site.hideLoader();
                }
            });
        },

        getDetailView: function getDetailView(params) {
            $.ajax({
                type: "POST",
                dataType: "html",
                cache: false,
                data: JSON.stringify(params),
                url: Site.resolveRelativeUrl("UserPanel/TrackingStatistics/TrackingStatisticsDetail"),
                contentType: "application/json; charset=UTF-8",

                success: function success(response) {
                    $("#statisticsDetailTable").html(response);
                },
                beforeSend: function beforeSend() {
                    Site.showLoader();
                },
                complete: function complete() {
                    Site.hideLoader();
                }
            });
        }
    };

    return _me;
})(jQuery);

TrackingStatistics.initialize();

