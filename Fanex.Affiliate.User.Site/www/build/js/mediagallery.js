(function ($) {
    var DEFAULT_PAGESIZE_LIST = [10, 20, 50, 100, 200];
    var defaultOption = {
        pageIndex: 0,
        pageSize: 20,
        totalItems: 0,
        pageSizeList: DEFAULT_PAGESIZE_LIST,
        onClickPrev: function () { },
        onClickLastPrev: function () { },
        onClickNext: function () { },
        onClickLastNext: function () { },
        onChangePageSize: function () { },
    };

    function buildPrevButton() {
        var html = ['<a href="javascript:;" title="First" class="icon glyphicon glyphicon-fast-backward page-first disable"></a>'];
        html.push('<a href="javascript:;" title="Prev" class="icon glyphicon glyphicon-triangle-left page-prev disable"></a>');

        return html.join('');
    }

    function buildNextButton() {
        var html = ['<a href="javascript:;" title="Next" class="icon glyphicon glyphicon-triangle-right page-next"></a>'];
        html.push('<a href="javascript:;" title="Last" class="icon glyphicon glyphicon-fast-forward page-last"></a>');

        return html.join('');
    }

    function buildPageInfo(totalPage) {
        var html = ['<div class="page-input">'];
        html.push('<span class="text-pageInfo">Page 1 of ' + totalPage + '</span>')
        html.push('</div>');

        return html.join('');
    }

    function buildPageSizeList(pageSizeList) {
        var htmlPageSize = ['<select class="pagesize-select" >'];
        for (var i = 0; i < pageSizeList.length; i++) {
            htmlPageSize.push('<option value="' + pageSizeList[i] + '">' + pageSizeList[i] + '</option>');
        }
        htmlPageSize.push('</select>');

        return htmlPageSize.join('');
    }

    function updatePageInfo($pagerContainer, pageIndex, totalPage) {
        $pagerContainer.find('.text-pageInfo').text('Page ' + pageIndex + ' of ' + totalPage);
    }

    function togglePrevButton($pagerContainer, isEnable) {
        $pagerContainer.find('.page-first').toggleClass('disable', !isEnable);
        $pagerContainer.find('.page-prev').toggleClass('disable', !isEnable);
    }

    function toggleNextButton($pagerContainer, isEnable) {
        $pagerContainer.find('.page-next').toggleClass('disable', !isEnable);
        $pagerContainer.find('.page-last').toggleClass('disable', !isEnable);
    }

    var _methods = {
        init: function (option) {
            return this.each(function () {
                var mainOption = $.extend(defaultOption, option || {});

                var $self = $(this);
                var totalPage = parseInt(mainOption.totalItems / mainOption.pageSize) + (mainOption.totalItems % mainOption.pageSize === 0 ? 0 : 1);
                $self.pageInfo = {
                    pageIndex: mainOption.pageIndex,
                    pageSize: mainOption.pageSize,
                    totalItems: mainOption.totalItems,
                    totalPage: totalPage
                }

                var htmlPrevButton = buildPrevButton();
                var htmlPagingInfo = buildPageInfo(totalPage);
                var htmlNextButton = buildNextButton();
                var htmlPageSizeList = buildPageSizeList(mainOption.pageSizeList);

                var $pagerContainer = $('<div class="page-container"></div>');
                $pagerContainer.append(htmlPrevButton + htmlPagingInfo + htmlNextButton + htmlPageSizeList);
                $self.append($pagerContainer);

                $pagerContainer.on('click', '.page-first', function () {
                    var pageInfo = $self.pageInfo;
                    if (pageInfo.pageIndex === 0) {
                        return;
                    }
                    
                    pageInfo.pageIndex = 0;
                    updatePageInfo($pagerContainer, pageInfo.pageIndex + 1, pageInfo.totalPage);
                    togglePrevButton($pagerContainer, false);
                    toggleNextButton($pagerContainer, true);
                });

                $pagerContainer.on('click', '.page-prev', function () {
                    var pageInfo = $self.pageInfo;
                    if (pageInfo.pageIndex === 0) {
                        return;
                    }

                    pageInfo.pageIndex--;
                    if (pageInfo.pageIndex === 0) {
                        togglePrevButton($pagerContainer, false);
                    }

                    toggleNextButton($pagerContainer, true);
                    updatePageInfo($pagerContainer, pageInfo.pageIndex + 1, pageInfo.totalPage);
                });

                $pagerContainer.on('click', '.page-next', function () {
                    var pageInfo = $self.pageInfo;
                    console.log(pageInfo);
                    if (pageInfo.pageIndex === pageInfo.totalPage - 1) {
                        return;
                    }

                    pageInfo.pageIndex++;
                    if (pageInfo.pageIndex === pageInfo.totalPage - 1) {
                        toggleNextButton($pagerContainer, false);
                    }

                    togglePrevButton($pagerContainer, true);
                    updatePageInfo($pagerContainer, pageInfo.pageIndex + 1, pageInfo.totalPage);
                });

                $pagerContainer.on('click', '.page-last', function () {
                    var pageInfo = $self.pageInfo;
                    if (pageInfo.pageIndex === pageInfo.totalPage - 1) {
                        return;
                    }

                    pageInfo.pageIndex = pageInfo.totalPage - 1;
                    updatePageInfo($pagerContainer, pageInfo.pageIndex + 1, pageInfo.totalPage);
                    togglePrevButton($pagerContainer, true);
                    toggleNextButton($pagerContainer, false);
                });

                $pagerContainer.on('change', '.pagesize-select', function () {
                    var pageInfo = $self.pageInfo;
                    pageInfo.pageIndex = 0;
                    pageInfo.pageSize = parseInt($(this).val());

                    var totalPage = parseInt(pageInfo.totalItems / pageInfo.pageSize) + (pageInfo.totalItems % pageInfo.pageSize === 0 ? 0 : 1);
                    pageInfo.totalPage = totalPage;
                    
                    updatePageInfo($pagerContainer, pageInfo.pageIndex + 1, pageInfo.totalPage);
                    togglePrevButton($pagerContainer, false);
                    toggleNextButton($pagerContainer, true);
                });
            });
        }
    }

    $.fn.pager = function (methodOrOptions) {
        if (_methods[methodOrOptions]) {
            return _methods[methodOrOptions].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof methodOrOptions === 'object' || !methodOrOptions) {
            // Default to "init"
            return _methods.init.apply(this, arguments);
        } else {
            $.error('Method ' + methodOrOptions + ' does not exist on jQuery.flatPopup');
        }
    };
})(jQuery)
var MediaGallery = (function ($) {
    var CATEGORY_CASINO = "casino";
    var CATEGORY_SPORTSBOOK = "sportsbook";

    var optionAll = "<option value=''>All</option>";

    var _me = {
        initialize: function () {
            _me.initEvent();
            $("#marketingPager").pager({
                totalItems: parseInt($("#TotalItem").val())
            });
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
            });

            $("#btnMarketingSearch").on("click", function () {
                var searchParams = _me.getSearchParam();
                _me.getBannerList(searchParams);

                var linkGenerationParams = _me.getLinkGenerationParam();
                _me.getTextLink(linkGenerationParams);
            });

            $("#dataContainerList").on("change", "#DeviceType", function () {
                var params = _me.getSearchParam();
                _me.getBannerList(params);
            });

            $("#btnGenerateLink").on("click", function () {
                var params = _me.getLinkGenerationParam();
                _me.getTextLink(params);
            });
        },

        getSubcategory: function (category) {
            $.ajax({
                type: "GET",
                dataType: "json",
                cache: false,
                data: { category: category },
                url: Site.resolveRelativeUrl("UserPanel/GetSubCategory"),
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
                selectedPage: $("#MemberSitePage").val()
            }
        },

        getBannerList: function (params) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/GetBanner"),
                success: function (response) {
                    $("#marketingDataList").html(response);
                }
            });
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
                url: Site.resolveRelativeUrl("UserPanel/GenerateLink"),
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
        }
    };

    return _me;
})(jQuery);

MediaGallery.initialize();