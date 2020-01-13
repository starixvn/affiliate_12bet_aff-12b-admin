var MediaGallery = (function ($) {
    var CATEGORY_CASINO = "casino";
    var CATEGORY_SPORTSBOOK = "sportsbook";

    var optionAll = "<option value=''>All</option>";

    var _pageSize = 20;
    var _pageIndex = 0;

    function initPager() {
        $("#marketingPager").pager({
            totalItems: parseInt($("#TotalItem").val()),
            pageSize: _pageSize,
            pageIndex: _pageIndex,
            languageCode: Site.getCurrentLanguage(),
            onChange: function (pageIndex, pageSize) {
                _pageIndex = pageIndex;
                _pageSize = pageSize;

                var searchParams = _me.getSearchParam();
                _me.getBannerList(searchParams);
            }
        });
    }

    var _me = {
        initialize: function () {
            _me.initEvent();
            initPager();
            _me.togglePager();
        },

        initEvent: function () {
            $("#Category").on("change", function () {
                var selectedValue = $(this).val().toLowerCase();
                var $subCategoryContainer = $("#SubCategory").closest(".form-group");

                if (selectedValue === CATEGORY_CASINO || selectedValue === CATEGORY_SPORTSBOOK) {
                    $subCategoryContainer.removeClass("hide");
                    _me.getSubcategory(selectedValue);
                }
                else {
                    $("#SubCategory").html(optionAll);
                    $subCategoryContainer.addClass("hide");
                }

                _me.getSize();
            });

            $("#SubCategory").on("change", function () {
                _me.getSize();
            });

            $("#Language").on("change", function () {
                _me.getSize();
            });

            $("#Currency").on("change", function () {
                _me.getSize();
            });

            $("#ImageType").on("change", function () {
                _me.getSize();
            });

            $("#DeviceType").on("change", function () {
                _me.getSize();
            });

            $("#btnMarketingSearch").on("click", function () {
                _me.submitSearch();
            });

            $("#dataContainerList").on("change", "#DeviceType", function () {
                _pageIndex = 0;
                var params = _me.getSearchParam();
                _me.getBannerList(params, _me.updatePager);
            });

            $("#btnGenerateLink").on("click", function () {
                _me.submitSearch();
            });

            $("#marketingDataList").on("click", ".copy-banner-code", function () {
                const el = document.createElement('textarea');
                el.value = $(this).data("name");
                el.setAttribute('readonly', '');
                el.style.position = 'absolute';
                el.style.left = '-9999px';
                document.body.appendChild(el);
                el.select();
                document.execCommand('copy');
                document.body.removeChild(el);
            });

            $("#marketingDataList").on("click", ".facebook-share", function () {
                var facebookWindow = window.open('https://www.facebook.com/sharer/sharer.php?u=' + $(this).data("url"), 'facebook-popup', 'height=350,width=600');
                if (facebookWindow.focus) { facebookWindow.focus(); }
                return false;
            });

            $("#marketingDataList").on("click", ".twitter-share", function () {
                var twiterWindow = window.open('https://twitter.com/share?url=' + $(this).data("url"), 'twiter-popup', 'height=350,width=600');
                if (twiterWindow.focus) { twiterWindow.focus(); }
                return false;
            });

            $("#marketingDataList").on("click", ".whatsapp-share", function () {
                var whatsappWindow = window.open('https://api.whatsapp.com/send?text=' + $(this).data("url"), 'whatsapp-popup', 'height=350,width=600');
                if (whatsappWindow.focus) { whatsappWindow.focus(); }
                return false;
            });

            $("#marketingDataList").on("click", ".wechat-share", function () {
                var wechatWindow = window.open('' + $(this).data("url"), 'wechat-popup', 'height=350,width=600');
                if (wechatWindow.focus) { wechatWindow.focus(); }
                return false;
            });

            $("#marketingDataList").on("click", ".line-share", function () {
                var lineWindow = window.open('https://social-plugins.line.me/lineit/share?url=' + $(this).data("url"), 'line-popup', 'height=350,width=600');
                if (lineWindow.focus) { lineWindow.focus(); }
                return false;
            });

            $("#marketingDataList").on("click", ".weiboi-share", function () {
                var weiboiWindow = window.open('http://service.weibo.com/share/share.php?url=' + $(this).data("url"), 'weiboi-popup', 'height=350,width=600');
                if (weiboiWindow.focus) { weiboiWindow.focus(); }
                return false;
            });

            $("#marketingDataList").on("click", ".qq-share", function () {
                var qqWindow = window.open('' + $(this).data("url"), 'qq-popup', 'height=350,width=600');
                if (qqWindow.focus) { qqWindow.focus(); }
                return false;
            });

            $("#marketingDataList").on("click", ".zalo-share", function () {
                var zaloWindow = window.open('' + $(this).data("url"), 'zalo-popup', 'height=350,width=600');
                if (zaloWindow.focus) { zaloWindow.focus(); }
                return false;
            });

            $("#marketingDataList").on("click", ".telegram-share", function () {
                var telegramWindow = window.open('https://telegram.me/share/url?url=' + $(this).data("url") + '&amp;text=text', 'telegram-popup', 'height=350,width=600');
                if (telegramWindow.focus) { telegramWindow.focus(); }
                return false;
            });

            $("#marketingDataList").on("click", ".talk-share", function () {
                var talkWindow = window.open('' + $(this).data("url"), 'talk-popup', 'height=350,width=600');
                if (talkWindow.focus) { talkWindow.focus(); }
                return false;
            });
        },

        togglePager: function () {
            var totalItem = parseInt($("#TotalItem").val());
            $("#marketingPager").toggle(totalItem !== 0);
        },

        submitSearch: function(){
            _pageIndex = 0;
            var searchParams = _me.getSearchParam();
            _me.getBannerList(searchParams, _me.updatePager);

            MarketingBase.generateTextLink();
        },

        getSubcategory: function (category) {
            $.ajax({
                type: "GET",
                dataType: "json",
                cache: false,
                data: { category: category },
                url: Site.resolveRelativeUrl("UserPanel/Marketing/GetSubCategory"),
                success: function (response) {
                    var html = _me.buildOptionList(response, "Value", "Value", true);
                    $("#SubCategory").html(html);
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        },

        getSize: function () {
            let param = {
                category: $("#Category").val(),
                subCategory: $("#SubCategory").val(),
                language: $("#Language").val(),
                currency: $("#Currency").val(),
                imageType: $("#ImageType").val(),
                deviceType: $("#DeviceType").val()
            };

            $.ajax({
                type: "GET",
                dataType: "json",
                cache: false,
                data: param,
                url: Site.resolveRelativeUrl("UserPanel/Marketing/GetSize"),
                success: function (response) {
                    var html = _me.buildOptionList(response, "Size", "Size", true);
                    $("#Size").html(html);
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        },

        buildOptionList: function (dataList, value, text, hasAllOption) {
            var optionList = dataList.map(function (item) {
                return "<option value='" + item[value] + "'>" + item[text] + "</option>"
            });

            if (hasAllOption) {
                optionList.unshift(optionAll);
            }

            return optionList.join("");
        },

        getSearchParam: function () {
            return {
                category: $("#Category").val(),
                subCategory: $("#SubCategory").val(),
                language: $("#Language").val(),
                imageType: $("#ImageType").val(),
                size: $("#Size").val(),
                currency: $("#Currency").val(),
                deviceType: $("#DeviceType").val(),
                selectedDomainId: $("#Domain").val(),
                selectedPage: $("#MemberSitePage").val(),
                pageIndex: _pageIndex + 1,
                pageSize: _pageSize,
                screenWidth: screen.availWidth
            }
        },

        getBannerList: function (params, onSuccess) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/Marketing/GetBanner"),
                success: function (response) {
                    $("#marketingDataList").html(response);
                    onSuccess && onSuccess();

                    _me.togglePager();
                }
            });
        },

        updatePager: function () {
            $("#marketingPager").pager("update", {
                pageIndex: _pageIndex,
                totalItems: parseInt($("#TotalItem").val())
            });
        }
    };

    return _me;
})(jQuery);

MediaGallery.initialize();