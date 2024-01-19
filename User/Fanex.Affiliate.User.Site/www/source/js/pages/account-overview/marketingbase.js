var MarketingBase = (function ($) {
    const NO_INFO_OPTION = "<option value=''>" + langResource.noInfo + "</option>";

    var _me = {
        initialize: function () {
            _me.initEvent();
        },

        initEvent: function () {
            $("#Language").on("change", function (e) {
                _me.getLinkTo();
            });

            $("#TargetCountry").on("change", function () {
                $("#btnMarketingSubmit").prop("disabled", false);
                _me.handleChangeTargetCountry();
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

        handleChangeTargetCountry: function () {
            let param = {
                targetCountry: $("#TargetCountry").val()
            };

            $.ajax({
                type: "GET",
                dataType: "json",
                cache: false,
                data: param,
                url: Site.resolveRelativeUrl("UserPanel/Marketing/GetFiltersByCountry"),
                success: function (response) {
                    if (response && response.length == 0) {
                        $("#btnMarketingSubmit").prop("disabled", true);
                        $("#Domain").html(NO_INFO_OPTION);
                        $("#MemberSitePage").html(NO_INFO_OPTION)
                        $("#Language").html(NO_INFO_OPTION)
                    } else {
                        var { domainList, languageList, pageList } = response;

                        _me.updateLanguageDropdown(languageList);
                        _me.updateAvailableDomainDropdown(domainList);
                        _me.updateLinkToDropdown(pageList);
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

        updateAvailableDomainDropdown: function (availableDomainList) {
            if (availableDomainList && !availableDomainList.length) {
                $("#btnMarketingSubmit").prop("disabled", true);
                $("#Domain").html(NO_INFO_OPTION);

                return;
            }

            var html = Site.buildOptionList(availableDomainList, "DomainId", "DomainName", false);

            var previousSelectedDomain = $("#Domain").val();

            $("#Domain").html(html);

            var hasPreviousSelectedDomain = availableDomainList.some(i => i.DomainId == previousSelectedDomain);

            if (hasPreviousSelectedDomain) {
                $("#Domain").val(previousSelectedDomain);
            }
        },

        updateLinkToDropdown: function (pageList) {
            if (pageList && !pageList.length) {
                $("#btnMarketingSubmit").prop("disabled", true);
                $("#MemberSitePage").html(NO_INFO_OPTION);

                return;
            }

            var html = Site.buildOptionList(pageList, "PageCode", "PageName", false);

            var previousSelectedPage = $("#MemberSitePage").val();

            $("#MemberSitePage").html(html);

            var hasPreviousSelectedPage = pageList.some(i => i.PageCode == previousSelectedPage);

            if (hasPreviousSelectedPage) {
                $("#MemberSitePage").val(previousSelectedPage);
            }
        },

        updateLanguageDropdown: function (languageList) {
            if (languageList && !languageList.length) {
                $("#btnMarketingSubmit").prop("disabled", true);
                $("#Language").html(NO_INFO_OPTION);

                return;
            }

            var html = Site.buildOptionList(languageList, "LanguageCode", "LanguageName", languageList.length > 1);

            $("#Language").html(html);
            $("#Language").val(languageList[0].LanguageCode);
        }
    };

    return _me;
})(jQuery);

$(document).ready(function () {
    MarketingBase.initialize();
})