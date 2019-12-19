function handleSetTop() {
    let headerHeight = $('#overview-table-header').height();
    $('#overview-table-total > th').css({ top: `${submenuHeight + headerHeight}px` });
}

function handleScroll() {
    if (window.innerWidth < supportMinWidth) {
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