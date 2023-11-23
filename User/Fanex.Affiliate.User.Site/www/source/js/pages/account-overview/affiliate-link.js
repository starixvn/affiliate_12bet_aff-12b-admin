var AffiliateLink = (function ($) {
    var _me = {
        initialize: function () {
            _me.initEvent();
            _me.resiterQrcodeEvent();
        },

        initEvent: function () {
            $("#btnMarketingSubmit").on("click", function () {
                _me.generateTextLink();
            });
        },

        resiterQrcodeEvent: function () {
            new ClipboardJS('.btn-copy-link');

            new ClipboardJS('.btn-copy-qr', {
                text: function (trigger) {
                    let imgTag = $(trigger).nextAll("img")[0];
                    return imgTag.outerHTML;
                }
            });

            $('.btn-save-img').click(function () {
                let base64Img = $(this).nextAll("img").attr("src");
                let downloadTag = document.createElement("a");
                let downloadName = $(this).data("image-name");
                downloadTag.href = base64Img;
                downloadTag.download = downloadName;
                downloadTag.click();
            });
        },
        generateTextLink: function () {
            var params = _me.getLinkGenerationParam();
            _me.getTextLink(params);
        },

        getLinkGenerationParam: function () {
            return {
                selectedTargetCountry: $("#TargetCountry").val(),
                selectedDomainId: $("#Domain").val(),
                selectedLanguage: $("#Language").val(),
                selectedPage: $("#MemberSitePage").val()
            }
        },

        getTextLink: function (params) {
            var that = this;
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/AffiliateLink/GenerateLink"),
                success: function (response) {
                    $("#textLinkList").html(response);
                    that.resiterQrcodeEvent();
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

AffiliateLink.initialize();