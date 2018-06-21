var WebSite = (function ($) {
    var _me = {
        getMoreWebsiteUrlData: function () {
            var moreUrlList = [];
            $("#websiteInfo .urlInput").each(function (index, data) {
                if (data.value) {
                    moreUrlList.push(Site.escapeHtml(data.value.trim()));
                }
            });

            return moreUrlList.join(";");
        },

        reoderInputUrl: function (webSiteLabel) {
            var index = 2;
            $(".moreWebsiteLabel").each(function (i, item) {
                var $item = $(item);
                var labelText = webSiteLabel + ' ' + index;
                $item.text(labelText);

                index++;
            });
        },

        deleteWebsiteUrl: function ($currentItem) {
            var $container = $currentItem.closest(".websiteInput");
            $container.remove();
        },

        addMoreUrl: function (webSiteLabel, deleteLabel, errorMessage) {
            var currentItemCount = $("#websiteInfo .urlInput").length + 2;
            var inputItem = '<div class="form-group websiteInput">' +
                    '<label class="col-sm-3 control-label require moreWebsiteLabel">' + webSiteLabel + ' ' + currentItemCount + '</label>' +
                    '<div class="col-sm-4">' +
                        '<input class="form-control urlInput" type="text" value="" />' +
                    '</div>' +
                    '<div class="col-sm-1">' +
                        '<button type="button" class="btn btn-submit btnDeleteUrl">' + deleteLabel + '</button>' +
                    '</div>' +
                    '<div class="col-sm-offset-3 col-sm-9"><span class="help-block hide">' + errorMessage + '</span></div>' +
                '</div>';

            $("#websiteInfo").append(inputItem);
            _me.reoderInputUrl(webSiteLabel);
        },

        canAddMoreUrl: function () {
            var $inputList = $("#websiteInfo .urlInput");
            for (var i = 0; i < $inputList.length; i++) {
                if ($inputList[i].value.trim() === "") {
                    return false;
                }
            }

            return true;
        }
    }

    return _me;
})(jQuery);
var MyAccount = (function ($) {
    function setFocusForLastInput() {
        $("#websiteInfo .urlInput").last().focus();
    }

    function hasPlayerUserNameInput() {
        return $("#txtPlayerUserName").length > 0;
    }

    var _me = {
        initialize: function () {
            _me.initEvent();
        },
        initEvent: function () {
            $("#btnAddUrl").on("click", function () {
                _me.clickAddUrl();
            });

            $("#websiteInfo").on("click", ".btnDeleteUrl", function () {
                var $this = $(this);
                WebSite.deleteWebsiteUrl($this);
                WebSite.reoderInputUrl(LangResource.websiteUrl);
            });

            $("#accountInfoForm").on("change", "input[name=HasWebSite][type=radio]", function () {
                var hasWebSite = this.value === "1";
                _me.changeWebsiteOption(hasWebSite);
            });

            $("#btnMyAccountSubmit").on("click", function () {
                var isValid = _me.checkRequiredInput();
                console.log("submit", true);

                if (isValid) {
                    var params = _me.getParams();

                    $.ajax({
                        type: "POST",
                        url: Site.resolveRelativeUrl("UserPanel/MyAccount"),
                        data: params,
                        dataType: "json",
                        cache: false,
                        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                        success: function (response) {
                            var message = response.IsOk ? LangResource.updateSuccessfully : response.Message;
                            window.alert(message);

                            if (response.IsOk) {
                                Site.reload();
                            }
                        },
                        beforeSend: function () {
                            Site.showLoader();
                        },
                        complete: function () {
                            Site.hideLoader();
                        }
                    });
                }
            });

            $("#btnCancel").on("click", function () {
                Site.redirect(Site.resolveRelativeUrl("UserPanel/#AccountOverView"));
            });
        },

        clickAddUrl: function () {
            var hasWebSite = _me.hasWebSite();
            _me.resetWebSiteGroup(hasWebSite);

            if (hasWebSite) {
                var $mainUrlInput = $("#txtSiteUrl");
                var mainUrlHasValue = !!$mainUrlInput.val();

                var $container = $mainUrlInput.closest(".form-group");
                _me.toggleErrorMessage($container, mainUrlHasValue);

                if (mainUrlHasValue) {
                    _me.addMoreUrl();
                }
                else {
                    $mainUrlInput.focus();
                }
            }
        },

        addMoreUrl: function () {
            var canAddMoreWebSite = WebSite.canAddMoreUrl();

            if (canAddMoreWebSite) {
                var currentItemCount = $("#websiteInfo .urlInput").length + 2;
                var errorMessage = LangResource.requiredMoreWebSiteUrl.replace("{i}", currentItemCount);
                WebSite.addMoreUrl(LangResource.websiteUrl, "Delete", errorMessage);
                setFocusForLastInput();
            }
        },

        changeWebsiteOption: function (hasWebSite) {
            var $mainUrlInput = $("#txtSiteUrl");
            $mainUrlInput.prop("disabled", !hasWebSite);
            $(".websiteInput").toggle(hasWebSite);
            $("#btnAddUrl").prop("disabled", !hasWebSite);

            var $noSiteDesc = $("#txtNoSiteDesc");
            $noSiteDesc.prop("disabled", hasWebSite);
        },

        checkRequiredInput: function () {
            var hasWebSite = _me.hasWebSite();
            _me.resetWebSiteGroup(hasWebSite);

            var selector = hasWebSite ? "input[type=text]" : "input[type=text]:not('#websiteInfo input'), textarea";
            var requiredInputList = $(selector, $("#accountInfoForm"));
            var isValid = true;

            for (var i = 0; i < requiredInputList.length; i++) {
                var element = requiredInputList[i];

                var $element = $(element);
                var hasValue = !!$element.val();

                var $container = $element.closest(".form-group");
                _me.toggleErrorMessage($container, hasValue);

                if (isValid && !hasValue) {
                    isValid = false;
                    $element.focus();
                }
            }

            return isValid;
        },

        resetWebSiteGroup: function (hasWebSite) {
            var $excludeWebSiteInfo = hasWebSite ? $("#txtNoSiteDesc") : $("#txtSiteUrl");
            var $container = $excludeWebSiteInfo.closest(".form-group");
            _me.toggleErrorMessage($container, true);

            var inputList = $("#websiteInfo .urlInput");
            for (var i = 0; i < inputList.length; i++) {
                var $urlInput = $(inputList[i]);
                var hasValue = !!$urlInput.val().trim();
                var $container = $urlInput.closest(".form-group");
                _me.toggleErrorMessage($container, hasValue);
            }
        },

        getParams: function () {
            var params = {
                phone: Site.escapeHtml($("#txtContactNumber").val().trim()),
                mobilePhone: Site.escapeHtml($("#txtMobile").val().trim())
            }

            if (_me.hasWebSite()) {
                params.siteUrl = Site.escapeHtml($("#txtSiteUrl").val().trim());
                params.moreSiteUrls = WebSite.getMoreWebsiteUrlData();
            }
            else {
                var descContent = $("#txtNoSiteDesc").val();
                params.noSiteDesc = descContent ? Site.escapeHtml(descContent.trim()) : ""
            }

            if (hasPlayerUserNameInput()) {
                params.playerUsername = $("#txtPlayerUserName").val();
            }

            return params;
        },

        toggleErrorMessage: function ($container, isValid) {
            $container.toggleClass("has-error", !isValid);
            $container.find(".help-block").toggleClass("hide", isValid);
        },

        hasWebSite: function () {
            return $("#hasWebSite").prop("checked");
        }
    };

    return _me;
})(jQuery)

MyAccount.initialize();