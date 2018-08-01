var commissionReport = (function ($) {

    var _me = {
        initialize: function () {
            $("#btnSubmit").click((e) => {
                e.preventDefault();
                var year = $("#year").val();
                $.ajax({
                    type: "POST",
                    url: Site.resolveRelativeUrl("UserPanel/CommissionReport"),
                    data: JSON.stringify({ "year": year }),
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=UTF-8",
                    success: function (response) {
                        console.log(response);
                    },
                    beforeSend: function () {
                        Site.showLoader();
                    },
                    complete: function () {
                        Site.hideLoader();
                    }
                });
            });
        }


    }
    return _me;
})(jQuery);

commissionReport.initialize();