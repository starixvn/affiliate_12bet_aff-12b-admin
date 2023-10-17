import Excel from '../../../modules/excel';
import {
    titleCellStyle,
    headerCellStyle,
    bodyCellStyle,
    totalCellStyle
} from '../../../modules/excelStyleConstant';
import {
    totalText,
    reportKey as fields
} from './constants';
import { formatDecimal } from '../../../modules/numberFormat';
import { AggregateHeader } from './header';

const TITLE_ROW_INDEX = 0;
const HEADER_ROW_INDEX = 1;
const DATA_ROW_INDEX = 3;

const MAIN_SECTION_STYLE = 'mainSectionStyle';
const TOTAL_SECTION_STYLE = 'totalSectionStyle';
const COL_WIDTH = 'wpx';

const colConfig = {
    [fields.UserName]: {
        [MAIN_SECTION_STYLE]: bodyCellStyle.left,
        [TOTAL_SECTION_STYLE]: totalCellStyle.default,
        [COL_WIDTH] : 150
    },
    [fields.MonthDateString]: {
        [MAIN_SECTION_STYLE]: bodyCellStyle.left,
        [TOTAL_SECTION_STYLE]: totalCellStyle.default,
        [COL_WIDTH] : 150
    },
    [fields.PreferredLanguage]: {
        [MAIN_SECTION_STYLE]: bodyCellStyle.left,
        [TOTAL_SECTION_STYLE]: totalCellStyle.default,
        [COL_WIDTH]: 120
    },
    [fields.TotalClick]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.number,
        [TOTAL_SECTION_STYLE]: totalCellStyle.number,
        [COL_WIDTH] : 80
    },
    [fields.UniqueClick]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.number,
        [TOTAL_SECTION_STYLE]: totalCellStyle.number,
        [COL_WIDTH] : 80
    },
    [fields.Signup]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.number,
        [TOTAL_SECTION_STYLE]: totalCellStyle.number,
        [COL_WIDTH] : 80
    },
    [fields.Fdm]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.number,
        [TOTAL_SECTION_STYLE]: totalCellStyle.number,
        [COL_WIDTH] : 80
    },
    [fields.ActiveMember]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.number,
        [TOTAL_SECTION_STYLE]: totalCellStyle.number,
        [COL_WIDTH] : 100
    },
    [fields.QualifiedMember]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.number,
        [TOTAL_SECTION_STYLE]: totalCellStyle.number,
        [COL_WIDTH] : 120
    },
    [fields.Deposit]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH] : 120
    },
    [fields.Withdrawal]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH] : 120
    },
    [fields.Turnover]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH] : 120
    },
    [fields.Winlost]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH] : 120
    },
    [fields.Bonus]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH] : 80
    },
    [fields.PlatformFee]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH] : 100
    },
    [fields.OperationalFee]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH] : 110
    },
    [fields.BankingFee]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH] : 100
    },
    [fields.TotalFee]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH] : 100
    },
    [fields.CommissionRate]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.right,
        [TOTAL_SECTION_STYLE]: totalCellStyle.right,
        [COL_WIDTH] : 80
    },
    [fields.PreviousBalanceBf]: {
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH]: 225
    },
    [fields.Earning]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH] : 100
    },
    [fields.NewFdmEarning]: {
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH]: 150
    },
    [fields.TotalEarning]: {
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH]: 100
    },
    [fields.AffWithdrawal]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH] : 100
    },
    [fields.AffAdjustment]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH] : 100
    },
    [fields.BalanceBf]:{
        [MAIN_SECTION_STYLE]: bodyCellStyle.decimal,
        [TOTAL_SECTION_STYLE]: totalCellStyle.decimal,
        [COL_WIDTH] : 150
    }

}

export default class ReportExcel {
    constructor(data, title, header, isMainView, languageList) {
        this.worksheet = {};
        this.title = isMainView ? title : 'AFFILIATE REPORT > ' + title;
        this.data = data;
        this.isMainView = isMainView;
        this.header = header;
        this.languageList = languageList;

        this.currentRowIndex = DATA_ROW_INDEX;
        this.mergeConfig = [];
        this.columnConfigs = this._getcolumnConfigs(header);

        this.totalColumn = this.columnConfigs.length;
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
        const visableCells = this.header.childerent.filter(c => c.isVisable);
        let curColIndex = 0;
        visableCells.forEach(cell => {
            if (cell instanceof AggregateHeader)
            {
                this._buildColspanColumn(HEADER_ROW_INDEX, curColIndex, cell);
                curColIndex += cell.totalVisableCol;
            }
            else{
                let address = Excel.getCellAddress({
                    c: curColIndex,
                    r: HEADER_ROW_INDEX
                });
                this.worksheet[address] = {
                    t: "s",
                    v: cell.text,
                    s: headerCellStyle
                }

                this.mergeConfig.push({
                    s: {
                        r: HEADER_ROW_INDEX,
                        c: curColIndex
                    },
                    e: {
                        r: HEADER_ROW_INDEX + 1,
                        c: curColIndex
                    }
                });

                curColIndex++;
            }
        })
    }

    _buildColspanColumn(rowIndex, colIndex, headerCell) {
        let childCells = headerCell.childerent.filter(c => c.isVisable);
        const colSpan = childCells.length;

        const colSpanCellAddress = Excel.getCellAddress({
            c: colIndex,
            r: rowIndex
        });
        this.worksheet[colSpanCellAddress] = {
            t: "s",
            v: headerCell.text,
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

        let curIndex = colIndex;
        childCells.forEach(cell => {
            const address = Excel.getCellAddress({
                c: curIndex,
                r: rowIndex + 1
            });

            this.worksheet[address] = {
                t: "s",
                v: cell.text || '',
                s: headerCellStyle
            }
            curIndex++;
        });
    }

    _createBodyData() {
        const mainData = this.data.MainData;

        for (let item of mainData) {
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
        let value = item[columnConfig.field];

        if (columnConfig.field === fields.PreferredLanguage) {
            value = this.languageList[value] || '';
        }

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
            c: this.totalColumn
        });

        this.worksheet["!merges"] = this.mergeConfig;
        this.worksheet["!ref"] = `${cellStart}:${cellEnd}`;
        this.worksheet['!cols'] = this.columnConfigs.map(item => {
            return {[COL_WIDTH] : item[COL_WIDTH]}
        })
    }

    _getcolumnConfigs(header) {
        const visableCols = header.getCols().filter(item => item.isVisable);
        return visableCols.map(col => {
            return {
                field: col.key,
                [MAIN_SECTION_STYLE]: colConfig[col.key][MAIN_SECTION_STYLE],
                [TOTAL_SECTION_STYLE]: colConfig[col.key][TOTAL_SECTION_STYLE],
                [COL_WIDTH]: colConfig[col.key][COL_WIDTH]
            }
        })
    }
}