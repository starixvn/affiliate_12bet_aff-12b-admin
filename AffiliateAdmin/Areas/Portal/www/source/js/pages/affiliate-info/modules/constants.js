import { globalResource } from '../../../modules/globalResource';

export const statusList = {
    Favorite: "Favorite",
    Pending: "Pending",
    Approved: "Approved",
    Disapproved: "Disapproved",
    Closed: "Closed"
}

export const defaultSearchBy = 0;
export const defaultSortBy = 1;

export const sortByList = {
    Username: 1,
    RegistrationDate: 2,
    ApprovalDate: 3,
    BalanceBF: 4,
    Member: 5
}

export const headerText = {
    Index: '#',
    UserName: 'Username',
    Status: 'Status',
    FirstName: 'First Name',
    LastName: 'Last Name',
    Language: 'Language',
    Email: 'Email',
    RegistrationDateText: 'Registration Date',
    ApprovalDateText: 'Approval Date',
    CasinoDateText: 'Casino Date',
    CommissionGroup: 'Commission Group',
    BalanceBf: 'Balance B/F',
    Member: 'Member',
    Password: 'Password'
}

export const resource = {
    Search: globalResource.Search,
    Space: ' - ',
    ReportName: 'AFFILIATE REPORT',
    AllCommGroups: 'All Comm Groups',
    AllLanguages: 'All Languages',
    AllStatuses: 'All Statuses',
    UsernameContains: 'USERNAME CONTAINS ',
    EmailContains: 'EMAIL CONTAINS ',
    SearchBy: 'Search By',
    SortBy: 'Sort By',
    AZ: 'A-Z',
    Apostrophe: "'",
    NA: 'N/A'
}