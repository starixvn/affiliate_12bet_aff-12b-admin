import { globalResource } from '../../../modules/globalResource';

export { globalResource };

export const allStatuses = 'Approved, Suspended';
export const statusList = {
    [allStatuses]: 'All Statuses',
    Approved: 'Approved',
    Suspended: 'Suspended'
}

export const regExp = {
    CheckAffCode: "(^[1-9])(\\d*)$"
}

export const headerText = {
    Index: globalResource.Index,
    UserName: 'Username',
    AffCode: 'Affiliate Code',
    Status: 'Status',
    TargetCountryCode: 'Target Country',
    Language: 'Language',
    RegistrationDateText: 'Registration Date',
    CreatedBy: 'Created by'
}