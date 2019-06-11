import { minDate } from  '../../../modules/constants';
import { globalResource } from '../../../modules/globalResource';

export { minDate, globalResource }

export const all = -1;
const sender = 0;
const email = 1;
const affiliate = 2;
const unknownAffiliate = -2

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
}

export const headerText = {
    Index: '#',
    Sender: 'Sender',
    Affiliate: 'Affiliate',
    Subject: 'Subject',
    EmailAddress: 'Email Address',
    SentDate: 'Sent Date',
}