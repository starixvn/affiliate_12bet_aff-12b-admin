(function ($) {
    var _languageCode;
    var pagingInfoText = {
        'en-US': 'Page {0} of {1}',
        'id-ID': 'Halaman {0} dari {1}',
        'zh-CN': '从 {0} 页到 {1} 页',
        'zh-TW': '從 {0} 頁到 {1} 頁',
        'th-TH': 'หน้า {0} จาก {1}',
        'vi-VN': 'Trang {0}',
        'ko-KR': '페이지 {0} 중 {1}'
    }

    var DEFAULT_PAGESIZE_LIST = [10, 20, 50, 100, 200];
    var defaultOption = {
        pageIndex: 0,
        pageSize: 20,
        totalItems: 0,
        pageSizeList: DEFAULT_PAGESIZE_LIST,
        languageCode: 'en-US',
        onChange: function (pageIndex, pageSize) { },
        onClickPrev: function () { },
        onClickLastPrev: function () { },
        onClickNext: function () { },
        onClickLastNext: function () { },
        onChangePageSize: function () { },
    };

    function getPagingInfoText(pageIndex, totalPage) {
        return (pagingInfoText[_languageCode] || pagingInfoText['en-US']).replace('{0}', pageIndex).replace('{1}', (totalPage || 1));
    }

    function buildPrevButton() {
        var html = ['<a href="javascript:;" title="First" class="icon glyphicon glyphicon-fast-backward page-first disable"></a>'];
        html.push('<a href="javascript:;" title="Prev" class="icon glyphicon glyphicon-triangle-left page-prev disable"></a>');

        return html.join('');
    }

    function buildNextButton(isDisabled) {
        var disable = isDisabled ? "disable" : "";
        var html = ['<a href="javascript:;" title="Next" class="icon glyphicon glyphicon-triangle-right page-next ' + disable + '"></a>'];
        html.push('<a href="javascript:;" title="Last" class="icon glyphicon glyphicon-fast-forward page-last ' + disable + '"></a>');

        return html.join('');
    }

    function buildPageInfo(totalPage) {
        var html = ['<div class="page-input">'];
        html.push('<span class="text-pageInfo">' + getPagingInfoText(1, totalPage) + '</span>')
        html.push('</div>');

        return html.join('');
    }

    function buildPageSizeList(pageSizeList, selectedItem) {
        var htmlPageSize = ['<select class="pagesize-select" >'];
        for (var i = 0; i < pageSizeList.length; i++) {
            var pageSize = pageSizeList[i];
            var selected = pageSize === selectedItem ? "selected" : "";
            htmlPageSize.push('<option value="' + pageSize + '" ' + selected + '>' + pageSize + '</option>');
        }
        htmlPageSize.push('</select>');

        return htmlPageSize.join('');
    }

    function updatePageInfo($pagerContainer, pageIndex, totalPage) {
        $pagerContainer.find('.text-pageInfo').text(getPagingInfoText(pageIndex, totalPage));
    }

    function togglePrevButton($pagerContainer, isEnable) {
        $pagerContainer.find('.page-first').toggleClass('disable', !isEnable);
        $pagerContainer.find('.page-prev').toggleClass('disable', !isEnable);
    }

    function toggleNextButton($pagerContainer, isEnable) {
        $pagerContainer.find('.page-next').toggleClass('disable', !isEnable);
        $pagerContainer.find('.page-last').toggleClass('disable', !isEnable);
    }

    function resetToInitialState($pagerContainer, isDisabledNextButton) {
        togglePrevButton($pagerContainer, false);
        toggleNextButton($pagerContainer, !isDisabledNextButton);
    }

    function calculateTotalPage(totalItems, pageSize) {
        return parseInt(totalItems / pageSize) + (totalItems % pageSize === 0 ? 0 : 1);
    }

    function isDisableNextButton(pageIndex, totalPage) {
        return totalPage === 0 || pageIndex === totalPage - 1;
    }

    var _methods = {
        init: function (option) {
            return this.each(function () {
                var mainOption = $.extend(defaultOption, option || {});

                var $self = $(this);
                $self.find("*").off();
                $self.html("");

                _languageCode = mainOption.languageCode;
                var totalPage = calculateTotalPage(mainOption.totalItems, mainOption.pageSize);
                var pageInfo = {
                    pageIndex: mainOption.pageIndex,
                    pageSize: mainOption.pageSize,
                    totalItems: mainOption.totalItems,
                    totalPage: totalPage
                }

                $self.data('pageInfo', pageInfo);

                var htmlPrevButton = buildPrevButton();
                var htmlPagingInfo = buildPageInfo(totalPage);

                var disableNextButton = pageInfo.totalPage === 0 || pageInfo.pageIndex === pageInfo.totalPage - 1;
                var htmlNextButton = buildNextButton(disableNextButton);
                var htmlPageSizeList = buildPageSizeList(mainOption.pageSizeList, mainOption.pageSize);

                var $pagerContainer = $('<div class="pager-container"></div>');
                $pagerContainer.append(htmlPrevButton + htmlPagingInfo + htmlNextButton + htmlPageSizeList);
                $self.append($pagerContainer);

                $pagerContainer.on('click', '.page-first', function () {
                    var pageInfo = $self.data('pageInfo');
                    if (pageInfo.pageIndex === 0) {
                        return;
                    }

                    pageInfo.pageIndex = 0;
                    updatePageInfo($pagerContainer, pageInfo.pageIndex + 1, pageInfo.totalPage);
                    var disableNextButton = isDisableNextButton(pageInfo.pageIndex, pageInfo.totalPage);
                    resetToInitialState($pagerContainer, disableNextButton);

                    $self.data('pageInfo', pageInfo);

                    mainOption.onChange(pageInfo.pageIndex, pageInfo.pageSize);
                });

                $pagerContainer.on('click', '.page-prev', function () {
                    var pageInfo = $self.data('pageInfo');
                    if (pageInfo.pageIndex === 0) {
                        return;
                    }

                    pageInfo.pageIndex--;
                    if (pageInfo.pageIndex === 0) {
                        togglePrevButton($pagerContainer, false);
                    }

                    toggleNextButton($pagerContainer, true);
                    updatePageInfo($pagerContainer, pageInfo.pageIndex + 1, pageInfo.totalPage);

                    $self.data('pageInfo', pageInfo);
                    mainOption.onChange(pageInfo.pageIndex, pageInfo.pageSize);
                });

                $pagerContainer.on('click', '.page-next', function () {
                    var pageInfo = $self.data('pageInfo');
                    if (pageInfo.pageIndex === pageInfo.totalPage - 1) {
                        return;
                    }

                    pageInfo.pageIndex++;
                    if (pageInfo.pageIndex === pageInfo.totalPage - 1) {
                        toggleNextButton($pagerContainer, false);
                    }

                    togglePrevButton($pagerContainer, true);
                    $self.data('pageInfo', pageInfo);
                    updatePageInfo($pagerContainer, pageInfo.pageIndex + 1, pageInfo.totalPage);

                    mainOption.onChange(pageInfo.pageIndex, pageInfo.pageSize);
                });

                $pagerContainer.on('click', '.page-last', function () {
                    var pageInfo = $self.data('pageInfo');
                    if (pageInfo.pageIndex === pageInfo.totalPage - 1) {
                        return;
                    }

                    pageInfo.pageIndex = pageInfo.totalPage - 1;
                    updatePageInfo($pagerContainer, pageInfo.pageIndex + 1, pageInfo.totalPage);
                    togglePrevButton($pagerContainer, true);
                    toggleNextButton($pagerContainer, false);

                    $self.data('pageInfo', pageInfo);
                    mainOption.onChange(pageInfo.pageIndex, pageInfo.pageSize);
                });

                $pagerContainer.on('change', '.pagesize-select', function () {
                    var pageInfo = $self.data('pageInfo');
                    pageInfo.pageIndex = 0;
                    pageInfo.pageSize = parseInt($(this).val());

                    var totalPage = calculateTotalPage(pageInfo.totalItems, pageInfo.pageSize);
                    pageInfo.totalPage = totalPage;

                    updatePageInfo($pagerContainer, pageInfo.pageIndex + 1, pageInfo.totalPage);
                    var disableNextButton = isDisableNextButton(pageInfo.pageIndex, pageInfo.totalPage);
                    resetToInitialState($pagerContainer, disableNextButton);

                    $self.data('pageInfo', pageInfo);
                    mainOption.onChange(pageInfo.pageIndex, pageInfo.pageSize);
                });
            });
        },

        update: function (pageInfo) {
            var $self = $(this);
            var oldPageInfo = $self.data('pageInfo');
            var newPageInfo = $.extend({}, oldPageInfo, pageInfo);
            newPageInfo.totalPage = calculateTotalPage(newPageInfo.totalItems, newPageInfo.pageSize);

            var $pagerContainer = $self.find('.pager-container');
            updatePageInfo($pagerContainer, newPageInfo.pageIndex + 1, newPageInfo.totalPage);
            var disableNextButton = isDisableNextButton(newPageInfo.pageIndex, newPageInfo.totalPage);
            resetToInitialState($pagerContainer, disableNextButton);
            $self.data('pageInfo', newPageInfo);
        }
    }

    $.fn.pager = function (methodOrOptions) {
        if (_methods[methodOrOptions]) {
            return _methods[methodOrOptions].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof methodOrOptions === 'object' || !methodOrOptions) {
            // Default to "init"
            return _methods.init.apply(this, arguments);
        } else {
            $.error('Method ' + methodOrOptions + ' does not exist on jQuery.pager');
        }
    };
})(jQuery);
var MarketingBase = (function ($) {
    const NO_INFO_OPTION = "<option value=''>" + langResource.noInfo + "</option>";

    var _me = {
        initialize: function () {
            _me.initEvent();
        },

        initEvent: function () {
            $("#TargetCountry").on("change", function () {
                $("#btnMarketingSubmit").prop("disabled", false);
                _me.handleChangeTargetCountry();
            });
        },

        handleChangeTargetCountry: function () {
            let shouldGetSize = !!$("#Size").length;

            let param = {
                targetProduct: $("#TargetProduct").val(),
                targetCountry: $("#TargetCountry").val(),
                shouldGetSize
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
                        $("#MemberSitePage").html(NO_INFO_OPTION);
                        $("#Language").html(NO_INFO_OPTION);
                        shouldGetSize && $("#Size").html(NO_INFO_OPTION);
                    } else {
                        var { domainList, languageList, pageList, sizeList } = response;

                        _me.updateLanguageDropdown(languageList);
                        _me.updateAvailableDomainDropdown(domainList);
                        _me.updateLinkToDropdown(pageList);
                        shouldGetSize && _me.updateSizeDropdown(sizeList);
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

            var html = Site.buildOptionList(languageList, "Value", "Text", languageList.length > 1);

            $("#Language").html(html);
            $("#Language").val(languageList[0].Value);
        },

        updateSizeDropdown: function (sizeList) {
            var html = Site.buildOptionList(sizeList, "Size", "Size", true);
            var previousSelectedSize = $("#Size").val();

            $("#Size").html(html);

            var hasPreviousSelectedSize = sizeList.some(i => i.Size == previousSelectedSize);

            if (hasPreviousSelectedSize) {
                $("#Size").val(previousSelectedSize);
            }
        }
    };

    return _me;
})(jQuery);

$(document).ready(function () {
    MarketingBase.initialize();
})
var MediaGallery = (function ($) {
    var _pageSize = 20;
    var _pageIndex = 0;

    let isCountDown = false;
    let preBannerId = 0;
    let myTimeout = null;

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
            $("#TargetProduct").on("change", function () {
                _me.getSize();
            });

            $("#Language").on("change", function () {
                _me.getSize();
            });

            $("#btnMarketingSubmit").on("click", function () {
                _me.submitSearch();
            });

            $("#alertMediaBlockRemove").on("click", function () {
                _me.hideAlert();
                myTimeout && window.clearTimeout(myTimeout);
            });

            var clipboard = new ClipboardJS('.copy-banner-code');

            clipboard.on('success', function (e) {
                var data = e.trigger.dataset;
                data && _me.countNumberOfCopies(data.original);
            })
        },

        togglePager: function () {
            var totalItem = parseInt($("#TotalItem").val());
            $("#marketingPager").toggle(totalItem !== 0);
        },

        submitSearch: function(){
            _pageIndex = 0;
            var searchParams = _me.getSearchParam();
            _me.getBannerList(searchParams, _me.updatePager);
        },

        countNumberOfCopies: function (bannerId) {
            if (!isCountDown || bannerId != preBannerId) {
                $("#alertMediaBlock").show();

                $(document.body).css('overflow', 'hidden');
                myTimeout && window.clearTimeout(myTimeout);
                myTimeout = window.setTimeout(_me.hideAlert, 3000);

                $.ajax({
                    type: "POST",
                    dataType: "json",
                    cache: false,
                    data: { bannerId },
                    url: Site.resolveRelativeUrl("UserPanel/MediaGallery/CountNumberOfCopies"),
                    success: function (response) {
                    },
                    beforeSend: function () {
                        isCountDown = true;
                        preBannerId = bannerId;
                    }
                });
            }
        },

        hideAlert: function () {
            isCountDown = false;
            $(document.body).css('overflow', 'auto');
            $("#alertMediaBlock").hide();
        },

        getSize: function () {
            let param = {
                targetProduct: $("#TargetProduct").val(),
                language: $("#Language").val(),
                targetCountry: $("#TargetCountry").val(),
            };

            $.ajax({
                type: "GET",
                dataType: "json",
                cache: false,
                data: param,
                url: Site.resolveRelativeUrl("UserPanel/MediaGallery/GetSize"),
                success: function (response) {
                    var html = Site.buildOptionList(response, "Size", "Size", true);
                    var previousSelectedSize = $("#Size").val();

                    $("#Size").html(html);

                    var hasPreviousSelectedSize = response.some(i => i.Size == previousSelectedSize);

                    if (hasPreviousSelectedSize) {
                        $("#Size").val(previousSelectedSize);
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

        getSearchParam: function () {
            return {
                selectedTargetCountry: $("#TargetCountry").val(),
                targetProduct: $("#TargetProduct").val(),
                language: $("#Language").val(),
                size: $("#Size").val(),
                selectedDomainId: $("#Domain").val(),
                selectedPage: $("#MemberSitePage").val(),
                pageIndex: _pageIndex + 1,
                pageSize: _pageSize
            }
        },

        getBannerList: function (params, onSuccess) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/MediaGallery/GetBanner"),
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