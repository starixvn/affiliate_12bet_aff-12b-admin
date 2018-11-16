import Excel from '../../../modules/excel';
import {
    titleCellStyle,
    headerCellStyle,
    bodyCellStyle
} from '../../../modules/excelStyleConstant';
import {
    headerText,
    resource
} from './constants';
import {
    format,
    formatDecimal
} from '../../../modules/numberFormat';

const REPORT_TITLE_ROW_INDEX = 0;
const HEADER_ROW_INDEX = 1;
const DATA_ROW_INDEX = 2;

const TOTAL_COLUMN = 13;

const MAIN_SECTION_STYLE = 'mainSectionStyle';

const fields = {
    Index: 'Index',
    UserName: 'UserName',
    Status: 'Status',
    FirstName: 'FirstName',
    LastName: 'LastName',
    Language: 'Language',
    Email: 'Email',
    CommissionGroup: 'CommissionGroup',
    BalanceBf: 'BalanceBf',
    Member: 'Member',
    RegistrationDateText: 'RegistrationDateText',
    ApprovalDateText: 'ApprovalDateText',
    CasinoDateText: 'CasinoDateText'
}

export default class ReportExcel {
    constructor(data, title, languageList) {
        this.title = title;
        this.data = data;
        this.languageList = languageList;

        this.worksheet = {};
        this.currentRowIndex = DATA_ROW_INDEX;
        this.mergeConfig = [];
        this.columnConfigs = this._getcolumnConfigs();
    }

    export () {
        Excel.process(() => {
            this._createReportTitle();
            this._createHeader();
            this._createBodyData();
            this._createExcelConfig();

            Excel.export(this.worksheet, this.title);
        })
    }

    _createReportTitle() {
        this.mergeConfig.push({
            s: {
                r: REPORT_TITLE_ROW_INDEX,
                c: 0
            },
            e: {
                r: REPORT_TITLE_ROW_INDEX,
                c: TOTAL_COLUMN - 1
            }
        });
        const address = Excel.getCellAddress({
            c: 0,
            r: REPORT_TITLE_ROW_INDEX
        });
        this.worksheet[address] = {
            t: "s",
            v: this.title,
            s: titleCellStyle
        }
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
        let value = fieldName === fields.Index ? rowIndex + 1 : item[fieldName];

        if (fieldName === fields.BalanceBf) {
            value = value === null ? resource.NA : formatDecimal(value);
        } else if (fieldName === fields.Member) {
            value = value === null ? resource.NA : format(value);
        } else if (fieldName === fields.Language) {
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
            c: TOTAL_COLUMN
        });

        this.worksheet["!merges"] = this.mergeConfig;
        this.worksheet["!ref"] = `${cellStart}:${cellEnd}`;
        this.worksheet['!cols'] = [{
            wpx: 70
        }, {
            wpx: 200
        }, {
            wpx: 150
        }, {
            wpx: 170
        }, {
            wpx: 170
        }, {
            wpx: 150
        }, {
            wpx: 250
        }, {
            wpx: 200
        }, {
            wpx: 200
        }, {
            wpx: 200
        }, {
            wpx: 250
        }, {
            wpx: 200
        }, {
            wpx: 200
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
            field: fields.Status,
            [MAIN_SECTION_STYLE]: bodyCellStyle.center
        }, {
            field: fields.FirstName,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left
        }, {
            field: fields.LastName,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left
        }, {
            field: fields.Language,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left
        }, {
            field: fields.Email,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left
        }, {
            field: fields.RegistrationDateText,
            [MAIN_SECTION_STYLE]: bodyCellStyle.center
        }, {
            field: fields.ApprovalDateText,
            [MAIN_SECTION_STYLE]: bodyCellStyle.center
        }, {
            field: fields.CasinoDateText,
            [MAIN_SECTION_STYLE]: bodyCellStyle.center
        }, {
            field: fields.CommissionGroup,
            [MAIN_SECTION_STYLE]: bodyCellStyle.center
        }, {
            field: fields.BalanceBf,
            [MAIN_SECTION_STYLE]: bodyCellStyle.right
        }, {
            field: fields.Member,
            [MAIN_SECTION_STYLE]: bodyCellStyle.right
        }]
    }
}