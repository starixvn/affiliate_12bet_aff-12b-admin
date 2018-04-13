var AccountOverview = (function ($) {
    var _me = {
        initialize: function () {
            console.log("Init");
            //var picker = new Pikaday({
            //    field: document.getElementById('statisticsDate'),
            //    format: 'D/M/YYYY',
            //    toString(date, format) {
            //        const day = date.getDate();
            //        const month = date.getMonth() + 1;
            //        const year = date.getFullYear();
            //        return `${month}/${day}/${year}`;
            //    },
            //    onSelect: function (date) {
            //        console.log(arguments);
            //        console.log(this);
            //        console.log(this.toString('MM/DD/YYYY'));
            //        //$('#statisticsDate').val(date);
            //    }
            //});
        },

        initEvent: function () {
        }
    };

    return _me;
})(jQuery);

AccountOverview.initialize();