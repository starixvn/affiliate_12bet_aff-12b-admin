import { resolveClientUrl } from "../../../modules/utils"

export const menuKey = {
    affiliateDomain: 'Affiliate Domain',
    memberDomains: '12Bet Domains',
    domainControl: 'Domain Control',
    subAffiliateDomain: 'Sub Affiliate Domain',
    specialDomainControl: 'Special Domain Control'
}

export const relativePath = {
    affiliateDomain: resolveClientUrl('Portal/Domain'),
    memberDomains: resolveClientUrl('Portal/MemberDomain'),
    domainControl: resolveClientUrl('Portal/DomainControl'),
    subAffiliateDomain: resolveClientUrl('Portal/SubAffiliateDomain'),
    specialDomainControl: resolveClientUrl('Portal/SpecialDomainControl')
}