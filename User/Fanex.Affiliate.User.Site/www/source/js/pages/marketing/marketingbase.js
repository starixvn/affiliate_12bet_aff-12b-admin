var MarketingBase = (function ($) {
    var _me = {
        generateTextLink: function () {
            var params = _me.getLinkGenerationParam();
            _me.getTextLink(params);
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
                url: Site.resolveRelativeUrl("UserPanel/OldMarketing/GenerateLink"),
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
        },
    };

    return _me;
})(jQuery);