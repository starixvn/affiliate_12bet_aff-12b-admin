var MultipleSelect = (function ($) {
    let _id = null;
    let _dataSource = null;

    var _me = {
        initialize: function (id, dataSource) {
            _id = id;
            _dataSource = dataSource;
        },
        initEvent: function () {
            $(`${_id}-multiple-select-label`).on('click', function () {
                $(`${_id}-multiple-select-options-container`).toggle();
            });

            $(`${_id}-option-all`).on('change', function (event) {
                $(`${_id}-option-checkbox`).prop("checked", $(this).is(":checked"));
            });

            $(`${_id}-option-checkbox`).on('change', function (event) {
                if (!$(this).is(":checked")) {
                    $(`${_id}-option-all`).prop("checked", false);
                } else {
                    let isCheckAll = $(`${_id}-option-checkbox:checked`).length == $(`${_id}-option-checkbox`).length

                    $(`${_id}-option-all`).prop("checked", isCheckAll);
                }
            });
        },
        resolveRelativeUrl: function (url) {
            return _rootUrl + url;
        },
        getCurrentLanguage: function () {
            return _language;
        },
        escapeHtml: function (inputString) {
            var div = document.createElement('div');
            div.appendChild(document.createTextNode(inputString));
            return div.innerHTML;
        },
        showLoader: function () {
            $("#blocker").show();
        },
        hideLoader: function () {
            $("#blocker").hide();
        },
        redirect: function (url) {
            window.location.href = url;
        },
        reload: function () {
            window.location.reload(true);
        },
        displayResult: function (isSuccess, message, selector) {
            selector = selector || "#messageResult";
            var $result = $(selector);
            $result.toggleClass("alert-success", isSuccess);
            $result.toggleClass("alert-danger", !isSuccess);
            $result.html(message);
            $result.show();
        }
    };

    return _me;
})(jQuery);

MultipleSelect.initialize();