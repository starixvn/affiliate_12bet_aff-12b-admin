import Excel from '../../../../modules/excel';
import { htmlDecode } from '../../../../modules/htmlHelper';
import {
    headerCellStyle,
    bodyCellStyle
} from '../../../../modules/excelStyleConstant';
import {
    headerText
} from './constants';

const HEADER_ROW_INDEX = 0;
const DATA_ROW_INDEX = 1;

const TOTAL_COLUMN = 10;

const MAIN_SECTION_STYLE = 'mainSectionStyle';

const fields = {
    Index: 'Index',
    UserName: 'UserName',
    IbcUserName: 'IbcUserName',
    RegisteredDate: 'RegisteredDate',
    Turnover: 'Turnover',
    Winloss: 'Winloss',
    Bonus: 'Bonus',
    Deposit: 'Deposit',
    Withdrawal: 'Withdrawal',
    BankingFee: 'BankingFee'
}

export default class ReportExcel {
    constructor(data, title) {
        this.data = data;
        this.title = title;

        this.worksheet = {};
        this.currentRowIndex = DATA_ROW_INDEX;
        this.mergeConfig = [];
        this.columnConfigs = this._getcolumnConfigs();
    }

    export() {
        Excel.process(() => {
            this._createHeader();
            this._createBodyData();
            this._createExcelConfig();

            Excel.export(this.worksheet, this.title);
        })
    }

    _createHeader() {
        for (let i = 0; i < TOTAL_COLUMN; i++) {
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
        }
    }

    _createBodyData() {
        const mainData = this.data.MainData;
        for (let i = 0; i < mainData.length; i++) {
            const item = mainData[i];

            for (let j = 0; j < TOTAL_COLUMN; j++) {
                const address = Excel.getCellAddress({
                    c: j,
                    r: this.currentRowIndex
                });

                this.worksheet[address] = this._buildDataRow(item, MAIN_SECTION_STYLE, i, j);
            }

            this.currentRowIndex++;
        }
    }

    _buildDataRow(item, sectionStyleName, rowIndex, colIndex) {
        const columnConfig = this.columnConfigs[colIndex];
        const fieldName = columnConfig.field;
        const value = fieldName === fields.Index ? rowIndex + 1 : item[fieldName];

        return {
            t: Excel.getCellType(value),
            v: htmlDecode(value),
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

        this.worksheet["!ref"] = `${cellStart}:${cellEnd}`;
        this.worksheet['!cols'] = [{
            wpx: 70
        }, {
            wpx: 200
        }, {
            wpx: 200
        }, {
            wpx: 200
        }, {
            wpx: 150
        }, {
            wpx: 150
        }, {
            wpx: 150
        }, {
            wpx: 150
        }, {
            wpx: 150
        }, {
            wpx: 150
        }];
    }

    _getcolumnConfigs() {
        return [{
            field: fields.Index,
            [MAIN_SECTION_STYLE]: bodyCellStyle.center
        }, {
            field: fields.UserName,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left
        }, {
            field: fields.IbcUserName,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left
        }, {
            field: fields.RegisteredDate,
            [MAIN_SECTION_STYLE]: bodyCellStyle.center
        }, {
            field: fields.Turnover,
            [MAIN_SECTION_STYLE]: bodyCellStyle.decimal
        }, {
            field: fields.Winloss,
            [MAIN_SECTION_STYLE]: bodyCellStyle.decimal
        }, {
            field: fields.Bonus,
            [MAIN_SECTION_STYLE]: bodyCellStyle.decimal
        }, {
            field: fields.Deposit,
            [MAIN_SECTION_STYLE]: bodyCellStyle.decimal
        }, {
            field: fields.Withdrawal,
            [MAIN_SECTION_STYLE]: bodyCellStyle.decimal
        }, {
            field: fields.BankingFee,
            [MAIN_SECTION_STYLE]: bodyCellStyle.decimal
        }]
    }
}