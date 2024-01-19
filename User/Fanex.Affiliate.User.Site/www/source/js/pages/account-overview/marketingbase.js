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
                _me.getAvailableDomains();
                _me.getLinkTo();
                _me.getLanguages();
            });
        },

        getLinkTo: function () {
            let param = {
                targetCountry: $("#TargetCountry").val()
            };

            $.ajax({
                type: "GET",
                dataType: "json",
                cache: false,
                data: param,
                url: Site.resolveRelativeUrl("UserPanel/Marketing/GetLinkTo"),
                success: function (response) {
                    if (response && response.length == 0) {
                        $("#btnMarketingSubmit").prop("disabled", true);
                        $("#MemberSitePage").html("<option value=''>" + langResource.noInfo + "</option>")
                    } else {
                        $("#Domain").val() && $("#btnMarketingSubmit").prop("disabled", false);
                        var html = Site.buildOptionList(response, "PageCode", "PageName", false);
                        var previousSelectedPage = $("#MemberSitePage").val();

                        $("#MemberSitePage").html(html);

                        var hasPreviousSelectedPage = response.some(i => i.PageCode == previousSelectedPage);

                        if (hasPreviousSelectedPage) {
                            $("#MemberSitePage").val(previousSelectedPage);
                        }
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

        getAvailableDomains: function () {
            let param = {
                targetCountry: $("#TargetCountry").val()
            };

            $.ajax({
                type: "GET",
                dataType: "json",
                cache: false,
                data: param,
                url: Site.resolveRelativeUrl("UserPanel/Marketing/GetAvailableDomains"),
                success: function (response) {
                    if (response && response.length == 0) {
                        $("#btnMarketingSubmit").prop("disabled", true);
                        $("#Domain").html("<option value=''>" + langResource.noInfo + "</option>");
                    } else {
                        $("#MemberSitePage").val() && $("#btnMarketingSubmit").prop("disabled", false);
                        var html = Site.buildOptionList(response, "DomainId", "DomainName", false);
                        var previousSelectedDomain = $("#Domain").val();

                        $("#Domain").html(html);

                        var hasPreviousSelectedDomain = response.some(i => i.DomainId == previousSelectedDomain);

                        if (hasPreviousSelectedDomain) {
                            $("#Domain").val(previousSelectedDomain);
                        }
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

        getLanguages: function () {
            let param = {
                targetCountry: $("#TargetCountry").val()
            };

            $.ajax({
                type: "GET",
                dataType: "json",
                cache: false,
                data: param,
                url: Site.resolveRelativeUrl("UserPanel/Marketing/GetLanguagesByTargetCountry"),
                success: function (response) {
                    if (response && response.length == 0) {
                        $("#btnMarketingSubmit").prop("disabled", true);
                        $("#Language").html("<option value=''>" + langResource.noInfo + "</option>");
                    } else {
                        var html = Site.buildOptionList(response, "LanguageCode", "LanguageName", true);

                        $("#Language").html(html);
                        $("#Language").val(response[0].LanguageCode);
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