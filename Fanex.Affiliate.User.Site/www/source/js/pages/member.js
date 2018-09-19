var Member = (function ($) {
    var _pageSize = 20;
    var _pageIndex = 0;

    function initPager() {
        $("#pager").pager({
            totalItems: parseInt($("#TotalItem").val()),
            pageSize: _pageSize,
            pageIndex: _pageIndex,
            languageCode: Site.getCurrentLanguage(),
            onChange: function (pageIndex, pageSize) {
                _pageIndex = pageIndex;
                _pageSize = pageSize;

                var params = _me.getParams();
                _me.submit(params);
            }
        });
    }

    function formatDateUnit(value) {
        return value < 10 ? '0' + value : value;
    }

    function initDatePicker(id, triggerId) {
        new Pikaday({
            field: document.getElementById(id),
            trigger: document.getElementById(triggerId),
            yearRange: [2000, new Date().getFullYear()],
            toString: function (date, format) {
                var day = date.getDate();
                var month = date.getMonth() + 1;
                var year = date.getFullYear();
                return formatDateUnit(month) + "/" + formatDateUnit(day) + "/" + year;
            },
            onOpen: function () {
                if ($("#chkGetAll").prop("checked")) {
                    this.hide();
                }
            }
        });
    }

    var _me = {
        initialize: function () {
            initDatePicker('FromDate', 'fromPicker');
            initDatePicker('ToDate', 'toPicker');
            initPager();
            _me.initEvent();
        },

        initEvent: function () {
            $("#btnSubmit").on("click", function () {
                var params = _me.getParams();
                _me.submit(params, _me.updatePager);
            });

            $("#chkGetAll").on("click", function () {
                var isChecked = $(this).prop("checked");
                _me.toggleDate(isChecked);
            });
        },

        getParams: function () {
            var isGetAll = $("#chkGetAll").prop("checked");

            return {
                FromDate: $("#FromDate").val(),
                ToDate: $("#ToDate").val(),
                IsGetAll: isGetAll,
                MemberType: $("#MemberType").val(),
                PageSize: _pageSize,
                PageIndex: _pageIndex + 1
            }
        },

        submit: function (params, onSuccess) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                data: params,
                url: Site.resolveRelativeUrl("UserPanel/Member/GetMemberList"),
                success: function (response) {
                    $("#dataList").html(response);
                    onSuccess && onSuccess();
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        },

        updatePager: function () {
            $("#pager").pager("update", {
                pageIndex: _pageIndex,
                totalItems: parseInt($("#TotalItem").val())
            });
        },

        toggleDate: function (isDisabled) {
            $(".date-wrapper > *").toggleClass("disabled", isDisabled);
        }
    };

    return _me;
})(jQuery);

Member.initialize();