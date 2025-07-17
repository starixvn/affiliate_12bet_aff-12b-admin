var AffiliateLink = (function ($) {

    function handleFalseResponse(response) {
        let data;
        switch (response.status) {
            case 403:
                data = response.json();
                alert(data.ErrMsg);
                location.reload();
                break;
            default:
                alert('Internal error');
                break;
        }
    }

    var _me = {
        initialize: function () {
            _me.initEvent();
            MarketingBase.initEvent();
            _me.renderLinkChannelStatisticPage();
        },

        initEvent: function () {
            $("#btnMarketingSubmit").click(function () {
                Site.showLoader();
                Promise.all([_me.renderQrCodePage(), _me.renderLinkChannelStatisticPage()])
                    .then(() => {
                        Site.hideLoader();
                        MarketingBase.registerToggleLinkEvent(_me.renderLinkChannelStatisticPage);
                        MarketingBase.resiterQrcodeEvent();
                    })
            });

            MarketingBase.initChannelSelectEvent();
        },

        renderLinkChannelStatisticPage: function () {
            return MarketingBase.renderLinkChannelStatisticPage()
                .then((response) => {
                    if (response.ok)
                        return response.text()
                    else {
                        handleFalseResponse(response);
                    }
                })
                .then(html => {
                    $("#link-statistic").empty().html(html);
                })
        },

        renderQrCodePage: function () {
            let param = {
                selectedLanguage: $('#Language').val(),
                selectedPage: $('#Page').val(),
                selectedChannel: $('#Channel').val(),
                selectedDomainId: $('#Domain').val(),
                selectedLinkType: $('#AffLinkType').val(),
                selectedTargetCountry: $('#TargetCountry').val()
            }
            return MarketingBase.renderQrCodePage(param)
                .then((response) => {
                    if (response.ok)
                        return response.text()
                    else {
                        handleFalseResponse(response);
                    }
                })
                .then(html => {
                    $("#qr-code").empty().html(html);
                });
        },
    };

    return _me;
})(jQuery);

AffiliateLink.initialize();