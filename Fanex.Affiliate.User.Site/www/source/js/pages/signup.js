var SignUp = (function ($) {
    var _me = {
        initialize: function () {
            _me.initEvent();
        },

        initEvent: function () {
            $("#signUpForm").on("submit", function (e) {
                return _me.submit();
            });

            $("#btnReset").on("click", function () {
                _me.reset();
            });

            $("#chkAgree").on("click", function () {
                $("#btnSubmit").prop("disabled", !this.checked);
            });

            $("#btnAddUrl").on("click", function () {
                _me.clickAddUrl();
            });

            $("#websiteInfo").on("click", ".btnDeleteUrl", function () {
                var $this = $(this);
                WebSite.deleteWebsiteUrl($this);
                WebSite.reoderInputUrl(resourceLanguage.webSiteUrl);
            });

            $("#signUpForm").on("change", "input[name=HasWebSite][type=radio]", function () {
                var hasWebSite = this.value === "1";
                _me.changeWebsiteOption(hasWebSite);
            });
        },

        submit: function(){
            var agree = $("#chkAgree").prop("checked");

            if (agree) {
                var isValid = _me.checkRequiredInput() && _me.checkValidEmail() && _me.checkMatchEmail();
                if (isValid) {
                    Site.showLoader();
                    _me.assignValueForWebSite();
                }

                return isValid;
            }

            return agree;
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

        clickAddUrl: function () {
            if (_me.hasWebSite()) {
                var $mainUrlInput = $("#SiteUrl");
                var mainUrlHasValue = !!$mainUrlInput.val();

                var $container = $mainUrlInput.closest(".form-group");
                _me.toggleErrorMessage($container, mainUrlHasValue);

                if (mainUrlHasValue) {
                    _me.addMoreUrl();
                }
            }
        },

        reset: function () {
            var inputList = $("input, select, textarea", $("#signUpForm"));

            for (var i = 0; i < requiredInputList.length; i++) {
                var element = requiredInputList[i];

                var $element = $(element);
                if (element.type !== "radio") {
                    $element.val("");
                }

                var $container = $element.closest(".form-group");
                _me.toggleErrorMessage($container, true);
            }

            $("#male").prop("checked", true);
        },

        changeWebsiteOption: function (hasWebSite) {
            var $mainUrlInput = $("#SiteUrl");
            $mainUrlInput.prop("disabled", !hasWebSite);
            $(".websiteInput").toggle(hasWebSite);
            $("#btnAddUrl").prop("disabled", !hasWebSite);

            var $noSiteDesc = $("#NoSiteDesc");
            $noSiteDesc.prop("disabled", hasWebSite);
        },

        checkRequiredInput: function () {
            var hasWebSite = _me.hasWebSite();
            _me.resetWebSiteGroup(hasWebSite);

            var selector = hasWebSite ? "input, select" : "input:not('#SiteUrl'), select, textarea";
            var requiredInputList = $(selector, $("#signUpForm"));
            var isValid = true;

            for (var i = 0; i < requiredInputList.length; i++) {
                var element = requiredInputList[i];

                if (element.type !== "radio" && element.type !== "hidden") {
                    var $element = $(element);
                    var hasValue = !!$element.val();

                    var $container = $element.closest(".form-group");
                    if ($element.hasClass("emailInput")) {
                        $container.find(".help-block").html(resourceLanguage.emailRequired);
                    }

                    _me.toggleErrorMessage($container, hasValue);

                    if (isValid && !hasValue) {
                        isValid = false;
                    }
                }
            }

            return isValid;
        },

        hasWebSite: function () {
            return $("input[name=HasWebSite][type=radio]:checked").val() === "1";
        },

        resetWebSiteGroup: function (hasWebSite) {
            var $excludeWebSiteInfo = hasWebSite ? $("#NoSiteDesc") : $("#SiteUrl");
            var $container = $excludeWebSiteInfo.closest(".form-group");
            _me.toggleErrorMessage($container, true);
        },

        addMoreUrl: function () {
            var canAddMoreWebSite = WebSite.canAddMoreUrl();

            var $container = $("#websiteInfo .urlInput").last().closest(".form-group");
            _me.toggleErrorMessage($container, canAddMoreWebSite);

            if (canAddMoreWebSite) {
                var currentItemCount = $("#websiteInfo .urlInput").length + 2;
                var errorMessage = resourceLanguage.requiredMoreWebSiteUrl.replace("{i}", currentItemCount);
                WebSite.addMoreUrl(resourceLanguage.webSiteUrl, "Delete", errorMessage);
            }
        },

        toggleErrorMessage: function ($container, isValid) {
            $container.toggleClass("has-error", !isValid);
            $container.find(".help-block").toggleClass("hide", isValid);
        },

        checkValidEmail: function () {
            var isValid = true;
            var pattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

            var emailInput = $(".emailInput");
            for (var i = 0; i < emailInput.length; i++) {
                var element = emailInput[i];
                var match = pattern.test(element.value);

                var $container = $(element).closest(".form-group");
                if (!match) {
                    $container.find(".help-block").html(resourceLanguage.emailInvalid);

                    if (isValid) {
                        isValid = false;
                    }
                }

                _me.toggleErrorMessage($container, match);
            }

            return isValid;
        },

        checkMatchEmail: function () {
            var $verifyEmail = $("#VerifyEmail");
            var isMatch = $verifyEmail.val() === $("#Email").val();
            var $container = $verifyEmail.closest(".form-group");
            $container.find(".help-block").html(resourceLanguage.emailNotMatch);
            _me.toggleErrorMessage($container, isMatch);

            return isMatch;
        }
    };

    return _me;
})(jQuery);

SignUp.initialize();