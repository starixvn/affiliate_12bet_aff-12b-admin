var TextBox = (function ($) {
    var _pageSize = 20;
    var _pageIndex = 0;

    function initPager() {
        $("#marketingPager").pager({
            totalItems: parseInt($("#TotalItem").val()),
            pageSize: _pageSize,
            pageIndex: _pageIndex,
            languageCode: Site.getCurrentLanguage(),
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
                _me.submitSearch();
            });

            $("#dataContainerList").on("change", "#DeviceType", function () {
                var params = _me.getSearchParam();
                _me.getTextList(params, _me.updatePager);
            });

            $("#btnGenerateLink").on("click", function () {
                _me.submitSearch();
            });
        },

        submitSearch: function () {
            _pageIndex = 0;
            var searchParams = _me.getSearchParam();
            _me.getTextList(searchParams, _me.updatePager);

            MarketingBase.generateTextLink();
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
                url: Site.resolveRelativeUrl("UserPanel/OldMarketing/GetText"),
                success: function (response) {
                    $("#marketingDataList").html(response);
                    onSuccess && onSuccess();

                    var totalItem = parseInt($("#TotalItem").val());
                    $("#marketingPager").toggle(totalItem !== 0);
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