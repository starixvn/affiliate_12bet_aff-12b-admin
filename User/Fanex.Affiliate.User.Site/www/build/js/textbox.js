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

            var $pagerContainer = $self.find('.page-container');
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
})(jQuery)
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
var TextBox = (function ($) {
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
                _me.getTextList(searchParams);
            }
        });
    }

    var _me = {
        initialize: function () {
            _me.initEvent();
            initPager();
        },

        initEvent: function () {
            $("#btnMarketingSearch").on("click", function () {
                _me.submitSearch();
            });

            $("#dataContainerList").on("change", "#DeviceType", function () {
                var params = _me.getSearchParam();
                _me.getTextList(params, _me.updatePager);
            });

            $("#btnGenerateLink").on("click", function () {
                _me.submitSearch();
            });
        },

        submitSearch: function () {
            _pageIndex = 0;
            var searchParams = _me.getSearchParam();
            _me.getTextList(searchParams, _me.updatePager);

            MarketingBase.generateTextLink();
        },

        getSearchParam: function () {
            return {
                category: $("#Category").val(),
                language: $("#Language").val(),
                currency: $("#Currency").val(),
                deviceType: $("#DeviceType").val(),
                selectedDomainId: $("#Domain").val(),
                selectedPage: $("#MemberSitePage").val(),
                pageIndex: _pageIndex + 1,
                pageSize: _pageSize
            }
        },

        getTextList: function (params, onSuccess) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/OldMarketing/GetText"),
                success: function (response) {
                    $("#marketingDataList").html(response);
                    onSuccess && onSuccess();

                    var totalItem = parseInt($("#TotalItem").val());
                    $("#marketingPager").toggle(totalItem !== 0);
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

TextBox.initialize();