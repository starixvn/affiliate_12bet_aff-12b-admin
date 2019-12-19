var PlayersDetails = (function ($) {
    var _pageSize = 20;
    var _pageIndex = 0;

    function initPager() {
        $("#player-detail-pager").pager({
            totalItems: parseInt($("#TotalItem").val()),
            pageSize: _pageSize,
            pageIndex: _pageIndex,
            languageCode: Site.getCurrentLanguage(),
            onChange: function (pageIndex, pageSize) {
                _pageIndex = pageIndex;
                _pageSize = pageSize;

                var params = _me.getPlayersDetailsParam();
                _me.renderPlayersDetails(params);
            }
        });
    }

    var _me = {
        initialize: function () {
            initPager();
        },

        getPlayersDetailsParam: function () {
            return {
                monthDate: $("#MonthDate").data("original-date"),
                pageSize: _pageSize,
                pageIndex: _pageIndex + 1
            };
        },

        renderPlayersDetails: function (params) {
            $.ajax({
                type: "POST",
                dataType: "html",
                cache: false,
                data: JSON.stringify(params),
                contentType: "application/json; charset=UTF-8",
                url: Site.resolveRelativeUrl("UserPanel/AccountOverView/PlayersDetailsContent"),
                success: function (response) {
                    $("#player-detail-table").html(response);
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        }
    };

    return _me;
})(jQuery);

PlayersDetails.initialize();

function handleScroll() {
    if (window.innerWidth < supportMinWidth) {
        $("#details-section-menu").removeClass("details-section-sticky");
        $("#player-detail-pager").removeClass("details-section-sticky");
    } else {
        let stickyPoint = $("#details-section").offset().top;

        if (window.pageYOffset >= stickyPoint) {
            $("#details-section-menu").addClass("details-section-sticky");
            $("#player-detail-pager").addClass("details-section-sticky");
        } else {
            $("#details-section-menu").removeClass("details-section-sticky");
            $("#player-detail-pager").removeClass("details-section-sticky");
        }
    }
}

window.onscroll = handleScroll;
window.addEventListener("resize", handleScroll);