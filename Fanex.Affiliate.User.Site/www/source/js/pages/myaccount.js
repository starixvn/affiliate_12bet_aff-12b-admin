var MyAccount = (function ($) {
    var _me = {
        initialize: function () {
            _me.initEvent();
        },
        initEvent: function () {
            $("#btnAddUrl").on("click", function () {
                _me.addMoreUrl();
            });

            $("#websiteInfo").on("click", ".btnDeleteUrl", function () {
                var $this = $(this);
                _me.deleteWebsiteUrl($this);
            });
        },
        addMoreUrl: function () {
            var currentItemCount = $("#websiteInfo .urlInput").length;
            var inputItem = '<div class="form-group websiteInput">' +
                    '<label class="col-sm-3 control-label moreWebsiteLabel">' + LangResource.websiteUrl + ' ' + currentItemCount + '</label>' +
                    '<div class="col-sm-4">' +
                        '<input class="form-control urlInput" type="text" value="" />' +
                    '</div>' +
                    '<div class="col-sm-1">' +
                        '<button type="button" class="btn btn-submit btnDeleteUrl">' + LangResource.deleteUrl + '</button>' +
                    '</div>' +
                '</div>';

            $("#websiteInfo").append(inputItem);
            _me.reoderInputUrl();
        },
        deleteWebsiteUrl: function ($this) {
            var $container = $this.closest(".websiteInput");
            $container.remove();

            _me.reoderInputUrl();
        },
        reoderInputUrl: function () {
            var index = 2;
            $(".moreWebsiteLabel").each(function (i, item) {
                var $item = $(item);
                var labelText = LangResource.websiteUrl + ' ' + index;
                $item.text(labelText);

                index++;
            });
        }
    };

    return _me;
})(jQuery)

MyAccount.initialize();