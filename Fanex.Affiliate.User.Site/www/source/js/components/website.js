var WebSite = (function ($) {
    var _me = {
        getMoreWebsiteUrlData: function () {
            var moreUrlList = [];
            $("#websiteInfo .urlInput").each(function (index, data) {
                if (data.value) {
                    moreUrlList.push(Site.escapeHtml(data.value.trim()));
                }
            });

            return moreUrlList.join(";");
        },

        reoderInputUrl: function (webSiteLabel) {
            var index = 2;
            $(".moreWebsiteLabel").each(function (i, item) {
                var $item = $(item);
                var labelText = webSiteLabel + ' ' + index;
                $item.text(labelText);

                index++;
            });
        },

        deleteWebsiteUrl: function ($currentItem) {
            var $container = $currentItem.closest(".websiteInput");
            $container.remove();
        },

        addMoreUrl: function (webSiteLabel, deleteLabel, errorMessage) {
            var currentItemCount = $("#websiteInfo .urlInput").length + 2;
            var inputItem = '<div class="form-group websiteInput">' +
                    '<label class="col-sm-3 control-label require moreWebsiteLabel">' + webSiteLabel + ' ' + currentItemCount + '</label>' +
                    '<div class="col-sm-4">' +
                        '<input class="form-control urlInput" type="text" value="" />' +
                    '</div>' +
                    '<div class="col-sm-1">' +
                        '<button type="button" class="btn btn-submit btnDeleteUrl">' + deleteLabel + '</button>' +
                    '</div>' +
                    '<div class="col-sm-offset-3 col-sm-9"><span class="help-block hide">' + errorMessage + '</span></div>' +
                '</div>';

            $("#websiteInfo").append(inputItem);
            _me.reoderInputUrl(webSiteLabel);
        },

        canAddMoreUrl: function () {
            var $inputList = $("#websiteInfo .urlInput");
            for (var i = 0; i < $inputList.length; i++) {
                if ($inputList[i].value.trim() === "") {
                    return false;
                }
            }

            return true;
        }
    }

    return _me;
})(jQuery);