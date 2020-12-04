import { Claim, claimResource } from '../../../modules/claim';
import { globalResource } from '../../../modules/globalResource';

export { globalResource };

export const statusList = {
    Favorite: "Favorite",
    Pending: "Pending",
    Approved: "Approved",
    Disapproved: "Disapproved",
    Closed: "Closed"
}

export const defaultSortBy = 1;

export const defaultSelectedColumnNumber = 2;

export const sortByList = {
    'Username': {value: 1},
    'Registration Date': { value: 2},
    'Approval Date': { value : 3},
    'Balance B/F': { value : 4 },
    'Member': { value: 5, renderClaim : new Claim(claimResource.AffMember)}
}

export const headerText = {
    Index: { label: globalResource.Index },
    UserName: { label: 'Username' },
    Status: { label: 'Status' },
    FirstName: { label: 'First Name' },
    LastName: { label: 'Last Name' },
    Language: { label: 'Language' },
    Phone: { label: 'Mobile Phone', renderClaim: new Claim(claimResource.PhoneNumber) },
    Email: { label: 'Email', renderClaim: new Claim(claimResource.Email) },
    RegistrationDateText: { label: 'Registration Date' },
    ApprovalDateText: { label: 'Approval Date' },
    CasinoDateText: { label: 'Casino Date' },
    CommissionGroup: { label: 'Commission Group', renderClaim: new Claim(claimResource.AffCommGroup)},
    BalanceBf: { label: 'Balance B/F' },
    Currency: { label: 'Aff. Currency' },
    PlayerUserName: { label: 'Aff. Player', renderClaim: new Claim(claimResource.PaymentInfo) },
    PlayerCurrency: { label: 'Aff. Player Currency' },
    Member: { label: 'Member', renderClaim: new Claim(claimResource.AffMember) },
    Password: { label: 'Password' },
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
    AffIdContains: 'AFFILIATE ID ',
    SearchBy: 'Search By',
    SortBy: 'Sort By',
    AZ: 'A-Z',
    Apostrophe: "'",
    NA: 'N/A',
    DisplayedColumns: 'Displayed Columns'
}

export const regExp = {
    CheckAffId: "(^[1-9])(\\d*)$"
}