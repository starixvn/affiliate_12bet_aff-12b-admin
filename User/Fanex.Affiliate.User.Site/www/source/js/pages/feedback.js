var Feedback = (function ($) {
    const affFeedbackRemoveHeight = 10;
    const none = 0;

    let _me = {
        initialize: function () {
            if ($("#affFeedback").length) {
                _me.initEvent();
                _me.handleShowFeedback();
            }
        },
        initEvent: function () {
            $('#affFeedbackRemove').on('click', function (event) {
                _me.handleCloseFeedback();
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
                if (_me.isValidToSubmit()) {
                    let rate = $('input[class="feedback-checkbox"]:checked').val() || none;

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
            _me.adjustAffFeedbackRemovePosition();
            window.addEventListener("resize", _me.adjustAffFeedbackRemovePosition);
        },

        handleDisabledSubmitBtn: function () {
            $("#submitFeedbackBtn").prop('disabled', !_me.isValidToSubmit());
        },

        isValidToSubmit: function () {
            return $('input[class="feedback-checkbox"]:checked').length || $('#feedbackMessage').val().trim();
        },

        handleCloseFeedback: function () {
            $.ajax({
                url: Site.resolveRelativeUrl("UserPanel/Feedback/CloseFeedback"),
                type: "POST",
                dataType: "json",
                cache: false,
                complete: function (jqXHR, textStatus) {
                    _me.handleHideFeedback();
                }
            });
        },

        handleSubmitFeedback: function (rate) {
            const data = {
                RateId: rate,
                Message: $('#feedbackMessage').val().trim()
            }
            $.ajax({
                url: Site.resolveRelativeUrl("UserPanel/Feedback/Insert"),
                type: "POST",
                dataType: "json",
                cache: false,
                data,
                success: function (response) {
                    if (response.IsOk) {
                        $(".aff-feedback-remove").remove();
                        $("#affFeedbackContent").html(`<p class="title-text">${window.feedback.thankYou}</p>`);
                        $("#affFeedbackContainer").css({
                            top: `30%`
                        });

                        window.setTimeout(_me.handleHideFeedback, 2000);
                    } else {
                        alert(response.Message);
                    }
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function (jqXHR, textStatus) {
                    Site.hideLoader();
                }
            });
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
            let affFeedbackContainerHeight = $('#affFeedbackContainer').outerHeight();
            let affFeedbackContainerWidth = $('#affFeedbackContainer').outerWidth();

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