var SignUp = (function ($) {
    function setFocusForLastInput() {
        $("#websiteInfo .urlInput").last().focus();
    }

    var _me = {
        initialize: function () {
            _me.initEvent();

            console.log("Validate");
        },

        initEvent: function () {
            $("#signUpForm").on("submit", function (e) {
                e.preventDefault();
                return _me.submit();
            });

            $("#btnReset").on("click", function () {
                _me.reset();
            });

            $("#chkAgree").on("click", function () {
                $("#btnSubmit").prop("disabled", !this.checked);
            });

            $("#btnAddUrl").on("click", function () {
                _me.handleAddingUrl();
            });

            $("#websiteInfo").on("click", ".btnDeleteUrl", function () {
                var $this = $(this);
                WebSite.deleteWebsiteUrl($this);
                WebSite.reoderInputUrl(resourceLanguage.webSiteUrl);
            });

            $("#signUpForm").on("change", "input[name=HasWebSite][type=radio]", function () {
                var hasWebSite = this.value === "True";
                _me.changeWebsiteOption(hasWebSite);
            });
        },

        submit: function () {
            var agree = $("#chkAgree").prop("checked");
            var validator = _me.getValidator();

            var isValidForm = agree && validator.form();
            if (isValidForm) {
                Site.showLoader();
                _me.assignValueForWebSite();
                var postData = $("#signUpForm").serializeObject();

                $.ajax({
                    type: "POST",
                    url: Site.resolveRelativeUrl("Signup/Index"),
                    data: postData,
                    dataType: "json",
                    cache: false,
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    success: function (response) {
                        _me.handleResponseResult(response);
                    },
                    complete: function () {
                        Site.hideLoader();
                    }
                });
            }

            return false;
        },

        assignValueForWebSite: function () {
            var $siteUrl = $("#SiteUrl");
            var $moreSiteUrl = $("#MoreSiteUrls");
            var $noSiteDesc = $("#NoSiteDesc");

            if (_me.hasWebSite()) {
                var moreWebSiteUrl = WebSite.getMoreWebsiteUrlData();
                $moreSiteUrl.val(moreWebSiteUrl);

                $noSiteDesc.val("");
            }
            else {
                $siteUrl.val("");
                $moreSiteUrl.val("");
            }
        },

        handleResponseResult: function (response) {
            Site.refreshCaptcha();
            $("#messageSuccess").toggle(response.IsOk);
            $("#messageError").toggle(!response.IsOk).html(response.Message || resourceLanguage.generalError);
            if (response.IsOk) {
                $("#mainContent").remove();
            }
            $(window).scrollTop(0);
        },

        handleAddingUrl: function () {
            var hasWebSite = _me.hasWebSite();
            var isValid = _me.validateWebSiteGroup();

            if (hasWebSite && isValid) {
                _me.addMoreUrl();
            }
        },

        addMoreUrl: function () {
            var currentItemCount = $("#websiteInfo .urlInput").length + 2;
            var errorMessage = resourceLanguage.requiredMoreWebSiteUrl.replace("{i}", currentItemCount);
            WebSite.addMoreUrl(resourceLanguage.webSiteUrl, resourceLanguage.deleteLabel, errorMessage);
            setFocusForLastInput();

            _me.refreshFormValidation();
        },

        refreshFormValidation: function () {
            var $form = $("#signUpForm");
            $form.removeData("validator").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse($form);
        },

        reset: function () {
            var inputList = $("input, select, textarea", $("#signUpForm"));
            for (var i = 0; i < inputList.length; i++) {
                var element = inputList[i];
                if (element.type !== "radio") {
                    $(element).val("");
                }
            }

            var validator = _me.getValidator();
            validator.resetForm();

            $("#male").prop("checked", true);
            _me.resetWebSiteGroup();
        },

        resetWebSiteGroup: function () {
            $("#hasWebSite").prop("checked", true);
            $("#NoSiteDesc").prop("disabled", true);
            $("#SiteUrl").prop("disabled", false);
            $(".websiteInput").remove();
        },

        changeWebsiteOption: function (hasWebSite) {
            _me.validateWebSiteGroup();
            var $mainUrlInput = $("#SiteUrl");
            $mainUrlInput.prop("disabled", !hasWebSite);
            $(".websiteInput").toggle(hasWebSite);
            $("#btnAddUrl").prop("disabled", !hasWebSite);

            var $noSiteDesc = $("#NoSiteDesc");
            $noSiteDesc.prop("disabled", hasWebSite);
        },

        validateWebSiteGroup: function () {
            var validator = _me.getValidator();
            var isValidMainInfo = validator.element("#SiteUrl") && validator.element("#NoSiteDesc");

            var isValidMoreUrl = true;
            var moreInputUrl = $(".urlInput");
            for (var i = 0; i < moreInputUrl.length; i++) {
                var item = moreInputUrl[i];
                isValidMoreUrl = isValidMoreUrl && validator.element(item);
            }

            return isValidMainInfo && isValidMoreUrl;
        },

        getValidator: function () {
            return $("#signUpForm").validate();
        },

        hasWebSite: function () {
            return $("input[name=HasWebSite][type=radio]:checked").val() === "True";
        }
    };

    return _me;
})(jQuery);

SignUp.initialize();