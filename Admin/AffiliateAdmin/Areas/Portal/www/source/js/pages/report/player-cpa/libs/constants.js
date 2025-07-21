export const statusOptions = [
    { value: '1', label: 'All Statuses' },
    { value: '2', label: 'Eligible' },
    { value: '3', label: 'Approved' },
    { value: '4', label: 'Ineligible' },
    { value: '5', label: 'Rejected' },
    { value: '6', label: 'Quota Exceeded' },
    { value: '7', label: 'Pending' },
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
    affName: '',
    playerName: "",
    dealName: "",
    cpaStatus: '',
    status: '1',
    viewBy: '1',
    fromDate: `${currentMonth}/01/${currentYear}`,
    toDate: `${currentMonth}/${now.getDate()}/${currentYear}`
}

export const defaultGeneralData = {
    totalPlayers: 0,
    totalAffiliates: 0,
    totalDeals: 0,
    totalCpaStatus: 0,
    totalStatus: 0,
}