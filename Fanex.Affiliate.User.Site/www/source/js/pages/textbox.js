var TextBox = (function ($) {
    var _me = {
        initialize: function () {
            _me.initEvent();
        },

        initEvent: function () {
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

        getSearchParam: function () {
            return {
                category: $("#Category").val(),
                language: $("#Language").val(),
                currency: $("#Currency").val(),
                deviceType: $("#DeviceType").val(),
                selectedDomainId: $("#Domain").val(),
                selectedPage: $("#MemberSitePage").val()
            }
        },

        getTextList: function (params) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/GetText"),
                success: function (response) {
                    $("#marketingDataList").html(response);
                }
            });
        },

        getLinkGenerationParam: function () {
            return {
                selectedLanguage: $("#Language").val(),
                selectedDomainId: $("#Domain").val(),
                selectedPage: $("#MemberSitePage").val()
            }
        },

        getTextLink: function (params) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/GenerateLink"),
                success: function (response) {
                    $("#textLinkList").html(response);
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        }
    };

    return _me;
})(jQuery);

TextBox.initialize();