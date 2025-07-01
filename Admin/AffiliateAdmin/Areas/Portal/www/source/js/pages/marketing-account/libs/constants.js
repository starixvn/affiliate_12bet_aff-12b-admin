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
    CreatedBy: 'Created By',
    Action: 'Action'
}

export const defaultGeneralInfo = {
    affType: 0,
    groupId: '',
    preferredLanguage: '',
    targetCountryName: '',
    targetCountryCode: '',
    affId: 0,
    affParentName: '',
    affCode: '',
    userName: '',
    mailOption: false
}

export const defaultPersonalInfo = {
    firstName: '',
    lastName: '',
    gender: false,
    email: '',
    phone: '',
    contactApps: ''
}

export const defaultAdditionalInfo = {
    address: '',
    city: '',
    state: '',
    postal: '',
    country: '',
    question: '',
    questionId: 0, 
    answer: ''
}

export const defaultWebsiteInfo = {
    siteUrl: '',
    moreSiteUrls: '',
    noSiteDesc: "",
    hasWebSite: false
}

export const defaultPaymentInfo = {
    preferredPayment: '',
    playerCustId: -1,
    playerUsername: '',
    playerCurrency: '',
    playerCurrencyId: 0
}

export const defaultFilterParams = {
    "ParentAffName": "",
    "Status": "",
    "IsFromHome": false,
    "GroupIds": "all",
    "TargetCountryCode": "",
    "Language": "",
    "UserName": "",
    "Email": "",
    "SortBy": 1,
    "SortDirectionType": false,
    "AffCode": "",
    "AffPlayer": "",
    "PageIndex": 1,
    "PageSize": 20,
    "AffCategoryIds": "2"
}