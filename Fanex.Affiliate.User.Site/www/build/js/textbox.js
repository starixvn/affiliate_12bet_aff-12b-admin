"use strict";

var TextBox = (function ($) {
    var _me = {
        initialize: function initialize() {
            _me.initEvent();
        },

        initEvent: function initEvent() {
            $("#btnMarketingSearch").on("click", function () {
                var searchParams = _me.getSearchParam();
                _me.getTextList(searchParams);

                var linkGenerationParams = _me.getLinkGenerationParam();
                _me.getTextLink(linkGenerationParams);
            });

            $("#dataContainerList").on("change", "#DeviceType", function () {
                var params = _me.getSearchParam();
                _me.getTextList(params);
            });

            $("#btnGenerateLink").on("click", function () {
                var params = _me.getLinkGenerationParam();
                _me.getTextLink(params);
            });
        },

        getSearchParam: function getSearchParam() {
            return {
                category: $("#Category").val(),
                language: $("#Language").val(),
                currency: $("#Currency").val(),
                deviceType: $("#DeviceType").val(),
                selectedDomainId: $("#Domain").val(),
                selectedPage: $("#MemberSitePage").val()
            };
        },

        getTextList: function getTextList(params) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/GetText"),
                success: function success(response) {
                    $("#marketingDataList").html(response);
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
                    $("#textLinkList").html(response);
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

TextBox.initialize();

