"use strict";

var MyAccount = (function ($) {
    function setFocusForLastInput() {
        $("#websiteInfo .urlInput").last().focus();
    }

    var _me = {
        initialize: function initialize() {
            _me.initEvent();
        },
        initEvent: function initEvent() {
            $("#btnAddUrl").on("click", function () {
                _me.addUrl();
            });

            $("#websiteInfo").on("click", ".btnDeleteUrl", function () {
                var $this = $(this);
                WebSite.deleteWebsiteUrl($this);
                WebSite.reoderInputUrl(resourceLanguage.webSiteUrl);
            });

            $("#accountForm").on("change", "input[name=HasWebSite][type=radio]", function () {
                var hasWebSite = this.value === "True";
                WebSite.changeWebsiteOption(hasWebSite);
            });

            $("#btnMyAccountSubmit").on("click", function () {
                _me.submit();
            });

            $("#btnCancel").on("click", function () {
                Site.redirect(Site.resolveRelativeUrl("UserPanel/Main/#AccountOverView"));
            });
        },

        submit: function submit() {
            var $form = $("#accountForm");
            var validator = $form.validate();
            var isValidForm = validator.form();
            if (isValidForm) {
                Site.showLoader();
                WebSite.assignValueForWebSite();
                var postData = $form.serializeObject();

                $.ajax({
                    type: "POST",
                    url: Site.resolveRelativeUrl("UserPanel/MyAccount"),
                    data: postData,
                    dataType: "json",
                    cache: false,
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                    success: function success(response) {
                        _me.handleResponseResult(response);
                    },
                    complete: function complete() {
                        Site.hideLoader();
                    }
                });
            } else {
                validator.focusInvalid();
            }

            return false;
        },

        handleResponseResult: function handleResponseResult(response) {
            var message = response.IsOk ? resourceLanguage.updateSuccessfully : response.Message;
            Site.displayResult(response.IsOk, message);
            $(window).scrollTop(0);
        },

        addUrl: function addUrl() {
            var lang = {
                requiredMoreWebSiteUrl: resourceLanguage.requiredMoreWebSiteUrl,
                webSiteUrl: resourceLanguage.webSiteUrl,
                deleteLabel: resourceLanguage.deleteLabel
            };

            WebSite.handleAddingUrl(lang);
        }
    };

    return _me;
})(jQuery);

MyAccount.initialize();

