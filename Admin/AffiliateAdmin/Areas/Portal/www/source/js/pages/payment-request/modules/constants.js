import { globalResource } from '../../../modules/globalResource';

export { globalResource }

export const headerText = {
    Index: '#',
    TrxnId: 'Trxn.ID',
    Creator: 'Creator'
}

export const resource = {
    SearchBy: 'Search By',

    AccountInformation: 'Account Information',
    UserName: 'User Name',
    RequestedDate: 'Requested Date',
    EmailAddress: 'Email Address',
    FirstName: 'First Name',
    LastName: 'Last Name',

    PaymentInformation: 'Payment Information',
    PaymentMethod: 'Payment Method',
    Amount: 'Amount',
    Status: 'Status',
    UserName12Bet: '12BET User Name',
    CompletedDate: 'Completed Date',
    UpdatedBy: 'Updated By',

    AdminNote: 'Admin Note',
    VisibleOnly: 'Visible only to admin',

    AffiliateNote: 'Affiliate Note',
    VisibleAlso: 'Visible also to affiliate',

    Message: 'You must choose Approved or Rejected for this request!'
}

export const searchByArray = [ 'User Name', 'Others' ]

export const statusList = {
    Processing: 'Pending',
    Approved: 'Approved',
    Rejected: 'Rejected',
    Cancelled: 'Cancelled'
}