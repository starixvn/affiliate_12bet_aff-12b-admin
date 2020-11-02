import { getVirtualUrl } from "../../../modules/utils"

export const menuKey = {
    affiliateInfo: 'affiliateInfo',
    paymentRequest: 'paymentRequest',
    commGroup: 'commGroup',
    emailHistory: 'emailHistory'
}

export const relativePath = {
    affiliateInfo: getVirtualUrl() + 'Portal/Affiliate',
    paymentRequest: getVirtualUrl() + 'Portal/PaymentRequest',
    commGroup: getVirtualUrl() + 'Portal/CommissionGroup',
    emailHistory: getVirtualUrl() + 'Portal/EmailHistory'
}