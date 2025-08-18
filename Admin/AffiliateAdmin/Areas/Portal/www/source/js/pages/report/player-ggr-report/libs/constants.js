export const totalText = 'Total';
export const reportName = 'PLAYER GGR REPORT ';
export const maxPageSize = 2000;

// Excel generation limits to prevent browser crashes
// Conservative limits to avoid JavaScript array size limitations
export const EXCEL_MAX_ROWS = 8000; // Maximum rows per Excel sheet (safe limit)
export const EXCEL_MAX_CELLS = 1500000; // Maximum cells per Excel sheet (1.5M cells - safe limit)

export const mappingColumns = {
    AffName: 'AffName',
    PlayerName: 'PlayerName',
    RegistrationDateText: 'RegistrationDateText',
    FdmDateText: 'FdmDateText',
    AllSelectedProducts: 'AllSelectedProducts',
    TotalTurnover: 'TotalTurnover',
    TotalGgr: 'TotalGgr',
    Turnover: 'Turnover',
    Ggr: 'Ggr'
}

export const headerText = {
    AffName: 'Affiliate',
    PlayerName: 'Player',
    RegistrationDateText: 'Registration Date',
    FdmDateText: 'FTD Date',
    AllSelectedProducts: 'All Selected Products',
    TotalTurnover: 'Turnover',
    TotalGgr: 'GGR',
    Turnover: 'Turnover',
    Ggr: 'GGR'
}

export const defaultGgrProductReport = { Turnover: 0, Ggr: 0 };