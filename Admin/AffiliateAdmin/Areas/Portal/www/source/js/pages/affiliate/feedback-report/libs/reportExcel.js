import Excel from '../../../../modules/excel';
import { htmlDecode } from '../../../../modules/htmlHelper';
import {
    titleCellStyle,
    headerCellStyle,
    bodyCellStyle
} from '../../../../modules/excelStyleConstant';
import {
    resource
} from './constants';
import { formatDateTime } from '../../../../modules/utils';
import { defaultDatetimeFormat } from '../../../../modules/constants';


const REPORT_TITLE_ROW_INDEX = 0;
const HEADER_ROW_INDEX = 1;
const DATA_ROW_INDEX = 2;

const TOTAL_COLUMN = 5;

const MAIN_SECTION_STYLE = 'mainSectionStyle';

const fields = {
    Index: 'Index',
    Affiliate: 'AffName',
    Rate: 'RateName',
    SentDate: 'SentDate',
    Message: 'Message',
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

    export() {
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
        let columnIndex = 0;
        for (let i = 0; i < TOTAL_COLUMN; i++) {
            const fieldName = this.columnConfigs[i].field;
            let address = Excel.getCellAddress({
                c: columnIndex,
                r: HEADER_ROW_INDEX
            });
            this.worksheet[address] = {
                t: "s",
                v: resource[fieldName],
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

        if (fieldName === fields.SentDate) {
            value = formatDateTime(value, defaultDatetimeFormat);
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
            [MAIN_SECTION_STYLE]: bodyCellStyle.center,
            width: 70
        }, {
            field: fields.Affiliate,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left,
            width: 200
        }, {
            field: fields.SentDate,
            [MAIN_SECTION_STYLE]: bodyCellStyle.center,
            width: 150
        }, {
            field: fields.Rate,
            [MAIN_SECTION_STYLE]: bodyCellStyle.center,
            width: 150
        }, {
            field: fields.Message,
            [MAIN_SECTION_STYLE]: bodyCellStyle.left,
            width: 500
        }]
    }
}