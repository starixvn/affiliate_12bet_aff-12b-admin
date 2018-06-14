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
                if (_me.canAddUrl()) {
                    _me.addMoreUrl();
                }
                setFocusForLastInput();
            });

            $("#websiteInfo").on("click", ".btnDeleteUrl", function () {
                var $this = $(this);
                _me.deleteWebsiteUrl($this);
            });

            $("#btnMyAccountSubmit").on("click", function () {
                var validationResult = _me.validateInputData();
                if (!validationResult.isValid) {
                    alert(validationResult.message);
                    return;
                }

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
            });

            $("#btnCancel").on("click", function () {
                Site.redirect(Site.resolveRelativeUrl("UserPanel/#AccountOverView"));
            });
        },
        addMoreUrl: function () {
            var currentItemCount = $("#websiteInfo .urlInput").length + 1;
            var inputItem = '<div class="form-group websiteInput">' +
                    '<label class="col-sm-3 control-label require moreWebsiteLabel">' + LangResource.websiteUrl + ' ' + currentItemCount + '</label>' +
                    '<div class="col-sm-4">' +
                        '<input class="form-control urlInput" type="text" value="" />' +
                    '</div>' +
                    '<div class="col-sm-1">' +
                        '<button type="button" class="btn btn-submit btnDeleteUrl">' + LangResource.deleteUrl + '</button>' +
                    '</div>' +
                '</div>';

            $("#websiteInfo").append(inputItem);
            _me.reoderInputUrl();
        },
        canAddUrl: function () {
            if ($("#txtSiteUrl").val().trim() === "") {
                $("#txtSiteUrl").focus();

                return false;
            }

            var $inputList = $("#websiteInfo .urlInput");
            for (var i = 0; i < $inputList.length; i++) {
                if ($inputList[i].value.trim() === "") {
                    return false;
                }
            }

            return true;
        },
        deleteWebsiteUrl: function ($currentItem) {
            var $container = $currentItem.closest(".websiteInput");
            $container.remove();

            _me.reoderInputUrl();
        },
        reoderInputUrl: function () {
            var index = 2;
            $(".moreWebsiteLabel").each(function (i, item) {
                var $item = $(item);
                var labelText = LangResource.websiteUrl + ' ' + index;
                $item.text(labelText);

                index++;
            });
        },
        validateInputData: function () {
            if ($("#txtContactNumber").val().trim() === "") {
                $("#txtContactNumber").focus();

                return {
                    isValid: false,
                    message: LangResource.requireContactNumber
                }
            }

            if ($("#txtMobile").val().trim() === "") {
                $("#txtMobile").focus();

                return {
                    isValid: false,
                    message: LangResource.requireMobile
                }
            }

            if ($("#hasWebSite").prop("checked") && $("#txtSiteUrl").val().trim() === "" || !_me.canAddUrl()) {
                $("#txtSiteUrl").focus();

                return {
                    isValid: false,
                    message: LangResource.requireSiteUrl
                }
            }

            if ($("#hasNoWebSite").prop("checked") && $("#txtNoSiteDesc").val().trim() === "") {
                $("#txtNoSiteDesc").focus();

                return {
                    isValid: false,
                    message: LangResource.requireNoWebsiteDesc
                }
            }

            if (hasPlayerUserNameInput() && $("#txtPlayerUserName").val().trim() === "") {
                $("#txtPlayerUserName").focus();

                return {
                    isValid: false,
                    message: LangResource.requirePlayerUserName
                }
            }

            return {
                isValid: true,
                message: ""
            }
        },

        getParams: function () {
            var params = {
                phone: Site.escapeHtml($("#txtContactNumber").val().trim()),
                mobilePhone: Site.escapeHtml($("#txtMobile").val().trim())
            }

            if ($("#hasWebSite").prop("checked")) {
                params.siteUrl = Site.escapeHtml($("#txtSiteUrl").val().trim());
                params.moreSiteUrls = _me.getMoreWebsiteUrlData();
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

        getMoreWebsiteUrlData: function () {
            var moreUrlList = [];
            $("#websiteInfo .urlInput").each(function (index, data) {
                if (data.value) {
                    moreUrlList.push(Site.escapeHtml(data.value.trim()));
                }
            });

            return moreUrlList.join(";");
        }
    };

    return _me;
})(jQuery)

MyAccount.initialize();