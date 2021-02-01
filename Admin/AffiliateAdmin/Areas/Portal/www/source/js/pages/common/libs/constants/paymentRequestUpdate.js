import { globalResource } from '../../../../modules/globalResource';

export { globalResource }

export const errorList = {
    NotChoose: 'You must choose Approved or Rejected for this request!',
    UpdateFailed: 'Update transaction failed!',
}

export const pending = 'Pending';

export const statusList = {
    [pending]: 'Processing',
    Approved: 'Approved',
    Rejected: 'Rejected',
    Cancelled: 'Cancelled',
}

export const resource = {
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
}