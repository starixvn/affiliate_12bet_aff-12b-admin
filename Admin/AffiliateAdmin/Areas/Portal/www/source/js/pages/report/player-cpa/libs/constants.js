export const statusOptions = [
    { value: '-1', label: 'All Statuses' },
    { value: '2', label: 'Eligible' },
    { value: '3', label: 'Approved' },
    { value: '1', label: 'Ineligible' },
    { value: '4', label: 'Rejected' },
    { value: '5', label: 'Quota Exceeded' },
    { value: '0', label: 'Pending' },
]

export const viewByOptions = [
    { value: '1', label: 'Active Qualified Date' },
    { value: '2', label: 'Holding Qualified Date' },
    { value: '3', label: 'Approval Date' },
]

export const MAX_MONTH_DIFF = 6;

export const DATE_FORMAT = 'MM/DD/YYYY';

const now = new Date();
const currentMonth = now.getMonth() + 1;
const currentYear = now.getFullYear();

export const defaultParams = {
    userName: '',
    custName: "",
    dealName: "",
    statusId: -1,
    viewBy: '1',
    fromDate: `${currentMonth}/01/${currentYear}`,
    //toDate: `${currentMonth}/${now.getDate()}/${currentYear}`,
    toDate: `12/31/2025`,
    pageSize: 20,
    pageIndex: 1,
    sortColumn: 'AffUserName',
    isAscending: true
}

export const defaultGeneralData = {
    totalPlayers: 0,
    totalAffiliates: 0,
    totalDeals: 0,
    totalCpaStatus: 0,
    totalStatus: 0,
}

export const TotalText = "Total (all pages)"