var Feedback = (function ($) {
    const affFeedbackRemoveHeight = 10;
    const none = 0;
    const rateList = {
        "dissatisfaction": 1,
        "unsatisfied": 2,
        "neutral": 3,
        "satisfied": 4,
        "veryPlease": 5
    };

    let _me = {
        initialize: function () {
            if ($("#affFeedback").length) {
                _me.initEvent();
                _me.handleShowFeedback();
            }
        },
        initEvent: function () {
            $('#affFeedbackRemove').on('click', function (event) {
                _me.handleSubmitFeedback(none);

                _me.handleHideFeedback();
            })

            $('.feedback-checkbox').on('click', function (event) {
                let isChecked = $(this).prop('checked');

                if (isChecked) {
                    $(".feedback-checkbox").prop('checked', false);
                    $(this).prop('checked', isChecked);
                }

                _me.handleDisabledSubmitBtn();
            })

            $('#submitFeedbackBtn').on('click', function (event) {
                //$(".aff-feedback-remove").remove();
                //$("#affFeedbackContent").html(`<p class="title-text">${window.feedback.thankYou}</p>`);
                //$("#affFeedbackContainer").css({
                //    top: `30%`
                //});

                //window.setTimeout(_me.handleHideFeedback, 2000);

                if ($('input[class="feedback-checkbox"]:checked').length) {
                    let rateName = $('input[class="feedback-checkbox"]:checked').val();
                    let rate = rateList[rateName] || none;

                    _me.handleSubmitFeedback(rate);
                }
            })

            $('#feedbackMessage').on('keyup', function (event) {
                _me.handleDisabledSubmitBtn();
            })
        },

        handleShowFeedback: function () {
            $(document.body).css('overflow', 'hidden');
            $("#submitFeedbackBtn").prop('disabled', true);
            _me.adjustAffFeedbackRemovePosition;
            window.addEventListener("resize", _me.adjustAffFeedbackRemovePosition);
        },

        handleDisabledSubmitBtn: function () {
            $("#submitFeedbackBtn").prop('disabled', !($('input[class="feedback-checkbox"]:checked').length || $('#feedbackMessage').val().trim()));
        },

        handleSubmitFeedback: function (rate) {
            if (_me.isValidData) {
                $.ajax({
                    url: Site.resolveRelativeUrl("UserPanel/Feedback/Insert"),
                    type: "POST",
                    dataType: "json",
                    cache: false,
                    success: function (result) {
                        $("#loginAfter").append(`<div id="affFeedback">${result}</div>`);
                    },
                    beforeSend: function () {
                        Site.showLoader();
                    },
                    complete: function (jqXHR, textStatus) {
                        Site.hideLoader();
                        if (textStatus === 'error') {

                        }
                    }
                });
            }
        },

        handleHideFeedback: function () {
            $("#affFeedback").remove();
            $(document.body).css('overflow', 'auto');
            window.removeEventListener("resize", _me.adjustAffFeedbackRemovePosition);

            if (window.feedback.isClickLogout) {
                window.location.href = Site.resolveRelativeUrl("Auth/SignOut");
            }
        },

        validateData: function () {
            return ($('input[class="feedback-checkbox"]:checked').length || $('#feedbackMessage').val().trim());
        },

        adjustAffFeedbackRemovePosition: function () {
            let affFeedbackContainerHeight = $('#affFeedbackContainer').outerHeight(true);
            let affFeedbackContainerWidth = $('#affFeedbackContainer').outerWidth(true);

            $('#affFeedbackContainer').css({
                top: `calc(50% - ${affFeedbackContainerHeight / 2}px)`,
                right: `calc(50% - ${affFeedbackContainerWidth / 2}px)`
            });

            let affFeedbackContainerPosition = $('#affFeedbackContainer').position();
            let affFeedbackContainerTop = affFeedbackContainerPosition.top;
            let affFeedbackContainerLeft = affFeedbackContainerPosition.left;

            $('#affFeedbackRemove').css({
                top: (affFeedbackContainerTop + affFeedbackRemoveHeight) + 'px',
                right: `calc(100% - ${affFeedbackContainerLeft + affFeedbackContainerWidth - affFeedbackRemoveHeight}px)`
            });
        }
    };

    return _me;
})(jQuery);

Feedback.initialize();