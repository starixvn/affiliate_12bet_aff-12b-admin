(function ($) {
    let _id = null;
    let _class = null;

    var _me = {
        initEvent: function (idName) {
            _id = `#${idName}-multiple-select`;
            _class = `.${idName}-multiple-select`;

            $(`${_id}-label`).on('click', function () {
                $(`${_id}-options-container`).toggle();

                document.addEventListener('click', _me.handleDocumentClick);
            });

            $(`${_id}-option-all`).on('change', function (event) {
                $(`${_class}-option-checkbox`).prop("checked", $(this).is(":checked"));

                _me.handleUpdateLabel();
            });

            $(`${_class}-option-checkbox`).on('change', function (event) {
                if (!$(this).is(":checked")) {
                    $(`${_id}-option-all`).prop("checked", false);
                } else {
                    let isCheckAll = $(`${_class}-option-checkbox:checked`).length == $(`${_class}-option-checkbox`).length

                    $(`${_id}-option-all`).prop("checked", isCheckAll);
                }

                _me.handleUpdateLabel();
            });
        },
        handleDocumentClick: function (event) {
            if (!$(`${_id}`).length) {
                _me.removeEventListener();
            } else {
                if (!$(`${_id}`).find(event.target).length) {
                    _me.removeEventListener();
                    $(`${_id}-options-container`).toggle();
                }
            }
        },
        removeEventListener: function () {
            document.removeEventListener('click', _me.handleDocumentClick)
        },
        handleUpdateLabel: function () {
            var selectedItems = $(`${_class}-option-checkbox:checked`).length;

            if (!selectedItems) {
                $(`${_id}`).addClass("multiple-select-wrapper is-invalid");
                $(`${_id}-text-display`).html("Please Select");
            } else {
                $(`${_id}`).removeClass("multiple-select-wrapper is-invalid");
                $(`${_id}-text-display`).html(`${selectedItems} selected`);
            }
        },
        getSelectedValues: function () {
            return $(`${_class}-option-checkbox`).map((index, el) => el.value).get().join(',');
        }
    };

    $.fn.multipleSelector = function () {
        _me.initEvent(this.prop('id'));

        return {
            getSelectedValues: _me.getSelectedValues
        }
    };
})(jQuery);