import moment from "moment";

export const statusOptions = [
    { value: -1, label: 'All Statuses' },
    { value: 2, label: 'Eligible' },
    { value: 3, label: 'Approved' },
    { value: 1, label: 'Ineligible' },
    { value: 4, label: 'Rejected' },
    { value: 5, label: 'Quota Exceeded' },
    { value: 0, label: 'Pending' },
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
    toDate: `${currentMonth}/${now.getDate()}/${currentYear}`,
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


export const headerText = {
    No: '#',
    Affiliate: 'Affiliate',
    Player: 'Player',
    RegistrationDate: 'Registration Date',
    DealName: 'Deal Name',
    PaymentFrequency: 'Payment Frequency',
    FTD: 'FTD',
    TotalDeposit: 'Total Deposit',
    Turnover: 'Turnover',
    ActiveQualifiedDate: 'Active Qualified Date',
    CPAAmount: 'CPA Amount',
    BetTicket: 'Bet Ticket',
    HoldingQualifiedDate: 'Holding Qualified Date',
    HoldingExpiredDate: 'Holding Expired Date',
    EstimatedCPAAmount: 'Estimated CPA Amount',
    ApprovedCPAAmount: 'Approved CPA Amount',
    CPAStatus: 'CPA Status',
    ApprovalDate: 'Approval Date'
};

export const dataKey = {
    No: 'no',
    Affiliate: 'affUserName',
    Player: 'custUserName',
    RegistrationDate: 'registeredDate',
    DealName: 'dealName',
    PaymentFrequency: 'scheduleName',
    FTD: 'ftd',
    TotalDeposit: 'totalDeposit',
    Turnover: 'isTO',
    ActiveQualifiedDate: 'aqd',
    CPAAmount: 'cpaAmount',
    BetTicket: 'betTicket',
    HoldingQualifiedDate: 'hqd',
    HoldingExpiredDate: 'hed',
    EstimatedCPAAmount: 'estimatedCPAAmount',
    ApprovedCPAAmount: 'approvedCPAAmount',
    CPAStatus: 'cpaStatus',
    ApprovalDate: 'cpaDate'
};

export const row1 = [
    {
        title: headerText.No,
        width: 60,
        sortable: false,
        rowSpan: 2,
        field: dataKey.No
    },
    {
        title: headerText.Affiliate,
        width: 150,
        sortable: true,
        sortColumn: "AffUserName",
        rowSpan: 2,
        field: dataKey.Affiliate
    },
    {
        title: headerText.Player,
        width: 150,
        sortable: true,
        sortColumn: "CustUserName",
        rowSpan: 2,
        field: dataKey.Player
    },
    {
        title: headerText.RegistrationDate,
        width: 150,
        sortable: true,
        sortColumn: "RegisteredDate",
        rowSpan: 2,
        field: dataKey.RegistrationDate
    },
    {
        title: headerText.DealName,
        width: 150,
        sortable: true,
        sortColumn: "DealName",
        rowSpan: 2,
        field: dataKey.DealName
    },
    {
        title: headerText.PaymentFrequency,
        width: 120,
        sortable: true,
        sortColumn: "ScheduleName",
        rowSpan: 2,
        field: dataKey.PaymentFrequency
    },
    {
        title: "Active Period",
        sortable: false,
        columnSpan: 5,
        width: 150 * 5,
    },
    {
        title: "Holding Period",
        sortable: false,
        columnSpan: 4,
        width: 150 * 4,
    },
    {
        title: "Approval Period",
        sortable: false,
        columnSpan: 3,
        width: 150 * 3
    }
];

export const row2 =[
    {
        title: headerText.FTD,
        width: 100,
        sortable: true,
        sortColumn: "FTD",
        field: dataKey.FTD
    },
    {
        title: headerText.TotalDeposit,
        width: 150,
        sortable: true,
        sortColumn: "TotalDeposit",
        field: dataKey.TotalDeposit
    },
    {
        title: headerText.Turnover,
        width: 150,
        sortable: true,
        sortColumn: "IsTO",
        field: dataKey.Turnover
    },
    {
        title: headerText.ActiveQualifiedDate,
        width: 150,
        sortable: true,
        sortColumn: "AQD",
        field: dataKey.ActiveQualifiedDate
    },
    {
        title: headerText.CPAAmount,
        width: 150,
        sortable: true,
        sortColumn: "CPAAmount",
        field: dataKey.CPAAmount
    },
    {
        title: headerText.BetTicket,
        width: 150,
        sortable: true,
        sortColumn: "HoldingTickets,HoldingBetTickets",
        field: dataKey.BetTicket
    },
    {
        title: headerText.HoldingQualifiedDate,
        width: 150,
        sortable: true,
        sortColumn: "HQD",
        field: dataKey.HoldingQualifiedDate
    },
    {
        title: headerText.HoldingExpiredDate,
        width: 150,
        sortable: true,
        sortColumn: "HED",
        field: dataKey.HoldingExpiredDate
    },
    {
        title: headerText.EstimatedCPAAmount,
        width: 150,
        sortable: true,
        sortColumn: "EstimatedCPAAmount",
        field: dataKey.EstimatedCPAAmount
    },
    {
        title: headerText.ApprovedCPAAmount,
        width: 150,
        sortable: true,
        sortColumn: "ApprovedCPAAmount",
        field: dataKey.ApprovedCPAAmount
    },
    {
        title: headerText.CPAStatus,
        width: 150,
        sortable: true,
        sortColumn: "CPAStatus",
        field: dataKey.CPAStatus
    },
    {
        title: headerText.ApprovalDate,
        width: 150,
        sortable: true,
        sortColumn: "CPADate",
        field: dataKey.ApprovalDate
    }
]

export const headerRows = [row1, row2]

export const dateFields = [
    dataKey.RegistrationDate,
    dataKey.ActiveQualifiedDate,
    dataKey.HoldingQualifiedDate,
    dataKey.HoldingExpiredDate,
    dataKey.ApprovalDate
]

export const decimalFields = [
    dataKey.FTD,
    dataKey.TotalDeposit,
    dataKey.CPAAmount,
    dataKey.EstimatedCPAAmount,
    dataKey.ApprovedCPAAmount
]
