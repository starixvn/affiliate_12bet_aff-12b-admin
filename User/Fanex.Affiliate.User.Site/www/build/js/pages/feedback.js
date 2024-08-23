'use strict';

var Feedback = (function ($) {
    var affFeedbackRemoveHeight = 10;
    var none = 0;

    var _me = {
        initialize: function initialize() {
            if ($("#affFeedback").length) {
                _me.initEvent();
                _me.handleShowFeedback();
            }
        },
        initEvent: function initEvent() {
            $('#affFeedbackRemove').on('click', function (event) {
                _me.handleCloseFeedback();
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
                if (_me.isValidToSubmit()) {
                    var rate = $('input[class="feedback-checkbox"]:checked').val() || none;

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
            _me.adjustAffFeedbackRemovePosition();
            window.addEventListener("resize", _me.adjustAffFeedbackRemovePosition);
        },

        handleDisabledSubmitBtn: function handleDisabledSubmitBtn() {
            $("#submitFeedbackBtn").prop('disabled', !_me.isValidToSubmit());
        },

        isValidToSubmit: function isValidToSubmit() {
            return $('input[class="feedback-checkbox"]:checked').length || $('#feedbackMessage').val().trim();
        },

        handleCloseFeedback: function handleCloseFeedback() {
            var data = {
                IsClickLogout: window.feedback.isClickLogout
            };

            $.ajax({
                url: Site.resolveRelativeLangUrl("UserPanel/Feedback/CloseFeedback"),
                type: "POST",
                dataType: "json",
                cache: false,
                data: data,
                complete: function complete(jqXHR, textStatus) {
                    _me.handleHideFeedback();
                }
            });
        },

        handleSubmitFeedback: function handleSubmitFeedback(rate) {
            var data = {
                RateId: rate,
                IsClickLogout: window.feedback.isClickLogout,
                Message: $('#feedbackMessage').val().trim()
            };
            $.ajax({
                url: Site.resolveRelativeLangUrl("UserPanel/Feedback/Insert"),
                type: "POST",
                dataType: "json",
                cache: false,
                data: data,
                success: function success(response) {
                    if (response.IsOk) {
                        $(".aff-feedback-remove").remove();
                        $("#affFeedbackContent").html('<p class="title-text">' + window.feedback.thankYou + '</p>');
                        $("#affFeedbackContainer").css({
                            top: '30%'
                        });

                        window.setTimeout(_me.handleHideFeedback, 2000);
                    } else {
                        alert(response.Message);
                    }
                },
                beforeSend: function beforeSend() {
                    Site.showLoader();
                },
                complete: function complete(jqXHR, textStatus) {
                    Site.hideLoader();
                }
            });
        },

        handleHideFeedback: function handleHideFeedback() {
            $("#affFeedback").remove();
            $(document.body).css('overflow', 'auto');
            window.removeEventListener("resize", _me.adjustAffFeedbackRemovePosition);

            if (window.feedback.isClickLogout) {
                window.location.href = Site.resolveRelativeLangUrl("Auth/SignOut");
            }
        },

        validateData: function validateData() {
            return $('input[class="feedback-checkbox"]:checked').length || $('#feedbackMessage').val().trim();
        },

        adjustAffFeedbackRemovePosition: function adjustAffFeedbackRemovePosition() {
            var affFeedbackContainerHeight = $('#affFeedbackContainer').outerHeight();
            var affFeedbackContainerWidth = $('#affFeedbackContainer').outerWidth();

            $('#affFeedbackContainer').css({
                top: 'calc(50% - ' + affFeedbackContainerHeight / 2 + 'px)',
                right: 'calc(50% - ' + affFeedbackContainerWidth / 2 + 'px)'
            });

            var affFeedbackContainerPosition = $('#affFeedbackContainer').position();
            var affFeedbackContainerTop = affFeedbackContainerPosition.top;
            var affFeedbackContainerLeft = affFeedbackContainerPosition.left;

            $('#affFeedbackRemove').css({
                top: affFeedbackContainerTop + affFeedbackRemoveHeight + 'px',
                right: 'calc(100% - ' + (affFeedbackContainerLeft + affFeedbackContainerWidth - affFeedbackRemoveHeight) + 'px)'
            });
        }
    };

    return _me;
})(jQuery);

Feedback.initialize();

