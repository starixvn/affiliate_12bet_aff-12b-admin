var TextBox = (function ($) {
    var _pageSize = 20;
    var _pageIndex = 0;

    function initPager() {
        $("#marketingPager").pager({
            totalItems: parseInt($("#TotalItem").val()),
            pageSize: _pageSize,
            pageIndex: _pageIndex,
            onChange: function (pageIndex, pageSize) {
                _pageIndex = pageIndex;
                _pageSize = pageSize;

                var searchParams = _me.getSearchParam();
                _me.getTextList(searchParams);
            }
        });
    }

    var _me = {
        initialize: function () {
            _me.initEvent();
            initPager();
        },

        initEvent: function () {
            $("#btnMarketingSearch").on("click", function () {
                var searchParams = _me.getSearchParam();
                _me.getTextList(searchParams, _me.updatePager);

                MarketingBase.generateTextLink();
            });

            $("#dataContainerList").on("change", "#DeviceType", function () {
                var params = _me.getSearchParam();
                _me.getTextList(params, _me.updatePager);
            });

            $("#btnGenerateLink").on("click", function () {
                MarketingBase.generateTextLink();
            });
        },

        getSearchParam: function () {
            return {
                category: $("#Category").val(),
                language: $("#Language").val(),
                currency: $("#Currency").val(),
                deviceType: $("#DeviceType").val(),
                selectedDomainId: $("#Domain").val(),
                selectedPage: $("#MemberSitePage").val(),
                pageIndex: _pageIndex + 1,
                pageSize: _pageSize
            }
        },

        getTextList: function (params, onSuccess) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/GetText"),
                success: function (response) {
                    $("#marketingDataList").html(response);
                    onSuccess && onSuccess();
                }
            });
        },

        updatePager: function () {
            $("#marketingPager").pager("update", {
                pageIndex: _pageIndex,
                totalItems: parseInt($("#TotalItem").val())
            });
        }
    };

    return _me;
})(jQuery);

TextBox.initialize();