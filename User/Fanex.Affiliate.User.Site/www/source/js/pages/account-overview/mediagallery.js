var MediaGallery = (function ($) {
    var _pageSize = 20;
    var _pageIndex = 0;

    let isCountDown = false;
    let preBannerId = 0;
    let myTimeout = null;

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
                _me.getBannerList(searchParams);
            }
        });
    }

    var _me = {
        initialize: function () {
            _me.initEvent();
            initPager();
            _me.togglePager();
        },

        initEvent: function () {
            $("#TargetProduct").on("change", function () {
                _me.getSize();
            });

            $("#Language").on("change", function () {
                _me.getSize();
            });

            $("#btnMarketingSearch").on("click", function () {
                _me.submitSearch();
            });

            $("#alertMediaBlockRemove").on("click", function () {
                _me.hideAlert();
                myTimeout && window.clearTimeout(myTimeout);
            });

            var clipboard = new ClipboardJS('.copy-banner-code');

            clipboard.on('success', function (e) {
                var data = e.trigger.dataset;
                data && _me.countNumberOfCopies(data.original);
            })
        },

        togglePager: function () {
            var totalItem = parseInt($("#TotalItem").val());
            $("#marketingPager").toggle(totalItem !== 0);
        },

        submitSearch: function(){
            _pageIndex = 0;
            var searchParams = _me.getSearchParam();
            _me.getBannerList(searchParams, _me.updatePager);
        },

        countNumberOfCopies: function (bannerId) {
            if (!isCountDown || bannerId != preBannerId) {
                $("#alertMediaBlock").show();

                $(document.body).css('overflow', 'hidden');
                myTimeout && window.clearTimeout(myTimeout);
                myTimeout = window.setTimeout(_me.hideAlert, 3000);

                $.ajax({
                    type: "POST",
                    dataType: "json",
                    cache: false,
                    data: { bannerId },
                    url: Site.resolveRelativeUrl("UserPanel/MediaGallery/CountNumberOfCopies"),
                    success: function (response) {
                    },
                    beforeSend: function () {
                        isCountDown = true;
                        preBannerId = bannerId;
                    }
                });
            }
        },

        hideAlert: function () {
            isCountDown = false;
            $(document.body).css('overflow', 'auto');
            $("#alertMediaBlock").hide();
        },

        getSize: function () {
            let param = {
                targetProduct: $("#TargetProduct").val(),
                language: $("#Language").val(),
            };

            $.ajax({
                type: "GET",
                dataType: "json",
                cache: false,
                data: param,
                url: Site.resolveRelativeUrl("UserPanel/MediaGallery/GetSize"),
                success: function (response) {
                    var html = Site.buildOptionList(response, "Size", "Size", true);
                    var previousSelectedSize = $("#Size").val();

                    $("#Size").html(html);

                    var hasPreviousSelectedSize = response.some(i => i.Size == previousSelectedSize);

                    if (hasPreviousSelectedSize) {
                        $("#Size").val(previousSelectedSize);
                    }
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        },

        getSearchParam: function () {
            return {
                targetProduct: $("#TargetProduct").val(),
                language: $("#Language").val(),
                size: $("#Size").val(),
                selectedDomainId: $("#Domain").val(),
                selectedPage: $("#MemberSitePage").val(),
                pageIndex: _pageIndex + 1,
                pageSize: _pageSize
            }
        },

        getBannerList: function (params, onSuccess) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/MediaGallery/GetBanner"),
                success: function (response) {
                    $("#marketingDataList").html(response);
                    onSuccess && onSuccess();

                    _me.togglePager();
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

MediaGallery.initialize();