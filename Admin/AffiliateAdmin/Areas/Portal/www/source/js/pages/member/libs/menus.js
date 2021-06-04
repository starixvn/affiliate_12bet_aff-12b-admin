import React from 'react'
import { Claim, claimResource } from '../../../modules/claim';
import { menuKey, relativePath } from '../libs/constants';

const AffMember = React.lazy(() => import(/* webpackChunkName: "memberList" */ '../member-list/index'));
const ActiveMember = React.lazy(() => import(/* webpackChunkName: "activeMember" */ '../active-member/index'));
const RegisteredMember = React.lazy(() => import(/* webpackChunkName: "registeredMember" */ '../registered-member/index'));

export const menus = [
    { key: menuKey.affMember, label: 'Member', href: relativePath.affMember, component: AffMember, renderClaim: new Claim(claimResource.MemberDetail) },
    { key: menuKey.activeMember, label: 'Active member', href: relativePath.activeMember, component: ActiveMember, renderClaim: new Claim(claimResource.ActiveMembers) },
    { key: menuKey.registeredMember, label: 'Registered', href: relativePath.registeredMember, component: RegisteredMember, renderClaim: new Claim(claimResource.RegisteredMembers) }
]