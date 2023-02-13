import { getVirtualUrl } from "../../../modules/utils"

export const menuKey = {
    affiliateDomain: 'Affiliate Domain',
    memberDomains: '12Bet Domains',
    domainControl: 'Domain Control'
}

export const relativePath = {
    affiliateDomain: getVirtualUrl() + 'Portal/Domain',
    memberDomains: getVirtualUrl() + 'Portal/MemberDomains',
    domainControl: getVirtualUrl() + 'Portal/DomainControl'
}