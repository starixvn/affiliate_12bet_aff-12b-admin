import Excel from '../../../../modules/excel';
import {
    titleCellStyle,
    headerCellStyle,
    bodyCellStyle,
    totalCellStyle
} from '../../../../modules/excelStyleConstant';

const FILENAME = 'PlayerDetail';
const DATA_ROW_INDEX = 1;

const TOTALCOLUMN = 9;

const MAIN_SECTION_STYLE = 'mainSectionStyle';
const TOTAL_SECTION_STYLE = 'totalSectionStyle';

const fields = {
    Index: 'Index',
    UserName: 'UserName',
    IbcUserName: 'IbcUserName',
    RegisteredDateText: 'RegisteredDateText',
    Turnover: 'Turnover',
    Winlost: 'Winlost',
    Deposit: 'Deposit',
    Bonus: 'Bonus',
    BankingFee: 'BankingFee'
}

export default class ReportExcel {
    constructor(data) {
        this.data = data;

        this.currentRowIndex = DATA_ROW_INDEX;
        this.mergeConfig = [];
        this.columnConfigs = this._getcolumnConfigs();
    }

    export () {
        Excel.process(() => {
            this._createHeader();
            this._createBodyData();
            this._createExcelConfig();

            Excel.export(this.worksheet, FILENAME);
        })
    }

    _createHeader() {

    }

    _createBodyData() {

    }

    _createExcelConfig() {
        return [{
            field: fields.Index,
            [MAIN_SECTION_STYLE]: bodyCellStyle.number
        }, {
            field: fields.UserName,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left
        }, {
            field: fields.IbcUserName,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left
        }, {
            field: fields.RegisteredDateText,
            [MAIN_SECTION_STYLE]: bodyCellStyle.center
        }, {
            field: fields.Turnover,
            [MAIN_SECTION_STYLE]: bodyCellStyle.decimal
        }, {
            field: fields.Winlost,
            [MAIN_SECTION_STYLE]: bodyCellStyle.decimal
        }, {
            field: fields.Deposit,
            [MAIN_SECTION_STYLE]: bodyCellStyle.decimal
        }, {
            field: fields.Bonus,
            [MAIN_SECTION_STYLE]: bodyCellStyle.decimal
        }, {
            field: fields.BankingFee,
            [MAIN_SECTION_STYLE]: bodyCellStyle.decimal
        }]
    }
}