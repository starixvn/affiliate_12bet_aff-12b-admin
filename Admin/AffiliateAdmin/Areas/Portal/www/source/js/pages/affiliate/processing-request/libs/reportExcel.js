import Excel from '../../../../modules/excel';
import {
    htmlDecode
} from '../../../../modules/htmlHelper';
import {
    titleCellStyle,
    headerCellStyle,
    bodyCellStyle
} from '../../../../modules/excelStyleConstant';
import {
    headerText,
    resource
} from './constants';
import {
    format,
    formatDecimal,
} from '../../../../modules/numberFormat';
import { formatDateTime } from '../../../../modules/utils';

import { defaultDatetimeFormat, defaultDateFormat } from '../../../../modules/constants';

const REPORT_TITLE_ROW_INDEX = 0;
const HEADER_ROW_INDEX = 1;
const DATA_ROW_INDEX = 2;

const TOTAL_COLUMN = 10;

const MAIN_SECTION_STYLE = 'mainSectionStyle';

const fields = {
    Index: 'Index',
    UserName: 'UserName',
    CreatedBy: 'CreatedBy',
    CreationDate: 'CreationDate',
    LastModified: 'LastModified',
    Submitted: 'SubmittedDate',
    Deadline: 'Deadline',
    ApprovedBy: 'ApprovedBy',
    ApprovalDate: 'ApprovalDate',
    Status: 'Status'
}

export default class ReportExcel {
    constructor(data, title) {
        this.title = title;
        this.data = data;

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
                c: 10
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
        let columnIndex = 0;
        for (let i = 0; i < TOTAL_COLUMN; i++) {
            const fieldName = this.columnConfigs[i].field;

            let address = Excel.getCellAddress({
                c: columnIndex,
                r: HEADER_ROW_INDEX
            });
            this.worksheet[address] = {
                t: "s",
                v: headerText[fieldName],
                s: headerCellStyle
            }

            columnIndex++;

        }
    }

    _createBodyData() {
        const mainData = this.data.MainData;
        for (let i = 0; i < mainData.length; i++) {
            const item = mainData[i];

            let columnIndex = 0;
            for (let j = 0; j < TOTAL_COLUMN; j++) {
                const columnConfig = this.columnConfigs[j];

                const address = Excel.getCellAddress({
                    c: columnIndex,
                    r: this.currentRowIndex
                });

                this.worksheet[address] = this._buildDataRow(item, MAIN_SECTION_STYLE, columnConfig, i);
                columnIndex++;
            }
            this.currentRowIndex++;
        }
    }

    _buildDataRow(item, sectionStyleName, columnConfig, rowIndex) {
        const fieldName = columnConfig.field;
        let value = fieldName === fields.Index ? rowIndex + 1 : item[fieldName];
        if (fieldName === fields.ApprovalDate || fieldName === fields.CreationDate || fieldName === fields.LastModified || fieldName === fields.Submitted)
        {
            value = formatDateTime(value, defaultDatetimeFormat);
        }
        if (fieldName === fields.Deadline){
            value = formatDateTime(value, defaultDateFormat);
        }
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

        this.worksheet["!merges"] = this.mergeConfig;
        this.worksheet["!ref"] = `${cellStart}:${cellEnd}`;
        this.worksheet['!cols'] = this.columnConfigs.reduce((result, currentItem) => {
            result.push({
                wpx: currentItem.width
            });
            return result;
        }, []);
    }

    _getcolumnConfigs() {
        return [{
            field: fields.Index,
            [MAIN_SECTION_STYLE]: bodyCellStyle.right,
            width: 70
        }, {
            field: fields.UserName,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left,
            width: 200
        }, {
            field: fields.CreatedBy,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left,
            width: 150
        }, {
            field: fields.CreationDate,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left,
            width: 150
        }, {
            field: fields.LastModified,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left,
            width: 170
        }, {
            field: fields.Submitted,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left,
            width: 170
        }, {
            field: fields.Deadline,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left,
            width: 150
        }, {
            field: fields.ApprovedBy,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left,
            width: 150
        }, {
            field: fields.ApprovalDate,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left,
            width: 250
        }, {
            field: fields.Status,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left,
            width: 200
        }]
    }
}