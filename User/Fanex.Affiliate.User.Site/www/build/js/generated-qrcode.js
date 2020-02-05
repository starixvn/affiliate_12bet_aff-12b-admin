var MarketingBase = (function ($) {
    var _me = {
        initialize: function () {

            $("#btnGenerateLink").on("click", function () {
                MarketingBase.generateTextLink();
            })
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
                downloadTag.href = base64Img;
                downloadTag.download = "qr-code.png";
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
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/Marketing/GenerateLink"),
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

$(document).ready(function () {
    MarketingBase.initialize();
})