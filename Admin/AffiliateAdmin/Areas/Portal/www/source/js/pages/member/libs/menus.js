import React from 'react'
import { Claim, claimResource } from '../../../modules/claim';
import { menuKey, relativePath } from '../libs/constants';

const AffMember = React.lazy(() => import(/* webpackChunkName: "memberList" */ '../member-list/index'));
const ActiveMember = React.lazy(() => import(/* webpackChunkName: "activeMember" */ '../active-member/index'));

export const menus = [
    {key: menuKey.affMember, label: 'Member', href: relativePath.affMember, component: AffMember, renderClaim: new Claim(claimResource.AffMember)},
    {key: menuKey.activeMember, label: 'Active member', href: relativePath.activeMember, component: ActiveMember, renderClaim: new Claim(claimResource.ActiveMembers) }
]