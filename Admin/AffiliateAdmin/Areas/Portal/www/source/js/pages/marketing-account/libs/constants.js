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

export const defaultPersonalInfo = {
    firstName: '',
    lastName: '',
    gender: 0,
    email: '',
    phone: ''
}

export const defaultAdditionalInfo = {
    address: '',
    city: '',
    state: '',
    postcode: '',
    country: '',
    question: '',
    answer: ''
}

export const defaultGeneralInfo = {
    hasData: false,
    paymentMethods: [],
    minimumPayment: 0,
    targetCountryCode: '',
    language: '',
    mailOption: true,
    hasHostAffPersonalInfoPermission: false
}

export const defaultWebsiteInfo = {
    siteUrl: '',
    moreSiteUrls: [],
    noSiteDescription: "",
    hasWebSite: false
}

export const defaultPaymentInfo = {
    preferredPayment: '',
    playerUsername: '',
    moneybookersEmail: '',
    bankName: '',
    bankAddress: '',
    bankCode: '',
    bankAccountName: '',
    bankAccountNumber: '',
}