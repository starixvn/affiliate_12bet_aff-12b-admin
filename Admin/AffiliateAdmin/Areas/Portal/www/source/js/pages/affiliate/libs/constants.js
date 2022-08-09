import { getVirtualUrl } from "../../../modules/utils"

export const menuKey = {
    affiliateInfo: 'affiliateInfo',
    paymentRequest: 'paymentRequest',
    commGroup: 'commGroup',
    emailHistory: 'emailHistory',
    feedbackReport: 'feedbackReport',
    massAdjustment: 'massAdjustment',
    viewLog: 'viewLog',
    procReqeust: 'procRequest'
}

export const relativePath = {
    affiliateInfo: getVirtualUrl() + 'Portal/Affiliate',
    paymentRequest: getVirtualUrl() + 'Portal/PaymentRequest',
    commGroup: getVirtualUrl() + 'Portal/CommissionGroup',
    emailHistory: getVirtualUrl() + 'Portal/EmailHistory',
    feedbackReport: getVirtualUrl() + 'Portal/FeedbackReport',
    massAdjustment: getVirtualUrl() + 'Portal/Adjustment',
    viewLog: getVirtualUrl() + 'Portal/ViewLog',
    procRequest: getVirtualUrl() + 'Portal/ProcessingRequest'
}