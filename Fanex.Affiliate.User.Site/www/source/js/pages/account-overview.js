var AccountOverview = (function ($) {
    var _me = {
        initialize: function () {
            console.log("Init");
            var picker = new Pikaday({
                field: $('#statisticsDate')[0],
                format: 'MM/DD/YYYY',
                onSelect: function (date) {
                    console.log(date);
                    $('#statisticsDate').val(date);
                }
            });
        },

        initEvent: function () {
        }
    };

    return _me;
})(jQuery);

AccountOverview.initialize();