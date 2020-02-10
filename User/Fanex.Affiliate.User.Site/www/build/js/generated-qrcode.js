var MarketingBase = (function ($) {
    var _me = {
        resiterQrcodeEvent: function() {
            new ClipboardJS('.btn-copy-link');

            new ClipboardJS('.btn-copy-qr', {
                text: function (trigger) {
                    let imgTag = $(trigger).nextAll("img")[0];
                    return imgTag.outerHTML;
                }
            })

            $('.btn-save-img').click(function () {
                let base64Img = $(this).nextAll("img").attr("src");
                let downloadTag = document.createElement("a");
                let downloadName = $(this).data("image-name");
                downloadTag.href = base64Img;
                downloadTag.download = downloadName;
                downloadTag.click();
            })
        },
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
            var that = this;
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/Marketing/GenerateLink"),
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
        },

        initialize: function () {
            this.resiterQrcodeEvent();
        },
    };

    return _me;
})(jQuery);

$(document).ready(function () {
    MarketingBase.initialize();
})