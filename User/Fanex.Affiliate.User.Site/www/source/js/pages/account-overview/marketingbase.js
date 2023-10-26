var MarketingBase = (function ($) {
    var _me = {
        initialize: function () {
            _me.initEvent();
        },

        initEvent: function () {
            $("#Language").on("change", function () {
                _me.getLinkTo();
            });

            $("#TargetCountry").on("change", function () {
                _me.getLinkTo();
            });
        },

        getLinkTo: function () {
            let param = {
                targetCountry: $("#TargetCountry").val(),
                language: $("#Language").val()
            };

            $.ajax({
                type: "GET",
                dataType: "json",
                cache: false,
                data: param,
                url: Site.resolveRelativeUrl("UserPanel/Marketing/GetLinkTo"),
                success: function (response) {
                    var html = Site.buildOptionList(response, "PageCode", "PageName", false);
                    var previousSelectedPage = $("#MemberSitePage").val();

                    $("#MemberSitePage").html(html);

                    var hasPreviousSelectedPage = response.some(i => i.PageCode == previousSelectedPage);

                    if (hasPreviousSelectedPage) {
                        $("#MemberSitePage").val(previousSelectedPage);
                    }
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

$(document).ready(function () {
    MarketingBase.initialize();
})