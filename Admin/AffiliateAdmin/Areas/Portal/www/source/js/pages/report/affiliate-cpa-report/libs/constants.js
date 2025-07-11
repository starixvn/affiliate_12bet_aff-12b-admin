import { getFirstDayOfMonth, getLastDayOfThisMonth, getToDay } from "../../../../modules/utils";

export const totalText = 'Total';
export const reportName = 'PLAYER GGR REPORT ';

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


export const defaultParams = {
    username: '',
    dealName: '',
    viewBy: 1,
    fromDate: getFirstDayOfMonth(),
    toDate: getLastDayOfThisMonth(),
    sortType: 1,
    sortDirectionType: false,
    pageIndex: 1,
    pageSize: 20
}

export const defaultGgrProductReport = { Turnover: 0, Ggr: 0 };

export const viewByOptions = [
    {
        label: 'Active Qualified Date',
        value: 1
    },
    {
        label: 'Holding Qualified Date',
        value: 2
    },
    {
        label: 'Approval Date',
        value: 3
    }
]