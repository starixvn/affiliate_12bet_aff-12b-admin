import ReportExcel from '../reportExcel';
import Excel from '../../../../modules/excel';
import { headerFake } from '../headerFake';

describe('affiliate report - Report Excel ', () => {
    const TITLE_ROW_INDEX = 0;
    const HEADER_ROW_INDEX = 1;

    const INDEX_DEPOSIT_COLUMN = 8;
    const INDEX_COMM_COLUMN = 17;
    const TOTAL_COLUMNS = 25;

    const MAIN_SECTION_STYLE = 'mainSectionStyle';
    const TOTAL_SECTION_STYLE = 'totalSectionStyle';

    const isMainView = true;
    const languageList = {
        'en-US': "English",
        'vi-VN': 'Vietnamese'
    }

    const itemHaveDescription = {
        ActiveMember: 0,
        AffAdjustment: 0,
        AffId: 927841,
        AffWithdrawal: 0,
        PreviousBalanceBf: 165,
        BalanceBf: 165,
        BankingFee: 0,
        Bonus: 0,
        CommissionRate: 0,
        Deposit: 0,
        Description: "12BETAFF001",
        Earning: 0,
        NewFdmEarning: 0,
        TotalEarning: 0,
        Fdm: 0,
        MonthDate: null,
        MonthDateString: "",
        OperationalFee: 0,
        PlatformFee: 0,
        QualifiedMember: 0,
        Signup: 0,
        TotalClick: 0,
        TotalFee: 0,
        Turnover: 0,
        UniqueClick: 0,
        UserName: "12BETAFF001",
        Winlost: 0,
        Withdrawal: 0,
        PreferredLanguage: 'en-US'
    };
    const itemHaveNotDescription = {
        ActiveMember: 0,
        AffAdjustment: 0,
        AffId: 968222,
        AffWithdrawal: 0,
        PreviousBalanceBf: 803935.7199,
        BalanceBf: 803935.7199,
        BankingFee: 0,
        Bonus: 0,
        CommissionRate: 0,
        Deposit: 0,
        Description: "",
        Earning: 0,
        NewFdmEarning: 0,
        TotalEarning: 0,
        Fdm: 0,
        MonthDate: null,
        MonthDateString: "",
        OperationalFee: 0,
        PlatformFee: 0,
        QualifiedMember: 0,
        Signup: 0,
        TotalClick: 1,
        TotalFee: 0,
        Turnover: 0,
        UniqueClick: 1,
        UserName: "12BETAFF002",
        Winlost: 0,
        Withdrawal: 0,
        PreferredLanguage: 'ABC'
    }
    const data = { MainData: [itemHaveDescription, itemHaveNotDescription], Total: 2 };
    const title = 'Report Excel';

    it('export - should call Excel.process', () => {
        const instance = new ReportExcel(data, title, headerFake, !isMainView, languageList);
        instance._createTitle = jest.fn();
        instance._createHeader = jest.fn();
        instance._createBodyData = jest.fn();
        instance._createTotalRow = jest.fn();
        instance._createExcelConfig = jest.fn();

        Excel.process = jest.fn();
        Excel.export = jest.fn();

        instance.export();
        Excel.process.mock.calls[0][0]();

        expect(Excel.process).toHaveBeenCalled();
        expect(instance._createTitle).toHaveBeenCalled();
        expect(instance._createHeader).toHaveBeenCalled();
        expect(instance._createBodyData).toHaveBeenCalled();
        expect(instance._createTotalRow).toHaveBeenCalled();
        expect(instance._createExcelConfig).toHaveBeenCalled();
        expect(Excel.export).toHaveBeenCalledWith({}, "AFFILIATE REPORT Report Excel");
    });

    it('_createTitle - should call Excel.getCellAddress', () => {
        const instance = new ReportExcel(data, title, headerFake, !isMainView, languageList);
        Excel.getCellAddress = jest.fn();

        instance._createTitle();

        expect(Excel.getCellAddress).toHaveBeenCalledWith({ c: 0, r: TITLE_ROW_INDEX });
    });

    it('_createHeader - should call _buildColspanColumn with Main view', () => {
        const instance = new ReportExcel(data, title, headerFake, isMainView, languageList);
        instance._buildColspanColumn = jest.fn();
        Excel.getCellAddress = jest.fn();

        instance._createHeader();

        expect(Excel.getCellAddress).toHaveBeenCalledTimes(8);

        expect(Excel.getCellAddress.mock.calls[0][0]).toEqual({ "c": 0, "r": HEADER_ROW_INDEX });
        expect(Excel.getCellAddress.mock.calls[1][0]).toEqual({ "c": 1, "r": HEADER_ROW_INDEX });
        expect(Excel.getCellAddress.mock.calls[2][0]).toEqual({ "c": 2, "r": HEADER_ROW_INDEX });
        expect(Excel.getCellAddress.mock.calls[3][0]).toEqual({ "c": 3, "r": HEADER_ROW_INDEX });
        expect(Excel.getCellAddress.mock.calls[4][0]).toEqual({ "c": 4, "r": HEADER_ROW_INDEX });
        expect(Excel.getCellAddress.mock.calls[5][0]).toEqual({ "c": 5, "r": HEADER_ROW_INDEX });
        expect(Excel.getCellAddress.mock.calls[6][0]).toEqual({ "c": 6, "r": HEADER_ROW_INDEX });

        expect(instance._buildColspanColumn).toHaveBeenCalledTimes(2);
        expect(instance._buildColspanColumn.mock.calls[0][0]).toBe(HEADER_ROW_INDEX);
        expect(instance._buildColspanColumn.mock.calls[0][1]).toBe(INDEX_DEPOSIT_COLUMN);

        expect(instance._buildColspanColumn.mock.calls[1][0]).toBe(HEADER_ROW_INDEX);
        expect(instance._buildColspanColumn.mock.calls[1][1]).toBe(INDEX_COMM_COLUMN);
    });

    it('_createHeader - should call _buildColspanColumn with Month on Month view', () => {
        const instance = new ReportExcel(data, title, headerFake, !isMainView, languageList);
        instance._buildColspanColumn = jest.fn();
        Excel.getCellAddress = jest.fn();

        instance._createHeader();

        expect(Excel.getCellAddress).toHaveBeenCalledWith({ c: 0, r: HEADER_ROW_INDEX });

        expect(instance._buildColspanColumn).toHaveBeenCalledTimes(2);
        expect(instance._buildColspanColumn.mock.calls[0][0]).toBe(HEADER_ROW_INDEX);
        expect(instance._buildColspanColumn.mock.calls[0][1]).toBe(INDEX_DEPOSIT_COLUMN);

        expect(instance._buildColspanColumn.mock.calls[1][0]).toBe(HEADER_ROW_INDEX);
        expect(instance._buildColspanColumn.mock.calls[1][1]).toBe(INDEX_COMM_COLUMN);
    });

    it('_buildColspanColumn - should call Excel.getCellAddress with headerText not exist column', () => {
        const instance = new ReportExcel(data, title, headerFake, isMainView, languageList);
        instance.columnConfigs.push({
            field: "abc",
            mainSectionStyle: { numFmt: '#,##0.00' },
            totalSectionStyle: { font: [Object], fill: [Object], numFmt: '#,##0.00' }
        });

        Excel.getCellAddress = jest.fn();

        instance._createHeader();

        expect(Excel.getCellAddress).toHaveBeenCalledTimes(TOTAL_COLUMNS + 2);
        expect(Excel.getCellAddress.mock.calls[0][0]).toEqual({ "c": TITLE_ROW_INDEX, "r": HEADER_ROW_INDEX });
        expect(Excel.getCellAddress.mock.calls[1][0]).toEqual({ "c": TITLE_ROW_INDEX + 1, "r": HEADER_ROW_INDEX });
        expect(Excel.getCellAddress.mock.calls[2][0]).toEqual({ "c": TITLE_ROW_INDEX + 2, "r": HEADER_ROW_INDEX });
        expect(Excel.getCellAddress.mock.calls[3][0]).toEqual({ "c": TITLE_ROW_INDEX + 3, "r": HEADER_ROW_INDEX });
        expect(Excel.getCellAddress.mock.calls[4][0]).toEqual({ "c": TITLE_ROW_INDEX + 4, "r": HEADER_ROW_INDEX });
        expect(Excel.getCellAddress.mock.calls[5][0]).toEqual({ "c": TITLE_ROW_INDEX + 5, "r": HEADER_ROW_INDEX });
        expect(Excel.getCellAddress.mock.calls[6][0]).toEqual({ "c": TITLE_ROW_INDEX + 6, "r": HEADER_ROW_INDEX });
        expect(Excel.getCellAddress.mock.calls[7][0]).toEqual({ "c": TITLE_ROW_INDEX + 7, "r": HEADER_ROW_INDEX });
        expect(Excel.getCellAddress.mock.calls[8][0]).toEqual({ "c": TITLE_ROW_INDEX + 8, "r": HEADER_ROW_INDEX });
        expect(Excel.getCellAddress.mock.calls[9][0]).toEqual({ "c": TITLE_ROW_INDEX + 8, "r": HEADER_ROW_INDEX + 1 });
        expect(Excel.getCellAddress.mock.calls[10][0]).toEqual({ "c": TITLE_ROW_INDEX + 9, "r": HEADER_ROW_INDEX + 1 });
        expect(Excel.getCellAddress.mock.calls[11][0]).toEqual({ "c": TITLE_ROW_INDEX + 10, "r": HEADER_ROW_INDEX + 1 });
        expect(Excel.getCellAddress.mock.calls[12][0]).toEqual({ "c": TITLE_ROW_INDEX + 11, "r": HEADER_ROW_INDEX + 1 });
        expect(Excel.getCellAddress.mock.calls[13][0]).toEqual({ "c": TITLE_ROW_INDEX + 12, "r": HEADER_ROW_INDEX + 1 });
        expect(Excel.getCellAddress.mock.calls[14][0]).toEqual({ "c": TITLE_ROW_INDEX + 13, "r": HEADER_ROW_INDEX + 1 });
    });

    //it('_buildColspanColumn - should call Excel.getCellAddress', () => {
    //    const instance = new ReportExcel(data, title, headerFake, isMainView);
    //    Excel.getCellAddress = jest.fn();

    //    instance._buildColspanColumn(HEADER_ROW_INDEX, INDEX_DEPOSIT_COLUMN, COLSPAN_MEMBER_STATISTICS, 'Member Statistics');

    //    expect(Excel.getCellAddress).toHaveBeenCalledTimes(9);
    //    expect(Excel.getCellAddress.mock.calls[0][0]).toEqual({ "c": INDEX_DEPOSIT_COLUMN, "r": HEADER_ROW_INDEX });
    //    expect(Excel.getCellAddress.mock.calls[1][0]).toEqual({ "c": INDEX_DEPOSIT_COLUMN, "r": HEADER_ROW_INDEX + 1 });
    //    expect(Excel.getCellAddress.mock.calls[2][0]).toEqual({ "c": INDEX_DEPOSIT_COLUMN + 1, "r": HEADER_ROW_INDEX + 1 });
    //    expect(Excel.getCellAddress.mock.calls[3][0]).toEqual({ "c": INDEX_DEPOSIT_COLUMN + 2, "r": HEADER_ROW_INDEX + 1 });
    //    expect(Excel.getCellAddress.mock.calls[4][0]).toEqual({ "c": INDEX_DEPOSIT_COLUMN + 3, "r": HEADER_ROW_INDEX + 1 });
    //    expect(Excel.getCellAddress.mock.calls[5][0]).toEqual({ "c": INDEX_DEPOSIT_COLUMN + 4, "r": HEADER_ROW_INDEX + 1 });
    //    expect(Excel.getCellAddress.mock.calls[6][0]).toEqual({ "c": INDEX_DEPOSIT_COLUMN + 5, "r": HEADER_ROW_INDEX + 1 });
    //    expect(Excel.getCellAddress.mock.calls[7][0]).toEqual({ "c": INDEX_DEPOSIT_COLUMN + 6, "r": HEADER_ROW_INDEX + 1 });
    //    expect(Excel.getCellAddress.mock.calls[8][0]).toEqual({ "c": INDEX_DEPOSIT_COLUMN + 7, "r": HEADER_ROW_INDEX + 1 });
    //});

    it('_createBodyData - should call _buildDataRow, Excel.getCellAddress with Main view', () => {
        const instance = new ReportExcel(data, title, headerFake, isMainView, languageList);

        instance._buildDataRow = jest.fn();
        Excel.getCellAddress = jest.fn();

        instance._createBodyData();

        expect(Excel.getCellAddress).toHaveBeenCalledTimes(TOTAL_COLUMNS * 2);
        expect(Excel.getCellAddress.mock.calls[0][0]).toEqual({ "c": 0, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[1][0]).toEqual({ "c": 1, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[2][0]).toEqual({ "c": 2, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[3][0]).toEqual({ "c": 3, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[4][0]).toEqual({ "c": 4, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[5][0]).toEqual({ "c": 5, "r": 3 });

        expect(instance._buildDataRow).toHaveBeenCalledTimes(TOTAL_COLUMNS * 2);
        expect(instance._buildDataRow.mock.calls[0][0]).toBe(itemHaveDescription);
        expect(instance._buildDataRow.mock.calls[0][1]).toBe(MAIN_SECTION_STYLE);
        expect(instance._buildDataRow.mock.calls[0][2]).toBe(0);

        expect(instance._buildDataRow.mock.calls[1][0]).toBe(itemHaveDescription);
        expect(instance._buildDataRow.mock.calls[1][1]).toBe(MAIN_SECTION_STYLE);
        expect(instance._buildDataRow.mock.calls[1][2]).toBe(1);
    });

    it('_createBodyData - should call _buildDataRow, Excel.getCellAddress with Month on Month view', () => {
        const instance = new ReportExcel(data, title, headerFake, !isMainView, languageList);

        instance._buildDataRow = jest.fn();
        Excel.getCellAddress = jest.fn();

        instance._createBodyData();

        expect(Excel.getCellAddress).toHaveBeenCalledTimes(TOTAL_COLUMNS * 2);
        expect(Excel.getCellAddress.mock.calls[0][0]).toEqual({ "c": 0, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[1][0]).toEqual({ "c": 1, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[2][0]).toEqual({ "c": 2, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[3][0]).toEqual({ "c": 3, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[4][0]).toEqual({ "c": 4, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[5][0]).toEqual({ "c": 5, "r": 3 });

        expect(instance._buildDataRow).toHaveBeenCalledTimes(TOTAL_COLUMNS * 2);
        expect(instance._buildDataRow.mock.calls[0][0]).toBe(itemHaveDescription);
        expect(instance._buildDataRow.mock.calls[0][1]).toBe(MAIN_SECTION_STYLE);
        expect(instance._buildDataRow.mock.calls[0][2]).toBe(0);

        expect(instance._buildDataRow.mock.calls[1][0]).toBe(itemHaveDescription);
        expect(instance._buildDataRow.mock.calls[1][1]).toBe(MAIN_SECTION_STYLE);
        expect(instance._buildDataRow.mock.calls[1][2]).toBe(1);
    });

    it('_createTotalRow - should call _buildDataRow, Excel.getCellAddress with Main view', () => {
        const customTotalData = { CommissionRate: "N/A", UserName: "Total (all pages)" };
        const instance = new ReportExcel(data, title, headerFake, isMainView, languageList);
        instance._buildDataRow = jest.fn();
        Excel.getCellAddress = jest.fn();

        instance._createTotalRow();

        expect(Excel.getCellAddress).toHaveBeenCalledTimes(TOTAL_COLUMNS);
        expect(Excel.getCellAddress.mock.calls[0][0]).toEqual({ "c": 0, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[1][0]).toEqual({ "c": 1, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[2][0]).toEqual({ "c": 2, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[3][0]).toEqual({ "c": 3, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[4][0]).toEqual({ "c": 4, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[5][0]).toEqual({ "c": 5, "r": 3 });

        expect(instance._buildDataRow).toHaveBeenCalledTimes(TOTAL_COLUMNS);
        expect(instance._buildDataRow.mock.calls[0][0]).toEqual(customTotalData);
        expect(instance._buildDataRow.mock.calls[0][1]).toBe(TOTAL_SECTION_STYLE);
        expect(instance._buildDataRow.mock.calls[0][2]).toBe(0);

        expect(instance._buildDataRow.mock.calls[1][0]).toEqual(customTotalData);
        expect(instance._buildDataRow.mock.calls[1][1]).toBe(TOTAL_SECTION_STYLE);
        expect(instance._buildDataRow.mock.calls[1][2]).toBe(1);
    });

    it('_createTotalRow - should call _buildDataRow, Excel.getCellAddress with Month on Month view', () => {
        const customTotalData = { CommissionRate: "%", MonthDateString: "Total (all pages)" };
        const instance = new ReportExcel(data, title, headerFake, !isMainView, languageList);
        instance._buildDataRow = jest.fn();
        Excel.getCellAddress = jest.fn();

        instance._createTotalRow();

        expect(Excel.getCellAddress).toHaveBeenCalledTimes(TOTAL_COLUMNS);
        expect(Excel.getCellAddress.mock.calls[0][0]).toEqual({ "c": 0, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[1][0]).toEqual({ "c": 1, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[2][0]).toEqual({ "c": 2, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[3][0]).toEqual({ "c": 3, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[4][0]).toEqual({ "c": 4, "r": 3 });
        expect(Excel.getCellAddress.mock.calls[5][0]).toEqual({ "c": 5, "r": 3 });

        expect(instance._buildDataRow).toHaveBeenCalledTimes(TOTAL_COLUMNS);
        expect(instance._buildDataRow.mock.calls[0][0]).toEqual(customTotalData);
        expect(instance._buildDataRow.mock.calls[0][1]).toBe(TOTAL_SECTION_STYLE);
        expect(instance._buildDataRow.mock.calls[0][2]).toBe(0);

        expect(instance._buildDataRow.mock.calls[1][0]).toEqual(customTotalData);
        expect(instance._buildDataRow.mock.calls[1][1]).toBe(TOTAL_SECTION_STYLE);
        expect(instance._buildDataRow.mock.calls[1][2]).toBe(1);
    });

    it('_buildDataRow - should call Excel.getCellType', () => {
        const instance = new ReportExcel(data, title, headerFake, isMainView, languageList);

        Excel.getCellType = jest.fn();

        const result = instance._buildDataRow(itemHaveDescription, MAIN_SECTION_STYLE, 2);

        expect(Excel.getCellType).toHaveBeenCalledWith(0);
        expect(result).toEqual({ s: { numFmt: "#,##0" }, t: undefined, v: 0 });
    });

    it('_createExcelConfig - should call Excel.getCellAddress', () => {
        const instance = new ReportExcel(data, title, headerFake, isMainView, languageList);

        Excel.getCellAddress = jest.fn();

        instance._createExcelConfig();

        expect(Excel.getCellAddress).toHaveBeenCalledTimes(2);
        expect(Excel.getCellAddress.mock.calls[0][0]).toEqual({ "c": 0, "r": 0 });
        expect(Excel.getCellAddress.mock.calls[1][0]).toEqual({ "c": TOTAL_COLUMNS, "r": 3 });
    });

    it('_getcolumnConfigs - return correct', () => {
        const expectResult = [
            { field: "UserName", mainSectionStyle: { alignment: { horizontal: "left", vertical: "center" } }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { bold: true } }, wpx: 150 },
            { field: "PreferredLanguage", mainSectionStyle: { alignment: { horizontal: "left", vertical: "center" } }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { bold: true } }, wpx: 120 },
            { field: "TotalClick", mainSectionStyle: { numFmt: "#,##0" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { bold: true }, numFmt: "#,##0" }, wpx: 80 },
            { field: "UniqueClick", mainSectionStyle: { numFmt: "#,##0" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0" }, wpx: 80 },
            { field: "Signup", mainSectionStyle: { numFmt: "#,##0" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0" }, wpx: 80 },
            { field: "Fdm", mainSectionStyle: { numFmt: "#,##0" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0" }, wpx: 80 },
            { field: "ActiveMember", mainSectionStyle: { numFmt: "#,##0" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0" }, wpx: 100 },
            { field: "QualifiedMember", mainSectionStyle: { numFmt: "#,##0" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0" }, wpx: 120 },
            { field: "Deposit", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 120 },
            { field: "Withdrawal", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 120 },
            { field: "Turnover", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 120 },
            { field: "Winlost", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 120 },
            { field: "Bonus", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 80 },
            { field: "PlatformFee", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 100 },
            { field: "OperationalFee", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 110 },
            { field: "BankingFee", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 100 },
            { field: "TotalFee", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 100 },
            { field: "CommissionRate", mainSectionStyle: { "alignment": { horizontal: "right", vertical: "center" } }, totalSectionStyle: { alignment: { horizontal: "right", vertical: "center" }, fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true } }, wpx: 80 },
            { field: "PreviousBalanceBf", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 225 },
            { field: "Earning", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 100 },
            { field: "NewFdmEarning", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 150 },
            { field: "TotalEarning", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 100 },
            { field: "AffWithdrawal", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 100 },
            { field: "AffAdjustment", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 100 },
            { field: "BalanceBf", mainSectionStyle: { numFmt: "#,##0.00" }, totalSectionStyle: { fill: { fgColor: { rgb: "ffc4a8" }, patternType: "solid" }, font: { "bold": true }, numFmt: "#,##0.00" }, wpx: 150 }]
        const instance = new ReportExcel(data, title, headerFake, isMainView);

        const result = instance._getcolumnConfigs(headerFake, isMainView);

        expect(result).toEqual(expectResult);
    });
});