"use strict";

var Feedback = (function ($) {
    var affFeedbackRemoveHeight = 10;
    var none = 0;
    var rateList = {
        "dissatisfaction": 1,
        "unsatisfied": 2,
        "neutral": 3,
        "satisfied": 4,
        "veryPlease": 5
    };

    var _me = {
        initialize: function initialize() {
            if ($("#affFeedback").length) {
                _me.initEvent();
                _me.handleShowFeedback();
            }
        },
        initEvent: function initEvent() {
            $('#affFeedbackRemove').on('click', function (event) {
                _me.handleSubmitFeedback(none);

                _me.handleHideFeedback();
            });

            $('.feedback-checkbox').on('click', function (event) {
                var isChecked = $(this).prop('checked');

                if (isChecked) {
                    $(".feedback-checkbox").prop('checked', false);
                    $(this).prop('checked', isChecked);
                }

                _me.handleDisabledSubmitBtn();
            });

            $('#submitFeedbackBtn').on('click', function (event) {
                //$(".aff-feedback-remove").remove();
                //$("#affFeedbackContent").html(`<p class="title-text">${window.feedback.thankYou}</p>`);
                //$("#affFeedbackContainer").css({
                //    top: `30%`
                //});

                //window.setTimeout(_me.handleHideFeedback, 2000);

                if ($('input[class="feedback-checkbox"]:checked').length) {
                    var rateName = $('input[class="feedback-checkbox"]:checked').val();
                    var rate = rateList[rateName] || none;

                    _me.handleSubmitFeedback(rate);
                }
            });

            $('#feedbackMessage').on('keyup', function (event) {
                _me.handleDisabledSubmitBtn();
            });
        },

        handleShowFeedback: function handleShowFeedback() {
            $(document.body).css('overflow', 'hidden');
            $("#submitFeedbackBtn").prop('disabled', true);
            _me.adjustAffFeedbackRemovePosition;
            window.addEventListener("resize", _me.adjustAffFeedbackRemovePosition);
        },

        handleDisabledSubmitBtn: function handleDisabledSubmitBtn() {
            $("#submitFeedbackBtn").prop('disabled', !($('input[class="feedback-checkbox"]:checked').length || $('#feedbackMessage').val().trim()));
        },

        handleSubmitFeedback: function handleSubmitFeedback(rate) {
            if (_me.isValidData) {
                $.ajax({
                    url: Site.resolveRelativeUrl("UserPanel/Feedback/Insert"),
                    type: "POST",
                    dataType: "json",
                    cache: false,
                    success: function success(result) {
                        $("#loginAfter").append("<div id=\"affFeedback\">" + result + "</div>");
                    },
                    beforeSend: function beforeSend() {
                        Site.showLoader();
                    },
                    complete: function complete(jqXHR, textStatus) {
                        Site.hideLoader();
                        if (textStatus === 'error') {}
                    }
                });
            }
        },

        handleHideFeedback: function handleHideFeedback() {
            $("#affFeedback").remove();
            $(document.body).css('overflow', 'auto');
            window.removeEventListener("resize", _me.adjustAffFeedbackRemovePosition);

            if (window.feedback.isClickLogout) {
                window.location.href = Site.resolveRelativeUrl("Auth/SignOut");
            }
        },

        validateData: function validateData() {
            return $('input[class="feedback-checkbox"]:checked').length || $('#feedbackMessage').val().trim();
        },

        adjustAffFeedbackRemovePosition: function adjustAffFeedbackRemovePosition() {
            var affFeedbackContainerHeight = $('#affFeedbackContainer').outerHeight(true);
            var affFeedbackContainerWidth = $('#affFeedbackContainer').outerWidth(true);

            $('#affFeedbackContainer').css({
                top: "calc(50% - " + affFeedbackContainerHeight / 2 + "px)",
                right: "calc(50% - " + affFeedbackContainerWidth / 2 + "px)"
            });

            var affFeedbackContainerPosition = $('#affFeedbackContainer').position();
            var affFeedbackContainerTop = affFeedbackContainerPosition.top;
            var affFeedbackContainerLeft = affFeedbackContainerPosition.left;

            $('#affFeedbackRemove').css({
                top: affFeedbackContainerTop + affFeedbackRemoveHeight + 'px',
                right: "calc(100% - " + (affFeedbackContainerLeft + affFeedbackContainerWidth - affFeedbackRemoveHeight) + "px)"
            });
        }
    };

    return _me;
})(jQuery);

Feedback.initialize();

