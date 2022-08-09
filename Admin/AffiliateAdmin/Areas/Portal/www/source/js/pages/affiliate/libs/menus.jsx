import React from 'react'
import { Claim, claimResource } from '../../../modules/claim';
import { menuKey, relativePath } from './constants';

const AffInfo = React.lazy(() => import(/* webpackChunkName: "affiliateInfo" */ '../affiliate-info/index'));
const PaymentRequest = React.lazy(() => import(/* webpackChunkName: "paymentRequest" */ '../payment-request/index'));
const CommGroup = React.lazy(() => import(/* webpackChunkName: "commissionGroup" */ '../commission-group/index'));
const EmailHistory = React.lazy(() => import(/* webpackChunkName: "emailHistory" */ '../email-history/index'));
const FeedbackReport = React.lazy(() => import(/* webpackChunkName: "feedbackReport" */ '../feedback-report/index'));
const MassAdjustment = React.lazy(() => import(/* webpackChunkName: "massAdjustment" */ '../mass-adjustment/index'));
const AllAffViewLog = React.lazy(() => import(/* webpackChunkName: "allAffViewLog" */ '../new-view-log/index'));
const ProcRequest = React.lazy(() => import(/* webpackChunkName: "proccessRequest" */ '../processing-request/index'));

export const menus = [
    { key: menuKey.affiliateInfo, label: 'Affiliate Info', href: relativePath.affiliateInfo, component: AffInfo, renderClaim: new Claim(claimResource.AffiliateInfo) },
    { key: menuKey.paymentRequest, label: 'Payment Request', href: relativePath.paymentRequest, component: PaymentRequest, renderClaim: new Claim(claimResource.PaymentRequest) },
    { key: menuKey.commGroup, label: 'Comm Group', href: relativePath.commGroup, component: CommGroup, renderClaim: new Claim(claimResource.CommissionGroup) },
    { key: menuKey.emailHistory, label: 'Email History', href: relativePath.emailHistory, component: EmailHistory, renderClaim: new Claim(claimResource.EmailHistory) },
    { key: menuKey.feedbackReport, label: 'Feedback Report', href: relativePath.feedbackReport, component: FeedbackReport, renderClaim: new Claim(claimResource.FeedbackReport) },
    { key: menuKey.massAdjustment, label: 'Adjustment', href: relativePath.massAdjustment, component: MassAdjustment, renderClaim: new Claim(claimResource.AffAdjustment, claimResource.WritePermission) },
    { key: menuKey.viewLog, label: 'View Log', href: relativePath.viewLog, component: AllAffViewLog, renderClaim: new Claim(claimResource.ViewLog) },
    { key: menuKey.procReqeust, label: 'Processing Request', href: relativePath.procRequest, component: ProcRequest, renderClaim: new Claim(claimResource.ProcRequest) }
]
