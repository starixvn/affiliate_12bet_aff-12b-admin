var MediaGallery = (function ($) {
    var CATEGORY_CASINO = "casino";
    var CATEGORY_SPORTSBOOK = "sportsbook";

    var optionAll = "<option value=''>All</option>";

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
            $("#Category").on("change", function () {
                var selectedValue = $(this).val().toLowerCase();
                var $subCategoryContainer = $("#SubCategory").closest(".form-group");

                if (selectedValue === CATEGORY_CASINO || selectedValue === CATEGORY_SPORTSBOOK) {
                    $subCategoryContainer.removeClass("hide");
                    _me.getSubcategory(selectedValue);
                }
                else {
                    $("#SubCategory").html(optionAll);
                    $subCategoryContainer.addClass("hide");
                }

                _me.getSize();
            });

            $("#SubCategory").on("change", function () {
                _me.getSize();
            });

            $("#Language").on("change", function () {
                _me.getSize();
            });

            $("#Currency").on("change", function () {
                _me.getSize();
            });

            $("#ImageType").on("change", function () {
                _me.getSize();
            });

            $("#DeviceType").on("change", function () {
                _me.getSize();
            });

            $("#btnMarketingSearch").on("click", function () {
                _me.submitSearch();

                MarketingBase.generateTextLink();
            });

            $("#dataContainerList").on("change", "#DeviceType", function () {
                _me.submitSearch();
            });

            $("#btnGenerateLink").on("click", function () {
                _me.submitSearch();

                MarketingBase.generateTextLink();
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

        getSubcategory: function (category) {
            $.ajax({
                type: "GET",
                dataType: "json",
                cache: false,
                data: { category: category },
                url: Site.resolveRelativeUrl("UserPanel/Marketing/GetSubCategory"),
                success: function (response) {
                    var html = _me.buildOptionList(response, "Value", "Value", true);
                    $("#SubCategory").html(html);
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        },

        countNumberOfCopies: function (bannerId) {
            if (!isCountDown || bannerId != preBannerId) {
                $("#alertMediaBlock").show();

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

            $("#alertMediaBlock").hide();
        },

        getSize: function () {
            let param = {
                category: $("#Category").val(),
                subCategory: $("#SubCategory").val(),
                language: $("#Language").val(),
                currency: $("#Currency").val(),
                imageType: $("#ImageType").val(),
                deviceType: $("#DeviceType").val()
            };

            $.ajax({
                type: "GET",
                dataType: "json",
                cache: false,
                data: param,
                url: Site.resolveRelativeUrl("UserPanel/Marketing/GetSize"),
                success: function (response) {
                    var html = _me.buildOptionList(response, "Size", "Size", true);
                    $("#Size").html(html);
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        },

        buildOptionList: function (dataList, value, text, hasAllOption) {
            var optionList = dataList.map(function (item) {
                return "<option value='" + item[value] + "'>" + item[text] + "</option>"
            });

            if (hasAllOption) {
                optionList.unshift(optionAll);
            }

            return optionList.join("");
        },

        getSearchParam: function () {
            return {
                category: $("#Category").val(),
                subCategory: $("#SubCategory").val(),
                language: $("#Language").val(),
                imageType: $("#ImageType").val(),
                size: $("#Size").val(),
                currency: $("#Currency").val(),
                deviceType: $("#DeviceType").val(),
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
                url: Site.resolveRelativeUrl("UserPanel/Marketing/GetBanner"),
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