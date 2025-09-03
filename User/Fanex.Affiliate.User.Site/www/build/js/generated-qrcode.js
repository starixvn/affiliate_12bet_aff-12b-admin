var MarketingBase = (function ($) {
    isCancelConfirm = false;
    let regexChannelNamePartten = /^[a-zA-Z0-9]+$/g;
    let regexChannelName = new RegExp(regexChannelNamePartten);

    const NO_INFO_OPTION = "<option value=''>" + langResource.noInfo + "</option>";

    function getDownloadParam(trigger) {
        return {
            selectedLanguage: $(trigger).data("selected-language"),
            selectedDomainId: $(trigger).data("selected-domain-id"),
            selectedPage: $(trigger).data("selected-page"),
            selectedChannel: $(trigger).data("selected-channel"),
            selectedLinkType: $(trigger).data("selected-link-type"),
            selectedTargetCountry: $(trigger).data("selected-target-country")
        };
    }

    function getToggleBtnStatus(cheked) {
        return cheked ? "on" : "off";
    }

    function handleFalseResponse(response) {
        let data;
        switch (response.status) {
            case 403:
                data = response.json();
                alert(data.ErrMsg);
                location.reload();
                break;
            default:
                alert("Internal Error");
                break;
        }
    }

    var _me = {

        initEvent: function () {
            $("#TargetCountry").off("change").change(function () {
                $("#btnMarketingSubmit").prop("disabled", false);
                _me.handleChangeFilter();
            });

            $("#AffLinkType").off("change").change(function () {
                $("#btnMarketingSubmit").prop("disabled", false);
                _me.handleChangeFilter(true);
            });

            $("#Domain").off("change").change(function () {
                $("#btnMarketingSubmit").prop("disabled", false);
                _me.handleChangeLinkTo();
            });
        },

        resiterQrcodeEvent: function () {
            new ClipboardJS('.btn-copy-link', {
                text: function (trigger) {
                    let parent = $(trigger).parents(".qr-item").first();
                    return parent.data("link");
                }
            });

            new ClipboardJS('.btn-copy-qr', {
                text: function (trigger) {
                    let imgTag = $(trigger).parents(".qr-action").prev().children("img.qrcode")[0];
                    return imgTag.outerHTML;
                }
            });

            $('.btn-save-img').click(function () {
                let downloadTag = document.createElement("a");
                let param = getDownloadParam($(this).parents(".qr-item")[0]);
                var query = $.param(param);
                let url = `${Site.resolveRelativeLangUrl("UserPanel/Marketing/DownloadQrCode")}?${query}`;
                downloadTag.href = url;
                downloadTag.click();
            });
        },

        initChannelSelectEvent: function () {
            if ($("#Channel").children().length > 1) {
                document.getElementById("Channel").selectedIndex = 1;
            }

            $("#Channel").focusin(function () {
                window.previousChannelIndex = this.selectedIndex;
                this.selectedIndex = -1;
            })

            $("#Channel").focusout(function () {
                if (this.selectedIndex <= 0) {
                    this.selectedIndex = window.previousChannelIndex === 0 ? 1 : window.previousChannelIndex;
                }
            })

            $("#Channel").change(function () {
                if ($(this).val() === 'add-new') {
                    let text = prompt(window.dic.enterChannel);
                    if (text) {
                        let textTrim = text.trim();
                        if (textTrim.length > 20) {
                            alert(window.dic.nameToLong20);
                            return;
                        }

                        if (regexChannelName.test(textTrim)) {
                            let addPromise = MarketingBase.addChannel(textTrim);
                            addPromise.then(result => {
                                if (result) {
                                    if (result.IsOk) {
                                        alert(window.dic.addChannelSuccess)
                                        $(this).append(new Option(result.Data.ChannelName, result.Data.ChannelCode, false, true));
                                        $("button.btn-gen").removeAttr("disabled");
                                    }
                                    else {
                                        alert(result.Message);
                                    }
                                }
                            })
                        } else {
                            alert(window.dic.invalidChannelName);
                        }
                    }
                }
                $(this).blur();
            })
        },
        addChannel: function (text) {
            Site.showLoader();
            let url = Site.resolveRelativeLangUrl("UserPanel/Marketing/AddChannel");
            let param = { channelName: text };
            let fetchData = fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8',
                    'X-Requested-With': 'XMLHttpRequest'
                },
                credentials: 'same-origin',
                body: new URLSearchParams(param)
            })
                .then((response => {
                    Site.hideLoader();
                    data = response.json();
                    if (response.ok) {
                        return data;
                    }
                    else {
                        handleFalseResponse(response);
                    }
                }))
            return Promise.resolve(fetchData);
        },

        renderLinkChannelStatisticPage: function () {
            let url = Site.resolveRelativeLangUrl("UserPanel/Marketing/RenderUsingMarketLinkPage");
            return fetch(url, {
                method: 'GET',
                credentials: 'same-origin',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8',
                    'X-Requested-With': 'XMLHttpRequest'
                },
            })
        },

        renderQrCodePage: function (param) {
            let url = new URL(Site.resolveRelativeLangUrl("UserPanel/Marketing/RenderQrCodePage"), window.location.origin);
            let query = new URLSearchParams(param);
            url.search = query.toString();
            return fetch(url, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8',
                    'X-Requested-With': 'XMLHttpRequest'
                },
                credentials: 'same-origin',
            })
        },

        toggleMarketingLink: function (param) {
            let url = Site.resolveRelativeLangUrl("UserPanel/Marketing/ToggleMarketingLink");
            return fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8',
                    'X-Requested-With': 'XMLHttpRequest'
                },
                credentials: 'same-origin',
                body: new URLSearchParams(param)
            })
        },

        registerToggleLinkEvent: function (callback) {
            $checkBox = $(".toggle-link");
            $checkBox.off("change");

            $checkBox.on("change", function () {
                if (isCancelConfirm) {
                    isCancelConfirm = false;
                    return;
                }
                let checked = $(this).prop('checked');
                let confirmText = checked
                    ? window.dic.confirmActiveLink
                    : window.dic.confirmDeactiveLink

                if (confirm(confirmText)) {
                    let parent = $(this).parents(".qr-item").first();
                    let param = {
                        ChannelCode: parent.data("selected-channel"),
                        LanguageCode: parent.data("selected-language"),
                        AffRefLink: parent.data("link"),
                        IsOn: checked
                    }

                    _me.toggleMarketingLink(param)
                        .then(response => {
                            if (response.ok) {
                                $(parent).find("button").prop('disabled', !checked);
                                $(parent).find(".qr-img").toggleClass("disabled");
                                callback && callback();
                            }
                            else {
                                handleFalseResponse(response);
                                isCancelConfirm = true;
                                $(this).bootstrapToggle(getToggleBtnStatus(!checked));
                            }
                        })


                }
                else {
                    isCancelConfirm = true;
                    $(this).bootstrapToggle(getToggleBtnStatus(!checked));
                }

            })
        },

        handleChangeLinkTo: function () {
            let param = {
                selectedDomainId: $("#Domain").val(),
                targetCountryCode: $('#TargetCountry').val(),
                linkType: $("#AffLinkType").val(),
            };

            $.ajax({
                type: "GET",
                dataType: "json",
                cache: false,
                data: param,
                url: Site.resolveRelativeLangUrl("UserPanel/Marketing/GetLinkTo"),
                success: function (response) {
                    if (response && response.length == 0) {
                        $('#btnMarketingSubmit').prop('disabled', true);
                        $("#Page").html(NO_INFO_OPTION);
                    } else {
                        _me.updateLinkToDropdown(response);
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

        handleChangeFilter: function (isChangeAffTypeLink = false) {
            let isMediaGallery = !!$("#Size").length;

            let param = {
                category: $("#Category").val(),
                subCategory: $("#SubCategory").val(),
                currency: $("#Currency").val(),
                imageType: $("#ImageType").val(),
                deviceType: $("#DeviceType").val(),
                targetId: $("#AffLinkType").val(),
                targetCountryCode: $('#TargetCountry').val(),
                domainId: $("#Domain").val(),
                isMediaGallery,
                isChangeAffTypeLink
            };

            $.ajax({
                type: 'GET',
                dataType: 'json',
                cache: false,
                data: param,
                url: Site.resolveRelativeLangUrl('UserPanel/Marketing/GetChangedFilters'),
                success: function (response) {
                    if (response && response.length == 0) {
                        $('#btnMarketingSubmit').prop('disabled', true);

                        $('#Domain').html(NO_INFO_OPTION);
                        isMediaGallery && $('#Size').html(NO_INFO_OPTION);
                        $("#Page").html(NO_INFO_OPTION);
                        !isChangeAffTypeLink && $('#Language').html(NO_INFO_OPTION);
                        (isChangeAffTypeLink && isMediaGallery) && $('#Category').html(NO_INFO_OPTION);
                    } else {
                        var { domainList, languageList, pageList, sizeList, categoryList } = response;

                        _me.updateAvailableDomainDropdown(domainList);
                        isMediaGallery && _me.updateSizeDropdown(sizeList);
                        _me.updateLinkToDropdown(pageList);
                        !isChangeAffTypeLink && _me.updateLanguageDropdown(languageList);
                        (isChangeAffTypeLink && isMediaGallery) && _me.updateCategoryDropdown(categoryList);

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
                $('#btnMarketingSubmit').prop('disabled', true);
                $('#Domain').html(NO_INFO_OPTION);

                return;
            }

            var html = Site.buildOptionList(availableDomainList, 'Value', 'Text', false);

            var previousSelectedDomain = $('#Domain').val();

            $('#Domain').html(html);

            var hasPreviousSelectedDomain = availableDomainList.some(i => i.Value == previousSelectedDomain);

            if (hasPreviousSelectedDomain) {
                $('#Domain').val(previousSelectedDomain);
            }
        },

        updateLinkToDropdown: function (pageList) {
            if (pageList && !pageList.length) {
                $("#btnMarketingSubmit").prop("disabled", true);
                $("#Page").html(NO_INFO_OPTION);

                return;
            }

            var html = Site.buildOptionList(pageList, "Value", "Text", false);

            var previousSelectedPage = $("#Page").val();

            $("#Page").html(html);

            var hasPreviousSelectedPage = pageList.some(i => i.Value == previousSelectedPage);

            if (hasPreviousSelectedPage) {
                $("#Page").val(previousSelectedPage);
            }
        },

        updateLanguageDropdown: function (languageList) {
            if (languageList && !languageList.length) {
                $('#Language').html(NO_INFO_OPTION);

                return;
            }

            const html = Site.buildOptionList(languageList, 'Value', 'Text', languageList.length > 1);

            $('#Language').html(html);
            $('#Language').val(languageList[0].Value);
        },

        updateSizeDropdown: function (sizeList) {
            if (sizeList && !sizeList.length) {
                $('#Size').html(NO_INFO_OPTION);

                return;
            }

            const html = Site.buildOptionList(sizeList, 'Value', 'Text', true);
            const previousSelectedSize = $('#Size').val();

            $('#Size').html(html);

            const hasPreviousSelectedSize = sizeList.some(i => i.Value === previousSelectedSize);

            if (hasPreviousSelectedSize) {
                $('#Size').val(previousSelectedSize);
            }
        },

        updateCategoryDropdown: function (categoryList) {
            if (categoryList && !categoryList.length) {
                $('#Category').html(NO_INFO_OPTION);

                return;
            }

            var html = Site.buildOptionList(categoryList, "Value", "Text", true);

            $("#Category").html(html);
            $("#Category").val('');

            var $subCategoryContainer = $("#SubCategory").closest(".form-group");
            var html = Site.buildOptionList([], "Value", "Text", true);
            $("#SubCategory").html(html);
            $subCategoryContainer.addClass("hide");
        },
    };

    return _me;
})(jQuery);