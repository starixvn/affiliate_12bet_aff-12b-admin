import React from 'react'
import { Claim, claimResource } from '../../../modules/claim';
import { menuKey, relativePath} from '../constants/key';

const AffInfo = React.lazy(() => import(/* webpackChunkName: "affiliateInfo" */ '../../affiliate-info/index'));
const PaymentRequest = React.lazy(() => import(/* webpackChunkName: "paymentRequest" */ '../../payment-request/index'));
const CommGroup = React.lazy(() => import(/* webpackChunkName: "commissionGroup" */ '../../commission-group/index'));
const EmailHistory = React.lazy(() => import(/* webpackChunkName: "emailHistory" */ '../../email-history/index'));

export const menus = [
    {key: menuKey.affiliateInfo, label: 'Affiliate Info', href: relativePath.affiliateInfo, component: AffInfo, renderClaim: new Claim(claimResource.AffInfo)},
    {key: menuKey.paymentRequest, label: 'Payment Request', href: relativePath.paymentRequest, component: PaymentRequest, renderClaim: new Claim(claimResource.Payment) },
    {key: menuKey.commGroup, label: 'Comm Group', href: relativePath.commGroup, component: CommGroup, renderClaim: new Claim(claimResource.CommGroup)},
    {key: menuKey.emailHistory, label: 'Email History', href: relativePath.emailHistory, component: EmailHistory, renderClaim: new Claim(claimResource.Email)}
]
