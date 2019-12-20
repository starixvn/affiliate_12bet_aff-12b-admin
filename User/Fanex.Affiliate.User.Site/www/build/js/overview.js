var SUPPORT_MIN_WITDH = 716;
var SUB_MENU_HEIGHT = 43;

function handleSetTop() {
    let headerHeight = $('#overview-table-header').height();
    $('#overview-table-total > th').css({ top: `${SUB_MENU_HEIGHT + headerHeight}px` });
}

function handleScroll() {
    if (window.innerWidth < SUPPORT_MIN_WITDH) {
        $("#details-section-menu").removeClass("details-section-sticky");
    } else {
        let stickyPoint = $("#details-section").offset().top;

        if (window.pageYOffset >= stickyPoint) {
            $("#details-section-menu").addClass("details-section-sticky");
        } else {
            $("#details-section-menu").removeClass("details-section-sticky");
        }
    }
}

function handleResize() {
    handleScroll();
    handleSetTop()
}

window.onscroll = handleScroll;
window.addEventListener("resize", handleResize);
$(document).ready(handleSetTop);