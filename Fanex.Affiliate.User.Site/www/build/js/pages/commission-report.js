"use strict";

var commissionReport = (function ($) {

    var _me = {
        initialize: function initialize() {
            $("#btnSubmit").click(function (e) {
                e.preventDefault();
                var year = $("#year").val();
                $.ajax({
                    type: "POST",
                    url: Site.resolveRelativeUrl("UserPanel/CommissionReport"),
                    data: JSON.stringify({ "year": year }),
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=UTF-8",
                    success: function success(response) {
                        console.log(response);
                    },
                    beforeSend: function beforeSend() {
                        Site.showLoader();
                    },
                    complete: function complete() {
                        Site.hideLoader();
                    }
                });
            });
        }

    };
    return _me;
})(jQuery);

commissionReport.initialize();

