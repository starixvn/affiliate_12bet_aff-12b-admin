"use strict";

var commissionReport = (function ($) {
    var _reportData = null;
    var _sortingColumn = 'Month';
    var _isSortingAsc = false;

    function formatDecimal(value) {
        return format(value, 2);
    }

    function format(value, digits) {
        if (typeof value !== "number" || isNaN(value)) {
            return "";
        }

        var roundedValue = round(value, digits);

        return roundedValue.toFixed(digits).replace(/(\d)(?=(\d{3})+(\.|$))/g, "$1,");
    }

    function round(value, digits) {
        var sign = value < 0 ? -1 : 1;
        var absoluteValue = Math.abs(value);
        var rounded = Number(Math.round(absoluteValue + 'e' + digits) + 'e-' + digits);

        return sign * rounded;
    }

    var _me = {
        initialize: function initialize() {
            _me.initEvent();
        },

        initEvent: function initEvent() {
            $('#dataTable').on('click', '.th', function () {
                var $this = $(this);
                _me.processSorting($this);
            });

            $('#btnSubmit').click(function (e) {
                e.preventDefault();
                _me.submit();
            });

            $('#dataTable').on('click', '.toggleDetailRow', function () {
                var $this = $(this);
                var isExpanding = $this.hasClass('bottom');

                $this.toggleClass('bottom', !isExpanding);
                $this.toggleClass('left', isExpanding);

                $this.closest('.sumrow').toggleClass('active', !isExpanding).nextUntil('.sumrow').toggleClass('expanded', !isExpanding);
                $('.sumrow.active').nextUntil('.sumrow').addClass('expanded');
            });
        },

        submit: function submit() {
            var year = $("#year").val();
            $.ajax({
                type: "POST",
                url: Site.resolveRelativeUrl("UserPanel/CommissionReport"),
                data: JSON.stringify({ "year": year }),
                dataType: "json",
                cache: false,
                contentType: "application/json; charset=UTF-8",
                success: function success(response) {
                    _reportData = response;
                    if (_reportData) {
                        _me.renderData();
                        _me.activeSortIcon($('#defaultSorting'));
                    }
                },
                beforeSend: function beforeSend() {
                    Site.showLoader();
                },
                complete: function complete() {
                    Site.hideLoader();
                }
            });
        },

        renderData: function renderData() {
            if (_reportData.Data && _reportData.Data.length) {
                var bodyHtml = _me.buildDataTable();
                $('#reportData').html(bodyHtml);
                $('.sumrow.active').nextUntil('.sumrow').addClass('expanded');
            } else {
                $('#reportData').html('<tr class="noInfo"><td class="text-center" colspan="10">' + langResource.noInfo + '</td></tr>');
            }
        },

        buildDataTable: function buildDataTable() {
            var mainData = _reportData.Data;
            var htmlRowList = [];

            for (var i = 0; i < mainData.length; i++) {
                var dataItem = mainData[i];
                var summaryDataRow = dataItem.Data;
                htmlRowList.push(_me.buildRow(summaryDataRow, true, i, dataItem.DiplayMonth));

                var detailRowList = dataItem.ProductData;
                for (var j = 0; j < detailRowList.length; j++) {
                    var productRow = detailRowList[j];
                    htmlRowList.push(_me.buildRow(productRow, false));
                }
            }

            htmlRowList.push(_me.buildTotalRow());

            return htmlRowList.join("");
        },

        buildRow: function buildRow(dataRow, isSummaryRow, index, title) {
            var isFirstSumRow = index === 0;
            var activeClass = isFirstSumRow ? ' active' : '';
            var toggleIconClass = isFirstSumRow ? 'bottom' : 'left';
            var className = isSummaryRow ? ' class="sumrow' + activeClass + '"' : '';

            var iconHtml = '<span class="chevron toggleDetailRow ' + toggleIconClass + '" aria-hidden="true"></span>';
            var rowItems = [];
            rowItems.push('<tr' + className + '>');
            rowItems.push('<td class="text-center">' + (isSummaryRow ? index + 1 : '') + '</td>');
            rowItems.push('<td class="title-cell"><div class="row-wrapper">' + (isSummaryRow ? title + iconHtml : dataRow.ProductName) + '</div></td>');
            rowItems.push(_me.buildCell(formatDecimal(dataRow.TurnOver)));
            rowItems.push(_me.buildCell(formatDecimal(dataRow.WinLoss)));
            if (isSummaryRow) {
                rowItems.push(_me.buildCell(formatDecimal(dataRow.PlatformFee)));
                rowItems.push(_me.buildCell(formatDecimal(dataRow.OperationalFee)));
                rowItems.push(_me.buildCell(formatDecimal(dataRow.BankingFee)));
                rowItems.push(_me.buildCell(formatDecimal(dataRow.Bonus)));
                rowItems.push(_me.buildCell(dataRow.CommissionRate + '%'));
                rowItems.push(_me.buildCell(formatDecimal(dataRow.Commission)));
            } else {
                for (var i = 0; i < 6; i++) {
                    rowItems.push(_me.buildCell(""));
                }
            }
            rowItems.push('</tr>');

            return rowItems.join("");
        },

        buildTotalRow: function buildTotalRow() {
            var dataRow = _reportData.Total;
            if (dataRow) {
                return '<tr class="total">' + '<td class="text-right" colspan="2">' + 'Total' + '</td>' + _me.buildCell(formatDecimal(dataRow.TurnOver)) + _me.buildCell(formatDecimal(dataRow.WinLoss)) + _me.buildCell(formatDecimal(dataRow.PlatformFee)) + _me.buildCell(formatDecimal(dataRow.OperationalFee)) + _me.buildCell(formatDecimal(dataRow.BankingFee)) + _me.buildCell(formatDecimal(dataRow.Bonus)) + _me.buildCell('') + _me.buildCell(formatDecimal(dataRow.Commission)) + '</tr>';
            }

            return '';
        },

        buildCell: function buildCell(cellData) {
            return '<td class="text-right">' + cellData + '</td>';
        },

        processSorting: function processSorting($element) {
            var columnName = $element.closest('.th').data('name');
            if (_reportData && _reportData.Data && _reportData.Data.length) {
                if (columnName === _sortingColumn) {
                    _isSortingAsc = !_isSortingAsc;
                } else {
                    _isSortingAsc = true;
                    _sortingColumn = columnName;
                }

                var sortingSelector = _isSortingAsc ? '.asc' : '.desc';
                var $sortIcon = $element.find(sortingSelector);
                _me.activeSortIcon($sortIcon);
                _me.sort();
                _me.renderData();
            }
        },

        activeSortIcon: function activeSortIcon($element) {
            $('.th').find('.caret.current').removeClass('current');
            $element.addClass('current');
        },

        sort: function sort() {
            var mainDataList = _reportData.Data;

            mainDataList.sort(function (item, nextItem) {
                if (_sortingColumn === 'Month') {
                    return _isSortingAsc ? item.Month - nextItem.Month : -(item.Month - nextItem.Month);
                } else {
                    return _isSortingAsc ? item.Data[_sortingColumn] - nextItem.Data[_sortingColumn] : -(item.Data[_sortingColumn] - nextItem.Data[_sortingColumn]);
                }
            });
        }
    };
    return _me;
})(jQuery);

commissionReport.initialize();

