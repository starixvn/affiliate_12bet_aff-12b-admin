"use strict";

var MediaGallery = (function ($) {
    var CATEGORY_CASINO = "casino";
    var CATEGORY_SPORTSBOOK = "sportsbook";

    var optionAll = "<option value=''>All</option>";

    var _me = {
        initialize: function initialize() {
            _me.initEvent();
        },

        initEvent: function initEvent() {
            $("#Category").on("change", function () {
                var selectedValue = $(this).val().toLowerCase();
                var $subCategoryContainer = $("#SubCategory").closest(".form-group");

                if (selectedValue === CATEGORY_CASINO || selectedValue === CATEGORY_SPORTSBOOK) {
                    $subCategoryContainer.removeClass("hide");
                    _me.getSubcategory(selectedValue);
                } else {
                    $("#SubCategory").html(optionAll);
                    $subCategoryContainer.addClass("hide");
                }
            });

            $("#btnMediaGallerySearch").on("click", function () {
                var searchParams = _me.getSearchParam();
                _me.getBannerList(searchParams);

                var linkGenerationParams = _me.getLinkGenerationParam();
                _me.getTextLink(linkGenerationParams);
            });

            $("#mediaGalleryBannerList").on("change", "#DeviceType", function () {
                var params = _me.getSearchParam();
                _me.getBannerList(params);
            });

            $("#btnGenerateLink").on("click", function () {
                var params = _me.getLinkGenerationParam();
                _me.getTextLink(params);
            });
        },

        getSubcategory: function getSubcategory(category) {
            $.ajax({
                type: "GET",
                dataType: "json",
                cache: false,
                data: { category: category },
                url: Site.resolveRelativeUrl("UserPanel/GetSubCategory"),
                success: function success(response) {
                    var html = _me.buildOptionList(response, "Value", "Value", true);
                    $("#SubCategory").html(html);
                },
                beforeSend: function beforeSend() {
                    Site.showLoader();
                },
                complete: function complete() {
                    Site.hideLoader();
                }
            });
        },

        buildOptionList: function buildOptionList(dataList, value, text, hasAllOption) {
            var optionList = dataList.map(function (item) {
                return "<option value='" + item[value] + "'>" + item[text] + "</option>";
            });

            if (hasAllOption) {
                optionList.unshift(optionAll);
            }

            return optionList.join("");
        },

        getSearchParam: function getSearchParam() {
            return {
                category: $("#Category").val(),
                subCategory: $("#SubCategory").val(),
                language: $("#Language").val(),
                imageType: $("#ImageType").val(),
                size: $("#Size").val(),
                currency: $("#Currency").val(),
                deviceType: $("#DeviceType").val(),
                selectedDomainId: $("#Domain").val(),
                selectedPage: $("#MemberSitePage").val()
            };
        },

        getBannerList: function getBannerList(params) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/GetBanner"),
                success: function success(response) {
                    $("#bannerList").html(response);
                }
            });
        },

        getLinkGenerationParam: function getLinkGenerationParam() {
            return {
                selectedLanguage: $("#Language").val(),
                selectedDomainId: $("#Domain").val(),
                selectedPage: $("#MemberSitePage").val()
            };
        },

        getTextLink: function getTextLink(params) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/GenerateLink"),
                success: function success(response) {
                    $("#mediaGalleryTextLinkList").html(response);
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

MediaGallery.initialize();

