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
    VisibleAdmin: 'Visible only to admin',

    AffiliateNote: 'Affiliate Note',
    VisibleAffiliate: 'Visible also to affiliate',

    Message: 'You must choose Approved or Rejected for this request!',
    UpdateFailed: 'Update transaction failed!',
    ApprovedLowerCase: 'approved',
    RejectedLowerCase: 'rejected',
    PendingLowerCase: 'pending'
}

const UserName = 0;
const Others = 1;
export const searchByList = { 
    [UserName]: 'User Name',
    [Others]: 'Others'
}

export const statusList = {
    Pending: 'Processing',
    Approved: 'Approved',
    Rejected: 'Rejected',
    Cancelled: 'Cancelled'
}