import { getVirtualUrl } from "../../../modules/utils"

export const menuKey = {
    affiliateInfo: 'affiliateInfo',
    paymentRequest: 'paymentRequest',
    commGroup: 'commGroup',
    emailHistory: 'emailHistory',
    massAdjustment: 'massAdjustment'
}

export const relativePath = {
    affiliateInfo: getVirtualUrl() + 'Portal/Affiliate',
    paymentRequest: getVirtualUrl() + 'Portal/PaymentRequest',
    commGroup: getVirtualUrl() + 'Portal/CommissionGroup',
    emailHistory: getVirtualUrl() + 'Portal/EmailHistory',
    massAdjustment: getVirtualUrl() + 'Portal/Adjustment'
}