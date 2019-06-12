import { minDate } from '../../../modules/constants';
import { globalResource } from '../../../modules/globalResource';

export { minDate, globalResource }

export const all = -1;
export const sender = 0;
export const email = 1;
export const affiliate = 2;
export const unknownAffiliate = -2

export const searchByList = {
    [all]: 'All',
    [sender]: 'Sender',
    [email]: 'Email',
    [affiliate]: 'Affiliate',
    [unknownAffiliate]: 'Unknown Affiliate',
}

export const resource = {
    Delete: 'Delete',
    Select: 'Select:',
    EmailDetail: 'Email Detail',
    Subject: 'Subject:',
    SentDate: 'Sent Date:',
    From: 'From:',
    Content: 'Content:',
}

export const headerText = {
    Index: '#',
    Sender: 'Sender',
    Affiliate: 'Affiliate',
    Subject: 'Subject',
    EmailAddress: 'Email Address',
    SentDate: 'Sent Date',
}

export const messages = {
    PleaseSelect: 'Please select items to delete',
    AreYouSureDeleteEmails: 'Are you sure to delete the selected emails?',
    DeleteEmails: 'Delete Emails Successfully',
    AreYouSureDeleteEmail: 'Are you sure to delete the selected email?',
    DeleteEmail: 'Delete Email Successfully',
}