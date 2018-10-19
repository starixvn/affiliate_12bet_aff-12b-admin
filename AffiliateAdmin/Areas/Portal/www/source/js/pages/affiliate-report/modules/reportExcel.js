import Excel from '../../../modules/excel';
import {
    titleCellStyle,
    headerCellStyle,
    bodyCellStyle,
    totalCellStyle
} from '../../../modules/excelStyleConstant';
import {
    headerText,
    totalText
} from './constants';
import { formatDecimal } from '../../../modules/numberFormat';

const TITLE_ROW_INDEX = 0;
const HEADER_ROW_INDEX = 1;
const DATA_ROW_INDEX = 3;

const TOTAL_COLUMN = 21;

const INDEX_MEMBER_STATISTICS_COLUMN = 7;
const INDEX_AFFILIATE_STATISTICS_COLUMN = 8;

const INDEX_DEPOSIT_COLUMN = 7;
const COLSPAN_MEMBER_STATISTICS = 9;
const INDEX_COMM_COLUMN = 16;
const COLSPAN_AFFILIATE_STATISTICS = 5;

const MAIN_SECTION_STYLE = 'mainSectionStyle';
const TOTAL_SECTION_STYLE = 'totalSectionStyle';

const fields = {
    UserName: 'UserName',
    Description: 'Description',
    MonthDateString: 'MonthDateString',
    TotalClick: 'TotalClick',
    UniqueClick: 'UniqueClick',
    Signup: 'Signup',
    Fdm: 'Fdm',
    ActiveMember: 'ActiveMember',
    QualifiedMember: 'QualifiedMember',
    Deposit: 'Deposit',
    Withdrawal: 'Withdrawal',
    Turnover: 'Turnover',
    Winlost: 'Winlost',
    Bonus: 'Bonus',
    PlatformFee: 'PlatformFee',
    OperationalFee: 'OperationalFee',
    BankingFee: 'BankingFee',
    TotalFee: 'TotalFee',
    CommissionRate: 'CommissionRate',
    Earning: 'Earning',
    AffWithdrawal: 'AffWithdrawal',
    AffAdjustment: 'AffAdjustment',
    BalanceBf: 'BalanceBf'
}

export default class ReportExcel {
    constructor(data, title, isMainView) {
        this.worksheet = {};
        this.title = isMainView ? title : 'AFFILIATE REPORT > ' + title;
        this.data = data;
        this.isMainView = isMainView;

        this.currentRowIndex = DATA_ROW_INDEX;
        this.mergeConfig = [];
        this.columnConfigs = this._getcolumnConfigs(isMainView);
        this.totalColumn = isMainView ? TOTAL_COLUMN : TOTAL_COLUMN - 1;
    }

    export () {
        Excel.process(() => {
            this._createTitle();
            this._createHeader();
            this._createBodyData();
            this._createTotalRow();
            this._createExcelConfig();

            Excel.export(this.worksheet, this.title.replace('> ', ''));
        })
    }

    _createTitle() {
        this.mergeConfig.push({
            s: {
                r: TITLE_ROW_INDEX,
                c: 0
            },
            e: {
                r: TITLE_ROW_INDEX,
                c: this.totalColumn - 1
            }
        });
        const address = Excel.getCellAddress({
            c: 0,
            r: TITLE_ROW_INDEX
        });
        this.worksheet[address] = {
            t: "s",
            v: this.title,
            s: titleCellStyle
        }
    }

    _createHeader() {
        const colSpanAffStatistics = this.isMainView ? COLSPAN_AFFILIATE_STATISTICS : COLSPAN_AFFILIATE_STATISTICS - 1;
        const length = this.columnConfigs.length - (COLSPAN_MEMBER_STATISTICS - 1) - (colSpanAffStatistics - 1);
        for (let i = 0; i < length; i++) {
            if (i === INDEX_MEMBER_STATISTICS_COLUMN) {
                this._buildColspanColumn(HEADER_ROW_INDEX, INDEX_DEPOSIT_COLUMN, COLSPAN_MEMBER_STATISTICS, headerText.MemberStatistics);
            } else if (i === INDEX_AFFILIATE_STATISTICS_COLUMN) {
                this._buildColspanColumn(HEADER_ROW_INDEX, INDEX_COMM_COLUMN, colSpanAffStatistics, headerText.AffiliateStatistics);
            } else {
                const fieldName = this.columnConfigs[i].field;
                let address = Excel.getCellAddress({
                    c: i,
                    r: HEADER_ROW_INDEX
                });
                this.worksheet[address] = {
                    t: "s",
                    v: headerText[fieldName],
                    s: headerCellStyle
                }

                this.mergeConfig.push({
                    s: {
                        r: HEADER_ROW_INDEX,
                        c: i
                    },
                    e: {
                        r: HEADER_ROW_INDEX + 1,
                        c: i
                    }
                });
            }
        }
    }

    _buildColspanColumn(rowIndex, colIndex, colSpan, colSpanheaderText) {
        const colSpanCellAddress = Excel.getCellAddress({
            c: colIndex,
            r: rowIndex
        });
        this.worksheet[colSpanCellAddress] = {
            t: "s",
            v: colSpanheaderText,
            s: headerCellStyle
        }

        const totalColumn = colIndex + colSpan;
        this.mergeConfig.push({
            s: {
                r: rowIndex,
                c: colIndex
            },
            e: {
                r: rowIndex,
                c: totalColumn - 1
            }
        });

        for (let i = colIndex; i < totalColumn; i++) {
            const address = Excel.getCellAddress({
                c: i,
                r: rowIndex + 1
            });

            const fieldName = this.columnConfigs[i].field;
            this.worksheet[address] = {
                t: "s",
                v: headerText[fieldName] || '',
                s: headerCellStyle
            }
        }
    }

    _createBodyData() {
        const mainData = this.data.MainData;

        for (let i = 0; i < mainData.length; i++) {
            const item = mainData[i];
            item.CommissionRate = formatDecimal(item.CommissionRate) + '%';

            if (!this.isMainView) {
                const {
                    MonthDateString,
                    Description
                } = item;
                item.MonthDateString = Description ? `${MonthDateString} (${Description})` : MonthDateString;
            }

            for (let j = 0; j < this.totalColumn; j++) {
                const address = Excel.getCellAddress({
                    c: j,
                    r: this.currentRowIndex
                });

                this.worksheet[address] = this._buildDataRow(item, MAIN_SECTION_STYLE, j);
            }

            this.currentRowIndex++;
        }
    }

    _createTotalRow() {
        const totalData = this.data.Total;
        const customTotalData = this.isMainView ? {
            [fields.UserName]: totalText,
            [fields.CommissionRate]: "N/A"
        } : {
            [fields.MonthDateString]: totalText,
            [fields.CommissionRate]: formatDecimal(totalData.CommissionRate) + '%'
        }
        const total = Object.assign({}, totalData, customTotalData);

        for (let j = 0; j < this.totalColumn; j++) {
            const address = Excel.getCellAddress({
                c: j,
                r: this.currentRowIndex
            });

            this.worksheet[address] = this._buildDataRow(total, TOTAL_SECTION_STYLE, j);
        }
    }

    _buildDataRow(item, sectionStyleName, colIndex) {
        const columnConfig = this.columnConfigs[colIndex];
        const value = item[columnConfig.field];

        return {
            t: Excel.getCellType(value),
            v: value,
            s: columnConfig[sectionStyleName]
        }
    }

    _createExcelConfig() {
        const cellStart = Excel.getCellAddress({
            r: 0,
            c: 0
        });
        const cellEnd = Excel.getCellAddress({
            r: this.currentRowIndex,
            c: TOTAL_COLUMN
        });

        this.worksheet["!merges"] = this.mergeConfig;
        this.worksheet["!ref"] = `${cellStart}:${cellEnd}`;
        this.worksheet['!cols'] = [{
                wpx: 150
            }, {
                wpx: 80
            }, {
                wpx: 80
            }, {
                wpx: 80
            }, {
                wpx: 80
            }, {
                wpx: 100
            }, {
                wpx: 120
            },
            {
                wpx: 120
            }, {
                wpx: 120
            }, {
                wpx: 120
            }, {
                wpx: 120
            }, {
                wpx: 80
            }, {
                wpx: 100
            }, {
                wpx: 110
            }, {
                wpx: 100
            }, {
                wpx: 100
            },
            {
                wpx: 80
            }, {
                wpx: 100
            }, {
                wpx: 100
            }, {
                wpx: 100
            }, {
                wpx: 120
            }
        ];
    }

    _getcolumnConfigs(isMainView) {
        const columnConfigList = [{
                field: isMainView ? fields.UserName : fields.MonthDateString,
                [MAIN_SECTION_STYLE]: bodyCellStyle.left,
                [TOTAL_SECTION_STYLE]: totalCellStyle.default
            },
            {
                field: fields.TotalClick,
                [MAIN_SECTION_STYLE]: bodyCellStyle.number,
                [TOTAL_SECTION_STYLE]: totalCellStyle.number
            },
            {
                field: fields.UniqueClick,
                [MAIN_SECTION_STYLE]: bodyCellStyle.number,
                [TOTAL_SECTION_STYLE]: totalCellStyle.number
            },
            {
                field: fields.Signup,
                [MAIN_SECTION_STYLE]: bodyCellStyle.number,
                [TOTAL_SECTION_STYLE]: totalCellStyle.number
            },
            {
                field: fields.Fdm,
                [MAIN_SECTION_STYLE]: bodyCellStyle.number,
                [TOTAL_SECTION_STYLE]: totalCellStyle.number
            },
            {
                field: fields.ActiveMember,
                [MAIN_SECTION_STYLE]: bodyCellStyle.number,
                [TOTAL_SECTION_STYLE]: totalCellStyle.number
            },
            {
                field: fields.QualifiedMember,
                [MAIN_SECTION_STYLE]: bodyCellStyle.number,
                [TOTAL_SECTION_STYLE]: totalCellStyle.number
            },
            {
                field: fields.Deposit,
                [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
                [TOTAL_SECTION_STYLE]: totalCellStyle.decimal
            },
            {
                field: fields.Withdrawal,
                [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
                [TOTAL_SECTION_STYLE]: totalCellStyle.decimal
            },
            {
                field: fields.Turnover,
                [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
                [TOTAL_SECTION_STYLE]: totalCellStyle.decimal
            },
            {
                field: fields.Winlost,
                [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
                [TOTAL_SECTION_STYLE]: totalCellStyle.decimal
            },
            {
                field: fields.Bonus,
                [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
                [TOTAL_SECTION_STYLE]: totalCellStyle.decimal
            },
            {
                field: fields.PlatformFee,
                [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
                [TOTAL_SECTION_STYLE]: totalCellStyle.decimal
            },
            {
                field: fields.OperationalFee,
                [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
                [TOTAL_SECTION_STYLE]: totalCellStyle.decimal
            },
            {
                field: fields.BankingFee,
                [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
                [TOTAL_SECTION_STYLE]: totalCellStyle.decimal
            },
            {
                field: fields.TotalFee,
                [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
                [TOTAL_SECTION_STYLE]: totalCellStyle.decimal
            },
            {
                field: fields.CommissionRate,
                [MAIN_SECTION_STYLE]: bodyCellStyle.right,
                [TOTAL_SECTION_STYLE]: totalCellStyle.right
            },
            {
                field: fields.Earning,
                [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
                [TOTAL_SECTION_STYLE]: totalCellStyle.decimal
            },
            {
                field: fields.AffWithdrawal,
                [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
                [TOTAL_SECTION_STYLE]: totalCellStyle.decimal
            }
        ];

        if (isMainView) {
            columnConfigList.push({
                field: fields.AffAdjustment,
                [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
                [TOTAL_SECTION_STYLE]: totalCellStyle.decimal
            })
        }

        columnConfigList.push({
            field: fields.BalanceBf,
            [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
            [TOTAL_SECTION_STYLE]: totalCellStyle.decimal
        });

        return columnConfigList;
    }
}